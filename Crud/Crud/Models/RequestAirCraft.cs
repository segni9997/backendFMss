using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class RequestAirCraft
    {
        [Key]
        public int id {  get; set; }
        public string AircraftName { get; set; }
        public int Capaciy { get; set; }
        public int AmountNeeded { get; set; }
        public DateTime  ReqDate { get; set; }
       /* public User UserID { get; set; }*/
    }
}
