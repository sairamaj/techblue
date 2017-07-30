# Attendance id < 100
Write-Host "Getting reward types"
$profileRewardType = (Invoke-RestMethod https://basicjavaclass.azurewebsites.net/api/students/rewards/types)  | where id -eq 101
$profileRewardType

foreach($profile in (Invoke-Restmethod https://d4htq98825.execute-api.us-east-1.amazonaws.com/prod/students/profiles) )
{
            $postedData = @{}
            $postedData["status"] = "done"
            $postedData["typeid"] = $($profileRewardType.id).ToString()
            
            Write-Host "   Updating $($student.id) for $date with reward type:$($profileRewardType.id)" 
            ConvertTo-Json $postedData
            Invoke-RestMethod "https://basicjavaclass.azurewebsites.net/api/students/rewards/$($profile.id)" -Method Post -Body (ConvertTo-Json $postedData)
}

