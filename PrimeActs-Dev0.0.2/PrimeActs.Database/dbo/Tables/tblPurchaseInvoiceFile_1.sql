CREATE TABLE [dbo].[tblPurchaseInvoiceFile](
	[PurchaseInvoiceFileID] [uniqueidentifier] NOT NULL,
	[PurchaseInvoiceID] [uniqueidentifier] NOT NULL,
	[FileID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_tblPurchaseInvoiceFile] PRIMARY KEY CLUSTERED 
(
	[PurchaseInvoiceFileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblPurchaseInvoiceFile]  WITH CHECK ADD  CONSTRAINT [FK_tblPurchaseInvoiceFile_tblPurchaseInvoice] FOREIGN KEY([PurchaseInvoiceID])
REFERENCES [dbo].[tblPurchaseInvoice] ([PurchaseInvoiceID])
GO

ALTER TABLE [dbo].[tblPurchaseInvoiceFile] CHECK CONSTRAINT [FK_tblPurchaseInvoiceFile_tblPurchaseInvoice]
GO

ALTER TABLE [dbo].[tblPurchaseInvoiceFile]  WITH CHECK ADD  CONSTRAINT [FK_tblPurchaseInvoiceFile_tblFile] FOREIGN KEY([FileID])
REFERENCES [dbo].[tblFile] ([FileID])
GO

ALTER TABLE [dbo].[tblPurchaseInvoiceFile] CHECK CONSTRAINT [FK_tblPurchaseInvoiceFile_tblFile]
GO
