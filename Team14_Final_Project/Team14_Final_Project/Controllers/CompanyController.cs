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
    public class CompanyController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var company = from s in db.Companies
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                company = company.Where(s => s.CompanyName.Contains(searchString) ||
                    s.CompanyDescription.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    company = company.OrderByDescending(s => s.CompanyName);
                    break;
                case "Date":
                    company = company.OrderBy(s => s.CompanyDescription);
                    break;
                case "date_desc":
                    company = company.OrderByDescending(s => s.CompanyDescription);
                    break;

            }
            return View(company.ToList());
        }

        // GET: /Company/
        //public ActionResult Index()
        //{
        //    return View(db.Companies.ToList());
        //}

        // GET: /Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: /Company/Create
        public ActionResult Create()
        {
            

            //populate list of industries; "Ask" the database for a list of the committees
            var query = from c in db.Industries
                        orderby c.IndustryName
                        select c;

            //create list and execute query - Convert query results into a list so we can use it on the view
            List<Industry> allIndustries = query.ToList();

            //convert to select list since we only want to allow one committe per event
            MultiSelectList allIndustriesList = new MultiSelectList(allIndustries, "IndustryID", "IndustryName");

            //create a blank list of integers for the industry IDs
            List<Int32> SelectedIndustries = new List<Int32>();

            var model = new CreateCompanyViewModel();
            var industries = db.Industries.Select(c => new
            {
                IndustryID = c.IndustryID,
                IndustryName = c.IndustryName

            }).ToList();

            model.Industries = new MultiSelectList(industries, "IndustryID", "IndustryName");

            //Add the selectList to the ViewBag so the view can use it 
            ViewBag.AllIndustries = allIndustriesList;

            return View(model);
        }

        // POST: /Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CompanyID,CompanyName,CompanyDescription,CompanyEmail")] Company company, int[] selectedIndustries)
        {

            if (ModelState.IsValid)
            {
                if (company.Industries == null)
                {
                    company.Industries = new List<Industry>();
                    foreach (int IndustriesID in selectedIndustries)
                    {
                        Industry IndustryToAdd = db.Industries.Find(IndustriesID);
                        company.Industries.Add(IndustryToAdd);
                    }
                }


                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: /Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }

            //find list of industries
            var query3 = from e in db.Industries
                         orderby e.IndustryName
                         select e;

            //convert list and execute query
            List<Industry> allIndustries = query3.ToList();

            //create list of selected events
            List<Int32> SelectedIndustries = new List<Int32>();

            //loop through list of events and add IndustryID
            foreach (Industry e in company.Industries)
            {
                SelectedIndustries.Add(e.IndustryID);
            }

            //convert list to multiselect list
            MultiSelectList allIndustriesList = new MultiSelectList(allIndustries, "IndustryID", "IndustryName", SelectedIndustries);

            //Add to viewbag
            ViewBag.AllIndustries = allIndustriesList;

            return View(company);
        }

        // POST: /Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CompanyID,CompanyName,CompanyDescription,CompanyEmail")] Company company, int[] SelectedIndustries, Int32 CompanyID)
        {
            if (ModelState.IsValid)
            {
                //Find associated member
                Company CompanyToChange = db.Companies.Find(company.CompanyID);

                //Update events to match selection from user
                //remove any existing events
                CompanyToChange.Industries.Clear();

                //if there are events to add, add them
                if (SelectedIndustries != null)
                {
                    foreach (int IndustriesID in SelectedIndustries)
                    {
                        Industry IndustryToAdd = db.Industries.Find(IndustriesID);
                        CompanyToChange.Industries.Add(IndustryToAdd);
                    }
                }

                //update rest of fields 
                CompanyToChange.CompanyName = company.CompanyName;
                CompanyToChange.CompanyDescription = company.CompanyDescription;
                CompanyToChange.CompanyEmail = company.CompanyEmail;


                db.Entry(CompanyToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: /Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: /Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
