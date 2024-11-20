using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class PilotRepo : IPilotRepo
    {
        private readonly DataContext _context;
        public PilotRepo(DataContext context)
        {
            _context = context;
            
        }

        public bool CreatePilot(Pilot pilot)
        {
            _context.Add(pilot);

            return Save();
        }
       
        public bool DeletePilot(Pilot pilot)
        {
            _context.Remove(pilot);
            return Save();
        }

        public ICollection<Pilot> GetApartPilot()
        {
           return _context.Pilot.OrderBy(a => a.id).Where(a => a.pilotLocation == "Depart").ToList(); 
        }

        public Pilot GetByemail(string email)
        {
          return _context.Pilot.FirstOrDefault(a => a.User.UserEmail == email);
        }

        public ICollection<Flight> GetflightFromPilot(int pilotId)
        {
            return _context.Flights.Where(f=> f.Pilot.id == pilotId).ToList();
        }

        public ICollection<Pilot> GetHomePilot()
        {
            return _context.Pilot.OrderBy(a => a.id).Where(a => a.pilotLocation == "Home").ToList();

        }

        public Pilot GetPilot(int id)
        {
            return _context.Pilot.Where(air => air.id == id).Include(d => d.User).Include(a => a.User.Role).FirstOrDefault();
        }

        public ICollection<Pilot> GetPilots()
        {
            return _context.Pilot.OrderBy(a => a.id).Include(a =>a.User).Include(a => a.User.Role).ToList();
        }

        public bool PilotExist(int id)
        {

            return _context.Pilot.Any(p => p.id == id);
        }

        public bool pilotUserExist(int id)
        {
            return _context.Pilot.Any(p => p.User.id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePilot(Pilot pilot)
        {
            _context.Update(pilot);
            return Save();
        }
    }
}
