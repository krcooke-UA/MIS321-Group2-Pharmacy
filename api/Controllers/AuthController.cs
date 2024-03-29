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
    public class AuthController : ControllerBase
    {
        //The main Data handler for user. Change to a different class file if different db.
        IAuthDataHandler dataHandler = new AuthDataHandler();  

        // POST: api/Auth/Register
        [EnableCors("OpenPolicy")]
        [HttpPost("Register")]
        public Register RegisterAuth ([FromBody] User newUser)
        {
            return dataHandler.RegisterUser(newUser);
        }

        // PUT: api/User/5
        [EnableCors("OpenPolicy")]
        [HttpPost("Login")]
        public Login LoginAuth([FromBody] User newUser)
        {
            dataHandler.RemoveExpiredToken();
            return dataHandler.LoginUser(newUser);
        }

        [EnableCors("OpenPolicy")]
        [HttpPost("Logout")]
        public Login LogoutAuth([FromBody] AuthToken token)
        {
            return dataHandler.LogoutUser(token);
        }

        [EnableCors("OpenPolicy")]
        [HttpPost("Validate-Token")]
        public bool ValidateToken([FromBody] AuthToken token)
        {
            return dataHandler.IsTokenValid(token);
        }
    }
}
