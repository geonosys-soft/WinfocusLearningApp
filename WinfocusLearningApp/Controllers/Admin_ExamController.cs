using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinfocusLearningApp.Authentication;

namespace WinfocusLearningApp.Controllers
{
    //[CustomAuthorize(Roles = "Admin")]
    public class Admin_ExamController : Controller
    {
        // GET: Admin_Exam
        public ActionResult Create_Exam()
        {
            return View();
        }
    }
}