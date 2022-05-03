using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Dynamic;
using api.Interfaces;
using api.Models;
using api.Database;

namespace api.Database
{
    public class AvailabilityDataHandler : IAvailabilityDataHandler
    {
        private Database db { get; set; }

        public AvailabilityDataHandler()
        {
            db = new Database();
        }
        public List<Availability> SelectAvailabilities()
        {
            List<Availability> availabilities = new List<Availability>();

            string stm = @"SELECT DISTINCT
	                        CONCAT_WS(' ', user_fname, user_lname) AS full_name,
                            DATE_FORMAT(availability_startdate, '%Y-%m-%d') AS date,
                            DATE_FORMAT(availability_startdate, '%T') AS start_time,
                            DATE_FORMAT(availability_enddate, '%T') AS end_time
                            FROM availability
                            JOIN users USING(user_id)
                            JOIN availability_detail USING(availability_id);";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);

            foreach (dynamic item in results)
            {
                string time = item.start_time + " - " + item.end_time;
                Availability tempAvailability = new Availability()
                {
                    FullName = item.full_name,
                    Date = item.date,
                    Time = time
                };

                availabilities.Add(tempAvailability);
            }
            db.Close();

            return availabilities;
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
                Availability tempAvailability = new Availability()
                {
                    User_Id = item.user_id,
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
                Availability tempAvailability = new Availability()
                {
                    User_Id = item.user_id,
                    Id = item.availability_id,
                    Date = item.date
                };

                availabilities.Add(tempAvailability);
            }
            db.Close();

            return availabilities;
        }

        public void MakeAvailability(Availability availability) {
            string stm1 = @"SELECT availability_startdate, availability_enddate FROM
                            (
	                            SELECT DATE_FORMAT(timeslot_date, '%Y-%m-%d %T') AS availability_startdate FROM calendar WHERE timeslot_id = @id1
                            ) AS startdate JOIN
                            (
	                            SELECT DATE_FORMAT(timeslot_date, '%Y-%m-%d %T') AS availability_enddate FROM calendar WHERE timeslot_id = @id2
                            ) AS enddate";
            db.Open();
            List<ExpandoObject> results1 = db.Select(stm1, availability.StartTimeSlot, availability.EndTimeSlot);
            foreach (dynamic item in results1)
            {
                availability.StartTime = item.availability_startdate;
                availability.EndTime = item.availability_enddate;
            }

            string stm2 = @"INSERT INTO availability
                            (user_id, availability_startdate, availability_enddate)
                            VALUES
                            (@user_id, @availability_startdate, @availability_enddate)";
            var values1 = GetValues(availability);
            db.Insert(stm2, values1);

            string stm3 = @"SELECT availability_id
                            FROM availability
                            WHERE availability_startdate = @id1 AND
                            availability_enddate = @id2 AND
                            user_id = @id3";
            List<ExpandoObject> results2 = db.Select(stm3, availability.StartTime, availability.EndTime, availability.User_Id);
            foreach (dynamic item in results2)
            {
                availability.Id = item.availability_id;
            }

            string stm4 = @"SELECT timeslot_id
                            FROM calendar
                            WHERE timeslot_id BETWEEN @id1 AND @id2";
            List<ExpandoObject> results3 = db.Select(stm4, availability.StartTimeSlot, availability.EndTimeSlot);
            List<Timeslot> timeslots = new List<Timeslot>();
            foreach (dynamic item in results3)
            {
                Timeslot timeslot = new Timeslot()
                {
                    Id = item.timeslot_id
                };
                timeslots.Add(timeslot);
            }

            string stm5 = @"INSERT INTO availability_detail
                            (availability_id, timeslot_id)
                            VALUES
                            (@availability_id, @timeslot_id)";
            foreach(Timeslot timeslot in timeslots) {
                var values2 = GetValuesAD(availability.Id, timeslot.Id);
                db.Insert(stm5, values2);
            }

            db.Close();
        }

        public Dictionary<string, object> GetValues(Availability availability)
        {
            var values = new Dictionary<string, object>(){
                {"@user_id", availability.User_Id},
                {"@availability_startdate", availability.StartTime},
                {"@availability_enddate", availability.EndTime}
            };

            return values;
        }
        public Dictionary<string, object> GetValuesAD(int availability_id, int timeslot_id)
        {
            var values = new Dictionary<string, object>(){
                {"@availability_id", availability_id},
                {"@timeslot_id", timeslot_id}
            };

            return values;
        }
    }
}