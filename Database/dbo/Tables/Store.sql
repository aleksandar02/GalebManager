﻿CREATE TABLE [dbo].[Store]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(255) NOT NULL, 
    [Address] NVARCHAR(255) NOT NULL, 
    [City] NVARCHAR(255) NOT NULL
)
