
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210929131900_Seed_Screen_Data')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    -- 5 = Company
    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('CompanyNameInput', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('CallBackUrlInput', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Slaimportance', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('CompanyEmailInput', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('IsSignedCheckBox', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('SendRejectetionCheckBox', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('SendLinkCheckBox', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('SupportCallCheckBox', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('VideoCallBackInput', 5, 1, 0, 0)

	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('IsActiveCheckBox', 5, 1, 0, 0)

	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('CompanySubmit', 5, 1, 0, 0)

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210929131900_Seed_Screen_Data', N'5.0.9');
	
	COMMIT;

    PRINT 'Data Seeded Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


