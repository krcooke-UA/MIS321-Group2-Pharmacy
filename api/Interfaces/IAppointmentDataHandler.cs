using api.Models;
using System.Collections.Generic;

namespace api.Interfaces
{
    public interface IAppointmentDataHandler
    {
         public List<Appointment> GetAppointments(string id);
         public List<Appointment> GetCustomerAppointments(string id);
         public List<Appointment> SelectAppointments(string id);
         public void MakeAppointment(Appointment appointment);
    }
}