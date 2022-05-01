using api.Models;

namespace api.Interfaces
{
    public interface IAuthDataHandler
    {
         Register RegisterUser(User newUser);
         Login LoginUser(User thisUser);
         Login LogoutUser(AuthToken authToken);
         bool IsTokenValid(AuthToken authToken);
         void RemoveExpiredToken();
    }
}