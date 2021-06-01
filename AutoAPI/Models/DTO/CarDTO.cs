using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoAPI.Models.DTO
{
    public class CarDTO
    {
        public int ID { get; set; }

        public int Bouwjaar { get; set; }

        public string Brandstof { get; set; }

        public int prijs { get; set; }
        public bool Verkocht { get; set; }

        public Merk Merk { get; set; }

        public Model Model { get; set; }

        public PersonDTO person { get; set; }

    }
}
