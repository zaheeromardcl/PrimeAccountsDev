CREATE TABLE [dbo].[tblConsignmentFile] (
    [ConsignmentFileID] UNIQUEIDENTIFIER NOT NULL,
    [ConsignmentID]     UNIQUEIDENTIFIER NULL,
    [FileID]            UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblConsignmentFile] PRIMARY KEY CLUSTERED ([ConsignmentFileID] ASC),
    CONSTRAINT [FK_tblConsignmentFile_tblConsignment] FOREIGN KEY ([ConsignmentID]) REFERENCES [dbo].[tblConsignment] ([ConsignmentID]),
    CONSTRAINT [FK_tblConsignmentFile_tblFile] FOREIGN KEY ([FileID]) REFERENCES [dbo].[tblFile] ([FileID])
);

