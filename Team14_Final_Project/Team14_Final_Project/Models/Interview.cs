using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Team14_Final_Project.Models
{
    public class Interview
    {
        [Key]
        public Int32 InterviewID { get; set; }


        //Navigational Properties
        public virtual Application ApplicationAccepted { get; set; }
    }
}