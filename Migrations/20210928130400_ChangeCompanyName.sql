
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210928130400_ChangeCompanyName')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    EXEC sp_rename 'Companies.Name', 'CompanyName', 'COLUMN';

    EXEC sp_rename 'Countries.Name', 'CountryName', 'COLUMN';

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210928130400_ChangeCompanyName', N'5.0.9');
	
	COMMIT;

    PRINT 'Transaction Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


