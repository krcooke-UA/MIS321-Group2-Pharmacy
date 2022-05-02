namespace api.Models
{
    public class Register
    {
        public int Response {get;set;}
        public string Message { get; set;}
        public string Email { get;set;}
        public int Id { get;set;}
        public string Type { get;set;}
        public AuthToken AuthToken { get;set;}
    }
}