using System.ComponentModel;

namespace backend.Dto
{
    public class CabinCrewDto
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string? cabinLocation { get; set; } = "Home";
        [DefaultValue("No")]
        public string? medRequest { get; set; } = "No";

      

    }
}
