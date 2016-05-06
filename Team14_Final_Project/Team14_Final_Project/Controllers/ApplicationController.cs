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
    public class ApplicationController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /Error/
        public ActionResult Error()
        {
            return View();
        }


        // GET: /Application/
        public ActionResult Index()
        {
            return View(db.Applications.ToList());
        }

        // GET: /Application/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: /Application/Apply
        public ActionResult Apply()
        {
            //POSITION
            //populate list of position
            var query = from c in db.Positions
                         orderby c.PositionTitle
                         select c;

            //create list and execute query
            List<Position> allPositions = query.ToList();

            //convert to select list since we only want to allow one position application
            SelectList allPositionsList = new SelectList(allPositions, "PositionID", "PositionTitle");

            //Add the selectList to the ViewBag so the view can use it 
            ViewBag.AllPositions = allPositionsList;

            //STUDENT
            //populate list of position
            var query2 = from c in db.Students
                        orderby c.EID
                        select c;

            //create list and execute query
            List<Student> allStudents = query2.ToList();

            //convert to select list since we only want to allow one position application
            SelectList allStudentsList = new SelectList(allStudents, "StudentID", "EID");

            //Add the selectList to the ViewBag so the view can use it 
            ViewBag.AllStudents = allStudentsList;

            return View();
        }

        // POST: /Application/Apply
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply([Bind(Include = "ApplicationID,ApplicationStatus")] Application application, Int32 PositionID, Int32 StudentID)
        {
            //POSITION
            //Use integer from the view to find the company selected by the user
            Position SelectedPosition = db.Positions.Find(PositionID);

            //Associate the selected company with the event
            application.Positionspplied = SelectedPosition;

            //STUDENT
            //Use integer from the view to find the company selected by the user
            Student SelectedStudent = db.Students.Find(StudentID);

            //Associate the selected company with the event
            application.StudentApplied = SelectedStudent;


            if (ModelState.IsValid)
            {

                //set studentID
                application.StudentEID = application.StudentApplied.EID;

                //check position type
                application.StudentType = application.StudentApplied.StudentPosition;
                application.PositionType = application.Positionspplied.PositionTypes;

                bool Type = application.StudentType != application.PositionType;

                if(Type == true)
                {
                    return RedirectToAction("Error");
                }

                //check deadline
                application.PositionDeadline = application.Positionspplied.PositionDeadline;
                bool Date = application.PositionDeadline >= DateTime.Today;

                if(Date == false)
                {
                    return RedirectToAction("Error");
                }

                //check major
                application.StudentMajor = application.StudentApplied.StudentMajor;
                application.PositionMajor = application.Positionspplied.Majors;

                foreach(Major major in application.PositionMajor)
                {
                    bool CheckMajor = application.StudentMajor != major;
                    if(CheckMajor == true)
                    {
                        return RedirectToAction("Error");
                    }

                }

                //validation for major
                //application.StudentMajor = application.StudentApplied.Major;
                //foreach (int i in application.Positionspplied.Majors)
                //{

                //}

                //set application title 



                application.ApplicationTitle = application.StudentEID + " - " + application.Positionspplied.PositionTitle;


                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(application);
        }

        // GET: /Application/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }


            return View(application);
        }

        // POST: /Application/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ApplicationID,ApplicationStatus")] Application application)
        {
            
            Application applicationToChange = db.Applications.Find(application.ApplicationID);
            
            if (ModelState.IsValid)
            {

                //application.ApplicationTitle = application.StudentEID + " - " + application.Positionspplied.PositionTitle;
                applicationToChange.ApplicationStatus = application.ApplicationStatus;

                //db.Entry(application).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(application);
        }

        // GET: /Application/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: /Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
