using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TravianFuryBoarClient.Data.Nation;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.Views;

namespace TravianFuryBoarClient.ViewModels
{


    public class FarmViewModel : Village, INotifyPropertyChanged
    {
        #region properties and parametres
        //DestinationVillage
        private string _addIdVillage = "Деревня которую хотите добавить";
        //AuthenticationKey
        private string _key = "Введите сюда свой ключ";
        //From this village must be send attack
        private ushort _sourceIdVillage;
        //It`s just troops, for example in Romans: troop 1 - legionare, troop - 2 pretorian and etc.
        private ushort troop_1 = 0;
        private ushort troop_2 = 0;
        private ushort troop_3 = 0;
        private ushort troop_4 = 0;
        private ushort troop_5 = 0;
        private ushort troop_6 = 0;
        //Attack with hero or without.
        private bool _isHero = true;
        BinaryFormatter bf;

        private MyVillages _village;
        private string _error;
        public string Key { get { return _key; } set { _key = value; OnPropertyChanged("Key"); } }
        public ICommand GetCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand InfoCommand { get; private set; }
        public ICommand AttackCommand { get; private set; }
        public ICommand ChangeKeyCommand { get; private set; }
        public ICommand CycleAttack { get; private set; }
        public string AddIdVillage { get => _addIdVillage; set { _addIdVillage = value; OnPropertyChanged("Id"); } }

        public ObservableCollection<MyVillages> Villages { get; private set; }
        public MyVillages Village { get=> _village; set { _village = value; OnPropertyChanged("Village"); }  }


        public ushort Troop_1 { get => troop_1; set { troop_1 = value; OnPropertyChanged("Troop_1"); } }
        public ushort Troop_2 { get => troop_2; set { troop_2 = value; OnPropertyChanged("Troop_2"); } }
        public ushort Troop_3 { get => troop_3; set { troop_3 = value; OnPropertyChanged("Troop_3"); } }
        public ushort Troop_4 { get => troop_4; set { troop_4 = value; OnPropertyChanged("Troop_4"); } }
        public ushort Troop_5 { get => troop_5; set { troop_5 = value; OnPropertyChanged("Troop_5"); } }
        public ushort Troop_6 { get => troop_6; set { troop_6 = value; OnPropertyChanged("Troop_6"); } }
        public bool IsHero { get => _isHero; set { _isHero = value; OnPropertyChanged("IsHero"); } }

        public ushort SourceIdVillage { get => _sourceIdVillage; set { _sourceIdVillage = value;
                OnPropertyChanged("IsHero");
            }
        }

        public string Error { get => _error; set {_error = value; OnPropertyChanged("Error");} }
      
        #endregion



        public FarmViewModel(string token)
        {
            Key = token;
            bf = new BinaryFormatter();
           
            AddCommand = new RelayCommand(AddVillageAsync);
            AttackCommand = new RelayCommand(AttackVillage);
            CycleAttack = new RelayCommand(AttackAlwaysVillage);
            ChangeKeyCommand = new RelayCommand(ChangeToken);
            InfoCommand = new RelayCommand(OpenInfo);
            GetListVillages();
        }
        public void ChangeToken(object obj)
        {
            File.WriteAllText("TokenInfo.info", Key);
        }
     

        private void GetListVillages()
        {
            
            string request = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Player: 131"",""PlayerProfile: 131""]},""session"":""" + Key + @"""}";
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
            var b = new ParserListVillages();
            WebResponse response =  WR.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    b = JsonConvert.DeserializeObject<ParserListVillages>(reader.ReadToEnd());

                }
            }
            response.Close();
            int lengthCache = b.cache[0].data.villages.Length;
            Villages = new ObservableCollection<MyVillages>();
            for (int i = 0; i < lengthCache; i++)
            {

                    Villages.Add(new MyVillages() { VillageId = b.cache[0].data.villages[i].villageId, VillageName = b.cache[0].data.villages[i].name });
                             
            }
            Village = Villages[0];
            OnPropertyChanged("Villages");

        }
            private void AttackVillage(object obj)
        {
            string unitsRequest = $"\"1\":{Troop_1},\"2\":{Troop_2},\"3\":{Troop_3},\"4\":{Troop_4},\"5\":{Troop_5},\"6\":{troop_6}";
            SendView send = new SendView(unitsRequest,Key,Village);
            send.ShowDialog();
        }

        private void AttackAlwaysVillage(object obj)
        {

            if (!File.Exists("Ending_village.data"))
            {
                int i = 0;
                string[] Villages = File.ReadAllText("VillagesID.info").Split(';');
                while (i < Villages.Length - 1)
                {
                    string actionRequest = "{\"controller\":\"troops\",\"action\":\"send\",";
                    string wrapLeft = "{";
                    string wrapRight = "}";
                    string paramsRequest = "\"params\":{ \"destVillageId\":\"" + Villages[i] + "\",\"villageId\":536985588,\"movementType\":4,\"redeployHero\":false,\"units\":{";
                    string unitsRequest = $"\"1\":{Troop_1},\"2\":{Troop_2},\"3\":{Troop_3},\"4\":{Troop_4},\"5\":{Troop_5},\"6\":{troop_6}";
                    string sessionKey = "}},\"session\":\"" + Key + "\"}";
                    string fullrequest = actionRequest + paramsRequest + unitsRequest + sessionKey;
                    WebRequest WR = WebRequest.Create("https://ru5.kingdoms.com/api/?");
                    WR.Method = "POST";
                    Console.WriteLine(fullrequest);
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(fullrequest);
                    WR.ContentType = "application/json";
                    WR.ContentLength = byteArray.Length;
                    using (Stream dataStream = WR.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }
                    WebResponse response = WR.GetResponse();
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {

                            string sParametres = reader.ReadToEnd();
                            if (sParametres.Contains("NotEnoughUnits"))
                            {
                                this.Error = "You havent enought units, sorry";
                                using (FileStream fs = new FileStream("Ending_village.data", FileMode.Append))
                                {
                                    string[] coordinates = Villages.GetAnotherCordinates(i);
                                    BinaryFormatter bf = new BinaryFormatter();
                                    bf.Serialize(fs, new PostRequestToServer() { coordinates = coordinates, requests = fullrequest });
                                    Thread.Sleep(3600000);
                                    fs.Close();
                                    Thread thread = new Thread( new ParameterizedThreadStart(AttackAlwaysVillage));
                                    thread.Start();
                                    return;
                                    
                                }

                                break;
                            };

                        }
                    }
                    response.Close();
                    i++;
                }
            }
            else
            {
                using (FileStream fs = new FileStream("Ending_village.data", FileMode.OpenOrCreate))
                {

                    BinaryFormatter bf = new BinaryFormatter();
                    PostRequestToServer prts = (PostRequestToServer)bf.Deserialize(fs);

                    for (int i = 0; i < prts.coordinates.Length; i++)
                    {
                        WebRequest WR = WebRequest.Create("https://ru5.kingdoms.com/api/?");
                        WR.Method = "POST";
                        Console.WriteLine(prts.requests);
                        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(prts.requests);
                        WR.ContentType = "application/json";
                        WR.ContentLength = byteArray.Length;
                        using (Stream dataStream = WR.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);
                        }
                        WebResponse response = WR.GetResponse();
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {

                                string sParametres = reader.ReadToEnd();
                                if (sParametres.Contains("NotEnoughUnits"))
                                {

                                    this.Error = "You havent enought units, sorry";

                                    string[] coordinates = prts.coordinates.GetAnotherCordinates(i);

                                    bf.Serialize(fs, new PostRequestToServer() { coordinates = coordinates, requests = prts.requests });
                                    Thread.Sleep(3600000);
                                    fs.Close();
                                    Thread thread = new Thread(new ParameterizedThreadStart(AttackAlwaysVillage));
                                    thread.Start();
                                    return;

                                }

                            };
                        }
                        response.Close();
                    }


                }
                File.Delete("Ending_village.data");

            }
        }


            public async void AddVillageAsync(object obj)
        {
            ObservableCollection<Village> OldCollection = new ObservableCollection<Village>();
            using (FileStream fs = new FileStream("Villages.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();

                OldCollection = (ObservableCollection<Village>)bf.Deserialize(fs);
            }
            string request = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Village:" + AddIdVillage+ @"""]},""session"":""" + Key + @"""}";
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
                string requestPlayer = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Player:" + b.cache[0].data.playerId + @"""]},""session"":""" + Key + @"""}";
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
                    OldCollection.Add(village);
                   
                }
            }
        

            using (FileStream fs = new FileStream("Villages.dat", FileMode.Open))
            {

                bf.Serialize(fs, OldCollection);

              
            }

            
            
        }
  
        
        public string[] GetCordinates()
        {
           string[] coordinates = File.ReadAllText("VillagesID.info").Split(';');
           return coordinates;
        }
       
        public void OpenInfo(object obj)
        {
            InfoView IV = new InfoView(Key);
            IV.Show();
        }

    }
    public static class ExensionStringArrayClass
    {
        public static string[] GetAnotherCordinates(this string[] coordinates, int i)
        {
            int sizeMas = coordinates.Length - i;
            string[] array = new string[sizeMas];
            for (int j = 0; i < coordinates.Length; i++, j++)
            {
                array[j] = coordinates[i];
            }
            return array;
        }
    }
   


}
