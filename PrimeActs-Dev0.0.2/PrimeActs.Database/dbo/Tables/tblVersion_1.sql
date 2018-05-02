CREATE TABLE [dbo].[tblVersion] (
    [MasterVersion] DECIMAL (6, 4) NOT NULL,
    [LocalVersion]  DECIMAL (6, 4) NOT NULL,
    CONSTRAINT [PK_tblVersion] PRIMARY KEY CLUSTERED ([MasterVersion] ASC)
);

