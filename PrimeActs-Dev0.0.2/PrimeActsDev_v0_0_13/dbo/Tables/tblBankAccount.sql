<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblBankAccount] (
    [BankAccountID] UNIQUEIDENTIFIER NOT NULL,
    [AccountName]   NVARCHAR (50)    NULL,
    [AccountNumber] NVARCHAR (20)    NULL,
    [IBAN]          NVARCHAR (40)    NULL,
    [SWIFT]         NVARCHAR (15)    NULL,
    [BankCode]      NVARCHAR (12)    NULL,
    [CountryID]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblBankAccount] PRIMARY KEY CLUSTERED ([BankAccountID] ASC),
    CONSTRAINT [FK_tblBankAccount_tlkpCoutry] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[tlkpCountry] ([CountryID])
);

=======
﻿CREATE TABLE [dbo].[tblBankAccount] (
    [BankAccountID] UNIQUEIDENTIFIER NOT NULL,
    [AccountName]   NVARCHAR (50)    NULL,
    [AccountNumber] NVARCHAR (20)    NULL,
    [IBAN]          NVARCHAR (40)    NULL,
    [SWIFT]         NVARCHAR (15)    NULL,
    [BankCode]      NVARCHAR (12)    NULL,
    [CountryID]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblBankAccount] PRIMARY KEY CLUSTERED ([BankAccountID] ASC),
    CONSTRAINT [FK_tblBankAccount_tlkpCoutry] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[tlkpCountry] ([CountryID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
