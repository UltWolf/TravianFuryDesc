using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravianFuryBoarClient.Models
{
    public class Area
    {
        public bool isOasis { get; set; }
        public string oasisType { get; set; }
        public string hasVillage { get; set; }
        public int hasNPC { get; set; }
        public string resType { get; set; }
        public int isHabitable { get; set; }
        public string landscape { get; set; }
    }
    public class AreaOut
    {
        public string Wood { get; set; }
        public string Iron { get; set; }
        public string Clay { get; set; }
        public string Crop { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }

   
}
