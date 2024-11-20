using backend.Data;
using backend.Interfaces;
using backend.Models;

namespace backend.Repository
{
    public class ContactusRepo : IContactUs
    {
        private readonly DataContext _context;
        public ContactusRepo(DataContext context)
        {
            _context = context;
        }

        public bool CreateContact(ContactUs contact)
        {
            _context.Add(contact);
            return Save();
            
        }

        public ICollection<ContactUs> GetContacts()
        {
            return _context.Contactus.OrderByDescending(a => a.id).ToList();    //toList is to get multiple data 

        }
        /*
        public ContactUs GetReviewedContact()
        {
            return _context.Contactus.Where(rev => rev.Review == "Review").FirstOrDefault();
        }
        */
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
