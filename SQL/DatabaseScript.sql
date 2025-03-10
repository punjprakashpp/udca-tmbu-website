USE [master]
GO
/****** Object:  Database [udcatmbu]    Script Date: 03/09/2025 22:46:00 ******/
CREATE DATABASE [udcatmbu] ON  PRIMARY
GO
ALTER DATABASE [udcatmbu] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [udcatmbu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [udcatmbu] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [udcatmbu] SET ANSI_NULLS OFF
GO
ALTER DATABASE [udcatmbu] SET ANSI_PADDING OFF
GO
ALTER DATABASE [udcatmbu] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [udcatmbu] SET ARITHABORT OFF
GO
ALTER DATABASE [udcatmbu] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [udcatmbu] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [udcatmbu] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [udcatmbu] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [udcatmbu] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [udcatmbu] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [udcatmbu] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [udcatmbu] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [udcatmbu] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [udcatmbu] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [udcatmbu] SET  DISABLE_BROKER
GO
ALTER DATABASE [udcatmbu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [udcatmbu] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [udcatmbu] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [udcatmbu] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [udcatmbu] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [udcatmbu] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [udcatmbu] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [udcatmbu] SET  READ_WRITE
GO
ALTER DATABASE [udcatmbu] SET RECOVERY SIMPLE
GO
ALTER DATABASE [udcatmbu] SET  MULTI_USER
GO
ALTER DATABASE [udcatmbu] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [udcatmbu] SET DB_CHAINING OFF
GO
USE [udcatmbu]
GO
/****** Object:  Table [dbo].[Visit]    Script Date: 03/09/2025 22:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Count] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 03/09/2025 22:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MidName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](256) NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 03/09/2025 22:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[Session] [nvarchar](50) NOT NULL,
	[RollNo] [nvarchar](50) NOT NULL,
	[RegNo] [nvarchar](50) NOT NULL,
	[RegYear] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MidName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[DOB] [date] NOT NULL,
	[Alumnus] [nvarchar](10) NOT NULL,
	[Achiever] [nvarchar](10) NOT NULL,
	[Qualification] [nvarchar](100) NULL,
	[Achievement] [nvarchar](100) NULL,
	[Occupation] [nvarchar](100) NULL,
	[Company] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Facebook] [nvarchar](200) NULL,
	[Instagram] [nvarchar](200) NULL,
	[Twitter] [nvarchar](200) NULL,
	[GitHub] [nvarchar](200) NULL,
	[LinkedIn] [nvarchar](200) NULL,
	[FilePath] [nvarchar](200) NULL,
	[EntryDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 03/09/2025 22:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[Align] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Qualification] [nvarchar](100) NULL,
	[Position] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[UploadDate] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Links]    Script Date: 03/09/2025 22:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Links](
	[LinkID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[LinkText] [nvarchar](255) NOT NULL,
	[LinkURL] [nvarchar](500) NOT NULL,
	[UploadDate] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 03/09/2025 22:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nchar](10) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[UploadDate] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Docs]    Script Date: 03/09/2025 22:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Docs](
	[DocsID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nchar](10) NOT NULL,
	[No] [nvarchar](50) NULL,
	[Semester] [nvarchar](50) NULL,
	[Session] [nvarchar](50) NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Date] [date] NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[UploadDate] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
