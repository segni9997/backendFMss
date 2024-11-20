using backend.Models;

namespace backend.Interfaces
{
    public interface ITeckGroup
    {
        public ICollection<TeckGroup> GetGroups();
        TeckGroup GetTeckgroup(int id);
        TeckGroup GetTeckbygroup(string group);
        public ICollection<TeckGroup> GetTeckhome();  
        bool TeckTeckExists(int id);
        bool CreateTeckGroup(TeckGroup group);
        bool DeleteTeckGroup(TeckGroup group);
        bool UpdateTeckGroup(TeckGroup group);
        bool Save();
    }
}
