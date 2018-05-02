<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpTransactionTaxCode] (
    [TransactionTaxCodeID]          UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxCode]            NVARCHAR (50)    NULL,
    [TransactionTaxCodeDescription] NVARCHAR (50)    NOT NULL,
    [UpdatedDate]                   DATETIME         NULL,
    [CreatedDate]                   DATETIME         NULL,
    [IsActive]                      BIT              CONSTRAINT [DF__tlkpVATCo__IsAct__3B40CD36] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]               UNIQUEIDENTIFIER NULL,
    [TransactionTaxLocationID]      UNIQUEIDENTIFIER NOT NULL,
    [RateSetBySaleDate]             BIT              NOT NULL,
    CONSTRAINT [PK_tblVATCode_1] PRIMARY KEY CLUSTERED ([TransactionTaxCodeID] ASC),
    CONSTRAINT [FK_tlkpTransactionTaxCode_tlkpTransactionTaxLocation] FOREIGN KEY ([TransactionTaxLocationID]) REFERENCES [dbo].[tlkpTransactionTaxLocation] ([TransactionTaxLocationID]),
    CONSTRAINT [FK_tlkpTransactionTaxCodeCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpTransactionTaxCodeUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tlkpTransactionTaxCode] (
    [TransactionTaxCodeID]          UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxCode]            NVARCHAR (50)    NULL,
    [TransactionTaxCodeDescription] NVARCHAR (50)    NOT NULL,
    [UpdatedDate]                   DATETIME         NULL,
    [CreatedDate]                   DATETIME         NULL,
    [IsActive]                      BIT              CONSTRAINT [DF__tlkpVATCo__IsAct__3B40CD36] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]               UNIQUEIDENTIFIER NULL,
    [TransactionTaxLocationID]      UNIQUEIDENTIFIER NOT NULL,
    [RateSetBySaleDate]             BIT              NOT NULL,
    CONSTRAINT [PK_tblVATCode_1] PRIMARY KEY CLUSTERED ([TransactionTaxCodeID] ASC),
    CONSTRAINT [FK_tlkpTransactionTaxCode_tlkpTransactionTaxLocation] FOREIGN KEY ([TransactionTaxLocationID]) REFERENCES [dbo].[tlkpTransactionTaxLocation] ([TransactionTaxLocationID]),
    CONSTRAINT [FK_tlkpTransactionTaxCodeCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpTransactionTaxCodeUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
