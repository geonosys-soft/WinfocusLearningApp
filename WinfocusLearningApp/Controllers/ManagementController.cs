using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WinfocusLearningApp.Authentication;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.Controllers
{ 
    [CustomAuthorize(Roles = "Admin")]
    public class ManagementController : Controller
    {
       
        private readonly Winfocus_CS dbEntities = new Winfocus_CS();
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
        public ActionResult AcademicYear(int?id) { 

            TblAccademicYear inf = new TblAccademicYear();
            if (id != null)
            {
                inf = dbEntities.TblAccademicYears.Find(id);
            }
            else
            {
                
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
            }
            return View(inf); 
        }
        

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
        public ActionResult MaterialType() { return View(); }
        public ActionResult Batch() { return View(); }
        public ActionResult PreferredTimeSlot() { return View(); }
        public ActionResult St_Registration() { return View(); }
        public ActionResult BDE_Registration() { return View(); }
        public ActionResult FeesDetails() { return View(); }
        public ActionResult Dashboard() {
            return View();
        }
    }
}