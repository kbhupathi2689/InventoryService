CREATE TABLE [dbo].[ProductSubCategory] (
    [ProductSubCategoryID]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [ProductSubCategoryName] NVARCHAR (MAX)   NOT NULL,
    [ProductCategoryID]      BIGINT           NOT NULL,
    [SupportedPlatform]      NVARCHAR (MAX)   NOT NULL,
    [LastUpdatedDate]        DATETIME         NOT NULL,
    [LastUpdatedUser]        NVARCHAR (MAX)   NOT NULL,
    [RowGuid]                UNIQUEIDENTIFIER NOT NULL,
    [DomainID]               BIGINT           NOT NULL,
    CONSTRAINT [PK_ProductSubCategory] PRIMARY KEY CLUSTERED ([ProductSubCategoryID] ASC),
    FOREIGN KEY ([DomainID]) REFERENCES [dbo].[Domain] ([ID]),
    FOREIGN KEY ([ProductCategoryID]) REFERENCES [dbo].[ProductCategory] ([ProductCategoryID])
);

