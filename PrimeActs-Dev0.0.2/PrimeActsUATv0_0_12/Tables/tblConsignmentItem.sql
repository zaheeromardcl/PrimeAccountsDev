CREATE TABLE [dbo].[tblConsignmentItem] (
    [ConsignmentItemID]     UNIQUEIDENTIFIER NOT NULL,
    [ConsignmentID]         UNIQUEIDENTIFIER NOT NULL,
    [DepartmentID]          UNIQUEIDENTIFIER NULL,
    [BestBeforeDate]        DATETIME         NULL,
    [ProduceID]             UNIQUEIDENTIFIER NOT NULL,
    [Brand]                 NVARCHAR (50)    NULL,
    [Rotation]              NVARCHAR (10)    NULL,
    [PackType]              NVARCHAR (10)    NOT NULL,
    [PackWtUnitID]          UNIQUEIDENTIFIER NULL,
    [PackWeight]            NUMERIC (8, 2)   NOT NULL,
    [PackSize]              NVARCHAR (10)    NOT NULL,
    [PackPall]              INT              NULL,
    [EstimatedProfit]       NUMERIC (18, 4)  NULL,
    [EstimatedChargeCost]   NUMERIC (18, 4)  NULL,
    [RetReduce]             NUMERIC (3, 2)   NULL,
    [EstimatedPurchaseCost] NUMERIC (18, 4)  NULL,
    [ItemStatus]            INT              CONSTRAINT [DF_tblConsignmentItem_Status] DEFAULT ((2)) NULL,
    [PorterageID]           UNIQUEIDENTIFIER NOT NULL,
    [NoteID]                UNIQUEIDENTIFIER NULL,
    [FK1]                   UNIQUEIDENTIFIER NULL,
    [FK2]                   UNIQUEIDENTIFIER NULL,
    [Bit1]                  BIT              NULL,
    [Bit2]                  BIT              NULL,
    [String1]               NVARCHAR (1000)  NULL,
    [String2]               NVARCHAR (1000)  NULL,
    [Numeric1]              NUMERIC (18, 4)  NULL,
    [Numeric2]              NUMERIC (18, 4)  NULL,
    [Int1]                  INT              NULL,
    [Int2]                  INT              NULL,
    [UpdatedDate]           DATETIME         NULL,
    [OriginCountryID]       UNIQUEIDENTIFIER NULL,
    [CreatedDate]           DATETIME         NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    [QuantityExpected]      INT              NOT NULL,
    CONSTRAINT [PK_tblConsignmentItem_1] PRIMARY KEY CLUSTERED ([ConsignmentItemID] ASC),
    CONSTRAINT [FK_tblConsignmentItem_tblConsignment] FOREIGN KEY ([ConsignmentID]) REFERENCES [dbo].[tblConsignment] ([ConsignmentID]),
    CONSTRAINT [FK_tblConsignmentItem_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblConsignmentItem_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblConsignmentItem_tblProduce] FOREIGN KEY ([ProduceID]) REFERENCES [dbo].[tblProduce] ([ProduceID]),
    CONSTRAINT [FK_tblConsignmentItem_tlkpCountry] FOREIGN KEY ([OriginCountryID]) REFERENCES [dbo].[tlkpCountry] ([CountryID]),
    CONSTRAINT [FK_tblConsignmentItem_tlkpPackWtUnit] FOREIGN KEY ([PackWtUnitID]) REFERENCES [dbo].[tlkpPackWtUnit] ([PackWtUnitID]),
    CONSTRAINT [FK_tblConsignmentItem_tlkpPorterage] FOREIGN KEY ([PorterageID]) REFERENCES [dbo].[tlkpPorterage] ([PorterageID]),
    CONSTRAINT [FK_tblConsignmentItemCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblConsignmentItemUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignmentItem_ConIDConItemID]
    ON [dbo].[tblConsignmentItem]([ConsignmentID] ASC, [ConsignmentItemID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignmentItem_ProduceIDDepartmentID]
    ON [dbo].[tblConsignmentItem]([ProduceID] ASC, [DepartmentID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignmentItem_ConsignmentItemIDConIDDepIDQty]
    ON [dbo].[tblConsignmentItem]([ConsignmentItemID] ASC, [ConsignmentID] ASC, [DepartmentID] ASC, [QuantityExpected] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignmentItem_ConsignmentItemIDConIDDepIDProdID]
    ON [dbo].[tblConsignmentItem]([ConsignmentItemID] ASC, [ConsignmentID] ASC, [DepartmentID] ASC, [ProduceID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignmentItem_ConsignmentItemIDConIDDepIDProdIDQty]
    ON [dbo].[tblConsignmentItem]([ConsignmentItemID] ASC, [ConsignmentID] ASC, [DepartmentID] ASC, [ProduceID] ASC, [QuantityExpected] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignmentItem_ConIDConItemIDDepIDProdIDQty]
    ON [dbo].[tblConsignmentItem]([ConsignmentID] ASC, [ConsignmentItemID] ASC, [DepartmentID] ASC, [ProduceID] ASC, [QuantityExpected] ASC);

