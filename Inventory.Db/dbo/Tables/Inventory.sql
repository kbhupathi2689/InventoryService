CREATE TABLE [dbo].[Inventory] (
    [InventoryID]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [InventoryName]             NVARCHAR (MAX)   NOT NULL,
    [InventoryAddress]          NVARCHAR (MAX)   NOT NULL,
    [InventoryPrimaryContact]   NVARCHAR (MAX)   NOT NULL,
    [InventorySecondaryContact] NVARCHAR (MAX)   NULL,
    [ManufacturerID]            BIGINT           NOT NULL,
    [ProductID]                 BIGINT           NOT NULL,
    [ShelfLocation]             NVARCHAR (MAX)   NOT NULL,
    [Bin]                       NVARCHAR (MAX)   NOT NULL,
    [InStock]                   BIT              NOT NULL,
    [QuantityOnHand]            INT              NOT NULL,
    [LastUpdatedDate]           DATETIME         NOT NULL,
    [LastUpdatedUser]           NVARCHAR (MAX)   NOT NULL,
    [RowGuid]                   UNIQUEIDENTIFIER NOT NULL,
    [DomainID]                  BIGINT           NOT NULL,
    CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED ([InventoryID] ASC),
    FOREIGN KEY ([DomainID]) REFERENCES [dbo].[Domain] ([ID]),
    FOREIGN KEY ([ManufacturerID]) REFERENCES [dbo].[Manufacturer] ([ManufacturerID]),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID])
);

