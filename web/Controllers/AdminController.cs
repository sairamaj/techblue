using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web.Models;

namespace web.Controllers
{
    [Authorize(Roles="Administrators")]
    public class AdminController : Controller
    {
        IClassRepository _classRepository;
        public AdminController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Students()
        {
            var students = await _classRepository.GetStudents();
            foreach(var student in students)
            {
                var profile = await _classRepository.GetProfile(student.Id);
                if( profile != null)
                {
                    student.GitUrl = profile.GitUrl;
                }
            }

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Attendance()
        {
            var classes = await _classRepository.GetClasses();
            return View(classes);
        }

        [HttpGet]
        public async Task<IActionResult> AttendanceByDate(string date)
        {
            var attendances = await _classRepository.GetAttendance(DateTime.ParseExact(date,"MMddyy", null));
            System.Console.WriteLine("In attendance by date:{0}" , date );
            return View(attendances);
        }

    }
}
