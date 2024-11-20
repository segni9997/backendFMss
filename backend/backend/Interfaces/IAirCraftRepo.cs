using backend.Models;

namespace backend.Interfaces
{
    public interface IAirCraftRepo
    {
        public ICollection<Aircraft> GetAirCrafts();
        Aircraft GetAircraft(int id);
        Aircraft GetAircraft(string name);
        bool getAircarftByNo(string AirNo);
        public ICollection<Aircraft> GetArrivedwell();
        public ICollection<Aircraft> GetArrivedPending();
        public ICollection<Aircraft> Getdepartured();
        bool AircraftExists(int id);
        bool CreateAircraft(Aircraft aircraft);
        bool UpdateAircraft(Aircraft aircraft);
        bool DeleteAircraft(Aircraft aircraft);

        bool Save();

    }
}
