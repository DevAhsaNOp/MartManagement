using MartManagement.BLL.Repositories;
using System.Web.Mvc;

namespace MartManagement.WebApp.Controllers
{
    public class HomeController : Controller
    {
        CustomerRepo RepoObj;

        public HomeController()
        {
            RepoObj = new CustomerRepo();
        }

        public ActionResult Dashboard()
        {
            var dashboardDetails = RepoObj.GetDashboardDetails();
            return View(dashboardDetails);
        }
    }
}