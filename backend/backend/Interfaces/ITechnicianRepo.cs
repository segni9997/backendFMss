using backend.Models;

namespace backend.Interfaces
{
    public interface ITechnicianRepo
    {
          ICollection<Technician> GetTechnicians();
        ICollection<Technician> GetTEchncianByGroup(string group);

        Technician GetTechnician(int id);
        Technician GetTechncainByEmail(string email);
        bool TechncianUserExist(int id);    
        bool TechnchianExist(int id);

        bool CreateTechnician (Technician technician);
        bool DeleteTechnician(Technician technician);
        public ICollection<Technician> getHomeTEchnicain();

        bool Save();
        
    }
}
