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
    public class PositionController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /Position/
        public ActionResult Index()
        {
            return View(db.Positions.ToList());
        }

        // GET: /Position/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // GET: /Position/Create
        public ActionResult Create()
        {
            //COMPANY
            //populate list of companies
            var query2 = from c in db.Companies
                        orderby c.CompanyName
                        select c;

            //create list and execute query
            List<Company> allCompanies = query2.ToList();

            //convert to select list since we only want to allow one committe per event
            SelectList allCompaniesList = new SelectList(allCompanies, "CompanyID", "CompanyName");

            //Add the selectList to the ViewBag so the view can use it 
            ViewBag.AllCompanies = allCompaniesList;


            //MAJORS
            //populate list of Majors; "Ask" the database for a list of the majors
            var query = from c in db.Majors
                        orderby c.MajorName
                        select c;

            //create list and execute query - Convert query results into a list so we can use it on the view
            List<Major> allMajors = query.ToList();

            //convert to select list since we only want to allow one committe per event
           MultiSelectList allMajorsList = new MultiSelectList(allMajors, "MajorID", "MajorName");

            //create a blank list of integers for the industry IDs
            List<Int32> SelectedMajors = new List<Int32>();

            var model = new CreatePositionViewModel();
            var majors = db.Majors.Select(c => new
            {
                MajorID = c.MajorID,
                MajorName = c.MajorName

            }).ToList();

            model.Majors = new MultiSelectList(majors, "MajorID", "MajorName");

            //Add the selectList to the ViewBag so the view can use it 
            ViewBag.AllMajors = allMajorsList;

            return View(model);


        }

        // POST: /Position/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PositionID,PositionTitle,PositionDescription,PositionTypes,PositionDeadline,Location,Company,Major")] Position position, int[] SelectedMajors, Int32 CompanyID)
        {
            //Position addedPosition = db.Positions.Find(position.PositionID);
            if (ModelState.IsValid)
            {

                //COMPANY
                //Use integer from the view to find the company selected by the user
                Company SelectedCompany = db.Companies.Find(CompanyID);

                //Associate the selected company with the event
                position.CompanyName = SelectedCompany;


                //MAJOR
                if (position.Majors == null)
                {
                    position.Majors = new List<Major>();
                    foreach (int MajorID in SelectedMajors)
                    {
                        Major MajorToAdd = db.Majors.Find(MajorID);
                        position.Majors.Add(MajorToAdd);
                    }
                }


                db.Positions.Add(position);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(position);
        }


        // GET: /Position/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }

            //COMPANY
            //populate list of company
            var query4 = from c in db.Companies
                        orderby c.CompanyName
                        select c;

            //create list and execute query
            List<Company> allCompanies = query4.ToList();

            //convert to select list
            SelectList list4 = new SelectList(allCompanies, "CompanyID", "CompanyName", position.CompanyName.CompanyID);

            //Add to viewbag
            ViewBag.AllCompanies = list4;

            //MAJOR
            //find list of majors
            var query3 = from e in db.Majors
                         orderby e.MajorName
                         select e;

            //convert list and execute query
            List<Major> allMajors = query3.ToList();

            //create list of selected events
            List<Int32> SelectedMajors = new List<Int32>();

            //loop through list of events and add MajorID
            foreach (Major e in position.Majors)
            {
                SelectedMajors.Add(e.MajorID);
            }

            //convert list to multiselect list
            MultiSelectList allIndustriesList = new MultiSelectList(allMajors, "MajorID", "MajorName", SelectedMajors);

            //Add to viewbag
            ViewBag.AllIndustries = allIndustriesList;

            return View(position);
        }

        //Get Position/Search


        // POST: /Position/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PositionID,PositionTitle,PositionDescription,PositionTypes,ApplicableMajor,PositionDeadline,Location")] Position position, int[] SelectedMajors, Int32 CompanyID)
        {
            if (ModelState.IsValid)
            {
                //2. Find the associated position
                Position positionToChange = db.Positions.Find(position.PositionID);

                //change company if necessary
                if (positionToChange.CompanyName.CompanyID != CompanyID)
                {
                    //find company
                    Company SelectedCompany = db.Companies.Find(CompanyID);

                    //update company
                    positionToChange.CompanyName = SelectedCompany;
                }

                //3. Update majors to match the selection from the user
                //a) Remove any existing majors using clear method
                positionToChange.Majors.Clear();

                //b) If there's something in the array, loop through it to add each major
                if (SelectedMajors != null)
                {
                    foreach (int majorID in SelectedMajors)
                    {
                        Major majorToAdd = db.Majors.Find(majorID);
                        positionToChange.Majors.Add(majorToAdd);
                    }
                }

                //c) Update rest of scalar fields. Event that gets passed over doesn't directly exist in database
                positionToChange.PositionTitle = position.PositionTitle;
                positionToChange.PositionDescription = position.PositionDescription;
                positionToChange.Location = position.Location;
                positionToChange.PositionTypes = position.PositionTypes;
                positionToChange.PositionDeadline = position.PositionDeadline;


                //d) Update db.Entry code to reflect the event you want to change. Save changes
                db.Entry(positionToChange).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(position);
        }

        // GET: /Position/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: /Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Position position = db.Positions.Find(id);
            db.Positions.Remove(position);
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
