USE [todo_test]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 2/29/2016 11:54:56 AM ******/
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
/****** Object:  Table [dbo].[categories_tasks]    Script Date: 2/29/2016 11:54:56 AM ******/
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
/****** Object:  Table [dbo].[tasks]    Script Date: 2/29/2016 11:54:56 AM ******/
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
SET IDENTITY_INSERT [dbo].[categories_tasks] ON 

INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (1, 1, 9)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (2, 1, 6)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (3, 4, 11)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (5, 8, 13)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (6, 11, 16)
SET IDENTITY_INSERT [dbo].[categories_tasks] OFF
