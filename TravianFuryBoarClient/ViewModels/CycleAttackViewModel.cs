using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels.Components;
using TravianFuryBoarClient.Views;

namespace TravianFuryBoarClient.ViewModels
{
    class CycleAttackViewModel : BaseViewModel
    { 
        private ushort _sourceIdVillage;
        //It`s just troops, for example in Romans: troop 1 - legionare, troop - 2 pretorian and etc.
        
        private Thread thr;
        //Attack with hero or without.
        private bool _isHero = true;
        


       
        public bool IsHero { get => _isHero; set { _isHero = value; OnPropertyChanged("IsHero"); } }
        private SendView SW;
        private DefaultUser _DU;
        private string _unitsRequest;
        private MyVillages _village;
        public ICommand BeginCommand;
        public ICommand StopCommand;
        public CycleAttackViewModel(  MyVillages village,DefaultUser DU) {
            BeginCommand = new RelayCommand(BeginAttack);
            StopCommand = new RelayCommand(StopAttack);
            _village = village;
            _DU = DU;
           
            thr  = new Thread(() =>BeginAttack(this));
            thr.SetApartmentState(ApartmentState.STA);
            thr .Start();

        }
        private void StopAttack(object obj)
        {
            try
            {
                if (thr != null)
                {
                    thr.Abort();
                };
            }
            catch (ThreadAbortException ex) { MessageBox.Show("You have been closed this thread"); }
        }
        private void BeginAttack(object obj) {
            while (true) {
                
                  SW = new SendView(  _DU, _village );
                  SW.ShowDialog() ;
                Random rand = new Random();
                int TimeDelay = 20000 * rand.Next(9);
                 Thread.Sleep(TimeDelay );
      
            }
        }
    }
}
