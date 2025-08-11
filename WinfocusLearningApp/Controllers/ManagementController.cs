using Newtonsoft.Json.Linq;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using System.Web.Mvc;
using WinfocusLearningApp.Authentication;
using WinfocusLearningApp.DataEntity;
using WinfocusLearningApp.ViewModels;


namespace WinfocusLearningApp.Controllers
{ 
    //[CustomAuthorize(Roles = "Admin")]
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
        public ActionResult TargetyearExam(int? id) { 
            TargetYearExamViewModel model = new TargetYearExamViewModel();
            if (id!=null)
            {
                var targetYearExam = dbEntities.TblTargetYears.Find(id);
                ViewBag.TargetExamID = new SelectList(dbEntities.TblTargetExams.Where(p => p.IsDeleted == 0), "ID", "TargetExam",targetYearExam.TargetExamID);
                model.ID = targetYearExam.ID;
                model.TargetYear = targetYearExam.TargetYear;
            }
            else
            {
                ViewBag.TargetExamID = new SelectList(dbEntities.TblTargetExams.Where(p => p.IsDeleted == 0), "ID", "TargetExam");
                
            }
            var data = (from TYre in dbEntities.TblTargetYears
                        join TExm in dbEntities.TblTargetExams on TYre.TargetExamID equals TExm.ID
                        where TYre.IsDelete == 0
                        select new TargetYearExamViewModel
                        {
                            ID = TYre.ID,
                            TargetExam = TExm.TargetExam,
                            TargetExamID = TYre.TargetExamID,
                            TargetYear = TYre.TargetYear,
                            IsDelete = TYre.IsDelete,
                            CreatedDt = TYre.CreatedDt,
                            DeletedDt = TYre.DeletedDt,
                            IsActive = TYre.IsActive
                        }).ToList();
            model.Exam = data;
            if (TempData["SuccessMessage"]!= null)
            {
                ViewBag.Success = TempData["SuccessMessage"];
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.Error = TempData["ErrorMessage"];
            }
            return View(model); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TargetYearExam(TargetYearExamViewModel model) {
            if (ModelState.IsValid)
            {
                if(model.ID!=0)
                {
                    var targetyearExam = dbEntities.TblTargetYears.Find(model.ID);
                    targetyearExam.TargetYear = model.TargetYear;
                    targetyearExam.TargetExamID = model.TargetExamID;
                    dbEntities.Entry(targetyearExam).State = System.Data.Entity.EntityState.Modified;
                    if (dbEntities.SaveChanges() > 0)
                    {
                        TempData["SuccessMessage"] = "Target Year Exam updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to update Target Year Exam.";
                    }
                }
                else
                {

                    TblTargetYear tblTargetYear = new TblTargetYear()
                    {
                        CreatedDt = DateTime.UtcNow,
                        IsActive = 1,
                        IsDelete = 0,
                        TargetYear = model.TargetYear,
                        TargetExamID = model.TargetExamID
                    };
                    dbEntities.TblTargetYears.Add(tblTargetYear);
                    if (dbEntities.SaveChanges() > 0)
                    {
                        TempData["SuccessMessage"] = "Target Year Exam created successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to create Target Year Exam.";
                    }
                }

                    return RedirectToAction("TargetyearExam","Management");
            }
            ViewBag.TargetExamID = new SelectList(dbEntities.TblTargetExams.Where(p => p.IsDeleted == 0), "ID", "TargetExam");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult deleteTargetExamYear(int? deleteId) {
            var targetYearExam = dbEntities.TblTargetYears.Find(deleteId);
            if (targetYearExam != null)
            {
                targetYearExam.IsDelete = 1;
                targetYearExam.DeletedDt = DateTime.UtcNow;
                dbEntities.Entry(targetYearExam).State = System.Data.Entity.EntityState.Modified;
                if (dbEntities.SaveChanges() > 0)
                {
                    TempData["SuccessMessage"] = "Target Year Exam deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete Target Year Exam.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Target Year Exam not found.";
            }
            return RedirectToAction("TargetyearExam", "Management");
        }
        public ActionResult Dashboard() {
            return View();
        }
    }
}