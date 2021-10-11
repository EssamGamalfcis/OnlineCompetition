using OnlineCompetition.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class Questions : BaseClass
    {
        public int Sort { get; set; } = 0;
        public float TotalScore { get; set; } = 10;
    }
}
