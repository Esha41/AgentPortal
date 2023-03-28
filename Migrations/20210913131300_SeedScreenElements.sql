

USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210913131300_SeedScreenElements')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;


    SET IDENTITY_INSERT [dbo].[ScreenElements] ON

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (1, 'Export', 1, 1, 0, 0) --Audit

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (2, 'Export', 2, 1, 0, 0) --Reporting

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (3, 'Export', 3, 1, 0, 0) --Roles

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (4, 'Export', 4, 1, 0, 0) --Users

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (5, 'Export', 5, 1, 0, 0) --Company

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (6, 'AddNew', 3, 1, 0, 0) --Roles

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (7, 'AddNew', 4, 1, 0, 0) --Users

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (8, 'AddNew', 5, 1, 0, 0) --Company

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (9, 'Edit', 3, 1, 0, 0) --Roles

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (10, 'Edit', 4, 1, 0, 0) --Users

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (11, 'Edit', 5, 1, 0, 0) --Company

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (12, 'Delete', 3, 1, 0, 0) --Roles

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (13, 'Delete', 4, 1, 0, 0) --Users

    INSERT INTO [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) 
    VALUES (14, 'Delete', 5, 1, 0, 0) --Company

    SET IDENTITY_INSERT [dbo].[ScreenElements] OFF

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210913131300_SeedScreenElements', N'5.0.9');

    COMMIT;
    PRINT 'Transactions completed successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END

