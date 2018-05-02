$PublishPath = "Path for the log"
$dacpac = "dbname.dacpac"
$publishProfile = "dbname.xml" # Publish profile xml for Publishing the database project

        # Generate Deploy Report
        $DeployReport =  ".\sqlpackage.exe /Action:DeployReport /Sourcefile:$dacpac /pr:'$publishProfile' /outputpath:$PublishPath"

        Invoke-Expression $DeployReport

        # Generate Script Report
        $GenerateScript =  ".\sqlpackage.exe /Action:Script /Sourcefile:$dacpac /pr:'$publishProfile' /outputpath:$PublishPath"

        Invoke-Expression $GenerateScript

        # Database Publish
        $publish = ".\sqlpackage.exe /Action:Publish /Sourcefile:$dacpac /pr:'$publishProfile'"

        Invoke-Expression $publish | Out-File $PublishPath