using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _03_IntroToEntityFramework.Entities
{
    public class Airplane
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int MaxPassanger { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
