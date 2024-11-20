using backend.Dto;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace backend.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<ContactUs> Contactus { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<MedicalRequest> MedicalRequests { get; set; }
        public DbSet<AirCraftRequest> AirCraftRequest { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cabincrew> CabinCrew { get; set; }
        public DbSet<Technician> Technician { get; set; }
        public DbSet<CoPilot> CoPilot { get; set; }
        public DbSet<Pilot> Pilot { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Tresury> Treasuries { get; set; }  
        public DbSet<Role> Roles { get; set; }  
        public DbSet<Cabingroup> Cabingroups { get; set; }
        public DbSet<TeckGroup> TeckGroups { get; set; }
       
        /*    protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Cabincrew>()
                    .HasMany(e => e.Flights)
                    .WithOne(e => e.Cabincrew)
                    .HasForeignKey(e => e.cabinId)
                    .IsRequired(false);
                modelBuilder.Entity<Technician>()
                .HasMany(e => e.Flights)
                .WithOne(e => e.Technician)
                .HasForeignKey(e => e.teckID)
                .IsRequired(false);
                modelBuilder.Entity<CoPilot>()
                .HasMany(e => e.Flights)
                .WithOne(e => e.CoPilot)
                .HasForeignKey(e => e.CoPilotId)
                .IsRequired(false);
                modelBuilder.Entity<Pilot>()
                .HasMany(e => e.Flights)
                .WithOne(e => e.Pilot)
                .HasForeignKey(e => e.pilotId)
                .IsRequired(false);
                modelBuilder.Entity<Aircraft>()
                .HasMany(e => e.Flights)
                .WithOne(e => e.Aircraft)
                .HasForeignKey(e => e.AircraftID)
                .IsRequired(false);
            }*/
     /*   protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasOne<Cabincrew>(n => n.Cabincrew)
                .WithMany(s => s.Flights)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Cabincrew>().ToTable("CabinCrew");
            modelBuilder.Entity<Flight>().ToTable("Flight");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>().HasOne<Technician>(n => n.Technician)
           .WithMany(s => s.Flights)
           .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Technician>().ToTable("Technician");
            modelBuilder.Entity<Flight>().ToTable("Flight");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>().HasOne<CoPilot>(n => n.CoPilot)
           .WithMany(s => s.Flights)
           .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CoPilot>().ToTable("CoPilot");
            modelBuilder.Entity<Flight>().ToTable("Flight");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>().HasOne<Aircraft>(n => n.Aircraft)
           .WithMany(s => s.Flights)
           .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Aircraft>().ToTable("AirCraft");
            modelBuilder.Entity<Flight>().ToTable("Flight");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>().HasOne<Pilot>(n => n.Pilot)
           .WithMany(s => s.Flights)
           .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Pilot>().ToTable("Pilot");
            modelBuilder.Entity<Flight>().ToTable("Flight");
            base.OnModelCreating(modelBuilder);
        }*/
    }
    
}
