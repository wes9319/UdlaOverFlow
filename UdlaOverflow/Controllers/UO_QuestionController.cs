using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UdlaOverflow.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UdlaOverflow.Controllers;

namespace UdlaOverflow.Controllers
{
    
    public class UO_QuestionController : Controller
    {
        //public static int? Det { get; set; }
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UO_Question
        public ActionResult Index()
        {
            var question = db.Question.Include(u => u.UO_Category);
            return View(question.ToList());
        }

        // GET: UO_Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Question uO_Question = db.Question.Find(id);
            Session["Detail"] = id;
            if (uO_Question == null)
            {
                return HttpNotFound();
            }
            return View(uO_Question);
        }

        // GET: UO_Question/Create
        public ActionResult Create()
        {
            ViewBag.UO_CategoryID = new SelectList(db.Category, "UO_CategoryID", "DescriptionCategory");
            return View();
        }

        // POST: UO_Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UO_QuestionID,UO_UserID,UO_CategoryID,TitleQuestion,DescriptionQuestion,DateQuestion")] UO_Question uO_Question)
        {
            if (ModelState.IsValid)
            {
                
                uO_Question.UO_UserID = User.Identity.GetUserId();
                uO_Question.DateQuestion = DateTime.Now;
                db.Question.Add(uO_Question);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }

            ViewBag.UO_CategoryID = new SelectList(db.Category, "UO_CategoryID", "DescriptionCategory", uO_Question.UO_CategoryID);
            return View(uO_Question);
        }

        // GET: UO_Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Question uO_Question = db.Question.Find(id);
            if (uO_Question == null)
            {
                return HttpNotFound();
            }
            ViewBag.UO_CategoryID = new SelectList(db.Category, "UO_CategoryID", "DescriptionCategory", uO_Question.UO_CategoryID);
            return View(uO_Question);
        }

        // POST: UO_Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UO_QuestionID,UO_UserID,UO_CategoryID,TitleQuestion,DescriptionQuestion,DateQuestion")] UO_Question uO_Question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uO_Question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
            ViewBag.UO_CategoryID = new SelectList(db.Category, "UO_CategoryID", "DescriptionCategory", uO_Question.UO_CategoryID);
            return View(uO_Question);
        }

        // GET: UO_Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Question uO_Question = db.Question.Find(id);
            if (uO_Question == null)
            {
                return HttpNotFound();
            }
            return View(uO_Question);
        }

        // POST: UO_Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UO_Question uO_Question = db.Question.Find(id);
            db.Question.Remove(uO_Question);
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public PartialViewResult _UserQuestions(string Userid)
        {
            var order = from c in db.Question
                        where c.UO_UserID == Userid
                        select c; //linq
            ViewBag.AspNetUsers = Userid;//hace las veces de variable temporal
            return PartialView(order.ToList());
        }
    }
}
