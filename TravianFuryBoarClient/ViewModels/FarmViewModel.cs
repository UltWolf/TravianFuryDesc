using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravianFuryBoarClient.Data.Nation;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels.Components;
using TravianFuryBoarClient.Views;

namespace TravianFuryBoarClient.ViewModels
{


    public class FarmViewModel : BaseViewModel
    {
        #region properties and parametres
        
        //DestinationVillage
        private string _addIdVillage = "Деревня которую хотите добавить";
        //AuthenticationKey
        private string _key = "Введите сюда свой ключ";
        //From this village must be send attack
        private ushort _sourceIdVillage;
        //It`s just troops, for example in Romans: troop 1 - legionare, troop - 2 pretorian and etc.
        
        //Attack with hero or without.
        private bool _isHero = true;
        BinaryFormatter bf;
        private DefaultUser _DU;
        private MyVillages _village;
        private string IDHero;
          
        private string _error;
        private string ProfileID;
        public string Key { get { return _key; } set { _key = value; OnPropertyChanged("Key"); } }
        public ICommand ChangeValueCommand { get; private set; }
        public ICommand CycleAttackCommand { get; private set; }
        public ICommand GetCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand InfoCommand { get; private set; }
        public ICommand AttackCommand { get; private set; }
        public ICommand ChangeKeyCommand { get; private set; }
        public string AddIdVillage { get => _addIdVillage; set { _addIdVillage = value; OnPropertyChanged("Id"); } }

        public ObservableCollection<MyVillages> Villages { get; private set; }
        public MyVillages Village { get=> _village; set { _village = value; OnPropertyChanged("Village"); }  }
 
        public bool IsHero { get => _isHero; set { _isHero = value; OnPropertyChanged("IsHero"); } }
        private string PathToProfileDirectory;

        public ushort SourceIdVillage { get => _sourceIdVillage; set { _sourceIdVillage = value;
                OnPropertyChanged("IsHero");
            }
        }

        public string Error { get => _error; set {_error = value; OnPropertyChanged("Error");} }
        #endregion



        public FarmViewModel(FarmView FV,DefaultUser DU)
        {
             _DU = DU;
            Key = DU.LastToken;
            IDHero = DU.Portrait;
            ServerUrl = DU.URLServer;
            bf = new BinaryFormatter();
            PathToProfileDirectory = Path.Combine("Profiles", IDHero);
            InitializeDirectory();
            AddCommand = new RelayCommand(AddVillageAsync);
            AttackCommand = new RelayCommand(AttackVillage);
            CycleAttackCommand = new RelayCommand(AlwaysAttack);
            ChangeKeyCommand = new RelayCommand(ChangeToken);
            InfoCommand = new RelayCommand(OpenInfo);
         
            ProfileID = GetProfileId();

            //if(ProfileID ==""){
                
            //    RegistrationView RV = new RegistrationView();
            //    RV.Show();
            //}
            GetListVillages();
        }
        public bool InitializeDirectory() {
            {
                if (Directory.Exists(PathToProfileDirectory))
                {
                    return true;
                }
                Directory.CreateDirectory(PathToProfileDirectory);
                return true;
            } }

        public string GetProfileId(){
            try{
       using(FileStream fs =  new FileStream(PathToProfileDirectory+"\\UserProfile.dat",FileMode.Open)){
    BinaryFormatter bf = new BinaryFormatter();
       var b = (UserProfile)bf.Deserialize(fs);
       return b.ProfileId;
}
            }
            catch(FileNotFoundException ex){
              string message = "You don`t create profile, on our server";
              string caption = "Error Detected";
              MessageBox.Show(message,caption);
              return "";
            }
        }

        
        public void ChangeToken(object obj)
        {
            File.WriteAllText("TokenInfo.info", Key);
        }

        private void GetListVillages()
        {
            string request = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Player:"  + IDHero + @""",""PlayerProfile :" + IDHero + @"""]},""session"":""" + Key + @"""}";
            WebRequest WR = WebRequest.Create(ServerUrl+"/api/?");
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
            try
            {
                int lengthCache = b.cache[0].data.villages.Length;
                Villages = new ObservableCollection<MyVillages>();
                for (int i = 0; i < lengthCache; i++)
                {
                    Villages.Add(new MyVillages() { VillageId = b.cache[0].data.villages[i].villageId, VillageName = b.cache[0].data.villages[i].name });
                }
                Village = Villages[0];
                OnPropertyChanged("Villages");
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("You put wrong token");
            }

        }
        private void AlwaysAttack(object obj) {
            
            CycleAttack CA = new CycleAttack(  Village,_DU);
            CA.ShowDialog();
            

        }
            private void AttackVillage(object obj)
        {
         
            SendView send = new SendView( _DU ,Village);
            send.ShowDialog();
        }


            public async void AddVillageAsync(object obj)
        {
            ObservableCollection<Village> OldCollection = new ObservableCollection<Village>();
            try { 
            using (FileStream fs = new FileStream(PathToProfileDirectory+"\\Villages.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();

                OldCollection = (ObservableCollection<Village>)bf.Deserialize(fs);
            }
            }
            catch(FileNotFoundException ex)
            {
                using (FileStream fs = new FileStream(PathToProfileDirectory+"\\Villages.dat", FileMode.Create ))
                {
                    File.Create("Villages.dat");
                }
            }
            string request = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Village:" + AddIdVillage+ @"""]},""session"":""" + Key + @"""}";
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
                string requestPlayer = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Player:" + b.cache[0].data.playerId + @"""]},""session"":""" + Key + @"""}";
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
                    OldCollection.Add(village);

                }
            }
            using (FileStream fs = new FileStream(PathToProfileDirectory+"\\Villages.dat", FileMode.Open))
            {
                bf.Serialize(fs, OldCollection);
            }
        }

 

        public void OpenInfo(object obj)
        {
            InfoView IV = new InfoView(Key,ServerUrl,_DU);
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
