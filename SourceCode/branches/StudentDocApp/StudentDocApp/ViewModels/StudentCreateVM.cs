using Microsoft.AspNetCore.Http;
using StudentDocApp.Models;
using System.Collections.Generic;

namespace StudentDocApp.ViewModels
{
    public class StudentCreateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Roll_no { get; set; }
        public IFormFile DisplayImage { get; set; }
        public IEnumerable<College> Colleges { get; set; }
        public string EditImagePath { get; set; }
        public int SelectedCollegeId { get; set; }

    }
}
