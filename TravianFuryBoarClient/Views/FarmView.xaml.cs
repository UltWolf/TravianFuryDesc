
using System.IO;
using System.Windows;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels;

namespace TravianFuryBoarClient.Views
{
    /// <summary>
    /// Логика взаимодействия для FarmView.xaml и FarmViewModel
    /// </summary>
    public partial class FarmView : Window
    {
        public FarmView(DefaultUser DU)
        {
            
            InitializeComponent();
            var fv = new FarmViewModel(this,DU);
            DataContext = fv;
        }

       

       
    }
}
