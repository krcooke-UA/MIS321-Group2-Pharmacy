namespace api.Models
{
    public class Login
    {
        public int Response {get;set;}
        public string Message { get; set;}
        public string Email { get;set;}
        public string Id { get;set;}
        public string Type { get;set;}
        public AuthToken AuthToken { get;set;}
    }
}