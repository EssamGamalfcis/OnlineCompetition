using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class QuestionnaireUsers
    {
        public long Id { get; set; }
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public long QuestionnaireId { get; set; }
    }
}
