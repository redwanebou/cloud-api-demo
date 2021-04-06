using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoAPI
{
    [Table("Persoon")]
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Naam { get; set; }

        public string Achternaam { get; set; }

        public int Leeftijd { get; set; }

        public string Adres { get; set; }
    }
}
