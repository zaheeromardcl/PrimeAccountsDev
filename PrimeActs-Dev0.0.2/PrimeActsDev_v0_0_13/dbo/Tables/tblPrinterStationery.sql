<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblPrinterStationery] (
    [PrinterStationeryID] UNIQUEIDENTIFIER NOT NULL,
    [PrinterID]           UNIQUEIDENTIFIER NOT NULL,
    [StationeryID]        UNIQUEIDENTIFIER NOT NULL,
    [DrawerNumber]        NVARCHAR (10)    NULL,
    [DrawerName]          NVARCHAR (50)    NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]         DATETIME         NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    [UpdatedDate]         DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([PrinterStationeryID] ASC),
    CONSTRAINT [FK_tblPrinterStationery_Printer] FOREIGN KEY ([PrinterID]) REFERENCES [dbo].[tblPrinter] ([PrinterID]),
    CONSTRAINT [FK_tblPrinterStationery_Stationery] FOREIGN KEY ([StationeryID]) REFERENCES [dbo].[tblStationery] ([StationeryID]),
    CONSTRAINT [FK_tblPrinterStationeryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPrinterStationeryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblPrinterStationery] (
    [PrinterStationeryID] UNIQUEIDENTIFIER NOT NULL,
    [PrinterID]           UNIQUEIDENTIFIER NOT NULL,
    [StationeryID]        UNIQUEIDENTIFIER NOT NULL,
    [DrawerNumber]        NVARCHAR (10)    NULL,
    [DrawerName]          NVARCHAR (50)    NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]         DATETIME         NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    [UpdatedDate]         DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([PrinterStationeryID] ASC),
    CONSTRAINT [FK_tblPrinterStationery_Printer] FOREIGN KEY ([PrinterID]) REFERENCES [dbo].[tblPrinter] ([PrinterID]),
    CONSTRAINT [FK_tblPrinterStationery_Stationery] FOREIGN KEY ([StationeryID]) REFERENCES [dbo].[tblStationery] ([StationeryID]),
    CONSTRAINT [FK_tblPrinterStationeryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPrinterStationeryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
