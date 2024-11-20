using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Tresury
    {
        [Key]

        public int id { get; set; }
        public string FullName { get; set; }



        public User User { get; set; }
    }
}
