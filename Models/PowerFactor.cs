using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Models
{
    public class PowerFactor
    {
        [Key]
        public int PowerID { get; set; }
        [Required]
        public string PowerName { get; set; }
    }
}
