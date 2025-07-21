USE [SDSFinder]
GO

/****** Object:  Table [dbo].[Document]    Script Date: 5/28/2025 3:14:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET IDENTITY_INSERT [dbo].[Document] ON;
GO

CREATE TABLE [dbo].[Document](
	[Id] [int]  IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[SafetyDocumentId] [nvarchar](128) NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[FileLocation] [nvarchar](300) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
) ON [PRIMARY]
GO
