CREATE TABLE [dbo].[tblPrinterStationery]
(
	[PrinterStationeryID] UNIQUEIDENTIFIER NOT NULL , 
    [PrinterID] UNIQUEIDENTIFIER NOT NULL, 
    [StationeryID] UNIQUEIDENTIFIER NOT NULL, 
    [DrawerNo] NCHAR(10) NULL, 
    [DrawerName] NVARCHAR(50) NULL, 
    PRIMARY KEY ([PrinterStationeryID]), 
    CONSTRAINT [FK_tblPrinterStationery_Stationery] FOREIGN KEY ([StationeryID]) REFERENCES [tblStationery]([StationeryID]),
	CONSTRAINT [FK_tblPrinterStationery_Printer] FOREIGN KEY ([PrinterID]) REFERENCES [tblPrinter]([PrinterID])
)
