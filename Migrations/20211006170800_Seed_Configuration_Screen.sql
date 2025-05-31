
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20211006170800_Seed_Configuration_Screen')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

	insert into screens values ('Configurations', 1, 0, 0)
	
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
    VALUES (N'20211006170800_Seed_Configuration_Screen', N'5.0.9');
	
	COMMIT;

    PRINT 'Transactions Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


