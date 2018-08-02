using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWithDB.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int ActivityID { get; set; }
        public int ChurchUserID { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual ChurchUser ChurchUser { get; set; }
    }
}