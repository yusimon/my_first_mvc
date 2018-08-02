using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthWithDB.Models
{
    public class Activity
    {
        [Key]
        public int ActivityID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }

        [Required]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}