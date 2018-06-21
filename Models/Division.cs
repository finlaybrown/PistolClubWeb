using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Models
{
    public class Division
    {
        [Key]
        public int DivisionID { get; set; }
        [Required]
        public string DivisionName { get; set; }
    }
}
