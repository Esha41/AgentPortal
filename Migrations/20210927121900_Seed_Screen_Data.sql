
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20210927121901_Seed_Screen_Data')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

    Delete from RoleScreenElements

    Delete from ScreenElements

    -- 1 = Audit
    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Filter', 1, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Export', 1, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ExportGridData', 1, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ExportAllData', 1, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Sort', 1, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Pagination', 1, 1, 0, 0)

    -- 2 = Reporting
    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Filter', 2, 1, 0, 0)
    
    -- 3 = Roles
    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('AddNew', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Edit', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Delete', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Filter', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Export', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ExportGridData', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ExportAllData', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Sort', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Pagination', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('RoleNameInput', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ScreenPrivilegesSelect', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('RoleNameSubmit', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('RolePrivilegesSubmit', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ElementSubmitButton', 3, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('RolesElementSelect', 3, 1, 0, 0)

    -- 4 = Users
    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('AddNew', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Edit', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Delete', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Filter', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Export', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ExportGridData', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ExportAllData', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Sort', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Pagination', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('UserNameInput', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('UserCompanySelect', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('UserEmail', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('UserSubmitButton', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('UserRolesSelect', 4, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('UserCountriesSelect', 4, 1, 0, 0)

    -- 5 = Company
    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('AddNew', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Edit', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Delete', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Filter', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Export', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ExportGridData', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('ExportAllData', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Sort', 5, 1, 0, 0)

    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('Pagination', 5, 1, 0, 0)

    -- 6 = Preferences
    INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt])
    VALUES ('GridPageSize', 6, 1, 0, 0)

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210927121901_Seed_Screen_Data', N'5.0.9');
	
	COMMIT;

    PRINT 'Data Seeded Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


