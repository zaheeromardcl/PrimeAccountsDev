CREATE TABLE [dbo].[tblProduceGroup] (
    [ProduceGroupID]   UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]       UNIQUEIDENTIFIER NOT NULL,
    [IsActive]         BIT              NOT NULL,
    [ProduceGroupCode] NVARCHAR (10)    NOT NULL,
    [ProduceGroupName] NVARCHAR (50)    NOT NULL,
    [UpdatedBy]        NVARCHAR (25)    NULL,
    [UpdatedDate]      DATETIME         NULL,
    [CreatedBy]        NVARCHAR (25)    NULL,
    [CreatedDate]      DATETIME         NULL,
    CONSTRAINT [PK_tblProduceMaster] PRIMARY KEY CLUSTERED ([ProduceGroupID] ASC),
    CONSTRAINT [FK_tblProduceGroup_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID])
);

