<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpTransactionTaxRate] (
    [TransactionTaxRateID]         UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxRatePercentage] NUMERIC (6, 4)   NOT NULL,
    [StartDate]                    DATE             NOT NULL,
    [UpdatedDate]                  DATETIME         NULL,
    [CreatedDate]                  DATETIME         NULL,
    [TransactionTaxCodeID]         UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]              UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]              UNIQUEIDENTIFIER NULL,
    [MinimumUnitCost]              NUMERIC (18, 4)  NULL,
    CONSTRAINT [PK_tblVATRate_1] PRIMARY KEY CLUSTERED ([TransactionTaxRateID] ASC),
    CONSTRAINT [FK_tlkpTransactionTaxRateCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpTransactionTaxRateUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpVATRate_tlkpVATCode] FOREIGN KEY ([TransactionTaxCodeID]) REFERENCES [dbo].[tlkpTransactionTaxCode] ([TransactionTaxCodeID])
);

=======
﻿CREATE TABLE [dbo].[tlkpTransactionTaxRate] (
    [TransactionTaxRateID]         UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxRatePercentage] NUMERIC (6, 4)   NOT NULL,
    [StartDate]                    DATE             NOT NULL,
    [UpdatedDate]                  DATETIME         NULL,
    [CreatedDate]                  DATETIME         NULL,
    [TransactionTaxCodeID]         UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]              UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]              UNIQUEIDENTIFIER NULL,
    [MinimumUnitCost]              NUMERIC (18, 4)  NULL,
    CONSTRAINT [PK_tblVATRate_1] PRIMARY KEY CLUSTERED ([TransactionTaxRateID] ASC),
    CONSTRAINT [FK_tlkpTransactionTaxRateCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpTransactionTaxRateUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpVATRate_tlkpVATCode] FOREIGN KEY ([TransactionTaxCodeID]) REFERENCES [dbo].[tlkpTransactionTaxCode] ([TransactionTaxCodeID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
