
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210928013405_Add_User_Per_Company')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    CREATE TABLE [UsersPerCompanies] (
        [Id] int NOT NULL IDENTITY,
        [AllowedUsers] int NOT NULL,
        [IsActive] bit NULL,
        [CreatedAt] bigint NOT NULL,
        [UpdatedAt] bigint NOT NULL,
        [CompanyId] int NOT NULL
        CONSTRAINT [PK_UsersPerCompanies] PRIMARY KEY ([Id])
    );

    CREATE INDEX [IX_UsersPerCompanies_CompanyId] ON [UsersPerCompanies] ([CompanyId]);

    ALTER TABLE [UsersPerCompanies] ADD CONSTRAINT [FK_UsersPerCompanies_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE CASCADE;

    ALTER TABLE [SystemUsers]
    ADD IsLoggedIn bit;

    ALTER TABLE [SystemUsers]
    ADD CONSTRAINT df_Logged_in
    DEFAULT 0 FOR IsLoggedIn;

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210928013405_Add_User_Per_Company', N'5.0.9');
	
	COMMIT;

    PRINT 'Transaction Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


