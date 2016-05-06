using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Team14_Final_Project.Models;
using System.Web.Mvc;


namespace Team14_Final_Project.Models
{
    public class ScheduleInterviewViewModel
    {
        public virtual InterviewRoom InterviewRoom { get; set; }
        public virtual InterviewTimes InterviewTime { get; set; }
    }
}