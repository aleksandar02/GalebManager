CREATE PROCEDURE [dbo].[UpdateBill]
(
	@Id BIGINT,
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
		UPDATE Bill 
		SET 
			[StoreId] = @StoreId,
			[Number] = @Number,
			[FactureNumber] = @FactureNumber,
			[SupplierId] = @SupplierId,
			[Date] = @Date,
			[Sum] = @Sum,
			[DateCreated] = @DateCreated,
			[UserCreated] = @UserCreated

		WHERE [Id] = @Id
	END