using StudentDocApp.Models;
using System.Collections.Generic;

namespace StudentDocApp.ViewModels
{
    public class CourseCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Subject> Subject { get; set; }
        public int SelectedSubjectId { get; set; }
    }
}
