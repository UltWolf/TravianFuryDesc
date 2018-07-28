using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TravianFuryBoarClient.ViewModels;

namespace TravianFuryBoarClient.Views
{
    /// <summary>
    /// Логика взаимодействия для OperationView.xaml
    /// </summary>
    public partial class OperationView : Window
    {
        public OperationView(System.Net.CookieCollection token)
        {

            InitializeComponent();
            this.DataContext = new OperationViewModel(token);
        }
    }
}
