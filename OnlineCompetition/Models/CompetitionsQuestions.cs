using OnlineCompetition.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class CompetitionsQuestions : BaseClass
    {
        public Competitions Competition { get; set; }
        public AnswerType AnswerType { get; set; } 
        public long? RightCompetitionQuestionAnswerId { get; set; } 
        public long CompetitionId { get; set; }
        public int Sort { get; set; }
    }
}
