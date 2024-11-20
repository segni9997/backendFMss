using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class FlightRepo : IFlightRepo
    {
        private readonly DataContext _context;
        public FlightRepo(DataContext context)
        {
          
            _context = context;
            
        }
       public  Flight Flight (int id)
        {
            return _context.Flights.Where(air => air.id == id ).Include(air => air.Pilot).Include(air => air.CoPilot).Include(air => air.Aircraft).FirstOrDefault();

        }
        public bool DeleteFlight(int id)
        {
            _context.Remove(Flight);
            return Save();
        }

        public bool FlightExists(int id)
        {
            return _context.Flights.Any(p => p.id == id);
        }

        public ICollection<Flight> GetFlights()
        {
            return _context.Flights.Include(air => air.Pilot).Include(air => air.CoPilot).Include(air => air.Aircraft).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFlight(Flight flight)
        {
            _context.Update(flight);
            return Save();
        }

        public bool CreateFlight( Flight flight)
        {
            _context.Add(flight);
       
            
            
            return Save();



        }

        public ICollection<Flight> getflighthome()
        {
            return _context.Flights.Where(a => a.LocationStatus == "Home")
                                   .Include(a => a.Pilot)
                                   .Include(a => a.CoPilot)
                                   .Include(a => a.Aircraft)
                                   .ToList();
        }

        public ICollection<Flight> getFlightdeparted()
        {
            return _context.Flights.Include(a => a.Pilot) . Include(a => a.CoPilot).Include(a => a.Aircraft).Where(a => a.LocationStatus == "Departed").ToList();

        }

        public ICollection<Flight> getFlightArrived()
        {
            return _context.Flights.Include(a => a.Pilot).Include(a => a.CoPilot).Include(a => a.Aircraft).Where(a => a.LocationStatus == "Arrived").ToList();

        }

        public ICollection<Flight> GEtFlightByPilotID(int id)
        {
            return _context.Flights.Where(a => a.Pilot.id == id && a.LocationStatus == "Home").Include(a => a.CoPilot).Include(a => a.Pilot).Include(a => a.Aircraft).ToList();
        }
        public ICollection<Flight> GetFlightByPilotIDDeparted(int id)
        {
            return _context.Flights.Where(a => a.Pilot.id == id && a.LocationStatus == "Departed").Include(a => a.CoPilot).Include(a => a.Aircraft).Include(a =>a.Pilot).ToList();
        }

        public ICollection<Flight> GetFlightByCabinGroup(string group)
        {
            return _context.Flights.Where(a => a.cabingroup == group && a.LocationStatus == "Home").Include(a => a.Pilot).Include(a => a.CoPilot).Include(a => a.Aircraft).ToList();
        }
        public ICollection<Flight> GetFlightByCabinGroupD(string group)
        {
            return _context.Flights.Where(a => a.cabingroup == group && a.LocationStatus == "Departed").Include(a => a.Pilot).Include(a => a.CoPilot).Include(a => a.Aircraft).OrderByDescending(a => a.DepartureDateTime).ToList();
        }

        public ICollection<Flight> GetFlightbyTechnicanGroup(string group)
        {
           return _context.Flights.Where(a => a.technicainGroup == group && a.LocationStatus == "Home").Include(a => a.Pilot).Include(a => a.CoPilot).Include(a => a.Aircraft). ToList();    
        }

        public bool DeleteFlight(Flight flight)
        {
            _context.Remove(flight);
            return Save();
        }

        public ICollection<Flight> GetFlightbyTechnicanGroupD(string group)
        {
            return _context.Flights.Where(a => a.cabingroup == group && a.LocationStatus == "Departed").Include(a => a.Pilot).Include(a => a.CoPilot).Include(a => a.Aircraft).ToList();

        }

        public ICollection<Flight> GetcabinDeparted(string group)
        {
            throw new NotImplementedException();
        }

        public ICollection<Flight> GetcabinArrived(string group)
        {
            return _context.Flights.Where(a => a.cabingroup == group && a.LocationStatus == "Arrived").Include(a => a.Pilot).Include(a => a.CoPilot).Include(a => a.Aircraft).ToList();

        }

        public ICollection<Flight> GettechncianArrived(string group)
        {
            return _context.Flights.Where(a => a.technicainGroup == group && a.LocationStatus == "Arrived").Include(a => a.Pilot).Include(a => a.CoPilot).Include(a => a.Aircraft).ToList();

        }

        public ICollection<Flight> GettechncianDeparted(string group)
        {
            return _context.Flights.Where(a => a.technicainGroup == group && a.LocationStatus == "Departed").Include(a => a.Pilot).Include(a => a.CoPilot).Include(a => a.Aircraft).ToList();

        }

        public ICollection<Flight> GetFlightByPilotIArrived(int id)
        {
            return _context.Flights.Where(a => a.Pilot.id == id && a.LocationStatus == "Arrived").Include(a => a.CoPilot).Include(a => a.Aircraft).ToList();

        }

        public ICollection<Flight> GEtFlightByCoPilotID(int id)
        {
            return _context.Flights.Where(a => a.CoPilot.id == id && a.LocationStatus == "Home").Include(a => a.CoPilot).Include(a => a.Pilot).Include(a => a.Aircraft).ToList();
            throw new NotImplementedException();
        }

        public ICollection<Flight> GetFlightByCoPilotIDDeparted(int id)
        {
            return _context.Flights.Where(a => a.CoPilot.id == id && a.LocationStatus == "Departed").Include(a => a.CoPilot).Include(a => a.Pilot).Include(a => a.Aircraft).ToList();

        }

        public ICollection<Flight> GetFlightByCoPilotIArrived(int id)
        {
            return _context.Flights.Where(a => a.CoPilot.id == id && a.LocationStatus == "Arrived").Include(a => a.CoPilot).Include(a => a.Pilot).Include(a => a.Aircraft).ToList();

        }

        public ICollection<Flight> GEtFlightByAirID(int id)
        {
          return _context.Flights.Where(a => a.Aircraft.id == id).ToList();
        }
    }
}
