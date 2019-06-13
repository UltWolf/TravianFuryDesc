using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels;

namespace TravianFuryBoarClient.Views
{
    /// <summary>
    /// Логика взаимодействия для SendView.xaml
    /// </summary>
    public partial class SendView : Window
    {
        private string PathToProfileDirectory;
        private Boolean Attack = true ;
        private string key;
        private string IDHero ;
        private string ServerUrl;
        public SendView(  DefaultUser DU,MyVillages sourceVillage)
        {
            key = DU.LastToken;
            IDHero = DU.Portrait;
            ServerUrl = DU.URLServer;
           
            PathToProfileDirectory = System.IO.Path.Combine("Profiles", IDHero);
            InitializeComponent();
            Thread tr = new Thread(()=>SendUnits( DU, sourceVillage));
          
            
            tr.Start();
        }

        public void sendArmy(  string key, MyVillages sourceVillage,BinaryFormatter bf, ObservableCollection<Village> Villages)
        {
            int i = 0;
            foreach (var Village in Villages)
            {
                Dispatcher.Invoke(() =>
                {
                    IVillages.Content = i.ToString();
                    AllVillages.Content = "/  " + (Villages.Count - 1).ToString();
                });

                string actionRequest = "{\"controller\":\"troops\",\"action\":\"send\",";
                string unitsRequest = $"\"1\":{Village.Troop_1},\"2\":{Village.Troop_2},\"3\":{Village.Troop_3},\"4\":{Village.Troop_4},\"5\":{Village.Troop_5},\"6\":{Village.Troop_6}";
                string wrapLeft = "{";
                string wrapRight = "}";
                string paramsRequest = "\"params\":{ \"destVillageId\":\"" + Village.Villageid + "\",\"villageId\":" + sourceVillage.VillageId + ",\"movementType\":4,\"redeployHero\":false,\"units\":{";

                string sessionKey = "}},\"session\":\"" + key + "\"}";
                string fullrequest = actionRequest + paramsRequest + unitsRequest + sessionKey;
                WebRequest WR = WebRequest.Create(ServerUrl +"/api/?");
                WR.Method = "POST";
                Dispatcher.Invoke(() =>
                {
                    FromLabel.Content = sourceVillage.VillageName;
                    ToLabel.Content = Village.Name;
                });

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
                            response.Close();
                            Dispatcher.Invoke(() => ErrorLabel.Content = "Ooops, we have some problem");
                            using (FileStream fs = new FileStream(PathToProfileDirectory + "\\EndingVillage.data", FileMode.Create,FileAccess.Write))
                            {
                                ObservableCollection<Village> lastVillages = new ObservableCollection<Village>();
                                for (; i < Villages.Count; i++)
                                {
                                    lastVillages.Add(Villages[i]);
                                    Dispatcher.Invoke(() =>
                                    {
                                        IVillages.Content = "We record the remaining files: ";
                                        AllVillages.Content = (Villages.Count - i).ToString();
                                    });
                                }
                                bf.Serialize(fs, lastVillages);
                            };
                            break;
                        }
                    }
                    response.Close();
                    i++;

                }

             
            }
            Dispatcher.Invoke(() =>
            {
                FromLabel.Content = "";
                ToLabel.Content = "";
                ErrorLabel.Content = "We send all our army succesfull";
            });


            Thread.Sleep(2000);
            Dispatcher.Invoke(() =>
            {
                this.Close();
            });
        }

       

        public void SendUnits(  DefaultUser DU, MyVillages sourceVillage)
        {
            var bf = new BinaryFormatter();
            ObservableCollection<Village> Villages = new ObservableCollection<Village>();
            try
            {
                using (FileStream fs = new FileStream(PathToProfileDirectory + "\\EndingVillage.data", FileMode.Open))
                {
                    try
                    {
                        Villages = (ObservableCollection<Village>)bf.Deserialize(fs);
                        fs.Dispose();
                        File.Delete(PathToProfileDirectory + "\\EndingVillage.data");
                        if (Villages.Count == 0)
                        {
                           
                            using (FileStream fsVillages = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
                            {
                                Villages = (ObservableCollection<Village>)bf.Deserialize(fsVillages);
                            }
                        }
                       
                            sendArmy(  key, sourceVillage, bf, Villages);
                        

                    }
                    catch (System.Runtime.Serialization.SerializationException e)
                    {
                        fs.Dispose();
                        File.Delete("EndingVillage.data");
                        using (FileStream fsVillages = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
                        {
                            Villages = (ObservableCollection<Village>)bf.Deserialize(fsVillages);
                        }
                        sendArmy( key, sourceVillage, bf, Villages);
                    }
                }
            }
            catch(FileNotFoundException e)
            {
                try
                {
                    using (FileStream fs = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
                    {
                        try
                        {
                            Villages = (ObservableCollection<Village>)bf.Deserialize(fs);
                            sendArmy(  key, sourceVillage, bf, Villages);
                        }
                        catch (Exception f)
                        {

                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    Dispatcher.Invoke(() =>
                    {
                        FromLabel.Content = "";
                        ToLabel.Content = "";
                        ErrorLabel.Content = "We can`t find your list with village";
                    });


                    Thread.Sleep(2000);
                    Dispatcher.Invoke(() =>
                    {
                        this.Close();
                    });

                }
            }
            
           
               
        }

        public void Stop() {
            Attack = false;
        }
        public void SendUnitsCycle(string unitsRequest, DefaultUser DU, MyVillages sourceVillage)
        {
            while (Attack)
            {
                var bf = new BinaryFormatter();
                ObservableCollection<Village> Villages = new ObservableCollection<Village>();
                try
                {
                    using (FileStream fs = new FileStream(PathToProfileDirectory + "\\EndingVillage.data", FileMode.Open))
                    {
                        try
                        {
                            Villages = (ObservableCollection<Village>)bf.Deserialize(fs);
                            fs.Dispose();
                            File.Delete(PathToProfileDirectory + "\\EndingVillage.data");
                            if (Villages.Count == 0)
                            {

                                using (FileStream fsVillages = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
                                {
                                    Villages = (ObservableCollection<Village>)bf.Deserialize(fsVillages);
                                }
                            }

                            sendArmy(  key, sourceVillage, bf, Villages);


                        }
                        catch (System.Runtime.Serialization.SerializationException e)
                        {
                            fs.Dispose();
                            File.Delete("EndingVillage.data");
                            using (FileStream fsVillages = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
                            {
                                Villages = (ObservableCollection<Village>)bf.Deserialize(fsVillages);
                            }
                            sendArmy(  key, sourceVillage, bf, Villages);
                        }
                    }
                }
                catch (FileNotFoundException e)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(PathToProfileDirectory + "\\Villages.dat", FileMode.Open))
                        {
                            try
                            {
                                Villages = (ObservableCollection<Village>)bf.Deserialize(fs);
                                sendArmy(  key, sourceVillage, bf, Villages);
                            }
                            catch (Exception f)
                            {

                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            FromLabel.Content = "";
                            ToLabel.Content = "";
                            ErrorLabel.Content = "We can`t find your list with village";
                        });


                        Thread.Sleep(2000);
                        Dispatcher.Invoke(() =>
                        {
                            this.Close();
                        });

                    }
                }

            }

        }

    }
}
