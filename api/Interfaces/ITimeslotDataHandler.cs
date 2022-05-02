using api.Models;
using System.Collections.Generic;

namespace api.Interfaces
{
    public interface ITimeslotDataHandler
    {
         public List<Timeslot> SelectTimeslots(string id);
    }
}