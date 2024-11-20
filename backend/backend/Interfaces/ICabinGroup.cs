using backend.Models;

namespace backend.Interfaces
{
    public interface ICabinGroup
    {
        public ICollection<Cabingroup> GetGroups();
        Cabingroup GetCabgroup(int id);
        Cabingroup GetByGroup(string group);    
        bool CabinGroupExists(int id);  
       public ICollection< Cabingroup> GetCabinHome();
        bool CreateCabinGroup(Cabingroup group);   
        bool DeleteCabinGroup(Cabingroup group);
        bool UpdateCabinGroup(Cabingroup group);
        bool Save();

    }
}
