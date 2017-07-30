# Attendance id < 100
Write-Host "Getting reward types"
$attendanceTypes = (Invoke-RestMethod https://basicjavaclass.azurewebsites.net/api/students/rewards/types)  | where id -le 100 

$studentAttendancesRewards = @{}

foreach($attendanceType in $attendanceTypes)
{
    # Get dates
        
    foreach($date in $attendanceType.extra.dates)
    {
        Write-Host " Getting attendances for $date"

        foreach($student in (Invoke-RestMethod "https://basicjavaclass.azurewebsites.net/api/attendance/$date" ))
        {
            $postedData = @{}
            $postedData["status"] = "done"
            $postedData["typeid"] = $($attendanceType.id).ToString()
            
            Write-Host "   Updating $($student.id) for $date with reward type:$($attendanceType.id)" 
            ConvertTo-Json $postedData
            Invoke-RestMethod "https://basicjavaclass.azurewebsites.net/api/students/rewards/$($student.id)" -Method Post -Body (ConvertTo-Json $postedData)
        }

    }
}

return
