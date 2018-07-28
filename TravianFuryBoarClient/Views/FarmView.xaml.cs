
using System.IO;
using System.Windows;

using TravianFuryBoarClient.ViewModels;

namespace TravianFuryBoarClient.Views
{
    /// <summary>
    /// Логика взаимодействия для FarmView.xaml и FarmViewModel
    /// </summary>
    public partial class FarmView : Window
    {
        public FarmView(string token)
        {
            
            InitializeComponent();
            var fv = new FarmViewModel(this,token);
            DataContext = fv;
        }

       

       
    }
}
