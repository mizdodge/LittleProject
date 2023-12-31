USE [by.humbi_master]
GO
/****** Object:  Table [dbo].[ItemList]    Script Date: 12/22/2023 11:58:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemList](
	[ID] [uniqueidentifier] NOT NULL,
	[ItemName] [varchar](255) NULL,
	[ItemDescription] [varchar](255) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](255) NOT NULL,
	[ModifiedBy] [varchar](255) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ItemList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemVariant]    Script Date: 12/22/2023 11:58:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemVariant](
	[ID] [uniqueidentifier] NOT NULL,
	[ItemListID] [uniqueidentifier] NOT NULL,
	[ItemVariantID] [uniqueidentifier] NULL,
	[ItemVariant] [varchar](255) NULL,
	[ItemVariantDescription] [varchar](255) NULL,
	[Quantity] [bigint] NOT NULL,
 CONSTRAINT [PK_ItemVariant] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemVariantMaster]    Script Date: 12/22/2023 11:58:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemVariantMaster](
	[ID] [uniqueidentifier] NOT NULL,
	[ItemVariantName] [varchar](255) NOT NULL,
	[ItemVariantDescription] [varchar](255) NULL,
	[CreatedBy] [varchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](255) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ItemVariantMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetailMaster]    Script Date: 12/22/2023 11:58:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetailMaster](
	[ID] [uniqueidentifier] NOT NULL,
	[Address] [varchar](max) NOT NULL,
	[City] [varchar](max) NOT NULL,
	[PostalCode] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserDetailMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 12/22/2023 11:58:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[ID] [uniqueidentifier] NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Firstname] [varchar](255) NOT NULL,
	[Lastname] [varchar](255) NULL,
	[UserDetailID] [uniqueidentifier] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ItemList] ([ID], [ItemName], [ItemDescription], [CreatedDate], [CreatedBy], [ModifiedBy], [ModifiedDate]) VALUES (N'4b60286f-af50-4572-89bc-2342ac676aec', N'Cedea', N'Cedea Ikan', CAST(N'2023-12-21T22:43:41.717' AS DateTime), N'admin', NULL, NULL)
GO
INSERT [dbo].[ItemVariant] ([ID], [ItemListID], [ItemVariantID], [ItemVariant], [ItemVariantDescription], [Quantity]) VALUES (N'657933f9-a6b8-4b80-913d-90e770be81b5', N'4b60286f-af50-4572-89bc-2342ac676aec', NULL, N'Ikan', N'ini varian ikan enak', 50)
GO
INSERT [dbo].[UserDetailMaster] ([ID], [Address], [City], [PostalCode]) VALUES (N'70e5805e-8d9a-46c9-bf2f-592de6ed9539', N'jl. bareng', N'jakarta', N'13131')
GO
INSERT [dbo].[UserMaster] ([ID], [Username], [Password], [Firstname], [Lastname], [UserDetailID], [IsAdmin]) VALUES (N'eb13c671-8b37-4fb9-8758-f0b908d6a4d3', N'admin', N'admin1234', N'admin', N'admin', N'70e5805e-8d9a-46c9-bf2f-592de6ed9539', 1)
GO
ALTER TABLE [dbo].[ItemList] ADD  CONSTRAINT [DF_ItemList_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[ItemVariant] ADD  CONSTRAINT [DF_ItemVariant_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[UserDetailMaster] ADD  CONSTRAINT [DF_UserDetailMaster_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[ItemVariant]  WITH CHECK ADD  CONSTRAINT [FK_ItemVariant_ItemList] FOREIGN KEY([ItemListID])
REFERENCES [dbo].[ItemList] ([ID])
GO
ALTER TABLE [dbo].[ItemVariant] CHECK CONSTRAINT [FK_ItemVariant_ItemList]
GO
ALTER TABLE [dbo].[UserDetailMaster]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Table_2] FOREIGN KEY([ID])
REFERENCES [dbo].[UserDetailMaster] ([ID])
GO
ALTER TABLE [dbo].[UserDetailMaster] CHECK CONSTRAINT [FK_Table_1_Table_2]
GO
ALTER TABLE [dbo].[UserMaster]  WITH CHECK ADD  CONSTRAINT [FK_UserMaster_UserDetailMaster] FOREIGN KEY([UserDetailID])
REFERENCES [dbo].[UserDetailMaster] ([ID])
GO
ALTER TABLE [dbo].[UserMaster] CHECK CONSTRAINT [FK_UserMaster_UserDetailMaster]
GO
