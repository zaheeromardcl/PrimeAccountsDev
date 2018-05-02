CREATE TABLE [dbo].[tblBatchNumberLog] (
    [BatchNumberLogID]    UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]           UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]          UNIQUEIDENTIFIER NULL,
    [ServerPrefix]        NVARCHAR (2)     NOT NULL,
    [BatchNumber]         INT              IDENTITY (1000, 1) NOT NULL,
    [TransactionDateTime] DATETIME         NULL,
    [UpdatedBy]           NVARCHAR (25)    NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedBy]           NVARCHAR (25)    NULL,
    [CreatedDate]         DATETIME         NULL
    
    CONSTRAINT [PK_tblBatchNumber] PRIMARY KEY CLUSTERED ([BatchNumberLogID] ASC),
    CONSTRAINT [FK_tblBatchNumberLog_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblBatchNumberLog_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID])
);

