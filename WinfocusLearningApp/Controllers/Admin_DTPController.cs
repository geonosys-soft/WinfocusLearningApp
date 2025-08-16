using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinfocusLearningApp.Authentication;
using WinfocusLearningApp.DataEntity;
using WinfocusLearningApp.ViewModels;

namespace WinfocusLearningApp.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class Admin_DTPController : Controller
    {
        Winfocus_CS db = new Winfocus_CS();
        // GET: Admin_DTP
        public ActionResult DTP_List()
        {
            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel();
            var dtpUsers = (from us in db.Users
                           join ur in db.Roles on us.RoleId equals ur.RoleId
                           join r in db.Roles on ur.RoleId equals r.RoleId where us.IsDeleted==0 && r.RoleId==4
                           select new UserDetailsViewModel
                           {
                               ActivationCode = us.ActivationCode,
                               RoleId = ur.RoleId,
                               CreatedBy = us.CreatedBy,
                               CreatedDate = us.CreatedDate,
                               DeletedBy = us.DeletedBy,
                               DeletedDate = us.DeletedDate,
                               Email = us.Email,
                               EmailVerify = us.EmailVerify,
                               FirstName = us.FirstName,
                               LastName = us.LastName,
                               Id = us.Id,
                               IsActive = us.IsActive,
                               IsDeleted = us.IsDeleted,
                               MobileNo = us.MobileNo,
                               Password = us.Password,
                               Picture = us.Picture,
                               ResetPasswordCode = us.ResetPasswordCode,
                               RoleName=ur.RoleName,
                               UniqueID = us.UniqueID,
                               UserName = us.UserName
                           }).ToList();
            if (dtpUsers != null)
            {
                userDetailsViewModel.UserDetails = dtpUsers;
            }
            return View(userDetailsViewModel);
        }
        public ActionResult QuestionAnswers(int? Id)
        {

            TblQuestionAnswer answer = new TblQuestionAnswer();
            if (Id == null)
            {
                var acyr = db.TblAccademicYears.Where(p => p.IsDeleted == 0);


                ViewBag.AccademicYearId = new SelectList(acyr, "Id", "AccademicYear");
                if (acyr.ToList().Count == 1)
                {
                    ViewBag.SyllabusID = new SelectList(db.TblSyllabus.Where(p => p.IsDeleted == 0 && p.ACID == acyr.FirstOrDefault().Id), "Id", "Name");
                }
                else
                {
                    ViewBag.SyllabusID = new SelectList(Enumerable.Empty<string>());
                }

                ViewBag.ClassID = new SelectList(Enumerable.Empty<string>());
                ViewBag.StreamID = new SelectList(Enumerable.Empty<string>());
                ViewBag.SubjectID = new SelectList(Enumerable.Empty<string>());
                ViewBag.ChapterID = new SelectList(Enumerable.Empty<string>());
                ViewBag.QuestionType = new SelectList(new[]
                    {
                        new { Id = 1, Name = "MCQ" },
                        new { Id = 2, Name = "Teacher MCQ" },
                        new { Id = 3, Name = "Question Bank" }
                    }, "Id", "Name");
                ViewBag.Week = new SelectList(new[]
                {
                    new { Id = 1, Name="Week 1" },
                    new { Id = 2, Name="Week 2" },
                    new { Id = 3, Name="Week 3" },
                    new { Id = 4, Name="Week 4" },
                }, "Id", "Name");
                ViewBag.Month = new SelectList(new[]
                {
                    new { Id = 1, Name="January" },
                    new { Id = 2, Name="February" },
                    new { Id = 3, Name="March" },
                    new { Id = 4, Name="April" },
                    new { Id = 5, Name="May" },
                    new { Id = 6, Name="June" },
                    new { Id = 7, Name="July" },
                    new { Id = 8, Name="August" },
                    new { Id = 9, Name="September" },
                    new { Id = 10,Name="October" },
                    new { Id = 11,Name="November" },
                    new { Id = 12,Name="December" },
                }, "Id", "Name");

                ViewBag.Answer = new SelectList(new[]
                {
                    new { Id = 1, Name="Option 1" },
                    new { Id = 2, Name="Option 2" },
                    new { Id = 3, Name="Option 3" },
                    new { Id = 4, Name="Option 4" },
                }, "Id", "Name");
                return View(answer);
            }
            else
            {
                var findQuestion = db.TblQuestionAnswers.Find(Id);
                ViewBag.AccademicYearId = new SelectList(db.TblAccademicYears.Where(p => p.IsDeleted == 0), "Id", "AccademicYear", findQuestion.AccademicYearId);
                ViewBag.SyllabusID = new SelectList(db.TblSyllabus.Where(p => p.IsDeleted == 0 && p.ACID == findQuestion.AccademicYearId), "Id", "Name", findQuestion.SyllabusID);
                ViewBag.ClassID = new SelectList(db.TblGrades.Where(p => p.IsDeleted == 0 && p.SyllabusID == findQuestion.SyllabusID), "Id", "Name", findQuestion.ClassID);
                ViewBag.StreamID = new SelectList(db.TblStreams.Where(p => p.IsDeleted == 0 && p.GradeID == findQuestion.ClassID), "Id", "Name", findQuestion.StreamID);
                ViewBag.SubjectID = new SelectList(db.TblSubjects.Where(p => p.IsDeleted == 0 && p.StreamID == findQuestion.StreamID), "Id", "Name", findQuestion.SubjectID);
                ViewBag.ChapterID = new SelectList(db.TblChapters.Where(p => p.IsDeleted == 0 && p.SubjectID == findQuestion.SubjectID), "Id", "ChapterName", findQuestion.ChapterID);
                ViewBag.QuestionType = new SelectList(new[]
                    {
                        new { Id = 1, Name = "MCQ" },
                        new { Id = 2, Name = "Teacher MCQ" },
                        new { Id = 3, Name = "Question Bank" }
                    }, "Id", "Name", findQuestion.QuestionType);
                ViewBag.Week = new SelectList(new[]
                {
                    new { Id = 1, Name="Week 1" },
                    new { Id = 2, Name="Week 2" },
                    new { Id = 3, Name="Week 3" },
                    new { Id = 4, Name="Week 4" },
                }, "Id", "Name", findQuestion.Week);
                ViewBag.Month = new SelectList(new[]
                {
                    new { Id = 1, Name="January" },
                    new { Id = 2, Name="February" },
                    new { Id = 3, Name="March" },
                    new { Id = 4, Name="April" },
                    new { Id = 5, Name="May" },
                    new { Id = 6, Name="June" },
                    new { Id = 7, Name="July" },
                    new { Id = 8, Name="August" },
                    new { Id = 9, Name="September" },
                    new { Id = 10,Name="October" },
                    new { Id = 11,Name="November" },
                    new { Id = 12,Name="December" },
                }, "Id", "Name", findQuestion.Month);

                ViewBag.Answer = new SelectList(new[]
                {
                    new { Id = 1, Name="Option 1" },
                    new { Id = 2, Name="Option 2" },
                    new { Id = 3, Name="Option 3" },
                    new { Id = 4, Name="Option 4" },
                }, "Id", "Name", findQuestion.Answer);

                return View(findQuestion);
            }

        }


        public ActionResult QuestionList()
        {
            if (TempData["QA_Model"] is QuestionAnswerViewModel model)
            {
                return QuestionList(model); // call POST method logic directly
            }
            QuestionAnswerViewModel questionAnswer = new QuestionAnswerViewModel();

            var acyr = db.TblAccademicYears.Where(p => p.IsDeleted == 0);


            ViewBag.AccademicYearId = new SelectList(acyr, "Id", "AccademicYear");
            if (acyr.ToList().Count == 1)
            {
                ViewBag.SyllabusID = new SelectList(db.TblSyllabus.Where(p => p.IsDeleted == 0 && p.ACID == acyr.FirstOrDefault().Id), "Id", "Name");
            }
            else
            {
                ViewBag.SyllabusID = new SelectList(Enumerable.Empty<string>());
            }
            ViewBag.CreatedBy = new SelectList(db.Users.Where(x => x.IsDeleted == 0 && (x.RoleId==4||x.RoleId==1)).Select(u => new{u.Id,FullName = u.FirstName + " " + u.LastName}),"Id","FullName");
            ViewBag.ClassID = new SelectList(Enumerable.Empty<string>());
            ViewBag.StreamID = new SelectList(Enumerable.Empty<string>());
            ViewBag.SubjectID = new SelectList(Enumerable.Empty<string>());
            ViewBag.ChapterID = new SelectList(Enumerable.Empty<string>());
            ViewBag.QuestionType = new SelectList(new[]
                {
                        new { Id = 1, Name = "MCQ" },
                        new { Id = 2, Name = "Teacher MCQ" },
                        new { Id = 3, Name = "Question Bank" }
                    }, "Id", "Name");
            return View(questionAnswer);
        }
        [HttpPost]
        public ActionResult QuestionList(QuestionAnswerViewModel model)
        {
           // var UserId = db.Users.FirstOrDefault(x => x.UniqueID == User.Identity.Name).Id;
            if (model != null)
            {
                var data = (from question in db.TblQuestionAnswers
                            join accyr in db.TblAccademicYears on question.AccademicYearId equals accyr.Id
                            join syllabus in db.TblSyllabus on question.SyllabusID equals syllabus.Id
                            join grade in db.TblGrades on question.ClassID equals grade.Id
                            join stream in db.TblStreams on question.StreamID equals stream.Id
                            join subject in db.TblSubjects on question.SubjectID equals subject.Id
                            join chapter in db.TblChapters on question.ChapterID equals chapter.Id
                            where question.QuestionType == model.QuestionType && chapter.Id == model.ChapterID
                            && question.CreatedBy == model.CreatedBy && question.IsDeleted == 0
                            select new QuestionAnswerViewModel
                            {
                                AccademicYear = accyr.AccademicYear,
                                AccademicYearId = question.AccademicYearId,
                                Answer = question.Answer,
                                Chapter = chapter.ChapterName,
                                ChapterID = question.ChapterID,
                                Class = grade.Name,
                                ClassID = question.ClassID,
                                Completiondate = question.Completiondate,
                                CompletionStatus = question.CompletionStatus,
                                CreatedBy = question.CreatedBy,
                                CreatedDt = question.CreatedDt,
                                DeletedBy = question.DeletedBy,
                                DeletedDt = question.DeletedDt,
                                ID = question.ID,
                                IsActive = question.IsActive,
                                IsDeleted = question.IsDeleted,
                                Mark = question.Mark,
                                Month = question.Month,
                                Option1 = question.Option1,
                                Option2 = question.Option2,
                                Option3 = question.Option3,
                                Option4 = question.Option4,
                                QImage = question.QImage,
                                Question = question.Question,
                                QuestionType = question.QuestionType,
                                Stream = stream.Name,
                                StreamID = question.StreamID,
                                Subject = subject.Name,
                                SubjectID = question.SubjectID,
                                Syllabus = syllabus.Name,
                                SyllabusID = question.SyllabusID,
                                Week = question.Week,
                                Year = question.Year,
                            }).ToList();
                model.questionAnswerViewModels = data;
                ViewBag.CreatedBy = new SelectList(db.Users.Where(x => x.IsDeleted == 0 && (x.RoleId == 4 || x.RoleId == 1)).Select(u => new { u.Id, FullName = u.FirstName + " " + u.LastName }), "Id", "FullName",model.CreatedBy);

                ViewBag.AccademicYearId = new SelectList(db.TblAccademicYears.Where(p => p.IsDeleted == 0), "Id", "AccademicYear", model.AccademicYearId);
                ViewBag.SyllabusID = new SelectList(db.TblSyllabus.Where(p => p.IsDeleted == 0 && p.ACID == model.AccademicYearId), "Id", "Name", model.SyllabusID);
                ViewBag.ClassID = new SelectList(db.TblGrades.Where(p => p.IsDeleted == 0 && p.SyllabusID == model.SyllabusID), "Id", "Name", model.ClassID);
                ViewBag.StreamID = new SelectList(db.TblStreams.Where(p => p.IsDeleted == 0 && p.GradeID == model.ClassID), "Id", "Name", model.StreamID);
                ViewBag.SubjectID = new SelectList(db.TblSubjects.Where(p => p.IsDeleted == 0 && p.StreamID == model.StreamID), "Id", "Name", model.SubjectID);
                ViewBag.ChapterID = new SelectList(db.TblChapters.Where(p => p.IsDeleted == 0 && p.SubjectID == model.SubjectID), "Id", "ChapterName", model.ChapterID);
                ViewBag.QuestionType = new SelectList(new[]
                    {
                        new { Id = 1, Name = "MCQ" },
                        new { Id = 2, Name = "Teacher MCQ" },
                        new { Id = 3, Name = "Question Bank" }
                    }, "Id", "Name", model.QuestionType);
            }
            else
            {
                return RedirectToAction("QuestionList");
            }
            return View(model);
        }
        public ActionResult DeleteQuestion(int deleteId)
        {
            if (deleteId != 0)
            {
                var findQuestion = db.TblQuestionAnswers.Find(deleteId);
                if (findQuestion != null)
                {
                    findQuestion.IsDeleted = 1;
                    findQuestion.DeletedDt = System.DateTime.Now;
                    db.Entry(findQuestion).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
                if (findQuestion != null)
                {
                    QuestionAnswerViewModel model = new QuestionAnswerViewModel()
                    {
                        AccademicYearId = findQuestion.AccademicYearId,
                        SyllabusID = findQuestion.SyllabusID,
                        ClassID = findQuestion.ClassID,
                        StreamID = findQuestion.StreamID,
                        QuestionType = findQuestion.QuestionType,
                        SubjectID = findQuestion.SubjectID,
                        ChapterID = findQuestion.ChapterID,
                        CreatedBy = findQuestion.CreatedBy
                        
                    };
                    TempData["QA_Model"] = model;
                }
            }
            return RedirectToAction("QuestionList");
        }

    }
}