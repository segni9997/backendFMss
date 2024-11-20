using backend.Data;
using backend.Interfaces;
using backend.Models;
using System.Data;

namespace backend.Repository
{
    public class CabingroupRepo : ICabinGroup
    {
        private readonly DataContext _context;

        public CabingroupRepo(DataContext dataContext)
        {
            _context = dataContext;
        }
        public bool CabinGroupExists(int id)
        {
            return _context.Cabingroups.Any(p => p.cId == id);
        }

        public bool CreateCabinGroup(Cabingroup group)
        {
            _context.Add(group);
            return Save();
        }

        public bool DeleteCabinGroup(Cabingroup group)
        {
            _context.Remove(group);
            return Save();
        }

        public Cabingroup GetByGroup(string group)
        {
            return _context.Cabingroups.Where(a => a.CGroup == group).FirstOrDefault();

        }

        public Cabingroup GetCabgroup(int id)
        {
            return _context.Cabingroups.Where(air => air.cId == id).FirstOrDefault();

        }

        public ICollection<Cabingroup> GetCabinHome()
        {
            return _context.Cabingroups.Where(a => a.location == 0).ToList();  
        }

        public ICollection<Cabingroup> GetGroups()
        {
            return _context.Cabingroups.ToList();
            
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCabinGroup(Cabingroup group)
        {
            _context.Update(group);
            return Save();
        }
    }
}
