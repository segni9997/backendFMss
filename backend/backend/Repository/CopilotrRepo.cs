using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class CopilotrRepo : ICopilotRepo
    {
        private readonly DataContext _context;
        public CopilotrRepo(DataContext context)
        {
            _context = context;
            
        }
        public bool CopilotExist(int id)
        {
            return _context.CoPilot.Any(p => p.id == id);
        }

        public bool CopilotUserExist(int id)
        {
            return _context.CoPilot.Any(p => p.User.id == id);
        }

        public bool CreateCoPilot(CoPilot copilot)
        {
           // throw new NotImplementedException();
              _context.Add(copilot);

              return Save();

        }

        public bool DeleteCoPilot(CoPilot copilot)
        {
           _context.Remove(copilot);
            return Save();
          //  throw new NotImplementedException();

        }

        public CoPilot GetByEmail(string email)
        {
            return _context.CoPilot.FirstOrDefault(a => a.User.UserEmail == email);
        }

        public CoPilot GetCopilot(int id)
        {
            return _context.CoPilot.Where(air => air.id == id).Include(air => air.User).Include(a => a.User.Role).FirstOrDefault();

        }
        public ICollection<CoPilot> GetCopilots()
        {
            return _context.CoPilot.OrderBy(a => a.id).Include(a => a.User).Include(a =>a.User.Role).ToList();    //toList is to get multiple data 

        }

        public ICollection<CoPilot> getHomeCopilot()
        {
            return _context.CoPilot.OrderBy(a => a.id).Where(a => a.copilotLocation == "Home").ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCoPilot(CoPilot copilot)
        {
             _context.Update(copilot);
             return Save();
           // throw new NotImplementedException();

        }
            

    }
}
