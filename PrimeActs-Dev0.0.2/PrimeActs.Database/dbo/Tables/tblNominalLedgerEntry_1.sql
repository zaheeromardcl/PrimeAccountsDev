CREATE TABLE [dbo].[tblNominalLedgerEntry] (
    [NominalLedgerEntryID]          UNIQUEIDENTIFIER NOT NULL,
    [BatchNumberLogID]                 UNIQUEIDENTIFIER NOT NULL,
    [NominalAccountID]              UNIQUEIDENTIFIER NOT NULL,
	[NominalLedgerEntryReference]   NVARCHAR (50)    NOT NULL,
    [NominalLedgerEntryAmount]      NUMERIC(18,4)            NOT NULL,
    [NominalLedgerEntryDate]        DATETIME         NOT NULL,
    [NominalLedgerEntryDescription] NVARCHAR (400)   NULL,
    [LedgerEntryTypeID]				UNIQUEIDENTIFIER NOT NULL,	
	[FXNominalLedgerEntryAmount]	NUMERIC(18,4)	 NULL,
	[CurrencyID]					UNIQUEIDENTIFIER NOT NULL,
	[ExchangeRate]					NUMERIC(18,4)	 NULL,
	[UpdatedBy]                     NVARCHAR (25)    NULL,
    [UpdatedDate]                   DATETIME         NULL,
    [CreatedBy]                     NVARCHAR (25)    NULL,
    [CreatedDate]                   DATETIME         NULL,
    [IsHistory]                     BIT              CONSTRAINT [DF__tblNomina__IsAct__6D9742D9] DEFAULT ((1)) NOT NULL,
    [AccountingYear]                INT              NOT NULL,
    [AccountingPeriod]              TINYINT          NOT NULL,
    CONSTRAINT [PK_tblNominalLedgerEntry] PRIMARY KEY CLUSTERED ([NominalLedgerEntryID] ASC),
    CONSTRAINT [FK_tblNominalLedgerEntry_tblBatchNumber] FOREIGN KEY ([BatchNumberLogID]) REFERENCES [dbo].[tblBatchNumberLog]([BatchNumberLogID]),
    CONSTRAINT [FK_tblNominalLedgerEntry_tblNominalAccount] FOREIGN KEY ([NominalAccountID]) REFERENCES [dbo].[tblNominalAccount]([NominalAccountID]),
	CONSTRAINT [FK_tblNominalLedgerEntry_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency]([CurrencyID])

	);

