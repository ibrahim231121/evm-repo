USE [ALPR]

INSERT [dbo].[SourceType] ([RecId], [SourceTypeName], [Description]) VALUES (10001, N'XML', N'XML Data Input')
INSERT [dbo].[SourceType] ([RecId], [SourceTypeName], [Description]) VALUES (10002, N'CSV', N'CSV Data Input')
INSERT [dbo].[SourceType] ([RecId], [SourceTypeName], [Description]) VALUES (10003, N'Manual', N'Manual Data Input')
GO

INSERT [dbo].[State] ([RecId], [StateName]) VALUES (1, N'TX')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (2, N'NY')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (3, N'CA')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (4, N'FL')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (5, N'AL')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (6, N'AK')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (7, N'AZ')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (8, N'AR')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (9, N'CO')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (10, N'CT')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (12, N'DE')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (13, N'GA')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (14, N'HI')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (15, N'ID')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (16, N'IL')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (17, N'IN')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (18, N'IA')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (19, N'KS')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (20, N'KY')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (21, N'LA')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (22, N'ME')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (23, N'MD')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (24, N'MA')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (25, N'MI')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (26, N'MN')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (27, N'MS')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (28, N'MO')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (29, N'MT')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (30, N'NE')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (31, N'NV')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (32, N'NH')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (33, N'NJ')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (34, N'NM')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (35, N'NC')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (36, N'ND')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (37, N'OH')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (38, N'OK')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (39, N'OR')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (40, N'PA')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (41, N'RI')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (42, N'SC')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (43, N'SD')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (44, N'TN')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (45, N'UT')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (46, N'VT')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (47, N'VA')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (48, N'WA')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (49, N'WV')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (50, N'WI')
GO
INSERT [dbo].[State] ([RecId], [StateName]) VALUES (51, N'WY')
GO

INSERT [dbo].[ServiceInfo] ([CMT_Tenant_RecId], [TenantIdentifier], [ServiceVersion], [DBVersion], [CreatedOn], [UpdatedOn], [TenantName]) VALUES (1, N'1', N'1', N'1', CAST(N'2023-06-14T00:00:00.000' AS DateTime), NULL, N'HDC')
GO