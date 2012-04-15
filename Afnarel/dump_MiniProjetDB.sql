USE [master]
GO
/****** Object:  Database [MiniProjetDB]    Script Date: 04/15/2012 16:14:08 ******/
CREATE DATABASE [MiniProjetDB] ON  PRIMARY 
( NAME = N'MiniProjetDB', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\MiniProjetDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MiniProjetDB_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\MiniProjetDB_log.ldf' , SIZE = 3136KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MiniProjetDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MiniProjetDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MiniProjetDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [MiniProjetDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [MiniProjetDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [MiniProjetDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [MiniProjetDB] SET ARITHABORT OFF
GO
ALTER DATABASE [MiniProjetDB] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [MiniProjetDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [MiniProjetDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [MiniProjetDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [MiniProjetDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [MiniProjetDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [MiniProjetDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [MiniProjetDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [MiniProjetDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [MiniProjetDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [MiniProjetDB] SET  DISABLE_BROKER
GO
ALTER DATABASE [MiniProjetDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [MiniProjetDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [MiniProjetDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [MiniProjetDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [MiniProjetDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [MiniProjetDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [MiniProjetDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [MiniProjetDB] SET  READ_WRITE
GO
ALTER DATABASE [MiniProjetDB] SET RECOVERY SIMPLE
GO
ALTER DATABASE [MiniProjetDB] SET  MULTI_USER
GO
ALTER DATABASE [MiniProjetDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [MiniProjetDB] SET DB_CHAINING OFF
GO
USE [MiniProjetDB]
GO
/****** Object:  Table [dbo].[user_album]    Script Date: 04/15/2012 16:14:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_album](
	[id_user] [int] NOT NULL,
	[id_album] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 04/15/2012 16:14:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[image]    Script Date: 04/15/2012 16:14:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[image](
	[hash] [varchar](40) NOT NULL,
	[size] [int] NOT NULL,
	[blob] [image] NOT NULL,
	[name] [varchar](50) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[album_photo]    Script Date: 04/15/2012 16:14:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[album_photo](
	[id_album] [int] NOT NULL,
	[id_photo] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[album]    Script Date: 04/15/2012 16:14:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[album](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
