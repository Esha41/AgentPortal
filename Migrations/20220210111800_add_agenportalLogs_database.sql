USE [master]
GO

CREATE DATABASE [AgentPortalLogs]
Go


USE [AgentPortalLogs]
GO


    BEGIN TRY

    BEGIN TRANSACTION;

		CREATE TABLE [dbo].[NLogs](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Level] [varchar](255) NOT NULL,
		[CallSite] [varchar](255) NOT NULL,
		[Type] [varchar](255) NOT NULL,
		[Message] [varchar](max) NOT NULL,
		[StackTrace] [varchar](max) NOT NULL,
		[InnerException] [varchar](max) NOT NULL,
		[AdditionalInfo] [varchar](max) NOT NULL,
		[LoggedOnDate] [datetime] NOT NULL,
	CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED
	(
	[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[NLogs] ADD CONSTRAINT [DF_Logs_TimeStamp] DEFAULT (getdate()) FOR [LoggedOnDate]

	Exec('CREATE PROCEDURE  [dbo].[InsertLog]
		@level varchar(20),
		@callsite varchar(MAX),
		@type varchar(MAX),
		@message varchar(MAX),
		@stackTrace varchar(MAX),
		@innerException varchar(MAX),
		@aditionalInfo varchar(MAX)
	AS
	BEGIN
		INSERT INTO [dbo].[NLogs]
			(
				[Level],
				CallSite,
				[Type],
				[Message],
				StackTrace,
				InnerException,
				AdditionalInfo
			)
			VALUES
			(
				@level,
				@callsite,
				@type,
				@message,
				@stackTrace,
				@innerException,
				@aditionalInfo
			)

	END')


	
	COMMIT;

    PRINT 'Transactions Completed Successfully!'

    END TRY

    BEGIN CATCH

    ROLLBACK TRANSACTION

    PRINT 'Error: ' + ERROR_MESSAGE()

    PRINT 'Transactions Rolled Back'

    END CATCH




