USE [master]
GO
/****** Object:  Database [WebMediaPortfolio-DB]    Script Date: 5/11/2019 9:10:25 PM ******/
CREATE DATABASE [WebMediaPortfolio-DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebMediaPortfolio-DB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\WebMediaPortfolio-DB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WebMediaPortfolio-DB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\WebMediaPortfolio-DB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebMediaPortfolio-DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET  MULTI_USER 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [WebMediaPortfolio-DB]
GO
/****** Object:  StoredProcedure [dbo].[SP_COM_CODING_INSERT]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_COM_CODING_INSERT] 
    @CodingType_ID int,
    @CODING_DESCRIPTION nvarchar(50) = NULL,
	@URL nvarchar(MAX) = NULL,
    @VALID_FROM date = NULL,
    @VALID_TO date = NULL,
    @CREATE_BY varchar(50) = NULL,
    @CREATE_DATE date = NULL,
    @UPDATE_BY varchar(50) = NULL,
    @UPDATE_DATE date = NULL,
	@ACCESS_LEVEL VARCHAR(50)= NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[COM_CODING] ([CodingType_ID], [CODING_DESCRIPTION],[URL], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL])
	SELECT @CodingType_ID, @CODING_DESCRIPTION,@URL, @VALID_FROM, @VALID_TO, @CREATE_BY, @CREATE_DATE, @UPDATE_BY, @UPDATE_DATE, @ACCESS_LEVEL
	
	-- Begin Return Select <- do not remove
	SELECT [CODING_ID], [CodingType_ID], [CODING_DESCRIPTION],[URL] [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL]
	FROM   [dbo].[COM_CODING]
	WHERE  [CODING_ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT





GO
/****** Object:  StoredProcedure [dbo].[SP_COM_CODING_UPDATE]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_COM_CODING_UPDATE] 
    @CODING_ID int,
    @CODING_DESCRIPTION nvarchar(50) = NULL,
    @VALID_FROM date = NULL,
    @VALID_TO date = NULL,
    @UPDATE_BY varchar(50) = NULL,
    @UPDATE_DATE date = NULL,
	@ACCESS_LEVEL VARCHAR(50)=NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[COM_CODING]
	SET   [CODING_DESCRIPTION] = @CODING_DESCRIPTION, [VALID_FROM] = @VALID_FROM, [VALID_TO] = @VALID_TO,  [UPDATE_BY] = @UPDATE_BY, [UPDATE_DATE] = @UPDATE_DATE, [ACCESS_LEVEL]=@ACCESS_LEVEL
	WHERE  [CODING_ID] = @CODING_ID
	
	-- Begin Return Select <- do not remove
	SELECT [CODING_ID], [CodingType_ID], [CODING_DESCRIPTION], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL]
	FROM   [dbo].[COM_CODING]
	WHERE  [CODING_ID] = @CODING_ID	
	-- End Return Select <- do not remove

	COMMIT





GO
/****** Object:  StoredProcedure [dbo].[SP_COM_CODINGTYPE_INSERT]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_COM_CODINGTYPE_INSERT]
@DESCRIPTION nVARCHAR(250) = NULL,@URL Nvarchar(MAX), @VALID_FROM DATE=NULL, @VALID_TO DATE = NULL, @CREATE_BY INT = NULL,
@CREATE_DATE DATETIME = NULL, @UPDATE_BY INT = NULL, @UPDATE_DATE DATETIME = NULL, @ACCESS_LEVEL VARCHAR(50)=NULL

AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [DBO].[COM_CodingType] ([DESCRIPTION],[URL], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE],
[UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL])
SELECT @DESCRIPTION ,@URL, @VALID_FROM , @VALID_TO , @CREATE_BY ,
@CREATE_DATE , @UPDATE_BY , @UPDATE_DATE ,@ACCESS_LEVEL 

SELECT [CODINGTYPE_ID], [DESCRIPTION],[URL],[VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE],
[UPDATE_BY], [UPDATE_DATE] , [ACCESS_LEVEL]
FROM [DBO].[COM_CODINGTYPE]
WHERE [CODINGTYPE_ID] = SCOPE_IDENTITY()

COMMIT






GO
/****** Object:  StoredProcedure [dbo].[SP_COM_CODINGTYPE_UPDATE]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_COM_CODINGTYPE_UPDATE]
@codingtype_id int, @DESCRIPTION nVARCHAR(250) = NULL, @VALID_FROM DATE=NULL, @VALID_TO DATE = NULL, @UPDATE_BY INT = NULL, @UPDATE_DATE DATETIME = NULL,
@ACCESS_LEVEL VARCHAR(50)=NULL

AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN
UPDATE [DBO].[COM_CodingType] 
SET [DESCRIPTION] = @DESCRIPTION, [VALID_FROM] = @VALID_FROM, [VALID_TO] = @VALID_TO, 
[UPDATE_BY] = @UPDATE_BY, [UPDATE_DATE] = @UPDATE_DATE , [ACCESS_LEVEL]=@ACCESS_LEVEL
WHERE [CodingType_ID] = @CodingType_ID 

SELECT [CODINGTYPE_ID], [DESCRIPTION], [VALID_FROM], [VALID_TO], 
[UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL]
FROM [DBO].[COM_CODINGTYPE]
WHERE [CODINGTYPE_ID] = @CodingType_ID 

COMMIT






GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_USER]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--Insert_User 'Mudassar2', '12345', 'mudassar@aspsnippets.com'
CREATE PROCEDURE [dbo].[SP_INSERT_USER]
	@Username NVARCHAR(20),
	@Password NVARCHAR(200),
	@Email NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT USER_Id FROM [COM_USER] WHERE Username = @Username)
	BEGIN
		SELECT -1 -- Username exists.
	END
	ELSE IF EXISTS(SELECT USER_ID FROM [COM_USER] WHERE Email = @Email)
	BEGIN
		SELECT -2 -- Email exists.
	END
	ELSE
	BEGIN
		INSERT INTO [COM_USER]
			   ([Username]
			   ,[Password]
			   ,[Email]
			   ,[Create_Date])
		VALUES
			   (@Username
			   ,@Password
			   ,@Email
			   ,GETDATE())
		
		SELECT SCOPE_IDENTITY() -- UserId			   
     END
END








GO
/****** Object:  StoredProcedure [dbo].[SP_VALIDATE_USER]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SP_VALIDATE_USER]
	@Username NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @UserId INT, @LastLoginDate DATETIME
	
	SELECT @UserId = [USER_ID], @LastLoginDate =  LAST_LOGIN_DATE
	FROM [COM_USER] WHERE Username = @Username
	
	IF @UserId IS NOT NULL
	BEGIN
		IF NOT EXISTS(SELECT [USER_ID] FROM COM_USER_ACTIVATION WHERE [USER_ID] = @UserId)
		BEGIN
			UPDATE [COM_USER]
			SET LAST_LOGIN_DATE =  GETDATE()
			WHERE [USER_ID] = @UserId
			SELECT @UserId [UserId] -- User Valid
		END
		ELSE
		BEGIN
			SELECT -2 -- User not activated.
		END
	END
	ELSE
	BEGIN
		SELECT -1 -- User invalid.
	END
END






GO
/****** Object:  UserDefinedFunction [dbo].[TRIM]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create Function
CREATE FUNCTION [dbo].[TRIM](@string VARCHAR(MAX))
RETURNS VARCHAR(MAX)
BEGIN
RETURN LTRIM(RTRIM(@string))
END





GO
/****** Object:  Table [dbo].[COM_CODING]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[COM_CODING](
	[CODING_ID] [int] IDENTITY(1,1) NOT NULL,
	[CodingType_ID] [int] NOT NULL,
	[CODING_DESCRIPTION] [nvarchar](50) NULL,
	[VALID_FROM] [date] NULL,
	[VALID_TO] [date] NULL,
	[CREATE_BY] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_BY] [int] NULL,
	[UPDATE_DATE] [datetime] NULL,
	[ACCESS_LEVEL] [varchar](50) NULL,
	[URL] [nvarchar](max) NULL,
 CONSTRAINT [PK_COM_CODING] PRIMARY KEY CLUSTERED 
(
	[CODING_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[COM_CODINGTYPE]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[COM_CODINGTYPE](
	[CodingType_ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[VALID_FROM] [date] NULL,
	[VALID_TO] [date] NULL,
	[CREATE_BY] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_BY] [int] NULL,
	[UPDATE_DATE] [datetime] NULL,
	[ACCESS_LEVEL] [varchar](50) NULL,
	[URL] [nvarchar](max) NULL,
 CONSTRAINT [PK_COM_CodingType] PRIMARY KEY CLUSTERED 
(
	[CodingType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[COM_CONTENT]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COM_CONTENT](
	[CONTENT_ID] [int] IDENTITY(1,1) NOT NULL,
	[CODING_ID] [int] NULL,
	[DESCRIPTION] [nvarchar](200) NULL,
	[URL] [nvarchar](max) NULL,
	[VIDEO_URL] [nvarchar](max) NULL,
	[VALID_FROM] [date] NULL,
	[VALID_TO] [date] NULL,
	[CREATE_BY] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_BY] [int] NULL,
	[UPDATE_DATE] [datetime] NULL,
 CONSTRAINT [PK_COM_CONTENT] PRIMARY KEY CLUSTERED 
(
	[CONTENT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[COM_USER]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[COM_USER](
	[USER_ID] [int] IDENTITY(1,1) NOT NULL,
	[USERNAME] [nvarchar](30) NULL,
	[PASSWORD] [nvarchar](200) NULL,
	[EMAIL] [nvarchar](100) NULL,
	[VALID_FROM] [date] NULL,
	[VALID_TO] [date] NULL,
	[CREATE_BY] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_BY] [int] NULL,
	[UPDATE_DATE] [datetime] NULL,
	[LAST_LOGIN_DATE] [datetime] NULL,
	[ACCESS_LEVEL] [varchar](50) NULL,
 CONSTRAINT [PK_COM_USER] PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[COM_User_Activation]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[COM_User_Activation](
	[USER_ID] [int] IDENTITY(1,1) NOT NULL,
	[ACTIVATION_CODE] [uniqueidentifier] NULL,
	[VALID_FROM] [date] NULL,
	[VALID_TO] [date] NULL,
	[CREATE_BY] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_BY] [int] NULL,
	[UPDATE_DATE] [datetime] NULL,
	[ACCESS_LEVEL] [varchar](50) NULL,
 CONSTRAINT [PK_COM_USER_ACTIVATION] PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[VW_COM_CODING]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_COM_CODING]
AS
SELECT   dbo.COM_CODING.CODING_ID AS Expr2, dbo.COM_CODING.CodingType_ID AS Expr3, dbo.COM_CODING.CODING_DESCRIPTION AS Expr1, dbo.COM_CODING.VALID_FROM AS Expr4, dbo.COM_CODING.VALID_TO AS Expr5, dbo.COM_CODING.CREATE_BY AS Expr6, dbo.COM_CODING.CREATE_DATE AS Expr7, 
             dbo.COM_CODING.UPDATE_BY AS Expr8, dbo.COM_CODING.UPDATE_DATE AS Expr9, dbo.COM_USER.USERNAME AS CreateByUser, COM_USER_1.USERNAME AS UpdateByUser, dbo.COM_CODING.*
FROM     dbo.COM_CODING INNER JOIN
             dbo.COM_USER ON dbo.COM_CODING.CREATE_BY = dbo.COM_USER.USER_ID INNER JOIN
             dbo.COM_USER AS COM_USER_1 ON dbo.COM_CODING.UPDATE_BY = COM_USER_1.USER_ID





GO
/****** Object:  View [dbo].[VW_COM_CODINGTYPE]    Script Date: 5/11/2019 9:10:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_COM_CODINGTYPE]
AS
SELECT   dbo.COM_CodingType.CodingType_ID, dbo.COM_CodingType.Description, dbo.COM_CodingType.VALID_FROM, dbo.COM_CodingType.VALID_TO, dbo.COM_CodingType.CREATE_BY, dbo.COM_CodingType.CREATE_DATE, dbo.COM_CodingType.UPDATE_BY, dbo.COM_CodingType.UPDATE_DATE, 
             COM_USER_1.USERNAME AS CreateByUser, dbo.COM_USER.USERNAME AS UpdateByUser
FROM     dbo.COM_CodingType LEFT OUTER JOIN
             dbo.COM_USER AS COM_USER_1 ON dbo.COM_CodingType.CREATE_BY = COM_USER_1.USER_ID LEFT OUTER JOIN
             dbo.COM_USER ON dbo.COM_CodingType.UPDATE_BY = dbo.COM_USER.USER_ID





GO
SET IDENTITY_INSERT [dbo].[COM_CODING] ON 

INSERT [dbo].[COM_CODING] ([CODING_ID], [CodingType_ID], [CODING_DESCRIPTION], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL], [URL]) VALUES (1, 1, N'Sub category 1', NULL, NULL, 1, CAST(0x0000AA4A00000000 AS DateTime), 1, CAST(0x0000AA4A00000000 AS DateTime), NULL, N'../Upload/2269download.jpg')
INSERT [dbo].[COM_CODING] ([CODING_ID], [CodingType_ID], [CODING_DESCRIPTION], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL], [URL]) VALUES (2, 1, N'Sub category 2', NULL, NULL, 1, CAST(0x0000AA4A00000000 AS DateTime), 1, CAST(0x0000AA4A00000000 AS DateTime), NULL, N'../Upload/2150download.jpg')
SET IDENTITY_INSERT [dbo].[COM_CODING] OFF
SET IDENTITY_INSERT [dbo].[COM_CODINGTYPE] ON 

INSERT [dbo].[COM_CODINGTYPE] ([CodingType_ID], [Description], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL], [URL]) VALUES (1, N'Category 1', NULL, NULL, 1, CAST(0x0000AA4A015558C8 AS DateTime), 1, CAST(0x0000AA4A015589E1 AS DateTime), NULL, N'../Upload/2383img-01.jpg')
INSERT [dbo].[COM_CODINGTYPE] ([CodingType_ID], [Description], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL], [URL]) VALUES (2, N'Category 2', NULL, NULL, 1, CAST(0x0000AA4A0155AD95 AS DateTime), 1, CAST(0x0000AA4A0155AD95 AS DateTime), NULL, N'../Upload/2809img-01.jpg')
INSERT [dbo].[COM_CODINGTYPE] ([CodingType_ID], [Description], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE], [ACCESS_LEVEL], [URL]) VALUES (3, N'Category 3', NULL, NULL, 1, CAST(0x0000AA4A0155C439 AS DateTime), 1, CAST(0x0000AA4A0155C439 AS DateTime), NULL, N'../Upload/2149img-01.jpg')
SET IDENTITY_INSERT [dbo].[COM_CODINGTYPE] OFF
SET IDENTITY_INSERT [dbo].[COM_CONTENT] ON 

INSERT [dbo].[COM_CONTENT] ([CONTENT_ID], [CODING_ID], [DESCRIPTION], [URL], [VIDEO_URL], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE]) VALUES (1, 1, N'Picture Item', N'../Upload/2725dubai.jpeg', N'', NULL, NULL, 1, CAST(0x0000AA4A0156E0C7 AS DateTime), 1, CAST(0x0000AA4A0158A254 AS DateTime))
INSERT [dbo].[COM_CONTENT] ([CONTENT_ID], [CODING_ID], [DESCRIPTION], [URL], [VIDEO_URL], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE]) VALUES (2, 1, N'Youtube video', N'https://youtu.be/LvJj9vxotUE?list=PLX9R3E_Pz-fY6lS4HB-6hD2kXxO178fQB', N'https://youtu.be/LvJj9vxotUE?list=PLX9R3E_Pz-fY6lS4HB-6hD2kXxO178fQB', NULL, NULL, 1, CAST(0x0000AA4A0157402B AS DateTime), 1, CAST(0x0000AA4A0157402B AS DateTime))
INSERT [dbo].[COM_CONTENT] ([CONTENT_ID], [CODING_ID], [DESCRIPTION], [URL], [VIDEO_URL], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE]) VALUES (3, 1, N'PDF Item', N'../Upload/2351sample.pdf', N'', NULL, NULL, 1, CAST(0x0000AA4A01585264 AS DateTime), 1, CAST(0x0000AA4A01585264 AS DateTime))
SET IDENTITY_INSERT [dbo].[COM_CONTENT] OFF
SET IDENTITY_INSERT [dbo].[COM_USER] ON 

INSERT [dbo].[COM_USER] ([USER_ID], [USERNAME], [PASSWORD], [EMAIL], [VALID_FROM], [VALID_TO], [CREATE_BY], [CREATE_DATE], [UPDATE_BY], [UPDATE_DATE], [LAST_LOGIN_DATE], [ACCESS_LEVEL]) VALUES (1, N'admin', N'UlBKsD7xupkARXA7rtsUahHGLvTFgawoLXO5bfQczLV6QIZM', N'admin@admin.com', NULL, NULL, NULL, CAST(0x0000AA4A01514DE7 AS DateTime), NULL, NULL, CAST(0x0000AA4A0154DC39 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[COM_USER] OFF
ALTER TABLE [dbo].[COM_CODING]  WITH CHECK ADD  CONSTRAINT [FK_COM_CODING_COM_CodingType] FOREIGN KEY([CodingType_ID])
REFERENCES [dbo].[COM_CODINGTYPE] ([CodingType_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[COM_CODING] CHECK CONSTRAINT [FK_COM_CODING_COM_CodingType]
GO
ALTER TABLE [dbo].[COM_CONTENT]  WITH CHECK ADD  CONSTRAINT [FK_COM_CONTENT_COM_Coding] FOREIGN KEY([CODING_ID])
REFERENCES [dbo].[COM_CODING] ([CODING_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[COM_CONTENT] CHECK CONSTRAINT [FK_COM_CONTENT_COM_Coding]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "COM_CODING"
            Begin Extent = 
               Top = 9
               Left = 721
               Bottom = 206
               Right = 1010
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "COM_USER"
            Begin Extent = 
               Top = 68
               Left = 0
               Bottom = 265
               Right = 252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "COM_USER_1"
            Begin Extent = 
               Top = 150
               Left = 411
               Bottom = 347
               Right = 663
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 41
         Width = 284
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_COM_CODING'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'       Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1360
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_COM_CODING'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_COM_CODING'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "COM_CodingType"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 356
               Right = 326
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "COM_USER_1"
            Begin Extent = 
               Top = 0
               Left = 567
               Bottom = 197
               Right = 819
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "COM_USER"
            Begin Extent = 
               Top = 10
               Left = 871
               Bottom = 207
               Right = 1123
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_COM_CODINGTYPE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_COM_CODINGTYPE'
GO
USE [master]
GO
ALTER DATABASE [WebMediaPortfolio-DB] SET  READ_WRITE 
GO
