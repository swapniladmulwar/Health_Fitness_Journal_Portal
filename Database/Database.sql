/****** Object:  Table [dbo].[Files]    Script Date: 3/20/2022 10:53:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[FileType] [varchar](100) NULL,
	[DataFiles] [varbinary](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](100) NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subscribers]    Script Date: 3/20/2022 10:53:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscribers](
	[SubscriberId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[DocumentId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubscriberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Subscribers]  WITH CHECK ADD  CONSTRAINT [FK_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Files] ([DocumentId])
GO
ALTER TABLE [dbo].[Subscribers] CHECK CONSTRAINT [FK_DocumentId]
GO