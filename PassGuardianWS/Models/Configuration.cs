namespace PassGuardianWS.Models
{
    public class Configuration
    {
        public int ConfigurationID { get; set; }
        public int UserID { get; set; }
        public int ChangeFrequency { get; set; }
        public int PasswordLength { get; set; }
        public bool Day { get; set; }
        public bool Week { get; set; }
        public bool Month { get; set; }

        
    }
}
