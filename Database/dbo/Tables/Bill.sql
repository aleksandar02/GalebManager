CREATE TABLE [dbo].[Bill]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [StoreId] INT NOT NULL, 
	[Number] NVARCHAR(50) NOT NULL,
    [FactureNumber] NVARCHAR(50) NULL DEFAULT NULL,
	[SupplierId] INT NOT NULL, 
	[Date] DATETIME2 NOT NULL, 
	[Sum] DECIMAL(10, 2) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL, 
    [UserCreated] NVARCHAR(255) NOT NULL, 

    CONSTRAINT [FK_Bill_Suppliers] FOREIGN KEY ([SupplierId]) REFERENCES [Supplier]([Id]), 
    CONSTRAINT [FK_Bill_Store] FOREIGN KEY ([StoreId]) REFERENCES [Store]([Id])
)

GO

CREATE INDEX [IX_Bill_Number] ON [dbo].[Bill] ([Number])
