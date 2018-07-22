using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Input;
using TravianFuryBoarClient.ViewModels.Components;

namespace TravianFuryBoarClient.ViewModels.LoginViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        public string Username { get => _username; set{_username = value; OnPropertyChanged("Username");} }
        public string Password{ get=> _password;set{_password=value;OnPropertyChanged("Password");}}
        public ICommand AuthorizationCommand{get;private set;}
        public LoginViewModel(){
            AuthorizationCommand = new RelayCommand(Login);
        }
        public void Login(object obj){
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