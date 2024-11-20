
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Flight
    {
        [Key]
        public int id { get; set; }

        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        [DefaultValue("Home")]
        public string LocationStatus { get; set; } = "Home";
        public Pilot Pilot { get; set; }
        public CoPilot CoPilot { get; set; }

        public string? cabingroup { get; set; }
        public string? technicainGroup { get; set; }

        public Aircraft Aircraft { get; set; }

    }
}
