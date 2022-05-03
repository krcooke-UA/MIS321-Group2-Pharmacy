using System;

namespace api.Models
{
    public class Availability
    {
        public int User_Id {get; set;}
        public int Id {get; set;}
        public string Date {get; set;}
        public string Time {get; set;}
        public string Timeslot_Text {get; set;}
        public int Timeslot_Id {get; set;}

        public string StartTimeSlot {get;set;}
        public string EndTimeSlot {get;set;}

        public string StartTime {get;set;}
        public string EndTime {get;set;}
    }
}