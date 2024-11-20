using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{

    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
        public UserRepo( DataContext context)
        {
            _context = context;
            
        }
        public bool Userexist(int id)
        {
            return _context.Users.Any(p => p.id == id);
        }
        
        public bool CreateUser(User user)
        {
            _context.Add(user);

            return Save();

        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(air => air.id == id).Include(air => air.Role).FirstOrDefault();

        }

   
        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(a => a.id).Include(a => a.Role).ToList();    //toList is to get multiple data 

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public User GetUsernamePassword(string username, string password)
        {
            return _context.Users.Where(f => f.UserName == username && f.Password == password).FirstOrDefault();
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserexistPassword(string pass)
        {
            return _context.Users.Any(u => u.Password == pass);

        }

        public bool UserexistUser(string username)
        {
            return _context.Users.Any(u => u.UserName == username); 
        }

        public bool UserExist(string username)
        {
            return _context.Users.Any(p => p.UserName == username);
        }

        public bool UserEmail(string UserEmail)
        {
            return _context.Users.Any(p => p.UserEmail == UserEmail);

        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.UserEmail == email);
        }

        public User GetRoleByEMail(string email)
        {
            throw new NotImplementedException();
        }

        public Cabincrew GetCabinByUserEmail(string email)
        {
            return _context.CabinCrew.Where(u => u.User.UserEmail == email).FirstOrDefault();
        }

        public User ChangePassword(User user)
        {
            throw new NotImplementedException();
        }
    }
}
