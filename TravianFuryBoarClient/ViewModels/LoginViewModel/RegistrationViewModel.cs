using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Input;
using TravianFuryBoarClient.ViewModels.Components;

namespace TravianFuryBoarClient.ViewModels.LoginViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _server;
        private string _profileId;
        public string Username { get => _username; set{_username = value; OnPropertyChanged("Username");} }
        public string Password{ get=> _password;set{_password=value;OnPropertyChanged("Password");}}
        public ICommand AuthenticateCommand{get;set;}
        public string ProfileId { get => _profileId; set { _profileId = value;OnPropertyChanged("ProfileId");}} 
        public string Server { get => _server; set { _server = value; OnPropertyChanged("Server");}}

        public RegistrationViewModel(){
            AuthenticateCommand = new RelayCommand(Registration);
            
        }
        public void Registration(object obj){
            WebRequest WR = WebRequest.Create(url);
            WR.Method = "POST";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(new{Username = Username,Password = Password}.ToString());
            WR.ContentType = "application/json";
            WR.ContentLength = byteArray.Length;

            using (Stream dataStream = WR.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            WebResponse response =  WR.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                }
            }
        }
    }
}