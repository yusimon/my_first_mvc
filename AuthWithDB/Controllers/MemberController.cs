using System.Web.Mvc;
using System.Linq;

namespace AuthWithDB.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            var db = new MainDbContext();
            return View();
        }

        // Create Member
        public ActionResult Create()
        {
            return View();
        }

        
    }
}