-- ===============================================
-- CONFIG ASP.NET PORTAL STARTER KIT DATABASE
-- Create Database Script
-- 
-- Version:	2.0 - 11/2002
--
-- ===============================================

CREATE DATABASE [BugTrackingSystem]
GO

USE [BugTrackingSystem]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Bts_Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Bts_Users]
GO

CREATE TABLE [dbo].[Bts_Users] (
	[ID] [int] IDENTITY (0, 1) NOT NULL ,
	[UserID] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserName] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Email] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 	
) ON [PRIMARY]
GO

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

