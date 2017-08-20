param(
    [parameter(mandatory=$true)]
    $Name,
    [parameter(mandatory=$true)]
    $Date
)

Function Get-StudentId
{
    param(
        $name
    )
    ((Get-Content .\students.json) | ConvertFrom-Json) | where name -eq $name | select -first 1
}

Function Dump-Students
{
    $url = 'https://basicjavaclass.azurewebsites.net/api/students' 
    (Invoke-RestMethod -Uri $url) | sort name
}

Function Show-ClassDates
{
    Invoke-RestMethod https://basicjavaclass.azurewebsites.net/api/classes
}

Function Test-ClassDate
{
    param(
        $DateToValidate
    )

    ((Invoke-RestMethod https://basicjavaclass.azurewebsites.net/api/classes) | where date -eq $DateToValidate | select date -first 1) -ne $null
}


Write-Host 'Getting student id'
$student = Get-StudentId -name $Name
if( $student -eq $null ){
    Write-Error "$Name could not be found in student list."
    Dump-Students
    return
}

Write-Host 'validating the class date'
if( (Test-ClassDate -DateToValidate $Date ) -eq $false){
   
   Write-Error "$Date is not in one of the classes."
   Show-ClassDates
   return
}
$url = "https://basicjavaclass.azurewebsites.net/api/students/attendance/$($student.Id)"
$data = @{}
$data["date"] = $Date
$data["id"] = $student.id
$data["name"] = $Name

$dataJson = ConvertTo-Json $data 
write-host "Adding $Name with $($student.Id) for the class $Date"
$url
Invoke-RestMethod -Uri $url -Method Post -Body $dataJson


