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
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels;

namespace TravianFuryBoarClient.Views
{
    /// <summary>
    /// Логика взаимодействия для CycleAttack.xaml
    /// </summary>
    public partial class CycleAttack : Window
    {
        private MyVillages village;
        private object dU;

        public CycleAttack( MyVillages village,DefaultUser DU )
        {
            InitializeComponent();
            var fv = new CycleAttackViewModel(  village, DU);
            DataContext = fv;
        }

        public CycleAttack(MyVillages village, object dU)
        {
            this.village = village;
            this.dU = dU;
        }
    }
}
