CREATE PROCEDURE [dbo].[GetInvoice]
	@Id BIGINT

	AS
	BEGIN
		SELECT 
			Invoice.[Id],
			Invoice.[StoreId],
			Invoice.[Number],
			Invoice.[BillNumber],
			Invoice.[BillId],
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
		WHERE Invoice.Id = @Id
	END
