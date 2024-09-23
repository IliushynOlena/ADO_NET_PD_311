using _03_IntroToEntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_IntroToEntityFramework
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
            modelBuilder.Entity<Airplane>().HasData(
                new Airplane[]
                {
                    new Airplane { Id = 1,Model = "Boeing 747", MaxPassanger = 300 },
                    new Airplane { Id = 2,Model = "Mria", MaxPassanger = 200 },
                    new Airplane { Id = 3,Model = "AN 225", MaxPassanger = 100 }
                });
            modelBuilder.Entity<Flight>().HasData(
                new Flight[]
                {
                    new Flight()
                    {
                         Number = 1,
                         ArrivalCity = "Lviv",
                         BoardingCity = "Rivne",
                         ArrivalTime = new DateTime(2024,9,25),
                         BoardingTime = new DateTime(2024,9,25),
                         AirplaneId = 1
                    },
                    new Flight()
                    {
                         Number = 2,
                          ArrivalCity = "Lviv",
                          BoardingCity = "Kyiv",
                           ArrivalTime = new DateTime(2024,9,25),
                           BoardingTime = new DateTime(2024,9,25),
                            AirplaneId = 2
                    },
                    new Flight()
                    {
                         Number = 3,
                         ArrivalCity = "Lviv",
                         BoardingCity = "Warshav",
                         ArrivalTime = new DateTime(2024,9,25),
                         BoardingTime = new DateTime(2024,9,25),
                         AirplaneId = 3
                          
                    }
                });
        }
    }
}
