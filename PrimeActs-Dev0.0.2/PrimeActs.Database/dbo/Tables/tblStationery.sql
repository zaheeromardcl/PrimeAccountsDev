CREATE TABLE [dbo].[tblStationery]
(
	[StationeryID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [StationeryName] NVARCHAR(50) NOT NULL

	CONSTRAINT AK_StationeryName UNIQUE(StationeryName)
)

GO


