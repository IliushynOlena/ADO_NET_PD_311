using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _03_IntroToEntityFramework.Entities
{
    //Entities = class Airplane Client Flight
    public class Airplane
    {
        //Primary Key naming : Id/id/ID, EntityName+Id
        public int Id { get; set; }
        [Required]//null => not null
        [MaxLength(100)]
        public string Model { get; set; }
        public int MaxPassanger { get; set; }

        //Navigation Properties
        //Relationship type : One to Many (1......*)
        public ICollection<Flight> Flights { get; set; }
    }
}
