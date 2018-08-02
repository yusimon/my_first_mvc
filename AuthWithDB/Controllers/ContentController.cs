using AuthWithDB.Models;
using AuthWithDB.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UploadAndDisplayImageInMvc.Repositories;

namespace AuthWithDB.Controllers
{
    public class ContentController : Controller
    {
        private MainDbContext db = new MainDbContext();
        // GET: Content
        public ActionResult ContentIndex()
        {
            var content = db.Contents.Select(s => new
            {
                s.ID,
                s.Title,
                s.Image,
                s.Description,
                s.Contents
            });
            List<ContentViewModel> contentViewModel =
                content.Select(item => new ContentViewModel()
                {
                    ID = item.ID,
                    Title = item.Title,
                    Image = item.Image,
                    Description = item.Description,
                    Contents = item.Contents
                }).ToList();

            return View(contentViewModel);
        }

        /// <summary>
        /// Retrive Image from database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Contents where temp.ID == Id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST Create
        [HttpPost]
        public ActionResult Create(ContentViewModel model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            ContentRepository repos = new ContentRepository();
            int i = repos.UploadImageInDataBase(file, model);
            if (i == 1)
            {
                return RedirectToAction("ContentIndex");
            }
            return View(model);
        }

        // GET: Edit content
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var content = db.Contents.SingleOrDefault(c => c.ID == id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: Edit Content
        [HttpPost, ValidateInput(false)]
        //[HttpPost]
        public ActionResult Edit(int? id, HttpPostedFileBase ImageData)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contentToUpdate = db.Contents.Find(id);
            if (TryUpdateModel(contentToUpdate, "",
               new string[] { "Title", "Description", "Contents" }))
            {
                try
                {
                    if (ImageData != null && ImageData.ContentLength > 0)
                    {
                        db.Contents.Remove(db.Contents.Find(id));

                        using (var reader = new System.IO.BinaryReader(ImageData.InputStream))
                        {
                            contentToUpdate.Image = reader.ReadBytes(ImageData.ContentLength);
                        }
                    }
                    
                    db.Entry(contentToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("ContentIndex");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(contentToUpdate);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.Message = "Delete failed. Please try again later.";
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {
                Content content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException /* dex */)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = false});
            }
            return RedirectToAction("ContentIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}