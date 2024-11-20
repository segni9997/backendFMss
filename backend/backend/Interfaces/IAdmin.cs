using backend.Models;

namespace backend.Interfaces
{
    public interface IAdmin
    {
        public ICollection<Admin> GetAdmins();
        Admin GetADmin(int id);
   
        bool AdminUserExist(int id);
        bool AdmintExist(int id);
        bool CreateAdmin(Admin admin);
        bool UpdateAdmin(Admin admin);
        bool DeleteAdmin(Admin admin);
        Admin GetAdminByEmail(string email);
        bool Save();
    }
}
