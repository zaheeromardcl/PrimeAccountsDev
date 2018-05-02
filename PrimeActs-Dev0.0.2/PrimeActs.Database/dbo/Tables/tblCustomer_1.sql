CREATE TABLE [dbo].[tblCustomer] (
    [CustomerID]          UNIQUEIDENTIFIER NOT NULL,
    [ParentCustomerID]    UNIQUEIDENTIFIER NULL,
    [CustomerCompanyName] NVARCHAR (50)    NOT NULL,
    [CustomerCode]        NVARCHAR (10)    NOT NULL,
    [CreditLimitCash]     NUMERIC(18,4)            NULL,
    [CreditLimitInvoice]  NUMERIC(18,4)            NULL,
    [CreditRating]        UNIQUEIDENTIFIER NULL,
    [TransactionTaxNo]            NVARCHAR (20)    NULL,
    [Statements]          BIT              NULL,
    [NoteID]              UNIQUEIDENTIFIER NULL,
    [UpdatedBy]           NVARCHAR (25)    NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedBy]           NVARCHAR (25)    NULL,
    [CreatedDate]         DATETIME         NULL,
    [CompanyID]           UNIQUEIDENTIFIER NULL,
      [IsActive]            BIT              CONSTRAINT [DF__tblCustom__IsAct__14270015] DEFAULT ((1)) NOT NULL,
	   [IsTransfer]          INT              CONSTRAINT [DF_tblCustomer_IsTransfer] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [FK_tblCustomer_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblCustomer_tblCustomer] FOREIGN KEY ([ParentCustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID]),
    CONSTRAINT [FK_tblCustomer_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID])
);

