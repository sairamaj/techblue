Function Get-GithubContent
{
    param(
        $Url,
        $FileName
    )
    $parts = $Url -split '/'
    $repository = $parts[-1]
    $owner = $parts[-2]
    $sessionGetUrl = "https://api.github.com/repos/$owner/$repository/contents/$FileName"
    $sessionGetUrl
    $contentBase64 = (Invoke-RestMethod $sessionGetUrl).content
    $contentAsBytes = [System.Convert]::FromBase64String($contentBase64)
    $contentAsString = [System.Text.Encoding]::UTF8.GetString($contentAsBytes)
    $contentAsString

}

Get-GithubContent -Url "https://github.com/sairamaj/basicjava" -FileName "session1.txt"
return
Write-Host "Getting reward types"
$sessionNotesRewardTypes = (Invoke-RestMethod https://basicjavaclass.azurewebsites.net/api/students/rewards/types)  | where { $_.id -gt 200 -and $_.id -lt 300}

return
$url = "https://github.com/sairamaj/basicjava"


return

# Attendance id < 100

foreach($profile in (Invoke-Restmethod https://d4htq98825.execute-api.us-east-1.amazonaws.com/prod/students/profiles) )
{
            $postedData = @{}
            $postedData["status"] = "done"
            $postedData["typeid"] = $($profileRewardType.id).ToString()
            
            Write-Host "   Updating $($student.id) for $date with reward type:$($profileRewardType.id)" 
            ConvertTo-Json $postedData
            Invoke-RestMethod "https://basicjavaclass.azurewebsites.net/api/students/rewards/$($profile.id)" -Method Post -Body (ConvertTo-Json $postedData)
}

