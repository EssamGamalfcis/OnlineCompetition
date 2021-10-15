using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class StudentCompetitionQuestionAnswer
    {
        public long Id { get; set; }
        public string StudentUserId { get; set; }
        public virtual Competitions Competition { get; set; }
        public long CompetitionId { get; set; }
        public virtual Questions Questions { get; set; }
        public long QuestionsId { get; set; }
        public virtual AnswersMaster AnswersMaster { get; set; }
        public long AnswersMasterId { get; set; }
        public long RightAnswersDetailsId { get; set; } /*right Answer*/
        public long? ActualAnswersDetailId { get; set; } /*student answer*/
        public string ActualAnswersText { get; set; } /*student free text answer*/
        public float? StudentScore { get; set; } 
    }
}
