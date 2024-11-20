using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public Role Role { get; set; }
        //[DefaultValue("Null")]
        //public string? UserRole { get; set; } = "Null";
        public string UserEmail { get; set; }
        public string UserName { get; set; }
      
        public string Password { get; set; }
        [DefaultValue("None")]
        public string Group { get; set; } = "None";
    
        public DateTime Joinddate { get; set; }
        public DateTime LoggedInTime { get; set; }
        public DateTime LoggedOutTime { get; set; }
        //Relations
        /* public ICollection<Aircraft> Aircrafts { get; set; }
         public ICollection<Flight> Flights { get; set; }*/

    }
}