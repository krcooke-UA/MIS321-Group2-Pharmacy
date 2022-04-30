using System;

namespace api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public string Street { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Type_id { get; set; } 
    }
}