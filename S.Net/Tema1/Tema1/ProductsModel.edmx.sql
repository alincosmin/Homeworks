
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/26/2015 19:52:11
-- Generated from EDMX file: C:\Users\Alin\Documents\FII Homeworks\Homeworks\S.Net\Tema1\Tema1\ProductsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Tema1Db];
GO
IF SCHEMA_ID(N'ProductsManager') IS NULL EXECUTE(N'CREATE SCHEMA [ProductsManager]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProductCategories'
CREATE TABLE [ProductsManager].[ProductCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [ProductsManager].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductCategoryProduct'
CREATE TABLE [ProductsManager].[ProductCategoryProduct] (
    [ProductCategory_Id] int  NOT NULL,
    [Products_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProductCategories'
ALTER TABLE [ProductsManager].[ProductCategories]
ADD CONSTRAINT [PK_ProductCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [ProductsManager].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ProductCategory_Id], [Products_Id] in table 'ProductCategoryProduct'
ALTER TABLE [ProductsManager].[ProductCategoryProduct]
ADD CONSTRAINT [PK_ProductCategoryProduct]
    PRIMARY KEY CLUSTERED ([ProductCategory_Id], [Products_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductCategory_Id] in table 'ProductCategoryProduct'
ALTER TABLE [ProductsManager].[ProductCategoryProduct]
ADD CONSTRAINT [FK_ProductCategoryProduct_ProductCategory]
    FOREIGN KEY ([ProductCategory_Id])
    REFERENCES [ProductsManager].[ProductCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Products_Id] in table 'ProductCategoryProduct'
ALTER TABLE [ProductsManager].[ProductCategoryProduct]
ADD CONSTRAINT [FK_ProductCategoryProduct_Product]
    FOREIGN KEY ([Products_Id])
    REFERENCES [ProductsManager].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategoryProduct_Product'
CREATE INDEX [IX_FK_ProductCategoryProduct_Product]
ON [ProductsManager].[ProductCategoryProduct]
    ([Products_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------