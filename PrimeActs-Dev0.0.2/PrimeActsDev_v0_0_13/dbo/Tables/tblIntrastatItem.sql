<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblIntrastatItem] (
    [IntrastatItemID]                  UNIQUEIDENTIFIER NOT NULL,
    [IntrastatCommodity]               NVARCHAR (50)    NOT NULL,
    [IntrastatValue]                   NUMERIC (18, 4)  NOT NULL,
    [IntrastatTerms]                   NVARCHAR (50)    NOT NULL,
    [IntrastatNature]                  INT              NOT NULL,
    [IntrastatNetMassAmount]           NUMERIC (12, 2)  NOT NULL,
    [IntrastatCountry]                 NVARCHAR (50)    NOT NULL,
    [IntrastatID]                      UNIQUEIDENTIFIER NOT NULL,
    [InrastatConsignmentOriginCountry] NVARCHAR (50)    NOT NULL,
    [UpdatedDate]                      DATETIME         NULL,
    [CreatedDate]                      DATETIME         NULL,
    [IsActive]                         BIT              DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]                  UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]                  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblIntrastatItem_1] PRIMARY KEY CLUSTERED ([IntrastatItemID] ASC),
    CONSTRAINT [FK_tblIntrastatItem_tblIntrastat] FOREIGN KEY ([IntrastatID]) REFERENCES [dbo].[tblIntrastat] ([IntrastatID]),
    CONSTRAINT [FK_tblIntrastatItemCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblIntrastatItemUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblIntrastatItem] (
    [IntrastatItemID]                  UNIQUEIDENTIFIER NOT NULL,
    [IntrastatCommodity]               NVARCHAR (50)    NOT NULL,
    [IntrastatValue]                   NUMERIC (18, 4)  NOT NULL,
    [IntrastatTerms]                   NVARCHAR (50)    NOT NULL,
    [IntrastatNature]                  INT              NOT NULL,
    [IntrastatNetMassAmount]           NUMERIC (12, 2)  NOT NULL,
    [IntrastatCountry]                 NVARCHAR (50)    NOT NULL,
    [IntrastatID]                      UNIQUEIDENTIFIER NOT NULL,
    [InrastatConsignmentOriginCountry] NVARCHAR (50)    NOT NULL,
    [UpdatedDate]                      DATETIME         NULL,
    [CreatedDate]                      DATETIME         NULL,
    [IsActive]                         BIT              DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]                  UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]                  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblIntrastatItem_1] PRIMARY KEY CLUSTERED ([IntrastatItemID] ASC),
    CONSTRAINT [FK_tblIntrastatItem_tblIntrastat] FOREIGN KEY ([IntrastatID]) REFERENCES [dbo].[tblIntrastat] ([IntrastatID]),
    CONSTRAINT [FK_tblIntrastatItemCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblIntrastatItemUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
