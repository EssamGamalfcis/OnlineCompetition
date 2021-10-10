using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class QuestionsAnswers : BaseClass
    {
        public CompetitionsQuestions CompetitionsQuestions { get; set; }
        //public long? CompetitionsQuestionsId { get; set; }
        public AnswersMaster AnswersMaster { get; set; }
        //public long? AnswersMasterId { get; set; }
        public long? AnswersDetailsId { get; set; }
        //public long? AnswersDetailsId { get; set; } /*the right answer*/
    }
}
