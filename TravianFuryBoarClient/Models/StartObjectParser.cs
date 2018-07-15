using System;


namespace TravianFuryBoarClient.Models
{
    public class StartObjectParser
    {
        public Cache[] cache { get; set; }
        public int ignoreSerial { get; set; }
        public int serialNo { get; set; }
        public Event[] _event { get; set; }
        public object[] response { get; set; }
    }

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
        public string playerId { get; set; }
        public string playerName { get; set; }
        public string kingdomId { get; set; }
        public string kingdomTag { get; set; }
        public string population { get; set; }
        public string tribe { get; set; }
        public object treasures { get; set; }
        public string villageId { get; set; }
        public object buildingTypes { get; set; }
        public string tribeId { get; set; }
        public bool canUseInstantConstruction { get; set; }
        public bool canUseInstantConstructionOnlyInVillage { get; set; }
        public Cache[] cache { get; set; }
        public int operation { get; set; }
        public string destVillageId { get; set; }
        public string status { get; set; }
        public float health { get; set; }
        public string lastHealthTime { get; set; }
        public int baseRegenerationRate { get; set; }
        public int regenerationRate { get; set; }
        public int fightStrength { get; set; }
        public string fightStrengthPoints { get; set; }
        public string attBonusPoints { get; set; }
        public string defBonusPoints { get; set; }
        public string resBonusPoints { get; set; }
        public string resBonusType { get; set; }
        public string freePoints { get; set; }
        public int speed { get; set; }
        public string untilTime { get; set; }
        public string maxScrollsPerDay { get; set; }
        public string scrollsUsedToday { get; set; }
        public string waterbucketUsedToday { get; set; }
        public string ointmentsUsedToday { get; set; }
        public string adventurePointCardUsedToday { get; set; }
        public string resourceChestsUsedToday { get; set; }
        public string cropChestsUsedToday { get; set; }
        public string artworkUsedToday { get; set; }
        public bool isMoving { get; set; }
        public string adventurePoints { get; set; }
        public string adventurePointTime { get; set; }
        public string levelUp { get; set; }
        public string xp { get; set; }
        public int xpThisLevel { get; set; }
        public int xpNextLevel { get; set; }
        public object level { get; set; }
        public string fetchedFromLobby { get; set; }
        public string gender { get; set; }
        public string hairColor { get; set; }
        public string groupId { get; set; }
        public string tag { get; set; }
        public string creationTime { get; set; }
        public string kingdomType { get; set; }
        public string name { get; set; }
        public string kingdomRole { get; set; }
        public bool isKing { get; set; }
        public string kingId { get; set; }
        public string kingstatus { get; set; }
        public Village[] villages { get; set; }
        public string active { get; set; }
        public int prestige { get; set; }
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
        public object dailyQuestsExchanged { get; set; }
        public string avatarIdentifier { get; set; }
        public string vacationStateStart { get; set; }
        public string vacationStateEnd { get; set; }
        public string usedVacationDays { get; set; }
        public string premiumConfirmation { get; set; }
        public string lang { get; set; }
        public string onlineStatusFilter { get; set; }
        public string extendedSimulator { get; set; }
        public string musicVolume { get; set; }
        public string soundVolume { get; set; }
        public string uiSoundVolume { get; set; }
        public string muteAll { get; set; }
        public string timeZone { get; set; }
        public string timeFormat { get; set; }
        public string attacksFilter { get; set; }
        public string mapFilter { get; set; }
        public int disableTabNotifications { get; set; }
        public string enableTabNotifications { get; set; }
        public string disableAnimations { get; set; }
        public string notpadsVisible { get; set; }
        public bool disableHelpNotifications { get; set; }
        public string enableHelpNotifications { get; set; }
        public string enableWelcomeScreen { get; set; }
        public string sessionId { get; set; }
        public string userAccountIdentifier { get; set; }
        public object type { get; set; }
        public object rights { get; set; }
        public string belongsToKing { get; set; }
        public string belongsToKingdom { get; set; }
        public bool isMainVillage { get; set; }
        public bool isTown { get; set; }
        public string treasuresUsable { get; set; }
        public string allowTributeCollection { get; set; }
        public string protectionGranted { get; set; }
        public int tributeCollectorPlayerId { get; set; }
        public float realTributePercent { get; set; }
        public string supplyBuildings { get; set; }
        public int supplyTroops { get; set; }

        public string usedControlPoints { get; set; }
        public string availableControlPoints { get; set; }
        public float culturePoints { get; set; }
        public string celebrationType { get; set; }
        public string celebrationEnd { get; set; }
        public string culturePointProduction { get; set; }
        public string treasureResourceBonus { get; set; }
        public int acceptance { get; set; }
        public string acceptanceProduction { get; set; }
        public string tributesCapacity { get; set; }
        public int tributeTreasures { get; set; }
        public int tributeProduction { get; set; }
        public string tributeTime { get; set; }
        public int tributesRequiredToFetch { get; set; }
        public int estimatedWarehouseLevel { get; set; }
        public string troopId { get; set; }
        public object villageName { get; set; }
        public string villageIdLocation { get; set; }
        public string villageNameLocation { get; set; }
        public string playerIdLocation { get; set; }
        public string playerNameLocation { get; set; }
        public object filter { get; set; }
        public string villageIdSupply { get; set; }
        public int capacity { get; set; }
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
        public Coordinates2 coordinates { get; set; }
        public bool isMainVillage { get; set; }
        public bool isTown { get; set; }
        public string treasuresUsable { get; set; }
        public string treasures { get; set; }
        public string allowTributeCollection { get; set; }
        public string protectionGranted { get; set; }
        public object tributeCollectorPlayerId { get; set; }
        public float realTributePercent { get; set; }
        public string supplyBuildings { get; set; }
        public object supplyTroops { get; set; }
        public Production2 production { get; set; }
        public Storage2 storage { get; set; }
        public Treasury2 treasury { get; set; }
        public Storagecapacity2 storageCapacity { get; set; }
        public string usedControlPoints { get; set; }
        public string availableControlPoints { get; set; }
        public float culturePoints { get; set; }
        public string celebrationType { get; set; }
        public string celebrationEnd { get; set; }
        public string culturePointProduction { get; set; }
        public string treasureResourceBonus { get; set; }
        public int acceptance { get; set; }
        public string acceptanceProduction { get; set; }
        public Tributes2 tributes { get; set; }
        public string tributesCapacity { get; set; }
        public int tributeTreasures { get; set; }
        public int tributeProduction { get; set; }
        public Tributeproductiondetail2 tributeProductionDetail { get; set; }
        public string tributeTime { get; set; }
        public int tributesRequiredToFetch { get; set; }
        public int estimatedWarehouseLevel { get; set; }
        public float bonusCulturePointProduction { get; set; }
    }

}