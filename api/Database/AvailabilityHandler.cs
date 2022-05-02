using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Dynamic;
using api.Interfaces;
using api.Models;
using api.Database;

namespace api.Database
{
    public class AvailabilityHandler : IAvailabilityDataHandler
    {
        private Database db { get; set; }

        public AvailabilityHandler()
        {
            db = new Database();
        }
        public List<Availability> SelectAvailabilities(string id)
        {
            List<Availability> availabilities = new List<Availability>();

            string stm = @"SELECT user_id, availability_id,
                            DATE_FORMAT(availability_startdate, '%Y-%m-%d') AS date,
                            DATE_FORMAT(timeslot_date, '%T') AS time, timeslot_text, timeslot_id
                            FROM availability
                            JOIN availability_detail USING(availability_id)
                            JOIN calendar USING(timeslot_id)
                            WHERE appointment_id IS NULL
                            AND DATE_FORMAT(availability_startdate, '%Y-%m-%d') = @id";
            db.Open();
            List<ExpandoObject> results = db.Select(stm, id);

            foreach (dynamic item in results)
            {
                User tempUser = new User()
                {
                    Id = item.user_id
                };
                Availability tempAvailability = new Availability()
                {
                    user = tempUser,
                    Id = item.availability_id,
                    Date = item.date,
                    Time = item.time,
                    Timeslot_Text = item.timeslot_text,
                    Timeslot_Id = item.timeslot_id
                };

                availabilities.Add(tempAvailability);
            }
            db.Close();

            return availabilities;
        }

        public List<Availability> GetBookedAvailabilities(string id)
        {
            List<Availability> availabilities = new List<Availability>();

            string stm = @"SELECT user_id, availability_id,
	                        DATE_FORMAT(availability_startdate, '%Y-%m-%d') AS date
                            FROM availability
                            WHERE user_id = @id";
            db.Open();
            List<ExpandoObject> results = db.Select(stm, id);

            foreach (dynamic item in results)
            {
                User tempUser = new User()
                {
                    Id = item.user_id
                };
                Availability tempAvailability = new Availability()
                {
                    user = tempUser,
                    Id = item.availability_id,
                    Date = item.date
                };

                availabilities.Add(tempAvailability);
            }
            db.Close();

            return availabilities;
        }
    }
}