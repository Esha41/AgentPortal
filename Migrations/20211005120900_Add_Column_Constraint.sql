
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20211005120900_Add_Column_Constraint')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

	ALTER TABLE [SystemUsers]
	ADD CONSTRAINT UC_SystemUsers_Email UNIQUE ([Email]);

	update SystemRoles set [Name] = [Name] + cast([Id] as varchar) where [Name] in (
		select sr.[Name] from SystemRoles sr where sr.Id <> SystemRoles.Id
	  )

    ALTER TABLE [SystemRoles]
	ADD CONSTRAINT UC_SystemRoles_Name UNIQUE ([Name]);
	   	
	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211005120900_Add_Column_Constraint', N'5.0.9');
	
	COMMIT;

    PRINT 'Data Seeded Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


