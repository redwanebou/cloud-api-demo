using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AutoAPI.Models
{
    [Table("Model")]
    public class Model
    {
        [Key]
        public int ID { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string Naam { get; set; }

        [Required]
        public Merk Merk { get; set; }
    }
}
