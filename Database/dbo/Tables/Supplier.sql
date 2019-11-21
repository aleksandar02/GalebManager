CREATE TABLE [dbo].[Supplier]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name]    NVARCHAR(128) NOT NULL, 
    [Address] NVARCHAR(128) NULL, 
    [City] NVARCHAR(128) NULL, 
    [Telephone] NVARCHAR(10) NULL,
)
