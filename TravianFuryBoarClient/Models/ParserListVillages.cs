using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravianFuryBoarClient.Models
{
    public class ParserListVillages
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
            public string playerId { get; set; }
            public string name { get; set; }
            public string tribeId { get; set; }
            public string kingdomId { get; set; }
            public string kingdomTag { get; set; }
            public string kingdomRole { get; set; }
            public bool isKing { get; set; }
            public string kingId { get; set; }
            public string kingstatus { get; set; }
            public Village[] villages { get; set; }
            public string population { get; set; }
            public string active { get; set; }
            public int prestige { get; set; }
            public int level { get; set; }
            public Stars stars { get; set; }
            public int nextLevelPrestige { get; set; }
            public bool hasNoobProtection { get; set; }
            public bool filterInformation { get; set; }
            public string signupTime { get; set; }
            public string vacationState { get; set; }
            public string uiLimitations { get; set; }
            public string gold { get; set; }
            public string silver { get; set; }
            public string deletionTime { get; set; }
            public int coronationDuration { get; set; }
            public string brewCelebration { get; set; }
            public string uiStatus { get; set; }
            public string hintStatus { get; set; }
            public string spawnedOnMap { get; set; }
            public string isActivated { get; set; }
            public string isInstant { get; set; }
            public string productionBonusTime { get; set; }
            public string cropProductionBonusTime { get; set; }
            public string premiumFeatureAutoExtendFlags { get; set; }
            public string plusAccountTime { get; set; }
            public string limitedPremiumFeatureFlags { get; set; }
            public string lastPaymentTime { get; set; }
            public bool isPunished { get; set; }
            public string limitationFlags { get; set; }
            public string limitation { get; set; }
            public bool isBannedFromMessaging { get; set; }
            public string bannedFromMessaging { get; set; }
            public string questVersion { get; set; }
            public int nextDailyQuestTime { get; set; }
            public string dailyQuestsExchanged { get; set; }
            public string avatarIdentifier { get; set; }
            public string vacationStateStart { get; set; }
            public string vacationStateEnd { get; set; }
            public string usedVacationDays { get; set; }
            public string description { get; set; }
        }

        public class Stars
        {
            public int bronze { get; set; }
            public int silver { get; set; }
            public int gold { get; set; }
        }

        public class Village
        {
            public string villageId { get; set; }
            public string playerId { get; set; }
            public string name { get; set; }
            public string tribeId { get; set; }
            public string belongsToKing { get; set; }
            public object belongsToKingdom { get; set; }
            public string type { get; set; }
            public string population { get; set; }
            public Coordinates coordinates { get; set; }
            public bool isMainVillage { get; set; }
            public bool isTown { get; set; }
            public string treasuresUsable { get; set; }
            public string treasures { get; set; }
            public string allowTributeCollection { get; set; }
            public string protectionGranted { get; set; }
            public object tributeCollectorPlayerId { get; set; }
            public float realTributePercent { get; set; }
            public string supplyBuildings { get; set; }
            public string supplyTroops { get; set; }
            public Production production { get; set; }
            public Storage storage { get; set; }
            public Treasury treasury { get; set; }
            public Storagecapacity storageCapacity { get; set; }
            public string usedControlPoints { get; set; }
            public string availableControlPoints { get; set; }
            public float culturePoints { get; set; }
            public string celebrationType { get; set; }
            public string celebrationEnd { get; set; }
            public string culturePointProduction { get; set; }
            public string treasureResourceBonus { get; set; }
            public int acceptance { get; set; }
            public string acceptanceProduction { get; set; }
            public Tributes tributes { get; set; }
            public string tributesCapacity { get; set; }
            public int tributeTreasures { get; set; }
            public int tributeProduction { get; set; }
            public Tributeproductiondetail tributeProductionDetail { get; set; }
            public string tributeTime { get; set; }
            public int tributesRequiredToFetch { get; set; }
            public int estimatedWarehouseLevel { get; set; }
        }

        public class Coordinates
        {
            public string x { get; set; }
            public string y { get; set; }
        }

        public class Production
        {
            public string _1 { get; set; }
            public string _2 { get; set; }
            public string _3 { get; set; }
            public string _4 { get; set; }
        }

        public class Storage
        {
            public float _1 { get; set; }
            public float _2 { get; set; }
            public float _3 { get; set; }
            public float _4 { get; set; }
        }

        public class Treasury
        {
            public string _1 { get; set; }
            public string _2 { get; set; }
            public string _3 { get; set; }
            public int _4 { get; set; }
        }

        public class Storagecapacity
        {
            public string _1 { get; set; }
            public string _2 { get; set; }
            public string _3 { get; set; }
            public string _4 { get; set; }
        }

        public class Tributes
        {
            public int _1 { get; set; }
            public int _2 { get; set; }
            public int _3 { get; set; }
            public int _4 { get; set; }
        }

        public class Tributeproductiondetail
        {
            public int _1 { get; set; }
            public int _2 { get; set; }
            public int _3 { get; set; }
            public int _4 { get; set; }
        }

    }
}
