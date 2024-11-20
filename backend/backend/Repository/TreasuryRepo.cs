using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class TreasuryRepo : ITreasury
        
    {
        private readonly DataContext _context;
        public TreasuryRepo(DataContext dataContext)
        {
            _context = dataContext;
            
        }
        public bool CreateTreasury(Tresury treasury)
        {
            _context.Add(treasury);

            return Save();
        }

        public bool DeleteTreasury(Tresury tresury)
        {
            _context.Remove(tresury);
            return Save();
        }

        public ICollection<Tresury> GetTreasuries()
        {
            return _context.Treasuries.OrderBy(a => a.id).Include(a => a.User).ToList();
        }

        public Tresury GetTreasury(int id)
        {
            return _context.Treasuries.Where(air => air.id == id).Include(d => d.User).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TreasryUserExist(int id)
        {
            return _context.Treasuries.Any(p => p.User.id == id);
        }

        public bool TreasurytExist(int id)
        {
            return _context.Treasuries.Any(p => p.id == id);
        }

        public bool UpdateTreasury(Tresury tresury)
        {
            _context.Update(tresury);
            return Save();
        }
    }
}
