using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team14_Final_Project.Models
{
    public class InterviewRoomSchedule
    {
        //Bridge table for Interview Times and Room

        public Int32 InterviewRoomScheduleID { get; set; }

        //Navigation Properties
        //public virtual InterviewTimes InterviewTimes { get; set; }
        //public virtual Room Room { get; set; }
    }
}