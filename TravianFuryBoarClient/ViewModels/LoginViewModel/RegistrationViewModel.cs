using System;
using System.IO;
using System.Net;
using System.Windows.Input;
using TravianFuryBoarClient.ViewModels.Components;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using TravianFuryBoarClient.Models;
using System.Runtime.Serialization.Formatters.Binary;
using TravianFuryBoarClient.Views;

namespace TravianFuryBoarClient.ViewModels.LoginViewModels
{
    public class RegistrationViewModel : BaseLoginViewModel
    {
    
        private string _server;
        private string _profileId;
        private RegistrationView _rw;
        public ICommand AuthenticateCommand{get;set;}
        public string ProfileId { get => _profileId; set { _profileId = value;OnPropertyChanged("ProfileId");}} 
        public string Server { get => _server; set { _server = value; OnPropertyChanged("Server");}}

        public RegistrationViewModel(RegistrationView rw ){
            AuthenticateCommand = new RelayCommand(Registration);
            _rw=rw;
            
        }
        public void Registration(object obj){
            var WR = (HttpWebRequest)WebRequest.Create(Path.Combine(url,"api/Account/register"));
            WR.Method = "POST";
            WR.ContentType = "application/json";
            //Create trust ssl connection 
            ServicePointManager.ServerCertificateValidationCallback +=
                 (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            
            using (StreamWriter writeStream = new StreamWriter(WR.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new { Username = Username, Password = Password, Server = Server, ProfileId = ProfileId });
                writeStream.Write(json);
            }
            WebResponse response =  WR.GetResponse();
            UserProfile userProfile;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    userProfile = JsonConvert.DeserializeObject<UserProfile>(reader.ReadToEnd());
                }
            }
            using(FileStream fs = new FileStream("UserProfile.dat",FileMode.Create)){
                       BinaryFormatter bf = new BinaryFormatter();
                       bf.Serialize(fs,userProfile);
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            _rw.Close();
        }
    }
}