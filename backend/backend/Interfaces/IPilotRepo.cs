using backend.Models;

namespace backend.Interfaces
{
    public interface IPilotRepo
    {
        public ICollection<Pilot> GetPilots();
        Pilot GetPilot(int id);
        ICollection<Pilot> GetHomePilot();
        ICollection<Pilot> GetApartPilot();
        Pilot GetByemail(string email);
        bool pilotUserExist(int id);    
        bool PilotExist(int id);
        bool CreatePilot(Pilot pilot);
        bool UpdatePilot(Pilot pilot);
        bool DeletePilot(Pilot pilot);
        bool Save();
    }
}
