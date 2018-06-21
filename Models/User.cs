using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        [Required]
        public string UserLastName { get; set; }
        [Required]
        public string PistolNZNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
