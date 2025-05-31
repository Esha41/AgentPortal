
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20211007175230_AddUserSessionModel')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    ALTER TABLE [SystemUsers] DROP CONSTRAINT [df_Logged_in];

    ALTER TABLE [SystemUsers] DROP COLUMN [IsLoggedIn];

    CREATE TABLE [UserSessions] (
        [SystemUserId] int NOT NULL,
        [SessionId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UserSessions] PRIMARY KEY ([SystemUserId]),
        CONSTRAINT [FK_UserSessions_SystemUsers_SystemUserId] FOREIGN KEY ([SystemUserId]) REFERENCES [SystemUsers] ([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_UserSessions_SystemUserId] ON [UserSessions] ([SystemUserId]); 

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211007175230_AddUserSessionModel', N'5.0.9');
	
	COMMIT;

    PRINT 'Transactions Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


