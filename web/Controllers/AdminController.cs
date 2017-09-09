using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using web.Models;
using System.IO;

namespace web.Controllers
{
    [Authorize(Roles = "Administrators")]
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
            foreach (var student in students)
            {
                var profile = await _classRepository.GetProfile(student.Id);
                if (profile != null)
                {
                    student.GitUrl = profile.GitUrl;
                }
            }

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Parents()
        {
            var parents = await _classRepository.GetParents();
            return View(parents);
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
            var attendances = await _classRepository.GetAttendance(DateTime.ParseExact(date, "MMddyy", null));
            System.Console.WriteLine("In attendance by date:{0}", date);
            return View(attendances);
        }

        [HttpGet]
        public async Task<IActionResult> Rewards()
        {
            // var rewardsJson = @"c:\temp\rewards.json";
            // if (System.IO.File.Exists(@"c:\temp\rewards.json"))
            // {
            //     return View(JsonConvert.DeserializeObject(System.IO.File.ReadAllText(rewardsJson), typeof(IEnumerable<Student>)));
            // }

            var students = await _classRepository.GetStudents();
            foreach (var student in students)
            {
                var profile = await _classRepository.GetProfile(student.Id);
                if (profile != null)
                {
                    student.GitUrl = profile.GitUrl;
                }
            }

            foreach (var student in students)
            {
                student.Rewards = await _classRepository.GetRewards(student.Id);
            }

            // var jsonInfo = JsonConvert.SerializeObject(students);
            // System.IO.File.WriteAllText(rewardsJson, jsonInfo);
            return View(students);
        }
    }
}
