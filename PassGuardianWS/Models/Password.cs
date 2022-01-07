using System;
using System.Collections.Generic;

namespace PassGuardianWS.Models
{
    public class Password
    {
        public int PasswordID { get; set; }
        public string UserName { get; set; }
        public string ApplicationPassword { get; set; }
        public string KeyPassword { get; set; }
        public string ApplicationName { get; set; }
        public int AppIicationId { get; set; }
        public DateTime LastChange { get; set; }
        public int UserID { get; set; }
    }
}
