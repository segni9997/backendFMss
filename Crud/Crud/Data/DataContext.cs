using Crud.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) 
        {
            
        }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Contactus> Contactus { get; set; } 
        public DbSet<Flight> Flights { get; set; }
        public DbSet<MedicalRequest> MedicalRequests { get; set; }
        public DbSet<RequestAirCraft> RequestAirCraft { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Crud.Models.CabinCrew>? CabinCrew { get; set; }
        public DbSet<Crud.Models.Technician>? Technician { get; set; }
        public DbSet<Crud.Models.CoPilot>? CoPilot { get; set; }
        public DbSet<Crud.Models.Pilot>? Pilot { get; set; }
    }
}
