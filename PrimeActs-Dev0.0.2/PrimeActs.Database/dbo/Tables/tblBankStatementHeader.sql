CREATE TABLE [dbo].[tblBankStatementHeader] (
    [BankStatementID]   UNIQUEIDENTIFIER NOT NULL,
    [BankStatementImportDate] DATETIME         NOT NULL,
    [BankStatementFileName]   NVARCHAR (100)   NOT NULL,
	[BankStatementReconciled] BIT   NOT NULL DEFAULT 0,   
    [UpdatedBy]               UNIQUEIDENTIFIER    NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedBy]               UNIQUEIDENTIFIER    NULL,
    [CreatedDate]             DATETIME         NULL,    
    CONSTRAINT [PK_tblBankStatement_1] PRIMARY KEY CLUSTERED ([BankStatementID] ASC)
);
