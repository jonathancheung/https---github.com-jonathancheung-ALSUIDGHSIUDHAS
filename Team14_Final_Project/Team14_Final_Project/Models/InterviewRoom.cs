using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team14_Final_Project.Models
{
    public enum RoomNumber { Room1, Room2, Room3, Room4 }

    public class InterviewRoom
    {
        public Int32 InterviewRoomID { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        [EnumDataType(typeof(RoomNumber))]
        public RoomNumber Rooms { get; set; }

        //Navigation Property
        //public virtual List<InterviewTimeAndRoom> InterviewTimeAndRoom { get; set; }


    }
}