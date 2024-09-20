using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _03_IntroToEntityFramework.Entities
{
    [Table("Passangers")]
    public class Client
    {
        public int Id { get; set; }
        [Required]//null => not null
        [MaxLength(100)]
        [Column("FirstName")]
        public string Name { get; set; }
        [Required]//null => not null
        [MaxLength(50)]
        public string Email { get; set; }
        public DateTime? Birhdate { get; set; }// ? -> not null - null
        //Relationship type : Many to Many (*......*)
        public ICollection<Flight> Flights { get; set; }

    }
}
