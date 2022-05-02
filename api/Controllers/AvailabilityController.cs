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
    public class AvailabilityController : ControllerBase
    {
        IAvailabilityDataHandler availabilityHandler = new AvailabilityHandler();
        // GET: api/Availability
        [EnableCors("OpenPolicy")]
        // [HttpGet("{id}", Name = "GetListed")]
        [HttpGet("GetListed/{id}")]
        public List<Availability> GetListed(string id)
        {
            return availabilityHandler.GetBookedAvailabilities(id);
        }

        // GET: api/Availability/5
        [EnableCors("OpenPolicy")]
        [HttpGet("GetAvailability/{id}")]
        public List<Availability> GetAvailability(string id)
        {
            return availabilityHandler.SelectAvailabilities(id);
        }

        // POST: api/Availability
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Availability/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Availability/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
