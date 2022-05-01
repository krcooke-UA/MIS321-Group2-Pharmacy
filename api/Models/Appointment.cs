using System;

namespace api.Models
{
    public class Appointment
    {
        public int Id {get; set;}
        public string Date {get; set;}
        public string Time {get; set;}
        public string Notes {get; set;}
        public string Type_Text {get; set;}
        public User user {get; set;}
    }
}