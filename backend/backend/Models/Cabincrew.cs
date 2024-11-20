using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Cabincrew
    {

        [Key]
        public int id { get; set; }
        public string FullName { get; set; }
        [DefaultValue("Home")]
        public string? cabinLocation { get; set; } = "Home";
        [DefaultValue("No")]
        public string? medRequest { get; set; } = "No";
        [System.Text.Json.Serialization.JsonIgnore]
        public string? Cabingroup { get; set; }   
        public User User { get; set; }
       
        [System.Text.Json.Serialization.JsonIgnore]
       public ICollection<Flight> Flights { get; set; }

    }
}
