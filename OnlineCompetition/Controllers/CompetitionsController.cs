using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineCompetition.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompetitionsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CompetitionsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ActionResult> Index()
        {
            var data = await _db.Competitions.ToListAsync();
            return View(data);
        }
    }
}