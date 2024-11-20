using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class TechncianRepo : ITechnicianRepo
    {
        private readonly DataContext _context;
        public TechncianRepo(DataContext context)
        {
            _context = context;
            
        }

        public bool CreateTechnician(Technician technician)
        {
            _context.Add(technician);
            return Save();

        }

        public bool DeleteTechnician(Technician technician)
        {
            _context.Remove(technician);
            return Save();
        }

        public ICollection<Technician> getHomeTEchnicain()
        {
            return _context.Technician.Where(t => t.TechnicianLocation == "Home").Include(air => air.User).Include(air => air.User.Role).ToList();

        }

        public Technician GetTechncainByEmail(string email)
        {
            return _context.Technician.Where(air => air.User.UserEmail == email).Include(air => air.User).Include(air => air.User).Include(air => air.User.Role).FirstOrDefault();

        }

        public ICollection<Technician> GetTEchncianByGroup(string group)
        {
            throw new NotImplementedException();
        }

        public Technician GetTechnician(int id)
        {
            return _context.Technician.Where(t => t.id == id).Include(air => air.User).Include(air => air.User.Role).FirstOrDefault();
        }

        public ICollection<Technician> GetTechnicians()
        {
           return _context.Technician.Include(t => t.User).Include(air => air.User.Role).ToList(); 
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;


        }
        public bool TechnchianExist(int id)
        {
            return _context.Technician.Any(t => t.id == id);
    
        }

        public bool TechncianUserExist(int id)
        {
            return _context.Technician.Any(p => p.User.id == id);
        }
    }

       
    }
