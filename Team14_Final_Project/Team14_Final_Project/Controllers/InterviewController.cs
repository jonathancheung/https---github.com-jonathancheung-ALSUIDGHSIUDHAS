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
    public class InterviewController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /Interview/
        public ActionResult Index()
        {
            var interviews = db.Interviews.Include(i => i.ApplicationAccepted);
            return View(interviews.ToList());
        }

        // GET: /Interview/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        // GET: /Interview/Create
        public ActionResult Create()
        {

            //APPLICATION
            //populate list of applications
            var query4 = from c in db.Applications
                         orderby c.ApplicationTitle
                         select c;

            //create list and execute query
            List<Application> allApplications = query4.ToList();

            //convert to select list
            SelectList list4 = new SelectList(allApplications, "ApplicationID", "ApplicationTitle");

            //Add to viewbag
            ViewBag.AllApplications = list4;


            //ViewBag.InterviewID = new SelectList(db.Applications, "ApplicationID", "StudentEID");
            return View();
        }

        // POST: /Interview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="InterviewID")] Interview interview, Int32 ApplicationID)
        {
            if (ModelState.IsValid)
            {

                //APPLICATION
                //Use integer from the view to find the application selected by the user
                Application SelectedApplication = db.Applications.Find(ApplicationID);

                //Associate the selected company with the event
                interview.ApplicationAccepted = SelectedApplication;

                db.Interviews.Add(interview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.InterviewID = new SelectList(db.Applications, "ApplicationID", "StudentEID", interview.InterviewID);
            return View(interview);
        }

        // GET: /Interview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            ViewBag.InterviewID = new SelectList(db.Applications, "ApplicationID", "StudentEID", interview.InterviewID);
            return View(interview);
        }

        // POST: /Interview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="InterviewID")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InterviewID = new SelectList(db.Applications, "ApplicationID", "StudentEID", interview.InterviewID);
            return View(interview);
        }

        // GET: /Interview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        // POST: /Interview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Interview interview = db.Interviews.Find(id);
            db.Interviews.Remove(interview);
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
