CREATE TABLE [dbo].[tblConsignmentItem] (
    [ConsignmentItemID]     UNIQUEIDENTIFIER NOT NULL,
    [ConsignmentID]         UNIQUEIDENTIFIER NULL,
    [DepartmentID]          UNIQUEIDENTIFIER NULL,
    [BestBeforeDate]        DATE             NULL,
    [ProduceID]             UNIQUEIDENTIFIER NOT NULL,
    [Brand]                 NVARCHAR (50)    NULL,
    [Rotation]              NVARCHAR (10)    NULL,
    [PackType]              NVARCHAR (10)    NOT NULL,
    [PackWtUnitID]          UNIQUEIDENTIFIER NULL,
    [PackWeight]            DECIMAL (8, 2)   NOT NULL,
    [PackSize]              NVARCHAR (10)    NOT NULL,
    [PackPall]              INT              NULL,
    [EstimatedProfit]       NUMERIC(18,4)            NULL,
    [EstimatedChargeCost]   NUMERIC(18,4)            NULL,
    [RetReduce]             NUMERIC (3, 2)   NULL,
    [EstimatedPurchaseCost] NUMERIC(18,4)            NULL,
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
    [UpdatedBy]             NVARCHAR (25)    NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedBy]             NVARCHAR (25)    NULL,
    [OriginCountryID]       UNIQUEIDENTIFIER NULL,
    [CreatedDate]           DATETIME         NULL,
    CONSTRAINT [PK_tblConsignmentItem_1] PRIMARY KEY CLUSTERED ([ConsignmentItemID] ASC),
    CONSTRAINT [FK_tblConsignmentItem_tblConsignment] FOREIGN KEY ([ConsignmentID]) REFERENCES [dbo].[tblConsignment] ([ConsignmentID]),
    CONSTRAINT [FK_tblConsignmentItem_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblConsignmentItem_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblConsignmentItem_tblProduce] FOREIGN KEY ([ProduceID]) REFERENCES [dbo].[tblProduce] ([ProduceID]),
    CONSTRAINT [FK_tblConsignmentItem_tlkpCountry] FOREIGN KEY ([OriginCountryID]) REFERENCES [dbo].[tlkpCountry] ([CountryID]),
    CONSTRAINT [FK_tblConsignmentItem_tlkpPackWtUnit] FOREIGN KEY ([PackWtUnitID]) REFERENCES [dbo].[tlkpPackWtUnit] ([PackWtUnitID]),
    CONSTRAINT [FK_tblConsignmentItem_tlkpPorterage] FOREIGN KEY ([PorterageID]) REFERENCES [dbo].[tlkpPorterage] ([PorterageID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignmentItem_ConIDConItemID]
    ON [dbo].[tblConsignmentItem]([ConsignmentID] ASC, [ConsignmentItemID] ASC);


GO
