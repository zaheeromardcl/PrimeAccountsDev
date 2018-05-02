CREATE TABLE [dbo].[tblPrinter]
(
	[PrinterID]		UNIQUEIDENTIFIER	NOT NULL,
	[PrinterName]	NVARCHAR (50)		NOT NULL,
	[NetworkName]	NVARCHAR (100)		NOT NULL,
	[DefaultOrder]	INT					NOT NULL,
	[IsActive]		BIT					NOT NULL DEFAULT(1),
	[IsColour] BIT NOT NULL DEFAULT 0, 
    [IsRaw] BIT NOT NULL DEFAULT 0, 
    [HasTractor] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_tblPrinter] PRIMARY KEY CLUSTERED ([PrinterID] ASC)
)
