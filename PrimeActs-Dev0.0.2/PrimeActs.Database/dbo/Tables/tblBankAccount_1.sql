CREATE TABLE [dbo].[tblBankAccount] (
    [BankAccountID] UNIQUEIDENTIFIER NOT NULL,
    [AccountName]   NVARCHAR (50)    NULL,
    [SortCode1]     TINYINT          NULL,
    [SortCode2]     TINYINT          NULL,
    [SortCode3]     TINYINT          NULL,
    [AccountNumber] INT              NULL,
    [IBAN]          VARCHAR (40)     NULL,
    [SWIFT]         VARCHAR (15)     NULL,
    [UpdatedBy]     NVARCHAR (25)    NULL,
    [UpdatedDate]   DATETIME         NULL,
    [CreatedBy]     NVARCHAR (25)    NULL,
    [CreatedDate]   DATETIME         NULL,
    [IsActive]      BIT              CONSTRAINT [DF_tblBankAccount_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblBankAccount] PRIMARY KEY CLUSTERED ([BankAccountID] ASC)
);

