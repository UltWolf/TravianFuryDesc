using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravianFuryBoarClient.Data.Nation;

namespace TravianFuryBoarClient.Models
{
    /// <summary>It`s class for visulation infromation about villages in farm menu and info menu
    /// </summary>
    [Serializable]
    public class Village
    {
        #region parametres
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
        private short _population;
        //Our nations
        private Nation _nation;
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
        public short Population
        {
            get => _population;
            set
            {
                _argX = value;
                OnPropertyChanged("Population");
            }
        }
        public Nation Nation { get => _nation; set {
                _nation = value;
                OnPropertyChanged("Nation");
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

    #endregion

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
