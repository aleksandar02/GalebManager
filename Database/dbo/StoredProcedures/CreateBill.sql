CREATE PROCEDURE [dbo].[CreateBill]
(
	@StoreId INT,
	@Number NVARCHAR(50),
	@FactureNumber NVARCHAR(50),
	@SupplierId INT,
	@Date DATETIME2(7),
	@Sum DECIMAL(10,2),
	@DateCreated DATETIME2(7),
	@UserCreated NVARCHAR(255)
)

AS
	BEGIN
		INSERT INTO Bill([StoreId], [Number], [FactureNumber], [SupplierId], [Date], [Sum], [DateCreated], [UserCreated])
				  VALUES(@StoreId, @Number, @FactureNumber, @SupplierId, @Date, @Sum, @DateCreated, @UserCreated)
	END