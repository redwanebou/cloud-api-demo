using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AutoAPI.Models
{
    public class Person
    {
        [Key]
        public string user_id { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string nickname { get; set; }

        [Required]
        public int geld { get; set; }
    }
}
