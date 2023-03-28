
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20220128124300_Seed_CompanySelect_element_in_Elements')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

	insert into ScreenElements values ('CompanySelect', 3, 1, 0, 0)

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220128124300_Seed_CompanySelect_element_in_Elements', N'5.0.9');
	
	COMMIT;

    PRINT 'Transactions Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


