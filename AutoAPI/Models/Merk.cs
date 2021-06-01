using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoAPI.Models
{
    [Table("Merk")]
    public class Merk
    {
        [Key]
        public int ID { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string Naam { get; set; }
        
        //public ICollection<Model> Modellen { get; set; }
    }
}
