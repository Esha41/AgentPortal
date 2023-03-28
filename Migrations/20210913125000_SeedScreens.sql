
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210913125000_SeedScreens')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;


    SET IDENTITY_INSERT [dbo].[Screens] ON

    INSERT INTO [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (1, 'Audit', 1, 0, 0)

    INSERT INTO [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (2, 'Reporting', 1, 0, 0)

    INSERT INTO [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (3, 'Roles', 1, 0, 0)

    INSERT INTO [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (4, 'Users', 1, 0, 0)

    INSERT INTO [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (5, 'Company', 1, 0, 0)

    INSERT INTO [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (6, 'Preferences', 1, 0, 0)

    SET IDENTITY_INSERT [dbo].[Screens] OFF

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210913125000_SeedScreens', N'5.0.9');

    COMMIT;
    PRINT 'Transactions completed successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END
