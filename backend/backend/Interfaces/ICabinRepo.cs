
using backend.Dto;
using backend.Models;

namespace backend.Interfaces
{
    public interface ICabinRepo
    {


        public ICollection<Cabincrew> GetCabins();
        public ICollection<Cabincrew> GetCabinsHome();
        Cabincrew GetCabinbyUserId(int userId);
        Cabincrew GetCabin(int id);
        Cabincrew GetCabinByEmai (string  emai);
        ICollection<Flight> GetCabinByflight(int flightId);
         bool cabinUserExist(int id);
        bool CabinExist(int id);
        bool CreateCabin(Cabincrew cabin);
        bool UpdateCabin(Cabincrew cabin);
        bool DeleteCabin(Cabincrew cabin);
        bool Save();
    }
}
