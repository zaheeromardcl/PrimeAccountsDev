CREATE TABLE [dbo].[tblReportPrintReference]
(
	[ReportPrintReferenceID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ReportPrintReferenceName] VARCHAR(50) NOT NULL, 
    [RawPrintMode] BIT NOT NULL
)
