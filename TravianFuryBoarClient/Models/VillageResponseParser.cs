using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravianFuryBoarClient.Models
{

    public class Rootobject
    {
        public Cache[] cache { get; set; }
        public int ignoreSerial { get; set; }
        public long time { get; set; }
        public int serialNo { get; set; }
        public object[] response { get; set; }
    }

    public class Cache
    {
        public string name { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string villageId { get; set; }
        public string playerId { get; set; }
        public string name { get; set; }
        public string tribeId { get; set; }
        public string belongsToKing { get; set; }
        public int belongsToKingdom { get; set; }
        public string type { get; set; }
        public string population { get; set; }
        public Coordinates coordinates { get; set; }
        public bool isMainVillage { get; set; }
        public bool isTown { get; set; }
        public string treasuresUsable { get; set; }
        public string treasures { get; set; }
        public string allowTributeCollection { get; set; }
        public string protectionGranted { get; set; }
        public int tributeCollectorPlayerId { get; set; }
        public float realTributePercent { get; set; }
    }

    public class Coordinates
    {
        public string x { get; set; }
        public string y { get; set; }
    }

}
