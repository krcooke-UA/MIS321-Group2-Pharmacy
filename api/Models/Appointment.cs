using System;

namespace api.Models
{
    public class Appointment
    {
        public int User_Id {get; set;}
        public int Id {get; set;}
        public string Date {get; set;}
        public string Time {get; set;}
        public int Type_Id {get; set;}
        public string Type_Text {get; set;}
        public int Availability_Id {get; set;}
        public int Timeslot_Id {get; set;}
    }
}