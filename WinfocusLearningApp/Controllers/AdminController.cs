using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinfocusLearningApp.DataEntity;
using WinfocusLearningApp.Models;

namespace WinfocusLearningApp.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly Winfocus_CS dbEntities=new Winfocus_CS();
        private static TimeZoneInfo INDIANTIME = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccademicYear()
        {
            return View();
        }
        public ActionResult StudyMaterialType(int? id)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIANTIME);
            if (TempData["StatusMessage"] != null)
            {
                ViewBag.StatusMessage = TempData["StatusMessage"].ToString();
            }

            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            TblMaterialModel inf = new TblMaterialModel();

            if (id == null)
            {



                //  ViewBag.AccademicYear = new SelectList(db.Programmes.Where(p => p.IsDeleted == 0), "Id", "Name");
                ViewBag.ACID = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.SyllabusID = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.GradeID = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.StreamID = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.CourseID = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.SubjectId = new SelectList(Enumerable.Empty<SelectListItem>());

            }
            else
            {
                ViewBag.ACID = new SelectList(Enumerable.Empty<SelectListItem>());
                // inf = db.TblMaterials.Find(id);
                //   ViewBag.ProgramId = new SelectList(db.Programmes.Where(p => p.IsDeleted == 0), "Id", "Name", inf.ProgramId);


            }

            return View(inf);
        }

    }
}