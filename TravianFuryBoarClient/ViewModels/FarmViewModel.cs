using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Input;
using TravianFuryBoarClient.Models;

namespace TravianFuryBoarClient.ViewModels
{

    public class FarmViewModel : Village, INotifyPropertyChanged
    {
        #region properties and parametres
        private string _addIdVillage = "Деревня которую хотите добавить";
        private string _key = "Введите сюда свой ключ";
        BinaryFormatter bf;
        ObservableCollection<Village> villages;
        public string Key { get { return _key; } set { _key = value; OnPropertyChanged("Key"); } }
        public ICommand GetCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public string AddIdVillage { get => _addIdVillage; set {_addIdVillage = value; OnPropertyChanged("Id");} }
        #endregion



        public FarmViewModel()
        {
            bf = new BinaryFormatter();
            GetCommand = new RelayCommand(GetFromDat);
            AddCommand = new RelayCommand(AddVillageAsync);
        }
        public async void AddVillageAsync(object obj)
        {

            string request = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""Village:"+AddIdVillage+@"""]},""session"":"""+Key+@"""}";
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
            WebResponse response = await WR.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            Village village = new Village()
            {
                Name = "BLABLA",
                Villageid = uint.Parse(this._addIdVillage),
                ArgX = ArgX,
                ArgY = ArgY,
                Population = Population,
                Nation = Nation,
                IsAbounded = IsAbounded
            };
            await WriteToDate(village);
            response.Close();
            
            
        }
        public async Task WriteToDate(Village village)
        {
            
            using (FileStream fs = new FileStream("Villages.dat", FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, village);
                
                await fs.WriteAsync(System.Text.Encoding.UTF8.GetBytes(village.ToString()), 0, System.Text.Encoding.UTF8.GetBytes(village.ToString()).Length);
            }
        }
        public void GetFromDat(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream("Villages.dat", FileMode.OpenOrCreate))
            {

                 villages =  (ObservableCollection<Village>)bf.Deserialize(fs);
            }
        }
 

    }
}
