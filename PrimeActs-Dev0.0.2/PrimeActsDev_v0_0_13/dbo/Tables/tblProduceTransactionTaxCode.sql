<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblProduceTransactionTaxCode] (
    [ProduceTransactionTaxCodeID] UNIQUEIDENTIFIER NOT NULL,
    [ProduceID]                   UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxCodeID]        UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxLocationID]    UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                 DATETIME         NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                 DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ProduceTransactionTaxCodeID] ASC),
    CONSTRAINT [FK_tblProduceTransactionTaxCodeCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblProduceTransactionTaxCodeUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [IX_tblProduceTransactionTaxCode_ProduceIDTTaxLocationID] UNIQUE NONCLUSTERED ([ProduceID] ASC, [TransactionTaxLocationID] ASC)
);

=======
﻿CREATE TABLE [dbo].[tblProduceTransactionTaxCode] (
    [ProduceTransactionTaxCodeID] UNIQUEIDENTIFIER NOT NULL,
    [ProduceID]                   UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxCodeID]        UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxLocationID]    UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                 DATETIME         NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                 DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ProduceTransactionTaxCodeID] ASC),
    CONSTRAINT [FK_tblProduceTransactionTaxCodeCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblProduceTransactionTaxCodeUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [IX_tblProduceTransactionTaxCode_ProduceIDTTaxLocationID] UNIQUE NONCLUSTERED ([ProduceID] ASC, [TransactionTaxLocationID] ASC)
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
