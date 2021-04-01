using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoAPI
{
    [Table("Auto")]
    public class Car
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("Merk")]
        [Required]
        public string Merk { get; set; }

        [Column("Model")]
        [Required]
        public string Model { get; set; }

        [Column("Bouwjaar")]
        [Required]
        public int Bouwjaar { get; set; }

        [Column("Brandstof")]
        [Required]
        public string Brandstof { get; set; }
    }
}
