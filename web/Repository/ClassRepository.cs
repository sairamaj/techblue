using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using web.Models;

class ClassRepository : IClassRepository
{
    const string StudentProfileApiUrl = "https://d4htq98825.execute-api.us-east-1.amazonaws.com/prod/students/profile/";

    const string CreateAttendenceApiUrl = "https://basicjavaclass.azurewebsites.net/api/students/attendance/";

    const string ClassesInfoUrl = "https://basicjavaclass.azurewebsites.net/api/classes";
    const string AttendanceGetUrl = "https://basicjavaclass.azurewebsites.net/api/attendance/";
    const string StudentRewardsGetUrl = "https://basicjavaclass.azurewebsites.net/api/students/rewards/";
    const string RewardTypeGetUrl = "https://basicjavaclass.azurewebsites.net/api/students/rewards/types";

    public async Task<IEnumerable<Student>> GetStudents()
    {
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync("https://basicjavaclass.azurewebsites.net/api/students");
        return JsonConvert.DeserializeObject<List<Student>>(jsonData);
    }

    public async Task<Profile> GetProfile(string id)
    {
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync(StudentProfileApiUrl + id);
        return JsonConvert.DeserializeObject<Profile>(jsonData);
    }

    public async Task<Profile> UpdateProfile(string id, Profile profile)
    {
        var client = new HttpClient();
        var settings = new JsonSerializerSettings();
        settings.ContractResolver = new LowercaseContractResolver();
        var json = JsonConvert.SerializeObject(profile, Formatting.Indented, settings);
        System.Console.WriteLine("updating :" + json);
        var result = await client.PostAsync(StudentProfileApiUrl + id, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
        System.Console.WriteLine(result.StatusCode);
        if (result.StatusCode != HttpStatusCode.OK)
        {
            throw new System.Exception("Error:" + result.RequestMessage);
        }
        System.Console.WriteLine("Updated successfully");
        return profile;
    }

    public async Task UpdateAttendance(string id, string name, System.DateTime date)
    {
        System.Console.WriteLine("Update Attedence: id:{0} date:{1}", id, date);

        var client = new HttpClient();
        var attendence = new Attendence
        {
            Date = date.ToString("MMddyy"),
            Id = id,
            Name = name
        };

        var url = CreateAttendenceApiUrl + id;
        var settings = new JsonSerializerSettings();
        settings.ContractResolver = new LowercaseContractResolver();
        var json = JsonConvert.SerializeObject(attendence, Formatting.Indented, settings);
        System.Console.WriteLine("updating :" + json);
        var result = await client.PostAsync(url, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
        System.Console.WriteLine(result.StatusCode);
        if (result.StatusCode != HttpStatusCode.OK)
        {
            throw new System.Exception("Error:" + result.RequestMessage);
        }

        await Task.FromResult(0);
    }

    public async Task<bool> IsClassRunningToday()
    {

        var client = new HttpClient();
        var jsonData = await client.GetStringAsync(ClassesInfoUrl);
        var classes = JsonConvert.DeserializeObject<List<Class>>(jsonData);

        foreach (var cls in classes)
        {
            System.Console.WriteLine(cls.Date + "->" + cls.Status);
        }

        var zone = System.TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
        var utcNow = System.DateTime.UtcNow;
        var pacificNow = System.TimeZoneInfo.ConvertTime(utcNow, zone);
        System.Console.WriteLine("Pacific time now:{0}", pacificNow.ToString("MMddyy"));
        return classes.Any(c => c.Status == "running" && c.Date == pacificNow.ToString("MMddyy"));
    }

    public async Task<bool> CheckAttendanceExists(string id, System.DateTime dt)
    {
        throw new System.NotImplementedException("");
    }
    public async Task<IEnumerable<Attendence>> GetAttendance(System.DateTime dt)
    {
        var client = new HttpClient();
        var data = await client.GetStringAsync(AttendanceGetUrl + dt.ToString("MMddyy"));
        var attendanceInfo = JsonConvert.DeserializeObject<List<Attendence>>(data);
        foreach (var attendance in attendanceInfo)
        {
            System.Console.WriteLine("... attendance:id:{0} - {1}", attendance.Id, attendance.Name);
            //System.Console.WriteLine("... attendance:name" + attendance.Data == null ? "na" : attendance.Data.Name);
        }

        return attendanceInfo;
    }

    public async Task<IEnumerable<Attendence>> GetAttendance(string id)
    {
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync(ClassesInfoUrl);
        var classes = JsonConvert.DeserializeObject<List<Class>>(jsonData);

        List<Attendence> attendances = new List<Attendence>();
        foreach (var cls in classes)
        {
            System.Console.WriteLine(cls.Date + "->" + cls.Status);
            client = new HttpClient();
            System.Console.WriteLine("Quering;:" + AttendanceGetUrl + cls.Date);
            var data = await client.GetStringAsync(AttendanceGetUrl + cls.Date);
            var attendanceInfo = JsonConvert.DeserializeObject<List<Attendence>>(data);
            var studentAttendanceInfo = attendanceInfo.FirstOrDefault(a => a.Id == id);
            if (studentAttendanceInfo != null)
            {
                studentAttendanceInfo.Date = cls.Date;
                attendances.Add(studentAttendanceInfo);
            }
        }

        return attendances;
    }

    public async Task<IEnumerable<Class>> GetClasses()
    {
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync(ClassesInfoUrl);
        return JsonConvert.DeserializeObject<List<Class>>(jsonData);
    }

    public async Task<IEnumerable<Reward>> GetRewards(string id)
    {
        var rewardTypes = await GetRewardTypes();
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync(StudentRewardsGetUrl + id);
        var exitingRewards = JsonConvert.DeserializeObject<List<Reward>>(jsonData);
        var allRewards = new List<Reward>();

        foreach(var rewardType in rewardTypes)
        {
            var foundReward = exitingRewards.FirstOrDefault( r=> r.TypeId == rewardType.Id);
            if(foundReward != null)
            {
                foundReward.Description = rewardType.Description;
                allRewards.Add(foundReward);
            }
            else{
                var reward = new Reward(){
                    TypeId = rewardType.Id,
                    Description = rewardType.Description,
                    Status = "not done"
                };
                allRewards.Add(reward);
            }
        }

        return allRewards;
    }

    private async Task<IEnumerable<RewardType>> GetRewardTypes()
    {
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync(RewardTypeGetUrl);
        return JsonConvert.DeserializeObject<List<RewardType>>(jsonData);
    }
}