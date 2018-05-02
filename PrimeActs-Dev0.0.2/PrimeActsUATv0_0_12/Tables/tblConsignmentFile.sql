CREATE TABLE [dbo].[tblConsignmentFile] (
    [ConsignmentFileID] UNIQUEIDENTIFIER NOT NULL,
    [ConsignmentID]     UNIQUEIDENTIFIER NULL,
    [FileID]            UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]   UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]       DATETIME         NULL,
    [UpdatedByUserID]   UNIQUEIDENTIFIER NULL,
    [UpdatedDate]       DATETIME         NULL,
    CONSTRAINT [PK_tblConsignmentFile] PRIMARY KEY CLUSTERED ([ConsignmentFileID] ASC),
    CONSTRAINT [FK_tblConsignmentFile_tblConsignment] FOREIGN KEY ([ConsignmentID]) REFERENCES [dbo].[tblConsignment] ([ConsignmentID]),
    CONSTRAINT [FK_tblConsignmentFile_tblFile] FOREIGN KEY ([FileID]) REFERENCES [dbo].[tblFile] ([FileID]),
    CONSTRAINT [FK_tblConsignmentFileCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblConsignmentFileUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

