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
        [HttpGet()]
        public int Get()
        {
            return 0;
        }

        // GET: api/Availability/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetAvailability")]
        public List<Availability> Get(string id)
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
