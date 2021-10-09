using System;
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
        public async Task<ActionResult> DeleteCompetition(CompetitionsDeleteVM obj)
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
        public async Task<ActionResult> CometitionQuestion_Index()
        {
            var data = new List<CompetitionQuestionsVM>();
            var competitionsQuestions = await _db.CompetitionsQuestions.Where(x => x.IsDeleted != true).OrderByDescending(x => x.CreationDate).ToListAsync();
            foreach (var competitionsQuestion in competitionsQuestions)
            {
                CompetitionQuestionsVM newObj = new CompetitionQuestionsVM();
                Competitions competition = new Competitions();
                competition = _db.Competitions.FirstOrDefault(x => x.Id == competitionsQuestion.CompetitionId);
                List<CompetitionQuestionsAnswersVM> answers =  _db.CompetitionsQuestionsAnswers.Where(x => x.CompetitionsQuestionsId == competitionsQuestion.Id).
                    Select(y=> new CompetitionQuestionsAnswersVM { 
                                                                Id = y.Id,
                                                                NameAR = y.NameAR,
                                                                NameEN = y.NameEN}).ToList();
                newObj.Id = competitionsQuestion.Id;
                newObj.NameAR = competitionsQuestion.NameAR;
                newObj.NameEN = competitionsQuestion.NameEN;
                newObj.RightAnswerId = competitionsQuestion.RightCompetitionQuestionAnswerId == null ? null : answers.FirstOrDefault(x => x.Id == competitionsQuestion.RightCompetitionQuestionAnswerId.Value).Id;
                newObj.RightAnswerAR = competitionsQuestion.RightCompetitionQuestionAnswerId == null ? null : answers.FirstOrDefault(x => x.Id == competitionsQuestion.RightCompetitionQuestionAnswerId.Value).NameAR;
                newObj.RightAnswerEN = competitionsQuestion.RightCompetitionQuestionAnswerId == null ? null : answers.FirstOrDefault(x => x.Id == competitionsQuestion.RightCompetitionQuestionAnswerId.Value).NameEN;
                
                newObj.AnswerType = competitionsQuestion.AnswerType;
                newObj.CompetitionQuestionsAnswers = answers;
                newObj.CompetitionId = competition.Id;
                newObj.CompetitionAR = competition.NameAR;
                newObj.CompetitionEN = competition.NameEN;
                newObj.Sort = competitionsQuestion.Sort;
                data.Add(newObj);
            }
            return View(data);
        }
        [HttpGet]
        public async Task<ActionResult> AddOrEditCompetitionQuestions(long? id)
        {
            var model = new CompetitionQuestionsVM();
            if (id != 0)
            {
                var competitionsQuestions =  _db.CompetitionsQuestions.FirstOrDefault(x => x.IsDeleted != true && x.Id == id);
                    CompetitionQuestionsVM newObj = new CompetitionQuestionsVM();
                    Competitions competition = new Competitions();
                    competition = _db.Competitions.FirstOrDefault(x => x.Id == competitionsQuestions.CompetitionId);
                    List<CompetitionQuestionsAnswersVM> answers = _db.CompetitionsQuestionsAnswers.Where(x => x.CompetitionsQuestionsId == competitionsQuestions.Id).
                        Select(y => new CompetitionQuestionsAnswersVM
                        {
                            Id = y.Id,
                            NameAR = y.NameAR,
                            NameEN = y.NameEN
                        }).ToList();
                    newObj.Id = competitionsQuestions.Id;
                    newObj.NameAR = competitionsQuestions.NameAR;
                    newObj.NameEN = competitionsQuestions.NameEN;
                    newObj.RightAnswerId = competitionsQuestions.RightCompetitionQuestionAnswerId == null ? null : answers.FirstOrDefault(x => x.Id == competitionsQuestions.RightCompetitionQuestionAnswerId.Value).Id;
                    newObj.RightAnswerAR = competitionsQuestions.RightCompetitionQuestionAnswerId == null ? null : answers.FirstOrDefault(x => x.Id == competitionsQuestions.RightCompetitionQuestionAnswerId.Value).NameAR;
                    newObj.RightAnswerEN = competitionsQuestions.RightCompetitionQuestionAnswerId == null ? null : answers.FirstOrDefault(x => x.Id == competitionsQuestions.RightCompetitionQuestionAnswerId.Value).NameEN;

                    newObj.AnswerType = competitionsQuestions.AnswerType;
                    newObj.CompetitionQuestionsAnswers = answers;
                    newObj.CompetitionId = competition.Id;
                    newObj.CompetitionAR = competition.NameAR;
                    newObj.CompetitionEN = competition.NameEN;
                    newObj.Sort = competitionsQuestions.Sort;
                model = newObj;
            }
            model.Competitions = await _db.Competitions.Where(x => x.IsDeleted != true).ToListAsync();
            return PartialView("_AddOrEditCompetitionQuestions", model);
        }
        [HttpPost]
        public async Task<ActionResult> AddOrUpdateCompetitionQuestions(CompetitionQuestionsVM obj)
        {
            try
            {
                if (obj.Id == 0)
                {
                    var competitionQuestion = new CompetitionsQuestions();
                    competitionQuestion.IsDeleted = false;
                    competitionQuestion.NameAR = obj.NameAR;
                    competitionQuestion.NameEN = obj.NameAR;
                    competitionQuestion.Sort = obj.Sort;
                    competitionQuestion.AnswerType = obj.AnswerType;
                    competitionQuestion.CompetitionId = obj.CompetitionId.Value;
                    competitionQuestion.CreationDate = DateTime.Now;
                    await _db.CompetitionsQuestions.AddAsync(competitionQuestion);
                    await _db.SaveChangesAsync();
                    foreach (var item in obj.CompetitionQuestionsAnswers)
                    {
                        var competitionQuestionAnswer = new CompetitionsQuestionsAnswers();
                        competitionQuestionAnswer.CompetitionsQuestionsId = competitionQuestion.Id;
                        competitionQuestionAnswer.IsDeleted = false;
                        competitionQuestionAnswer.NameAR = item.NameAR;
                        competitionQuestionAnswer.NameEN = item.NameAR;
                        competitionQuestionAnswer.CreationDate = DateTime.Now;
                        await _db.CompetitionsQuestionsAnswers.AddAsync(competitionQuestionAnswer);
                        await _db.SaveChangesAsync();
                        if (item.IsRightAnswer == true)
                        {
                            var updateRightAnswer = await _db.CompetitionsQuestions.FirstOrDefaultAsync(x => x.Id == competitionQuestion.Id);
                            updateRightAnswer.RightCompetitionQuestionAnswerId = competitionQuestionAnswer.Id;
                            await _db.SaveChangesAsync();

                        }
                    }
                }
                else
                {
                    var competitionQuestion = await _db.CompetitionsQuestions.FirstOrDefaultAsync(x => x.Id == obj.Id);
                    competitionQuestion.IsDeleted = false;
                    competitionQuestion.NameAR = obj.NameAR;
                    competitionQuestion.NameEN = obj.NameAR;
                    competitionQuestion.Sort = obj.Sort;
                    competitionQuestion.AnswerType = obj.AnswerType;
                    competitionQuestion.CompetitionId = obj.CompetitionId.Value;
                    competitionQuestion.CreationDate = DateTime.Now;
                    await _db.SaveChangesAsync();
                    foreach (var item in obj.CompetitionQuestionsAnswers)
                    {
                        var competitionQuestionAnswer = await _db.CompetitionsQuestionsAnswers.FirstOrDefaultAsync(x => x.Id == item.Id);
                        competitionQuestionAnswer.CompetitionsQuestionsId = competitionQuestion.Id;
                        competitionQuestionAnswer.IsDeleted = false;
                        competitionQuestionAnswer.NameAR = item.NameAR;
                        competitionQuestionAnswer.NameEN = item.NameAR;
                        competitionQuestionAnswer.CreationDate = DateTime.Now;
                        await _db.SaveChangesAsync();
                        if (item.IsRightAnswer == true)
                        {
                            competitionQuestion.RightCompetitionQuestionAnswerId = competitionQuestionAnswer.Id;
                            await _db.SaveChangesAsync();

                        }
                    }
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
        public async Task<ActionResult> DeleteCompetitionQuestion(CompetitionsDeleteVM obj)
        {
            try
            {
                var newCompetitionQuestion = await _db.CompetitionsQuestions.FirstOrDefaultAsync(x => x.Id == obj.Id);
                newCompetitionQuestion.IsDeleted = true;
                _db.SaveChanges();
                var realtedAnswers = await _db.CompetitionsQuestionsAnswers.Where(x => x.CompetitionsQuestionsId == obj.Id).ToListAsync();
                foreach (var item in realtedAnswers)
                {
                    item.IsDeleted = true;
                    await _db.SaveChangesAsync();
                }
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