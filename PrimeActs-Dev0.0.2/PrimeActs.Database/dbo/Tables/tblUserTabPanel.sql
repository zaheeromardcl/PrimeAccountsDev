CREATE TABLE [dbo].[tblUserTabPanel]
(
	[PanelId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [ContentType] NVARCHAR(50) NOT NULL, 
    [HoldingDiv] NVARCHAR(50) NOT NULL, 
    [IsSelected] BIT NOT NULL, 
    [ControllerState] NVARCHAR(50) NULL, 
    [JsonData] NVARCHAR(MAX) NULL,
	[UriParam] NVARCHAR(100) NULL
)
