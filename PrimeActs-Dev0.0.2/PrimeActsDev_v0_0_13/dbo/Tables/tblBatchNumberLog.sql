<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblBatchNumberLog] (
    [BatchNumberLogID]    UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]           UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]          UNIQUEIDENTIFIER NULL,
    [ServerPrefix]        NVARCHAR (2)     NOT NULL,
    [BatchNumber]         INT              IDENTITY (1000, 1) NOT NULL,
    [TransactionDateTime] DATETIME         NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedDate]         DATETIME         NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblBatchNumber] PRIMARY KEY CLUSTERED ([BatchNumberLogID] ASC),
    CONSTRAINT [FK_tblBatchNumberLog_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblBatchNumberLog_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblBatchNumberLogCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblBatchNumberLogUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblBatchNumberLog_CompanyID_BatchNumber]
    ON [dbo].[tblBatchNumberLog]([CompanyID] ASC, [BatchNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblBatchNumberLog_DivisionID_BatchNumber]
    ON [dbo].[tblBatchNumberLog]([DivisionID] ASC, [BatchNumber] ASC);

=======
﻿CREATE TABLE [dbo].[tblBatchNumberLog] (
    [BatchNumberLogID]    UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]           UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]          UNIQUEIDENTIFIER NULL,
    [ServerPrefix]        NVARCHAR (2)     NOT NULL,
    [BatchNumber]         INT              IDENTITY (1000, 1) NOT NULL,
    [TransactionDateTime] DATETIME         NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedDate]         DATETIME         NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblBatchNumber] PRIMARY KEY CLUSTERED ([BatchNumberLogID] ASC),
    CONSTRAINT [FK_tblBatchNumberLog_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblBatchNumberLog_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblBatchNumberLogCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblBatchNumberLogUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblBatchNumberLog_CompanyID_BatchNumber]
    ON [dbo].[tblBatchNumberLog]([CompanyID] ASC, [BatchNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblBatchNumberLog_DivisionID_BatchNumber]
    ON [dbo].[tblBatchNumberLog]([DivisionID] ASC, [BatchNumber] ASC);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
