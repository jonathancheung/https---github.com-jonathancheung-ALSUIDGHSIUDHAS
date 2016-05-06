using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Team14_Final_Project.Models
{
    public enum ApplicationStatus {Pending ,Rejected, Accepted }

    public class Application
    {
        [Key]
        public Int32 ApplicationID { get; set; }

        [Display(Name = "Application Status")]
        [EnumDataType(typeof(ApplicationStatus))]
        public ApplicationStatus ApplicationStatus { get; set; }

        //StudentID
        public String StudentEID { get; set; }

        //Major
        public Major StudentMajor { get; set; }
        public List<Major> PositionMajor { get; set; }

        //Full time or Internship
        [EnumDataType(typeof(PType))]
        public PType StudentType {get; set;}

        [EnumDataType(typeof(PType))]
        public PType PositionType { get; set; }

        //Deadline
        public DateTime PositionDeadline { get; set; }
        public DateTime Today = DateTime.Today;

        public virtual Student StudentApplied { get; set; }
        public virtual Position Positionspplied { get; set; }
    }
}
