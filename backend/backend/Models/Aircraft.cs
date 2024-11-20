using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Aircraft
    {
        [Key]
        public int id { get; set; }
        public string  AirCraftName { get; set; }
        public string  AirCraftModel { get; set; }
        public string AircraftNo { get; set; } 
        public int AircrftCapacity { get; set; }
        public DateTime AddedDate { get; set; }
        [DefaultValue("Arrived")]
        public string? AirCraftLocation { get; set; } = "Arrived";
        [DefaultValue("Pending...")]
        public string? Technicianresult { get; set; } = "Pending...";
        [System.Text.Json.Serialization.JsonIgnore]

        public ICollection<Flight> Flights { get; set; }   
    }
}
