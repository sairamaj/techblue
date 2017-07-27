using web.Extensions;
using web.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace web
{
    public static class AttendanceUtility
    {
        public static bool IsClassRunning
        {
            get
            {
                // todo need to find out how can we get IClassRepository injected
                return new ClassRepository().IsClassRunningToday().Result;
            }
        }

        public static bool DoesAttendanceExists(IEnumerable<Attendence> attendances, string[] dates)
        {
            System.Console.WriteLine("In AttendanceUtility DoesAttendance exists");
            foreach(var date in dates)
            {
                System.Console.WriteLine("Verifying date:" + date);
                
                if(attendances.Any(a => a.Date == date))
                {
                    return true;
                }
            }

            return false;
        }
    }
}