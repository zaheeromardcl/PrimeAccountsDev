<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblSupplierBankAccount] (
    [SupplierBankAccountID] UNIQUEIDENTIFIER NOT NULL,
    [BankAccountID]         UNIQUEIDENTIFIER NOT NULL,
    [SupplierID]            UNIQUEIDENTIFIER NULL,
    [SupplierDepartmentID]  UNIQUEIDENTIFIER NULL,
    [SupplierLocationID]    UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]           DATETIME         NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    [UpdatedDate]           DATETIME         NULL,
    [IsActive]              BIT              NOT NULL,
    [UseToPayInvoice]       BIT              NOT NULL,
    CONSTRAINT [PK_tblSupplierBankAccount] PRIMARY KEY CLUSTERED ([SupplierBankAccountID] ASC),
    CONSTRAINT [FK_tblSupplierBankAccount_tblBankAccount] FOREIGN KEY ([BankAccountID]) REFERENCES [dbo].[tblBankAccount] ([BankAccountID]),
    CONSTRAINT [FK_tblSupplierBankAccount_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblSupplierBankAccount_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblSupplierBankAccount_tblSupplierLocation] FOREIGN KEY ([SupplierLocationID]) REFERENCES [dbo].[tblSupplierLocation] ([SupplierLocationID]),
    CONSTRAINT [FK_tblSupplierBankAccountCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierBankAccountUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblSupplierBankAccount] (
    [SupplierBankAccountID] UNIQUEIDENTIFIER NOT NULL,
    [BankAccountID]         UNIQUEIDENTIFIER NOT NULL,
    [SupplierID]            UNIQUEIDENTIFIER NULL,
    [SupplierDepartmentID]  UNIQUEIDENTIFIER NULL,
    [SupplierLocationID]    UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]           DATETIME         NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    [UpdatedDate]           DATETIME         NULL,
    [IsActive]              BIT              NOT NULL,
    [UseToPayInvoice]       BIT              NOT NULL,
    CONSTRAINT [PK_tblSupplierBankAccount] PRIMARY KEY CLUSTERED ([SupplierBankAccountID] ASC),
    CONSTRAINT [FK_tblSupplierBankAccount_tblBankAccount] FOREIGN KEY ([BankAccountID]) REFERENCES [dbo].[tblBankAccount] ([BankAccountID]),
    CONSTRAINT [FK_tblSupplierBankAccount_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblSupplierBankAccount_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblSupplierBankAccount_tblSupplierLocation] FOREIGN KEY ([SupplierLocationID]) REFERENCES [dbo].[tblSupplierLocation] ([SupplierLocationID]),
    CONSTRAINT [FK_tblSupplierBankAccountCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierBankAccountUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
