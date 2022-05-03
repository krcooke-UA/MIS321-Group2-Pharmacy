using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Interfaces;
using api.Database;
using Microsoft.AspNetCore.Cors; 

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        IAppointmentDataHandler appointmentDataHandler = new AppointmentDataHandler();

        // GET: api/Appointment
        [EnableCors("OpenPolicy")]
        [HttpGet("GetCustomerAppointments/{id}")]
        public List<Appointment> GetCustomer(string id)
        {
            return appointmentDataHandler.GetCustomerAppointments(id);
        }

        // GET: api/Appointment/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetAppointments")]
        public List<Appointment> Get(string id)
        {
            return appointmentDataHandler.GetAppointments(id);
        }

        // POST: api/Appointment
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Appointment value)
        {
            appointmentDataHandler.MakeAppointment(value);
        }

        // PUT: api/Appointment/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Appointment/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
