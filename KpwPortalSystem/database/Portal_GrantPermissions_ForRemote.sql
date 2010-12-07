-- ===============================================
-- CONFIG ASP.NET Portal STARTER KIT DATABASE
-- Grant permissions for Remote DB Install
-- 
-- Version:	1.2 - 10/02 (mho)
--
-- ===============================================

USE master
IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = 'PortalUser')
BEGIN
    declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'
    if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
        select @logindb = N'master'
    if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
        select @loginlang = @@language
    exec sp_addlogin 'PortalUser', 'PortalUser', @logindb, @loginlang
END


USE [Portal]
EXEC sp_grantdbaccess N'PortalUser'
EXEC sp_addrolemember N'db_owner', N'PortalUser'
