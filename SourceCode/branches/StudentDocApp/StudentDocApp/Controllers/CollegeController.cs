using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDocApp.Models;

namespace StudentDocApp.Controllers
{
    public class CollegeController : Controller
    {
        private readonly AppDbContext dbContext;

        public CollegeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var college = dbContext.Colleges;
            return View(college);
        }
        public IActionResult Details(int id)
        {
            var college = dbContext.Colleges
                            .Include(m => m.Students)
                        .Where(c => c.Id == id).FirstOrDefault();
            return View(college);
        }
        [HttpGet]
        public IActionResult Addcollege()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Addcollege([FromForm] College college)
        {
            dbContext.Add(college);
            dbContext.SaveChanges();
            TempData["message"] = "Successfully Added";

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var Colleges = dbContext.Colleges.FirstOrDefaultAsync(c => c.Id == id);
            return View();

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var college = dbContext.Colleges.Find(id);
            dbContext.Colleges.Remove(college);
            dbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var College = dbContext.Colleges.FirstOrDefault(c => c.Id == id);
            return View(College);
            //return View(College);

        }
        [HttpPost]
        public IActionResult Edit([FromForm] College college)
        {
            dbContext.Entry(college).State = EntityState.Modified;
            dbContext.SaveChanges();
            TempData["message"] = "Successfully Added";
            return RedirectToAction("Index");
        }
    }
}