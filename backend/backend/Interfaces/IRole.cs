using backend.Models;

namespace backend.Interfaces
{
    public interface IRole
    {
       public ICollection<Role> Getroles();
        Role Getrole(int id);
        bool RoleExist(int id); 
        bool Createrole(Role role); 
        bool DeleteRole(Role role);
        bool UpdateRole(Role role);
        
        bool Save();

    }
}
