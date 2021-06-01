using AutoAPI.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoAPI
{
    [Table("Auto")]
    public class Car
    {
        [Key]
        public int ID { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Bouwjaar moet bestaan uit cijfers.")]
        public int Bouwjaar { get; set; }

        [MaxLength(13)]
        public string Brandstof { get; set; }

        [Range(0, 100000)]
        public int prijs { get; set; }

        [Required]
        public bool Verkocht { get; set; }

        [Required]
        public Merk Merk { get; set; }

        [Required]
        public Model Model { get; set; }

        // auth0 gebruiker
        [Required]
        public Person person { get; set; }

    }
}
