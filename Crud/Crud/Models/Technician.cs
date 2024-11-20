using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class Technician
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

  
       public  ICollection<Flight> Flights { get; set; }    

    }
}
