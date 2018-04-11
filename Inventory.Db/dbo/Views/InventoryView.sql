
CREATE VIEW InventoryView AS
SELECT INV.InventoryID as ID, 
INV.InventoryAddress as Location, 
INV.InventoryPrimaryContact as Contact, 
DM.Name as Domain,
MN.ManufacturerName as Manufacturer,
PR.ProductName as Product,
INV.ShelfLocation as Shelf,
INV.Bin as Container,
CASE WHEN INV.InStock = 1 THEN 'Yes' ELSE 'No' END AS StockAvailable,
INV.QuantityOnHand as StockCount
FROM Inventory INV 
INNER JOIN Domain DM ON INV.DomainID = DM.ID
INNER JOIN Manufacturer MN ON INV.ManufacturerID = MN.ManufacturerID
INNER JOIN Product PR ON INV.ProductID = PR.ProductID
