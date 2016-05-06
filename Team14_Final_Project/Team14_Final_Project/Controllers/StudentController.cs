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
    public class StudentController : Controller
    {
        private AppDbContext db = new AppDbContext();

        //// GET: /Student/
        //public ActionResult Index()
        //{
        //    return View(db.Users.ToList());
        //}

        // GET: /Student/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //Student Search
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s=> s.AppUsers.FirstName.Contains(searchString) ||
                    s.AppUsers.Email.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.AppUsers.FirstName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.AppUsers.Email);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.StudentMajor);
                    break;

            }
            return View(students.ToList());
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FirstName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,StudentID,GradSemester,GradYear,StudentPosition,GPA")] Student student, Int32 MajorID)
        {

            //2. Use integer from the view to find the committee selected by the user
            Major StudentMajor = db.Majors.Find(MajorID);

            //3. Associate the selected committee with the event
            student.StudentMajor = StudentMajor;


            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: /Student/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            //Major
            //populate list of majors
            var query = from c in db.Majors
                        orderby c.MajorName
                        select c;

            //create list and execute query
            List<Major> allMajors = query.ToList();

            //convert to select list
            SelectList list = new SelectList(allMajors, "MajorID", "MajorName", student.StudentMajor.MajorID);

            //Add to viewbag
            ViewBag.AllMajors = list;

            return View(student);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FirstName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,StudentID,GradSemester,GradYear,StudentPosition,GPA")] Student student, Int32 MajorID)
        {
            //var query = from c in db.Majors
            //            orderby c.MajorName
            //            select c;

            //create list and execute query
            //List<Major> allMajors = query.ToList();

            //convert to select list
            //SelectList list = new SelectList(allMajors, "MajorID", "MajorName", student.StudentMajor.MajorID);

            //Add to viewbag
            //ViewBag.AllMajors = list;
            if (ModelState.IsValid)
            {
                var query = from c in db.Majors
                            orderby c.MajorName
                            select c;

                //create list and execute query
                List<Major> allMajors = query.ToList();

                //2. Find the associated student
                Student studentToChange = db.Students.Find(student.StudentID);

                //change major if necessary
                if (studentToChange.StudentMajor.MajorID != MajorID)
                {
                    SelectList list = new SelectList(allMajors, "MajorID", "MajorName", student.StudentMajor.MajorID);

                    //find major
                    Major SelectedMajor = db.Majors.Find(MajorID);

                    //update major
                    studentToChange.StudentMajor = SelectedMajor;
                    ViewBag.AllMajors = list;
                }

             

                studentToChange.EID = student.EID;
                studentToChange.GradSemester = student.GradSemester;
                studentToChange.GradYear = student.GradYear;
                studentToChange.StudentPosition = student.StudentPosition;
                studentToChange.GPA = student.GPA;

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: /Student/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
