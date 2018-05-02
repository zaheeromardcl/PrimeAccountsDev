<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpCountry] (
    [CountryID]               UNIQUEIDENTIFIER NOT NULL,
    [CountryName]             NVARCHAR (50)    NOT NULL,
    [CountryCode]             NVARCHAR (10)    NOT NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedDate]             DATETIME         NULL,
    [IsActive]                BIT              CONSTRAINT [DF__tlkpCount__IsAct__3CF40B7E] DEFAULT ((1)) NOT NULL,
    [CompanyID]               UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]         UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]         UNIQUEIDENTIFIER NULL,
    [BankCodeFormat]          NVARCHAR (20)    NULL,
    [BankAccountNumberFormat] NVARCHAR (30)    NULL,
    CONSTRAINT [PK_tlkpCountry] PRIMARY KEY CLUSTERED ([CountryID] ASC),
    CONSTRAINT [FK_tlkpCountry_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpCountryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpCountryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tlkpCountry] (
    [CountryID]               UNIQUEIDENTIFIER NOT NULL,
    [CountryName]             NVARCHAR (50)    NOT NULL,
    [CountryCode]             NVARCHAR (10)    NOT NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedDate]             DATETIME         NULL,
    [IsActive]                BIT              CONSTRAINT [DF__tlkpCount__IsAct__3CF40B7E] DEFAULT ((1)) NOT NULL,
    [CompanyID]               UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]         UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]         UNIQUEIDENTIFIER NULL,
    [BankCodeFormat]          NVARCHAR (20)    NULL,
    [BankAccountNumberFormat] NVARCHAR (30)    NULL,
    CONSTRAINT [PK_tlkpCountry] PRIMARY KEY CLUSTERED ([CountryID] ASC),
    CONSTRAINT [FK_tlkpCountry_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpCountryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpCountryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
