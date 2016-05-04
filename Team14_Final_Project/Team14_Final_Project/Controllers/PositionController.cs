﻿using System;
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
            //populate list of committees; "Ask" the database for a list of the committees
            var query = from c in db.Majors
                        orderby c.MajorName
                        select c;

            //create list and execute query - Convert query results into a list so we can use it on the view
            List<Major> allMajors = query.ToList();

            //convert to select list since we only want to allow one committe per event
            MultiSelectList allMajorsList = new MultiSelectList(allMajors, "MajorID", "MajorName");

            //Add the selectList to the ViewBag so the view can use it 
            ViewBag.AllMajors = allMajorsList;

            return View();


        }

        // POST: /Position/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PositionID,PositionTitle,PositionDescription,PositionTypes,ApplicableMajor,PositionDeadline")] Position position, int[] allMajors, Int32 MajorID)
        {
            ////2. Use integer from the view to find the committee selected by the user
            //Major SelectedMajor = db.Majors.Find(MajorID);

            ////3. Associate the selected committee with the event
            //position.ApplicableMajors = SelectedMajor;

            //if (ModelState.IsValid)
            //{
            //    db.Positions.Add(position);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            db.Positions.Add(position);
            Position addedPosition = db.Positions.Find(position.PositionID);
            if (ModelState.IsValid)
            {
              
                //if (addedPosition.Majors == null)
                //{
                //    addedPosition.Majors = new List<Major>();
                //}
                //foreach (int id in allIndustries)
                //{
                //    Industry ind = db.Industries.Find(id);
                //    addedCompany.Industries.Add(ind);
                //}
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
            return View(position);
        }

        //Get Position/Search


        // POST: /Position/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PositionID,PositionTitle,PositionDescription,PositionTypes,ApplicableMajor,PositionDeadline")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
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