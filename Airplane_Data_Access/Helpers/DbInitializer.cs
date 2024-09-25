
using Airplane_Data_Access.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Airplane_Data_Access.Helpers
{
    public static class DbInitializer
    {
        public static void SeedAirplanes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airplane>().HasData(
              new Airplane[]
              {
                    new Airplane { Id = 1,Model = "Boeing 747", MaxPassanger = 300 },
                    new Airplane { Id = 2,Model = "Mria", MaxPassanger = 200 },
                    new Airplane { Id = 3,Model = "AN 225", MaxPassanger = 100 }
              });
        }
        public static void SeedFlights(this ModelBuilder modelBuilder)
        {
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
