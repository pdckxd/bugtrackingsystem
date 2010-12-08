USE [Portal]
GO

/****** Object:  Table [dbo].[Kpw_Images]    Script Date: 05/13/2010 22:53:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kpw_Images]') AND type in (N'U'))
DROP TABLE [dbo].[Kpw_Images]
GO

/****** Object:  Table [dbo].[Kpw_Images]    Script Date: 05/13/2010 22:53:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kpw_Applies]') AND type in (N'U'))
DROP TABLE [dbo].[Kpw_Applies]
GO

USE [Portal]
GO

/****** Object:  Table [dbo].[Kpw_Images]    Script Date: 05/13/2010 22:53:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kpw_Images](
	[ID] [int] IDENTITY NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Size] [int] NOT NULL,
	[Image] [nvarchar](500) NOT NULL,
	[ThumbImage] [nvarchar](500) NOT NULL,
	[BigImage] [nvarchar](500) NOT NULL,
	[FitsImage] [nvarchar](500) NOT NULL,
	[StarName] [nvarchar](50) NULL,
	[Ra] [int] NULL,
	[Dec] [int] NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Kpw_Applies](
	[ID] [int] IDENTITY NOT NULL,
	[UserId] [nvarchar](100) NOT NULL,
	[ApplyDate] [datetime] NOT NULL,
	[TimeRange] [int] NOT NULL,
	[ApplyStatus] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]

GO


