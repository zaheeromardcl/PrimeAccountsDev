<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblDepartmentPrintTask] (
    [DepartmentPrintTaskID] UNIQUEIDENTIFIER NOT NULL,
    [DepartmentPrinterID]   UNIQUEIDENTIFIER NOT NULL,
    [PrintTaskID]           UNIQUEIDENTIFIER NOT NULL,
    [Preference]            INT              NOT NULL,
    [PrinterStationeryID]   UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([DepartmentPrintTaskID] ASC),
    CONSTRAINT [FK_tblDepartmentPrintTask_PrinterStationery] FOREIGN KEY ([PrinterStationeryID]) REFERENCES [dbo].[tblPrinterStationery] ([PrinterStationeryID]),
    CONSTRAINT [FK_tblDepartmentPrintTask_tblDepartmentPrinter] FOREIGN KEY ([DepartmentPrinterID]) REFERENCES [dbo].[tblDepartmentPrinter] ([DepartmentPrinterID]),
    CONSTRAINT [FK_tblDepartmentPrintTask_tblPrintTask] FOREIGN KEY ([PrintTaskID]) REFERENCES [dbo].[tblPrintTask] ([PrintTaskID])
);

=======
﻿CREATE TABLE [dbo].[tblDepartmentPrintTask] (
    [DepartmentPrintTaskID] UNIQUEIDENTIFIER NOT NULL,
    [DepartmentPrinterID]   UNIQUEIDENTIFIER NOT NULL,
    [PrintTaskID]           UNIQUEIDENTIFIER NOT NULL,
    [Preference]            INT              NOT NULL,
    [PrinterStationeryID]   UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([DepartmentPrintTaskID] ASC),
    CONSTRAINT [FK_tblDepartmentPrintTask_PrinterStationery] FOREIGN KEY ([PrinterStationeryID]) REFERENCES [dbo].[tblPrinterStationery] ([PrinterStationeryID]),
    CONSTRAINT [FK_tblDepartmentPrintTask_tblDepartmentPrinter] FOREIGN KEY ([DepartmentPrinterID]) REFERENCES [dbo].[tblDepartmentPrinter] ([DepartmentPrinterID]),
    CONSTRAINT [FK_tblDepartmentPrintTask_tblPrintTask] FOREIGN KEY ([PrintTaskID]) REFERENCES [dbo].[tblPrintTask] ([PrintTaskID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
