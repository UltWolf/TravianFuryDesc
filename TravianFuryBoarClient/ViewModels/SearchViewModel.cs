using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravianFuryBoarClient.Models;

namespace TravianFuryBoarClient.ViewModels
{
    class SearchViewModel:AreaOut, INotifyPropertyChanged
    {
     
        private string token;
        private BinaryFormatter bf;

      

        public ObservableCollection<Area> Areas { get; private set; }

        public Area Area { get; set; }
        public ICommand FindCommand{ get; private set; }
   
        public SearchViewModel(string _token)
        {

           
            FindCommand = new RelayCommand(FindVillages);
           
            token = _token;
        }
        public void FindVillages(object obj)
        {
            
            for (int startposition = 200000; startposition<400000;startposition++) {
                string request = @"{""controller"":""cache"",""action"":""get"",""params"":{ ""names"":[""MapDetails:527" + startposition + @"""]},""session"":""" + token + @"""}";
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
                var b = new AreaParser();
                WebResponse response = WR.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        b = JsonConvert.DeserializeObject<AreaParser>(reader.ReadToEnd());

                    }
                }
            }
            }

        
}
}
