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
    public class ClassSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
