using System.ComponentModel;

namespace backend.Dto
{
    public class CoPilotDto
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string? copilotLocation { get; set; } = "Home";
        [DefaultValue("No")]
        public string? medRequest { get; set; } = "No";
    }
}
