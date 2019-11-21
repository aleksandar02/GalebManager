﻿CREATE PROCEDURE [dbo].[GetBillByFactureNumber]
	@FactureNumber NVARCHAR(50)

AS
	BEGIN
		SELECT 
			Bill.[Id],
			Bill.[StoreId],
			Bill.[Number],
			Bill.[FactureNumber],
			Bill.[SupplierId],
			Bill.[Date],
			Bill.[Sum],
			Bill.[DateCreated],
			Bill.[UserCreated],
			Store.[Name] AS StoreName,
			Supplier.[Name] AS SupplierName
		
		FROM Bill
		INNER JOIN [Store] ON Store.Id = Bill.StoreId
		INNER JOIN [Supplier] ON Supplier.Id = Bill.SupplierId
		WHERE Bill.FactureNumber = @FactureNumber
	END
