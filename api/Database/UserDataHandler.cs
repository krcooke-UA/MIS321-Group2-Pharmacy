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
                    Email = item.user_email,
                    Password = item.user_password
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
                    Email = item.user_email,
                    Password = item.user_password
                };

                myUser.Add(temp);
            }
            db.Close();
            return myUser;
        }
        public List<User> SelectOneByUsername(string email)
        {
            List<User> myUser = new List<User>();

            string stm = @"SELECT * from users WHERE user_email = '" + email + "' LIMIT 1";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                User temp = new User()
                {
                    Id = item.user_id,
                    Email = item.user_email,
                    Password = item.user_password
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

            string stm = @"INSERT INTO users(user_fname, user_lname, user_email, user_password, user_type_id)
                VALUES(@user_fname, @user_lname, @user_email, @user_password, 2)";

            db.Open();
            db.Insert(stm, values);
            db.Close();
        }

        public void Update(User User)
        {
            System.Console.WriteLine("Made it to the update");

            var values = GetValues(User);

            string stm = @"UPDATE users SET
            user_email = @user_email,
            user_password = @user_password
            WHERE user_id = @user_id";

            db.Open();
            db.Update(stm, values);
            db.Close();
        }

       

        public Dictionary<string, object> GetValues(User User)
        {
            var values = new Dictionary<string, object>(){
                {"@user_fname", User.Fname},
                {"@user_lname", User.Lname},
                {"@user_password", User.Password},
                {"@user_email", User.Email},
                
            };

            return values;
        }
    }
}