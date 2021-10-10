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
    public class CompetitionsDeleteVM
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
    }
