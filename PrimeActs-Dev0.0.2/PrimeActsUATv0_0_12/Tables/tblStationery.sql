CREATE TABLE [dbo].[tblStationery] (
    [StationeryID]    UNIQUEIDENTIFIER NOT NULL,
    [StationeryName]  NVARCHAR (50)    NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]     DATETIME         NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    [UpdatedDate]     DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([StationeryID] ASC),
    CONSTRAINT [FK_tblStationeryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblStationeryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [AK_StationeryName] UNIQUE NONCLUSTERED ([StationeryName] ASC)
);

