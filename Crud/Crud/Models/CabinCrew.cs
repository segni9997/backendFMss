using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class CabinCrew
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
    
       public ICollection<Flight> Flights { get; set; }

    }
}
