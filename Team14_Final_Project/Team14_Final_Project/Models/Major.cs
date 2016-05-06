using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team14_Final_Project.Models
{
    public class Major
    {
        [Key]
        public Int32 MajorID { get; set; }

        [Display(Name = "Major Name")]
        public String MajorName { get; set; }

        public List<Position> Positions { get; set; }
        public List<Student> StudentMajorList { get; set; }
    }
}