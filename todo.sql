USE [todo]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 2/29/2016 11:53:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[categories_tasks]    Script Date: 2/29/2016 11:53:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories_tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NULL,
	[task_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tasks]    Script Date: 2/29/2016 11:53:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](255) NULL,
	[due_date] [datetime] NULL,
	[task_completed] [bit] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [name]) VALUES (1, N'house chores')
INSERT [dbo].[categories] ([id], [name]) VALUES (2, N'Epicodus')
INSERT [dbo].[categories] ([id], [name]) VALUES (3, N'r')
INSERT [dbo].[categories] ([id], [name]) VALUES (4, N'exercise')
INSERT [dbo].[categories] ([id], [name]) VALUES (5, N'fdsfasd')
SET IDENTITY_INSERT [dbo].[categories] OFF
SET IDENTITY_INSERT [dbo].[categories_tasks] ON 

INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (1, 1, 9)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (2, 1, 6)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (3, 4, 11)
SET IDENTITY_INSERT [dbo].[categories_tasks] OFF
SET IDENTITY_INSERT [dbo].[tasks] ON 

INSERT [dbo].[tasks] ([id], [description], [due_date], [task_completed]) VALUES (9, N'take trash out', CAST(N'2016-02-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[tasks] ([id], [description], [due_date], [task_completed]) VALUES (10, N'run 1 mile', CAST(N'2016-02-23T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[tasks] ([id], [description], [due_date], [task_completed]) VALUES (6, N'clean house', CAST(N'1994-01-23T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[tasks] ([id], [description], [due_date], [task_completed]) VALUES (11, N'run fast', CAST(N'2016-12-09T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tasks] OFF
