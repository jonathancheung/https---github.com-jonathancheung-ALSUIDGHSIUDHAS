﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace Team14_Final_Project.Models
{
    public class Recruiter
    {
        [Key]
        public Int32 RecruiterID { get; set; }

        //Navigational Property
        public virtual Company Company { get; set; }

        public virtual AppUser AppUsers { get; set; } 
    }
}