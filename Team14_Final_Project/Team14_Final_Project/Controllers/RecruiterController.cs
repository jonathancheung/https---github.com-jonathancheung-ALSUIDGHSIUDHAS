using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Team14_Final_Project.Models;
using System.Net;
using System.Data.Entity;
using System.Collections.Generic;

namespace Team14_Final_Project.Controllers
{
    public class RecruiterController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private ApplicationSignInManager _signInManager;
        private AppUserManager _userManager;

        public RecruiterController()
        {
        }

        public RecruiterController(AppUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: /Recruiter/
        public ActionResult Index()
        {
            return View(db.Recruiters.ToList());
        }

        // GET: /Recruiter/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Create(DateTime? LastLoginDate)
        {
            //COMPANY
            //populate list of companies
            var query2 = from c in db.Companies
                         orderby c.CompanyName
                         select c;

            //create list and execute query
            List<Company> allCompanies = query2.ToList();

            //convert to select list since we only want to allow one committe per event
            //SelectList allCompaniesList = 

            //Add the selectList to the ViewBag so the view can use 
            ViewBag.CompanyLists = new SelectList(allCompanies, "CompanyID", "CompanyName");

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FirstName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Company")] CreateRecruiterViewModel model, Recruiter recruiter, Int32 CompanyID)
        public async Task<ActionResult> Create(CreateRecruiterViewModel model, Int32 CompanyID, Recruiter recruiter)
        {

            if (ModelState.IsValid)
            {
                Company SelectedCompany = db.Companies.Find(CompanyID);

                recruiter.Company = SelectedCompany;


                //TODO: Add fields to user here so they will be saved to do the database
                var user = new AppUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Recruiters = model.Recruiter };
                user.UserName = model.Email;
                user.Email = model.Email;


                var result = await UserManager.CreateAsync(user, model.Password);

                //db.Recruiters.Add(recruiter);
                //db.SaveChanges();
                //return RedirectToAction("Index");

                //TODO:  Once you get roles working, you may want to add users to roles upon creation
                //await UserManager.AddToRoleAsync(user.Id, "Member");

                // --OR--
                // await UserManager.AddToRoleAsync(user.Id, "Employee");

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        // GET: /Recruiter/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        // POST: /Recruiter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,RecruiterID")] Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recruiter);
        }

        //GET: /Recruiter/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        // POST: /Recruiter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Recruiter recruiter = db.Recruiters.Find(id);
            db.Recruiters.Remove(recruiter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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