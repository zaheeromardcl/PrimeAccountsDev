<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpLedgerEntryType] (
    [LedgerEntryTypeID]          UNIQUEIDENTIFIER NOT NULL,
    [LedgerEntryTypeDescription] NVARCHAR (50)    NOT NULL,
    [LedgerEntryTypeNumber]      INT              NOT NULL,
    [IsActive]                   BIT              CONSTRAINT [DF__tlkpLedge__IsAct__7BE56230] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblSalesLedgerEntryType] PRIMARY KEY CLUSTERED ([LedgerEntryTypeID] ASC)
);

=======
﻿CREATE TABLE [dbo].[tlkpLedgerEntryType] (
    [LedgerEntryTypeID]          UNIQUEIDENTIFIER NOT NULL,
    [LedgerEntryTypeDescription] NVARCHAR (50)    NOT NULL,
    [LedgerEntryTypeNumber]      INT              NOT NULL,
    [IsActive]                   BIT              CONSTRAINT [DF__tlkpLedge__IsAct__7BE56230] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblSalesLedgerEntryType] PRIMARY KEY CLUSTERED ([LedgerEntryTypeID] ASC)
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
