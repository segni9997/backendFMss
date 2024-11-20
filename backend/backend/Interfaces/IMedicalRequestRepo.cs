using backend.Models;

namespace backend.Interfaces
{
    public interface IMedicalRequestRepo
    {
        ICollection<MedicalRequest> GetMedicalRequests();
        MedicalRequest GetMedrequest(int id);
        bool MedrequestExist(int  id);
        bool CreateMedrequest(MedicalRequest request);
        bool UpdateMedReq(MedicalRequest request);
        bool Save();
    }
}
