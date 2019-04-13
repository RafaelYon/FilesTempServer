using System;

namespace TempFileServer.Models
{
    public class LoginToken
    {
        public string access_token  { get; set; } 
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string access_code { get; set; }
    }
}