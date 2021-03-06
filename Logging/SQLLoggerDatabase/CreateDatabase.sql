USE [Log]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 08/24/2011 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operation](
	[Guid] [uniqueidentifier] NOT NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[InstanceHash] [int] NULL,
	[Method] [nvarchar](max) NOT NULL,
	[ThreadId] [int] NOT NULL,
	[ThreadName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedTableType [dbo].[tblOperation]    Script Date: 08/24/2011 21:41:59 ******/
CREATE TYPE [dbo].[tblOperation] AS TABLE(
	[Guid] [uniqueidentifier] NOT NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[InstanceHash] [int] NULL,
	[Method] [nvarchar](max) NOT NULL,
	[ThreadId] [int] NOT NULL,
	[ThreadName] [nvarchar](max) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tblLogEntry]    Script Date: 08/24/2011 21:41:59 ******/
CREATE TYPE [dbo].[tblLogEntry] AS TABLE(
	[Guid] [uniqueidentifier] NOT NULL,
	[Group] [uniqueidentifier] NOT NULL,
	[Level] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[ThreadId] [int] NOT NULL,
	[ThreadName] [nvarchar](max) NOT NULL,
	[Expiry] [smalldatetime] NOT NULL,
	[OperationGuid] [uniqueidentifier] NULL,
	[ExceptionType] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tblDictionary]    Script Date: 08/24/2011 21:41:59 ******/
CREATE TYPE [dbo].[tblDictionary] AS TABLE(
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Value] [nvarchar](max) NULL
)
GO
/****** Object:  Table [dbo].[Application]    Script Date: 08/24/2011 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Argument]    Script Date: 08/24/2011 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Argument](
	[Name] [nvarchar](200) NOT NULL,
	[OperationGuid] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Argument_1] PRIMARY KEY CLUSTERED 
(
	[Name] ASC,
	[OperationGuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spRegisterApplication]    Script Date: 08/24/2011 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spRegisterApplication] @ApplicationGuid uniqueidentifier,
	@ApplicationName nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	IF NOT EXISTS(SELECT * FROM dbo.[Application] WHERE Guid=@ApplicationGuid)
	BEGIN
		INSERT dbo.[Application] ([Guid], Name) 
		VALUES (@ApplicationGuid, @ApplicationName) 
	END ELSE BEGIN
		UPDATE dbo.[Application]
		SET Name = @ApplicationName
		WHERE [Guid] = @ApplicationGuid
	END
END
GO
/****** Object:  Table [dbo].[LogEntry]    Script Date: 08/24/2011 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogEntry](
	[Group] [uniqueidentifier] NOT NULL,
	[Level] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[ThreadId] [int] NOT NULL,
	[ThreadName] [nvarchar](max) NOT NULL,
	[OperationGuid] [uniqueidentifier] NULL,
	[ExceptionType] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[ApplicationGuid] [uniqueidentifier] NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Expiry] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_LogEntry] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Context]    Script Date: 08/24/2011 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Context](
	[LogEntryGuid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Context] PRIMARY KEY CLUSTERED 
(
	[LogEntryGuid] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spLog]    Script Date: 08/24/2011 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spLog]
	@ApplicationGuid uniqueidentifier,
	@LogEntry dbo.tblLogEntry readonly,
	@Context dbo.tblDictionary readonly,
	@Operation dbo.tblOperation readonly,
	@Argument dbo.tblDictionary readonly
AS
BEGIN
	SET NOCOUNT ON;
	INSERT dbo.Operation (
		[Guid],
		ParentGuid,
		CategoryName,
		Name,
		InstanceHash,
		Method,
		ThreadId,
		ThreadName
	) 
	SELECT	O.[Guid],
		O.ParentGuid,
		O.CategoryName,
		O.Name,
		O.InstanceHash,
		O.Method,
		O.ThreadId,
		O.ThreadName
	FROM	@Operation O
	LEFT OUTER JOIN Operation OT WITH (NOLOCK) ON OT.[Guid] = O.[Guid]
	WHERE OT.[Guid] IS NULL
		
	INSERT dbo.Argument (
		OperationGuid, 
		Name,
		Value
	)
	SELECT	A.Guid,
		A.Name,
		A.Value
	FROM @Argument A
	LEFT OUTER JOIN Operation OT WITH (NOLOCK) ON OT.[Guid] = A.[Guid]
	WHERE OT.[Guid] IS NULL
		
	INSERT dbo.LogEntry (
		[Guid],
		[Group],
		Level,
		Message,
		ThreadId,
		ThreadName,
		Expiry,
		ApplicationGuid,
		OperationGuid,
		ExceptionType,
		StackTrace
	) SELECT	[Guid],
		[Group],
		Level,
		Message,
		ThreadId,
		ThreadName,
		Expiry,
		@ApplicationGuid,
		OperationGuid,
		ExceptionType,
		StackTrace
	FROM @LogEntry
	
	INSERT dbo.Context (
		LogEntryGuid, 
		Name,
		Value
	)
	SELECT	C.Guid,
		C.Name,
		C.Value
	FROM @Context C
END
GO
/****** Object:  StoredProcedure [dbo].[spExpire]    Script Date: 08/24/2011 21:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExpire] (@Date DateTime) AS

DELETE
FROM	dbo.LogEntry WITH (READPAST)
WHERE	Expiry < @Date

DELETE	C
FROM	dbo.Context AS C WITH (READPAST)
	LEFT OUTER JOIN dbo.LogEntry AS L WITH (NOLOCK) ON C.LogEntryGuid = L.[Guid]
WHERE	L.[Guid] IS NULL

DELETE	O
FROM	dbo.Operation AS O WITH (READPAST)
	LEFT OUTER JOIN dbo.LogEntry AS L WITH (NOLOCK) ON L.OperationGuid = O.[Guid]
WHERE	L.[Guid] IS NULL

DELETE	A
FROM	dbo.Argument AS A WITH (READPAST)
	LEFT OUTER JOIN dbo.Operation AS O WITH (NOLOCK) ON A.OperationGuid = O.[Guid]
WHERE	O.[Guid] IS NULL
GO
/****** Object:  ForeignKey [FK_Argument_Operation]    Script Date: 08/24/2011 21:41:59 ******/
ALTER TABLE [dbo].[Argument]  WITH NOCHECK ADD  CONSTRAINT [FK_Argument_Operation] FOREIGN KEY([OperationGuid])
REFERENCES [dbo].[Operation] ([Guid])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Argument] NOCHECK CONSTRAINT [FK_Argument_Operation]
GO
/****** Object:  ForeignKey [FK_Context_LogEntry]    Script Date: 08/24/2011 21:41:59 ******/
ALTER TABLE [dbo].[Context]  WITH NOCHECK ADD  CONSTRAINT [FK_Context_LogEntry] FOREIGN KEY([LogEntryGuid])
REFERENCES [dbo].[LogEntry] ([Guid])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Context] NOCHECK CONSTRAINT [FK_Context_LogEntry]
GO
/****** Object:  ForeignKey [FK_LogEntry_Application]    Script Date: 08/24/2011 21:41:59 ******/
ALTER TABLE [dbo].[LogEntry]  WITH NOCHECK ADD  CONSTRAINT [FK_LogEntry_Application] FOREIGN KEY([ApplicationGuid])
REFERENCES [dbo].[Application] ([Guid])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[LogEntry] NOCHECK CONSTRAINT [FK_LogEntry_Application]
GO
/****** Object:  ForeignKey [FK_LogEntry_Operation1]    Script Date: 08/24/2011 21:41:59 ******/
ALTER TABLE [dbo].[LogEntry]  WITH NOCHECK ADD  CONSTRAINT [FK_LogEntry_Operation1] FOREIGN KEY([OperationGuid])
REFERENCES [dbo].[Operation] ([Guid])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[LogEntry] NOCHECK CONSTRAINT [FK_LogEntry_Operation1]
GO
/****** Object:  ForeignKey [FK_Operation_Operation1]    Script Date: 08/24/2011 21:41:59 ******/
ALTER TABLE [dbo].[Operation]  WITH NOCHECK ADD  CONSTRAINT [FK_Operation_Operation1] FOREIGN KEY([ParentGuid])
REFERENCES [dbo].[Operation] ([Guid])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Operation] NOCHECK CONSTRAINT [FK_Operation_Operation1]
GO
