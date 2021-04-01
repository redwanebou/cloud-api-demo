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
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("Naam")]
        [Required]
        public string Naam { get; set; }

        [Column("Achternaam")]
        [Required]
        public string Achternaam { get; set; }

        [Column("Leeftijd")]
        [Required]
        public int Leeftijd { get; set; }

        [Column("Adres")]
        [Required]
        public string Adres { get; set; }
    }
}
