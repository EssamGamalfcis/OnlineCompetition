﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel;
using AdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCompetition.Models;


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

        /*Cometition Setup*/
        public async Task<ActionResult> Cometition_Index()
        {
            var data = await _db.Competitions.Where(x=>x.IsDeleted != true).OrderByDescending(x=>x.CreationDate).ToListAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<ActionResult> AddOrEditCompetition(long? id)
        {
            var model = id == 0 ? new Competitions() : await _db.Competitions.FirstOrDefaultAsync(x => x.Id == id);
            return PartialView("_AddOrEditCompetition",model);
        }
        [HttpPost]
        public async Task<ActionResult> AddOrUpdateCompetition(CompetitionsVM obj)
        {
            try
            {
                if (obj.Id == 0)
                {
                    Competitions newCompetition = new Competitions();
                    newCompetition.IsActive = obj.IsActive;
                    newCompetition.IsDeleted = false;
                    newCompetition.NameAR = obj.NameAR;
                    newCompetition.NameEN = obj.NameEN;
                    newCompetition.CreationDate = DateTime.Now;
                    newCompetition.Duration = obj.Duration;
                    await _db.Competitions.AddAsync(newCompetition);
                    _db.SaveChanges();
                    var retObj = new Response
                    {
                        ArabicMsg = "تم الحفظ بنجاح",
                        EnglishMsg = "Saved Successfully",
                        Success = true
                    };
                    return Ok(retObj);
                }
                else
                {
                    Competitions newCompetition = await _db.Competitions.FirstOrDefaultAsync(x => x.Id == obj.Id);
                    newCompetition.IsActive = obj.IsActive;
                    newCompetition.IsDeleted = false;
                    newCompetition.NameAR = obj.NameAR;
                    newCompetition.NameEN = obj.NameEN;
                    newCompetition.CreationDate = DateTime.Now;
                    newCompetition.Duration = obj.Duration;
                    _db.SaveChanges();
                    var retObj = new Response
                    {
                        ArabicMsg = "تم الحفظ بنجاح",
                        EnglishMsg = "Saved Successfully",
                        Success = true
                    };
                    return Ok(retObj);
                }
            }
            catch (Exception e)
            {
                var retObj = new Response
                {
                    ArabicMsg = "خطأ بالسيرفر",
                    EnglishMsg = "Server Error",
                    Success = false
                };
                return Ok(retObj);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCompetition(DeleteVM obj)
        {
            try
            {
                Competitions newCompetition = await _db.Competitions.FirstOrDefaultAsync(x => x.Id == obj.Id);
                newCompetition.IsDeleted = true;
                _db.SaveChanges();
                var retObj = new Response
                {
                    ArabicMsg = "تم الحذف بنجاح",
                    EnglishMsg = "Saved Successfully",
                    Success = true
                };
                return Ok(retObj);
            }
            catch (Exception e)
            {
                var retObj = new Response
                {
                    ArabicMsg = "خطأ بالسيرفر",
                    EnglishMsg = "Server Error",
                    Success = false
                };
                return Ok(retObj);
            }
        }
        /*end of Competition Setup*/
        /*qesutions Setup*/
        public async Task<ActionResult> Question_Index()
        {
            var data = await _db.Questions.Where(x => x.IsDeleted != true).OrderByDescending(x => x.CreationDate).ToListAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<ActionResult> AddOrEditQuestions(long? id)
        {
            var model = new Questions();
            if (id != 0)
            {
                model = await _db.Questions.FirstOrDefaultAsync(x => x.IsDeleted != true && x.Id == id);
            }
            return PartialView("_AddOrEditQuestions", model);
        }
        [HttpPost]
        public async Task<ActionResult> AddOrUpdateQuestions(Questions obj)
        {
            try
            {
                if (obj.Id == 0)
                {
                    obj.CreationDate = DateTime.Now;
                    obj.IsDeleted = false;
                    _db.Questions.Add(obj);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var objToUpdate = await _db.Questions.FirstOrDefaultAsync(x => x.Id == obj.Id);
                    objToUpdate.NameAR = obj.NameAR;
                    objToUpdate.NameEN = obj.NameEN;
                    objToUpdate.Sort = obj.Sort;
                    objToUpdate.TotalScore = obj.TotalScore;
                    objToUpdate.CreationDate = DateTime.Now;
                    objToUpdate.IsDeleted = false;
                    await _db.SaveChangesAsync();
                }
                var retObj = new Response
                {
                    ArabicMsg = "تم الحفظ بنجاح",
                    EnglishMsg = "Saved Successfully",
                    Success = true
                };
                return Ok(retObj);
            }
            catch (Exception e)
            {
                var retObj = new Response
                {
                    ArabicMsg = "خطأ بالسيرفر",
                    EnglishMsg = "Server Error",
                    Success = false
                };
                return Ok(retObj);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteQuestion(DeleteVM obj)
        {
            try
            {
                var newQuestion = await _db.Questions.FirstOrDefaultAsync(x => x.Id == obj.Id);
                newQuestion.IsDeleted = true;
                await _db.SaveChangesAsync();
                var retObj = new Response
                {
                    ArabicMsg = "تم الحذف بنجاح",
                    EnglishMsg = "Saved Successfully",
                    Success = true
                };
                return Ok(retObj);
            }
            catch (Exception e)
            {
                var retObj = new Response
                {
                    ArabicMsg = "خطأ بالسيرفر",
                    EnglishMsg = "Server Error",
                    Success = false
                };
                return Ok(retObj);
            }
        }
    }
}