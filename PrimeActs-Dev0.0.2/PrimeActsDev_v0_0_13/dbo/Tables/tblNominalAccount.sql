<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblNominalAccount] (
    [NominalAccountID]   UNIQUEIDENTIFIER NOT NULL,
    [NominalAccountName] NVARCHAR (50)    NOT NULL,
    [NominalCode]        NVARCHAR (50)    NOT NULL,
    [IsPandL]            BIT              NOT NULL,
    [IsBroughtForward]   BIT              NOT NULL,
    [IsCurrent]          BIT              NOT NULL,
    [BankAccountID]      UNIQUEIDENTIFIER NULL,
    [IsPettyCashAccount] BIT              NOT NULL,
    [IsSystem]           BIT              NOT NULL,
    [UpdatedDate]        DATETIME         NULL,
    [CreatedDate]        DATETIME         NULL,
    [CompanyID]          UNIQUEIDENTIFIER NOT NULL,
    [IsActive]           BIT              CONSTRAINT [DF__tblNomina__IsAct__1DB06A4F] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]    UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblNominalAccount] PRIMARY KEY CLUSTERED ([NominalAccountID] ASC),
    CONSTRAINT [FK_tblNominalAccount_tblBankAccount] FOREIGN KEY ([BankAccountID]) REFERENCES [dbo].[tblBankAccount] ([BankAccountID]),
    CONSTRAINT [FK_tblNominalAccount_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblNominalAccountCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblNominalAccountUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblNominalAccount] (
    [NominalAccountID]   UNIQUEIDENTIFIER NOT NULL,
    [NominalAccountName] NVARCHAR (50)    NOT NULL,
    [NominalCode]        NVARCHAR (50)    NOT NULL,
    [IsPandL]            BIT              NOT NULL,
    [IsBroughtForward]   BIT              NOT NULL,
    [IsCurrent]          BIT              NOT NULL,
    [BankAccountID]      UNIQUEIDENTIFIER NULL,
    [IsPettyCashAccount] BIT              NOT NULL,
    [IsSystem]           BIT              NOT NULL,
    [UpdatedDate]        DATETIME         NULL,
    [CreatedDate]        DATETIME         NULL,
    [CompanyID]          UNIQUEIDENTIFIER NOT NULL,
    [IsActive]           BIT              CONSTRAINT [DF__tblNomina__IsAct__1DB06A4F] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]    UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblNominalAccount] PRIMARY KEY CLUSTERED ([NominalAccountID] ASC),
    CONSTRAINT [FK_tblNominalAccount_tblBankAccount] FOREIGN KEY ([BankAccountID]) REFERENCES [dbo].[tblBankAccount] ([BankAccountID]),
    CONSTRAINT [FK_tblNominalAccount_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblNominalAccountCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblNominalAccountUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
