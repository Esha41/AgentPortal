
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20211006191500_Seed_Configuration_Elements')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

	insert into ScreenElements values ('PasswordRequireNonAlphanumericCheckBox', 10, 1, 0, 0)
    insert into ScreenElements values ('PasswordRequireLowercaseCheckBox', 10, 1, 0, 0)
    insert into ScreenElements values ('PasswordRequireUppercaseCheckBox', 10, 1, 0, 0)
    insert into ScreenElements values ('PasswordRequireDigitCheckBox', 10, 1, 0, 0)
    insert into ScreenElements values ('PasswordRequiredLengthCheckInput', 10, 1, 0, 0)
    insert into ScreenElements values ('RestrictLastUsedPasswordsInput', 10, 1, 0, 0)
    insert into ScreenElements values ('ForcePasswordChangeDaysInput', 10, 1, 0, 0)
       
    insert into ScreenElements values ('PasswordPolicySubmit', 10, 1, 0, 0)   
    insert into ScreenElements values ('PreferencesSubmit', 6, 1, 0, 0)   

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211006191500_Seed_Configuration_Elements', N'5.0.9');
	
	COMMIT;

    PRINT 'Transactions Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


