CREATE TABLE [dbo].[tblPurchaseLedgerEntry] (
    [PurchaseLedgerEntryID]          UNIQUEIDENTIFIER NOT NULL,
    [LedgerEntryTypeID]              UNIQUEIDENTIFIER NOT NULL,
    [PurchaseLedgerEntryDescription] NVARCHAR (100)   NOT NULL,
    [PurchaseAmount]                 NUMERIC(18,4)            NOT NULL,
    [FXPurchaseAmount]               FLOAT (53)       NOT NULL,
    [CurrencyID]                     UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                   NUMERIC (16, 4)  NULL,
    [BatchNumberLogID]               UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]           UNIQUEIDENTIFIER NOT NULL,
    [NoteID]                         UNIQUEIDENTIFIER NULL,
    [UpdatedBy]                      NVARCHAR (25)    NULL,
    [UpdatedDate]                    DATETIME         NULL,
    [CreatedBy]                      NVARCHAR (25)    NULL,
    [CreatedDate]                    DATETIME         NULL,
    [IsHistory]                      BIT              CONSTRAINT [DF__tblPurcha__IsAct__2A164134] DEFAULT ((1)) NOT NULL,
    [AccountingYear]                 INT              NOT NULL,
    [AccountingPeriod]               TINYINT          NOT NULL,
   
    CONSTRAINT [PK_tblPurchaseLedgerEntry_1] PRIMARY KEY CLUSTERED ([PurchaseLedgerEntryID] ASC),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tblBatchNumber] FOREIGN KEY ([BatchNumberLogID]) REFERENCES [dbo].[tblBatchNumberLog] ([BatchNumberLogID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tlkpLedgerEntryType] FOREIGN KEY ([LedgerEntryTypeID]) REFERENCES [dbo].[tlkpLedgerEntryType] ([LedgerEntryTypeID])
);

