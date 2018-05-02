CREATE TABLE [dbo].[tblDepartmentPrinter]
(
	[DepartmentPrinterID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [DepartmentID] UNIQUEIDENTIFIER NOT NULL, 
    [PrinterID] UNIQUEIDENTIFIER NOT NULL, 
    [Preference] INT NOT NULL DEFAULT 0, 
    [PrinterStationeryID] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_tblDepartmentPrinter_Department] FOREIGN KEY ([DepartmentID]) REFERENCES [tblDepartment]([DepartmentID]),
	CONSTRAINT [FK_tblDepartmentPrinter_Printer] FOREIGN KEY ([PrinterID]) REFERENCES [tblPrinter]([PrinterID]),
	CONSTRAINT [FK_tblDepartmentPrinter_PrinterStationery] FOREIGN KEY ([PrinterStationeryID]) REFERENCES [tblPrinterStationery]([PrinterStationeryID])
)
