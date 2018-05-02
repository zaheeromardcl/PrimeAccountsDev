CREATE TABLE [dbo].[tblPrice] (
    [PriceID]       UNIQUEIDENTIFIER NOT NULL,
    [CurrentPrice]  NVARCHAR (50)    NOT NULL,
    [PriceDateTime] DATETIME         CONSTRAINT [DF_tblPrice_PriceDateTime] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]     NVARCHAR (25)    NULL,
    [UpdatedDate]   DATETIME         NULL,
    [CreatedBy]     NVARCHAR (25)    NULL,
    [CreatedDate]   DATETIME         NULL,
    [IsActive]      BIT              CONSTRAINT [DF__tblPrice__IsActi__6F7F8B4B] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblPrice_1] PRIMARY KEY CLUSTERED ([PriceID] ASC)
);

