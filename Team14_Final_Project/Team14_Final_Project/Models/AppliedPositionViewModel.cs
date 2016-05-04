using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Team14_Final_Project.Models;

namespace Team14_Final_Project.Models
{
    public class AppliedPositionViewModel : Position
    {
        public Int32 AppliedPositionID { get; set; }

        public bool AppliedToPosition { get; set; }
    }
}