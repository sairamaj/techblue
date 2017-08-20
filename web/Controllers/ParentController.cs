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
    [Authorize(Roles = "Parents")]
    public class ParentController : Controller
    {
        IClassRepository _classRepository;
        public ParentController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
