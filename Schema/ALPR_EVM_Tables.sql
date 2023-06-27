Use ALPR
GO

/****** Object:  Table [dbo].[ALPRExportDetail]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SourceType](
	[sysSerial] [int] IDENTITY(1,1) NOT NULL,
	[SourceTypeName] [varchar](50) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_SourceType] PRIMARY KEY CLUSTERED 
(
	[sysSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[ALPRExportDetail](
	[SysSerial] [int] IDENTITY(1,1) NOT NULL,
	[CapturedPlateId] [bigint] NOT NULL,
	[TicketNumber] [bigint] NOT NULL,
	[ExportedOn] [datetime] NOT NULL,
	[ExportPath] [varchar](1000) NOT NULL,
	[UriLocation] [varchar](1024) NULL,
 CONSTRAINT [PK_ALPRExportDetail] PRIMARY KEY CLUSTERED 
(
	[SysSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CapturedPlates]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[CapturedPlates](
	[sysSerial] [bigint] IDENTITY(1,1) NOT NULL,
	[RowGuid] [uniqueidentifier] NOT NULL,
	[CapturedAT] [datetime] NOT NULL,
	[NumberPlate] [varchar](50) NOT NULL,
	[BackColor] [int] NULL,
	[CameraIndex] [smallint] NOT NULL,
	[Color] [int] NULL,
	[Confidence] [int] NULL,
	[PlateType] [int] NULL,
	[PlateState] [varchar](10) NULL,
	[Region_Bottom] [int] NOT NULL,
	[Region_Left] [int] NOT NULL,
	[Region_Right] [int] NOT NULL,
	[Region_Top] [int] NOT NULL,
	[CharactersConfidence] [varchar](200) NULL,
	[AvgConfidence] [int] NULL,
	[GeoLocation] [geography] NOT NULL,
	[GeoLocationCode] [int] NOT NULL,
	[HitListJSON] [varchar](max) NULL,
	[Notes] [varchar](1000) NULL,
	[IRSAClientID] [int] NOT NULL,
	[ClientSerial] [bigint] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[NotifyOverride] [int] NOT NULL,
	[Client_RowGuid] [uniqueidentifier] NULL,
	[Notify] [smallint] NOT NULL,
	[HotlistCSV] [varchar](1000) NULL,
	[State] [smallint] NOT NULL,
	[Status] [smallint] NOT NULL,
	[XmlExportStatus] [smallint] NOT NULL,
	[TicketNumber] [bigint] NULL,
	[LastFormRecordID] [int] NULL,
	[AppliedCapturePolicyID] [int] NULL,
	[AppliedNotificationPolicyID] [int] NULL,
	[StorageType] [smallint] NOT NULL,
	[UriLocation] [varchar](1024) NULL,
	[CaptureType] [smallint] NOT NULL,
	[NotifyType] [smallint] NULL,
 CONSTRAINT [PK_CapturedPlates] PRIMARY KEY CLUSTERED 
(
	[sysSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RowGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CapturePlatesSummary]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CapturePlatesSummary](
	[CapturePlateId] [bigint] NOT NULL,
	[StationId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[UnitId] [varchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoginId] [varchar](110) NOT NULL,
	[CaptureDate] [date] NOT NULL,
	[HasAlert] [bit] NOT NULL,
	[HasTicket] [bit] NOT NULL,
 CONSTRAINT [PK_CapturePlatesSummary_Composite] PRIMARY KEY CLUSTERED 
(
	[CapturePlateId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CapturePlatesSummaryStatus]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CapturePlatesSummaryStatus](
	[SyncId] [int] IDENTITY(1,1) NOT NULL,
	[LastExecutionDate] [datetime] NULL,
	[LastExecutionEndDate] [datetime] NULL,
	[StatusID] [int] NULL,
	[StatusDesc] [varchar](1024) NULL,
 CONSTRAINT [PK_CapturePlatesSummaryStatus] PRIMARY KEY CLUSTERED 
(
	[SyncId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Hotlist]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Hotlist](
	[sysSerial] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](500) NULL,
	[SourceID] [int] NULL,
	[RulesExpression] [varchar](1000) NULL,
	[AlertPriority] [smallint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[LastTimeStamp] [timestamp] NOT NULL,
	[StationID] [int] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[Hotlist] ADD [Color] [varchar](10) NULL
ALTER TABLE [dbo].[Hotlist] ADD [StorageType] [smallint] NULL
ALTER TABLE [dbo].[Hotlist] ADD [URILocation] [varchar](1024) NULL
ALTER TABLE [dbo].[Hotlist] ADD [AudioType] [smallint] NULL
ALTER TABLE [dbo].[Hotlist] ADD [AudioSize] [int] NULL
 CONSTRAINT [PK_HotList] PRIMARY KEY CLUSTERED 
(
	[sysSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HotlistDataSource]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[HotlistDataSource](
	[sysSerial] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[SourceName] [varchar](50) NULL,
	[AgencyID] [int] NULL,
	[SourceTypeID] [int] NOT NULL,
	[SchedulePeriod] [decimal](10, 0) NULL,
	[LastUpdated] [datetime] NULL,
	[IsExpire] [bit] NULL,
	[SchemaDefinition] [varchar](500) NULL,
	[LastUpdateExternalHotListID] [int] NULL,
	[ConnectionType] [varchar](10) NULL,
	[Userid] [varchar](50) NULL,
	[locationPath] [varchar](100) NULL,
	[Password] [varchar](50) NULL,
	[port] [int] NULL,
	[LastRun] [datetime] NULL,
	[Status] [smallint] NOT NULL DEFAULT ((0)),
	[SkipFirstLine] [bit] NOT NULL DEFAULT ((0)),
	[StatusDesc] [varchar](8000) NULL,
 CONSTRAINT [PK_HotListSource] PRIMARY KEY CLUSTERED 
(
	[sysSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HotListNumberPlates]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HotListNumberPlates](
	[sysSerial] [bigint] IDENTITY(1,1) NOT NULL,
	[HotListID] [int] NOT NULL,
	[NumberPlatesID] [bigint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[LastTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_HotListNumberPlates] PRIMARY KEY CLUSTERED 
(
	[sysSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NumberPlates]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NumberPlates](
	[sysSerial] [bigint] IDENTITY(1,1) NOT NULL,
	[ImportSerialID] [varchar](50) NULL,
	[NCICNumber] [varchar](50) NULL,
	[AgencyID] [varchar](50) NULL,
	[DateOfInterest] [datetime] NULL,
	[NumberPlate] [varchar](50) NULL,
	[StateID] [varchar](50) NULL,
	[LicenseYear] [varchar](50) NULL,
	[LicenseType] [varchar](50) NULL,
	[VehicleYear] [varchar](50) NULL,
	[VehicleMake] [varchar](50) NULL,
	[VehicleModel] [varchar](50) NULL,
	[VehicleStyle] [varchar](50) NULL,
	[VehicleColor] [varchar](50) NULL,
	[Note] [varchar](500) NULL,
	[InsertType] [smallint] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[LastTimeStamp] [timestamp] NOT NULL,
	[Status] [smallint] NOT NULL,
	[FirstName] [varchar](200) NULL,
	[LastName] [varchar](200) NULL,
	[Alias] [varchar](50) NULL,
	[ViolationInfo] [varchar](max) NULL,
	[Notes] [varchar](max) NULL,
 CONSTRAINT [PK_NumberPlates] PRIMARY KEY CLUSTERED 
(
	[sysSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NumberPlatesTemp]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NumberPlatesTemp](
	[sysSerial] [bigint] IDENTITY(1,1) NOT NULL,
	[ImportSerialID] [varchar](50) NULL,
	[NCICNumber] [varchar](50) NULL,
	[AgencyID] [varchar](50) NULL,
	[DateOfInterest] [datetime] NULL,
	[NumberPlate] [varchar](50) NULL,
	[StateID] [varchar](50) NULL,
	[LicenseYear] [varchar](50) NULL,
	[LicenseType] [varchar](50) NULL,
	[VehicleYear] [varchar](50) NULL,
	[VehicleMake] [varchar](50) NULL,
	[VehicleModel] [varchar](50) NULL,
	[VehicleStyle] [varchar](50) NULL,
	[VehicleColor] [varchar](50) NULL,
	[Note] [varchar](500) NULL,
	[InsertType] [smallint] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[LastTimeStamp] [timestamp] NOT NULL,
	[Status] [smallint] NOT NULL,
	[FirstName] [varchar](200) NULL,
	[LastName] [varchar](200) NULL,
	[Alias] [varchar](50) NULL,
	[ViolationInfo] [varchar](max) NULL,
 CONSTRAINT [PK_NumberPlatesTemp] PRIMARY KEY CLUSTERED 
(
	[sysSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserCapturedPlates]    Script Date: 07/03/2023 6:02:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCapturedPlates](
	[sysSerial] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[CapturedID] [bigint] NOT NULL,
 CONSTRAINT [PK_UserCapturedPlates] PRIMARY KEY CLUSTERED 
(
	[sysSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ALPRExportDetail] ADD  CONSTRAINT [DF__ALPRExpor__Expor__04AFB25B]  DEFAULT (getutcdate()) FOR [ExportedOn]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  DEFAULT (newid()) FOR [RowGuid]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  CONSTRAINT [DF_CapturedPlates_NotifyOverride]  DEFAULT ((0)) FOR [NotifyOverride]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  DEFAULT ((0)) FOR [Notify]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  DEFAULT ((0)) FOR [State]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  DEFAULT ((0)) FOR [XmlExportStatus]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  DEFAULT ((1)) FOR [StorageType]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  DEFAULT ((1)) FOR [CaptureType]
GO
ALTER TABLE [dbo].[Hotlist] ADD  CONSTRAINT [DF_HotList_AlertPriority]  DEFAULT ((1)) FOR [AlertPriority]
GO
ALTER TABLE [dbo].[NumberPlates] ADD  CONSTRAINT [DF_NumberPlates_InsertType]  DEFAULT ((2)) FOR [InsertType]
GO
ALTER TABLE [dbo].[NumberPlates] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[NumberPlatesTemp] ADD  CONSTRAINT [DF_NumberPlatesTemp_InsertType]  DEFAULT ((2)) FOR [InsertType]
GO
ALTER TABLE [dbo].[NumberPlatesTemp] ADD  CONSTRAINT [DF_NumberPlatesTemp_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Hotlist]  WITH NOCHECK ADD  CONSTRAINT [FK_HotList_HotListDataSource] FOREIGN KEY([SourceID])
REFERENCES [dbo].[HotlistDataSource] ([sysSerial])
GO
ALTER TABLE [dbo].[Hotlist] NOCHECK CONSTRAINT [FK_HotList_HotListDataSource]
GO
ALTER TABLE [dbo].[HotlistDataSource]  WITH CHECK ADD  CONSTRAINT [FK_HotListSource_SourceType] FOREIGN KEY([SourceTypeID])
REFERENCES [dbo].[SourceType] ([sysSerial])
GO
ALTER TABLE [dbo].[HotlistDataSource] CHECK CONSTRAINT [FK_HotListSource_SourceType]
GO
