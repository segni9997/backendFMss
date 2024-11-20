using System.ComponentModel;

namespace backend.Dto
{
    public class AirCraftDto
    {
        public int id { get; set; }
        public string AirCraftName { get; set; }
        public string AirCraftModel { get; set; }
        public string AircraftNo { get; set; }
        public int AircrftCapacity { get; set; }
        public DateTime AddedDate { get; set; }
        [DefaultValue("Arrived")]
        public string? AirCraftLocation { get; set; } = "Arrived";
        [DefaultValue("Pending...")]
        public string? Technicianresult { get; set; } = "Pending...";
    }
}
