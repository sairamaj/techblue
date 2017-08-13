Function Get-GithubContent
{
    param(
        $Url,
        $FileName
    )
    $clientid = 'd55a0eb2eb441dd8f663'
    $clientSecret = '############################'
    

    $parts = $Url -split '/'
    $repository = $parts[-1]
    $owner = $parts[-2]
    $sessionGetUrl = "https://api.github.com/repos/$owner/$repository/contents/$($FileName)?client_id=$($clientid)&client_secret=$($clientSecret)"
    write-host $sessionGetUrl
    try{
        $contentBase64 = (Invoke-RestMethod $sessionGetUrl).content
        $contentAsBytes = [System.Convert]::FromBase64String($contentBase64)
        $contentAsString = [System.Text.Encoding]::UTF8.GetString($contentAsBytes)
        @($true,$contentAsString)
    }catch{
        $val = ConvertFrom-Json $_
        @($false,$val)
    }

}

Write-Host "Getting Session notes reward types"
$sessionNotesInfo = @{}
foreach($sessionNotesType in (Invoke-RestMethod https://basicjavaclass.azurewebsites.net/api/students/rewards/types)  | where { $_.id -gt 200 -and $_.id -lt 300})
{
    $sessionNotesInfo[$sessionNotesType.id] = $sessionNotesType.extra.filename
    
}


foreach($profile in (Invoke-Restmethod https://d4htq98825.execute-api.us-east-1.amazonaws.com/prod/students/profiles) )
{
 #   $profile.id = '766e375f-360a-4945-904e-cb4aaac5d1e7'
  #  $profile.giturl = "https://github.com/sairamaj/basicjava"

    foreach($sessionNote in $sessionNotesInfo.GetEnumerator())
    {
        Write-host "Getting $($profile.GitUrl) for $($sessionNote.value)"
        $file = "notes/$($sessionNote.value)"
        $result = (Get-GithubContent -Url $profile.giturl -FileName $file )
        
        if( $result[0] -eq $false )
        {
            $status = $file + " " + ($result[1]).message          
        }
        else
        {
            $content = $result[1]
            if( $content.Length -le 200 )
            {
                $status = "$($sessionNote.value) is of size: $($content.Length). Should be minimum 200 characters."
            }
            else
            {
                $status = 'done'
            }
        }

        $postedData = @{}
        $postedData["typeid"] = $sessionNote.Key.ToString()
        $postedData["status"] = $status
        Write-Host "Updating $($profile.id) for status id:$($sessionNote.Key.ToString()) with $status"
        Invoke-RestMethod "https://basicjavaclass.azurewebsites.net/api/students/rewards/$($profile.id)" -Method Post -Body (ConvertTo-Json $postedData)
    }
  # return
}

return

# get notes reward types.
$sessionNotesRewardTypes = (Invoke-RestMethod https://basicjavaclass.azurewebsites.net/api/students/rewards/types)  | where { $_.id -gt 200 -and $_.id -lt 300}

Get-GithubContent -Url "https://github.com/sairamaj/basicjava" -FileName "session1.txt"
return
Write-Host "Getting reward types"


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

