
CREATE VIEW ProductsView AS
SELECT PR.ProductID as ID, 
PR.ProductName as Name, 
PRT.ProductTypeName as Type, 
PC.ProductCategoryName as Category,
PSC.ProductSubCategoryName as SubCategory,
PR.ProductVersion as Version,
PSC.SupportedPlatform AS Platform,
PR.StandardCost as StandardCost,
PR.ProductSellingPrice as SellingPrice,
PR.ProductReleasedOn as ReleasedDate

FROM Product PR 
INNER JOIN ProductType PRT ON PR.ProductTypeID = PRT.ProductTypeID
INNER JOIN ProductCategory PC ON PR.ProductCategoryID = PC.ProductCategoryID AND PRT.ProductTypeID = PC.ProductTypeID
INNER JOIN ProductSubCategory PSC ON PR.ProductSubCategoryID = PSC.ProductSubCategoryID AND PSC.ProductCategoryID = PC.ProductCategoryID



