using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using TravianFuryBoarClient.Views;

namespace TravianFuryBoarClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyLabel.Text = getToken();

        }
        public string  getToken()
        {
           
                if (File.Exists("TokenInfo.info"))
                {
                    return File.ReadAllText("TokenInfo.info");
                }
                else
                {
                    return "Введите сюда свой ключ";
                }
            
        }
      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FarmView FW = new FarmView(KeyLabel.Text);
            FW.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            File.WriteAllText("TokenInfo.info", KeyLabel.Text);
        }
    }
}
