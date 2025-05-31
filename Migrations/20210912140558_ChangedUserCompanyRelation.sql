
IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210912140558_ChangedUserCompanyRelation')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    DROP TABLE [SystemUserCompanies];

    ALTER TABLE [SystemUsers] ADD [CompanyId] int NULL;

    CREATE INDEX [IX_SystemUsers_CompanyId] ON [SystemUsers] ([CompanyId]);

    ALTER TABLE [SystemUsers] ADD CONSTRAINT [FK_SystemUsers_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE NO ACTION;

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210912140558_ChangedUserCompanyRelation', N'5.0.9');

    COMMIT;
    PRINT 'Transactions completed successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END