

USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210922094548_ChangeResourceLanguagesToUserPreference')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    ALTER TABLE [SystemUsers] DROP CONSTRAINT [FK_SystemUsers_ResourceLanguages_ResourceLanguageId];

    ALTER TABLE [ResourceLanguages] ADD [GridPageSize] int NOT NULL DEFAULT 0;

    ALTER TABLE [ResourceLanguages] ADD [SystemUserId] int NOT NULL DEFAULT 0;

    CREATE UNIQUE INDEX [IX_ResourceLanguages_SystemUserId] ON [ResourceLanguages] ([SystemUserId]);

    ALTER TABLE [ResourceLanguages] ADD CONSTRAINT [FK_ResourceLanguages_SystemUsers_SystemUserId] FOREIGN KEY ([SystemUserId]) REFERENCES [SystemUsers] ([Id]) ON DELETE CASCADE;

    ALTER TABLE [ResourceLanguages] DROP CONSTRAINT [FK_ResourceLanguages_SystemUsers_SystemUserId];

    ALTER TABLE [ResourceLanguages] DROP CONSTRAINT [PK_ResourceLanguages];

    EXEC sp_rename N'[ResourceLanguages]', N'UserPreferences';

    EXEC sp_rename N'[UserPreferences].[IX_ResourceLanguages_SystemUserId]', N'IX_UserPreferences_SystemUserId', N'INDEX';

    ALTER TABLE [UserPreferences] ADD CONSTRAINT [PK_UserPreferences] PRIMARY KEY ([Id]);

    ALTER TABLE [UserPreferences] ADD CONSTRAINT [FK_UserPreferences_SystemUsers_SystemUserId] FOREIGN KEY ([SystemUserId]) REFERENCES [SystemUsers] ([Id]) ON DELETE CASCADE;

    ALTER TABLE [UserPreferences] DROP CONSTRAINT [FK_UserPreferences_SystemUsers_SystemUserId];

    DROP INDEX [IX_UserPreferences_SystemUserId] ON [UserPreferences];

    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserPreferences]') AND [c].[name] = N'SystemUserId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [UserPreferences] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [UserPreferences] DROP COLUMN [SystemUserId];

    EXEC sp_rename N'[SystemUsers].[ResourceLanguageId]', N'UserPreferenceId', N'COLUMN';

    EXEC sp_rename N'[SystemUsers].[IX_SystemUsers_ResourceLanguageId]', N'IX_SystemUsers_UserPreferenceId', N'INDEX';

    ALTER TABLE [SystemUsers] ADD CONSTRAINT [FK_SystemUsers_UserPreferences_UserPreferenceId] FOREIGN KEY ([UserPreferenceId]) REFERENCES [UserPreferences] ([Id]) ON DELETE NO ACTION;

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210922094548_ChangeResourceLanguagesToUserPreference', N'5.0.9');

    COMMIT;
    PRINT 'Transactions completed successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END