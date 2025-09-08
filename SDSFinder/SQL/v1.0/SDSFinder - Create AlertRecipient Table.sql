USE [SDSFinder]
GO

/****** Object:  Table [dbo].[AlertRecipients]    Script Date: 9/5/2025 4:37:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AlertRecipients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[UserName] [nvarchar](100) NULL
) ON [PRIMARY]
GO

