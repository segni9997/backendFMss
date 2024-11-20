using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class MedicalRequest
    {
        [Key]
        public int id { get; set; }
        public string FullName { get; set; }
        public string Reason { get; set; }
      /*  public ICollection<User> UserId { get; set; }*/


    }
}
