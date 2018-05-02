CREATE TABLE [dbo].[tblDepartmentPrinterReport]
(
	[DepartmentPrinterFunctionID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [DepartmentPrinterID] UNIQUEIDENTIFIER NOT NULL, 
    [ReportPrintReferenceID] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_tblDepartmentPrinterReport_ToTable] FOREIGN KEY ([ReportPrintReferenceID]) REFERENCES [tblReportPrintReference]([ReportPrintReferenceID]), 
    CONSTRAINT [FK_tblDepartmentPrinterReport_ToTable_1] FOREIGN KEY ([DepartmentPrinterID]) REFERENCES [tblDepartmentPrinters]([DepartmentPrinterID]) 
)
