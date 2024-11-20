using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class TeckGroup
    {
        [Key]
       public  int tid { get; set; }    
        public string TGroup { get; set; }
        public int location { get; set; }
         
    }
}
