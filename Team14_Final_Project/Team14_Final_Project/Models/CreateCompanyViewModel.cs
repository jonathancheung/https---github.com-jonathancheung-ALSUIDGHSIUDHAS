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
    public class CreateCompanyViewModel
    {
        public virtual Company Company { get; set; }

        public int[] selectedIndustries { get; set; }
        public MultiSelectList Industries { get; set; }
    }
}