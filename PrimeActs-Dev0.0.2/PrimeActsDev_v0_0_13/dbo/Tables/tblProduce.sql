<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblProduce] (
    [ProduceID]       UNIQUEIDENTIFIER NOT NULL,
    [ProduceCode]     NVARCHAR (50)    NOT NULL,
    [ProduceName]     NVARCHAR (50)    NOT NULL,
    [ProduceGroupID]  UNIQUEIDENTIFIER NOT NULL,
    [MasterGroupID]   UNIQUEIDENTIFIER NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedDate]     DATETIME         NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblProduce] PRIMARY KEY CLUSTERED ([ProduceID] ASC),
    CONSTRAINT [FK_tblProduce_tblMasterGroup] FOREIGN KEY ([MasterGroupID]) REFERENCES [dbo].[tblMasterGroup] ([MasterGroupID]),
    CONSTRAINT [FK_tblProduce_tblProduceMaster1] FOREIGN KEY ([ProduceGroupID]) REFERENCES [dbo].[tblProduceGroup] ([ProduceGroupID]),
    CONSTRAINT [FK_tblProduceCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblProduceUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblProduce] (
    [ProduceID]       UNIQUEIDENTIFIER NOT NULL,
    [ProduceCode]     NVARCHAR (50)    NOT NULL,
    [ProduceName]     NVARCHAR (50)    NOT NULL,
    [ProduceGroupID]  UNIQUEIDENTIFIER NOT NULL,
    [MasterGroupID]   UNIQUEIDENTIFIER NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedDate]     DATETIME         NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblProduce] PRIMARY KEY CLUSTERED ([ProduceID] ASC),
    CONSTRAINT [FK_tblProduce_tblMasterGroup] FOREIGN KEY ([MasterGroupID]) REFERENCES [dbo].[tblMasterGroup] ([MasterGroupID]),
    CONSTRAINT [FK_tblProduce_tblProduceMaster1] FOREIGN KEY ([ProduceGroupID]) REFERENCES [dbo].[tblProduceGroup] ([ProduceGroupID]),
    CONSTRAINT [FK_tblProduceCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblProduceUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
