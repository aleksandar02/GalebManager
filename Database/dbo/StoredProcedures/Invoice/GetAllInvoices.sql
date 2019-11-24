CREATE PROCEDURE [dbo].[GetAllInvoices]
AS
	BEGIN
		SELECT 
			Invoice.[Id],
			Invoice.[StoreId],
			Invoice.[Number],
			Invoice.[BillNumber],
			Invoice.[SupplierId],
			Invoice.[Date],
			Invoice.[Sum],
			Invoice.[DateCreated],
			Invoice.[UserCreated],
			Store.[Name] AS StoreName,
			Supplier.[Name] AS SupplierName

		FROM Invoice 
		INNER JOIN Store ON Store.Id = Invoice.StoreId
		INNER JOIN Supplier ON Supplier.Id = Invoice.SupplierId
		ORDER BY Invoice.DateCreated DESC
	END
