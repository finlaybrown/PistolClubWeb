using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Models
{
    public class Score
    {
        [Key]
        public int ScoreID { get; set; }

        //Foriegn Keys
        public int UserID { get; set; }
        public User User { get; set; }

        public int GradeID { get; set; }
        public Grade Grade { get; set; }

        public int PowerID { get; set; }
        public PowerFactor Power { get; set; }

        public int DivisionID { get; set; }
        public Division Division { get; set; }

        //Score Data
        [Required]
        public string StageName { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public int Penalty { get; set; }
        [Required]
        public double Time { get; set; }
        [Required]
        public double HitFactor { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime ShootDate { get; set; }

        [Required]
        public int StagePoints { get; set; }
    }
}
