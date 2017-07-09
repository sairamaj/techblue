# post checkin

$id = "3"
$postData = @{}
$postData["id"] = $id
$postData["name"] = "krish"
$postData["url"] = "krishurl"

$jsonData = ConvertTo-Json $postData
$url = "https://d4htq98825.execute-api.us-east-1.amazonaws.com/prod/students/profile/$id"
Write-Host "$url"
Write-Host "posting $jsonData"
Invoke-RestMethod -Method Post -Uri $url -Body $jsonData
Write-Host "getting"
Invoke-RestMethod -Method Get -Uri $url