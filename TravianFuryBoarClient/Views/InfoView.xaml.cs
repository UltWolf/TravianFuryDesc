using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels;

namespace TravianFuryBoarClient.Views
{
    /// <summary>
    /// Логика взаимодействия для InfoView.xaml
    /// </summary>
    public partial class InfoView : Window
    {
        public InfoView(string Key, string UrlServer, DefaultUser DU)
        {
            InitializeComponent();
            DataContext = new InfoViewModel(this,Key,UrlServer, DU);
        }

        
    }
}
