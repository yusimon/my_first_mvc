using AuthWithDB.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace AuthWithDB.Controllers
{
    public class HomeController : Controller
    {

        public static string date_posted = "";
        public static string time_posted = "";
        public static string check_public = "";
        public static string new_item = "";

        public ActionResult Index()
        {
            var db = new MainDbContext();
            return View(db.Lists.ToList());
        }
        
        // POST: Home
        [HttpPost]
        public ActionResult Index(Lists list)
        {
            if (!String.IsNullOrEmpty(new_item))
            {
                if (ModelState.IsValid)
                {
                    string timeToday = DateTime.Now.ToString("h:mm:ss tt");
                    string dateToday = DateTime.Now.ToString("M/dd/yyyy");

                    using (var db = new MainDbContext())
                    {
                        Claim sessionEmail = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email);
                        string userEmail = sessionEmail.Value;
                        var userIdQuery = db.Users.Where(u => u.Email == userEmail).Select(u => u.Id);
                        var userIds = userIdQuery.ToList();
                        string check_public = Request.Form["check_public"];
                        string new_item = Request.Form["new_item"];

                        var dbList = db.Lists.Create();
                        dbList.Details = new_item;
                        dbList.Date_Posted = dateToday;
                        dbList.Time_Posted = timeToday;
                        dbList.User_Id = userIds[0];
                        if (check_public != null) { dbList.Public = "YES"; }
                        else { dbList.Public = "NO"; }
                        db.Lists.Add(dbList);
                        db.SaveChanges();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect form data input");
                }
            }

            ModelState.AddModelError("Error", "New item cannot be empty!");
            var listTable = new MainDbContext();
            return View(listTable.Lists.ToList());
        }

        // Edit the row item
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new MainDbContext();
            var model = new Lists();
            model = db.Lists.Find(id);
            //date_posted = model.Date_Posted;
            //time_posted = model.Time_Posted;
            //check_public = model.Public;
            return View(model);
        }

        // Overload Edit
        [HttpPost]
        public ActionResult Edit(Lists list)
        {
            var db = new MainDbContext();
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            //string new_item = Request.Form["new_item"];
            //string check_public = Request.Form["check_public"];

            if (ModelState.IsValid)
            {
                list.Time_Posted = timeToday;
                list.Date_Posted = dateToday;
                //list.Details = new_item;
                //if (check_public != null) { list.Public = "YES"; }
                //else { list.Public = "NO"; }
                //list.Public = check_public;

                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(list);
        }

        // Delete row
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new MainDbContext();

            var model = db.Lists.Find(id);

            if(model == null)
            {
                return HttpNotFound();
            }

            db.Lists.Remove(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}