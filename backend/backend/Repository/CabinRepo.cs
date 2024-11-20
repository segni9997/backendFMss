using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class CabinRepo : ICabinRepo
    {
        private readonly DataContext _context;
        public CabinRepo(DataContext context)
        {
            _context = context;
            
        }
        public bool CabinExist(int id)
        {
            return _context.CabinCrew.Any(p => p.id == id);
        }

        public bool cabinUserExist(int id)
        {
            return _context.CabinCrew.Any(p => p.User.id == id);
        }

        public bool CreateCabin(Cabincrew cabin)
        {
            _context.Add(cabin);
            return Save();
        }

        public bool DeleteCabin(Cabincrew cabin)
        {
            _context.Remove(cabin);
            return Save();
        }

        public Cabincrew GetCabin(int id)
        {
            return _context.CabinCrew.Where(air => air.id == id).Include(air => air.User).Include(air => air.User).Include(air => air.User.Role).FirstOrDefault();
        }

        public Cabincrew GetCabinByEmai(string emai)
        {
            return _context.CabinCrew.Where(air => air.User.UserEmail == emai).Include(air => air.User).Include(air => air.User).Include(air => air.User.Role).FirstOrDefault();

        }

        public ICollection<Cabincrew> GetCabinByEmail(string email)
        {
           return _context.CabinCrew.Where(t => t.User.UserEmail == email ).ToList();    
        }

        public ICollection<Flight> GetCabinByflight(int flightId)
        {
           throw new NotImplementedException();
        }

        public Cabincrew GetCabinbyUserId(int userId)
        {
            return _context.CabinCrew.Where(t => t.User.id == userId).FirstOrDefault();

        }

        public ICollection<Cabincrew> GetCabins()
        {
            return _context.CabinCrew.Include(air => air.User).Include(air => air.User).Include(air => air.User.Role).ToList();    //toList is to get multiple data 
        }

        public ICollection<Cabincrew> GetCabinsHome()
        {
            return _context.CabinCrew.Where(t => t.cabinLocation == "Home").Include(t => t.User).Include(air => air.User).Include(air => air.User.Role).ToList();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCabin(Cabincrew cabin)
        {
            _context.Update(cabin);
            return Save();
        }
    }
}
