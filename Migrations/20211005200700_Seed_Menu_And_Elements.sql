
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20211005200700_Seed_Menu_And_Elements')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

	insert into screens values ('UserCountries', 1, 0, 0)
	insert into screens values ('UserRoles', 1, 0, 0)
	insert into screens values ('DocumentsPerCompany', 1, 0, 0)

	insert into ScreenElements values ('HawkAppIdInput', 5, 1, 0, 0)
	insert into ScreenElements values ('HawkUserInput', 5, 1, 0, 0)
	insert into ScreenElements values ('HawkSecretInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpHostNameInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpUserNameInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpPasswordInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpDirectoryInput', 5, 1, 0, 0)
	insert into ScreenElements values ('RetriesWhenFailPublishedInput', 5, 1, 0, 0)
	insert into ScreenElements values ('GdprdaysToBeKeptInput', 5, 1, 0, 0)
	insert into ScreenElements values ('CodeInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpPortInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FTpUserSecureProtocolCheckBox', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpActiveCheckBox', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpResponseHostNameInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpResponseUserNameInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpResponsePasswordInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpResponsePortInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpResponseUserSecureProtocolInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpResponseActiveCheckBox', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpResponseDirectoryInput', 5, 1, 0, 0)
	insert into ScreenElements values ('FtpResponseCheckbox', 5, 1, 0, 0)
	insert into ScreenElements values ('SimilarityThresholdInput', 5, 1, 0, 0)
	insert into ScreenElements values ('EnableCheckBox', 5, 1, 0, 0)
	insert into ScreenElements values ('SlaminutesInput', 5, 1, 0, 0)
	insert into ScreenElements values ('SlabBatchQuantityInput', 5, 1, 0, 0)
	insert into ScreenElements values ('LogoInput', 5, 1, 0, 0)
	insert into ScreenElements values ('MaxCallInput', 5, 1, 0, 0)
	insert into ScreenElements values ('AgentControllerInput', 5, 1, 0, 0)
	insert into ScreenElements values ('CustomerRetriesInput', 5, 1, 0, 0)
	insert into ScreenElements values ('SmsProviderInput', 5, 1, 0, 0)
  	
	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211005200700_Seed_Menu_And_Elements', N'5.0.9');
	
	COMMIT;

    PRINT 'Data Seeded Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


