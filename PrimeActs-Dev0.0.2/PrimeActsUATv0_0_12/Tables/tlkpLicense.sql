CREATE TABLE [dbo].[tlkpLicense] (
    [LicenseHash]        NVARCHAR (MAX) NOT NULL,
    [LicenseCompanyName] NVARCHAR (100) NOT NULL,
    [ExpiryDate]         DATETIME       NOT NULL,
    [MaxUserCount]       INT            NOT NULL,
    [MaxCompanyCount]    INT            NOT NULL,
    [UseCloud]           BIT            NOT NULL,
    [LastLicenseCheck]   DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([LicenseCompanyName] ASC)
);

