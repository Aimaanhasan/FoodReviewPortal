using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodReview.Models;
using Microsoft.AspNet.Identity;

namespace FoodReview.Controllers
{
    public class RatingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rating
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var ratings = db.Ratings.Include(r => r.ApplicationUser).Include(r => r.Restaurant);
            return View(ratings.ToList());
        }

        // GET: Rating/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ratings ratings = db.Ratings.Find(id);
            if (ratings == null)
            {
                return HttpNotFound();
            }
            return View(ratings);
        }

        // GET: Rating/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email");
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "ID", "Name");
            return View();
        }
        [HttpGet, ActionName("Add")]
        public ActionResult Add()
        {
            return RedirectToAction("Index", "Home");

        }

        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Add(FormCollection form)
        {
            if(form == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var comment = form["Comment"].ToString();
            var articleId = int.Parse(form["ArticleId"]);
            var rating = int.Parse(form["Rating"]);
            //var user = ApplicationUserManager.FindById(User.Identity.GetUserId());
            //var email = user.Email;


            Ratings artComment = new Ratings()

            {
                RestaurantID = articleId,
                Comment = comment,
                RatingNum = rating,
                ApplicationUserID = User.Identity.GetUserId()

                //ThisDateTime = DateTime.Now
            };

            db.Ratings.Add(artComment);
            db.SaveChanges();

            return RedirectToAction("Details", "Restaurant", new { id = articleId });
        }

        // POST: Rating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "RatingsID,RestaurantID,ApplicationUserID,RatingNum,Comment")] Ratings ratings)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(ratings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", ratings.ApplicationUserID);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "ID", "Name", ratings.RestaurantID);
            return View(ratings);
        }

        // GET: Rating/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ratings ratings = db.Ratings.Find(id);
            if (ratings == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", ratings.ApplicationUserID);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "ID", "Name", ratings.RestaurantID);
            return View(ratings);
        }

        // POST: Rating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "RatingsID,RestaurantID,ApplicationUserID,RatingNum,Comment")] Ratings ratings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ratings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", ratings.ApplicationUserID);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "ID", "Name", ratings.RestaurantID);
            return View(ratings);
        }

        // GET: Rating/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ratings ratings = db.Ratings.Find(id);
            if (ratings == null)
            {
                return HttpNotFound();
            }
            return View(ratings);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ratings ratings = db.Ratings.Find(id);
            db.Ratings.Remove(ratings);
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
