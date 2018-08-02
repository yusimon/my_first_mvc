using System.Web.Mvc;

namespace AuthWithDB.Controllers
{
    public class FileController : Controller
    {
        private MainDbContext db = new MainDbContext();
        
        // GET: /File/
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);

            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}