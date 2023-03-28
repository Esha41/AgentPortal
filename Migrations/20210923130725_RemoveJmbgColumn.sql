

USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210923130725_RemoveJmbgColumn')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;
    
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SystemUsers]') AND [c].[name] = N'Jmbg');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SystemUsers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [SystemUsers] DROP COLUMN [Jmbg];

    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SystemUsers]') AND [c].[name] = N'UserName');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [SystemUsers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [SystemUsers] DROP COLUMN [UserName];

    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserPreferences]') AND [c].[name] = N'Language');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [UserPreferences] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [UserPreferences] ALTER COLUMN [Language] nvarchar(5) NOT NULL;

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210923130725_RemoveJmbgColumn', N'5.0.9');

    COMMIT;
    PRINT 'Transactions completed successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END