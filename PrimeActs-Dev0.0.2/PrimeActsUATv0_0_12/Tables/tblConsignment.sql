CREATE TABLE [dbo].[tblConsignment] (
    [ConsignmentID]          UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]             UNIQUEIDENTIFIER NULL,
    [ConsignmentDescription] NVARCHAR (100)   NOT NULL,
    [ConsignmentReference]   NVARCHAR (50)    NOT NULL,
    [ServerCode]             NVARCHAR (1)     CONSTRAINT [DF_tblConsignment_ServerCode] DEFAULT ('') NOT NULL,
    [PortID]                 UNIQUEIDENTIFIER NULL,
    [PurchaseTypeID]         UNIQUEIDENTIFIER NOT NULL,
    [Handling]               NUMERIC (18, 4)  NOT NULL,
    [Commission]             NUMERIC (18, 4)  NOT NULL,
    [ShowVehicleOnInvoice]   BIT              CONSTRAINT [DF_tblConsignment_ShowVehicleOnInvoice] DEFAULT ((0)) NOT NULL,
    [Vehicle]                NVARCHAR (20)    NOT NULL,
    [VehicleDetail]          NVARCHAR (1000)  NOT NULL,
    [SupplierDepartmentID]   UNIQUEIDENTIFIER NOT NULL,
    [SupplierReference]      NVARCHAR (20)    NULL,
    [FK1]                    UNIQUEIDENTIFIER NULL,
    [FK2]                    UNIQUEIDENTIFIER NULL,
    [Bit1]                   BIT              NULL,
    [Bit2]                   BIT              NULL,
    [String1]                NVARCHAR (1000)  NULL,
    [String2]                NVARCHAR (1000)  NULL,
    [Numeric1]               NUMERIC (18, 4)  NULL,
    [Numeric2]               NUMERIC (18, 4)  NULL,
    [Int1]                   INT              NULL,
    [Int2]                   INT              NULL,
    [UpdatedDate]            DATETIME         NULL,
    [CreatedDate]            DATETIME         NULL,
    [DespatchLocationID]     UNIQUEIDENTIFIER NULL,
    [DespatchDate]           DATETIME         CONSTRAINT [DF_tblConsignment_DespatchDate] DEFAULT (getdate()) NOT NULL,
    [NoteID]                 UNIQUEIDENTIFIER NULL,
    [ContractDate]           DATETIME         NULL,
    [IsHistory]              BIT              NOT NULL,
    [CreatedByUserID]        UNIQUEIDENTIFIER NULL,
    [UpdatedByUserID]        UNIQUEIDENTIFIER NULL,
    [WhenActivated]          DATETIME         NULL,
    [IsDeleted]              BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblConsignment_1] PRIMARY KEY CLUSTERED ([ConsignmentID] ASC),
    CONSTRAINT [FK_tblConsignment_tblConsignment] FOREIGN KEY ([ConsignmentID]) REFERENCES [dbo].[tblConsignment] ([ConsignmentID]),
    CONSTRAINT [FK_tblConsignment_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblConsignment_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblConsignment_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblConsignment_tblUser] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblConsignment_tblUser1] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblConsignment_tlkpDespatch] FOREIGN KEY ([DespatchLocationID]) REFERENCES [dbo].[tlkpDespatchLocation] ([DespatchLocationID]),
    CONSTRAINT [FK_tblConsignment_tlkpPort] FOREIGN KEY ([PortID]) REFERENCES [dbo].[tlkpPort] ([PortID]),
    CONSTRAINT [FK_tblConsignment_tlkpPurchaseType] FOREIGN KEY ([PurchaseTypeID]) REFERENCES [dbo].[tlkpPurchaseType] ([PurchaseTypeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignment]
    ON [dbo].[tblConsignment]([ConsignmentID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignment_IsHistory]
    ON [dbo].[tblConsignment]([IsHistory] ASC, [IsDeleted] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblConsignment_1]
    ON [dbo].[tblConsignment]([ConsignmentID] ASC, [IsHistory] ASC, [IsDeleted] ASC, [ConsignmentReference] ASC);

