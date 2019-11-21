CREATE PROCEDURE [dbo].[GetStores]
AS
	BEGIN
		SELECT 
			[Id],
			[Name],
			[Address],
			[City]

			FROM Store;
	END
