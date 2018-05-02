CREATE TABLE [dbo].[tblPrintTask]
(
	[PrintTaskID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PrintTaskName] VARCHAR(50) NOT NULL, 
    [HasColour] BIT NULL, 
    [RequireColour] BIT NULL, 
    [HasRaw] BIT NULL, 
    [RequireRaw] BIT NULL, 
    [HasTractor] BIT NULL, 
    [RequireTractor] BIT NULL
)
