using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TravianFuryBoarClient.Data.Nation;

namespace TravianFuryBoarClient.Models
{
    /// <summary>It`s class for visulation infromation about villages in farm menu and info menu
    /// </summary>
    [Serializable]
    public class Village
    {

        #region parametres
        private string _playerName;
        //Village id for attacking
        private uint _villageid;
        //VillagesName
        private string _name;
        //XCordinates where our village is located
        private short _argX;
        //YCordinates where our village is located
        private short _argY;
        //Our village abandoned or not.
        private bool _isEmpty;
        //How many people live in this villages
        private  short _population;
        //Our nations
        public string urlIcon { get; set; } 
        [NonSerialized] 
        private Image _icon;     
        private Nation _nation;
        private bool _original;
        private ushort troop_1 = 0;
        private ushort troop_2 = 0;
        private ushort troop_3 = 0;
        private ushort troop_4 = 0;
        private ushort troop_5 = 0;
        private ushort troop_6 = 0;
        #endregion


        #region properties
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public short ArgX
        {
            get => _argX;
            set
            {
                _argX = value;
                OnPropertyChanged("ArgX");
            }
        }
        public short ArgY
        {
            get => _argX;
            set
            {
                _argX = value;
                OnPropertyChanged("ArgY");
            }
        }
        public  short Population
        {
            get => _population;
            set
            {
                _population = value;
                OnPropertyChanged("Population");
            }
        }
        public Nation Nation { get => _nation; set {
                _nation = value;
                OnPropertyChanged("Nation");
            }
        }
        public Image Icon
        {
            get => _icon; set
            {
                _icon = value;
                OnPropertyChanged("Icon");
            }
        }
        public Boolean IsAbounded { get => _isEmpty;
            set
            {
                _isEmpty = value;
                OnPropertyChanged("IsAbounded");
            }
        }
        public uint Villageid {
            get => _villageid;
            set
            {
                _villageid = value;
                OnPropertyChanged("VillageId");
            }
        }
        public bool Original { get => _original;set { _original = value;OnPropertyChanged("Original"); } }

        public ushort Troop_1 { get => troop_1; set { troop_1 = value; OnPropertyChanged("Troop_1"); } }
        public ushort Troop_2 { get => troop_2; set { troop_2 = value; OnPropertyChanged("Troop_2"); } }
        public ushort Troop_3 { get => troop_3; set { troop_3 = value; OnPropertyChanged("Troop_3"); } }
        public ushort Troop_4 { get => troop_4; set { troop_4 = value; OnPropertyChanged("Troop_4"); } }
        public ushort Troop_5 { get => troop_5; set { troop_5 = value; OnPropertyChanged("Troop_5"); } }
        public ushort Troop_6 { get => troop_6; set { troop_6 = value; OnPropertyChanged("Troop_6"); } }

        public string PlayerName { get => _playerName; set { _playerName = value; OnPropertyChanged("PlayerName"); } }

        #endregion

        public override bool Equals(object obj)
        {
            try { 
            if(this.Villageid == ((Village)obj).Villageid)
            {
                return true;
            }
            }
            catch(InvalidCastException e)
            {
                return false;
            }
            return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

      
    }
}
