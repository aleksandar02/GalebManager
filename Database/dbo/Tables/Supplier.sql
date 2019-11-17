CREATE TABLE [dbo].[Supplier]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name]    NVARCHAR(128) NOT NULL, 
    [Address] NVARCHAR(128) NULL, 
    [City] NVARCHAR(128) NULL, 
    [Telephone] NCHAR(10) NULL,
)
