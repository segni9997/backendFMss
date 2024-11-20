using backend.Models;

namespace backend.Interfaces
{
    public interface ICopilotRepo
    {
        public ICollection<CoPilot> GetCopilots();
        CoPilot GetCopilot(int id);
        CoPilot GetByEmail(string email);
        ICollection<CoPilot> getHomeCopilot();
        bool CopilotUserExist(int id);  
        bool CopilotExist(int id);
        bool CreateCoPilot(CoPilot copilot);
        bool UpdateCoPilot(CoPilot copilot);
        bool DeleteCoPilot(CoPilot copilot);
        bool Save();
    }
}
