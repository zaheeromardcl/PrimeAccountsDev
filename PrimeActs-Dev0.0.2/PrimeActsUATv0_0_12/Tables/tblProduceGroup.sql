CREATE TABLE [dbo].[tblProduceGroup] (
    [ProduceGroupID]   UNIQUEIDENTIFIER NOT NULL,
    [IsActive]         BIT              NOT NULL,
    [ProduceGroupCode] NVARCHAR (10)    NOT NULL,
    [ProduceGroupName] NVARCHAR (50)    NOT NULL,
    [UpdatedDate]      DATETIME         NULL,
    [CreatedDate]      DATETIME         NULL,
    [CreatedByUserID]  UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblProduceMaster] PRIMARY KEY CLUSTERED ([ProduceGroupID] ASC),
    CONSTRAINT [FK_tblProduceGroupCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblProduceGroupUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

