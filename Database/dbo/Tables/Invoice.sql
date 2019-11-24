CREATE TABLE [dbo].[Invoice]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[StoreId] INT NOT NULL, 
    [Number] NVARCHAR(50) NOT NULL, 
    [BillNumber] NVARCHAR(50) NULL DEFAULT NULL, 
    [SupplierId] INT NOT NULL, 
    [Date] DATETIME2 NOT NULL, 
    [Sum] DECIMAL(10, 2) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL, 
    [UserCreated] NVARCHAR(255) NOT NULL,

	CONSTRAINT [FK_Invoice_Store] FOREIGN KEY ([StoreId]) REFERENCES [Store]([Id]),
	CONSTRAINT [FK_Invoice_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Supplier]([Id])


)

GO

CREATE INDEX [IX_Invoice_Number] ON [dbo].[Invoice] ([Number])
