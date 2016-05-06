using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Team14_Final_Project.Models;

namespace Team14_Final_Project.Models
{
    //public enum InterviewBlocks { NineAM, 10AM, 11AM, 1PM, 2PM, 3PM, 4PM, 5PM }


    public class InterviewTimes
    {
        public Int32 InterviewTimesID { get; set; }
        


        [Display(Name = "Position Deadline")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:H}", ApplyFormatInEditMode = true)]
        public DateTime StartTimeHour { get; set; }

        //DateTime StartTimeHour = default(DateTime).Add(myDateTime.TimeOfDay)




        //[Required(ErrorMessage = "Please Select a Start Time")]
        //[Display(Name = "Start Time")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime StartTime { get; set; }
        //{ get { return DateTime.Now; } set { StartTime = DateTime.Now; } }

        //[Required]
        //[Display(Name = "Room Number")]
        //[EnumDataType(typeof(InterviewBlocks))]
        //public InterviewBlocks InterviewSlots {get; set;}

        //Navigation Property

        //public virtual List<InterviewTimeAndRoom> InterviewTimeAndRoom { get; set; }
    }
}