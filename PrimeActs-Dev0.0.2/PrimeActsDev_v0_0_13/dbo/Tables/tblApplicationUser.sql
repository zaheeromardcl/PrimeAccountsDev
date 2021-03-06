<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblApplicationUser] (
    [Firstname]            NVARCHAR (MAX)   NULL,
    [Lastname]             NVARCHAR (MAX)   NULL,
    [Nickname]             NVARCHAR (MAX)   NULL,
    [LastLoggedOn]         DATETIME         NULL,
    [Email]                NVARCHAR (256)   NULL,
    [EmailConfirmed]       BIT              NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)   NULL,
    [AdminPasswordHash]    NVARCHAR (MAX)   NULL,
    [SecurityStamp]        NVARCHAR (MAX)   NULL,
    [PhoneNumber]          NVARCHAR (MAX)   NULL,
    [PhoneNumberConfirmed] BIT              NOT NULL,
    [TwoFactorEnabled]     BIT              NOT NULL,
    [LockoutEndDateUtc]    DATETIME         NULL,
    [LockoutEnabled]       BIT              NOT NULL,
    [AccessFailedCount]    INT              NOT NULL,
    [UserName]             NVARCHAR (256)   NOT NULL,
    [DepartmentId]         UNIQUEIDENTIFIER NULL,
    [CompanyId]            UNIQUEIDENTIFIER NULL,
    [DivisionId]           UNIQUEIDENTIFIER NULL
);

=======
﻿CREATE TABLE [dbo].[tblApplicationUser] (
    [Firstname]            NVARCHAR (MAX)   NULL,
    [Lastname]             NVARCHAR (MAX)   NULL,
    [Nickname]             NVARCHAR (MAX)   NULL,
    [LastLoggedOn]         DATETIME         NULL,
    [Email]                NVARCHAR (256)   NULL,
    [EmailConfirmed]       BIT              NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)   NULL,
    [AdminPasswordHash]    NVARCHAR (MAX)   NULL,
    [SecurityStamp]        NVARCHAR (MAX)   NULL,
    [PhoneNumber]          NVARCHAR (MAX)   NULL,
    [PhoneNumberConfirmed] BIT              NOT NULL,
    [TwoFactorEnabled]     BIT              NOT NULL,
    [LockoutEndDateUtc]    DATETIME         NULL,
    [LockoutEnabled]       BIT              NOT NULL,
    [AccessFailedCount]    INT              NOT NULL,
    [UserName]             NVARCHAR (256)   NOT NULL,
    [DepartmentId]         UNIQUEIDENTIFIER NULL,
    [CompanyId]            UNIQUEIDENTIFIER NULL,
    [DivisionId]           UNIQUEIDENTIFIER NULL
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
