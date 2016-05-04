using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team14_Final_Project.Models
{
    public class AppliedStudents
    {
        //Bridge Table for Students and Position
        public Int32 AppliedStudentsID { get; set; }

        public Boolean AppliedToPosition { get; set; }

        ////Navigation Properties
        //public virtual Student Student { get; set; }
        //public virtual Position Position { get; set; }
    }
}