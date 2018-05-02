CREATE TABLE [dbo].[tlkpTransferType] (
    [TransferTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [TransferTypeName] NVARCHAR (50)    NOT NULL,
    [IsActive]         BIT              DEFAULT ((1)) NOT NULL,
    [TransferTypeCode] NVARCHAR (10)    NOT NULL,
    CONSTRAINT [PK_tlkpTransferType] PRIMARY KEY CLUSTERED ([TransferTypeID] ASC)
);

