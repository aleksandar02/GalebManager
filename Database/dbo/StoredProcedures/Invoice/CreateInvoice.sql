CREATE PROCEDURE [dbo].[CreateInvoice]
(
	@StoreId INT,
	@Number NVARCHAR(50),
	@BillNumber NVARCHAR(50),
	@BillId BIGINT,
	@SupplierId INT,
	@Date DATETIME2(7),
	@Sum DECIMAL(10, 2),
	@DateCreated DATETIME2(7),
	@UserCreated NVARCHAR(255)
)

AS
	BEGIN
		INSERT INTO Invoice ([StoreId], [Number], [BillNumber], [BillId], [SupplierId], [Date], [Sum], [DateCreated], [UserCreated])
					  VALUES(@StoreId, @Number, @BillNumber, @BillId, @SupplierId, @Date, @Sum, @DateCreated, @UserCreated)
	END