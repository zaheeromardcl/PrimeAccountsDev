CREATE TABLE [dbo].[tblProduceIntrastat] (
    [ProduceIntrastatID] UNIQUEIDENTIFIER NOT NULL,
    [ProduceID]          UNIQUEIDENTIFIER NOT NULL,
    [IntrastatCode]      INT              NOT NULL,
    [EndDate]            DATETIME         NOT NULL,
    [UpdatedBy]          NVARCHAR (50)    NOT NULL,
    [UpdatedDate]        DATETIME         NOT NULL,
    [CreatedBy]          NVARCHAR (50)    NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    CONSTRAINT [PK_tblProduceIntrastat] PRIMARY KEY CLUSTERED ([ProduceIntrastatID] ASC),
    CONSTRAINT [FK_tblProduceIntrastat_tblProduce] FOREIGN KEY ([ProduceID]) REFERENCES [dbo].[tblProduce] ([ProduceID])
);

