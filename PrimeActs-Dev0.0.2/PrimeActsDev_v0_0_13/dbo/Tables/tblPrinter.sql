<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblPrinter] (
    [PrinterID]       UNIQUEIDENTIFIER NOT NULL,
    [PrinterName]     NVARCHAR (50)    NOT NULL,
    [NetworkName]     NVARCHAR (100)   NOT NULL,
    [DefaultOrder]    INT              NOT NULL,
    [IsActive]        BIT              DEFAULT ((1)) NOT NULL,
    [IsColour]        BIT              DEFAULT ((0)) NOT NULL,
    [IsRaw]           BIT              DEFAULT ((0)) NOT NULL,
    [HasTractor]      BIT              DEFAULT ((0)) NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]     DATETIME         NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    [UpdatedDate]     DATETIME         NULL,
    CONSTRAINT [PK_tblPrinter] PRIMARY KEY CLUSTERED ([PrinterID] ASC),
    CONSTRAINT [FK_tblPrinterCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPrinterUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblPrinter] (
    [PrinterID]       UNIQUEIDENTIFIER NOT NULL,
    [PrinterName]     NVARCHAR (50)    NOT NULL,
    [NetworkName]     NVARCHAR (100)   NOT NULL,
    [DefaultOrder]    INT              NOT NULL,
    [IsActive]        BIT              DEFAULT ((1)) NOT NULL,
    [IsColour]        BIT              DEFAULT ((0)) NOT NULL,
    [IsRaw]           BIT              DEFAULT ((0)) NOT NULL,
    [HasTractor]      BIT              DEFAULT ((0)) NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]     DATETIME         NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    [UpdatedDate]     DATETIME         NULL,
    CONSTRAINT [PK_tblPrinter] PRIMARY KEY CLUSTERED ([PrinterID] ASC),
    CONSTRAINT [FK_tblPrinterCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPrinterUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
