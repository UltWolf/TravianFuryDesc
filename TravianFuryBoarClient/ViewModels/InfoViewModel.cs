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
using TravianFuryBoarClient.Views;

namespace TravianFuryBoarClient.ViewModels
{
    class InfoViewModel : BaseViewModel
    {
        
        private string token;
        private BinaryFormatter bf;
        private string _numberVillages = "";
        private string IDHero;

        private string _error;
        private string ProfileID;
        private string Key;
        private string PathToProfileDirectory;

        private string _image_1;
        private string _image_2;
        private string _image_3;
        private string _image_4;
        private string _image_5;
        private string _image_6;

        private ushort troop_1 = 0;
        private ushort troop_2 = 0;
        private ushort troop_3 = 0;
        private ushort troop_4 = 0;
        private ushort troop_5 = 0;
        private ushort troop_6 = 0;


        public ObservableCollection<Village> Villages { get; private set; }
        public string Image_1 { get => _image_1; set { _image_1 = value; OnPropertyChanged("Image_1"); } }
        public string Image_2 { get => _image_2; set { _image_2 = value; OnPropertyChanged("Image_2"); } }
        public string Image_3 { get => _image_3; set { _image_3 = value; OnPropertyChanged("Image_3"); } }
        public string Image_4 { get => _image_4; set { _image_4 = value; OnPropertyChanged("Image_4"); } }
        public string Image_5 { get => _image_5; set { _image_5 = value; OnPropertyChanged("Image_5"); } }
        public string Image_6 { get => _image_6; set { _image_6 = value; OnPropertyChanged("Image_6"); } }

        public ushort Troop_1 { get => troop_1; set { troop_1 = value; OnPropertyChanged("Troop_1"); } }
        public ushort Troop_2 { get => troop_2; set { troop_2 = value; OnPropertyChanged("Troop_2"); } }
        public ushort Troop_3 { get => troop_3; set { troop_3 = value; OnPropertyChanged("Troop_3"); } }
        public ushort Troop_4 { get => troop_4; set { troop_4 = value; OnPropertyChanged("Troop_4"); } }
        public ushort Troop_5 { get => troop_5; set { troop_5 = value; OnPropertyChanged("Troop_5"); } }
        public ushort Troop_6 { get => troop_6; set { troop_6 = value; OnPropertyChanged("Troop_6"); } }





        public Village Village { get; set; }

        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveAndCloseCommand { get; private set; }
        public ICommand SyncCommand { get; private set; }
        public ICommand ChangeValueCommand { get; private set; }
        public ICommand SetCommand { get; private set; }
        private InfoView _IV;
        public string NumberVillages { get => _numberVillages; set { _numberVillages = value; OnPropertyChanged("NumberVillages"); } }

        public InfoViewModel(InfoView IV,string _token, string UrlServer, DefaultUser DU)
        {
            _IV = IV;
            Key = DU.LastToken;
            IDHero = DU.Portrait;
            ServerUrl = DU.URLServer;
            bf = new BinaryFormatter();
            PathToProfileDirectory = Path.Combine("Profiles", IDHero);
            LoadVillages();
            SyncCommand = new RelayCommand(Sync);
            DeleteCommand = new RelayCommand(DeleteVillage);
            SaveAndCloseCommand = new RelayCommand(SaveAndClose);
            SetCommand = new RelayCommand(SetToAll);
            token = _token;
            ServerUrl = UrlServer;
            SetImagesUrl();
        
        }

        private void SetImagesUrl() {
            Image_1 = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Troops\Img\Germanics\1.png";
            Image_2 = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Troops\Img\Germanics\2.png";
            Image_3 = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Troops\Img\Germanics\3.png";
            Image_4 = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Troops\Img\Germanics\4.png";
            Image_5 = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Troops\Img\Germanics\5.png";
            Image_6 = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Troops\Img\Germanics\6.png";
        }

        private void SetToAll (object obj){
            foreach (var vil in Villages) {
                if (!vil.Original) {
                    vil.Troop_1 = Troop_1;
                    vil.Troop_2 = Troop_2;
                    vil.Troop_3 = Troop_3;
                    vil.Troop_4 = Troop_4;
                    vil.Troop_5 = Troop_5;
                    vil.Troop_6 = Troop_6;
                }
            }
        }
        private void SaveAndClose(object obj) {
            using (FileStream fs = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
            {
                try
                {
                    bf.Serialize(fs, Villages);
                }
                catch (Exception e)
                {

                }
            }
            _IV.Close();
        }
        public void DeleteVillage(object obj)
        {
  using (FileStream fs = new FileStream(PathToProfileDirectory + "\\Villages.dat" , FileMode.Open))
                {
                    try
                    {
                        Villages = (ObservableCollection<Village>)bf.Deserialize(fs);
                    }
                    catch(Exception e ){

                    }
                }
            Villages[0].Equals(Village);
         Villages. Remove(Village) ;
    using (FileStream fs = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
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
            if (File.Exists(PathToProfileDirectory + "\\Villages.dat"))
            {
                using (FileStream fs = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
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
            using (FileStream fs = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();

                OldCollection = (ObservableCollection<Village>)bf.Deserialize(fs);
            }
                int i = 0;
                foreach (var vil in OldCollection)
                {
                    NumberVillages = i.ToString() + " / " + (OldCollection.Count - 1).ToString();
                    string request = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Village:" + vil.Villageid + @"""]},""session"":""" + token + @"""}";
                    WebRequest WR = WebRequest.Create(ServerUrl + "/api/?");
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
                        WebRequest WRPlayer = WebRequest.Create(ServerUrl + "/api/?");
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
                                Population =  short.Parse(b.cache[0].data.population),
                                
                                IsAbounded = int.Parse(player.cache[0].data.kingdomId) == 0
                            };
                        if (int.Parse(b.cache[0].data.tribeId) == 1)
                        {
                            village.urlIcon = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Nation\Img\Roman.png";

                        }
                        else if (int.Parse(b.cache[0].data.tribeId) == 2)
                        {
                            village.urlIcon = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Nation\Img\Gaul.png";
                        }
                        else if (int.Parse(b.cache[0].data.tribeId) == 3)
                        {
                            village.urlIcon = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Nation\Img\Germanics.png";
                        }
                        else
                        {
                            village.urlIcon = @"C:\Users\ctrat\Projects\TravianFuryDesc\TravianFuryBoarClient\Data\Nation\Img\None.png";
                        }
                        village.Icon = new System.Web.UI.WebControls.Image() { ImageUrl = village.urlIcon };
                      
                        NewCollection.Add(village);
                        i++;
                        }
                    }
                }

            using (FileStream fs = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
            {

                bf.Serialize(fs, NewCollection);

            }
                NumberVillages = NewCollection.Count.ToString();
                LoadVillages();
            }

        }
    }


