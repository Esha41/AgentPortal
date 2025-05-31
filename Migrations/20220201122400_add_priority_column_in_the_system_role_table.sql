USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20220201122400_add_priority_column_in_the_system_role_table')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

	ALTER TABLE [dbo].[SystemRoles]
	ADD Priority int not null default 0;

	
        /****** Object:  Index [UC_SystemRoles_Name]    Script Date: 01/02/2022 1:05:43 PM ******/
    ALTER TABLE [dbo].[SystemRoles] DROP CONSTRAINT [UC_SystemRoles_Name]


    SET ANSI_PADDING ON


    /****** Object:  Index [UC_SystemRoles_Name]    Script Date: 01/02/2022 1:05:43 PM ******/
    ALTER TABLE [dbo].[SystemRoles] ADD  CONSTRAINT [UC_SystemRoles_Name] UNIQUE NONCLUSTERED 
    (
	    [Name] ASC , [CompanyId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]




	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220201122400_add_priority_column_in_the_system_role_table', N'5.0.9');

	COMMIT;

    PRINT 'Column added successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END