using api.Interfaces;
using api.Models;
using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace api.Database
{
    public class AuthDataHandler : IAuthDataHandler
    {
        const double TOKEN_EXPIRATION_TIME = 1;
        IUserDataHandler dataHandler = new UserDataHandler();
        static List<AuthToken> allSessionTokens = new List<AuthToken>();
        public bool IsTokenValid(AuthToken authToken)
        {
            foreach(AuthToken storedToken in allSessionTokens) {
                if(authToken.Token == storedToken.Token) return true;
            }
            return false;
        }

        public Login LoginUser(User thisUser)
        {
            if(thisUser.Email == null || thisUser.Password == null) {
                return new Login() {
                    Response = 400,
                    Message = "Null stuff"
                };
            }
            User foundUser;
            try {
                foundUser = dataHandler.SelectOneByUsername(thisUser.Email)[0];
            }
            catch {
                return new Login() {
                    Response = 400,
                    Message = "No user found"
                };
            }
            string hashedPasswordFromUserInput = HashPassword(thisUser.Password);
            if(hashedPasswordFromUserInput == foundUser.Password) {
                Guid thisGuid = Guid.NewGuid();
                allSessionTokens.Add(new AuthToken(thisGuid));
                Console.WriteLine(thisUser.Email + " is valid");
                return new Login() {
                    Response = 200,
                    Message = "User is valid",
                    Email = thisUser.Email,
                    Id = foundUser.Id.ToString(),
                    Type = foundUser.Type_Text,
                    AuthToken = new AuthToken(thisGuid)
                };
            }
            else {
                return new Login() {
                    Response = 400,
                    Message = "Wrong info"
                };
            }
        }

        public Login LogoutUser(AuthToken authToken)
        {
            int index = -1;
            for(int i = 0; i < allSessionTokens.Count; i++) {
                if(authToken.Token == allSessionTokens[i].Token) {
                    index = i;
                }
            }
            try {
                allSessionTokens.RemoveAt(index);
                return new Login() {
                    Response = 200,
                    Message = "Logout good"
                };
            }
            catch {
                return new Login() {
                    Response = 400,
                    Message = "Login bad"
                };
            }
        }

        public Register RegisterUser(User newUser)
        {
            try{
                List<User> userList = dataHandler.SelectOneByUsername(newUser.Email);
                if(userList.Count > 0) {
                    return new Register() {
                        Response = 400,
                        Message = "User already exists",
                    };
                }
            }
            catch {
                Console.WriteLine("User DNE");
            }
            string hashedPassword = HashPassword(newUser.Password);
            Console.WriteLine(hashedPassword);
            newUser.Password = hashedPassword;

            try{
                dataHandler.Insert(newUser);
            }
            catch(Exception e) {
                return new Register() {
                    Response = 400,
                    Message = e.Message
                };
            }
            User foundUser;
            foundUser = dataHandler.SelectOneByUsername(newUser.Email)[0];
            Guid thisGuid = Guid.NewGuid();
            allSessionTokens.Add(new AuthToken(thisGuid));
            return new Register() {
                Response = 200,
                Message = $"User {foundUser.Email} created",
                Email = foundUser.Email,
                Id = foundUser.Id,
                Type = foundUser.Type_Text,
                AuthToken = new AuthToken(thisGuid)
            };
        }

        public void RemoveExpiredToken()
        {
            DateTime currentDate = DateTime.Now;
            foreach(AuthToken authToken in allSessionTokens) {
                TimeSpan ts = DateTime.Now - authToken.TimeStamp;
                if(ts.TotalHours > TOKEN_EXPIRATION_TIME) {
                    allSessionTokens.Remove(authToken);
                }
            }
        }

        private string HashPassword(string notHashedPass) {
            byte[] salt = new byte[4499];
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: notHashedPass,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));
            return hashed;
        }
    }
}