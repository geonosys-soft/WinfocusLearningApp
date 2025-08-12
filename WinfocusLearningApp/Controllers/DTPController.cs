using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WinfocusLearningApp.Controllers
{
    public class DTPController : Controller
    {
        // GET: DTP
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult QuestionAnswers() { 
        return View();
        }
    }
}