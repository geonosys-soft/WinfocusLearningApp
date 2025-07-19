using System.Web.Mvc;

namespace WinfocusLearningApp.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            // Add logic to fetch dashboard data from your database
            return View();
        }
        
    }
}