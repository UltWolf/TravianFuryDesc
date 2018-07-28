using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;
using System.Windows.Input;
using Newtonsoft.Json;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels.Components;
using TravianFuryBoarClient.Views;

namespace TravianFuryBoarClient.ViewModels.LoginViewModels
{
    public class LoginViewModel : BaseLoginViewModel
    {
       
        public ICommand AuthorizationCommand{get;private set;}
        private LoginView _lv;
        public LoginViewModel(LoginView lv){
            AuthorizationCommand = new RelayCommand(Login);
            _lv = lv;
        }
        public void Login(object obj){
            var WR = (HttpWebRequest)WebRequest.Create(Path.Combine(url,"api/Account/Login"));
            WR.Method = "POST";
            WR.ContentType = "application/json";
            //Create trust ssl connection
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
           
            using (StreamWriter writeStream = new StreamWriter(WR.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new { Username = Username, Password = Password });
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
           _lv.Close();
    }
    }
    }