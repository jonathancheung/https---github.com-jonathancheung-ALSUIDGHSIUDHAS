using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team14_Final_Project.Models
{


    public enum PType { Internship, FullTime }
    public class Position
    {
        public Int32 PositionID { get; set; }

        [Required(ErrorMessage = "Position title is required.")]
        [Display(Name = "Position Title")]
        public String PositionTitle { get; set; }

        [Display(Name = "Description")]
        public String PositionDescription { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [Display(Name = "Location")]
        public String Location { get; set; }

        [Required]
        [EnumDataType(typeof(PType))]
        public PType PositionTypes { get; set; }



        [Display(Name = "Position Deadline")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PositionDeadline { get; set; }

        //Navigation Properties
        ////public virtual List<Interview> InterviewTimes { get; set; }
        //public virtual List<AppliedStudents> StudentsApplied { get; set; }
        public virtual List<Major> Majors { get; set; }
        public virtual Company CompanyName { get; set; }

        public virtual List<Application> PositionApplied {get; set;}

    }
}
