using OnlineCompetition.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class AnswersDetails
    {
        public long Id { get; set; }
        public virtual AnswersMaster AnswerMaster { get; set; }
        public long AnswerMasterId { get; set; }
        public string AnswerText { get; set; }
    }
}
