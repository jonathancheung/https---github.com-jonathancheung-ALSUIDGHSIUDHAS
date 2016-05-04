using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team14_Final_Project.Models;

namespace Team14_Final_Project.Controllers
{
    public class InterviewTimesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /InterviewTimes/
        public ActionResult Index()
        {
            return View(db.InterviewTimes.ToList());
        }

        // GET: /InterviewTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewTimes interviewtimes = db.InterviewTimes.Find(id);
            if (interviewtimes == null)
            {
                return HttpNotFound();
            }
            return View(interviewtimes);
        }

        // GET: /InterviewTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /InterviewTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="InterviewTimesID")] InterviewTimes interviewtimes)
        {
            if (ModelState.IsValid)
            {
                db.InterviewTimes.Add(interviewtimes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interviewtimes);
        }

        // GET: /InterviewTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewTimes interviewtimes = db.InterviewTimes.Find(id);
            if (interviewtimes == null)
            {
                return HttpNotFound();
            }
            return View(interviewtimes);
        }

        // POST: /InterviewTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="InterviewTimesID")] InterviewTimes interviewtimes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewtimes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interviewtimes);
        }

        // GET: /InterviewTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewTimes interviewtimes = db.InterviewTimes.Find(id);
            if (interviewtimes == null)
            {
                return HttpNotFound();
            }
            return View(interviewtimes);
        }

        // POST: /InterviewTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterviewTimes interviewtimes = db.InterviewTimes.Find(id);
            db.InterviewTimes.Remove(interviewtimes);
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
