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
    public class UO_CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UO_Category
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }

        // GET: UO_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Category uO_Category = db.Category.Find(id);
            if (uO_Category == null)
            {
                return HttpNotFound();
            }
            return View(uO_Category);
        }

        // GET: UO_Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UO_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UO_CategoryID,DescriptionCategory")] UO_Category uO_Category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(uO_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uO_Category);
        }

        // GET: UO_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Category uO_Category = db.Category.Find(id);
            if (uO_Category == null)
            {
                return HttpNotFound();
            }
            return View(uO_Category);
        }

        // POST: UO_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UO_CategoryID,DescriptionCategory")] UO_Category uO_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uO_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uO_Category);
        }

        // GET: UO_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UO_Category uO_Category = db.Category.Find(id);
            if (uO_Category == null)
            {
                return HttpNotFound();
            }
            return View(uO_Category);
        }

        // POST: UO_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UO_Category uO_Category = db.Category.Find(id);
            db.Category.Remove(uO_Category);
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
