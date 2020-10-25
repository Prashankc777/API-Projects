using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyApi.Models.DTO
{
    public class trailUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        public Models.Trail.DifficultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }

        [Required]
        public double Elevation { get; set; }


    }
}
