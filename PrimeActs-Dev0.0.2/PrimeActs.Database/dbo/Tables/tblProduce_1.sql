CREATE TABLE [dbo].[tblProduce] (
    [ProduceID]      UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]     UNIQUEIDENTIFIER NULL,
    [ProduceCode]    NVARCHAR (50)    NOT NULL,
    [ProduceName]    NVARCHAR (50)    NOT NULL,
    [ProduceGroupID] UNIQUEIDENTIFIER NOT NULL,
    [MasterGroupID]  UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxCodeID]      UNIQUEIDENTIFIER NULL,
    [IsActive]       BIT              NOT NULL,
    [UpdatedBy]      NVARCHAR (25)    NULL,
    [UpdatedDate]    DATETIME         NULL,
    [CreatedBy]      NVARCHAR (25)    NULL,
    [CreatedDate]    DATETIME         NULL,
    CONSTRAINT [PK_tblProduce] PRIMARY KEY CLUSTERED ([ProduceID] ASC),
    CONSTRAINT [FK_tblProduce_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblProduce_tblMasterGroup] FOREIGN KEY ([MasterGroupID]) REFERENCES [dbo].[tblMasterGroup] ([MasterGroupID]),
    CONSTRAINT [FK_tblProduce_tblProduceMaster1] FOREIGN KEY ([ProduceGroupID]) REFERENCES [dbo].[tblProduceGroup] ([ProduceGroupID]),
    CONSTRAINT [FK_tblProduce_tlkpTransactionTaxCode] FOREIGN KEY ([TransactionTaxCodeID]) REFERENCES [dbo].[tlkpTransactionTaxCode] ([TransactionTaxCodeID])
);

