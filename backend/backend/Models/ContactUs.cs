using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class ContactUs
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Message { get; set; }
        
    }
}
