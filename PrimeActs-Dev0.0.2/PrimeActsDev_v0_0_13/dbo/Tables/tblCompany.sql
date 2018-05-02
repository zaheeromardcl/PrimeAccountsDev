<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblCompany] (
    [CompanyID]           UNIQUEIDENTIFIER NOT NULL,
    [ParentCompanyID]     UNIQUEIDENTIFIER NULL,
    [CompanyName]         NVARCHAR (50)    NOT NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedDate]         DATETIME         NULL,
    [AddressID]           UNIQUEIDENTIFIER NULL,
    [RegisteredAddressID] UNIQUEIDENTIFIER NULL,
    [CompanyNo]           NVARCHAR (20)    NULL,
    [Logo]                VARBINARY (MAX)  NULL,
    [Telephone]           NVARCHAR (20)    NULL,
    [FaxNo]               NVARCHAR (20)    NULL,
    [EmailAddress]        NVARCHAR (150)   NULL,
    [Website]             NVARCHAR (50)    NULL,
    [InvoiceInfo]         NTEXT            NULL,
    [IsActive]            BIT              CONSTRAINT [DF__tblCompan__IsAct__286302EC] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblCompany_1] PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    CONSTRAINT [FK_tblCompany_tblAddress_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblCompany_tblAddress_RegisteredAddressID] FOREIGN KEY ([RegisteredAddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblCompany_tblCompany] FOREIGN KEY ([ParentCompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblCompanyCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCompanyUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblCompany] (
    [CompanyID]           UNIQUEIDENTIFIER NOT NULL,
    [ParentCompanyID]     UNIQUEIDENTIFIER NULL,
    [CompanyName]         NVARCHAR (50)    NOT NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedDate]         DATETIME         NULL,
    [AddressID]           UNIQUEIDENTIFIER NULL,
    [RegisteredAddressID] UNIQUEIDENTIFIER NULL,
    [CompanyNo]           NVARCHAR (20)    NULL,
    [Logo]                VARBINARY (MAX)  NULL,
    [Telephone]           NVARCHAR (20)    NULL,
    [FaxNo]               NVARCHAR (20)    NULL,
    [EmailAddress]        NVARCHAR (150)   NULL,
    [Website]             NVARCHAR (50)    NULL,
    [InvoiceInfo]         NTEXT            NULL,
    [IsActive]            BIT              CONSTRAINT [DF__tblCompan__IsAct__286302EC] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblCompany_1] PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    CONSTRAINT [FK_tblCompany_tblAddress_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblCompany_tblAddress_RegisteredAddressID] FOREIGN KEY ([RegisteredAddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblCompany_tblCompany] FOREIGN KEY ([ParentCompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblCompanyCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCompanyUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
