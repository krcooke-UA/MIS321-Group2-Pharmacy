using System;

namespace api.Models
{
    public class AuthToken
    {
        public Guid Token { get; set;}
        public DateTime TimeStamp { get;set;}
        public AuthToken(Guid token) {
            Token = token;
            TimeStamp = DateTime.Now;
        }
    }
}