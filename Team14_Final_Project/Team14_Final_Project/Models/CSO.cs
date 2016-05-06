using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Team14_Final_Project.Models
{
    public class CSO
    {
        [Key]
        public Int32 CSOID { get; set; }

        //NavigationProperty
        public AppUser AppUsers { get; set; }
    }
}