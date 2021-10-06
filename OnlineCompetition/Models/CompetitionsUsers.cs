﻿using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class CompetitionsUsers
    {
        public long Id { get; set; }
        public  ApplicationUser StudentUser { get; set; }
        public Guid StudentUserId { get; set; }
        public Competitions Competition { get; set; }
        public long CompetitionId { get; set; }
        public float Score { get; set; }

    }
}
