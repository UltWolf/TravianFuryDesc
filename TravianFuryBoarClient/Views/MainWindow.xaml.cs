using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.Views;

namespace TravianFuryBoarClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BinaryFormatter bf = new BinaryFormatter();
        Dictionary<string, Dictionary<string, string>> dictUser = new Dictionary<string, Dictionary<string, string>>(); 
        private DefaultUser DU = new DefaultUser();
        public MainWindow()
        {
            InitializeComponent();
            dictUser =   getUserData();
            if(dictUser.Count==0)
            {
                DU = CreateDefaultUser();
            }
            else
            {
                DU.URLServer = dictUser.First().Key;
                DU.Portrait = dictUser.First().Value.First().Key;
                DU.LastToken = dictUser.First().Value.First().Value;
            }
            KeyLabel.Text = DU.LastToken;
            HeroLabel.Text = DU.Portrait;
            ServerLabel.Text = DU.URLServer;
            UserBox.ItemsSource = getUserList();

        }
        public  Dictionary<string, Dictionary<string, string>>  getUserData()
        {
           
                if (File.Exists("UserData.DATA"))
                {
                try
                {
                    using (FileStream fs = new FileStream("UserData.DATA", FileMode.Open))
                    {
                        return (Dictionary<string, Dictionary<string, string>>)bf.Deserialize(fs);
                    }
                }
                catch(SerializationException SE)
                {
                    return null;
                }

            }
            return null;


        }
      
         public DefaultUser CreateDefaultUser()
        {
            DefaultUser DU = new DefaultUser();
            DU.Portrait = " Введите здесь номер своего героя";
            DU.URLServer = " Введите здесь адресс своего сервера";
            DU.LastToken = " Введите здесь свой токен";
            return DU;
        }      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (KeyLabel.Text != "Введите сюда свой ключ" && KeyLabel.Text != ""  )
            {
                FarmView FW = new FarmView(DU);
                FW.Show();
                this.Close();
            }
            else
            {
                string message = "You don`t put token in label";
                string caption = "Error Detected";
                MessageBox.Show(message, caption);
                
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegistrationView rw = new RegistrationView();
            rw.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoginToolsView LTV = new LoginToolsView();
            LTV.Show();
            this.Close();
        }

        private Dictionary<string, Dictionary<string, string>> getUserList()
        {
            Dictionary<string, Dictionary<string, string>> listUser = new Dictionary<string, Dictionary<string, string>>();
            try
            {
                using (FileStream fs = new FileStream("UserData.DATA", FileMode.Open))
                {
                    listUser = (Dictionary<string, Dictionary<string, string>>)bf.Deserialize(fs);
                }

            }
            catch(SerializationException w)
            {

            }
            catch (FileNotFoundException er)
            {

            }
            return listUser;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Dictionary<string, Dictionary<string, string>> listUser = new Dictionary<string, Dictionary<string, string>>();
            try { 
            using (FileStream fs = new FileStream("UserData.DATA", FileMode.Open))
            {
                   listUser = (Dictionary<string, Dictionary<string, string>> )bf.Deserialize(fs);
            }
            }
            catch(SerializationException er)
            {

            }
            catch(FileNotFoundException er)
            {
                
            }
            if (listUser.ContainsKey(ServerLabel.Text))
            {
                listUser[ServerLabel.Text].Add(HeroLabel.Text, KeyLabel.Text);
            }
            else
            {  Dictionary<string, string> InsideValues = new Dictionary<string, string>();
                InsideValues.Add(HeroLabel.Text, KeyLabel.Text);
                listUser.Add(ServerLabel.Text, InsideValues);
            }
          
           
            using (FileStream fs = new FileStream("UserData.DATA", FileMode.Create))
            {   
                bf.Serialize(fs, listUser);
            }
        }

        private void UserBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           KeyValuePair<string, Dictionary<string, string>> returnresult = new KeyValuePair<string, Dictionary<string, string>>();
           returnresult = (KeyValuePair<string, Dictionary<string, string>>) UserBox.SelectedItem  ;
            ServerLabel.Text = returnresult.Key;
            HeroLabel.Text = returnresult.Value.Keys.First() ;
            KeyLabel.Text = returnresult.Value.Values.First();
            DU.URLServer = returnresult.Key;
            DU.Portrait = returnresult.Value.Keys.First();
            DU.LastToken = returnresult.Value.Values.First();
        }
    }
}
