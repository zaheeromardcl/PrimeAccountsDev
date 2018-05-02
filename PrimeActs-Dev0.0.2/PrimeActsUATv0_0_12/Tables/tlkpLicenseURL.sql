CREATE TABLE [dbo].[tlkpLicenseURL] (
    [ServerCode] NVARCHAR (1)   NOT NULL,
    [ServerURL]  NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([ServerCode] ASC)
);

