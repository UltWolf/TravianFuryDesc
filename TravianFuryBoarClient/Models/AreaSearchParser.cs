using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravianFuryBoarClient.Models
{

    public class AreaParser
    {
        public Cache[] cache { get; set; }
        public int ignoreSerial { get; set; }
        public long time { get; set; }
        public int serialNo { get; set; }
        public object[] response { get; set; }
        public class Cache
        {
            public string name { get; set; }
            public Data data { get; set; }
        }
        public class Data
        {
            public bool isOasis { get; set; }
            public string oasisType { get; set; }
            public string hasVillage { get; set; }
            public int hasNPC { get; set; }
            public string resType { get; set; }
            public int isHabitable { get; set; }
            public string landscape { get; set; }
        }
    }

    

   

}
