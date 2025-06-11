USE [master]
GO
IF EXISTS 
    (SELECT [name] 
     FROM [master].[sys].[server_principals]
     WHERE [name] = 'HQ_SECURITY\WebApp_SDSFinder')
     
BEGIN
    DECLARE @CloseOpenSessions NVARCHAR(MAX)
    DECLARE Session_Cursor CURSOR FOR
    SELECT 'KILL ' + CAST([session_id] AS NVARCHAR(MAX))
        FROM [master].[sys].[dm_exec_sessions]
        WHERE [login_name] = 'HQ_SECURITY\WebApp_SDSFinder'

    OPEN Session_Cursor
    FETCH NEXT FROM Session_Cursor INTO @CloseOpenSessions
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC [sys].[sp_executesql] @CloseOpenSessions
        FETCH NEXT FROM Session_Cursor INTO @CloseOpenSessions
    END
    CLOSE Session_Cursor
    DEALLOCATE Session_Cursor

    DROP LOGIN [HQ_SECURITY\WebApp_SDSFinder]
    PRINT 'Existing login dropped'
END
GO
CREATE LOGIN [HQ_SECURITY\WebApp_SDSFinder] FROM WINDOWS WITH DEFAULT_DATABASE=[master]
GO
use [master];
GO

USE [SDSFinder]
GO
IF EXISTS 
    (SELECT [name] 
     FROM [SDSFinder].[sys].[database_principals]
     WHERE [name] = 'HQ_SECURITY\WebApp_SDSFinder')
BEGIN
    DROP USER [HQ_SECURITY\WebApp_SDSFinder];
    PRINT 'Existing user dropped in App DB'
END
GO

CREATE USER [HQ_SECURITY\WebApp_SDSFinder] FOR LOGIN [HQ_SECURITY\WebApp_SDSFinder]
GO
USE [SDSFinder]
GO
ALTER ROLE [db_datareader] ADD MEMBER [HQ_SECURITY\WebApp_SDSFinder]
GO
USE [SDSFinder]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [HQ_SECURITY\WebApp_SDSFinder]

USE [Common]
GO
IF EXISTS 
    (SELECT [name] 
     FROM [Common].[sys].[database_principals]
     WHERE [name] = 'HQ_SECURITY\WebApp_SDSFinder')
BEGIN
    DROP USER [HQ_SECURITY\WebApp_SDSFinder];
    PRINT 'Existing user dropped in App DB'
END
GO

CREATE USER [HQ_SECURITY\WebApp_SDSFinder] FOR LOGIN [HQ_SECURITY\WebApp_SDSFinder]
GO
USE [Common]
GO
ALTER ROLE [db_datareader] ADD MEMBER [HQ_SECURITY\WebApp_SDSFinder]
 