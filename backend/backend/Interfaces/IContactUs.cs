using backend.Models;

namespace backend.Interfaces
{
    public interface IContactUs
    {
        ICollection<ContactUs> GetContacts();
     
        bool CreateContact (ContactUs contact);
     
        bool Save();

    }
}
