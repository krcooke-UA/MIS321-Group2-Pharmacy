using System;

namespace api.Models
{
    public class Availability
    {
        public User user {get; set;}
        public int Id {get; set;}
        public string Date {get; set;}
        public string Time {get; set;}
        public string Timeslot_Text {get; set;}
        public int Timeslot_Id {get; set;}
    }
}