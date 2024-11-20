using backend.Data;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class AirCraftRepo : IAirCraftRepo
    {
        private readonly DataContext _context;

      
        public AirCraftRepo(DataContext context)
        {
           _context = context; 
            
        }

        public bool AircraftExists(int id)
        {
            return _context.Aircrafts.Any(p => p.id == id);
        }

        public bool CreateAircraft(Aircraft aircraft)
        {
            _context.Add(aircraft);

            return Save();

        }

        public bool DeleteAircraft(Aircraft aircraft)
        {
           _context.Remove(aircraft);
            return Save();  
        }

        public bool getAircarftByNo(string AirNo)
        {
            return _context.Aircrafts.Any( p => p.AircraftNo == AirNo);
        }

        public Aircraft GetAircraft(int id)
        {
            return _context.Aircrafts.Where(air => air.id == id).FirstOrDefault();
                

        }

        public Aircraft GetAircraft(string name)
        {
            return _context.Aircrafts.Where(air => air.AirCraftModel == name).FirstOrDefault();
        }

        /*  public Aircraft GetAircrafts()
 {
     return context.Aircrafts.OrderBy(p => p.id).FirstOrDefault();
 }*/

        public ICollection<Aircraft> GetAirCrafts()
        {
            return _context.Aircrafts.OrderBy(a => a.id).ToList();    //toList is to get multiple data 

        }

        public ICollection<Aircraft> GetArrivedPending()
        {
            return _context.Aircrafts.OrderBy(a => a.id).Where(a => a.AirCraftLocation == "Arrived" && a.Technicianresult == "Pending...").ToList();

        }

        public ICollection<Aircraft> GetArrivedwell()
        {
            return _context.Aircrafts.OrderBy(a => a.id).Where(a => a.AirCraftLocation == "Arrived" && a.Technicianresult == "Ready").ToList();
        }

        public ICollection<Aircraft> Getdepartured()
        {
            return _context.Aircrafts.OrderBy(a => a.id).Where(a => a.AirCraftLocation == "Departured" ).ToList();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAircraft(Aircraft aircraft)
        {
            _context.Update(aircraft);
            return Save();
        }
    }
}
