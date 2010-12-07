-- ===============================================
-- CONFIG ASP.NET Portal STARTER KIT DATABASE
-- Drop Database Script
-- 
-- Version:	1.2 - 10/02 (mho)
--
-- ===============================================

USE [master]

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Portal')
BEGIN
	DECLARE @spid smallint
	DECLARE @sql varchar(4000)

	DECLARE crsr CURSOR FAST_FORWARD FOR
		SELECT spid FROM sysprocesses p INNER JOIN sysdatabases d ON d.[name] = 'Portal' AND p.dbid = d.dbid

	OPEN crsr
	FETCH NEXT FROM crsr INTO @spid

	WHILE @@FETCH_STATUS != -1
	BEGIN
		SET @sql = 'KILL ' + CAST(@spid AS varchar)
		EXEC(@sql) 
		FETCH NEXT FROM crsr INTO @spid
	END

	CLOSE crsr
	DEALLOCATE crsr

	DROP DATABASE [Portal]
END
GO