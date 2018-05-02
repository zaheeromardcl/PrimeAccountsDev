Add-AzureAccount
Select-AzureSubscription -SubscriptionId "450e0a8e-f16d-430d-944a-52a7e17ab4e4"

$ServerName = "mpi66vpdki"
$DatabaseName = "PrimeActsDeployment0_0_5a"
$BlobName = "PrimeActs0_0_5a.bacpac"

$StorageName = "portalvhdsvlq1lbftn329n"
$ContainerName = "vhds"
$StorageKey = "utHUt9aR07O7OIJzsn+JOSCwjTM8+gL+OpvM4clPOJdeViV+Lhbz7rThqa9A1pKQHNh0EnBPAEpzTlGck9W1Wg=="

$credential = Get-Credential
$SqlCtx = New-AzureSqlDatabaseServerContext -ServerName $ServerName -Credential $credential

$StorageCtx = New-AzureStorageContext -StorageAccountName $StorageName -StorageAccountKey $StorageKey
$Container = Get-AzureStorageContainer -Name $ContainerName -Context $StorageCtx

$exportRequest = Start-AzureSqlDatabaseExport -SqlConnectionContext $SqlCtx -StorageContainer $Container -DatabaseName $DatabaseName -BlobName $BlobName

Get-AzureSqlDatabaseImportExportStatus -RequestId $exportRequest.RequestGuid -ServerName $ServerName -Username $credential.UserName