using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team14_Final_Project.Models
{
    public enum RoomNumber { Room1, Room2, Room3, Room4}

    public class Room
    {
        [Key]
        public Int32 RoomID { get; set; }

        [Required(ErrorMessage = "Please select a room.")]
        [EnumDataType(typeof(RoomNumber))]
        public RoomNumber Rooms { get; set; }

        //Navigation Property
        //public virtual List<InterviewRoomSchedule> InterviewRoomSchedules { get; set; }


    }
}