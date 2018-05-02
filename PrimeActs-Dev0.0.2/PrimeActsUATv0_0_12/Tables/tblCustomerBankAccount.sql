CREATE TABLE [dbo].[tblCustomerBankAccount] (
    [CustomerBankAccountID] UNIQUEIDENTIFIER NOT NULL,
    [BankAccountID]         UNIQUEIDENTIFIER NOT NULL,
    [CustomerID]            UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentID]  UNIQUEIDENTIFIER NULL,
    [CustomerLocationID]    UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]           DATETIME         NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    [UpdatedDate]           DATETIME         NULL,
    [IsActive]              BIT              NOT NULL,
    CONSTRAINT [PK_tblCustomerBankAccount] PRIMARY KEY CLUSTERED ([CustomerBankAccountID] ASC),
    CONSTRAINT [FK_tblCustomerBankAccount_tblBankAccount] FOREIGN KEY ([BankAccountID]) REFERENCES [dbo].[tblBankAccount] ([BankAccountID]),
    CONSTRAINT [FK_tblCustomerBankAccount_tblCustomer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID]),
    CONSTRAINT [FK_tblCustomerBankAccount_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblCustomerBankAccount_tblCustomerLocation] FOREIGN KEY ([CustomerLocationID]) REFERENCES [dbo].[tblCustomerLocation] ([CustomerLocationID]),
    CONSTRAINT [FK_tblCustomerBankAccountCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerBankAccountUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

