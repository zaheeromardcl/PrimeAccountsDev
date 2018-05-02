<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblPurchaseInvoiceFile] (
    [PurchaseInvoiceFileID] UNIQUEIDENTIFIER NOT NULL,
    [PurchaseInvoiceID]     UNIQUEIDENTIFIER NOT NULL,
    [FileID]                UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]           DATETIME         NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    [UpdatedDate]           DATETIME         NULL,
    CONSTRAINT [PK_tblPurchaseInvoiceFile] PRIMARY KEY CLUSTERED ([PurchaseInvoiceFileID] ASC),
    CONSTRAINT [FK_tblPurchaseInvoiceFile_tblFile] FOREIGN KEY ([FileID]) REFERENCES [dbo].[tblFile] ([FileID]),
    CONSTRAINT [FK_tblPurchaseInvoiceFile_tblPurchaseInvoice] FOREIGN KEY ([PurchaseInvoiceID]) REFERENCES [dbo].[tblPurchaseInvoice] ([PurchaseInvoiceID]),
    CONSTRAINT [FK_tblPurchaseInvoiceFileCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPurchaseInvoiceFileUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblPurchaseInvoiceFile] (
    [PurchaseInvoiceFileID] UNIQUEIDENTIFIER NOT NULL,
    [PurchaseInvoiceID]     UNIQUEIDENTIFIER NOT NULL,
    [FileID]                UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]           DATETIME         NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    [UpdatedDate]           DATETIME         NULL,
    CONSTRAINT [PK_tblPurchaseInvoiceFile] PRIMARY KEY CLUSTERED ([PurchaseInvoiceFileID] ASC),
    CONSTRAINT [FK_tblPurchaseInvoiceFile_tblFile] FOREIGN KEY ([FileID]) REFERENCES [dbo].[tblFile] ([FileID]),
    CONSTRAINT [FK_tblPurchaseInvoiceFile_tblPurchaseInvoice] FOREIGN KEY ([PurchaseInvoiceID]) REFERENCES [dbo].[tblPurchaseInvoice] ([PurchaseInvoiceID]),
    CONSTRAINT [FK_tblPurchaseInvoiceFileCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPurchaseInvoiceFileUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
