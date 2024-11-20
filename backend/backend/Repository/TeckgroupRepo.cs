using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class TeckgroupRepo : ITeckGroup
    {
        private readonly DataContext _context;

        public TeckgroupRepo(DataContext dataContext)
        {
            _context = dataContext;
        }
        public bool TeckTeckExists(int id)
        {
            return _context.TeckGroups.Any(p => p.tid == id);

        }

        public bool CreateTeckGroup(TeckGroup group)
        {
            _context.Add(group);
            return Save();
        }

        public bool DeleteTeckGroup(TeckGroup group)
        {
            _context.Remove(group);
            return Save();
        }

        public ICollection<TeckGroup> GetGroups()
        {
            return _context.TeckGroups.ToList();

        }

        public TeckGroup GetTeckgroup(int id)
        {
            return _context.TeckGroups.Where(air => air.tid == id).FirstOrDefault();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateTeckGroup(TeckGroup group)
        {
            _context.Update(group);
            return Save();
        }

        public ICollection<TeckGroup> GetTeckhome()
        {
            return _context.TeckGroups.Where(a => a.location == 0).ToList();
        }

        public TeckGroup GetTeckbygroup(string group)
        {
            return _context.TeckGroups.Where(a => a.TGroup == group).FirstOrDefault();
        }
    }
}
