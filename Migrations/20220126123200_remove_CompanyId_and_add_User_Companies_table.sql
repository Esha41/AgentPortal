
USE [AgentPortal]
GO


IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20220126123200_remove_CompanyId_and_add_User_Companies_table')
BEGIN
    BEGIN TRY

    BEGIN TRANSACTION;


	/****** Object:  Table [dbo].[UserCompanies]    Script Date: 26/01/2022 11:55:17 AM ******/
	CREATE TABLE [dbo].[UserCompanies](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[SystemUserId] [int] NOT NULL,
		[CompanyId] [int] NOT NULL,
		[IsActive] [bit] NULL,
		[CreatedAt] [bigint] NOT NULL,
		[UpdatedAt] [bigint] NOT NULL
	 CONSTRAINT [PK_UserCompanies] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[UserCompanies]  WITH CHECK ADD  CONSTRAINT [FK_Companyies_Companyid] FOREIGN KEY([CompanyId])
	REFERENCES [dbo].[Companies] ([Id])

	ALTER TABLE [dbo].[UserCompanies] CHECK CONSTRAINT [FK_Companyies_Companyid]
	
	ALTER TABLE [dbo].[UserCompanies]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserId] FOREIGN KEY([SystemUserId])
	REFERENCES [dbo].[SystemUsers] ([Id])
	
	ALTER TABLE [dbo].[UserCompanies] CHECK CONSTRAINT [FK_SystemUserId]
	
	
	
	/** check id there is already data in the system users table if yes then transfer it to the userCompanies table before deleting company id column **/
	DECLARE @IdCount int;	
	SELECT @IdCount = count( id ) FROM [dbo].[SystemUsers] where [CompanyId] is not null

	IF @IdCount >  0
	BEGIN
		DECLARE @User CURSOR
		DECLARE @CompanyId int
		DEClARE @UserId int
		SET @User = CURSOR FOR
		SELECT Id , CompanyId FROM [dbo].[SystemUsers] where [CompanyId] is not null
		OPEN @User
		FETCH NEXT
		FROM @User INTO @UserId, @CompanyId
		WHILE @@FETCH_STATUS = 0
		BEGIN

		/* insert already data in user companies table*/
		INSERT INTO [dbo].[UserCompanies]
				   ([SystemUserId]
				   ,[CompanyId],[CreatedAt],[UpdatedAt]
					)
			 VALUES
				   (@UserId , @CompanyId , 0 , 0)


			FETCH NEXT
			FROM @User INTO @UserId, @CompanyId
		END

		CLOSE @User
		DEALLOCATE @User
	END

	/* remove company id column from the System user table */
	ALTER TABLE  [dbo].[SystemUsers]  drop constraint [FK_SystemUsers_Companies_CompanyId];
	DROP INDEX [IX_SystemUsers_CompanyId] ON [dbo].[SystemUsers];
	ALTER TABLE [dbo].[SystemUsers] 
	DROP COLUMN CompanyId;


	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220126123200_remove_CompanyId_and_add_User_Companies_table', N'5.0.9');

	COMMIT;

    PRINT 'Transactions Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH



END
