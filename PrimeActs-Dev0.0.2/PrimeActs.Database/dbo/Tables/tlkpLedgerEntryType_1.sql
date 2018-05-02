CREATE TABLE [dbo].[tlkpLedgerEntryType] (
    [LedgerEntryTypeID]          UNIQUEIDENTIFIER NOT NULL,
    [LedgerEntryTypeDescription] NVARCHAR (50)    NOT NULL,
    [LedgerEntryTypeNumber]      TINYINT          NOT NULL,
    [UpdatedBy]                  NVARCHAR (25)    NULL,
    [UpdatedDate]                DATETIME         NULL,
    [CreatedBy]                  NVARCHAR (25)    NULL,
    [CreatedDate]                DATETIME         NULL,
    [IsActive]                   BIT              CONSTRAINT [DF__tlkpLedge__IsAct__7BE56230] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblSalesLedgerEntryType] PRIMARY KEY CLUSTERED ([LedgerEntryTypeID] ASC)
);

