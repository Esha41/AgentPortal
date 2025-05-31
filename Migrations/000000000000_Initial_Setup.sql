USE [master]
GO
/****** Object:  Database [AgentPortal]    Script Date: 9/7/2021 12:54:27 PM ******/
CREATE DATABASE [AgentPortal]
-- CONTAINMENT = NONE
-- ON  PRIMARY 
--( NAME = N'AgentPortal', FILENAME = N'D:\DATABASES\AgentPortal.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
-- LOG ON 
--( NAME = N'AgentPortal_log', FILENAME = N'D:\DATABASES\AgentPortal_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
-- WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AgentPortal] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgentPortal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgentPortal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgentPortal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgentPortal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgentPortal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgentPortal] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgentPortal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AgentPortal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgentPortal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgentPortal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgentPortal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AgentPortal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgentPortal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgentPortal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgentPortal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgentPortal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AgentPortal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgentPortal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgentPortal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgentPortal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AgentPortal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgentPortal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AgentPortal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AgentPortal] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AgentPortal] SET  MULTI_USER 
GO
ALTER DATABASE [AgentPortal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AgentPortal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AgentPortal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AgentPortal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AgentPortal] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AgentPortal] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AgentPortal] SET QUERY_STORE = OFF
GO
USE [AgentPortal]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ColumnPreferences]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColumnPreferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[ScreenId] [int] NOT NULL,
	[ColumnName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_ColumnPreferences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CallBackURL] [nvarchar](250) NULL,
	[HawkAppID] [nvarchar](50) NULL,
	[HawkUser] [nvarchar](50) NULL,
	[HawkSecret] [nvarchar](50) NULL,
	[FtpHostName] [nvarchar](50) NULL,
	[FtpUserName] [nvarchar](50) NULL,
	[FtpPassword] [nvarchar](50) NULL,
	[FtpDirectory] [nvarchar](150) NULL,
	[RetriesWhenFailPublished] [int] NOT NULL,
	[GDPRDaysToBeKept] [int] NOT NULL,
	[Code] [nvarchar](10) NULL,
	[FtpPort] [int] NULL,
	[FtpUserSecureProtocol] [bit] NULL,
	[FtpActive] [bit] NULL,
	[FtpResponseHostName] [nvarchar](50) NULL,
	[FtpResponseUserName] [nvarchar](50) NULL,
	[FtpResponsePassword] [nvarchar](50) NULL,
	[FtpResponsePort] [int] NULL,
	[FtpResponseUserSecureProtocol] [bit] NULL,
	[FtpResponseActive] [bit] NULL,
	[FtpResponseDirectory] [nvarchar](150) NULL,
	[ResponseWithFtp] [bit] NULL,
	[SimilarityThreshold] [int] NULL,
	[Enabled] [bit] NULL,
	[SLAMinutes] [int] NULL,
	[SLABatchQuantity] [int] NULL,
	[SLAImportance] [int] NOT NULL,
	[Email] [nvarchar](200) NULL,
	[IsSignedCompany] [bit] NOT NULL,
	[SendRejectionReasonAsCode] [bit] NOT NULL,
	[Logo] [nvarchar](max) NULL,
	[SendLink] [bit] NOT NULL,
	[SupportsCalls] [bit] NOT NULL,
	[MaxCallTIme] [int] NULL,
	[VideoCallBackUrl] [nvarchar](250) NULL,
	[AgentController] [nvarchar](100) NULL,
	[CustomerRetries] [int] NULL,
	[SMSProvider] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[Code2D] [nvarchar](2) NOT NULL,
	[Code3D] [nvarchar](3) NOT NULL,
	[MobileCode] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentClasses]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentClasses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentClassName] [nvarchar](250) NOT NULL,
	[EnumValue] [nvarchar](100) NOT NULL,
	[RecognitionMappedName] [nvarchar](max) NULL,
	[FriendlyName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentClasses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentGroupNames]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentGroupNames](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentGroupName] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentGroupNames] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentsPerCompanies]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentsPerCompanies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentClassId] [int] NOT NULL,
	[DocumentGroupId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentsPerCompanies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceLanguages]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceLanguages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Language] [nvarchar](3) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_ResourceLanguages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleScreenColumns]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleScreenColumns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ScreenColumnId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_RoleScreenColumns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleScreenElements]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleScreenElements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ScreenElementId] [int] NOT NULL,
	[Privilege] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_RoleScreenElements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleScreens]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleScreens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemRoleId] [int] NOT NULL,
	[ScreenId] [int] NOT NULL,
	[Privilege] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_RoleScreens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScreenColumns]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScreenColumns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScreenId] [int] NOT NULL,
	[ColumnName] [nvarchar](50) NOT NULL,
	[DefaultOrder] [int] NOT NULL,
	[DefaultVisibility] [bit] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_ScreenColumns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScreenElements]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScreenElements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScreenElementName] [nvarchar](50) NOT NULL,
	[ScreenId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_ScreenElements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Screens]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Screens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScreenName] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Screens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemRoles]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemRoles](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_SystemRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUserCompanies]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUserCompanies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_SystemUserCompanies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUserCountries]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUserCountries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_SystemUserCountries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUserRoles]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[SystemRoleId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_SystemUserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUsers]    Script Date: 9/7/2021 12:54:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](256) NULL,
	[Jmbg] [nvarchar](50) NULL,
	[ResourceLanguageId] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_SystemUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7a442ad8-e27a-4f65-b021-71883b1e7da7', 1, N'portaladmin@mailinator.com', N'PORTALADMIN@MAILINATOR.COM', N'portaladmin@mailinator.com', N'PORTALADMIN@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAECRzOgK2CfKiM2Eop+pNB+i6r1KrHwQl77xWM1MXJIVzle6ePijEGY1lY3VGiqut4w==', N'U7IA3KPURJV4FNS2AGY35XQ7TPCNENA3', N'a4d21fa7-ae23-4f7b-97a5-71e55a235751', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Super Admin', 1, 1604403101, 1604403101)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Role Supervisor', 1, 1604649235, 1604649235)
GO
SET IDENTITY_INSERT [dbo].[SystemUserRoles] ON 
GO
INSERT [dbo].[SystemUserRoles] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (0, 1, 1, 1, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[SystemUserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemUsers] ON 
GO
INSERT [dbo].[SystemUsers] ([Id], [UserName], [FullName], [Email], [Jmbg], [ResourceLanguageId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'portaladmin@mailinator.com', N'Portal Admin', N'portaladmin@mailinator.com', N'', NULL, 1, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[SystemUsers] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUsers_SystemUserId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_SystemUserId] ON [dbo].[AspNetUsers]
(
	[SystemUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColumnPreferences_ScreenId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_ColumnPreferences_ScreenId] ON [dbo].[ColumnPreferences]
(
	[ScreenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColumnPreferences_SystemUserId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_ColumnPreferences_SystemUserId] ON [dbo].[ColumnPreferences]
(
	[SystemUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentsPerCompanies_CompanyId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentsPerCompanies_CompanyId] ON [dbo].[DocumentsPerCompanies]
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentsPerCompanies_DocumentClassId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentsPerCompanies_DocumentClassId] ON [dbo].[DocumentsPerCompanies]
(
	[DocumentClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentsPerCompanies_DocumentGroupId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentsPerCompanies_DocumentGroupId] ON [dbo].[DocumentsPerCompanies]
(
	[DocumentGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleScreenColumns_RoleId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleScreenColumns_RoleId] ON [dbo].[RoleScreenColumns]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleScreenColumns_ScreenColumnId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleScreenColumns_ScreenColumnId] ON [dbo].[RoleScreenColumns]
(
	[ScreenColumnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleScreenElements_RoleId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleScreenElements_RoleId] ON [dbo].[RoleScreenElements]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleScreenElements_ScreenElementId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleScreenElements_ScreenElementId] ON [dbo].[RoleScreenElements]
(
	[ScreenElementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleScreens_ScreenId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleScreens_ScreenId] ON [dbo].[RoleScreens]
(
	[ScreenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleScreens_SystemRoleId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleScreens_SystemRoleId] ON [dbo].[RoleScreens]
(
	[SystemRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ScreenColumns_ScreenId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_ScreenColumns_ScreenId] ON [dbo].[ScreenColumns]
(
	[ScreenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ScreenElements_ScreenId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_ScreenElements_ScreenId] ON [dbo].[ScreenElements]
(
	[ScreenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SystemUserCompanies_CompanyId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SystemUserCompanies_CompanyId] ON [dbo].[SystemUserCompanies]
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SystemUserCompanies_SystemUserId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SystemUserCompanies_SystemUserId] ON [dbo].[SystemUserCompanies]
(
	[SystemUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SystemUserCountries_CountryId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SystemUserCountries_CountryId] ON [dbo].[SystemUserCountries]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SystemUserCountries_SystemUserId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SystemUserCountries_SystemUserId] ON [dbo].[SystemUserCountries]
(
	[SystemUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SystemUserRoles_SystemRoleId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SystemUserRoles_SystemRoleId] ON [dbo].[SystemUserRoles]
(
	[SystemRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SystemUserRoles_SystemUserId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SystemUserRoles_SystemUserId] ON [dbo].[SystemUserRoles]
(
	[SystemUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SystemUsers_ResourceLanguageId]    Script Date: 9/7/2021 12:54:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SystemUsers_ResourceLanguageId] ON [dbo].[SystemUsers]
(
	[ResourceLanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Audits] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ((1)) FOR [SendRejectionReasonAsCode]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ColumnPreferences]  WITH CHECK ADD  CONSTRAINT [FK_ColumnPreferences_Screens_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screens] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ColumnPreferences] CHECK CONSTRAINT [FK_ColumnPreferences_Screens_ScreenId]
GO
ALTER TABLE [dbo].[ColumnPreferences]  WITH CHECK ADD  CONSTRAINT [FK_ColumnPreferences_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ColumnPreferences] CHECK CONSTRAINT [FK_ColumnPreferences_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[DocumentsPerCompanies]  WITH CHECK ADD  CONSTRAINT [FK_DocumentsPerCompanies_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentsPerCompanies] CHECK CONSTRAINT [FK_DocumentsPerCompanies_Companies_CompanyId]
GO
ALTER TABLE [dbo].[DocumentsPerCompanies]  WITH CHECK ADD  CONSTRAINT [FK_DocumentsPerCompanies_DocumentClasses_DocumentClassId] FOREIGN KEY([DocumentClassId])
REFERENCES [dbo].[DocumentClasses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentsPerCompanies] CHECK CONSTRAINT [FK_DocumentsPerCompanies_DocumentClasses_DocumentClassId]
GO
ALTER TABLE [dbo].[DocumentsPerCompanies]  WITH CHECK ADD  CONSTRAINT [FK_DocumentsPerCompanies_DocumentGroupNames_DocumentGroupId] FOREIGN KEY([DocumentGroupId])
REFERENCES [dbo].[DocumentGroupNames] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentsPerCompanies] CHECK CONSTRAINT [FK_DocumentsPerCompanies_DocumentGroupNames_DocumentGroupId]
GO
ALTER TABLE [dbo].[RoleScreenColumns]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenColumns_ScreenColumns_ScreenColumnId] FOREIGN KEY([ScreenColumnId])
REFERENCES [dbo].[ScreenColumns] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreenColumns] CHECK CONSTRAINT [FK_RoleScreenColumns_ScreenColumns_ScreenColumnId]
GO
ALTER TABLE [dbo].[RoleScreenColumns]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenColumns_SystemRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreenColumns] CHECK CONSTRAINT [FK_RoleScreenColumns_SystemRoles_RoleId]
GO
ALTER TABLE [dbo].[RoleScreenElements]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenElements_ScreenElements_ScreenElementId] FOREIGN KEY([ScreenElementId])
REFERENCES [dbo].[ScreenElements] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreenElements] CHECK CONSTRAINT [FK_RoleScreenElements_ScreenElements_ScreenElementId]
GO
ALTER TABLE [dbo].[RoleScreenElements]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenElements_SystemRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreenElements] CHECK CONSTRAINT [FK_RoleScreenElements_SystemRoles_RoleId]
GO
ALTER TABLE [dbo].[RoleScreens]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreens_Screens_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screens] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreens] CHECK CONSTRAINT [FK_RoleScreens_Screens_ScreenId]
GO
ALTER TABLE [dbo].[RoleScreens]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreens_SystemRoles_SystemRoleId] FOREIGN KEY([SystemRoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreens] CHECK CONSTRAINT [FK_RoleScreens_SystemRoles_SystemRoleId]
GO
ALTER TABLE [dbo].[ScreenColumns]  WITH CHECK ADD  CONSTRAINT [FK_ScreenColumns_Screens_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screens] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScreenColumns] CHECK CONSTRAINT [FK_ScreenColumns_Screens_ScreenId]
GO
ALTER TABLE [dbo].[ScreenElements]  WITH CHECK ADD  CONSTRAINT [FK_ScreenElements_Screens_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screens] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScreenElements] CHECK CONSTRAINT [FK_ScreenElements_Screens_ScreenId]
GO
ALTER TABLE [dbo].[SystemUserCompanies]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserCompanies_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserCompanies] CHECK CONSTRAINT [FK_SystemUserCompanies_Companies_CompanyId]
GO
ALTER TABLE [dbo].[SystemUserCompanies]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserCompanies_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserCompanies] CHECK CONSTRAINT [FK_SystemUserCompanies_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[SystemUserCountries]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserCountries_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserCountries] CHECK CONSTRAINT [FK_SystemUserCountries_Countries_CountryId]
GO
ALTER TABLE [dbo].[SystemUserCountries]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserCountries_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserCountries] CHECK CONSTRAINT [FK_SystemUserCountries_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[SystemUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserRoles_SystemRoles_SystemRoleId] FOREIGN KEY([SystemRoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserRoles] CHECK CONSTRAINT [FK_SystemUserRoles_SystemRoles_SystemRoleId]
GO
ALTER TABLE [dbo].[SystemUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserRoles_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserRoles] CHECK CONSTRAINT [FK_SystemUserRoles_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[SystemUsers]  WITH CHECK ADD  CONSTRAINT [FK_SystemUsers_ResourceLanguages_ResourceLanguageId] FOREIGN KEY([ResourceLanguageId])
REFERENCES [dbo].[ResourceLanguages] ([Id])
GO
ALTER TABLE [dbo].[SystemUsers] CHECK CONSTRAINT [FK_SystemUsers_ResourceLanguages_ResourceLanguageId]
GO
USE [master]
GO
ALTER DATABASE [AgentPortal] SET  READ_WRITE 
GO
