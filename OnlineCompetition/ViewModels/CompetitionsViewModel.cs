using Microsoft.AspNetCore.Identity;
using OnlineCompetition.Enums;
using OnlineCompetition.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class CompetitionsVM
    {
        public long? Id { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public string NameAR { get; set; }
        [Required]
        public string NameEN { get; set; }
    }
    public class DeleteVM
    {
        public long Id { get; set; }
    }
    public class CompetitionQuestionsVM
    {
        public long? Id { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public long? RightAnswerCode { get; set; }
        public string RightAnswerAR { get; set; }
        public string RightAnswerEN { get; set; }
        public long? CompetitionId { get; set; }
        public string CompetitionAR { get; set; }
        public string CompetitionEN { get; set; }
        public AnswerType AnswerType { get; set; }
        //public List<CompetitionQuestionsAnswersVM> CompetitionQuestionsAnswers { get; set; }
        public List<Competitions> Competitions { get; set; }
        public int Sort { get; set; }
        public float? TotalGrade { get; set; }

    }
    public class CompetitionQuestionsAnswersVM
    {
        public long Id { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public bool? IsRightAnswer { get; set; }
    }
    public class AnsewrsVM
    {
        public AnswersMaster AnswersMaster { get; set; }
        public List<AnswersDetails> AnswersDetails { get; set; }
    }
    public class QuestionVM
    {
       public Competitions Competion { get; set; }
        public List<Questions> Questions { get; set; }
        public List<AnswersMaster> AnswersMaster { get; set; }
    }
    public class CompetitionQuestionAnswerVM
    {
        public List<CompetitionQuestionsAnswers> CompetitionQuestionsAnswers { get; set; }
    }
    public class CompetitionStudnetsVM
    {
        public List<ApplicationUser> Students { get; set; }
        public Competitions Competion { get; set; }
    }
    public class CompetitionStudentVM
    {
        public List<CompetitionsUsers> CompetitionStudnets { get; set; }
    }
    public class CompetitionStudentToSolveVM
    {
        public Competitions Competitions { get; set; }
        public CompetitionsUsers CompetitionsUsers { get; set; }
        public double TotalScore { get; set; }
        public double? ActualScore { get; set; }
        public bool? SolvedBefore { get; set; }
    }
    public class CompetitionQuestionsForStudentVM
    {
        public Competitions Competitions { get; set; }
        public List<QuestionWithAnswersVM> Questions { get; set; }
    }
    public class QuestionWithAnswersVM
    { 
        public Questions Question { get; set; }
        public AnswersMaster AnswersMaster { get; set; }
        public List<AnswersDetails> AnswersDetails { get; set; }
        public long? AnswerDetailsId { get; set; } /*selected answer*/
        public string ActualAnswerText { get; set; }
        
    }
    public class StudentCompetitionQuestionAnswerVM
    {
        public List<StudentCompetitionQuestionAnswer> StudentCompetitionQuestionAnswers { get; set; }
    }
}
