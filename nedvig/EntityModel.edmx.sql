
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/29/2023 15:04:45
-- Generated from EDMX file: C:\Users\lada4\OneDrive\Desktop\nedvig\nedvig\EntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AgentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AgentSet];
GO
IF OBJECT_ID(N'[dbo].[ClientSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientSet];
GO
IF OBJECT_ID(N'[dbo].[NeedSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NeedSet];
GO
IF OBJECT_ID(N'[dbo].[ApartmentNeedSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApartmentNeedSet];
GO
IF OBJECT_ID(N'[dbo].[LandNeedSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LandNeedSet];
GO
IF OBJECT_ID(N'[dbo].[HouseNeedSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HouseNeedSet];
GO
IF OBJECT_ID(N'[dbo].[EstateSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EstateSet];
GO
IF OBJECT_ID(N'[dbo].[ApartmentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApartmentSet];
GO
IF OBJECT_ID(N'[dbo].[LandSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LandSet];
GO
IF OBJECT_ID(N'[dbo].[HouseSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HouseSet];
GO
IF OBJECT_ID(N'[dbo].[OfferSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OfferSet];
GO
IF OBJECT_ID(N'[dbo].[DealSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DealSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AgentSet'
CREATE TABLE [dbo].[AgentSet] (
    [Id] int  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Comission] int  NOT NULL
);
GO

-- Creating table 'ClientSet'
CREATE TABLE [dbo].[ClientSet] (
    [Id] int  NOT NULL,
    [Surname] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Mail] nvarchar(max)  NULL,
    [Number] nvarchar(max)  NULL
);
GO

-- Creating table 'NeedSet'
CREATE TABLE [dbo].[NeedSet] (
    [Id] int  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Adress] nvarchar(max)  NULL,
    [MaxPrice] int  NULL,
    [MinPrice] int  NULL,
    [IdClient] int  NOT NULL,
    [IdAgent] int  NOT NULL
);
GO

-- Creating table 'ApartmentNeedSet'
CREATE TABLE [dbo].[ApartmentNeedSet] (
    [Id] int  NOT NULL,
    [MaxRoom] int  NULL,
    [MinRoom] int  NULL,
    [MaxFloor] int  NULL,
    [MinFloor] int  NULL,
    [MaxArea] float  NULL,
    [MinArea] float  NULL,
    [IdNeed] int  NOT NULL
);
GO

-- Creating table 'LandNeedSet'
CREATE TABLE [dbo].[LandNeedSet] (
    [Id] int  NOT NULL,
    [MaxArea] float  NULL,
    [MinArea] float  NULL,
    [IdNeed] int  NOT NULL
);
GO

-- Creating table 'HouseNeedSet'
CREATE TABLE [dbo].[HouseNeedSet] (
    [Id] int  NOT NULL,
    [MaxRoom] int  NULL,
    [MinRoom] int  NULL,
    [MaxFloor] int  NULL,
    [MinFloor] int  NULL,
    [MaxArea] float  NULL,
    [MinArea] float  NULL,
    [IdNeed] int  NOT NULL
);
GO

-- Creating table 'EstateSet'
CREATE TABLE [dbo].[EstateSet] (
    [Id] int  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Adress] nvarchar(max)  NULL,
    [Coordinate] nvarchar(max)  NULL
);
GO

-- Creating table 'ApartmentSet'
CREATE TABLE [dbo].[ApartmentSet] (
    [Id] int  NOT NULL,
    [Room] int  NULL,
    [Floor] int  NULL,
    [Area] float  NULL,
    [IdEstate] int  NOT NULL
);
GO

-- Creating table 'LandSet'
CREATE TABLE [dbo].[LandSet] (
    [Id] int  NOT NULL,
    [Area] float  NULL,
    [IdEstate] int  NOT NULL
);
GO

-- Creating table 'HouseSet'
CREATE TABLE [dbo].[HouseSet] (
    [Id] int  NOT NULL,
    [Room] int  NULL,
    [Floor] int  NULL,
    [Area] float  NULL,
    [IdEstate] int  NOT NULL
);
GO

-- Creating table 'OfferSet'
CREATE TABLE [dbo].[OfferSet] (
    [Id] int  NOT NULL,
    [IdClient] int  NOT NULL,
    [IdAgent] int  NOT NULL,
    [IdEstate] int  NOT NULL,
    [Price] int  NOT NULL
);
GO

-- Creating table 'DealSet'
CREATE TABLE [dbo].[DealSet] (
    [Id] int  NOT NULL,
    [IdNeed] int  NOT NULL,
    [IdOffer] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AgentSet'
ALTER TABLE [dbo].[AgentSet]
ADD CONSTRAINT [PK_AgentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientSet'
ALTER TABLE [dbo].[ClientSet]
ADD CONSTRAINT [PK_ClientSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NeedSet'
ALTER TABLE [dbo].[NeedSet]
ADD CONSTRAINT [PK_NeedSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ApartmentNeedSet'
ALTER TABLE [dbo].[ApartmentNeedSet]
ADD CONSTRAINT [PK_ApartmentNeedSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LandNeedSet'
ALTER TABLE [dbo].[LandNeedSet]
ADD CONSTRAINT [PK_LandNeedSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HouseNeedSet'
ALTER TABLE [dbo].[HouseNeedSet]
ADD CONSTRAINT [PK_HouseNeedSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EstateSet'
ALTER TABLE [dbo].[EstateSet]
ADD CONSTRAINT [PK_EstateSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ApartmentSet'
ALTER TABLE [dbo].[ApartmentSet]
ADD CONSTRAINT [PK_ApartmentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LandSet'
ALTER TABLE [dbo].[LandSet]
ADD CONSTRAINT [PK_LandSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HouseSet'
ALTER TABLE [dbo].[HouseSet]
ADD CONSTRAINT [PK_HouseSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OfferSet'
ALTER TABLE [dbo].[OfferSet]
ADD CONSTRAINT [PK_OfferSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DealSet'
ALTER TABLE [dbo].[DealSet]
ADD CONSTRAINT [PK_DealSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------