CREATE TABLE [dbo].[ProductCategory] (
    [ProductCategoryID]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [ProductCategoryName] NVARCHAR (MAX)   NOT NULL,
    [ProductCategoryDesc] NVARCHAR (MAX)   NOT NULL,
    [LastUpdatedDate]     DATETIME         NOT NULL,
    [LastUpdatedUser]     NVARCHAR (MAX)   NOT NULL,
    [RowGuid]             UNIQUEIDENTIFIER NOT NULL,
    [ProductTypeID]       BIGINT           NOT NULL,
    CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED ([ProductCategoryID] ASC),
    FOREIGN KEY ([ProductTypeID]) REFERENCES [dbo].[ProductType] ([ProductTypeID])
);

