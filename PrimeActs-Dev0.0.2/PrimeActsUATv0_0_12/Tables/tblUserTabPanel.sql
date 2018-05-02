CREATE TABLE [dbo].[tblUserTabPanel] (
    [PanelID]         UNIQUEIDENTIFIER NOT NULL,
    [UserID]          UNIQUEIDENTIFIER NOT NULL,
    [Name]            NVARCHAR (50)    NOT NULL,
    [ContentType]     NVARCHAR (50)    NOT NULL,
    [HoldingDiv]      NVARCHAR (50)    NOT NULL,
    [IsSelected]      BIT              NOT NULL,
    [ControllerState] NVARCHAR (50)    NULL,
    [JsonData]        NVARCHAR (MAX)   NULL,
    [UriParam]        NVARCHAR (100)   NULL,
    PRIMARY KEY CLUSTERED ([PanelID] ASC)
);

