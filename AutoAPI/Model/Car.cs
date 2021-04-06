using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoAPI
{
    [Table("Auto")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Merk { get; set; }

        public string Model { get; set; }

        public int Bouwjaar { get; set; }

        public string Brandstof { get; set; }
    }
}
