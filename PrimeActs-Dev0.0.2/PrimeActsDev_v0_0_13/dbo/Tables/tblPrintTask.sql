<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblPrintTask] (
    [PrintTaskID]    UNIQUEIDENTIFIER NOT NULL,
    [PrintTaskName]  NVARCHAR (50)    NOT NULL,
    [HasColour]      BIT              NULL,
    [RequireColour]  BIT              NULL,
    [HasRaw]         BIT              NULL,
    [RequireRaw]     BIT              NULL,
    [HasTractor]     BIT              NULL,
    [RequireTractor] BIT              NULL,
    PRIMARY KEY CLUSTERED ([PrintTaskID] ASC)
);

=======
﻿CREATE TABLE [dbo].[tblPrintTask] (
    [PrintTaskID]    UNIQUEIDENTIFIER NOT NULL,
    [PrintTaskName]  NVARCHAR (50)    NOT NULL,
    [HasColour]      BIT              NULL,
    [RequireColour]  BIT              NULL,
    [HasRaw]         BIT              NULL,
    [RequireRaw]     BIT              NULL,
    [HasTractor]     BIT              NULL,
    [RequireTractor] BIT              NULL,
    PRIMARY KEY CLUSTERED ([PrintTaskID] ASC)
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
