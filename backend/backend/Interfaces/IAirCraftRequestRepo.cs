using backend.Models;

namespace backend.Interfaces
{
    public interface IAirCraftRequestRepo
    {
        public ICollection<AirCraftRequest> GetAirCraftRequests();
        AirCraftRequest GetAircraftRequest(int id);
       
        public ICollection<AirCraftRequest> GetPendingRequests();
        public ICollection<AirCraftRequest> GetAccepted();
      public ICollection<AirCraftRequest> Getdeclined();
         public ICollection<AirCraftRequest> GetAccptedArrived();
     
        bool AircraftRequestExists(int id);
        bool CreateAircraftRequest(AirCraftRequest aircraftRequest);
        bool UpdateAircraftRequest(AirCraftRequest aircraftRequest);
        bool DeleteAircraftRequest(AirCraftRequest aircraftRequest);

        bool Save();
    }
}
