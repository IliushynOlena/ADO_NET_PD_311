using Airplane_Data_Access;
using Airplane_Data_Access.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _03_IntroToEntityFramework
{



    internal class Program
    {
        static void Main(string[] args)
        {

            AirplaneDbContext dbContext = new AirplaneDbContext();
            dbContext.Clients.Add(new Client
            {
                Name = "Volodia",
                Birhdate = new DateTime(2000, 12, 15),
                Email = "vovan@gmail.com"
            });
            //dbContext.SaveChanges();

            //foreach (var client in dbContext.Clients)
            //{
            //    Console.WriteLine($"Name : {client.Name}  . Email {client.Email}");
            //}

            //Include(relation data) = JOIN

            var filteredFlight = dbContext.Flights
                .Include(f=>f.Airplane)//Flights join Airplane
                .Include(f=>f.Clients)//Flights join Clients
                .Where(f => f.ArrivalCity == "Lviv")
                .OrderBy(f => f.BoardingTime);
            foreach (var flight in filteredFlight)
            {
                Console.WriteLine($"{flight.ArrivalCity} - " +
                    $"{flight.BoardingCity}. " +
                    $"{flight.BoardingTime}.\n" +
                    $"Airplane Id : {flight.AirplaneId}  ." +
                    $"Airplane Model : {flight.Airplane?.Model}" +
                    $"\nCount passangers : {flight.Clients?.Count}");
            }


            var client = dbContext.Clients.Find(1);
            //Explicit loading data : context.Entry(entity).Collection/References.Load();
            dbContext.Entry(client).Collection(c=>c.Flights).Load();
            Console.WriteLine($"{client.Name}. Count Flight : {client.Flights.Count}");

            foreach (var item in client.Flights)
            {
                Console.WriteLine(item.ArrivalCity);
            }
           



        }
    }
}
