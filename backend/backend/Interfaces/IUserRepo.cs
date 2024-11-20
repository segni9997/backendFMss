using backend.Models;

namespace backend.Interfaces
{
    public interface IUserRepo
    {
        public ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUsernamePassword(string username, string password);
        User GetByEmail(string email);
        User GetRoleByEMail(string email);
        Cabincrew GetCabinByUserEmail(string email);
        User ChangePassword(User user);
        bool Userexist(int id);
        bool UserExist(string username);
        bool UserEmail(string UserEmail);
        bool UserexistPassword(string id);
        bool UserexistUser(string id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);




    }
}
