using System.ComponentModel;

namespace backend.Dto
{
    public class PilotDto
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string? pilotLocation { get; set; } = "Home";
        [DefaultValue("No")]
        public string? medRequest { get; set; } = "No";
    }
}
