<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblCustomer] (
    [CustomerID]          UNIQUEIDENTIFIER NOT NULL,
    [ParentCustomerID]    UNIQUEIDENTIFIER NULL,
    [CustomerCompanyName] NVARCHAR (50)    NOT NULL,
    [CustomerCode]        NVARCHAR (10)    NOT NULL,
    [CreditLimitCash]     NUMERIC (18, 4)  NULL,
    [CreditLimitInvoice]  NUMERIC (18, 4)  NULL,
    [CreditRatingID]      UNIQUEIDENTIFIER NULL,
    [Statements]          BIT              NULL,
    [NoteID]              UNIQUEIDENTIFIER NULL,
    [CompanyID]           UNIQUEIDENTIFIER NULL,
    [IsActive]            BIT              CONSTRAINT [DF__tblCustom__IsAct__14270015] DEFAULT ((1)) NOT NULL,
    [IsTransfer]          INT              CONSTRAINT [DF_tblCustomer_IsTransfer] DEFAULT ((0)) NOT NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]         DATETIME         NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    [UpdatedDate]         DATETIME         NULL,
    CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [FK_tblCustomer_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblCustomer_tblCustomer] FOREIGN KEY ([ParentCustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID]),
    CONSTRAINT [FK_tblCustomer_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblCustomer_tlkpCreditRating] FOREIGN KEY ([CreditRatingID]) REFERENCES [dbo].[tlkpCreditRating] ([CreditRatingID]),
    CONSTRAINT [FK_tblCustomerCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblCustomer] (
    [CustomerID]          UNIQUEIDENTIFIER NOT NULL,
    [ParentCustomerID]    UNIQUEIDENTIFIER NULL,
    [CustomerCompanyName] NVARCHAR (50)    NOT NULL,
    [CustomerCode]        NVARCHAR (10)    NOT NULL,
    [CreditLimitCash]     NUMERIC (18, 4)  NULL,
    [CreditLimitInvoice]  NUMERIC (18, 4)  NULL,
    [CreditRatingID]      UNIQUEIDENTIFIER NULL,
    [Statements]          BIT              NULL,
    [NoteID]              UNIQUEIDENTIFIER NULL,
    [CompanyID]           UNIQUEIDENTIFIER NULL,
    [IsActive]            BIT              CONSTRAINT [DF__tblCustom__IsAct__14270015] DEFAULT ((1)) NOT NULL,
    [IsTransfer]          INT              CONSTRAINT [DF_tblCustomer_IsTransfer] DEFAULT ((0)) NOT NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]         DATETIME         NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    [UpdatedDate]         DATETIME         NULL,
    CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [FK_tblCustomer_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblCustomer_tblCustomer] FOREIGN KEY ([ParentCustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID]),
    CONSTRAINT [FK_tblCustomer_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblCustomer_tlkpCreditRating] FOREIGN KEY ([CreditRatingID]) REFERENCES [dbo].[tlkpCreditRating] ([CreditRatingID]),
    CONSTRAINT [FK_tblCustomerCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
