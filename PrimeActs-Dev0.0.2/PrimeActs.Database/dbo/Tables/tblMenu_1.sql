CREATE TABLE [dbo].[tblMenu] (
    [MenuID]          UNIQUEIDENTIFIER CONSTRAINT [DF_tblMenu_MenuIDIN] DEFAULT (newid()) NOT NULL,
    [ParentID]        UNIQUEIDENTIFIER NOT NULL,
    [MenuDescription] NVARCHAR (50)    NOT NULL,
    [MenuLinkTo]      NVARCHAR (50)    NOT NULL,
    [IsCurrent]       BIT              NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedBy]       NVARCHAR (25)    NULL,
    [CreatedDate]     DATETIME         NULL,
    [IsActive]        BIT              CONSTRAINT [DF__tblMenu__IsActiv__1CBC4616] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblMenu] PRIMARY KEY CLUSTERED ([MenuID] ASC)
);

