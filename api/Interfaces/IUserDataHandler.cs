using api.Models;
using System.Collections.Generic;

namespace api.Interfaces
{
    public interface IUserDataHandler
    {
        List<User> Select();
         List<User> SelectOneById(int id);
         List<User> SelectOneByUsername(string username);
         void Delete(int id);
         void Insert(User User);
         void Update(User User);

         Dictionary<string, object> GetValues(User User);
    }
}