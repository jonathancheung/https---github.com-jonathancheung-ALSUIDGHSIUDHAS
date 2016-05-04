﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team14_Final_Project.Models
{
    public class Recruiter : AppUser
    {
        [Key]
        public Int32 RecruiterID { get; set; }

        //Navigational Property
        //public virtual List<Company> Company { get; set; }

    }
}