CREATE TABLE Product (
    [ProductID] int IDENTITY(1,1) PRIMARY KEY,
    [Name] varchar(255),
    [Description] varchar(255),
    [Image] varchar(255),
);

CREATE TABLE Category (
    [CategoryID] int IDENTITY(1,1) PRIMARY KEY,
    [Name] varchar(255)
);

CREATE TABLE ProductCategory (
    [ProductID] int,
    [CategoryID] int,
	PRIMARY KEY ([ProductID], [CategoryID])
);

