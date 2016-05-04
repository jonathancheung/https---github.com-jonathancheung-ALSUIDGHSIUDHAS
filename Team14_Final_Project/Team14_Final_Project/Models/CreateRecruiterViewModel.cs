using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Team14_Final_Project.Models;

namespace Team14_Final_Project.Models
{
    public class CreateRecruiterViewModel
    {
        public virtual AppUser AppUser { get; set; }
        public virtual Recruiter Recruiter { get; set; }
    }
}