using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class Pilot
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

       
      

    }
}
