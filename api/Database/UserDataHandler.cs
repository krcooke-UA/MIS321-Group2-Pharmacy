using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Dynamic;
using api.Interfaces;
using api.Models;
using api.Database;

namespace api.Database
{
    public class UserDataHandler : IUserDataHandler
    {
        private Database db { get; set; }

        public UserDataHandler()
        {
            db = new Database();
        }

        public List<User> Select()
        {
            List<User> myUser = new List<User>();

            string stm = @"SELECT * from users";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                User temp = new User()
                {
                    Id = item.user_id,
                    Username = item.username,
                    Password = item.h_password
                };

                myUser.Add(temp);
            }
            db.Close();
            return myUser;
        }

        public List<User> SelectOneById(int id)
        {
            List<User> myUser = new List<User>();

            string stm = @"SELECT * from users WHERE user_id = " + id + " LIMIT 1";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                User temp = new User()
                {
                    Id = item.user_id,
                    Username = item.username,
                    Password = item.h_password
                };

                myUser.Add(temp);
            }
            db.Close();
            return myUser;
        }
        public List<User> SelectOneByUsername(string username)
        {
            List<User> myUser = new List<User>();

            string stm = @"SELECT * from users WHERE username = '" + username + "' LIMIT 1";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                User temp = new User()
                {
                    Id = item.user_id,
                    Username = item.username,
                    Password = item.h_password
                };

                myUser.Add(temp);
            }
            db.Close();
            return myUser;
        }
        

        public void Delete(int id)
        {
            string stm = $@"DELETE FROM Users WHERE user_id = {id}";

            db.Open();
            db.Delete(stm);
            db.Close();
        }

        public void Insert(User User)
        {
            System.Console.WriteLine("Made it to the insert");

            var values = GetValues(User);

            string stm = @"INSERT INTO users(username, password)
             VALUES(@username, @password)";

            db.Open();
            db.Insert(stm, values);
            db.Close();
        }

        public void Update(User User)
        {
            System.Console.WriteLine("Made it to the update");

            var values = GetValues(User);

            string stm = @"UPDATE users SET
            username = @username,
            h_password = @password
            WHERE user_id = @user_id";

            db.Open();
            db.Update(stm, values);
            db.Close();
        }

       

        public Dictionary<string, object> GetValues(User User)
        {
            var values = new Dictionary<string, object>(){
                {"@user_id", User.Id},
                {"@password", User.Password},
                {"@username", User.Username},
                
            };

            return values;
        }
    }
}