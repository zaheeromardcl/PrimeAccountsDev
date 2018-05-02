CREATE TABLE [dbo].[tblRepository] (
    [VersionID]   DECIMAL (6, 4)  NOT NULL,
    [CodeZipFile] VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_tblRepository] PRIMARY KEY CLUSTERED ([VersionID] ASC)
);

