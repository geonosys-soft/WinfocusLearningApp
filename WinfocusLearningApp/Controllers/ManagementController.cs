using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WinfocusLearningApp.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Admin/Management
        public ActionResult Index()
        {
            // You can redirect to the first item or a management dashboard
            return RedirectToAction("AllUsers");
        }

        public ActionResult AllUsers() { 
        //code updated
            return View();
        }

        public ActionResult CreateUsers() { return View(); }
        public ActionResult AcademicYear() { return View(); }
        public ActionResult ClassManagement() { return View(); }
        public ActionResult Stream() { return View(); }
        public ActionResult Course() { return View(); }
        public ActionResult Subject() { return View(); }
        public ActionResult Chapter() { return View(); }
        public ActionResult SubChapter() { return View(); }
        public ActionResult Module() { return View(); }
        public ActionResult NoteType() { return View(); }
        public ActionResult SchoolState() { return View(); }
        public ActionResult District() { return View(); }
        public ActionResult SchoolDetails() { return View(); }
        public ActionResult CreateGroup() { return View(); }
        public ActionResult ManageGroup() { return View(); }
        public ActionResult CourseFee() { return View(); }
        public ActionResult RequestCallBack() { return View(); }
        public ActionResult StudentReference() { return View(); }
        public ActionResult CreateLessonSummary() { return View(); }
        public ActionResult SyllabusCreation() { return View(); }
        public ActionResult AddMeeting() { return View(); }
        public ActionResult TimeTable() { return View(); }
        public ActionResult Blog() { return View(); }
        public ActionResult SupportTicket() { return View(); }
        public ActionResult Complaints() { return View(); }
    }
}