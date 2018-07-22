using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TravianFuryBoarClient.Data.Nation;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels.Components;

namespace TravianFuryBoarClient.ViewModels
{
    class InfoViewModel : BaseViewModel
    {
        
        private string token;
        private BinaryFormatter bf;
        private string _numberVillages = "";


        public ObservableCollection<Village> Villages { get; private set; }

        public Village Village { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SyncCommand { get; private set; }

        public string NumberVillages { get => _numberVillages; set { _numberVillages = value; OnPropertyChanged("NumberVillages"); } }

        public InfoViewModel(string _token)
        {

            LoadVillages();
            SyncCommand = new RelayCommand(Sync);
            DeleteCommand = new RelayCommand(DeleteVillage);
            token = _token;
        }

        public void DeleteVillage(object obj)
        {
  using (FileStream fs = new FileStream("Villages.dat", FileMode.Open))
                {
                    try
                    {
                        Villages = (ObservableCollection<Village>)bf.Deserialize(fs);
                    }
                    catch(Exception e ){

                    }
                }
        Villages.Remove(Village);
    using (FileStream fs = new FileStream("Villages.dat", FileMode.Open))
                {
                    try
                    {
                        bf.Serialize(fs,Villages);
                    }
                    catch(Exception e ){

                    }
                }
            LoadVillages();
        }
        public void LoadVillages()
        {
            bf = new BinaryFormatter();
            if (File.Exists("Villages.dat"))
            {
                using (FileStream fs = new FileStream("Villages.dat", FileMode.Open))
                {
                    try
                    {
                        Villages = (ObservableCollection<Village>)bf.Deserialize(fs);
                    }
                    catch (Exception e) { }
                }
            }

            OnPropertyChanged("Villages");
        }
        public void Sync(object obj)
        {

            Thread thr = new Thread(() => SyncTask(obj));
            thr.Start();

        }
        public async void SyncTask(object obj)
        {
            Villages = new ObservableCollection<Village>();
            OnPropertyChanged("Villages");
            ObservableCollection<Village> NewCollection = new ObservableCollection<Village>();
            ObservableCollection<Village> OldCollection = new ObservableCollection<Village>();
            using (FileStream fs = new FileStream("Villages.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();

                OldCollection = (ObservableCollection<Village>)bf.Deserialize(fs);
            }
                int i = 0;
                foreach (var vil in OldCollection)
                {
                    NumberVillages = i.ToString() + " / " + (OldCollection.Count - 1).ToString();
                    string request = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Village:" + vil.Villageid + @"""]},""session"":""" + token + @"""}";
                    WebRequest WR = WebRequest.Create("https://ru5.kingdoms.com/api/?");
                    WR.Method = "POST";
                    Console.WriteLine(request);
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(request);
                    WR.ContentType = "application/json";
                    WR.ContentLength = byteArray.Length;

                    using (Stream dataStream = WR.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }
                    var b = new Rootobject();
                    WebResponse response = await WR.GetResponseAsync();
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            b = JsonConvert.DeserializeObject<Rootobject>(reader.ReadToEnd());

                        }
                    }
                    response.Close();
                    var player = new PlayerObject();
                    if (b.cache[0].data != null)
                    {
                        string requestPlayer = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Player:" + b.cache[0].data.playerId + @"""]},""session"":""" + token + @"""}";
                        WebRequest WRPlayer = WebRequest.Create("https://ru5.kingdoms.com/api/?");
                        WRPlayer.Method = "POST";
                        Console.WriteLine(requestPlayer);
                        byteArray = System.Text.Encoding.UTF8.GetBytes(requestPlayer);
                        WRPlayer.ContentType = "application/json";
                        WRPlayer.ContentLength = byteArray.Length;
                        using (Stream dataStream = WRPlayer.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);
                        }
                        WebResponse responsePlayer = await WRPlayer.GetResponseAsync();
                        using (Stream stream = responsePlayer.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                player = JsonConvert.DeserializeObject<PlayerObject>(reader.ReadToEnd());

                            }
                        }
                        responsePlayer.Close();

                        Village village;
                        if (b.cache[0].data != null)
                        {
                            village = new Village()
                            {

                                Name = b.cache[0].data.name,
                                Villageid = uint.Parse(b.cache[0].data.villageId),
                                ArgX = short.Parse(b.cache[0].data.coordinates.x),
                                ArgY = short.Parse(b.cache[0].data.coordinates.y),
                                Population = short.Parse(b.cache[0].data.population),
                                Nation = (Nation)Nation.Parse(typeof(Nation), b.cache[0].data.tribeId),
                                IsAbounded = int.Parse(player.cache[0].data.kingdomId) == 0
                            };
                            NewCollection.Add(village);
                        i++;
                        }
                    }
                }

            using (FileStream fs = new FileStream("Villages.dat", FileMode.Open))
            {

                bf.Serialize(fs, NewCollection);

            }

                NumberVillages = NewCollection.Count.ToString();
                LoadVillages();


            }

        }
    }


