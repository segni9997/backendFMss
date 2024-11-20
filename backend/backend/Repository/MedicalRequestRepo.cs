using backend.Data;
using backend.Interfaces;
using backend.Models;

namespace backend.Repository
{
    public class MedicalRequestRepo : IMedicalRequestRepo
    {
        private readonly DataContext _context;
        public MedicalRequestRepo(DataContext context)
        {
            
            _context = context;
        }
        public bool CreateMedrequest(MedicalRequest request)
        {
            _context.Add(request);

            return Save();
        }

        public ICollection<MedicalRequest> GetMedicalRequests()
        {
            return _context.MedicalRequests.OrderBy(a => a.id).ToList();
        }

        public MedicalRequest GetMedrequest(int id)
        {
            return _context.MedicalRequests.Where(air => air.id == id).FirstOrDefault();
        }

        public bool MedrequestExist(int id)
        {
            return _context.MedicalRequests.Any(p => p.id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMedReq(MedicalRequest request)
        {
            _context.Update(request);
            return Save();
        }
    }
}
