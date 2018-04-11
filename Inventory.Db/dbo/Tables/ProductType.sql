CREATE TABLE [dbo].[ProductType] (
    [ProductTypeID]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [ProductTypeName] NVARCHAR (MAX)   NOT NULL,
    [ProductTypeDesc] NVARCHAR (MAX)   NOT NULL,
    [LastUpdatedDate] DATETIME         NOT NULL,
    [LastUpdatedUser] NVARCHAR (MAX)   NOT NULL,
    [RowGuid]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED ([ProductTypeID] ASC)
);

