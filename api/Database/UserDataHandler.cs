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

            string stm = @"SELECT user_id, user_email, user_password, user_fname, user_lname, DATE_FORMAT(user_dob, '%Y-%m-%d') AS dob,
                user_gender, user_city, user_state, user_zipcode, user_street
                FROM users";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                User temp = new User()
                {
                    Id = item.user_id,
                    Email = item.user_email,
                    Password = item.user_password,
                    Fname = item.user_fname,
                    Lname = item.user_lname,
                    Dob = item.dob,
                    Gender = item.user_gender,
                    City = item.user_city,
                    State = item.user_state,
                    Zipcode = item.user_zipcode,
                    Street = item.user_street
                };

                myUser.Add(temp);
            }
            db.Close();
            return myUser;
        }

        public List<User> SelectOneById(int id)
        {
            List<User> myUser = new List<User>();

            string stm = @"SELECT user_id, user_email, user_password, user_fname, user_lname, DATE_FORMAT(user_dob, '%Y-%m-%d') AS dob,
                user_gender, user_city, user_state, user_zipcode, user_street
                FROM users WHERE user_id = " + id + " LIMIT 1";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                User temp = new User()
                {
                    Id = item.user_id,
                    Email = item.user_email,
                    Password = item.user_password,
                    Fname = item.user_fname,
                    Lname = item.user_lname,
                    Dob = item.dob,
                    Gender = item.user_gender,
                    City = item.user_city,
                    State = item.user_state,
                    Zipcode = item.user_zipcode,
                    Street = item.user_street
                };

                myUser.Add(temp);
            }
            db.Close();
            return myUser;
        }
        public List<User> SelectOneByUsername(string email)
        {
            List<User> myUser = new List<User>();

            string stm = @"SELECT user_id, user_email, user_password, user_type_text FROM users
                        JOIN user_types USING(user_type_id)
                        WHERE user_email = '" + email + "' LIMIT 1";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                User temp = new User()
                {
                    Id = item.user_id,
                    Email = item.user_email,
                    Password = item.user_password,
                    Type_Text = item.user_type_text,
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

            User.Dob = "0000-00-00";
            User.Gender = "NA";
            User.City = "NA";
            User.State = "NA";
            User.Zipcode = 0;
            User.Street = "NA";

            var values = GetValues(User);

            string stm = @"INSERT INTO users
                (user_fname, user_lname, user_email, user_password, user_type_id, user_dob, user_gender, user_city, user_state, user_zipcode, user_street)
                VALUES
                (@user_fname, @user_lname, @user_email, @user_password, 2, @user_dob, @user_gender, @user_city, @user_state, @user_zipcode, @user_street)";

            db.Open();
            db.Insert(stm, values);
            db.Close();
        }

        public void Update(User User)
        {
            System.Console.WriteLine("Made it to the update");

            var values = GetValues(User);

            string stm = @"UPDATE users SET
            user_fname = @user_fname,
            user_lname = @user_lname,
            user_dob = @user_dob,
            user_gender = @user_gender,
            user_city = @user_city,
            user_state = @user_state,
            user_zipcode = @user_zipcode,
            user_street = @user_street
            WHERE user_id = @user_id";

            db.Open();
            db.Update(stm, values);
            db.Close();
        }

       

        public Dictionary<string, object> GetValues(User User)
        {
            var values = new Dictionary<string, object>(){
                {"@user_id", User.Id},
                {"@user_fname", User.Fname},
                {"@user_lname", User.Lname},
                {"@user_password", User.Password},
                {"@user_email", User.Email},
                {"@user_dob", User.Dob},
                {"@user_gender", User.Gender},
                {"@user_city", User.City},
                {"@user_state", User.State},
                {"@user_zipcode", User.Zipcode},
                {"@user_street", User.Street}
            };

            return values;
        }
    }
}