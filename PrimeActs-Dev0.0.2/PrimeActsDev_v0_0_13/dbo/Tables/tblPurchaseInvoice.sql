<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblPurchaseInvoice] (
    [PurchaseInvoiceID]        UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]     UNIQUEIDENTIFIER NOT NULL,
    [AddressID]                UNIQUEIDENTIFIER NOT NULL,
    [PurchaseInvoiceReference] NVARCHAR (20)    NOT NULL,
    [ServerCode]               NVARCHAR (1)     NULL,
    [PurchaseInvoiceDate]      DATETIME         NOT NULL,
    [CurrencyID]               UNIQUEIDENTIFIER NULL,
    [ExchangeRate]             NUMERIC (16, 4)  NULL,
    [NoteID]                   UNIQUEIDENTIFIER NULL,
    [DivisionID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]              DATETIME         NULL,
    [CreatedDate]              DATETIME         NULL,
    [IsActivated]              BIT              NOT NULL,
    [Status]                   INT              NULL,
    [CreatedByUserID]          UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]          UNIQUEIDENTIFIER NULL,
    [SupplierBankAccountID]    UNIQUEIDENTIFIER NULL,
    [IsDeleted]                BIT              CONSTRAINT [DF__tblPurcha__IsDel__73DA2C14] DEFAULT ((0)) NOT NULL,
    [IncTaxCheckTotal]         NUMERIC (18, 4)  CONSTRAINT [DF_tblPurchaseInvoice_Total] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblPurchaseInvoice_1] PRIMARY KEY CLUSTERED ([PurchaseInvoiceID] ASC)
);

=======
﻿CREATE TABLE [dbo].[tblPurchaseInvoice] (
    [PurchaseInvoiceID]        UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]     UNIQUEIDENTIFIER NOT NULL,
    [AddressID]                UNIQUEIDENTIFIER NOT NULL,
    [PurchaseInvoiceReference] NVARCHAR (20)    NOT NULL,
    [ServerCode]               NVARCHAR (1)     NULL,
    [PurchaseInvoiceDate]      DATETIME         NOT NULL,
    [CurrencyID]               UNIQUEIDENTIFIER NULL,
    [ExchangeRate]             NUMERIC (16, 4)  NULL,
    [NoteID]                   UNIQUEIDENTIFIER NULL,
    [DivisionID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]              DATETIME         NULL,
    [CreatedDate]              DATETIME         NULL,
    [IsActivated]              BIT              NOT NULL,
    [Status]                   INT              NULL,
    [CreatedByUserID]          UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]          UNIQUEIDENTIFIER NULL,
    [SupplierBankAccountID]    UNIQUEIDENTIFIER NULL,
    [IsDeleted]                BIT              CONSTRAINT [DF__tblPurcha__IsDel__73DA2C14] DEFAULT ((0)) NOT NULL,
    [IncTaxCheckTotal]         NUMERIC (18, 4)  CONSTRAINT [DF_tblPurchaseInvoice_Total] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblPurchaseInvoice_1] PRIMARY KEY CLUSTERED ([PurchaseInvoiceID] ASC)
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
