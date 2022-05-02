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
    public class UserController : ControllerBase
    {
        //The main Data handler for user. Change to a different class file if different db.
        IUserDataHandler dataHandler = new UserDataHandler();  

        // GET: api/User
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<User> Get()
        {
            return dataHandler.Select();
        }

        // GET: api/User/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetUser")]
        public List<User> Get(int id)
        {
            return dataHandler.SelectOneById(id);
        }

        // POST: api/User
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] User newUser)
        {
            dataHandler.Insert(newUser);
        }

        // PUT: api/User/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put([FromBody] User updatedUser)
        {
            dataHandler.Update(updatedUser);
        }

        // DELETE: api/User/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dataHandler.Delete(id);
        }
    }
}
