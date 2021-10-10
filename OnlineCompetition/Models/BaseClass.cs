using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class BaseClass
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string NameAR { get; set; }
        [Required]
        public string NameEN { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DeleteDate { get; set; }
    }
}
