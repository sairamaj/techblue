using web.Extensions;
using Microsoft.AspNetCore.Http;

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
    }
}