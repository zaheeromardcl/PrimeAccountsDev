CREATE TABLE [dbo].[tblDepartmentPrintTask]
(
	[DepartmentPrintTaskID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [DepartmentPrinterID] UNIQUEIDENTIFIER NOT NULL, 
    [PrintTaskID] UNIQUEIDENTIFIER NOT NULL, 
    [Preference] INT NOT NULL DEFAULT 0, 
    [PrinterStationeryID] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_tblDepartmentPrinterReport_ToTable] FOREIGN KEY ([PrintTaskID]) REFERENCES [tblPrintTask]([PrintTaskID]), 
    CONSTRAINT [FK_tblDepartmentPrinterReport_ToTable_1] FOREIGN KEY ([DepartmentPrinterID]) REFERENCES [tblDepartmentPrinter]([DepartmentPrinterID]),
	CONSTRAINT [FK_tblDepartmentPrinterReport_PrinterStationery] FOREIGN KEY ([PrinterStationeryID]) REFERENCES [tblPrinterStationery]([PrinterStationeryID])  
)
