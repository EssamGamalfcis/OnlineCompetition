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
    [Authorize]
    public class CompetitionsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CompetitionsController(ApplicationDbContext db)
        {
            _db = db;
        }

        /*cometition setup*/
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Cometition_Index()
        {
            var data = await _db.Competitions.Where(x=>x.IsDeleted != true).OrderByDescending(x=>x.CreationDate).ToListAsync();
            return View(data);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddOrEditCompetition(long? id)
        {
            var model = id == 0 ? new Competitions() : await _db.Competitions.FirstOrDefaultAsync(x => x.Id == id);
            return PartialView("_AddOrEditCompetition",model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddOrUpdateCompetition(CompetitionsVM obj)
        {
            try
            {
                if (obj.Id == 0)
                {
                    List<Competitions> competitions = await _db.Competitions.ToListAsync();
                    foreach (var competition in competitions)
                    {
                        competition.IsActive = false;
                    }
                    _db.SaveChanges();
                    Competitions newCompetition = new Competitions();
                    newCompetition.IsActive = obj.IsActive;
                    newCompetition.IsDeleted = false;
                    newCompetition.NameAR = obj.NameAR;
                    newCompetition.NameEN = obj.NameEN;
                    newCompetition.CreationDate = DateTime.Now;
                    newCompetition.Duration = obj.Duration;
                    await _db.Competitions.AddAsync(newCompetition);
                    _db.SaveChanges();
                    List<ApplicationUser> students = await (from user in _db.Users
                                                      join userRole in _db.UserRoles on user.Id equals userRole.UserId
                                                      join role in _db.Roles on userRole.RoleId equals role.Id
                                                      where role.Name == "Student"
                                                      select user).ToListAsync();
                    foreach (var stud in students)
                    {
                        var newCompetitionUser = new CompetitionsUsers();
                        newCompetitionUser.CompetitionId = newCompetition.Id;
                        newCompetitionUser.SolvedBefore = false;
                        newCompetitionUser.StudentUserId = stud.Id;
                        newCompetitionUser.Score = null;
                        await _db.CompetitionsUsers.AddAsync(newCompetitionUser);
                        _db.SaveChanges();
                    }
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Question_Index(long? competitionId)
        {
            //var data = await _db.Questions.Where(x => x.IsDeleted != true).OrderByDescending(x => x.CreationDate).ToListAsync();
            long competitionIdToQuery = competitionId == null ?  _db.Competitions.FirstOrDefault(x => x.IsActive == true).Id : competitionId.Value;
            var data = await (from QU in _db.Questions
                        join COMQU in _db.CompetitionQuestionsAnswers on QU.Id equals COMQU.QuestionId
                        where QU.IsDeleted != true && COMQU.CompetitionsId == competitionIdToQuery
                        select new QuestionVMNew
                        { 
                            Question = QU,
                            AnswerMasterId = COMQU.AnswersMasterId,
                            AnswerDetailId = COMQU.AnswersDetailsId
                        }
                        ).ToListAsync();
            ViewBag.CompetitionId = competitionIdToQuery;
            ViewBag.AllCompetitions = await _db.Competitions.Where(x=>x.IsDeleted != true).ToListAsync();
            return View(data);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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


                    var newAnswerMaster = new AnswersMaster();
                    newAnswerMaster.NameAR = "اجابة";
                    newAnswerMaster.NameEN = "Answer";
                    newAnswerMaster.AnswerType = OnlineCompetition.Enums.AnswerType.Article;
                    newAnswerMaster.CreationDate = DateTime.Now;
                    newAnswerMaster.IsDeleted = false;
                    _db.AnswersMaster.Add(newAnswerMaster);
                    await _db.SaveChangesAsync();

                    /*Default*/
                    var newAnswerDetail = new AnswersDetails();
                    newAnswerDetail.AnswerText = "مقالى";
                    newAnswerDetail.AnswerMasterId = newAnswerMaster.Id;
                    _db.AnswersDetails.Add(newAnswerDetail);
                    await _db.SaveChangesAsync();
                    CompetitionQuestionsAnswers newCompetitionAnswer = new CompetitionQuestionsAnswers();
                    newCompetitionAnswer.CompetitionsId = obj.CompetitionId;
                    newCompetitionAnswer.QuestionId = obj.Id;
                    newCompetitionAnswer.AnswersDetailsId = newAnswerDetail.Id;
                    newCompetitionAnswer.AnswersMasterId = newAnswerMaster.Id;
                    await _db.CompetitionQuestionsAnswers.AddAsync(newCompetitionAnswer);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddOrEditAnswer(long? id,long questionId,long answerDetailId)
        {
            var model = new AnsewrsVM();
            if (id != 0)
            {
                model = (from AM in _db.AnswersMaster
                         where AM.Id == id
                         select new AnsewrsVM
                         {
                             QuestionId = questionId,
                             AnswersMaster = AM,
                             AnswersDetails = _db.AnswersDetails.Where(x => x.AnswerMasterId == AM.Id).ToList()
                         }).FirstOrDefault();
            }
            else
            {
                model.QuestionId = questionId;
                model.AnswersMaster = new AnswersMaster();
                model.AnswersDetails = new List<AnswersDetails>();
            }
            ViewBag.AnswerDetailId = answerDetailId;
            return PartialView("_AddOrEditAnswers", model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddOrUpdateAnswerNew(AnsewrsNewVM obj)
        {
            try
            {
                if (obj.AnswersMaster.Id == 0)
                {
                    long answerDetailId = 0;
                    var newAnswerMaster = new AnswersMaster();
                    newAnswerMaster = obj.AnswersMaster;
                    newAnswerMaster.CreationDate = DateTime.Now;
                    newAnswerMaster.IsDeleted = false;
                    //if (obj.AnswersMaster.AnswerType == OnlineCompetition.Enums.AnswerType.Article)
                    //{
                    //    if (_db.AnswersMaster.Any(x => x.AnswerType == OnlineCompetition.Enums.AnswerType.Article && x.IsDeleted != true))
                    //    {
                    //        var retObj2 = new Response
                    //        {
                    //            ArabicMsg = "لا يمكن اضافة اكثر من اجابة للمقالى او الصح و خطأ",
                    //            EnglishMsg = "can't insert more than one article or true and false answer",
                    //            Success = false
                    //        };
                    //        return Ok(retObj2);
                    //    }
                    //}
                    //if (obj.AnswersMaster.AnswerType == OnlineCompetition.Enums.AnswerType.TrueOrFalse)
                    //{
                    //    if (_db.AnswersMaster.Any(x => x.AnswerType == OnlineCompetition.Enums.AnswerType.TrueOrFalse && x.IsDeleted != true))
                    //    {
                    //        var retObj2 = new Response
                    //        {
                    //            ArabicMsg = "لا يمكن اضافة اكثر من اجابة للمقالى او الصح و خطأ",
                    //            EnglishMsg = "can't insert more than one article or true and false answer",
                    //            Success = false
                    //        };
                    //        return Ok(retObj2);
                    //    }
                    //}
                    _db.AnswersMaster.Add(newAnswerMaster);
                    await _db.SaveChangesAsync();
                    if (obj.AnswersMaster.AnswerType == OnlineCompetition.Enums.AnswerType.Article)
                    {
                        var newAnswerDetail = new AnswersDetails();
                        newAnswerDetail.AnswerText = "مقالى";
                        newAnswerDetail.AnswerMasterId = newAnswerMaster.Id;
                        _db.AnswersDetails.Add(newAnswerDetail);
                        await _db.SaveChangesAsync();
                        answerDetailId = newAnswerDetail.Id;
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
                        answerDetailId = obj.AnswersDetails.FirstOrDefault(x => x.IsRight == true) == obj.AnswersDetails.FirstOrDefault() ? newAnswerDetail.Id : newAnswerDetail2.Id;
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
                            await _db.SaveChangesAsync();
                            if (answerDetailId == 0)
                            {
                                answerDetailId = answersDetail.IsRight ? newAnswerDetail.Id : 0;
                            }
                        }
                       
                    }
                    CompetitionQuestionsAnswers newCompetitionAnswer =await _db.CompetitionQuestionsAnswers.FirstOrDefaultAsync(x => x.CompetitionsId == obj.CompetitionId && x.QuestionId == obj.QuestionId);
                    newCompetitionAnswer.AnswersDetailsId = answerDetailId;
                    newCompetitionAnswer.AnswersMasterId = newAnswerMaster.Id;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var newAnswerMaster = await _db.AnswersMaster.FirstOrDefaultAsync(x => x.Id == obj.AnswersMaster.Id);
                    newAnswerMaster.DeleteDate = DateTime.Now;
                    newAnswerMaster.IsDeleted = true;
                    await _db.SaveChangesAsync();
                    var deleteOldAnswerDetails = await _db.AnswersDetails.Where(x => x.AnswerMasterId == obj.AnswersMaster.Id).ToListAsync();
                    _db.AnswersDetails.RemoveRange(deleteOldAnswerDetails);
                    await _db.SaveChangesAsync();
                    obj.AnswersMaster.Id = 0;
                    await AddOrUpdateAnswerNew(obj);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CompetitionQuestionAnswer_Index()
        {
            var data = await _db.Competitions.Where(x => x.IsDeleted != true).OrderByDescending(x => x.CreationDate).ToListAsync();
            return View(data);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<List<AnswersDetails>> GetAnswerDetails(long AnswerMasterId)
        {
            return await _db.AnswersDetails.Where(x => x.AnswerMasterId == AnswerMasterId).ToListAsync();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        /*student competition*/
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> CometitionStudnet_Index()
        {
            List<CompetitionStudentToSolveVM> data = new List<CompetitionStudentToSolveVM>();
            string userId =  _db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
            List<long> competitionAuthorizeToSee = await _db.CompetitionsUsers.Where(x => x.StudentUserId == userId).Select(x=>x.CompetitionId).ToListAsync();
            data = (
                    from COM in _db.Competitions
                    join COMUS in _db.CompetitionsUsers on COM.Id equals COMUS.CompetitionId
                    where COM.IsDeleted != true && COM.IsActive == true && competitionAuthorizeToSee.Contains(COM.Id) && COMUS.StudentUserId == userId
                    select new CompetitionStudentToSolveVM { 
                    Competitions = COM,
                    CompetitionsUsers = COMUS,
                    ActualScore = COMUS.Score
                    }).ToList();
            var modelToSend = new List<CompetitionStudentToSolveVM>();
            foreach (var item in data)
            {
                CompetitionStudentToSolveVM newObj = new CompetitionStudentToSolveVM();
                newObj.Competitions = item.Competitions;
                newObj.CompetitionsUsers = item.CompetitionsUsers;
                newObj.ActualScore = item.ActualScore;
                var questionsId = await _db.CompetitionQuestionsAnswers.Where(x => x.CompetitionsId == item.Competitions.Id).Select(x=>x.QuestionId).ToListAsync();
                newObj.TotalScore =  _db.Questions.Where(x => questionsId.Contains(x.Id)).Select(x => x.TotalScore).ToList().Sum();
                newObj.SolvedBefore = _db.CompetitionsUsers.FirstOrDefault(x => x.CompetitionId == item.Competitions.Id && x.StudentUserId == userId).SolvedBefore;
                modelToSend.Add(newObj);
            }
            ViewBag.CompetitionStudnet = await _db.CompetitionsUsers.Where(x => competitionAuthorizeToSee.Contains(x.Id)).ToListAsync();

            return View(modelToSend);
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> StartCompetition(long compeitionId)
        {
            CompetitionQuestionsForStudentVM model = new CompetitionQuestionsForStudentVM();
            string userId = _db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
            model.Competitions = await _db.Competitions.FirstOrDefaultAsync(x => x.Id == compeitionId);
            List<CompetitionQuestionsAnswers> compQuestionsAnswers = await _db.CompetitionQuestionsAnswers.Where(x => x.CompetitionsId == compeitionId).ToListAsync();
            List<long> compQuestions = compQuestionsAnswers.Select(x => x.QuestionId).ToList();
            model.Questions = (
                                from QUE in _db.Questions
                                join COMQUE in _db.CompetitionQuestionsAnswers on QUE.Id equals COMQUE.QuestionId
                                where COMQUE.CompetitionsId == compeitionId
                                select new QuestionWithAnswersVM
                                { 
                                    Question = QUE,
                                    AnswersMaster = _db.AnswersMaster.FirstOrDefault(x=>x.Id == COMQUE.AnswersMasterId),
                                    AnswersDetails = _db.AnswersDetails.Where(x=>x.AnswerMasterId == COMQUE.AnswersMasterId).ToList(),
                                    ActualAnswerText = "",
                                    AnswerDetailsId = null,
                                }).ToList();
            return PartialView("_StudentCompetition", model);
        }
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> SubmitStudentAnswers(StudentCompetitionQuestionAnswerVM obj)
        {
            try
            {
                string userId = _db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
                float? actualScore = null;
                foreach (var StudentCompetitionQuestionAnswer in obj.StudentCompetitionQuestionAnswers)
                {
                    var newStudentCompetitionQuestionAnswer = new StudentCompetitionQuestionAnswer();
                    newStudentCompetitionQuestionAnswer = StudentCompetitionQuestionAnswer;
                    newStudentCompetitionQuestionAnswer.StudentUserId = userId;
                    newStudentCompetitionQuestionAnswer.RightAnswersDetailsId = _db.CompetitionQuestionsAnswers.FirstOrDefault(x => x.CompetitionsId == StudentCompetitionQuestionAnswer.CompetitionId && x.QuestionId == StudentCompetitionQuestionAnswer.QuestionsId).AnswersDetailsId;
                    if (string.IsNullOrEmpty(StudentCompetitionQuestionAnswer.ActualAnswersText) == true)
                    {
                        long questionId = StudentCompetitionQuestionAnswer.QuestionsId;
                        float questionFullScore =  _db.Questions.Where(x => x.Id == questionId).FirstOrDefault().TotalScore;
                        newStudentCompetitionQuestionAnswer.StudentScore = StudentCompetitionQuestionAnswer.ActualAnswersDetailId == newStudentCompetitionQuestionAnswer.RightAnswersDetailsId ? questionFullScore : 0;
                        actualScore += newStudentCompetitionQuestionAnswer.StudentScore;
                        actualScore = actualScore == null ? 0 : actualScore;
                    }
                    await _db.StudentCompetitionQuestionAnswer.AddAsync(newStudentCompetitionQuestionAnswer);
                    await _db.SaveChangesAsync();
                }
                long competitionId = obj.StudentCompetitionQuestionAnswers[0].CompetitionId;
                bool isValidToCorrect = true;
                var competitionQuestionsAnswers = await _db.CompetitionQuestionsAnswers.Where(x => x.CompetitionsId == competitionId).ToListAsync();
                foreach (var competitionQuestionsAnswer in competitionQuestionsAnswers)
                {
                    AnswersMaster answerMaster = await _db.AnswersMaster.FirstOrDefaultAsync(x => x.Id == competitionQuestionsAnswer.AnswersMasterId);
                    if (answerMaster.AnswerType == OnlineCompetition.Enums.AnswerType.Article)
                    {
                        isValidToCorrect = false;
                        break;
                    }
                }
                var competitionUser = _db.CompetitionsUsers.FirstOrDefault(x => x.StudentUserId == userId && x.CompetitionId == obj.StudentCompetitionQuestionAnswers[0].CompetitionId);
                competitionUser.SolvedBefore = true;
                if (isValidToCorrect)
                {
                    competitionUser.Score = actualScore;
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
        /*end student competition*/
        /*start corrector competition*/

        [HttpGet]
        [Authorize(Roles = "Corrector,Teacher")]
        public async Task<ActionResult> GetCompetitionToCorrectIndex(long? competitionId)
        {
            var model = new List<CorrectorIndexVM>();
            var modelToSend = new List<CorrectorIndexVM>();
            if (competitionId == null)
            {
                model = (from COMUS in _db.CompetitionsUsers
                         join COM in _db.Competitions on COMUS.CompetitionId equals COM.Id
                         join STUD in _db.Users on COMUS.StudentUserId equals STUD.Id
                         where COMUS.SolvedBefore == true && COMUS.Score == null
                         select new CorrectorIndexVM
                         {
                             Competition = COM,
                             Student = STUD,
                             StudentScore = COMUS.Score
                         
                         }).ToList();
            }
            foreach (var item in model)
            {
                CorrectorIndexVM newObj = new CorrectorIndexVM();
                newObj = item;
                var questionsId = await _db.CompetitionQuestionsAnswers.Where(x => x.CompetitionsId == newObj.Competition.Id).Select(x => x.QuestionId).ToListAsync();
                newObj.CompetitionFullScore = _db.Questions.Where(x => questionsId.Contains(x.Id)).Select(x => x.TotalScore).ToList().Sum();
                modelToSend.Add(newObj);
            }
            return View(modelToSend);
        }
        [HttpGet]
        [Authorize(Roles = "Corrector,Teacher")]
        public async Task<ActionResult> GetCompetitionToCorrect(long competitionId ,string userId)
        {
            try
            {
                var model = new List<CorrectorVM>();
                model = (from STAN in _db.StudentCompetitionQuestionAnswer
                         join COM in _db.Competitions on STAN.CompetitionId equals COM.Id
                         join QU in _db.Questions on STAN.QuestionsId equals QU.Id
                         where STAN.CompetitionId == competitionId && STAN.StudentUserId == userId
                         select new CorrectorVM
                         {
                             Id = STAN.Id,
                             CompetitionId = STAN.CompetitionId,
                             CompetitionName = COM.NameAR,
                             QuestionId = STAN.Id,
                             QuestionName = QU.NameAR,
                             ActualAnswerDetailId = STAN.ActualAnswersDetailId,
                             ActualAnswerDetailText = STAN.ActualAnswersDetailId == null ? null : _db.AnswersDetails.FirstOrDefault(x=>x.Id == STAN.ActualAnswersDetailId.Value).AnswerText,
                             ActualFreeTextAnswer = STAN.ActualAnswersText,
                             AnswerDetailId = STAN.RightAnswersDetailsId,
                             AnswerDetailText = _db.AnswersDetails.FirstOrDefault(x => x.Id == STAN.RightAnswersDetailsId).AnswerText,
                             StudentScore = STAN.StudentScore,
                             QuestionScore = QU.TotalScore
                         }).ToList();
                return PartialView("_CorrectCompetition", model);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Corrector,Teacher")]
        public async Task<ActionResult> SubmitStudentScore(SubmitStudentAnswersVM obj)
        {
            try
            {
                float studentTotalScore = 0;
                string studId = "";
                long competitionId = 0;
                foreach (var StudentCompetitionQuestionAnswer in obj.Data)
                {
                    var newStudentCompetitionQuestionAnswer = await _db.StudentCompetitionQuestionAnswer.FirstOrDefaultAsync(x => x.Id == StudentCompetitionQuestionAnswer.Id);
                    newStudentCompetitionQuestionAnswer.StudentScore = StudentCompetitionQuestionAnswer.StudentScore;
                    studentTotalScore += newStudentCompetitionQuestionAnswer.StudentScore == null ? 0 : newStudentCompetitionQuestionAnswer.StudentScore.Value;
                    if (studId == "" && competitionId == 0)
                    {
                        studId = newStudentCompetitionQuestionAnswer.StudentUserId;
                        competitionId = newStudentCompetitionQuestionAnswer.CompetitionId;
                    }
                }
                var competitionUser = await _db.CompetitionsUsers.FirstOrDefaultAsync(x => x.CompetitionId == competitionId && x.StudentUserId == studId);
                competitionUser.Score = studentTotalScore;
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
    }
}