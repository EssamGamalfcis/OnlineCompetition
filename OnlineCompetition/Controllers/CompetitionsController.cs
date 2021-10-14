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
using OnlineCompetition.MVC.Enums;

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

        /*cometition setup*/
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
        /*end competition setup*/
        /*qesutions setup*/
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
        /*end qesutions Setup*/

        /*answer Setup*/
        public async Task<ActionResult> Answer_Index()
        {
            var data = from AM in _db.AnswersMaster
                       where AM.IsDeleted != true
                       select new AnsewrsVM
                       {
                           AnswersMaster = AM,
                           AnswersDetails = _db.AnswersDetails.Where(x=>x.AnswerMasterId == AM.Id).ToList()
                       };


            var test = await data.ToListAsync();
            return View(await data.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult> AddOrEditAnswer(long? id)
        {
            var model = new AnsewrsVM();
            if (id != 0)
            {
                model = (from AM in _db.AnswersMaster
                         where AM.Id == id
                         select new AnsewrsVM
                         {
                             AnswersMaster = AM,
                             AnswersDetails = _db.AnswersDetails.Where(x => x.AnswerMasterId == AM.Id).ToList()
                         }).FirstOrDefault();
            }
            else
            {
                model.AnswersMaster = new AnswersMaster();
                model.AnswersDetails = new List<AnswersDetails>();
            }
            return PartialView("_AddOrEditAnswers", model);
        }
        [HttpPost]
        public async Task<ActionResult> AddOrUpdateAnswer(AnsewrsVM obj)
        {
            try
            {
                if (obj.AnswersMaster.Id == 0)
                {
                    var newAnswerMaster = new AnswersMaster();
                    newAnswerMaster = obj.AnswersMaster;
                    newAnswerMaster.CreationDate = DateTime.Now;
                    newAnswerMaster.IsDeleted = false;
                    if (obj.AnswersMaster.AnswerType == OnlineCompetition.Enums.AnswerType.Article)
                    {
                        if (_db.AnswersMaster.Any(x => x.AnswerType == OnlineCompetition.Enums.AnswerType.Article && x.IsDeleted != true))
                        {
                            var retObj2 = new Response
                            {
                                ArabicMsg = "لا يمكن اضافة اكثر من اجابة للمقالى او الصح و خطأ",
                                EnglishMsg = "can't insert more than one article or true and false answer",
                                Success = false
                            };
                            return Ok(retObj2);
                        }
                    }
                    if (obj.AnswersMaster.AnswerType == OnlineCompetition.Enums.AnswerType.TrueOrFalse)
                    {
                        if (_db.AnswersMaster.Any(x => x.AnswerType == OnlineCompetition.Enums.AnswerType.TrueOrFalse && x.IsDeleted != true))
                        {
                            var retObj2 = new Response
                            {
                                ArabicMsg = "لا يمكن اضافة اكثر من اجابة للمقالى او الصح و خطأ",
                                EnglishMsg = "can't insert more than one article or true and false answer",
                                Success = false
                            };
                            return Ok(retObj2);
                        }
                    }
                    _db.AnswersMaster.Add(newAnswerMaster);
                    await _db.SaveChangesAsync();
                    if (obj.AnswersMaster.AnswerType == OnlineCompetition.Enums.AnswerType.Article)
                    {
                        var newAnswerDetail = new AnswersDetails();
                        newAnswerDetail.AnswerText = "مقالى";
                        newAnswerDetail.AnswerMasterId = newAnswerMaster.Id;
                        _db.AnswersDetails.Add(newAnswerDetail);
                        await _db.SaveChangesAsync();
                    }
                    else if (obj.AnswersMaster.AnswerType == OnlineCompetition.Enums.AnswerType.TrueOrFalse)
                    {
                        var newAnswerDetail = new AnswersDetails();
                        newAnswerDetail.AnswerText = "صح";
                        newAnswerDetail.AnswerMasterId = newAnswerMaster.Id;
                        _db.AnswersDetails.Add(newAnswerDetail);
                        var newAnswerDetail2 = new AnswersDetails();
                        newAnswerDetail2.AnswerText = "خطأ";
                        newAnswerDetail2.AnswerMasterId = newAnswerMaster.Id;
                        _db.AnswersDetails.Add(newAnswerDetail2);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        foreach (var answersDetail in obj.AnswersDetails)
                        {
                            var newAnswerDetail = new AnswersDetails();
                            newAnswerDetail = answersDetail;
                            newAnswerDetail.AnswerText = answersDetail.AnswerText;
                            newAnswerDetail.AnswerMasterId = newAnswerMaster.Id;
                            _db.AnswersDetails.Add(newAnswerDetail);
                        }
                        await _db.SaveChangesAsync();
                    }
                }
                else
                {
                    var newAnswerMaster = await _db.AnswersMaster.FirstOrDefaultAsync(x=>x.Id == obj.AnswersMaster.Id);
                    newAnswerMaster.DeleteDate = DateTime.Now;
                    newAnswerMaster.IsDeleted = true;
                    await _db.SaveChangesAsync();
                    var deleteOldAnswerDetails = await _db.AnswersDetails.Where(x => x.AnswerMasterId == obj.AnswersMaster.Id).ToListAsync();
                    _db.AnswersDetails.RemoveRange(deleteOldAnswerDetails);
                    await _db.SaveChangesAsync();
                    obj.AnswersMaster.Id = 0;
                    await AddOrUpdateAnswer(obj);
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
        public async Task<ActionResult> DeleteAnswer(DeleteVM obj)
        {
            try
            {
                var newQuestion = await _db.AnswersMaster.FirstOrDefaultAsync(x => x.Id == obj.Id);
                newQuestion.IsDeleted = true;
                await _db.SaveChangesAsync();
                var deleteOldAnswerDetails = await _db.AnswersDetails.Where(x => x.AnswerMasterId == obj.Id).ToListAsync();
                _db.AnswersDetails.RemoveRange(deleteOldAnswerDetails);
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
        /*end answer Setup*/
        /*start link competition question answers and link competition students*/
        public async Task<ActionResult> CompetitionQuestionAnswer_Index()
        {
            var data = await _db.Competitions.Where(x => x.IsDeleted != true).OrderByDescending(x => x.CreationDate).ToListAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<ActionResult> AddOrEditCompetitionQuestionAnswers(long id)
        {
            QuestionVM model = new QuestionVM();
            model.Competion = await _db.Competitions.FirstOrDefaultAsync(x => x.Id == id);
            model.Questions = await _db.Questions.Where(x => x.IsDeleted != true).ToListAsync();
            model.AnswersMaster = await _db.AnswersMaster.Where(x => x.IsDeleted != true).ToListAsync();
            ViewBag.SelectedCompetitionQuestionsAnswers = await _db.CompetitionQuestionsAnswers.Where(x => x.CompetitionsId == id).ToListAsync();
            return PartialView("_AddOrEditCompetitionQuestionAnswers", model);
        }
        [HttpGet]
        public async Task<List<AnswersDetails>> GetAnswerDetails(long AnswerMasterId)
        {
            return await _db.AnswersDetails.Where(x => x.AnswerMasterId == AnswerMasterId).ToListAsync();
        }
        public async Task<ActionResult> AddOrUpdateCompetitionQuestionAnswer(CompetitionQuestionAnswerVM obj)
        {
            try
            {
                var listToDelete = await _db.CompetitionQuestionsAnswers.Where(x => x.CompetitionsId == obj.CompetitionQuestionsAnswers[0].CompetitionsId).ToListAsync();
                _db.CompetitionQuestionsAnswers.RemoveRange(listToDelete);
                await _db.SaveChangesAsync();
                foreach (var competitionQuestionsAnswers in obj.CompetitionQuestionsAnswers)
                {
                    var newCompetitionQuestionsAnswers = new CompetitionQuestionsAnswers();
                    newCompetitionQuestionsAnswers = competitionQuestionsAnswers;
                    await _db.CompetitionQuestionsAnswers.AddAsync(newCompetitionQuestionsAnswers);
                }
                await _db.SaveChangesAsync();
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

        [HttpGet]
        public async Task<ActionResult> AddOrEditCompetitionStudents(long id)
        {
            CompetitionStudnetsVM model = new CompetitionStudnetsVM();
            ViewBag.SelectedCompetitionUsers = await _db.CompetitionsUsers.Where(x => x.CompetitionId == id).ToListAsync();
            model.Competion = await _db.Competitions.FirstOrDefaultAsync(x=>x.Id == id);
            model.Students = (from US in _db.Users
                             join UR in _db.UserRoles on US.Id equals UR.UserId
                             join RO in _db.Roles on UR.RoleId equals RO.Id
                             where RO.Name == Roles.Student.ToString()
                             select US).ToList();
            return PartialView("_AddOrEditCompetitionStudents", model);
        }
        public async Task<ActionResult> AddOrUpdateCompetitionStudent(CompetitionStudentVM obj)
        {
            try
            {
                var listToDelete = await _db.CompetitionsUsers.Where(x => x.CompetitionId == obj.CompetitionStudnets[0].CompetitionId).ToListAsync();
                _db.CompetitionsUsers.RemoveRange(listToDelete);
                await _db.SaveChangesAsync();
                foreach (var competitionStudnet in obj.CompetitionStudnets)
                {
                    var newCompetitionStudent = new CompetitionsUsers();
                    newCompetitionStudent = competitionStudnet;
                    await _db.CompetitionsUsers.AddAsync(newCompetitionStudent);
                }
                await _db.SaveChangesAsync();
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
        /*end link competition question answers and link competition students*/
    }
}