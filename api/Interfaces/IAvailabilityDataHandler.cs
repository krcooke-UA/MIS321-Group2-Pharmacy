using api.Models;
using System.Collections.Generic;

namespace api.Interfaces
{
    public interface IAvailabilityDataHandler
    {
        public List<Availability> SelectAvailabilities();
         List<Availability> SelectAvailabilities(string id);
         List<Availability> GetBookedAvailabilities(string id);
         void MakeAvailability(Availability availability);
    }
}