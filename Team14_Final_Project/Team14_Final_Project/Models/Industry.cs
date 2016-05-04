using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Team14_Final_Project.Models
{
    public class Industry
    {
        [Key]
        public Int32 IndustryID { get; set; }

        [Required(ErrorMessage = "Industry name is required.")]
        [Display(Name = "Industry Name")]
        public String IndustryName { get; set; }

        //Navigation Property
        [Display(Name = "Companies")]
        public virtual List<Company> Companies { get; set; }

    }
}