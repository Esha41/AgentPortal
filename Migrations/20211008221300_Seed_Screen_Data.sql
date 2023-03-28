
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20211008221300_Seed_Screen_Data')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    SET IDENTITY_INSERT [dbo].[Screens] ON

    INSERT INTO [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (11, 'UserPerCompany', 1, 0, 0)

    SET IDENTITY_INSERT [dbo].[Screens] OFF

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211008221300_Seed_Screen_Data', N'5.0.9');
	
	COMMIT;

    PRINT 'Data Seeded Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


