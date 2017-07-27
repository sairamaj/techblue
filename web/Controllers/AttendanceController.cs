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
    [Authorize]
    public class AttendanceController : Controller
    {
        public const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        private IClassRepository _classRepository;
        public AttendanceController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetCurrentAttendances());
        }

         
        [HttpPost]
        public async Task<IActionResult> MarkAttendanceLastSession(string date)
        {
            System.Console.WriteLine("..... In MarkAttendanceLastSession :" + date);
            var id = GetClaimValue(ObjectIdentifier);
            await _classRepository.UpdateAttendance(id, User.Identity.Name, DateTime.ParseExact(date,"MMddyy",null));

            return View("Index", await GetCurrentAttendances());
        }

        [HttpPost]
        public async Task<IActionResult> MarkAttendance()
        {
            System.Console.WriteLine("In AttendenceController.MarkAttendence...");

            var zone = System.TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var utcNow = System.DateTime.UtcNow;
            var pacificNow = System.TimeZoneInfo.ConvertTime(utcNow, zone);

            var id = GetClaimValue(ObjectIdentifier);
            await _classRepository.UpdateAttendance(id, User.Identity.Name, pacificNow);
            return View();
        }

        public bool DoesAttendanceExists(IEnumerable<Attendence> attendances, string date)
        {
            Console.WriteLine("In DoesAttendanceExists:" + date);
            return true;
        }

        private string GetClaimValue(string claimType)
        {
            foreach(var c in User.Claims)
            {
                System.Console.WriteLine("{0} - {1}", c.Value, c.Type);
            }

            var claim = User.Claims.FirstOrDefault(c => c.Type == claimType);
            if( claim == null)
            {
                return null;
            }
            return claim.Value;
        }

        private async Task<IEnumerable<Attendence>> GetCurrentAttendances()
        {
            var id = GetClaimValue(ObjectIdentifier);
            var attendances = await _classRepository.GetAttendance(id);
            foreach(var attendance in attendances)
            {
                System.Console.WriteLine("AttendanceController.index {0} ",attendance.Date);
            }

            return attendances;
        }
    } 
}
