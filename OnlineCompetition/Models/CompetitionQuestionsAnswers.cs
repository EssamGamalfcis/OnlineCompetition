using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class CompetitionQuestionsAnswers
    {
        public long Id { get; set; }
        public virtual Competitions Competitions { get; set; }
        public long CompetitionsId { get; set; }
        public virtual Questions Question { get; set; }
        public long QuestionId { get; set; }
        public virtual AnswersMaster AnswersMaster { get; set; }
        public long AnswersMasterId { get; set; }
        public long AnswersDetailsId { get; set; }
    }
}
