CREATE TABLE [dbo].[tlkpLicenseOption] (
    [LicenseOptionName]    NVARCHAR (50) NOT NULL,
    [OptionMaxCount]       INT           NOT NULL,
    [AvailableLocal]       BIT           NOT NULL,
    [AvailableCloud]       BIT           NOT NULL,
    [RequirePasswordCloud] BIT           NOT NULL,
    [RequirePasswordLocal] BIT           NOT NULL,
    [OptionUserName]       NVARCHAR (30) NULL,
    [OptionPasswordHash]   NVARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([LicenseOptionName] ASC)
);

