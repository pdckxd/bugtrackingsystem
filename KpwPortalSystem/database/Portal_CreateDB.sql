-- ===============================================
-- CONFIG ASP.NET PORTAL STARTER KIT DATABASE
-- Create Database Script
-- 
-- Version:	2.0 - 11/2002
--
-- ===============================================

CREATE DATABASE [Portal]
GO

USE [Portal]
GO
exec sp_dboption N'Portal', N'autoclose', N'false'
GO

exec sp_dboption N'Portal', N'bulkcopy', N'false'
GO

exec sp_dboption N'Portal', N'trunc. log', N'false'
GO

exec sp_dboption N'Portal', N'torn page detection', N'true'
GO

exec sp_dboption N'Portal', N'read only', N'false'
GO

exec sp_dboption N'Portal', N'dbo use', N'false'
GO

exec sp_dboption N'Portal', N'single', N'false'
GO

exec sp_dboption N'Portal', N'autoshrink', N'false'
GO

exec sp_dboption N'Portal', N'ANSI null default', N'false'
GO

exec sp_dboption N'Portal', N'recursive triggers', N'false'
GO

exec sp_dboption N'Portal', N'ANSI nulls', N'false'
GO

exec sp_dboption N'Portal', N'concat null yields null', N'false'
GO

exec sp_dboption N'Portal', N'cursor close on commit', N'false'
GO

exec sp_dboption N'Portal', N'default to local cursor', N'false'
GO

exec sp_dboption N'Portal', N'quoted identifier', N'false'
GO

exec sp_dboption N'Portal', N'ANSI warnings', N'false'
GO

exec sp_dboption N'Portal', N'auto create statistics', N'true'
GO

exec sp_dboption N'Portal', N'auto update statistics', N'true'
GO

if( ( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) ) or ( (@@microsoftversion / power(2, 24) = 7) and (@@microsoftversion & 0xffff >= 1082) ) )
	exec sp_dboption N'Portal', N'db chaining', N'false'
GO

use [Portal]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserRoles_Roles]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Portal_UserRoles] DROP CONSTRAINT FK_UserRoles_Roles
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserRoles_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Portal_UserRoles] DROP CONSTRAINT FK_UserRoles_Users
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_AddAnnouncement]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_AddAnnouncement]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_AddContact]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_AddContact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_AddEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_AddEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_AddLink]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_AddLink]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_AddMessage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_AddMessage]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_AddRole]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_AddRole]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_AddUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_AddUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_AddUserRole]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_AddUserRole]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_DeleteAnnouncement]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_DeleteAnnouncement]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_DeleteContact]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_DeleteContact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_DeleteDocument]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_DeleteDocument]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_DeleteEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_DeleteEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_DeleteLink]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_DeleteLink]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_DeleteModule]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_DeleteModule]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_DeleteRole]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_DeleteRole]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_DeleteUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_DeleteUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_DeleteUserRole]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_DeleteUserRole]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetAnnouncements]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetAnnouncements]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetAuthRoles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetAuthRoles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetContacts]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetContacts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetDocumentContent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetDocumentContent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetDocuments]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetDocuments]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetEvents]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetHtmlText]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetHtmlText]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetLinks]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetLinks]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetNextMessageID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetNextMessageID]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetPortalRoles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetPortalRoles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetPrevMessageID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetPrevMessageID]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetRoleMembership]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetRoleMembership]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetRolesByUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetRolesByUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetSingleAnnouncement]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetSingleAnnouncement]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetSingleContact]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetSingleContact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetSingleDocument]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetSingleDocument]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetSingleEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetSingleEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetSingleLink]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetSingleLink]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetSingleMessage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetSingleMessage]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetSingleRole]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetSingleRole]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetSingleUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetSingleUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetThreadMessages]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetThreadMessages]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetTopLevelMessages]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetTopLevelMessages]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_GetUsers]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_GetUsers]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UpdateAnnouncement]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_UpdateAnnouncement]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UpdateContact]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_UpdateContact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UpdateDocument]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_UpdateDocument]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UpdateEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_UpdateEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UpdateHtmlText]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_UpdateHtmlText]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UpdateLink]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_UpdateLink]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UpdateRole]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_UpdateRole]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UpdateUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_UpdateUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UserLogin]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Portal_UserLogin]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_Announcements]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_Announcements]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_Contacts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_Contacts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_Discussion]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_Discussion]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_Documents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_Documents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_Events]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_Events]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_HtmlText]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_HtmlText]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_Links]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_Links]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_Roles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_Roles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_UserRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_UserRoles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Portal_Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Portal_Users]
GO

CREATE TABLE [dbo].[Portal_Announcements] (
	[ItemID] [int] IDENTITY (0, 1) NOT NULL ,
	[ModuleID] [int] NOT NULL ,
	[CreatedByUser] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedDate] [datetime] NULL ,
	[Title] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[MoreLink] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[MobileMoreLink] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ExpireDate] [datetime] NULL ,
	[Description] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portal_Contacts] (
	[ItemID] [int] IDENTITY (0, 1) NOT NULL ,
	[ModuleID] [int] NOT NULL ,
	[CreatedByUser] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedDate] [datetime] NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Role] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Email] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Contact1] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Contact2] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portal_Discussion] (
	[ItemID] [int] IDENTITY (0, 1) NOT NULL ,
	[ModuleID] [int] NOT NULL ,
	[Title] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedDate] [datetime] NULL ,
	[Body] [nvarchar] (3000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DisplayOrder] [nvarchar] (750) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedByUser] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portal_Documents] (
	[ItemID] [int] IDENTITY (0, 1) NOT NULL ,
	[ModuleID] [int] NOT NULL ,
	[CreatedByUser] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedDate] [datetime] NULL ,
	[FileNameUrl] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FileFriendlyName] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Category] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Content] [image] NULL ,
	[ContentType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ContentSize] [int] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portal_Events] (
	[ItemID] [int] IDENTITY (0, 1) NOT NULL ,
	[ModuleID] [int] NOT NULL ,
	[CreatedByUser] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedDate] [datetime] NULL ,
	[Title] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[WhereWhen] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Description] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ExpireDate] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portal_HtmlText] (
	[ModuleID] [int] NOT NULL ,
	[DesktopHtml] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[MobileSummary] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[MobileDetails] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portal_Links] (
	[ItemID] [int] IDENTITY (0, 1) NOT NULL ,
	[ModuleID] [int] NOT NULL ,
	[CreatedByUser] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedDate] [datetime] NULL ,
	[Title] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Url] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[MobileUrl] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ViewOrder] [int] NULL ,
	[Description] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portal_Roles] (
	[RoleID] [int] IDENTITY (0, 1) NOT NULL ,
	[PortalID] [int] NOT NULL ,
	[RoleName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portal_UserRoles] (
	[UserID] [int] NOT NULL ,
	[RoleID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portal_Users] (
	[UserID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Password] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Email] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Portal_Announcements] ADD 
	CONSTRAINT [PK_Announcements] PRIMARY KEY  NONCLUSTERED 
	(
		[ItemID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Portal_Contacts] ADD 
	CONSTRAINT [PK_Contacts] PRIMARY KEY  NONCLUSTERED 
	(
		[ItemID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Portal_Discussion] ADD 
	CONSTRAINT [PK_Discussion] PRIMARY KEY  NONCLUSTERED 
	(
		[ItemID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Portal_Documents] ADD 
	CONSTRAINT [PK_Documents] PRIMARY KEY  NONCLUSTERED 
	(
		[ItemID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Portal_Events] ADD 
	CONSTRAINT [PK_Events] PRIMARY KEY  NONCLUSTERED 
	(
		[ItemID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Portal_HtmlText] ADD 
	CONSTRAINT [PK_HtmlText] PRIMARY KEY  NONCLUSTERED 
	(
		[ModuleID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Portal_Links] ADD 
	CONSTRAINT [PK_Links] PRIMARY KEY  NONCLUSTERED 
	(
		[ItemID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Portal_Roles] ADD 
	CONSTRAINT [PK_PortalRoles] PRIMARY KEY  NONCLUSTERED 
	(
		[RoleID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Portal_Users] ADD 
	CONSTRAINT [PK_PortalUsers] PRIMARY KEY  NONCLUSTERED 
	(
		[UserID]
	)  ON [PRIMARY] ,
	CONSTRAINT [IX_PortalUsers] UNIQUE  NONCLUSTERED 
	(
		[Email]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Portal_UserRoles] ADD 
	CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY 
	(
		[RoleID]
	) REFERENCES [dbo].[Portal_Roles] (
		[RoleID]
	) ON DELETE CASCADE  NOT FOR REPLICATION ,
	CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY 
	(
		[UserID]
	) REFERENCES [dbo].[Portal_Users] (
		[UserID]
	) ON DELETE CASCADE  NOT FOR REPLICATION 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



-- =============================================================
-- create the stored procs
-- =============================================================
CREATE PROCEDURE Portal_AddAnnouncement
(
    @ModuleID       int,
    @UserName       nvarchar(100),
    @Title          nvarchar(150),
    @MoreLink       nvarchar(150),
    @MobileMoreLink nvarchar(150),
    @ExpireDate     DateTime,
    @Description    nvarchar(2000),
    @ItemID         int OUTPUT
)
AS

INSERT INTO Portal_Announcements
(
    ModuleID,
    CreatedByUser,
    CreatedDate,
    Title,
    MoreLink,
    MobileMoreLink,
    ExpireDate,
    Description
)

VALUES
(
    @ModuleID,
    @UserName,
    GetDate(),
    @Title,
    @MoreLink,
    @MobileMoreLink,
    @ExpireDate,
    @Description
)

SELECT
    @ItemID = @@Identity



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_AddContact
(
    @ModuleID int,
    @UserName nvarchar(100),
    @Name     nvarchar(50),
    @Role     nvarchar(100),
    @Email    nvarchar(100),
    @Contact1 nvarchar(250),
    @Contact2 nvarchar(250),
    @ItemID   int OUTPUT
)
AS

INSERT INTO Portal_Contacts
(
    CreatedByUser,
    CreatedDate,
    ModuleID,
    Name,
    Role,
    Email,
    Contact1,
    Contact2
)

VALUES
(
    @UserName,
    GetDate(),
    @ModuleID,
    @Name,
    @Role,
    @Email,
    @Contact1,
    @Contact2
)

SELECT
    @ItemID = @@Identity



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_AddEvent
(
    @ModuleID    int,
    @UserName    nvarchar(100),
    @Title       nvarchar(100),
    @ExpireDate  DateTime,
    @Description nvarchar(2000),
    @WhereWhen   nvarchar(100),
    @ItemID      int OUTPUT
)
AS

INSERT INTO Portal_Events
(
    ModuleID,
    CreatedByUser,
    Title,
    CreatedDate,
    ExpireDate,
    Description,
    WhereWhen
)

VALUES
(
    @ModuleID,
    @UserName,
    @Title,
    GetDate(),
    @ExpireDate,
    @Description,
    @WhereWhen
)

SELECT
    @ItemID = @@Identity



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_AddLink
(
    @ModuleID    int,
    @UserName    nvarchar(100),
    @Title       nvarchar(100),
    @Url         nvarchar(250),
    @MobileUrl   nvarchar(250),
    @ViewOrder   int,
    @Description nvarchar(2000),
    @ItemID      int OUTPUT
)
AS

INSERT INTO Portal_Links
(
    ModuleID,
    CreatedByUser,
    CreatedDate,
    Title,
    Url,
    MobileUrl,
    ViewOrder,
    Description
)
VALUES
(
    @ModuleID,
    @UserName,
    GetDate(),
    @Title,
    @Url,
    @MobileUrl,
    @ViewOrder,
    @Description
)

SELECT
    @ItemID = @@Identity



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE Portal_AddMessage
(
    @ItemID int OUTPUT,
    @Title nvarchar(100),
    @Body nvarchar(3000),
    @ParentID int,
    @UserName nvarchar(100),
    @ModuleID int
)   

AS 

/* Find DisplayOrder of parent item */
DECLARE @ParentDisplayOrder as nvarchar(750)

SET @ParentDisplayOrder = ""

SELECT 
    @ParentDisplayOrder = DisplayOrder
FROM Portal_Discussion 
WHERE 
    ItemID = @ParentID

INSERT INTO Portal_Discussion
(
    Title,
    Body,
    DisplayOrder,
    CreatedDate, 
    CreatedByUser,
    ModuleID
)

VALUES
(
    @Title,
    @Body,
    @ParentDisplayOrder + CONVERT( nvarchar(24), GetDate(), 21 ),
    GetDate(),
    @UserName,
    @ModuleID
)

SELECT 
    @ItemID = @@Identity



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE Portal_AddRole
(
    @PortalID    int,
    @RoleName    nvarchar(50),
    @RoleID      int OUTPUT
)
AS

INSERT INTO Portal_Roles
(
    PortalID,
    RoleName
)

VALUES
(
    @PortalID,
    @RoleName
)

SELECT
    @RoleID = @@Identity



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





CREATE  PROCEDURE Portal_AddUser
(
    @Name     nvarchar(50),
    @Email    nvarchar(100),
    @Password nvarchar(50),
    @UserID   int OUTPUT
)
AS

INSERT INTO Portal_Users
(
    Name,
    Email,
    Password
)

VALUES
(
    @Name,
    @Email,
    @Password
)

SELECT
    @UserID = @@Identity





GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_AddUserRole
(
    @UserID int,
    @RoleID int
)
AS

SELECT 
    *
FROM Portal_UserRoles

WHERE
    UserID=@UserID
    AND
    RoleID=@RoleID

/* only insert if the record doesn't yet exist */
IF @@Rowcount < 1

    INSERT INTO Portal_UserRoles
    (
        UserID,
        RoleID
    )

    VALUES
    (
        @UserID,
        @RoleID
    )



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE Portal_DeleteAnnouncement
(
    @ItemID int
)
AS

DELETE FROM Portal_Announcements

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_DeleteContact
(
    @ItemID int
)
AS

DELETE FROM Portal_Contacts

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_DeleteDocument
(
    @ItemID int
)
AS

DELETE FROM Portal_Documents

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_DeleteEvent
(
    @ItemID int
)
AS

DELETE FROM Portal_Events

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_DeleteLink
(
    @ItemID int
)
AS

DELETE FROM Portal_Links

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE  PROCEDURE Portal_DeleteModule
(
    @ModuleID       int
)
AS
      DELETE FROM Portal_Announcements
      WHERE ModuleID = @ModuleID

      DELETE FROM Portal_Contacts
      WHERE ModuleID = @ModuleID

      DELETE FROM Portal_Discussion
      WHERE ModuleID = @ModuleID

      DELETE FROM Portal_Documents
      WHERE ModuleID = @ModuleID

      DELETE FROM Portal_Events
      WHERE ModuleID = @ModuleID

      DELETE FROM Portal_HtmlText
      WHERE ModuleID = @ModuleID

      DELETE FROM Portal_Links
      WHERE ModuleID = @ModuleID


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_DeleteRole
(
    @RoleID int
)
AS

DELETE FROM Portal_Roles

WHERE
    RoleID = @RoleID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_DeleteUser
(
    @UserID int
)
AS

DELETE FROM Portal_Users

WHERE
    UserID=@UserID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_DeleteUserRole
(
    @UserID int,
    @RoleID int
)
AS

DELETE FROM Portal_UserRoles

WHERE
    UserID=@UserID
    AND
    RoleID=@RoleID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetAnnouncements
(
    @ModuleID int
)
AS

SELECT
    ItemID,
    CreatedByUser,
    CreatedDate,
    Title,
    MoreLink,
    MobileMoreLink,
    ExpireDate,
    Description

FROM Portal_Announcements

WHERE
    ModuleID = @ModuleID
  AND
    ExpireDate > GetDate()



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetAuthRoles
(
    @PortalID    int,
    @ModuleID    int,
    @AccessRoles nvarchar (256) OUTPUT,
    @EditRoles   nvarchar (256) OUTPUT
)
AS

SELECT  
    @AccessRoles = Portal_Tabs.AuthorizedRoles,
    @EditRoles   = Portal_Modules.AuthorizedEditRoles
    
FROM Portal_Modules
  INNER JOIN
    Portal_Tabs ON Portal_Modules.TabID = Portal_Tabs.TabID
    
WHERE   
    Portal_Modules.ModuleID = @ModuleID
  AND
    Portal_Tabs.PortalID = @PortalID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetContacts
(
    @ModuleID int
)
AS

SELECT
    ItemID,
    CreatedDate,
    CreatedByUser,
    Name,
    Role,
    Email,
    Contact1,
    Contact2

FROM Portal_Contacts

WHERE
    ModuleID = @ModuleID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetDocumentContent
(
    @ItemID int
)
AS

SELECT
    Content,
    ContentType,
    ContentSize,
    FileFriendlyName

FROM Portal_Documents

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetDocuments
(
    @ModuleID int
)
AS

SELECT
    ItemID,
    FileFriendlyName,
    FileNameUrl,
    CreatedByUser,
    CreatedDate,
    Category,
    ContentSize
    
FROM Portal_Documents

WHERE
    ModuleID = @ModuleID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetEvents
(
    @ModuleID int
)
AS

SELECT
    ItemID,
    Title,
    CreatedByUser,
    WhereWhen,
    CreatedDate,
    ExpireDate,
    Description

FROM Portal_Events

WHERE
    ModuleID = @ModuleID
  AND
    ExpireDate > GetDate()



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetHtmlText
(
    @ModuleID int
)
AS

SELECT
    *

FROM Portal_HtmlText

WHERE
    ModuleID = @ModuleID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetLinks
(
    @ModuleID int
)
AS

SELECT
    ItemID,
    CreatedByUser,
    CreatedDate,
    Title,
    Url,
    ViewOrder,
    Description

FROM Portal_Links

WHERE
    ModuleID = @ModuleID

ORDER BY
    ViewOrder



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetNextMessageID
(
    @ItemID int,
    @NextID int OUTPUT
)
AS

DECLARE @CurrentDisplayOrder as nvarchar(750)
DECLARE @CurrentModule as int

/* Find DisplayOrder of current item */
SELECT
    @CurrentDisplayOrder = DisplayOrder,
    @CurrentModule = ModuleID
FROM Portal_Discussion
WHERE
    ItemID = @ItemID

/* Get the next message in the same module */
SELECT Top 1
    @NextID = ItemID

FROM Portal_Discussion

WHERE
    DisplayOrder > @CurrentDisplayOrder
    AND
    ModuleID = @CurrentModule

ORDER BY
    DisplayOrder ASC

/* end of this thread? */
IF @@Rowcount < 1
    SET @NextID = null



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




/* returns all roles for the specified portal */
CREATE PROCEDURE Portal_GetPortalRoles
(
    @PortalID  int
)
AS

SELECT  
    RoleName,
    RoleID

FROM Portal_Roles

WHERE   
    PortalID = @PortalID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetPrevMessageID
(
    @ItemID int,
    @PrevID int OUTPUT
)
AS

DECLARE @CurrentDisplayOrder as nvarchar(750)
DECLARE @CurrentModule as int

/* Find DisplayOrder of current item */
SELECT
    @CurrentDisplayOrder = DisplayOrder,
    @CurrentModule = ModuleID
FROM Portal_Discussion
WHERE
    ItemID = @ItemID

/* Get the previous message in the same module */
SELECT Top 1
    @PrevID = ItemID

FROM Portal_Discussion

WHERE
    DisplayOrder < @CurrentDisplayOrder
    AND
    ModuleID = @CurrentModule

ORDER BY
    DisplayOrder DESC

/* already at the beginning of this module? */
IF @@Rowcount < 1
    SET @PrevID = null



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




/* returns all members for the specified role */
CREATE PROCEDURE Portal_GetRoleMembership
(
    @RoleID  int
)
AS

SELECT  
    Portal_UserRoles.UserID,
    Name,
    Email

FROM Portal_UserRoles
    
INNER JOIN 
    Portal_Users On Portal_Users.UserID = Portal_UserRoles.UserID

WHERE   
    Portal_UserRoles.RoleID = @RoleID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




/* returns all roles for the specified user */
CREATE PROCEDURE Portal_GetRolesByUser
(
    @Email         nvarchar(100)
)
AS

SELECT  
    Portal_Roles.RoleName,
    Portal_Roles.RoleID

FROM Portal_UserRoles
  INNER JOIN 
    Portal_Users ON Portal_UserRoles.UserID = Portal_Users.UserID
  INNER JOIN 
    Portal_Roles ON Portal_UserRoles.RoleID = Portal_Roles.RoleID

WHERE   
    Portal_Users.Email = @Email



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE  PROCEDURE Portal_GetSingleAnnouncement
(
    @ItemID int
)
AS

SELECT
    CreatedByUser,
    CreatedDate,
    Title,
    ModuleID,
    MoreLink,
    MobileMoreLink,
    ExpireDate,
    Description

FROM Portal_Announcements

WHERE
    ItemID = @ItemID


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





CREATE  PROCEDURE Portal_GetSingleContact
(
    @ItemID int
)
AS

SELECT
    CreatedByUser,
    CreatedDate,
    ModuleID,
    Name,
    Role,
    Email,
    Contact1,
    Contact2

FROM Portal_Contacts

WHERE
    ItemID = @ItemID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE  PROCEDURE Portal_GetSingleDocument
(
    @ItemID int
)
AS

SELECT
    FileFriendlyName,
    FileNameUrl,
    CreatedByUser,
    CreatedDate,
    Category,
    ContentSize,
    ModuleID

FROM Portal_Documents

WHERE
    ItemID = @ItemID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE  PROCEDURE Portal_GetSingleEvent
(
    @ItemID int
)
AS

SELECT
    CreatedByUser,
    CreatedDate,
    ModuleID,
    Title,
    ExpireDate,
    Description,
    WhereWhen

FROM Portal_Events

WHERE
    ItemID = @ItemID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE  PROCEDURE Portal_GetSingleLink
(
    @ItemID int
)
AS

SELECT
    CreatedByUser,
    CreatedDate,
    ModuleID,
    Title,
    Url,
    MobileUrl,
    ViewOrder,
    Description

FROM Portal_Links

WHERE
    ItemID = @ItemID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE  PROCEDURE Portal_GetSingleMessage
(
    @ItemID int
)
AS

DECLARE @nextMessageID int
EXECUTE Portal_GetNextMessageID @ItemID, @nextMessageID OUTPUT
DECLARE @prevMessageID int
EXECUTE Portal_GetPrevMessageID @ItemID, @prevMessageID OUTPUT

SELECT
    ItemID,
    ModuleID,
    Title,
    CreatedByUser,
    CreatedDate,
    Body,
    DisplayOrder,
    NextMessageID = @nextMessageID,
    PrevMessageID = @prevMessageID

FROM Portal_Discussion

WHERE
    ItemID = @ItemID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE Portal_GetSingleRole
(
    @RoleID int
)
AS

SELECT
    RoleName

FROM Portal_Roles

WHERE
    RoleID = @RoleID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetSingleUser
(
    @Email nvarchar(100)
)
AS

SELECT
    Email,
    Password,
    Name

FROM Portal_Users

WHERE
    Email = @Email



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE Portal_GetThreadMessages
(
    @Parent nvarchar(750)
)
AS

SELECT
    ItemID,
    DisplayOrder,
    REPLICATE( '&nbsp;', ( ( LEN( DisplayOrder ) / 23 ) - 1 ) * 5 ) AS Indent,
    Title,  
    CreatedByUser,
    CreatedDate,
    Body

FROM Portal_Discussion

WHERE
    LEFT(DisplayOrder, 23) = @Parent
  AND
    (LEN( DisplayOrder ) / 23 ) > 1

ORDER BY
    DisplayOrder



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_GetTopLevelMessages
(
    @ModuleID int
)
AS

SELECT
    ItemID,
    DisplayOrder,
    LEFT(DisplayOrder, 23) AS Parent,    
    (SELECT COUNT(*) -1  FROM Portal_Discussion Disc2 WHERE LEFT(Disc2.DisplayOrder,LEN(RTRIM(Disc.DisplayOrder))) = Disc.DisplayOrder) AS ChildCount,
    Title,  
    CreatedByUser,
    CreatedDate

FROM Portal_Discussion Disc

WHERE 
    ModuleID=@ModuleID
  AND
    (LEN( DisplayOrder ) / 23 ) = 1

ORDER BY
    DisplayOrder



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE Portal_GetUsers
AS

SELECT  
    UserID,
    Email

FROM Portal_Users
    
ORDER BY Email




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE Portal_UpdateAnnouncement
(
    @ItemID         int,
    @UserName       nvarchar(100),
    @Title          nvarchar(150),
    @MoreLink       nvarchar(150),
    @MobileMoreLink nvarchar(150),
    @ExpireDate     datetime,
    @Description    nvarchar(2000)
)
AS

UPDATE Portal_Announcements

SET
    CreatedByUser   = @UserName,
    CreatedDate     = GetDate(),
    Title           = @Title,
    MoreLink        = @MoreLink,
    MobileMoreLink  = @MobileMoreLink,
    ExpireDate      = @ExpireDate,
    Description     = @Description

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_UpdateContact
(
    @ItemID   int,
    @UserName nvarchar(100),
    @Name     nvarchar(50),
    @Role     nvarchar(100),
    @Email    nvarchar(100),
    @Contact1 nvarchar(250),
    @Contact2 nvarchar(250)
)
AS

UPDATE Portal_Contacts

SET
    CreatedByUser = @UserName,
    CreatedDate   = GetDate(),
    Name          = @Name,
    Role          = @Role,
    Email         = @Email,
    Contact1      = @Contact1,
    Contact2      = @Contact2

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_UpdateDocument
(
    @ItemID           int,
    @ModuleID         int,
    @FileFriendlyName nvarchar(150),
    @FileNameUrl      nvarchar(250),
    @UserName         nvarchar(100),
    @Category         nvarchar(50),
    @Content          image,
    @ContentType      nvarchar(50),
    @ContentSize      int
)
AS
IF (@ItemID=0) OR NOT EXISTS (
    SELECT 
        * 
    FROM Portal_Documents 
    WHERE 
        ItemID = @ItemID
)
INSERT INTO Portal_Documents
(
    ModuleID,
    FileFriendlyName,
    FileNameUrl,
    CreatedByUser,
    CreatedDate,
    Category,
    Content,
    ContentType,
    ContentSize
)

VALUES
(
    @ModuleID,
    @FileFriendlyName,
    @FileNameUrl,
    @UserName,
    GetDate(),
    @Category,
    @Content,
    @ContentType,
    @ContentSize
)
ELSE

BEGIN

IF (@ContentSize=0)

UPDATE Portal_Documents

SET 
    CreatedByUser    = @UserName,
    CreatedDate      = GetDate(),
    Category         = @Category,
    FileFriendlyName = @FileFriendlyName,
    FileNameUrl      = @FileNameUrl

WHERE
    ItemID = @ItemID
ELSE

UPDATE Portal_Documents

SET
    CreatedByUser     = @UserName,
    CreatedDate       = GetDate(),
    Category          = @Category,
    FileFriendlyName  = @FileFriendlyName,
    FileNameUrl       = @FileNameUrl,
    Content           = @Content,
    ContentType       = @ContentType,
    ContentSize       = @ContentSize

WHERE
    ItemID = @ItemID

END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE Portal_UpdateEvent
(
    @ItemID      int,
    @UserName    nvarchar(100),
    @Title       nvarchar(100),
    @ExpireDate  datetime,
    @Description nvarchar(2000),
    @WhereWhen   nvarchar(100)
)

AS

UPDATE Portal_Events

SET
    CreatedByUser = @UserName,
    CreatedDate   = GetDate(),
    Title         = @Title,
    ExpireDate    = @ExpireDate,
    Description   = @Description,
    WhereWhen     = @WhereWhen

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE Portal_UpdateHtmlText
(
    @ModuleID      int,
    @DesktopHtml   ntext,
    @MobileSummary ntext,
    @MobileDetails ntext
)
AS

IF NOT EXISTS (
    SELECT 
        * 
    FROM Portal_HtmlText 
    WHERE 
        ModuleID = @ModuleID
)
INSERT INTO Portal_HtmlText (
    ModuleID,
    DesktopHtml,
    MobileSummary,
    MobileDetails
) 
VALUES (
    @ModuleID,
    @DesktopHtml,
    @MobileSummary,
    @MobileDetails
)
ELSE
UPDATE Portal_HtmlText

SET
    DesktopHtml   = @DesktopHtml,
    MobileSummary = @MobileSummary,
    MobileDetails = @MobileDetails

WHERE
    ModuleID = @ModuleID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE Portal_UpdateLink
(
    @ItemID      int,
    @UserName    nvarchar(100),
    @Title       nvarchar(100),
    @Url         nvarchar(250),
    @MobileUrl   nvarchar(250),
    @ViewOrder   int,
    @Description nvarchar(2000)
)
AS

UPDATE Portal_Links

SET
    CreatedByUser = @UserName,
    CreatedDate   = GetDate(),
    Title         = @Title,
    Url           = @Url,
    MobileUrl     = @MobileUrl,
    ViewOrder     = @ViewOrder,
    Description   = @Description

WHERE
    ItemID = @ItemID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE Portal_UpdateRole
(
    @RoleID      int,
    @RoleName    nvarchar(50)
)
AS

UPDATE Portal_Roles

SET
    RoleName = @RoleName

WHERE
    RoleID = @RoleID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




CREATE  PROCEDURE Portal_UpdateUser
(
    @UserID        int,
    @Email           nvarchar(100),
    @Password    nvarchar(50)
)
AS

UPDATE Portal_Users

SET
    Email    = @Email,
    Password = @Password

WHERE
    UserID    = @UserID


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE  PROCEDURE Portal_UserLogin
(
    @Email    nvarchar(100),
    @Password nvarchar(50),
    @UserName nvarchar(100) OUTPUT
)
AS

SELECT
    @UserName = Name

FROM Portal_Users

WHERE
    Email = @Email
  AND
    Password = @Password




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

