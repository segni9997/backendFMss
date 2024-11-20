using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class AdminRepo : IAdmin
    {
        private readonly DataContext _context;
        public AdminRepo( DataContext Context)
        {
            _context = Context;
            
        }
        public bool AdmintExist(int id)
        {
            return _context.Admins.Any(p => p.id == id);
        }

        public bool AdminUserExist(int id)
        {
            return _context.Admins.Any(p => p.User.id == id);
        }

        public bool CreateAdmin(Admin admin)
        {
            _context.Add(admin);

            return Save();
        }

        public bool DeleteAdmin(Admin admin)
        {
            _context.Remove(admin);
            return Save();

        }

        public Admin GetADmin(int id)
        {
            return _context.Admins.Where(air => air.id == id).Include(d => d.User).Include(a => a.User.Role).FirstOrDefault();
        }
        
        public Admin GetAdminByEmail(string email)
        {
            return _context.Admins.Where(air => air.User.UserEmail == email).Include(a => a.User).Include(a => a.User.Role).FirstOrDefault();
        }

        public ICollection<Admin> GetAdmins()
        {
            return _context.Admins.OrderBy(a => a.id).Include(a => a.User).Include(a => a.User.Role).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAdmin(Admin admin)
        {
            _context.Update(admin);
            return Save();
        }
    }
}
