<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpPackWtUnit] (
    [PackWtUnitID]    UNIQUEIDENTIFIER NOT NULL,
    [WtUnit]          NVARCHAR (10)    NULL,
    [KgMultiple]      NUMERIC (10, 4)  NULL,
    [CompanyID]       UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedDate]     DATETIME         NULL,
    [IsActive]        BIT              CONSTRAINT [DF_tlkpPackWtUnit_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpPackWtUnit] PRIMARY KEY CLUSTERED ([PackWtUnitID] ASC),
    CONSTRAINT [FK_tlkpPackWtUnit_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpPackWtUnitCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpPackWtUnitUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tlkpPackWtUnit] (
    [PackWtUnitID]    UNIQUEIDENTIFIER NOT NULL,
    [WtUnit]          NVARCHAR (10)    NULL,
    [KgMultiple]      NUMERIC (10, 4)  NULL,
    [CompanyID]       UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedDate]     DATETIME         NULL,
    [IsActive]        BIT              CONSTRAINT [DF_tlkpPackWtUnit_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpPackWtUnit] PRIMARY KEY CLUSTERED ([PackWtUnitID] ASC),
    CONSTRAINT [FK_tlkpPackWtUnit_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpPackWtUnitCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpPackWtUnitUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
