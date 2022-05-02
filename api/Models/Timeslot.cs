using System;

namespace api.Models
{
    public class Timeslot
    {
        public int Id {get;set;}
        public DateTime Datetime {get;set;}
        public string Date {get;set;}
        public string Time {get;set;}
        public string Text {get;set;}
    }
}