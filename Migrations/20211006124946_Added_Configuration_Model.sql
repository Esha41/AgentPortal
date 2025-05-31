
USE [AgentPortal]
GO

IF NOT EXISTS (SELECT * 
                 FROM [dbo].[__EFMigrationsHistory] 
                 WHERE [MigrationId] = '20211006124946_Added_Configuration_Model')
BEGIN

    BEGIN TRY

    BEGIN TRANSACTION;

	CREATE TABLE [Configurations] (
        [Id] int NOT NULL IDENTITY,
        [PasswordRequireNonAlphanumeric] bit NOT NULL,
        [PasswordRequireLowercase] bit NOT NULL,
        [PasswordRequireUppercase] bit NOT NULL,
        [PasswordRequireDigit] bit NOT NULL,
        [PasswordRequiredLength] int NOT NULL,
        [RestrictLastUsedPasswords] int NOT NULL,
        [ForcePasswordChangeDays] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] bigint NOT NULL,
        [UpdatedAt] bigint NOT NULL,
        CONSTRAINT [PK_Configurations] PRIMARY KEY ([Id])
    );
    
    SET IDENTITY_INSERT [dbo].[Configurations] ON

    INSERT INTO [dbo].[Configurations]
           ([Id]
		   ,[PasswordRequireNonAlphanumeric]
           ,[PasswordRequireLowercase]
           ,[PasswordRequireUppercase]
           ,[PasswordRequireDigit]
           ,[PasswordRequiredLength]
           ,[RestrictLastUsedPasswords]
           ,[ForcePasswordChangeDays]
           ,[IsActive]
           ,[CreatedAt]
           ,[UpdatedAt])
     VALUES
           (1
		   ,1
           ,1
           ,1
           ,1
           ,8
           ,3
           ,99
           ,1
           ,0
           ,0)

    SET IDENTITY_INSERT [dbo].[Configurations] OFF

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211006124946_Added_Configuration_Model', N'5.0.9');
	
	COMMIT;

    PRINT 'Transactions Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH

END


