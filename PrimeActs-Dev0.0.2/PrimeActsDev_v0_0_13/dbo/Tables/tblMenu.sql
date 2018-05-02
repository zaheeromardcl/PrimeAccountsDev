<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblMenu] (
    [MenuID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ParentID]        UNIQUEIDENTIFIER NOT NULL,
    [MenuDescription] NVARCHAR (50)    NOT NULL,
    [MenuLinkTo]      NVARCHAR (50)    NOT NULL,
    [IsCurrent]       BIT              NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedBy]       NVARCHAR (25)    NULL,
    [CreatedDate]     DATETIME         NULL,
    [IsActive]        BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblMenu] PRIMARY KEY CLUSTERED ([MenuID] ASC)
);

=======
﻿CREATE TABLE [dbo].[tblMenu] (
    [MenuID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ParentID]        UNIQUEIDENTIFIER NOT NULL,
    [MenuDescription] NVARCHAR (50)    NOT NULL,
    [MenuLinkTo]      NVARCHAR (50)    NOT NULL,
    [IsCurrent]       BIT              NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedBy]       NVARCHAR (25)    NULL,
    [CreatedDate]     DATETIME         NULL,
    [IsActive]        BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblMenu] PRIMARY KEY CLUSTERED ([MenuID] ASC)
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
