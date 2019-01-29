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
    public class UO_AnswerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        //private int? DetalleID;
        // GET: UO_Answer
        [AllowAnonymous]
        public ActionResult Index()
        {
            var answer = db.Answer.Include(u => u.UO_Question);
            return View(answer.ToList());
        }

        // GET: UO_Answer/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Answer uO_Answer = db.Answer.Find(id);
            //DetalleID = id;
            if (uO_Answer == null)
            {
                return HttpNotFound();
            }
            return View(uO_Answer);
        }

        // GET: UO_Answer/Create
        [Authorize]
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
        [Authorize]
        public ActionResult Create([Bind(Include = "UO_AnswerID,UO_QuestionID,UO_UserID,UO_CategoryID,TopicAnswer,DescriptionAnswer")] UO_Answer uO_Answer)
        {
            UO_QuestionController u = new UO_QuestionController();

            if (ModelState.IsValid)
            {
                ViewData["Det"] = System.Web.HttpContext.Current.Session["Det"] as String;

                //UO_Question uQ = db.Question.Find();
                //UO_Question uQ = new UO_Question();
                //uQ.
                var i = Session["Detail"];
                UO_Question uQ = db.Question.Find(i);
                //uO_Answer.UO_QuestionID = uQ.UO_QuestionID;
                //UO_Category uC = db.Category.Find(uQ.UO_CategoryID);
                ////UO_Answer uA = db.Answer.Find(DetalleID);
                ////UO_Question uQ = db.Question.Find(uA.UO_QuestionID);
                ////UO_Category uC = db.Category.Find(uQ.UO_CategoryID);
                uO_Answer.UO_CategoryID = uQ.UO_CategoryID;
                uO_Answer.UO_QuestionID = uQ.UO_QuestionID; 
                //uO_Answer.UO_QuestionID = ;
                uO_Answer.UO_UserID = User.Identity.GetUserId();
                db.Answer.Add(uO_Answer);
                db.SaveChanges();
                return RedirectToAction("Index", "UO_Question");
            }

            ViewBag.UO_QuestionID = new SelectList(db.Question, "UO_QuestionID", "TitleQuestion", uO_Answer.UO_QuestionID);
            return View(uO_Answer);
        }

        // GET: UO_Answer/Edit/5
        [Authorize]
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
        [Authorize]
        public ActionResult Edit([Bind(Include = "UO_AnswerID,UO_QuestionID,UO_UserID,UO_CategoryID,TopicAnswer,DescriptionAnswer")] UO_Answer uO_Answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uO_Answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
            ViewBag.UO_QuestionID = new SelectList(db.Question, "UO_QuestionID", "TitleQuestion", uO_Answer.UO_QuestionID);
            return View(uO_Answer);
        }

        // GET: UO_Answer/Delete/5
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            UO_Answer uO_Answer = db.Answer.Find(id);
            db.Answer.Remove(uO_Answer);
            db.SaveChanges();
            return RedirectToAction("Index","Manage");
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

        public PartialViewResult _UserAnswers(string Userid)
        {
            var order = from c in db.Answer
                        where c.UO_UserID == Userid
                        select c; //linq
            ViewBag.AspNetUsers = Userid;//hace las veces de variable temporal
            return PartialView(order.ToList());
        }

        //public ActionResult Vote(int id)
        //{
        //    UO_Answer uO_Answer = db.Answer.Find(id);
        //    uO_Answer.RateAnswer = uO_Answer.RateAnswer++;
        //    uO_Answer.UO_AnswerID = uO_Answer.UO_AnswerID;
        //    uO_Answer.UO_QuestionID = uO_Answer.UO_QuestionID;
        //    uO_Answer.UO_UserID = uO_Answer.UO_UserID;
        //    uO_Answer.UO_CategoryID = uO_Answer.UO_CategoryID;
        //    uO_Answer.TopicAnswer = uO_Answer.TopicAnswer;
        //    uO_Answer.DescriptionAnswer = uO_Answer.DescriptionAnswer;
        //    uO_Answer.ApplicationUsers = uO_Answer.ApplicationUsers;
        //    db.Entry(uO_Answer).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return Redirect(Request.UrlReferrer.ToString());
        //}

        // GET: UO_Answer/Edit/5
        public ActionResult Vote (int? id)
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
            return RedirectToAction("Index", "Manage");
        }

        // POST: UO_Answer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote([Bind(Include = "UO_AnswerID,UO_QuestionID,UO_UserID,UO_CategoryID,TopicAnswer,DescriptionAnswer,RateAnswer")] UO_Answer uO_Answer)
        {
            if (ModelState.IsValid)
            {
                uO_Answer.RateAnswer = uO_Answer.RateAnswer++;
                db.Entry(uO_Answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Manage");
            }
            ViewBag.UO_QuestionID = new SelectList(db.Question, "UO_QuestionID", "TitleQuestion", uO_Answer.UO_QuestionID);
            return RedirectToAction("Index", "Manage");
        }
    }
}
