namespace Backend.Common.Configurations.AppConfiguration
{
    public class CookieConfiguration
    {
        public string Domain { get; set; }
        
        public bool HttpOnly { get; set; }
        
        public bool Secure { get; set; }
        
        public int MaxAgeDays { get; set; }
        
        public string CookieName { get; set; }
    }
}