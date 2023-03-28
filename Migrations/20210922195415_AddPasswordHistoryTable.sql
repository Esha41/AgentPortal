

USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210922195415_AddPasswordHistoryTable')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    CREATE TABLE [PasswordHistory] (
        [Id] int NOT NULL IDENTITY,
        [SystemUserId] int NOT NULL,
        [ChangedAt] bigint NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        CONSTRAINT [PK_PasswordHistory] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PasswordHistory_SystemUsers_SystemUserId] FOREIGN KEY ([SystemUserId]) REFERENCES [SystemUsers] ([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_PasswordHistory_SystemUserId] ON [PasswordHistory] ([SystemUserId]);

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210922195415_AddPasswordHistoryTable', N'5.0.9');

    COMMIT;

    PRINT 'Transactions completed successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END

