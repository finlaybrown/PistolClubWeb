using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Models
{
    public class Grade
    {
        [Key]
        public int GradeID { get; set; }
        [Required]
        public string GradeName { get; set; }
    }
}
