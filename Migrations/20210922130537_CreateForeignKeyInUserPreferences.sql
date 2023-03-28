
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210922130537_CreateForeignKeyInUserPreferences')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    DELETE FROM UserPreferences

    ALTER TABLE [SystemUsers] DROP CONSTRAINT [FK_SystemUsers_UserPreferences_UserPreferenceId];

    DROP INDEX [IX_SystemUsers_UserPreferenceId] ON [SystemUsers];

    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SystemUsers]') AND [c].[name] = N'UserPreferenceId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SystemUsers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [SystemUsers] DROP COLUMN [UserPreferenceId];

    ALTER TABLE [UserPreferences] ADD [SystemUserId] int NOT NULL DEFAULT 0;

    CREATE INDEX [IX_UserPreferences_SystemUserId] ON [UserPreferences] ([SystemUserId]);

    ALTER TABLE [UserPreferences] ADD CONSTRAINT [FK_UserPreferences_SystemUsers_SystemUserId] FOREIGN KEY ([SystemUserId]) REFERENCES [SystemUsers] ([Id]) ON DELETE CASCADE;

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210922130537_CreateForeignKeyInUserPreferences', N'5.0.9');

    COMMIT;

    PRINT 'Transactions completed successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END
