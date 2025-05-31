
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210927121900_Seed_Super_Admin')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    INSERT INTO [dbo].[RoleScreens]
           ([SystemRoleId]
           ,[ScreenId]
           ,[Privilege]
           ,[IsActive]
           ,[CreatedAt]
           ,[UpdatedAt])
    SELECT 1 -- SystemRoleId
            , [Id] -- ScreenId
            , 2 -- Privilege
            , 1 -- IsActive
            , 0 -- CreatedAt
            , 0 -- UpdatedAt
    FROM [dbo].[Screens] 
    where [Id] not in (
        select [ScreenId] from [dbo].[RoleScreens] where [SystemRoleId] = 1 
    )

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210927121900_Seed_Super_Admin', N'5.0.9');
	
	COMMIT;

    PRINT 'Data Seeded Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


