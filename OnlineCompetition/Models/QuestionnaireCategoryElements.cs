using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class QuestionnaireCategoryElements : BaseClass
    {
        public QuestionnaireCategory QuestionnaireCategory { get; set; }
        public long QuestionnaireCategoryId { get; set; }
    }
}
