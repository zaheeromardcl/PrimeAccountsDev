CREATE TABLE [dbo].[tblDepartmentPrinter] (
    [DepartmentPrinterID]        UNIQUEIDENTIFIER NOT NULL,
    [DepartmentID]               UNIQUEIDENTIFIER NOT NULL,
    [PrinterID]                  UNIQUEIDENTIFIER NOT NULL,
    [Preference]                 INT              DEFAULT ((0)) NOT NULL,
    [DefaultPrinterStationeryID] UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]            UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                DATETIME         NULL,
    [UpdatedByUserID]            UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([DepartmentPrinterID] ASC),
    CONSTRAINT [FK_tblDepartmentPrinter_Department] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblDepartmentPrinter_Printer] FOREIGN KEY ([PrinterID]) REFERENCES [dbo].[tblPrinter] ([PrinterID]),
    CONSTRAINT [FK_tblDepartmentPrinter_PrinterStationery] FOREIGN KEY ([DefaultPrinterStationeryID]) REFERENCES [dbo].[tblPrinterStationery] ([PrinterStationeryID]),
    CONSTRAINT [FK_tblDepartmentPrinterCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblDepartmentPrinterUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

