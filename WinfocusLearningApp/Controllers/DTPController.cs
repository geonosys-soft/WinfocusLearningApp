using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.Controllers
{
    public class DTPController : Controller
    {
        Winfocus_CS db=new Winfocus_CS();

        // GET: DTP
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult QuestionAnswers(int? Id)
        {
            TblQuestionAnswer answer = new TblQuestionAnswer();
            if (Id == null)
            {
              
                ViewBag.AccademicYearId = new SelectList(db.TblAccademicYears.Where(p => p.IsDeleted == 0), "Id", "AccademicYear");
                ViewBag.SyllabusID = new SelectList(Enumerable.Empty<string>());
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
                ViewBag.Week=new SelectList(new[] 
                { 
                    new { Id = 1, Name="Week 1" },
                    new { Id = 2, Name="Week 2" },
                    new { Id = 3, Name="Week 3" },
                    new { Id = 4, Name="Week 4" },
                }, "Id","Name");
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
            }
            else
            {
            }

        public ActionResult QuestionList()
        {
            return View();
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