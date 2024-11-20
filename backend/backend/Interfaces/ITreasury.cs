using backend.Models;

namespace backend.Interfaces
{
    public interface ITreasury
    {
        public ICollection<Tresury> GetTreasuries();
        Tresury GetTreasury(int id);

        bool TreasryUserExist(int id);
        bool TreasurytExist(int id);
        bool CreateTreasury(Tresury treasury);
        bool UpdateTreasury(Tresury tresury);
        bool DeleteTreasury(Tresury tresury);
        bool Save();
    }
}
