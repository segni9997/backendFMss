
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class Contactus
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Message { get; set; }

    }


}
