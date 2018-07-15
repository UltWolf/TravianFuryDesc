

public class PlayerObject
{
    public CachePlayer[] cache { get; set; }
    public int ignoreSerial { get; set; }
    public long time { get; set; }
    public int serialNo { get; set; }
    public object[] response { get; set; }
}

public class CachePlayer
{
    public string name { get; set; }
    public DataPlayer data { get; set; }
}

public class DataPlayer
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
    public string population { get; set; }
    public string active { get; set; }
    public int prestige { get; set; }
    public int level { get; set; }
    public int nextLevelPrestige { get; set; }
    public bool hasNoobProtection { get; set; }
    public bool filterInformation { get; set; }
    public string signupTime { get; set; }
    public string vacationState { get; set; }
}





public class Coordinates
{
    public string x { get; set; }
    public string y { get; set; }
}
