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
    public class InterviewRoomController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /InterviewRoom/
        public ActionResult Index()
        {
            return View(db.InterviewRooms.ToList());
        }

        // GET: /InterviewRoom/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewRoom interviewroom = db.InterviewRooms.Find(id);
            if (interviewroom == null)
            {
                return HttpNotFound();
            }
            return View(interviewroom);
        }

        // GET: /InterviewRoom/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /InterviewRoom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="InterviewRoomID,Rooms")] InterviewRoom interviewroom)
        {
            if (ModelState.IsValid)
            {
                db.InterviewRooms.Add(interviewroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interviewroom);
        }

        // GET: /InterviewRoom/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewRoom interviewroom = db.InterviewRooms.Find(id);
            if (interviewroom == null)
            {
                return HttpNotFound();
            }
            return View(interviewroom);
        }

        // POST: /InterviewRoom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="InterviewRoomID,Rooms")] InterviewRoom interviewroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interviewroom);
        }

        // GET: /InterviewRoom/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewRoom interviewroom = db.InterviewRooms.Find(id);
            if (interviewroom == null)
            {
                return HttpNotFound();
            }
            return View(interviewroom);
        }

        // POST: /InterviewRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterviewRoom interviewroom = db.InterviewRooms.Find(id);
            db.InterviewRooms.Remove(interviewroom);
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
