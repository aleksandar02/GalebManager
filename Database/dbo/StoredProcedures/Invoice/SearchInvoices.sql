CREATE PROCEDURE [dbo].[SearchInvoices]
(
	@StoreId INT,
	@SupplierId INT,
	@DateFrom DATETIME2(7),
	@DateTo DATETIME2(7),
	@FactureStatus INT
)
AS
	BEGIN
		SELECT DISTINCT
			I.[Id],
			I.[StoreId],
			I.[Number],
			I.[BillNumber],
			I.[SupplierId],
			I.[Date],
			I.[Sum],
			I.[DateCreated],
			I.[UserCreated],
			Store.[Name] AS StoreName,
			Supplier.[Name] AS SupplierName

		FROM Invoice I
		INNER JOIN [Supplier] ON Supplier.Id = I.SupplierId	
		INNER JOIN [Store] ON Store.Id = I.StoreId
		LEFT JOIN  [Bill] B ON (B.FactureNumber = I.Number)


		WHERE (I.[Date] >= @DateFrom AND I.[Date] <= @DateTo)   AND
		      (I.[StoreId]  = @StoreId OR @StoreId = -1) AND
			  (I.[SupplierId]  = @SupplierId OR @SupplierId = -1) AND
			  ((B.[FactureNumber] IS NOT NULL AND @FactureStatus = 1) OR 
			  (B.[FactureNumber] IS  NULL AND @FactureStatus = 2)  OR @FactureStatus = -1)
		ORDER BY I.DateCreated DESC
	END