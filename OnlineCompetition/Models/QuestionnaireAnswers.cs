using OnlineCompetition.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class QuestionnaireAnswers : BaseClass
    {
        
        public Questionnaire Questionnaire { get; set; }
        public long QuestionnaireId { get; set; }
        public AnswerType AnswerType { get; set; }
    }
}
