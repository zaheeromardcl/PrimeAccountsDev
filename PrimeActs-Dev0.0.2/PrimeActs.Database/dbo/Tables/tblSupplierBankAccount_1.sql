CREATE TABLE [dbo].[tblSupplierBankAccount] (
    [SupplierBankAccountID] UNIQUEIDENTIFIER NOT NULL,
    [BankAccountID]         UNIQUEIDENTIFIER NOT NULL,
    [SupplierID]            UNIQUEIDENTIFIER NULL,
    [SupplierDepartmentID]  UNIQUEIDENTIFIER NULL,
    [SupplierLocationID]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSupplierBankAccount] PRIMARY KEY CLUSTERED ([SupplierBankAccountID] ASC),
    CONSTRAINT [FK_tblSupplierBankAccount_tblBankAccount] FOREIGN KEY ([BankAccountID]) REFERENCES [dbo].[tblBankAccount] ([BankAccountID]),
    CONSTRAINT [FK_tblSupplierBankAccount_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblSupplierBankAccount_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblSupplierBankAccount_tblSupplierLocation] FOREIGN KEY ([SupplierLocationID]) REFERENCES [dbo].[tblSupplierLocation] ([SupplierLocationID])
);

