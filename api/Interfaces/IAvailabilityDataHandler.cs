using api.Models;
using System.Collections.Generic;

namespace api.Interfaces
{
    public interface IAvailabilityDataHandler
    {
         List<Availability> SelectAvailabilities(string id);
    }
}