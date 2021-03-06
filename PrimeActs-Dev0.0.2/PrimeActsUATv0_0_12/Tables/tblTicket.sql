﻿CREATE TABLE [dbo].[tblTicket] (
    [TicketID]             UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]           UNIQUEIDENTIFIER NULL,
    [PONumber]             NVARCHAR (20)    NULL,
    [ServerCode]           NVARCHAR (1)     NULL,
    [TicketReference]      NVARCHAR (50)    NOT NULL,
    [CustomerDepartmentID] UNIQUEIDENTIFIER NULL,
    [NoteID]               UNIQUEIDENTIFIER NULL,
    [TicketDate]           DATETIME         NOT NULL,
    [UpdatedDate]          DATETIME         NULL,
    [CreatedDate]          DATETIME         NULL,
    [CurrencyID]           UNIQUEIDENTIFIER NULL,
    [ExchangeRate]         NUMERIC (16, 4)  NULL,
    [IsCashSale]           BIT              NULL,
    [SalesPersonUserID]    UNIQUEIDENTIFIER NULL,
    [FK1]                  UNIQUEIDENTIFIER NULL,
    [FK2]                  UNIQUEIDENTIFIER NULL,
    [Bit1]                 BIT              NULL,
    [Bit2]                 BIT              NULL,
    [String1]              NVARCHAR (1000)  NULL,
    [String2]              NVARCHAR (1000)  NULL,
    [Numeric1]             NUMERIC (18, 4)  NULL,
    [Numeric2]             NUMERIC (18, 4)  NULL,
    [Int1]                 INT              NULL,
    [Int2]                 INT              NULL,
    [Commission]           NUMERIC (6, 2)   CONSTRAINT [DF_tblTicket_Commission1] DEFAULT ((0)) NOT NULL,
    [Handling]             NUMERIC (6, 2)   CONSTRAINT [DF_tblTicket_Handling1] DEFAULT ((0)) NOT NULL,
    [TicketPrefix]         NVARCHAR (1)     NULL,
    [IsPrinted]            BIT              CONSTRAINT [DF_tblTicket_IsSaved1] DEFAULT ((0)) NOT NULL,
    [IsHistory]            BIT              NOT NULL,
    [TransferReference]    NVARCHAR (50)    NULL,
    [CreatedByUserID]      UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]      UNIQUEIDENTIFIER NULL,
    [IsDeleted]            BIT              CONSTRAINT [DF__tblTicket__IsDel__74CE504D] DEFAULT ((0)) NOT NULL,
    [PorterageCorrector]   NUMERIC (18, 4)  NULL,
    CONSTRAINT [PK_tblTicket_111] PRIMARY KEY CLUSTERED ([TicketID] ASC),
    CONSTRAINT [FK_tblTicket_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblTicket_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblTicket_tblNote1] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblTicket_tlkpCurrency1] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblTicketCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblTicketUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblTicket_CustomerDepartmentIDIsCasheSaleIsDeleted]
    ON [dbo].[tblTicket]([CustomerDepartmentID] ASC, [IsCashSale] ASC, [IsDeleted] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblTicket_TicketDateIsCashSaleIsDeleted]
    ON [dbo].[tblTicket]([TicketDate] ASC, [IsCashSale] ASC, [IsDeleted] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblTicket]
    ON [dbo].[tblTicket]([TicketID] ASC, [TicketReference] ASC, [IsDeleted] ASC);

