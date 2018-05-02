CREATE TABLE [dbo].[tblSetupLocal] (
    [SetupName]          NVARCHAR (50)   NOT NULL,
    [SetupValueType]     TINYINT         NOT NULL,
    [SetupValueInt]      INT             NULL,
    [SetupValueNumeric]  NUMERIC (38, 9) NULL,
    [SetupValueBit]      BIT             NULL,
    [SetupValueNvarchar] NVARCHAR (MAX)   NULL,
    [UpdatedBy]          NVARCHAR (25)   NULL,
    [UpdatedDate]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (25)   NULL,
    [CreatedDate]        DATETIME        NULL,
    
    CONSTRAINT [PK_tblSetup] PRIMARY KEY CLUSTERED ([SetupName] ASC)
);

