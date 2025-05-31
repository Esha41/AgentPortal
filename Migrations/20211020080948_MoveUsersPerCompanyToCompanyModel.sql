
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20211020080948_MoveUsersPerCompanyToCompanyModel')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    -- Drop existing table
    DROP TABLE [UsersPerCompanies];
    
    -- Add column in companies table
    ALTER TABLE [Companies] ADD [UsersPerCompany] int NOT NULL DEFAULT 0;

    -- Remove screen seeding
    DELETE FROM SCREENS WHERE [ScreenName] = 'UserPerCompany'

    -- Seed element in company screen
    INSERT INTO [ScreenElements] values ('UserPerCompany', 5, 1, 0, 0)
    
	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211020080948_MoveUsersPerCompanyToCompanyModel', N'5.0.9');
	
	COMMIT;

    PRINT 'Transaction Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


