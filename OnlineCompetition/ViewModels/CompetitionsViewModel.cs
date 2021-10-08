using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class CompetitionsVM
    {
        public long? Id { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public string NameAR { get; set; }
        [Required]
        public string NameEN { get; set; }
    }
    public class CompetitionsDeleteVM
    {
        public long Id { get; set; }
    }
}
