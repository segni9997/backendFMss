using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class RoleRepo : IRole
    {
        private readonly DataContext _context;
        public RoleRepo(DataContext dataContext)
        {
            _context = dataContext;
        }
        public bool Createrole(Role role)
        {
            _context.Add(role);
            return Save();
        }

        public bool DeleteRole(Role role)
        {
            _context.Remove(role);
            return Save();
        }

        public Role Getrole(int id)
        {
            return _context.Roles.Where(air => air.id == id).FirstOrDefault();
        }

        public ICollection<Role> Getroles()
        {
            return _context.Roles.ToList();
        }

        public bool RoleExist(int id)
        {
            return _context.Roles.Any(p => p.id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRole(Role role)
        {
            _context.Update(role);
            return Save();
        }
    }
}
