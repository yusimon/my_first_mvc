using AuthWithDB.CustomLibraries;
using AuthWithDB.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AuthWithDB.Controllers
{
    public class NewUserController : Controller
    {
        // GET: NewUser
        public ActionResult Index()
        {
            var db = new MainDbContext();
            return View(db.NewUsers.ToList());
        }

        // Create user
        public ActionResult Create()
        {
            return View();
        }

        // Post user
        [HttpPost]
        public ActionResult Create(NewUser newUser)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    string dateEdited = DateTime.Now.ToString("yyyy-MM-dd");
                    var encryptedPassword = CustomEncrypt.Encrypt(newUser.Password);
                    var user = db.NewUsers.Create();
                    user.FirstName = newUser.FirstName;
                    user.LastName = newUser.LastName;
                    user.Email = newUser.Email;
                    user.Password = encryptedPassword;
                    user.DateEdited = dateEdited;
                    db.NewUsers.Add(user);
                    db.SaveChanges();
                }

            }else
            {
                ModelState.AddModelError("", "Missing some field(s) value");
            }
            
            return RedirectToAction("Index");
        }
    }
}