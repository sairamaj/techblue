using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IClassRepository classRepository)
        {
            _classRepository = classRepository;    
        }

        IClassRepository _classRepository;
        public IActionResult Index()
        {
            var students = new ClassRepository().GetStudents().Result;
            foreach(var student in students)
            {
                System.Console.WriteLine(student.Name);
            }
            return View(students);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
