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
            dbContext.SaveChanges();

            foreach (var client in dbContext.Clients)
            {
                Console.WriteLine($"Name : {client.Name}  . Email {client.Email}");
            }

            var filteredFlight = dbContext.Flights.Where(f => f.ArrivalCity == "Lviv").OrderBy(f => f.BoardingTime);
            foreach (var flight in filteredFlight)
            {
                Console.WriteLine($"{flight.ArrivalCity} - {flight.BoardingCity}. {flight.BoardingTime}");
            }





        }
    }
}
