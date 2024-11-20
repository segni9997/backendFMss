using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class CoPilot
    {
        [Key]
        public int id { get; set; }
        public string FullName { get; set; }
        [DefaultValue("Home")]
        public string? copilotLocation { get; set; } = "Home";
        [DefaultValue("No")]
        public string? medRequest { get; set; } = "No";
        public User User { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public ICollection<Flight> Flights { get; set; }
     
    

    }
}
