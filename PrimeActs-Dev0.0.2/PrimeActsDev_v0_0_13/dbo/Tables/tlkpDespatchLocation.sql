<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpDespatchLocation] (
    [DespatchLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [DespatchLocationCode] NVARCHAR (10)    NOT NULL,
    [DespatchLocationName] NVARCHAR (100)   NOT NULL,
    [CompanyID]            UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]          DATETIME         NULL,
    [CreatedDate]          DATETIME         NULL,
    [IsActive]             BIT              CONSTRAINT [DF_tlkpDespatchLocation_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]      UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]      UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpDespatch] PRIMARY KEY CLUSTERED ([DespatchLocationID] ASC),
    CONSTRAINT [FK_tlkpDespatchLocation_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpDespatchLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpDespatchLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tlkpDespatchLocation] (
    [DespatchLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [DespatchLocationCode] NVARCHAR (10)    NOT NULL,
    [DespatchLocationName] NVARCHAR (100)   NOT NULL,
    [CompanyID]            UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]          DATETIME         NULL,
    [CreatedDate]          DATETIME         NULL,
    [IsActive]             BIT              CONSTRAINT [DF_tlkpDespatchLocation_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]      UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]      UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpDespatch] PRIMARY KEY CLUSTERED ([DespatchLocationID] ASC),
    CONSTRAINT [FK_tlkpDespatchLocation_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpDespatchLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpDespatchLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
