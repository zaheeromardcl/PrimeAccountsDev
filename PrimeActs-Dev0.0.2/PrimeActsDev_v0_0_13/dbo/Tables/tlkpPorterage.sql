<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpPorterage] (
    [PorterageID]     UNIQUEIDENTIFIER NOT NULL,
    [PorterageCode]   NVARCHAR (50)    NOT NULL,
    [UnitPrice]       NUMERIC (18, 4)  NOT NULL,
    [MinimumAmount]   NUMERIC (18, 4)  NULL,
    [DepartmentID]    UNIQUEIDENTIFIER NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedDate]     DATETIME         NULL,
    [IsActive]        BIT              CONSTRAINT [DF__tblPorter__IsAct__22751F6C] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    [CompanyID]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblPorterage] PRIMARY KEY CLUSTERED ([PorterageID] ASC),
    CONSTRAINT [FK_tlkpPorterage_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpPorterageCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpPorterageUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tlkpPorterage] (
    [PorterageID]     UNIQUEIDENTIFIER NOT NULL,
    [PorterageCode]   NVARCHAR (50)    NOT NULL,
    [UnitPrice]       NUMERIC (18, 4)  NOT NULL,
    [MinimumAmount]   NUMERIC (18, 4)  NULL,
    [DepartmentID]    UNIQUEIDENTIFIER NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedDate]     DATETIME         NULL,
    [IsActive]        BIT              CONSTRAINT [DF__tblPorter__IsAct__22751F6C] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    [CompanyID]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblPorterage] PRIMARY KEY CLUSTERED ([PorterageID] ASC),
    CONSTRAINT [FK_tlkpPorterage_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpPorterageCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpPorterageUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
