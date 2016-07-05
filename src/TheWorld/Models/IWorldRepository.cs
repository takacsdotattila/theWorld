using System.Collections.Generic;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
        IEnumerable<Trip> GetAllTripsWithStops(string name);
        Trip GetTripByName(string tripName, string name);
        void AddStop(string tripName, string name, Stop newStop);
    }
}