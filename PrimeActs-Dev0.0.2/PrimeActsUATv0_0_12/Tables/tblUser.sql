﻿CREATE TABLE [dbo].[tblUser] (
    [UserID]                 UNIQUEIDENTIFIER NOT NULL,
    [Firstname]              NVARCHAR (MAX)   NULL,
    [Lastname]               NVARCHAR (MAX)   NULL,
    [Nickname]               NVARCHAR (MAX)   NULL,
    [IsActive]               BIT              NOT NULL,
    [LastLoggedOn]           DATETIME         NULL,
    [Email]                  NVARCHAR (256)   NULL,
    [EmailConfirmed]         BIT              NOT NULL,
    [PasswordHash]           NVARCHAR (MAX)   NULL,
    [AdminPasswordHash]      NVARCHAR (MAX)   NULL,
    [SecurityStamp]          NVARCHAR (MAX)   NULL,
    [PhoneNumber]            NVARCHAR (MAX)   NULL,
    [PhoneNumberConfirmed]   BIT              NOT NULL,
    [TwoFactorEnabled]       BIT              NOT NULL,
    [LockoutEndDateUtc]      DATETIME         NULL,
    [LockoutEnabled]         BIT              NOT NULL,
    [AccessFailedCount]      INT              NOT NULL,
    [UserName]               NVARCHAR (256)   NOT NULL,
    [DefaultDepartmentID]    UNIQUEIDENTIFIER NULL,
    [DefaultCompanyID]       UNIQUEIDENTIFIER NULL,
    [DefaultDivisionID]      UNIQUEIDENTIFIER NULL,
    [LastLoggedOnServerCode] NVARCHAR (1)     NULL,
    [CreatedByUserID]        UNIQUEIDENTIFIER NULL,
    [CreatedDate]            DATETIME         NULL,
    [UpdatedByUserID]        UNIQUEIDENTIFIER NULL,
    [UpdatedDate]            DATETIME         NULL,
    CONSTRAINT [PK_tblApplicationUser] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_tblUserCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblUserUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

