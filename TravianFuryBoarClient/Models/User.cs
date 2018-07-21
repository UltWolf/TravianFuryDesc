namespace TravianFuryBoarClient.Models
{
    public class User
    {
        public int ProfileId { get; set; }  
        public string Username { get; set; }    
        public string Password { get; set; }    
        public string LastToken { get; set; }
    }
}