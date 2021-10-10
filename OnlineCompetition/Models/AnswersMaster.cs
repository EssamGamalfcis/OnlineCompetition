using OnlineCompetition.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCompetition.Models
{
    public class AnswersMaster : BaseClass
    {
        public AnswerType AnswerType { get; set; }
    }
}
