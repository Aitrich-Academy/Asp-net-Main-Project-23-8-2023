USE [master]
GO
/****** Object:  Database [E_COMMERCE]    Script Date: 9/4/2023 12:24:52 PM ******/
CREATE DATABASE [E_COMMERCE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'E_COMMERCE', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\E_COMMERCE.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'E_COMMERCE_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\E_COMMERCE_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [E_COMMERCE] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [E_COMMERCE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [E_COMMERCE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [E_COMMERCE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [E_COMMERCE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [E_COMMERCE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [E_COMMERCE] SET ARITHABORT OFF 
GO
ALTER DATABASE [E_COMMERCE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [E_COMMERCE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [E_COMMERCE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [E_COMMERCE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [E_COMMERCE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [E_COMMERCE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [E_COMMERCE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [E_COMMERCE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [E_COMMERCE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [E_COMMERCE] SET  ENABLE_BROKER 
GO
ALTER DATABASE [E_COMMERCE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [E_COMMERCE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [E_COMMERCE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [E_COMMERCE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [E_COMMERCE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [E_COMMERCE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [E_COMMERCE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [E_COMMERCE] SET RECOVERY FULL 
GO
ALTER DATABASE [E_COMMERCE] SET  MULTI_USER 
GO
ALTER DATABASE [E_COMMERCE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [E_COMMERCE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [E_COMMERCE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [E_COMMERCE] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [E_COMMERCE] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [E_COMMERCE] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'E_COMMERCE', N'ON'
GO
ALTER DATABASE [E_COMMERCE] SET QUERY_STORE = OFF
GO
USE [E_COMMERCE]
GO
/****** Object:  User [HiremeNow]    Script Date: 9/4/2023 12:24:52 PM ******/
CREATE USER [HiremeNow] FOR LOGIN [HiremeNow] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[CATEGORY]    Script Date: 9/4/2023 12:24:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORY](
	[CAT_ID] [int] IDENTITY(1,1) NOT NULL,
	[CAT_NAME] [varchar](50) NOT NULL,
	[CAT_DESC] [varchar](50) NOT NULL,
	[CAT_IMAGE] [varbinary](max) NULL,
	[CAT_STATUS] [varchar](50) NOT NULL,
	[CAT_CREATEDBY] [varchar](50) NOT NULL,
	[CAT_CREATEDDATE] [varchar](50) NOT NULL,
	[CAT_MODIBY] [varchar](50) NOT NULL,
	[CAT_MODIDATE] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CATEGORY] PRIMARY KEY CLUSTERED 
(
	[CAT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORDERS]    Script Date: 9/4/2023 12:24:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORDERS](
	[ORD_ID] [int] IDENTITY(1,1) NOT NULL,
	[ORD_USERID] [int] NOT NULL,
	[ORD_PROID] [int] NOT NULL,
	[ORD_QTY] [int] NOT NULL,
	[ORD_TOTAL] [int] NOT NULL,
	[ORD_STATUS] [varchar](50) NOT NULL,
	[ORD_CREATEBY] [varchar](50) NOT NULL,
	[ORD_CREATEDATE] [varchar](50) NOT NULL,
	[ORD_MODIBY] [varchar](50) NOT NULL,
	[ORD_MODIDATE] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ORDERS] PRIMARY KEY CLUSTERED 
(
	[ORD_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCTS]    Script Date: 9/4/2023 12:24:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTS](
	[PRO_ID] [int] IDENTITY(1,1) NOT NULL,
	[PRO_NAME] [varchar](50) NOT NULL,
	[PROCAT_ID] [int] NOT NULL,
	[PRO_DESC] [varchar](100) NOT NULL,
	[PRO_STOCK] [int] NOT NULL,
	[PRO_IMAGE] [varbinary](max) NULL,
	[PRO_PRICE] [int] NOT NULL,
	[PRO_STATUS] [varchar](50) NOT NULL,
	[PRO_CREATEBY] [varchar](50) NOT NULL,
	[PRO_CREATEDATE] [varchar](50) NOT NULL,
	[PRO_MODIBY] [varchar](50) NOT NULL,
	[PRO_MODIDATE] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PRODUCTS] PRIMARY KEY CLUSTERED 
(
	[PRO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USER]    Script Date: 9/4/2023 12:24:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER](
	[USER_ID] [int] IDENTITY(1,1) NOT NULL,
	[USER_NAME] [varchar](50) NOT NULL,
	[USER_EMAIL] [varchar](50) NOT NULL,
	[USER_PASSWORD] [varchar](50) NOT NULL,
	[USER_PHONE] [varchar](50) NOT NULL,
	[USER_ADDRESS] [varchar](50) NOT NULL,
	[USER_IMAGE] [varbinary](max) NULL,
	[USER_ROLE] [varchar](50) NOT NULL,
	[USER_STATUS] [varchar](50) NOT NULL,
	[USER_CREATEBY] [varchar](50) NOT NULL,
	[USER_CREATEDATE] [varchar](50) NOT NULL,
	[USER_MODIBY] [varchar](50) NOT NULL,
	[USER_MODIDATE] [varchar](50) NOT NULL,
 CONSTRAINT [PK_USER] PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_USER]    Script Date: 9/4/2023 12:24:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_USER] ON [dbo].[USER]
(
	[USER_EMAIL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ORDERS]  WITH CHECK ADD  CONSTRAINT [FK_ORDERS_PRODUCTS] FOREIGN KEY([ORD_PROID])
REFERENCES [dbo].[PRODUCTS] ([PRO_ID])
GO
ALTER TABLE [dbo].[ORDERS] CHECK CONSTRAINT [FK_ORDERS_PRODUCTS]
GO
ALTER TABLE [dbo].[ORDERS]  WITH CHECK ADD  CONSTRAINT [FK_ORDERS_USER] FOREIGN KEY([ORD_USERID])
REFERENCES [dbo].[USER] ([USER_ID])
GO
ALTER TABLE [dbo].[ORDERS] CHECK CONSTRAINT [FK_ORDERS_USER]
GO
ALTER TABLE [dbo].[PRODUCTS]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCTS_CATEGORY] FOREIGN KEY([PROCAT_ID])
REFERENCES [dbo].[CATEGORY] ([CAT_ID])
GO
ALTER TABLE [dbo].[PRODUCTS] CHECK CONSTRAINT [FK_PRODUCTS_CATEGORY]
GO
USE [master]
GO
ALTER DATABASE [E_COMMERCE] SET  READ_WRITE 
GO
