CREATE TABLE [dbo].[Manufacturer] (
    [ManufacturerID]      BIGINT           IDENTITY (1, 1) NOT NULL,
    [ManufacturerName]    NVARCHAR (MAX)   NOT NULL,
    [ManufacturerAddress] NVARCHAR (MAX)   NOT NULL,
    [ManufacturerContact] NVARCHAR (MAX)   NOT NULL,
    [LastUpdatedDate]     DATETIME         NOT NULL,
    [LastUpdatedUser]     NVARCHAR (MAX)   NOT NULL,
    [RowGuid]             UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Manufacturer] PRIMARY KEY CLUSTERED ([ManufacturerID] ASC)
);

