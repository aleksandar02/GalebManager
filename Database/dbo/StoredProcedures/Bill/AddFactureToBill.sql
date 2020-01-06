CREATE PROCEDURE [dbo].[AddFactureToBill]
	@BillId INT,
	@StoreId INT,
	@Number NVARCHAR(50),
	@BillNumber NVARCHAR(50),
	@SupplierId INT,
	@Date DATETIME2(7),
	@Sum DECIMAL(10,2),
	@DateCreated DATETIME2(7),
	@UserCreated NVARCHAR(255)
AS
	BEGIN
		BEGIN TRAN
			BEGIN TRY
				UPDATE Bill SET FactureNumber = @Number WHERE Id = @BillId;

				INSERT INTO Invoice ([StoreId], [Number], [BillNumber], [SupplierId], [Date], [Sum], [DateCreated], [UserCreated])
					  VALUES(@StoreId, @Number, @BillNumber, @SupplierId, @Date, @Sum, @DateCreated, @UserCreated)				

			END TRY
			BEGIN CATCH
				ROLLBACK TRAN
			END CATCH

			COMMIT TRAN
	END