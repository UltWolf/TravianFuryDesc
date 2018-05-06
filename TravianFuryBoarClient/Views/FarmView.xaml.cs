
using System.Windows;

using TravianFuryBoarClient.ViewModels;

namespace TravianFuryBoarClient.Views
{
    /// <summary>
    /// Логика взаимодействия для FarmView.xaml и FarmViewModel
    /// </summary>
    public partial class FarmView : Window
    {
        public FarmView()
        {
            
            InitializeComponent();
            var fv = new FarmViewModel();
            DataContext = fv;
        }
    }
}
