
IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '202109101300_IdentityInRolesTable')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION

    ALTER TABLE [dbo].[RoleScreenColumns]  DROP  CONSTRAINT [FK_RoleScreenColumns_SystemRoles_RoleId]

    ALTER TABLE [dbo].[RoleScreenElements]  DROP  CONSTRAINT [FK_RoleScreenElements_SystemRoles_RoleId]

    ALTER TABLE [dbo].[RoleScreens]  DROP  CONSTRAINT [FK_RoleScreens_SystemRoles_SystemRoleId]

    ALTER TABLE [dbo].[SystemUserRoles]  DROP  CONSTRAINT [FK_SystemUserRoles_SystemRoles_SystemRoleId]

    ALTER TABLE [dbo].[SystemRoles] DROP CONSTRAINT [PK_SystemRoles]

    ALTER TABLE SystemRoles DROP COLUMN Id

    ALTER TABLE SystemRoles ADD Id INT NOT NULL IDENTITY (1, 1)

    ALTER TABLE [dbo].[SystemRoles] WITH CHECK ADD CONSTRAINT [PK_SystemRoles] PRIMARY KEY ([Id])

    ALTER TABLE [dbo].[RoleScreenColumns]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenColumns_SystemRoles_RoleId] FOREIGN KEY([RoleId])
    REFERENCES [dbo].[SystemRoles] ([Id])
    ON DELETE CASCADE

    ALTER TABLE [dbo].[RoleScreenElements]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenElements_SystemRoles_RoleId] FOREIGN KEY([RoleId])
    REFERENCES [dbo].[SystemRoles] ([Id])
    ON DELETE CASCADE

    ALTER TABLE [dbo].[RoleScreens]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreens_SystemRoles_SystemRoleId] FOREIGN KEY([SystemRoleId])
    REFERENCES [dbo].[SystemRoles] ([Id])
    ON DELETE CASCADE

    ALTER TABLE [dbo].[SystemUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserRoles_SystemRoles_SystemRoleId] FOREIGN KEY([SystemRoleId])
    REFERENCES [dbo].[SystemRoles] ([Id])
    ON DELETE CASCADE

    INSERT INTO [dbo].[__EFMigrationsHistory]
               ([MigrationId], [ProductVersion])
         VALUES
               ('202109101300_IdentityInRolesTable', N'5.0.2')

    COMMIT TRANSACTION

    PRINT 'Transactions completed successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END
