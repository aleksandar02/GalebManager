CREATE PROCEDURE [dbo].[UpdateInvoice]
(
	@Id BIGINT,
	@StoreId INT,
	@Number NVARCHAR(50),
	@BillNumber NVARCHAR(50),
	@SupplierId INT,
	@Date DATETIME2(7),
	@Sum DECIMAL(10, 2),
	@DateCreated DATETIME2(7),
	@UserCreated NVARCHAR(255)
)
	AS
		BEGIN
			UPDATE Invoice 
			SET [StoreId] = @StoreId, 
				[Number] = @Number,
				[BillNumber] = @BillNumber,
				[SupplierId] = @SupplierId,
				[Date] = @Date,
				[Sum] = @Sum,
				[DateCreateD] = @DateCreated,
				[UserCreated] = @UserCreated

			WHERE [Id] = @Id;
		END
