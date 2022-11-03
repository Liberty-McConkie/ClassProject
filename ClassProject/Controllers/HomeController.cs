using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassProject.Models;

namespace ClassProject.Controllers
{
    public class HomeController : Controller
    {
        private IStudentRepository _repo { get; set; }

        public HomeController(IStudentRepository temp)
        {
            _repo = temp;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewStudentInfo()
        {
            var blah = _repo.StudentInfo.ToList();
            return View(blah);
        }
        [HttpGet]
        public IActionResult NewEmployeeRecord()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewEmployeeRecord(StudentInfo si)
        {
            _repo.AddStudent(si);
            return View("Confirmation", si);
        }
        [HttpGet]
        public IActionResult Edit(int empid)
        {
            var StudentInfo = _repo.StudentInfo.Single(x => x.EmpID == empid);
            return View("NewEmployeeRecord", StudentInfo);
        }
        [HttpPost]
        public IActionResult Edit(StudentInfo si)
        {
            _repo.UpdateStudent(si);
            return RedirectToAction("ViewStudentInfo");
        }
        [HttpGet]
        public IActionResult Delete(int empid)
        {
            var StudentInfo = _repo.StudentInfo.Single(x => x.EmpID == empid);
            return View(StudentInfo);
        }
        [HttpPost]
        public IActionResult Delete(StudentInfo si)
        {
            _repo.DeleteStudent(si);
            return RedirectToAction("ViewStudentInfo");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
