CREATE TABLE [dbo].[tlkpTransferType] (
    [TransferTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [TransferTypeName] NVARCHAR (50)    NOT NULL,
    [IsActive]         BIT              NOT NULL DEFAULT(1),
    [TransferTypeCode] NVARCHAR (10)    NOT NULL,
    CONSTRAINT [PK_tlkpTransferType] PRIMARY KEY CLUSTERED ([TransferTypeID] ASC)
);

