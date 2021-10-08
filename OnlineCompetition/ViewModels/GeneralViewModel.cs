using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class Response
    {
      public bool Success { get; set; }
        public string ArabicMsg { get; set; }
        public string EnglishMsg { get; set; }
    }
}
