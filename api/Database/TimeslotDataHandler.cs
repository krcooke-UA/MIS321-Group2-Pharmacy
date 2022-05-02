using System.Collections.Generic;
using System.Dynamic;
using api.Interfaces;
using api.Models;

namespace api.Database
{
    public class TimeslotDataHandler : ITimeslotDataHandler
    {
        private Database db { get; set; }

        public TimeslotDataHandler()
        {
            db = new Database();
        }
        public List<Timeslot> SelectTimeslots(string id)
        {
            List<Timeslot> timeslots = new List<Timeslot>();

            string stm = @"SELECT timeslot_id, timeslot_date,
                DATE_FORMAT(timeslot_date, '%Y-%m-%d') AS date, 
                DATE_FORMAT(timeslot_date, '%T') AS time, timeslot_text
                FROM calendar WHERE DATE_FORMAT(timeslot_date, '%Y-%m-%d') = @id";
            db.Open();
            List<ExpandoObject> results = db.Select(stm, id);


            foreach (dynamic item in results)
            {
                Timeslot temp = new Timeslot()
                {
                    Id = item.timeslot_id,
                    Datetime = item.timeslot_date,
                    Date = item.date,
                    Time = item.time,
                    Text = item.timeslot_text
                };

                timeslots.Add(temp);
            }
            db.Close();
            return timeslots;
        }
    }
}