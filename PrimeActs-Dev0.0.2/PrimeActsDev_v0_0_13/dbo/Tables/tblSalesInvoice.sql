<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblSalesInvoice] (
    [SalesInvoiceID]              UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentID]        UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentAddressID] UNIQUEIDENTIFIER NULL,
    [ServerCode]                  NVARCHAR (1)     NOT NULL,
    [SalesInvoiceReference]       NVARCHAR (20)    NOT NULL,
    [SalesInvoiceDate]            DATETIME         NOT NULL,
    [DivisionAddressID]           UNIQUEIDENTIFIER NULL,
    [CurrencyID]                  UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                NUMERIC (16, 4)  NULL,
    [NoteID]                      UNIQUEIDENTIFIER NULL,
    [DivisionID]                  UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                 DATETIME         NULL,
    [CreatedDate]                 DATETIME         NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSalesInvoice] PRIMARY KEY CLUSTERED ([SalesInvoiceID] ASC),
    CONSTRAINT [FK_tblSalesInvoice_tblAddress] FOREIGN KEY ([CustomerDepartmentAddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblSalesInvoice_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblSalesInvoice_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblSalesInvoice_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSalesInvoice_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblSalesInvoiceCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSalesInvoiceUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblSalesInvoice] (
    [SalesInvoiceID]              UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentID]        UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentAddressID] UNIQUEIDENTIFIER NULL,
    [ServerCode]                  NVARCHAR (1)     NOT NULL,
    [SalesInvoiceReference]       NVARCHAR (20)    NOT NULL,
    [SalesInvoiceDate]            DATETIME         NOT NULL,
    [DivisionAddressID]           UNIQUEIDENTIFIER NULL,
    [CurrencyID]                  UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                NUMERIC (16, 4)  NULL,
    [NoteID]                      UNIQUEIDENTIFIER NULL,
    [DivisionID]                  UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                 DATETIME         NULL,
    [CreatedDate]                 DATETIME         NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSalesInvoice] PRIMARY KEY CLUSTERED ([SalesInvoiceID] ASC),
    CONSTRAINT [FK_tblSalesInvoice_tblAddress] FOREIGN KEY ([CustomerDepartmentAddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblSalesInvoice_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblSalesInvoice_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblSalesInvoice_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSalesInvoice_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblSalesInvoiceCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSalesInvoiceUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
