using System.ComponentModel;

namespace backend.Dto
{
    public class TechncianDto
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string? TechnicianLocation { get; set; } = "Home";

        [DefaultValue("No")]
        public string? medRequest { get; set; } = "No";


    }
}
