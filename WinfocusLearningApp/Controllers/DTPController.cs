using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinfocusLearningApp.Authentication;
using WinfocusLearningApp.DataEntity;
using WinfocusLearningApp.ViewModels;

namespace WinfocusLearningApp.Controllers
{
   // [CustomAuthorize(Roles = "DTP")]
    public class DTPController : Controller
    {
        Winfocus_CS db=new Winfocus_CS();

        // GET: DTP
        public ActionResult Index()
        {
            return View();
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
                ViewBag.AccademicYearId = new SelectList(db.TblAccademicYears.Where(p => p.IsDeleted == 0), "Id", "AccademicYear",findQuestion.AccademicYearId);
                ViewBag.SyllabusID = new SelectList(db.TblSyllabus.Where(p=>p.IsDeleted==0 && p.ACID==findQuestion.AccademicYearId),"Id","Name",findQuestion.SyllabusID);
                ViewBag.ClassID = new SelectList(db.TblGrades.Where(p => p.IsDeleted == 0 && p.SyllabusID==findQuestion.SyllabusID), "Id", "Name", findQuestion.ClassID);
                ViewBag.StreamID = new SelectList(db.TblStreams.Where(p => p.IsDeleted == 0 && p.GradeID==findQuestion.ClassID), "Id", "Name", findQuestion.StreamID);
                ViewBag.SubjectID = new SelectList(db.TblSubjects.Where(p => p.IsDeleted == 0 && p.StreamID==findQuestion.StreamID), "Id", "Name", findQuestion.SubjectID);
                ViewBag.ChapterID = new SelectList(db.TblChapters.Where(p => p.IsDeleted == 0 && p. SubjectID== findQuestion.SubjectID),"Id", "ChapterName", findQuestion.ChapterID);
                ViewBag.QuestionType = new SelectList(new[]
                    {
                        new { Id = 1, Name = "MCQ" },
                        new { Id = 2, Name = "Teacher MCQ" },
                        new { Id = 3, Name = "Question Bank" }
                    }, "Id", "Name",findQuestion.QuestionType);
                ViewBag.Week = new SelectList(new[]
                {
                    new { Id = 1, Name="Week 1" },
                    new { Id = 2, Name="Week 2" },
                    new { Id = 3, Name="Week 3" },
                    new { Id = 4, Name="Week 4" },
                }, "Id", "Name",findQuestion.Week);
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
                }, "Id", "Name",findQuestion.Month);

                ViewBag.Answer = new SelectList(new[]
                {
                    new { Id = 1, Name="Option 1" },
                    new { Id = 2, Name="Option 2" },
                    new { Id = 3, Name="Option 3" },
                    new { Id = 4, Name="Option 4" },
                }, "Id", "Name",findQuestion.Answer);

                return View(findQuestion);
            }
            
        }
       

        public ActionResult QuestionList()
        {
            TblQuestionAnswer questionAnswer = new TblQuestionAnswer();

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
            return View(questionAnswer);
        }
        [HttpPost]
        public ActionResult QuestionList(QuestionAnswerViewModel model)
        {
            var UserId=db.Users.FirstOrDefault(x=>x.UniqueID==User.Identity.Name).Id;
            if (model == null)
            {
                var data = (from question in db.TblQuestionAnswers
                            join accyr in db.TblAccademicYears on question.AccademicYearId equals accyr.Id
                            join syllabus in db.TblSyllabus on question.SyllabusID equals syllabus.Id
                            join grade in db.TblGrades on question.ClassID equals grade.Id
                            join stream in db.TblStreams on question.StreamID equals stream.Id
                            join subject in db.TblSubjects on question.SubjectID equals subject.Id
                            join chapter in db.TblChapters on question.ChapterID equals chapter.Id
                            where question.QuestionType==model.QuestionType && chapter.Id==model.ChapterID
                            && question.CreatedBy==UserId
                            select new QuestionAnswerViewModel
                            {
                                AccademicYear=accyr.AccademicYear,
                                AccademicYearId=question.AccademicYearId,
                                Answer=question.Answer,
                                Chapter=chapter.ChapterName,
                                ChapterID=question.ChapterID,
                                Class=grade.Name,
                                ClassID=question.ClassID,
                                Completiondate=question.Completiondate,
                                CompletionStatus=question.CompletionStatus,
                                CreatedBy=question.CreatedBy,
                                CreatedDt=question.CreatedDt,
                                DeletedBy=question.DeletedBy,
                                DeletedDt=question.DeletedDt,
                                ID=question.ID,
                                IsActive=question.IsActive,
                                IsDeleted=question.IsDeleted,
                                Mark = question.Mark,
                                Month=question.Month,
                                Option1 = question.Option1,
                                Option2 = question.Option2,
                                Option3 = question.Option3,
                                Option4 = question.Option4,
                                QImage=question.QImage,
                                Question = question.Question,
                                QuestionType=question.QuestionType,
                                Stream=stream.Name,
                                StreamID=question.StreamID,
                                Subject=subject.Name,
                                SubjectID = question.SubjectID,
                                Syllabus=syllabus.Name,
                                SyllabusID = question.SyllabusID,
                                Week=question.Week,
                                Year=question.Year,
                            }).ToList();
                model.questionAnswerViewModels = data;
            }
            else
            {
                return View();
            }
            return View(model);
        }
        public ActionResult SQuestionList()
        {
            return View();
        }
        public ActionResult QAReport()
        {
            return View();
        }
        public ActionResult SQAReport()
        {
            return View();
        }
        public ActionResult Dashboard()
        {

            return View();
        }
    }
}