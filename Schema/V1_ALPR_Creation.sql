USE [ALPR]

/****** Object:  Table [dbo].[ALPRExportDetail] ******/
CREATE TABLE [dbo].[ALPRExportDetail](
	[RecId] [bigint] NOT NULL,
	[CapturedPlateId] [bigint] NOT NULL,
	[TicketNumber] [bigint] NOT NULL,
	[ExportedOn] [datetime] NOT NULL,
	[ExportPath] [nvarchar](1000) NOT NULL,
	[UriLocation] [nvarchar](1024) NULL,
 CONSTRAINT [PK_ALPRExportDetail] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))

/****** Object:  Table [dbo].[CapturedPlates] ******/
CREATE TABLE [dbo].[CapturedPlates](
	[RecId] [bigint] NOT NULL,
	[RowGuid] [uniqueidentifier] NOT NULL,
	[CapturedAT] [datetime] NOT NULL,
	[NumberPlate] [nvarchar](50) NOT NULL,
	[BackColor] [int] NULL,
	[CameraIndex] [smallint] NOT NULL,
	[Color] [int] NULL,
	[Confidence] [int] NULL,
	[PlateType] [int] NULL,
	[PlateState] [nvarchar](10) NULL,
	[Region_Bottom] [int] NOT NULL,
	[Region_Left] [int] NOT NULL,
	[Region_Right] [int] NOT NULL,
	[Region_Top] [int] NOT NULL,
	[CharactersConfidence] [nvarchar](200) NULL,
	[AvgConfidence] [int] NULL,
	[GeoLocation] [geography] NOT NULL,
	[GeoLocationCode] [int] NOT NULL,
	[HitListJSON] [nvarchar](max) NULL,
	[Notes] [nvarchar](1000) NULL,
	[IRSAClientID] [int] NOT NULL,
	[ClientSerial] [bigint] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[NotifyOverride] [int] NOT NULL,
	[Client_RowGuid] [uniqueidentifier] NULL,
	[Notify] [smallint] NOT NULL,
	[HotlistCSV] [nvarchar](1000) NULL,
	[State] [smallint] NOT NULL,
	[Status] [smallint] NOT NULL,
	[XmlExportStatus] [smallint] NOT NULL,
	[TicketNumber] [bigint] NULL,
	[LastFormRecordID] [int] NULL,
	[AppliedCapturePolicyID] [int] NULL,
	[AppliedNotificationPolicyID] [int] NULL,
	[StorageType] [smallint] NOT NULL,
	[UriLocation] [nvarchar](1024) NULL,
	[CaptureType] [smallint] NOT NULL,
	[NotifyType] [smallint] NULL,
 CONSTRAINT [PK_CapturedPlates] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))

/****** Object:  Table [dbo].[CapturePlatesSummary] ******/
CREATE TABLE [dbo].[CapturePlatesSummary](
	[CapturePlateId] [bigint] NOT NULL,
	[StationId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[UnitId] [nvarchar](50) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[LoginId] [nvarchar](110) NOT NULL,
	[CaptureDate] [date] NOT NULL,
	[HasAlert] [bit] NOT NULL,
	[HasTicket] [bit] NOT NULL,
 CONSTRAINT [PK_CapturePlatesSummary_Composite] PRIMARY KEY CLUSTERED 
(
	[CapturePlateId] ASC,
	[UserId] ASC
))

/****** Object:  Table [dbo].[CapturePlatesSummaryStatus] ******/
CREATE TABLE [dbo].[CapturePlatesSummaryStatus](
	[SyncId] [bigint] NOT NULL,
	[LastExecutionDate] [datetime] NULL,
	[LastExecutionEndDate] [datetime] NULL,
	[StatusID] [int] NULL,
	[StatusDesc] [nvarchar](1024) NULL,
 CONSTRAINT [PK_CapturePlatesSummaryStatus] PRIMARY KEY CLUSTERED 
(
	[SyncId] ASC
))

/****** Object:  Table [dbo].[UserCapturedPlates] ******/
CREATE TABLE [dbo].[UserCapturedPlates](
	[RecId] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[CapturedID] [bigint] NOT NULL,
 CONSTRAINT [PK_UserCapturedPlates] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))

/****** Object:  Table [dbo].[Hotlist] ******/
CREATE TABLE [dbo].[Hotlist](
	[RecId] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[SourceID] [bigint] NOT NULL,
	[RulesExpression] [nvarchar](1000) NULL,
	[AlertPriority] [smallint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[LastTimeStamp] [timestamp] NOT NULL,
	[StationID] [bigint] NULL,
	[Color] [nvarchar](10) NULL,
	[StorageType] [smallint] NULL,
	[URILocation] [nvarchar](1024) NULL,
	[AudioType] [smallint] NULL,
	[AudioSize] [int] NULL,
 CONSTRAINT [PK_HotList] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))

/****** Object:  Table [dbo].[HotlistDataSource] ******/
CREATE TABLE [dbo].[HotlistDataSource](
	[RecId] [bigint] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[SourceName] [nvarchar](50) NULL,
	[AgencyID] [bigint] NULL,
	[SourceTypeID] [bigint] NOT NULL,
	[SchedulePeriod] [decimal](10, 0) NULL,
	[LastUpdated] [datetime] NULL,
	[IsExpire] [bit] NULL,
	[SchemaDefinition] [nvarchar](500) NULL,
	[LastUpdateExternalHotListID] [bigint] NULL,
	[ConnectionType] [nvarchar](10) NULL,
	[Userid] [nvarchar](50) NULL,
	[locationPath] [nvarchar](100) NULL,
	[Password] [nvarchar](50) NULL,
	[port] [int] NULL,
	[LastRun] [datetime] NULL,
	[Status] [smallint] NOT NULL,
	[SkipFirstLine] [bit] NOT NULL,
	[StatusDesc] [nvarchar](500) NULL,
 CONSTRAINT [PK_HotListSource] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))

/****** Object:  Table [dbo].[SourceType] ******/
CREATE TABLE [dbo].[SourceType](
	[RecId] [bigint] NOT NULL,
	[SourceTypeName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_SourceType] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))
/****** Object:  Table [dbo].[HotListNumberPlates] ******/
CREATE TABLE [dbo].[HotListNumberPlates](
	[RecId] [bigint] NOT NULL,
	[HotListId] [bigint] NOT NULL,
	[NumberPlatesId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NULL,
	[LastTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_HotListNumberPlates] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))

/****** Object:  Table [dbo].[NumberPlates] ******/
CREATE TABLE [dbo].[NumberPlates](
	[RecId] [bigint] NOT NULL,
	[ImportSerialID] [nvarchar](50) NULL,
	[NCICNumber] [nvarchar](50) NULL,
	[AgencyID] [nvarchar](50) NULL,
	[DateOfInterest] [datetime] NOT NULL,
	[NumberPlate] [nvarchar](50) NOT NULL,
	[StateId] [tinyint] NULL,
	[LicenseYear] [nvarchar](50) NULL,
	[LicenseType] [nvarchar](50) NULL,
	[VehicleYear] [nvarchar](50) NULL,
	[VehicleMake] [nvarchar](50) NULL,
	[VehicleModel] [nvarchar](50) NULL,
	[VehicleStyle] [nvarchar](50) NULL,
	[VehicleColor] [nvarchar](50) NULL,
	[Note] [nvarchar](500) NULL,
	[InsertType] [smallint] NULL,
	[CreatedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NULL,
	[LastTimeStamp] [timestamp] NULL,
	[Status] [smallint] NULL,
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[Alias] [nvarchar](50) NULL,
	[ViolationInfo] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_NumberPlates] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))

/****** Object:  Table [dbo].[NumberPlatesTemp] ******/
CREATE TABLE [dbo].[NumberPlatesTemp](
	[RecId] [bigint] NOT NULL,
	[ImportSerialID] [nvarchar](50) NULL,
	[NCICNumber] [nvarchar](50) NULL,
	[AgencyID] [nvarchar](50) NULL,
	[DateOfInterest] [datetime] NOT NULL,
	[NumberPlate] [nvarchar](50) NOT NULL,
	[StateId] [tinyint] NULL,
	[LicenseYear] [nvarchar](50) NULL,
	[LicenseType] [nvarchar](50) NULL,
	[VehicleYear] [nvarchar](50) NULL,
	[VehicleMake] [nvarchar](50) NULL,
	[VehicleModel] [nvarchar](50) NULL,
	[VehicleStyle] [nvarchar](50) NULL,
	[VehicleColor] [nvarchar](50) NULL,
	[Note] [nvarchar](500) NULL,
	[InsertType] [smallint] NULL,
	[CreatedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NULL,
	[LastTimeStamp] [timestamp] NULL,
	[Status] [smallint] NULL,
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[Alias] [nvarchar](50) NULL,
	[ViolationInfo] [nvarchar](max) NULL,
 CONSTRAINT [PK_NumberPlatesTemp] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))

/****** Object:  Table [dbo].[State] ******/
CREATE TABLE [dbo].[State](
	[RecId] [tinyint] NOT NULL,
	[StateName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
))

/****** Object:  Table [dbo].[ServiceInfo] ******/
CREATE TABLE [dbo].[ServiceInfo](
	[CMT_Tenant_RecId] [bigint] NOT NULL,
	[TenantIdentifier] [nvarchar](128) NOT NULL,
	[ServiceVersion] [nvarchar](25) NULL,
	[DBVersion] [nvarchar](25) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[TenantName] [nvarchar](128) NOT NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[ALPRExportDetail] ADD  CONSTRAINT [DF_ALPRExportDetail]  DEFAULT (getutcdate()) FOR [ExportedOn]
GO
ALTER TABLE [dbo].[NumberPlates] ADD  CONSTRAINT [DF_NumberPlates_InsertType]  DEFAULT ((2)) FOR [InsertType]
GO
ALTER TABLE [dbo].[NumberPlates] ADD  CONSTRAINT [DF_NumberPlates_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[NumberPlatesTemp] ADD  CONSTRAINT [DF_NumberPlatesTemp_InsertType]  DEFAULT ((2)) FOR [InsertType]
GO
ALTER TABLE [dbo].[NumberPlatesTemp] ADD  CONSTRAINT [DF_NumberPlatesTemp_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[HotListNumberPlates]  WITH CHECK ADD  CONSTRAINT [FK_HotListNumberPlates_HotList] FOREIGN KEY([HotListId])
REFERENCES [dbo].[Hotlist] ([RecId])
GO
ALTER TABLE [dbo].[HotListNumberPlates] CHECK CONSTRAINT [FK_HotListNumberPlates_HotList]
GO
ALTER TABLE [dbo].[HotListNumberPlates]  WITH CHECK ADD  CONSTRAINT [FK_HotListNumberPlates_NumberPlates] FOREIGN KEY([NumberPlatesId])
REFERENCES [dbo].[NumberPlates] ([RecId])
GO
ALTER TABLE [dbo].[HotListNumberPlates] CHECK CONSTRAINT [FK_HotListNumberPlates_NumberPlates]
GO
ALTER TABLE [dbo].[NumberPlates]  WITH CHECK ADD  CONSTRAINT [FK_NumberPlates_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([RecId])
GO
ALTER TABLE [dbo].[NumberPlates] CHECK CONSTRAINT [FK_NumberPlates_State]
GO
ALTER TABLE [dbo].[NumberPlatesTemp]  WITH CHECK ADD  CONSTRAINT [FK_NumberPlatesTemp_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([RecId])
GO
ALTER TABLE [dbo].[NumberPlatesTemp] CHECK CONSTRAINT [FK_NumberPlatesTemp_State]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  CONSTRAINT [DF_CapturedPlates_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  CONSTRAINT [DF_CapturedPlates_NotifyOverride]  DEFAULT ((0)) FOR [NotifyOverride]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  CONSTRAINT [DF_CapturedPlates_Notify]  DEFAULT ((0)) FOR [Notify]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  CONSTRAINT [DF_CapturedPlates_State]  DEFAULT ((0)) FOR [State]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  CONSTRAINT [DF_CapturedPlates_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  CONSTRAINT [DF_CapturedPlates_XmlExportStatus]  DEFAULT ((0)) FOR [XmlExportStatus]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  CONSTRAINT [DF_CapturedPlates_StorageType]  DEFAULT ((1)) FOR [StorageType]
GO
ALTER TABLE [dbo].[CapturedPlates] ADD  CONSTRAINT [DF_CapturedPlates_CaptureType]  DEFAULT ((1)) FOR [CaptureType]
GO
ALTER TABLE [dbo].[Hotlist] ADD  CONSTRAINT [DF_HotList_AlertPriority]  DEFAULT ((1)) FOR [AlertPriority]
GO
ALTER TABLE [dbo].[HotlistDataSource] ADD  CONSTRAINT [DF_HotlistDataSource_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[HotlistDataSource] ADD  CONSTRAINT [DF_HotlistDataSource_SkipFirstLine]  DEFAULT ((0)) FOR [SkipFirstLine]
GO
ALTER TABLE [dbo].[Hotlist]  WITH NOCHECK ADD  CONSTRAINT [FK_HotList_HotListDataSource] FOREIGN KEY([SourceID])
REFERENCES [dbo].[HotlistDataSource] ([RecId])
GO
ALTER TABLE [dbo].[Hotlist] NOCHECK CONSTRAINT [FK_HotList_HotListDataSource]
GO
ALTER TABLE [dbo].[HotlistDataSource]  WITH CHECK ADD  CONSTRAINT [FK_HotListSource_SourceType] FOREIGN KEY([SourceTypeID])
REFERENCES [dbo].[SourceType] ([RecId])
GO
ALTER TABLE [dbo].[HotlistDataSource] CHECK CONSTRAINT [FK_HotListSource_SourceType]
GO