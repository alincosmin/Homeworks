
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/26/2015 19:47:48
-- Generated from EDMX file: C:\Users\Alin\Documents\FII Homeworks\Homeworks\S.Net\Tema1\Tema1\CustomersModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Tema1Db];
GO
IF SCHEMA_ID(N'CustomerManager') IS NULL EXECUTE(N'CREATE SCHEMA [CustomerManager]');
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

-- Creating table 'CustomerTypes'
CREATE TABLE [CustomerManager].[CustomerTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [CustomerManager].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CustomerType_Id] int  NOT NULL
);
GO

-- Creating table 'CustomerEmails'
CREATE TABLE [CustomerManager].[CustomerEmails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Customer_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CustomerTypes'
ALTER TABLE [CustomerManager].[CustomerTypes]
ADD CONSTRAINT [PK_CustomerTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [CustomerManager].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerEmails'
ALTER TABLE [CustomerManager].[CustomerEmails]
ADD CONSTRAINT [PK_CustomerEmails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerType_Id] in table 'Customers'
ALTER TABLE [CustomerManager].[Customers]
ADD CONSTRAINT [FK_CustomerTypeCustomer]
    FOREIGN KEY ([CustomerType_Id])
    REFERENCES [CustomerManager].[CustomerTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerTypeCustomer'
CREATE INDEX [IX_FK_CustomerTypeCustomer]
ON [CustomerManager].[Customers]
    ([CustomerType_Id]);
GO

-- Creating foreign key on [Customer_Id] in table 'CustomerEmails'
ALTER TABLE [CustomerManager].[CustomerEmails]
ADD CONSTRAINT [FK_CustomerCustomerEmail]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [CustomerManager].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerCustomerEmail'
CREATE INDEX [IX_FK_CustomerCustomerEmail]
ON [CustomerManager].[CustomerEmails]
    ([Customer_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------