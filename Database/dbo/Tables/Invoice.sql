CREATE TABLE [dbo].[Invoice]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
	[StoreId] INT NOT NULL, 
    [Number] NVARCHAR(50) NOT NULL, 
    [BillBumber] NVARCHAR(50) NULL DEFAULT NULL, 
    [BillId] BIGINT NULL, 
    [SupplierId] INT NOT NULL, 
    [Date] DATETIME2 NOT NULL, 
    [Sum] DECIMAL(10, 2) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL, 
    [UserCreated] NVARCHAR(255) NOT NULL,

	CONSTRAINT [FK_Invoice_Store] FOREIGN KEY ([StoreId]) REFERENCES [Store]([Id])

)
