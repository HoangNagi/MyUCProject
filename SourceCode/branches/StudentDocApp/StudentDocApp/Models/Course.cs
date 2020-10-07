using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDocApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MyProperty { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public Course()
        {
            Subjects = new Collection<Subject>();
        }
    }
}
