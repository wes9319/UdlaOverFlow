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

namespace UdlaOverflow.Controllers
{
    public class UO_AnswerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UO_Answer
        public ActionResult Index()
        {
            var answer = db.Answer.Include(u => u.UO_Question);
            return View(answer.ToList());
        }

        // GET: UO_Answer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Answer uO_Answer = db.Answer.Find(id);
            if (uO_Answer == null)
            {
                return HttpNotFound();
            }
            return View(uO_Answer);
        }

        // GET: UO_Answer/Create
        public ActionResult Create()
        {
            ViewBag.UO_QuestionID = new SelectList(db.Question, "UO_QuestionID", "TitleQuestion");
            return View();
        }

        // POST: UO_Answer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UO_AnswerID,UO_QuestionID,UO_UserID,UO_CategoryID,TopicAnswer,DescriptionAnswer")] UO_Answer uO_Answer)
        {
            if (ModelState.IsValid)
            {
                uO_Answer.UO_UserID = User.Identity.GetUserId();
                db.Answer.Add(uO_Answer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UO_QuestionID = new SelectList(db.Question, "UO_QuestionID", "TitleQuestion", uO_Answer.UO_QuestionID);
            return View(uO_Answer);
        }

        // GET: UO_Answer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Answer uO_Answer = db.Answer.Find(id);
            if (uO_Answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.returnUrl = Request.UrlReferrer;
            ViewBag.UO_QuestionID = new SelectList(db.Question, "UO_QuestionID", "TitleQuestion", uO_Answer.UO_QuestionID);
            return View(uO_Answer);
        }

        // POST: UO_Answer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UO_AnswerID,UO_QuestionID,UO_UserID,UO_CategoryID,TopicAnswer,DescriptionAnswer")] UO_Answer uO_Answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uO_Answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UO_QuestionID = new SelectList(db.Question, "UO_QuestionID", "TitleQuestion", uO_Answer.UO_QuestionID);
            return View(uO_Answer);
        }

        // GET: UO_Answer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Answer uO_Answer = db.Answer.Find(id);
            if (uO_Answer == null)
            {
                return HttpNotFound();
            }
            return View(uO_Answer);
        }

        // POST: UO_Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UO_Answer uO_Answer = db.Answer.Find(id);
            db.Answer.Remove(uO_Answer);
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

        public PartialViewResult _AnswersForQuestion(int Questionid)
        {
            var order = from c in db.Answer
                        where c.UO_QuestionID == Questionid
                        select c; //linq
            ViewBag.UO_Questions = Questionid;//hace las veces de variable temporal
            return PartialView(order.ToList());
        }
    }
}
