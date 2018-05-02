CREATE TABLE [dbo].[tblTicketItem] (
    [TicketItemID]          UNIQUEIDENTIFIER NOT NULL,
    [TicketID]              UNIQUEIDENTIFIER NOT NULL,
    [TicketItemDescription] NVARCHAR (400)   NOT NULL,
    [TicketItemQuantity]    FLOAT (53)       NOT NULL,
    [TicketItemTotalPrice]  NUMERIC(18,4)            NOT NULL,
    [ConsignmentItemID]     UNIQUEIDENTIFIER NULL,
    [HaulierSupplierID]     UNIQUEIDENTIFIER NULL,
    [TransactionTaxCodeID]             UNIQUEIDENTIFIER NULL,
    [TransactionTaxRatePercentage]     NUMERIC (6, 4)   NULL,
    [CurrencyAmount]        NUMERIC(18,4)            NULL,
    [PorterageID]           UNIQUEIDENTIFIER NULL,
    [UpdatedBy]             NVARCHAR (25)    NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedBy]             NVARCHAR (25)    NULL,
    [CreatedDate]           DATETIME         NULL,
  
    [DepartmentID]          UNIQUEIDENTIFIER NULL,
    [PorterageValue]        NUMERIC(18,4)            CONSTRAINT [DF_tblTicketItem_PorterageValue] DEFAULT ((0.00)) NOT NULL,
    [OriginalTicketItemID]  UNIQUEIDENTIFIER NULL,
    [IsLatest]              BIT              NULL,
    [TransferTypeID]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblTicketItem_1] PRIMARY KEY CLUSTERED ([TicketItemID] ASC),
    CONSTRAINT [FK_tblTicketItem_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblTicketItem_tblSupplier] FOREIGN KEY ([HaulierSupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblTicketItem_tblTicket] FOREIGN KEY ([TicketID]) REFERENCES [dbo].[tblTicket] ([TicketID]),
    CONSTRAINT [FK_tblTicketItem_tlkpTransferType] FOREIGN KEY ([TransferTypeID]) REFERENCES [dbo].[tlkpTransferType] ([TransferTypeID]),
    CONSTRAINT [FK_tblTicketItem_tlkpTransactionTaxCode] FOREIGN KEY ([TransactionTaxCodeID]) REFERENCES [dbo].[tlkpTransactionTaxCode] ([TransactionTaxCodeID])
);


GO

CREATE NONCLUSTERED INDEX [IX_tblTicketItem] ON [dbo].[tblTicketItem]
(
	[OriginalTicketItemID] ASC,
	[TicketItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_tblTicketItem_ConsignmentItemID] ON [dbo].[tblTicketItem]
(
	[ConsignmentItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
