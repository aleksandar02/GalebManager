CREATE PROCEDURE [dbo].[GetSuppliers]
AS
	BEGIN
		SELECT 
			[Id],
			[Name],
			[Address],
			[City],
			[Telephone]

			FROM Supplier;
	END
