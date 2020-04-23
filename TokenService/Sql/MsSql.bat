//*****************************************************
//  Setup of teh database using docker.
//  Includes SQL to create the database and the tables
//  Select queries
//*****************************************************


/****** Run MSSQL in a Docker container 
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Testmn0123" -p 1433:1433 --name sql1 -d mcr.microsoft.com/mssql/server:2017-latest  


/****** Create the database
CREATE DATABASE [OAuth];

USE [OAuth]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 4/2/2020 4:47:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier]  NOT NULL PRIMARY KEY,
	[UserName] [nvarchar](100)  NOT NULL,
	[Salt] [nvarchar](150) NULL,
	[HashPassword] [nvarchar](500) NULL	
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Authorize](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Client_Id] [uniqueidentifier] NOT NULL,
	[Scope] [nvarchar](150) NOT NULL,
	[Code] [nvarchar](150) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client_Id] [uniqueidentifier] NOT NULL,
	[ClientName] [nvarchar](150) NOT NULL, 
	[Client_Secret] [nvarchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Client_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [UserId]
      ,[UserName]
      ,[Salt]
      ,[HashPassword]
  FROM [OAuth].[dbo].[Users]
 

/****** Genrate the EF Core DB first classes
Scaffold-DbContext "Server=(local);Database=OAuth;User Id=secureadmin; Password=watershed100;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Infrastructure.Models -context SecurityContext  -Project Infrastructure -force
