using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class AirCraftRequestRepo : IAirCraftRequestRepo
    {
        private readonly DataContext _context;


        public AirCraftRequestRepo(DataContext context)
        {
            _context = context;

        }

        public bool AircraftRequestExists(int id)
        {
            return _context.AirCraftRequest.Any(p => p.id == id);
        }

        public bool CreateAircraftRequest(AirCraftRequest aircraftRequest)
        {
            _context.Add(aircraftRequest);

            return Save();

        }

        public bool DeleteAircraftRequest(AirCraftRequest aircraftRequest)
        {
            _context.Remove(aircraftRequest);
            return Save();
        }

        public ICollection<AirCraftRequest> GetAccepted()
        {
            return _context.AirCraftRequest.OrderBy(a => a.id).Where(a => a.ReqStatus == "Accepted" && a.Added > 0 && a.ArrivalSatus == "Soon").ToList();
        }

        public ICollection<AirCraftRequest> GetAccptedArrived()
        {
            return _context.AirCraftRequest.OrderBy(a => a.id).Where(a => a.ArrivalSatus == "Arrived" && a.Added > 0).ToList();

        }

        public AirCraftRequest GetAircraftRequest(int id)
        {
            return _context.AirCraftRequest.Where(air => air.id == id).FirstOrDefault();

        }

      /*  public Aircraft GetAircraft(string name)
        {
            return _context.Aircrafts.Where(air => air.AirCraftModel == name).FirstOrDefault();
        }*/

        /*  public Aircraft GetAircrafts()
 {
     return context.Aircrafts.OrderBy(p => p.id).FirstOrDefault();
 }*/

        public ICollection<AirCraftRequest> GetAirCraftRequests()
        {
            return _context.AirCraftRequest.OrderBy(a => a.id).ToList();    //toList is to get multiple data 

        }

        public ICollection<AirCraftRequest> Getdeclined()
        {
            return _context.AirCraftRequest.OrderBy(a => a.id).Where(a => a.ReqStatus == "Declined").ToList();

        }

        public ICollection<AirCraftRequest> GetPendingRequests()
        {
            return _context.AirCraftRequest.OrderBy(a => a.id).Where(a => a.ReqStatus == "Pending").ToList();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAircraftRequest(AirCraftRequest aircraftRequest)
        {
            _context.Update(aircraftRequest);
            return Save();
        }
    }
}
