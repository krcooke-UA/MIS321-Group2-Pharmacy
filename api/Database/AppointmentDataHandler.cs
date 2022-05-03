using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Dynamic;
using api.Interfaces;
using api.Models;
using api.Database;

namespace api.Database
{
    public class AppointmentDataHandler : IAppointmentDataHandler
        {
        private Database db { get; set; }

        public AppointmentDataHandler()
        {
            db = new Database();
        }
        public List<Appointment> GetAppointments(string id) {
            List<Appointment> appointments = new List<Appointment>();

            string stm = @"SELECT DISTINCT
                            DATE_FORMAT(availability_startdate, '%Y-%m-%d') AS date,
                            DATE_FORMAT(availability_startdate, '%T') AS time,
                            customer_full_name
                            FROM availability
                            JOIN availability_detail USING(availability_id)
                            JOIN appointments USING(appointment_id)
                            JOIN
                            (
                                SELECT customer_id, customer_full_name FROM customers
                            ) AS customers ON appointments.user_id = customers.customer_id
                            WHERE availability.user_id = @id;";
            db.Open();
            List<ExpandoObject> results = db.Select(stm, id);

            foreach (dynamic item in results)
            {
                Appointment tempAppointment = new Appointment()
                {
                    Date = item.date,
                    Time = item.time,
                    FullName = item.customer_full_name
                };

                appointments.Add(tempAppointment);
            }
            db.Close();

            return appointments;

        }
        public List<Appointment> GetCustomerAppointments(string id) {
            List<Appointment> appointments = new List<Appointment>();

            string stm = @"SELECT
                            DATE_FORMAT(appointment_date, '%Y-%m-%d') AS date,
                            DATE_FORMAT(appointment_date, '%T') AS time
                            FROM appointments
                            WHERE user_id = @id;";
            db.Open();
            List<ExpandoObject> results = db.Select(stm, id);

            foreach (dynamic item in results)
            {
                Appointment tempAppointment = new Appointment()
                {
                    Date = item.date,
                    Time = item.time
                };

                appointments.Add(tempAppointment);
            }
            db.Close();

            return appointments;
        }
        public List<Appointment> SelectAppointments(string id)
        {
            List<Appointment> appointments = new List<Appointment>();

            string stm = @"SELECT user_id, appointment_id,
                            DATE_FORMAT(appointment_date, '%Y-%m-%d') AS date,
                            DATE_FORMAT(appointment_date, '%T') AS time
                            FROM appointments
                            WHERE user_id = @id
                            AND appointment_date < CURRENT_DATE()";
            db.Open();
            List<ExpandoObject> results = db.Select(stm, id);

            foreach (dynamic item in results)
            {
                Appointment tempAppointment = new Appointment()
                {
                    User_Id = item.user_id,
                    Id = item.appointment_id,
                    Date = item.date,
                    Time = item.time,
                    Availability_Id = item.availability_id,
                    Timeslot_Id = item.timeslot_id
                };

                appointments.Add(tempAppointment);
            }
            db.Close();

            return appointments;
        }
        public void MakeAppointment(Appointment appointment) {

            string stm1 = @"INSERT INTO appointments
                (user_id, appointment_date)
                VALUES
                (@user_id, @appointment_date)";

            db.Open();
            var values1 = GetValues(appointment);
            db.Insert(stm1, values1);

            var datetime = appointment.Date + " " + appointment.Time;
            string stm2 = @"SELECT appointment_id
                    FROM appointments
                    WHERE user_id = @user_id AND appointment_date = @appointment_date";
            appointment.Id = db.SelectAvailabilityDetail(stm2, appointment.User_Id, datetime);

            string stm3 = @"UPDATE availability_detail SET
                    appointment_id = @appointment_id
                    WHERE availability_id = @availability_id AND timeslot_id = @timeslot_id";
            var values2 = GetValuesAD(appointment, appointment.Id);
            db.Insert(stm3, values2);

            db.Close();
        }
        public Dictionary<string, object> GetValues(Appointment appointment)
        {
            var datetime = appointment.Date + " " + appointment.Time;
            var values = new Dictionary<string, object>(){
                {"@user_id", appointment.User_Id},
                {"@appointment_date", datetime}
            };

            return values;
        }
        public Dictionary<string, object> GetValuesAD(Appointment appointment, int appointment_id)
        {
            var values = new Dictionary<string, object>(){
                {"@availability_id", appointment.Availability_Id},
                {"@timeslot_id", appointment.Timeslot_Id},
                {"@appointment_id", appointment_id}
            };

            return values;
        }
    }
}