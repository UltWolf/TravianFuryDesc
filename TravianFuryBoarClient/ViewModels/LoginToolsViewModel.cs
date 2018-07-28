using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Input;
using Newtonsoft.Json;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels.Components;
using TravianFuryBoarClient.Views;

namespace TravianFuryBoarClient.ViewModels
{
    class LoginToolsViewModel:BaseLoginViewModel
    {
        private LoginToolsView _ltv;
        public ICommand Login { get; private set; }
        public LoginToolsViewModel(LoginToolsView LTV){
            Login = new RelayCommand(Authenticate);
            _ltv = LTV;
        }
        public Cookie GetPHPSESSION()
        {
            var WR = (HttpWebRequest)WebRequest.Create("https://www.gettertools.com/ru/62-Вход");
            WR.Method = "GET";
            WR.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)WR.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var Cookie = response.Cookies;
                    return Cookie[0];
                   
                }
            }

        }
    
        public void Authenticate(object obj){
            var WR = (HttpWebRequest)WebRequest.Create("https://www.gettertools.com/en/62-Login");
            WR.Method = "POST";
            WR.ContentType = "application/x-www-form-urlencoded";
            Cookie cookie = GetPHPSESSION();
            WR.CookieContainer = new CookieContainer();
            WR.CookieContainer.Add(cookie);
            WR.Referer = "https://www.gettertools.com/en/62-Login";
            WR.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:61.0) Gecko/20100101 Firefox/61.0";
            WR.Host = "www.gettertools.com";
            WR.KeepAlive = true;
            WR.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            WR.Headers.Add("Upgrade-Insecure-Request", "1");
            WR.Headers.Add("Cache-Control", "no-cache");
            WR.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            WR.Headers.Add("Accept-Encoding", "gzip, deflate, br");

            string data = "action=login&username="+Username+"&password="+Password;
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
    
            WR.ContentLength = byteArray.Length;

            using (Stream dataStream = WR.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

          
            HttpWebResponse response =  (HttpWebResponse) WR.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {   var Cookie = response.Cookies;
                    OperationView ov = new OperationView(Cookie);
                    ov.Show();
                    _ltv.Close();
                }
            }

        }
    }
}
