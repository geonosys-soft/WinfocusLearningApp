using System.Web.Mvc;

namespace WinfocusLearningApp.AControllers
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