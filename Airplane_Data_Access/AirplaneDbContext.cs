
using Airplane_Data_Access.Entities;
using Airplane_Data_Access.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airplane_Data_Access
{
    public class AirplaneDbContext : DbContext  
    {
        //Collections
        //Airplanes
        //Clients
        //Flights
        public AirplaneDbContext()
        {
            //this.Database.EnsureDeleted();      
            //this.Database.EnsureCreated();  
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-1LCG8OH\SQLEXPRESS;
                                                Initial Catalog=AirplaneDbPD_311;
                                                Integrated Security=True;
                                                Connect Timeout=3;Encrypt=False;
                                                Trust Server Certificate=False;
                                                Application Intent=ReadWrite;
                                                Multi Subnet Failover=False");

        
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API configuration
            modelBuilder.Entity<Airplane>()
                .Property(a => a.Model)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Client>().ToTable("Passangers");
            modelBuilder.Entity<Client>().Property(c=> c.Name)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("FirstName");

            modelBuilder.Entity<Client>().Property(c => c.Email)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Flight>().HasKey(f=>f.Number);  //primary key
            modelBuilder.Entity<Flight>()
                .Property(f => f.ArrivalCity)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Flight>()
               .Property(f => f.BoardingCity)
               .HasMaxLength(50)
               .IsRequired();
            //Navigation properties
            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Clients)
                .WithMany(c => c.Flights);

            modelBuilder.Entity<Flight>()
                .HasOne(f=> f.Airplane)
                .WithMany(a => a.Flights)
                .HasForeignKey(f=>f.AirplaneId);

            //Initialization - Seeder
            modelBuilder.SeedAirplanes();
            modelBuilder.SeedFlights();


        }
    }
}
