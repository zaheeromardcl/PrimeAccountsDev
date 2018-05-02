CREATE TABLE [dbo].[tlkpPorterage] (
    [PorterageID]   UNIQUEIDENTIFIER NOT NULL,
    [PorterageCode] NVARCHAR (50)    NOT NULL,
    [UnitPrice]     NUMERIC(18,4)            NOT NULL,
    [MinimumAmount] NUMERIC(18,4)            NULL,
    [DepartmentID]  UNIQUEIDENTIFIER NULL,
    [UpdatedBy]     NVARCHAR (25)    NULL,
    [UpdatedDate]   DATETIME         NULL,
    [CreatedBy]     NVARCHAR (25)    NULL,
    [CreatedDate]   DATETIME         NULL,
    [IsActive]      BIT              CONSTRAINT [DF__tblPorter__IsAct__22751F6C] DEFAULT ((1)) NOT NULL,
    [CompanyID]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblPorterage] PRIMARY KEY CLUSTERED ([PorterageID] ASC),
    CONSTRAINT [FK_tlkpPorterage_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

