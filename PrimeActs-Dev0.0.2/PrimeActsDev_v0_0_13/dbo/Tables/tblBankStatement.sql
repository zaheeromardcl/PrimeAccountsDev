<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblBankStatement] (
    [BankStatementID]          UNIQUEIDENTIFIER NOT NULL,
    [BankStatementImportDate]  DATETIME         NULL,
    [BankStatementFileName]    NVARCHAR (100)   NULL,
    [BankStatementReconciled]  BIT              DEFAULT ((0)) NOT NULL,
    [UpdatedDate]              DATETIME         NULL,
    [CreatedDate]              DATETIME         NULL,
    [BankAccountID]            UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]          UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]          UNIQUEIDENTIFIER NULL,
    [StartDate]                DATETIME         NULL,
    [EndDate]                  DATETIME         NULL,
    [BankStatementDescription] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_tblBankStatementHeader_1] PRIMARY KEY CLUSTERED ([BankStatementID] ASC),
    CONSTRAINT [FK_tblBankStatement_tblBankAccount] FOREIGN KEY ([BankAccountID]) REFERENCES [dbo].[tblBankAccount] ([BankAccountID]),
    CONSTRAINT [FK_tblBankStatementCreatedbyUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblBankStatementUpdatedbyUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblBankStatement] (
    [BankStatementID]          UNIQUEIDENTIFIER NOT NULL,
    [BankStatementImportDate]  DATETIME         NULL,
    [BankStatementFileName]    NVARCHAR (100)   NULL,
    [BankStatementReconciled]  BIT              DEFAULT ((0)) NOT NULL,
    [UpdatedDate]              DATETIME         NULL,
    [CreatedDate]              DATETIME         NULL,
    [BankAccountID]            UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]          UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]          UNIQUEIDENTIFIER NULL,
    [StartDate]                DATETIME         NULL,
    [EndDate]                  DATETIME         NULL,
    [BankStatementDescription] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_tblBankStatementHeader_1] PRIMARY KEY CLUSTERED ([BankStatementID] ASC),
    CONSTRAINT [FK_tblBankStatement_tblBankAccount] FOREIGN KEY ([BankAccountID]) REFERENCES [dbo].[tblBankAccount] ([BankAccountID]),
    CONSTRAINT [FK_tblBankStatementCreatedbyUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblBankStatementUpdatedbyUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
