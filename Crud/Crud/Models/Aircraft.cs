using Crud.Data;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class Aircraft
    {
        [Key]
        public  int id { get; set; }
        public string AirCraftName { get; set; }
        public string AirCraftModel { get; set; }
        public string AircraftNo { get; set; }
        public int AircrftCapacity { get; set; }
        public DateTime AddedDate { get; set; }


    }
}

