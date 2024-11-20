using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Cabingroup
    {
        [Key]
        public int cId { get; set; }
        public string CGroup { get; set; }
        public int location { get; set; }
    }
}
