using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodReview.Models;
using PagedList;

namespace FoodReview.Controllers
{
    public class RestaurantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Restaurant
        [OutputCache(Duration = 60)]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var students = from s in db.Restaurants
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.Type.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;

                case "type_desc":
                    students = students.OrderByDescending(s => s.Type);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
          //  return View(students.ToPagedList(pageNumber, pageSize));

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        
        // GET: Restaurant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurants article = db.Restaurants.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleId = id.Value;
            

            var comments = db.Ratings.Where(d => d.RestaurantID.Equals(id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = db.Ratings.Where(d => d.RestaurantID.Equals(id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.RatingNum);
                System.Diagnostics.Debug.WriteLine(ratingSum);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(article);
        }

        

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Type,isApproved")] Restaurants restaurants, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    restaurants.Files = new List<File> { avatar };
                }
                db.Restaurants.Add(restaurants);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurants);
        }
        [OutputCache(Duration = 60)]
        // GET: Restaurant/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurants restaurants = db.Restaurants.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            if (restaurants == null)
            {
                return HttpNotFound();
            }
            return View(restaurants);
        }

        // POST: Restaurant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit(int id, [Bind(Include = "ID,Name,Type,isApproved")] Restaurants restaurants, HttpPostedFileBase upload)
         {
             var studentToUpdate = db.Restaurants.Find(id);
             if (ModelState.IsValid)
             {
                 if (upload != null && upload.ContentLength > 0)
                 {

                     if (studentToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
                     {
                         db.Files.Remove(studentToUpdate.Files.First(f => f.FileType == FileType.Avatar));
                     }
                     var avatar = new File
                     {
                         FileName = System.IO.Path.GetFileName(upload.FileName),
                         FileType = FileType.Avatar,
                         ContentType = upload.ContentType
                     };
                     using (var reader = new System.IO.BinaryReader(upload.InputStream))
                     {
                         avatar.Content = reader.ReadBytes(upload.ContentLength);
                     }
                     studentToUpdate.Files = new List<File> { avatar };
                 }

                 db.Entry(studentToUpdate).State = EntityState.Modified;
                 db.SaveChanges();
                 db.Entry(restaurants).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");

             }
             return View(studentToUpdate);
         }*/
        [OutputCache(Duration = 60)]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.Restaurants.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
                new string[] { "Name", "Type", "isApproved" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (studentToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(studentToUpdate.Files.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        studentToUpdate.Files = new List<File> { avatar };
                    }
                    db.Entry(studentToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        /*[HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload,[Bind(Include = "ID,Name,Type,isApproved")]  Restaurants restaurants)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.Restaurants.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
                new string[] { "Name,Type,isApproved" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (studentToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(studentToUpdate.Files.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        studentToUpdate.Files = new List<File> { avatar };
                    }
                    db.Entry(studentToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException dex)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                if(ModelState.IsValid)
                {
                    db.Entry(restaurants).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

            }
            return View(studentToUpdate);
        }*/

        // GET: Restaurant/Delete/5
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurants restaurants = db.Restaurants.Find(id);
            if (restaurants == null)
            {
                return HttpNotFound();
            }
            return View(restaurants);
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurants restaurants = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurants);
            db.SaveChanges();
            return RedirectToAction("Index");
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
