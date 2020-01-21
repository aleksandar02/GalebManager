CREATE PROCEDURE [dbo].[AddBillToFacture]
	@InvoiceId INT,
	@StoreId INT,
	@Number NVARCHAR(50),
	@FactureNumber NVARCHAR(50),
	@SupplierId INT,
	@Date DATETIME2(7),
	@Sum DECIMAL(10,2),
	@DateCreated DATETIME2(7),
	@UserCreated NVARCHAR(255)
AS
	BEGIN
		BEGIN TRAN
			BEGIN TRY
				UPDATE Invoice SET BillNumber = @Number WHERE Id = @InvoiceId;

				INSERT INTO Bill([StoreId], [Number], [FactureNumber], [SupplierId], [Date], [Sum], [DateCreated], [UserCreated])
					  VALUES(@StoreId, @Number, @FactureNumber, @SupplierId, @Date, @Sum, @DateCreated, @UserCreated)				

			END TRY
			BEGIN CATCH
				ROLLBACK TRAN
			END CATCH

			COMMIT TRAN
	END