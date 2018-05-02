<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblFile] (
    [FileID]      UNIQUEIDENTIFIER NOT NULL,
    [FileName]    NVARCHAR (50)    NOT NULL,
    [ContentType] NVARCHAR (50)    NULL,
    [FileContent] VARBINARY (MAX)  NOT NULL,
    CONSTRAINT [PK_tblFile] PRIMARY KEY CLUSTERED ([FileID] ASC)
);

=======
﻿CREATE TABLE [dbo].[tblFile] (
    [FileID]      UNIQUEIDENTIFIER NOT NULL,
    [FileName]    NVARCHAR (50)    NOT NULL,
    [ContentType] NVARCHAR (50)    NULL,
    [FileContent] VARBINARY (MAX)  NOT NULL,
    CONSTRAINT [PK_tblFile] PRIMARY KEY CLUSTERED ([FileID] ASC)
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
