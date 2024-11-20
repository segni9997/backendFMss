using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class User
    {
        [Key]
        public int id {  get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }    
        public DateTime Joinddate { get; set; }
        public DateTime LoggedInTime { get; set; }
        public DateTime LoggedOutTime { get;set; }
        //Relations
       /* public ICollection<Aircraft> Aircrafts { get; set; }
        public ICollection<Flight> Flights { get; set; }*/



    }
}
