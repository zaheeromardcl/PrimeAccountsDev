<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblProduceIntrastat] (
    [ProduceIntrastatID] UNIQUEIDENTIFIER NOT NULL,
    [ProduceID]          UNIQUEIDENTIFIER NOT NULL,
    [IntrastatCode]      INT              NOT NULL,
    [StartDate]          DATETIME         NOT NULL,
    [UpdatedDate]        DATETIME         NULL,
    [CreatedDate]        DATETIME         NULL,
    [CreatedByUserID]    UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblProduceIntrastat] PRIMARY KEY CLUSTERED ([ProduceIntrastatID] ASC),
    CONSTRAINT [FK_tblProduceIntrastat_tblProduce] FOREIGN KEY ([ProduceID]) REFERENCES [dbo].[tblProduce] ([ProduceID]),
    CONSTRAINT [FK_tblProduceIntrastatCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblProduceIntrastatUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblProduceIntrastat] (
    [ProduceIntrastatID] UNIQUEIDENTIFIER NOT NULL,
    [ProduceID]          UNIQUEIDENTIFIER NOT NULL,
    [IntrastatCode]      INT              NOT NULL,
    [StartDate]          DATETIME         NOT NULL,
    [UpdatedDate]        DATETIME         NULL,
    [CreatedDate]        DATETIME         NULL,
    [CreatedByUserID]    UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblProduceIntrastat] PRIMARY KEY CLUSTERED ([ProduceIntrastatID] ASC),
    CONSTRAINT [FK_tblProduceIntrastat_tblProduce] FOREIGN KEY ([ProduceID]) REFERENCES [dbo].[tblProduce] ([ProduceID]),
    CONSTRAINT [FK_tblProduceIntrastatCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblProduceIntrastatUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
