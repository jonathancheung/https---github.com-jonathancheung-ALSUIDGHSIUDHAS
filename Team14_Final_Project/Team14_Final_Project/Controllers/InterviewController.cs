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


            ////ROOM
            ////populate list of applications
            //var query = from c in db.InterviewRooms
            //            orderby c.Rooms
            //             select c;

            ////create list and execute query
            //List<InterviewRoom> allRooms = query.ToList();

            ////convert to select list
            //SelectList list = new SelectList(allRooms, "InterviewRoomID", "Rooms");

            ////Add to viewbag
            //ViewBag.AllRooms = list;

            ////ROOM
            ////populate list of applications
            //var query2 = from c in db.InterviewTimes
            //             orderby c.StartTime
            //            select c;

            ////create list and execute query
            //List<InterviewTimes> allTimes = query2.ToList();

            ////convert to select list
            //SelectList list2 = new SelectList(allTimes, "InterviewTimesID", "StartTime");

            ////Add to viewbag
            //ViewBag.AllTimes = list2;


            //ViewBag.InterviewID = new SelectList(db.Applications, "ApplicationID", "StudentEID");
            return View();
        }

        // POST: /Interview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="InterviewID")] Interview interview, Int32 ApplicationID, Int32 InterviewRoomID, Int32 InterviewTimeID)
        {
            if (ModelState.IsValid)
            {

                //APPLICATION
                //Use integer from the view to find the application selected by the user
                Application SelectedApplication = db.Applications.Find(ApplicationID);
                //InterviewRoom SelectedRoom = db.InterviewRooms.Find(InterviewRoomID);
                //InterviewTimes SelectedTime = db.InterviewTimes.Find(InterviewTimeID);

                //Associate the selected company with the event
                interview.ApplicationAccepted = SelectedApplication;
                //interview.Room = SelectedRoom;
                //interview.Time = SelectedTime;

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
            Interview interviewToChange = db.Interviews.Find(interview.InterviewID);

            if (ModelState.IsValid)
            {
                interviewToChange.ApplicationAccepted = interview.ApplicationAccepted;
                //interviewToChange.Room = interview.Room;
                //interviewToChange.Time = interview.Time;


                db.Entry(interview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.InterviewID = new SelectList(db.Applications, "ApplicationID", "StudentEID", interview.InterviewID);
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
