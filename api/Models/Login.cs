namespace api.Models
{
    public class Login
    {
        public int Response {get;set;}
        public string Message { get; set;}
        public string Username { get;set;}
        public AuthToken AuthToken { get;set;}
    }
}