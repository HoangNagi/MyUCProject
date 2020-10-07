using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentDocApp.Models;
using StudentDocApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using StudentDocApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Data;

namespace StudentDocApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFileService fileService;

        public StudentController(AppDbContext dbContext,
                           IHostingEnvironment environment,
                           IFileService fileService
            )
        {
            this.dbContext = dbContext;
            hostingEnvironment = environment;
            this.fileService = fileService;
        }
        public static string getpath;

        public IActionResult Index()
        {
            var students = dbContext.Students
                                .Include(s => s.College).OrderByDescending(s => s.Id);

            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var student = new StudentCreateVM();
            student.Colleges = dbContext.Colleges.ToList();
            return View(student);
        }


        [HttpPost]
        public IActionResult Create([FromForm] StudentCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student()
                {
                    DisplayImage = vm.DisplayImage.FileName,
                    Name = vm.Name,
                    Roll_no = vm.Roll_no,
                    CollegeId = vm.SelectedCollegeId,
                };
                var fileName = fileService.Upload(vm.DisplayImage);
                student.DisplayImage = fileName;
                getpath = fileName;

                dbContext.Add(student);
                dbContext.SaveChanges();
                TempData["message"] = "Successfully Added";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = dbContext.Students
                             .Include(s => s.College)
                             .Where(s => s.Id == id).FirstOrDefault();

            if (student == null) return View();
            var studentVm = new StudentCreateVM();
            studentVm.Id = student.Id;
            studentVm.EditImagePath = student.DisplayImage;
            studentVm.Name = student.Name;
            studentVm.Roll_no = student.Roll_no;
            studentVm.SelectedCollegeId = student.College.Id;
            studentVm.Colleges = dbContext.Colleges.ToList();
            return View(studentVm);
        }
        [HttpPost]

        public IActionResult Edit([FromForm] StudentCreateVM vm, int id)
        {
            if (ModelState.IsValid)
            {
                var student = dbContext.Students
                             .Include(s => s.College)
                         .Where(s => s.Id == id).FirstOrDefault();
                if (vm.DisplayImage != null)
                {

                    student.DisplayImage = fileService.Upload(vm.DisplayImage);
                }

                student.Name = vm.Name;
                student.Roll_no = vm.Roll_no;
                student.CollegeId = vm.SelectedCollegeId;
                dbContext.SaveChanges();
                TempData["editmessage"] = "Edited Successfully";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            StudentDetailsVM vm = new StudentDetailsVM();
            var student = dbContext.Students
            .Include(s => s.College)
            .Where(s => s.Id == id).First();
            vm.EditImagePath = student.DisplayImage;
            vm.Name = student.Name;
            vm.College = student.College;
            vm.Roll_no = student.Roll_no;
            return View(vm);
        }

        public ActionResult Delete(int? Id, bool? saveChangesError = false)
        {

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete nagi";
            }
            Student student = dbContext.Students.Find(Id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                Student studentToDelete = new Student() { Id = Id };
                dbContext.Students.Remove(studentToDelete);
                dbContext.SaveChanges();
            }
            catch (DataException/* dex */) //log ko lagi dex
            {

                return RedirectToAction("Delete", new { Id = Id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);  //yo database bata connection hatako

        }
    }
}