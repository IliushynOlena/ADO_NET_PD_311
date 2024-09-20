using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _03_IntroToEntityFramework.Entities
{
    public class Flight
    {
        [Key]//primary key
        public int Number { get; set; }
        [Required, MaxLength(50)]//null => not null
        public string ArrivalCity { get; set; }
        [Required, MaxLength(50)]//null => not null
        public string BoardingCity { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime BoardingTime { get; set; }
        public int? Rating { get; set; }

        //Navigation Properties
        //Relationship type : One to Many (1......*)
        public Airplane Airplane { get; set; }
        //Foreing  key naming : RelatedEntityName + RelatedEntityPrimaryKeyName
        public int AirplaneId { get; set; }//foreign key
        //Relationship type : Many to Many (*......*)
        public ICollection<Client> Clients { get; set; }

    }
}
