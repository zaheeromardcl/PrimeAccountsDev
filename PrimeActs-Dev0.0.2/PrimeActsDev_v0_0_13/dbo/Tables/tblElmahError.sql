<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblElmahError] (
    [ErrorId]     UNIQUEIDENTIFIER NOT NULL,
    [Application] NVARCHAR (60)    NOT NULL,
    [Host]        NVARCHAR (50)    NOT NULL,
    [Type]        NVARCHAR (100)   NOT NULL,
    [Source]      NVARCHAR (60)    NOT NULL,
    [Message]     NVARCHAR (500)   NOT NULL,
    [User]        NVARCHAR (50)    NOT NULL,
    [StatusCode]  INT              NOT NULL,
    [TimeUtc]     DATETIME         NOT NULL,
    [Sequence]    INT              IDENTITY (1, 1) NOT NULL,
    [AllXml]      NTEXT            NOT NULL,
    CONSTRAINT [PK_tblElmahError_1] PRIMARY KEY CLUSTERED ([ErrorId] ASC)
);

=======
﻿CREATE TABLE [dbo].[tblElmahError] (
    [ErrorId]     UNIQUEIDENTIFIER NOT NULL,
    [Application] NVARCHAR (60)    NOT NULL,
    [Host]        NVARCHAR (50)    NOT NULL,
    [Type]        NVARCHAR (100)   NOT NULL,
    [Source]      NVARCHAR (60)    NOT NULL,
    [Message]     NVARCHAR (500)   NOT NULL,
    [User]        NVARCHAR (50)    NOT NULL,
    [StatusCode]  INT              NOT NULL,
    [TimeUtc]     DATETIME         NOT NULL,
    [Sequence]    INT              IDENTITY (1, 1) NOT NULL,
    [AllXml]      NTEXT            NOT NULL,
    CONSTRAINT [PK_tblElmahError_1] PRIMARY KEY CLUSTERED ([ErrorId] ASC)
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
