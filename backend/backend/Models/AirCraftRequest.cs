using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class AirCraftRequest
    {
        [Key]
        public int id { get; set; }
        public string AircraftName { get; set; }
        public string Model { get; set; }
        public int Capaciy { get; set; }
        public int AmountNeeded { get; set; }
        public int Added { get; set; }
        public DateTime ReqDate { get; set; }
        [DefaultValue("Pending")]
        public string ReqStatus { get; set; } = "Pending";
        [DefaultValue("Not-Arrived")]
        public string ArrivalSatus { get; set; } = "Not-Arrived";
        public DateTime? EstimatedArrivalDate { get; set; } 
        /* public User UserID { get; set; }*/
    }
}
