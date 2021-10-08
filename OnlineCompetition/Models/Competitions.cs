using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class Competitions : BaseClass
    {
        [Required]
        public int Duration { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;

    }
}
