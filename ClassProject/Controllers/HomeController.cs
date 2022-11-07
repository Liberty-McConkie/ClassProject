using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassProject.Models;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace ClassProject.Controllers
{
    public class HomeController : Controller
    {
        private byte[] fileData;

        private IStudentRepository _repo { get; set; }

        public HomeController(IStudentRepository temp)
        {
            _repo = temp;
        }
        public IActionResult Index()
        {
            var average = _repo.StudentInfo
                .Select(x => x.Payrate).Average();

            var empCount = _repo.StudentInfo
                .Select(x => x.EmpID).Count();

            var taAverage = _repo.StudentInfo
                .Where(x => x.PositionType == "TA")
                .Select(x => x.Payrate).Average();

            var raAverage = _repo.StudentInfo
                .Where(x => x.PositionType == "RA")
                .Select(x => x.Payrate).Average();

            var officeAverage = _repo.StudentInfo
                .Where(x => x.PositionType == "Office")
                .Select(x => x.Payrate).Average();

            var stInstAverage = _repo.StudentInfo
                .Where(x => x.PositionType == "Student Instructor")
                .Select(x => x.Payrate).Average();

            var otherAverage = _repo.StudentInfo
                .Where(x => x.PositionType == "Other")
                .Select(x => x.Payrate).Average();

            var needEmail = _repo.StudentInfo
                .Where(x => x.AuthorizationToWorkReceived == false)
                .Select(x => x.FirstName + " " + x.LastName).ToArray();

            var ivm = new IndexViewModel
            {
                avgPay = Math.Round((decimal)average, 2),
                taAvgPay = Math.Round((decimal)taAverage, 2),
                raAvgPay = Math.Round((decimal)raAverage, 2),
                officeAvgPay = Math.Round((decimal)officeAverage, 2),
                stInstAvgPay = Math.Round((decimal)stInstAverage, 2),
                otherAvgPay = Math.Round((decimal)otherAverage, 2),
                empCountMath = Math.Round((decimal)empCount,2),
                empNeedEmail = needEmail.ToArray()

            };

            return View(ivm);
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

        public FileContentResult ExportToExcel()
        {
            var empList = _repo.StudentInfo.Where(x => x.Semester == "Fall" & x.Year1 == "2022").ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Current Employee";
            ws.Cells["A2"].Value = "Report";

            ws.Cells["B1"].Value = "Date";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}",DateTimeOffset.Now);

            ws.Cells["A5"].Value = "Employee Id";
            ws.Cells["B5"].Value = "First Name";
            ws.Cells["C5"].Value = "Last Name";
            ws.Cells["D5"].Value = "Semester";
            ws.Cells["E5"].Value = "Year";
            ws.Cells["F5"].Value = "Supervisor";
            ws.Cells["G5"].Value = "Email";
            ws.Cells["H5"].Value = "Phone Number";

            int rowStart = 6;
            foreach (var item in empList) 
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.EmpID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.FirstName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.LastName;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Semester;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Year1;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Supervisor;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Phone;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            
            using (var ms = new MemoryStream())
            {
                pck.SaveAs(ms);
                ms.Seek(0, SeekOrigin.Begin);
                fileData = ms.ToArray();
            }


            string fileName = "CurrentEmployeeReport.xlsx";
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

            Response.Headers["Content-Disposition"] = String.Format("attachment;filename={0}", fileName);

            return File(fileData, contentType);

            /*Response.Clear();
            Response.ContentType = "application/vnd/openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["content-disposition"] = "attachment: filename = " + "ExcelReport.xlsx";
            Response.Body.WriteAsync(pck.GetAsByteArray());
            Response.Body.Close();*/


        }

        public FileContentResult AllEmpExportToExcel()
        {
            var empList = _repo.StudentInfo.OrderBy(x => x.Year1).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "All Employees";
            ws.Cells["A2"].Value = "Report";

            ws.Cells["B1"].Value = "Date";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A5"].Value = "Employee Id";
            ws.Cells["B5"].Value = "First Name";
            ws.Cells["C5"].Value = "Last Name";
            ws.Cells["D5"].Value = "Semester";
            ws.Cells["E5"].Value = "Year";
            ws.Cells["F5"].Value = "Supervisor";
            ws.Cells["G5"].Value = "Email";
            ws.Cells["H5"].Value = "Phone Number";

            int rowStart = 6;
            foreach (var item in empList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.EmpID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.FirstName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.LastName;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Semester;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Year1;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Supervisor;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Phone;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();

            using (var ms = new MemoryStream())
            {
                pck.SaveAs(ms);
                ms.Seek(0, SeekOrigin.Begin);
                fileData = ms.ToArray();
            }


            string fileName = "HistoricalEmployeesReport.xlsx";
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

            Response.Headers["Content-Disposition"] = String.Format("attachment;filename={0}", fileName);

            return File(fileData, contentType);

            /*Response.Clear();
            Response.ContentType = "application/vnd/openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["content-disposition"] = "attachment: filename = " + "ExcelReport.xlsx";
            Response.Body.WriteAsync(pck.GetAsByteArray());
            Response.Body.Close();*/


        }

        public FileContentResult EmpExportBySupervisorToExcel()
        {
            var empList = _repo.StudentInfo.OrderBy(x => x.Supervisor).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "All Employees";
            ws.Cells["A2"].Value = "Report";

            ws.Cells["B1"].Value = "Date";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A5"].Value = "Employee Id";
            ws.Cells["B5"].Value = "First Name";
            ws.Cells["C5"].Value = "Last Name";
            ws.Cells["D5"].Value = "Supervisor";
            ws.Cells["E5"].Value = "Semester";
            ws.Cells["F5"].Value = "Year";
            ws.Cells["G5"].Value = "Email";
            ws.Cells["H5"].Value = "Phone Number";

            int rowStart = 6;
            foreach (var item in empList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.EmpID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.FirstName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.LastName;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Supervisor;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Semester;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Year1;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Phone;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();

            using (var ms = new MemoryStream())
            {
                pck.SaveAs(ms);
                ms.Seek(0, SeekOrigin.Begin);
                fileData = ms.ToArray();
            }


            string fileName = "EmployeeReportBySupervisor.xlsx";
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

            Response.Headers["Content-Disposition"] = String.Format("attachment;filename={0}", fileName);

            return File(fileData, contentType);

            /*Response.Clear();
            Response.ContentType = "application/vnd/openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["content-disposition"] = "attachment: filename = " + "ExcelReport.xlsx";
            Response.Body.WriteAsync(pck.GetAsByteArray());
            Response.Body.Close();*/


        }

    }
}
