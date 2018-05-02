CREATE TABLE [dbo].[tgenConsignmentNumber] (
    [ConsignmentID]     UNIQUEIDENTIFIER NOT NULL,
    [ConsignmentNumber] INT              IDENTITY (1000, 1) NOT NULL, 
    CONSTRAINT [PK_tgenConsignmentNumber] PRIMARY KEY ([ConsignmentID])
);

