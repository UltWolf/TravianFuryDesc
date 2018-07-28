using System;

namespace TravianFuryBoarClient.Models
{
    public class VillageOperation
    {
        public string SourceVillage { get; set; }
        public string DestinationVillage { get; set; }
        public DateTime Time { get; set; }
        //Type attack what`s we send
        public string TypeAttack{get;set;}
        //How many warriors we send
        public string[] CountWar{get;set;}
    }
}