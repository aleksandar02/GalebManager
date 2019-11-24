CREATE PROCEDURE [dbo].[SearchBills]
(
	@StoreId INT,
	@SupplierId INT,
	@DateFrom DATETIME2(7),
	@DateTo DATETIME2(7),
	@FactureStatus INT
)
AS
	BEGIN
		SELECT 
			B.[Id],
			B.[StoreId],
			B.[Number],
			B.[FactureNumber],
			B.[SupplierId],
			B.[Date],
			B.[Sum],
			B.[DateCreated],
			B.[UserCreated],
			Store.[Name] AS StoreName,
			Supplier.[Name] AS SupplierName

		FROM Bill B
		INNER JOIN [Supplier] ON Supplier.Id = B.SupplierId	
		INNER JOIN [Store] ON Store.Id = B.StoreId
		LEFT JOIN  [Invoice] I ON (I.BillNumber = B.Number)


		WHERE (B.[Date] >= @DateFrom AND B.[Date] <= @DateTo)   AND
		      (B.[StoreId]  = @StoreId OR @StoreId = -1) AND
			  (B.[SupplierId]  = @SupplierId OR @SupplierId = -1) AND
			  ((I.[BillNumber] IS NOT NULL AND @FactureStatus = 1) OR 
			  (I.[BillNumber] IS  NULL AND @FactureStatus = 2)  OR @FactureStatus = -1)
	END
