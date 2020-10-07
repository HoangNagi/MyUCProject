using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDocApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Roll_no { get; set; }
        public string DisplayImage { get; set; }
        [Required]
        public int CollegeId { get; set; }
        public virtual College College { get; set; }
    }
}
