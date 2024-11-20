using System.ComponentModel.DataAnnotations;
using Crud.Data;
using Microsoft.EntityFrameworkCore;

namespace Crud.Models
{
    public class Flight

    {
        [Key]
        public int id { get; set; }
       
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public Pilot? Pilot { get; set; }
        public CoPilot? CoPilot { get; set; }
        public ICollection<CabinCrew>? CabinCrews { get; set; } 
        public ICollection<Technician>? Technicians { get; set; }   
        public Aircraft?  Aircraft { get; set; }


    }
}
