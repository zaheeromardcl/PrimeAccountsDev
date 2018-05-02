<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpPort] (
    [PortID]          UNIQUEIDENTIFIER NOT NULL,
    [PortName]        NVARCHAR (100)   NOT NULL,
    [PortCode]        NVARCHAR (10)    NOT NULL,
    [CompanyID]       UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedDate]     DATETIME         NULL,
    [IsActive]        BIT              CONSTRAINT [DF_tlkpPort_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpPort] PRIMARY KEY CLUSTERED ([PortID] ASC),
    CONSTRAINT [FK_tlkpPort_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpPortCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpPortUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tlkpPort] (
    [PortID]          UNIQUEIDENTIFIER NOT NULL,
    [PortName]        NVARCHAR (100)   NOT NULL,
    [PortCode]        NVARCHAR (10)    NOT NULL,
    [CompanyID]       UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedDate]     DATETIME         NULL,
    [IsActive]        BIT              CONSTRAINT [DF_tlkpPort_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpPort] PRIMARY KEY CLUSTERED ([PortID] ASC),
    CONSTRAINT [FK_tlkpPort_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpPortCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpPortUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
