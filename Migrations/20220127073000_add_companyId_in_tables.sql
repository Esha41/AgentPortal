USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20220127073000_add_companyId_in_tables')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

	ALTER TABLE [dbo].[SystemRoles]
	ADD CompanyId int NULL;

	ALTER TABLE [dbo].[SystemRoles]
	ADD CONSTRAINT FK_SystemRoles_company
	FOREIGN KEY (CompanyId) REFERENCES Companies(Id);

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220127073000_add_companyId_in_tables', N'5.0.9');

	COMMIT;

    PRINT 'Column added successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END