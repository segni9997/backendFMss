using System.ComponentModel;

namespace backend.Dto
{
    public class FlightDto
    {
        public int id { get; set; }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        [DefaultValue("Home")]
        public string LocationStatus { get; set; } = "Home";
        public string? cabingroup { get; set; }
        public string? technicainGroup { get; set; }
    }
}
