$context= New-AzureStorageContext -StorageAccountName "portalvhdsvlq1lbftn329n" -StorageAccountKey "utHUt9aR07O7OIJzsn+JOSCwjTM8+gL+OpvM4clPOJdeViV+Lhbz7rThqa9A1pKQHNh0EnBPAEpzTlGck9W1Wg=="

Set-AzureStorageBlobContent -Context $context -Container "vhds" -File "c:\Bacpacs\PrimeActsDev.bacpac" -Blob "PrimeActsDevBLOB.bacpac"