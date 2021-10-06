using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class CompetitionsQuestionsAnswers
    {
       public long Id { get; set; }
       public CompetitionsQuestions CompetitionsQuestions { get; set; }
       public long CompetitionsQuestionsId { get; set; }
    }
}
