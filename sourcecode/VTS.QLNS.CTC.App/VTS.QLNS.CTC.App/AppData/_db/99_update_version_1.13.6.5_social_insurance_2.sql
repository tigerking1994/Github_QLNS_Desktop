IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Table_1_iID_BH_DanhMucLoaiCap]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BH_DM_LoaiChi] DROP CONSTRAINT [DF_Table_1_iID_BH_DanhMucLoaiCap]
END
GO
/****** Object:  Table [dbo].[BH_DM_LoaiChi]    Script Date: 12/8/2023 9:11:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BH_DM_LoaiChi]') AND type in (N'U'))
DROP TABLE [dbo].[BH_DM_LoaiChi]
GO
/****** Object:  Table [dbo].[BH_DM_LoaiChi]    Script Date: 12/8/2023 9:11:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BH_DM_LoaiChi](
	[iID] [uniqueidentifier] NOT NULL,
	[sMaLoaiChi] [nvarchar](50) NULL,
	[sTenDanhMucLoaiChi] [nvarchar](100) NULL,
	[iNamLamViec] [int] NULL,
	[dNgaySua] [smalldatetime] NULL,
	[dNgayTao] [smalldatetime] NULL,
	[sNguoiSua] [nvarchar](100) NULL,
	[sNguoiTao] [nvarchar](100) NULL,
	[sMoTa] [nvarchar](max) NULL,
	[iTrangThai] [int] NULL,
	[sLNS] [nvarchar](100) NULL,
	[sDSXauNoiMa] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'1e909740-3235-4be4-b992-6c7d101ec384', N'2312', N'Chi các chế độ BHXH', 2023, CAST(N'2023-12-07T09:00:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'123123', 1, N'9010001,9010002', N'9010001, 9010002')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'b04c48e3-1bbc-4981-b59e-bbdf0ccece84', N'123123', N'Chi kinh phí quản lý BHXH, BHYT', 2023, CAST(N'2023-12-06T10:41:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'12321', 1, N'9010003', N'12312')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'6d0cb545-dc20-4d9d-be72-2b133c1e9774', N'12312', N'Chi kinh phí KCB tại quân y đơn vị ', 2023, CAST(N'2023-12-06T10:41:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'2312', 1, N'9010004,9010005', N'21312')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'3e321e6d-8ea6-40de-bbb9-7c5befeb387d', N'12312', N'Chi kinh phí KCB tại Trường Sa', 2023, CAST(N'2023-12-06T10:41:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'2313', 1, N'9010006,9010007', N'12321')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'0bd3f182-1261-472f-8e01-fb8ce57b97c2', N'1231', N'Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ', 2023, CAST(N'2023-12-06T10:41:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'312321', 1, N'9050001,9050002', N'1231')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'5211ca63-232b-47f8-bb53-4cc1407b68a4', N'231', N'Chi kinh phí chăm sóc sức khỏe ban đầu NLĐ ', 2023, CAST(N'2023-12-06T13:46:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'213123', 0, N'9050001', N'2321')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'107ff3e7-ee97-4b26-b148-b0631f60a2fa', N'1231', N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân', 2023, CAST(N'2023-12-07T10:56:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'23123', 1, N'9010008', N'9010008, 9010008-010-011-0001, 9010008-010-011-0002')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'ee2053ea-6cd5-40bb-a706-f03138a96c55', N'08', N'Chi hỗ trợ BHTN', 2023, CAST(N'2023-12-06T10:41:00' AS SmallDateTime), CAST(N'2023-12-07T10:52:00' AS SmallDateTime), N'admin', N'admin', N'312321', 1, N'9010010', N'9010010, 9010010-010-011-0001, 9010010-010-011-0001-0001, 9010010-010-011-0001-0002, 9010010-010-011-0001-0003, 9010010-010-011-0001-0004, 9010010-010-011-0001-0005, 9010010-010-011-0001-0006, 9010010-010-011-0002, 9010010-010-011-0002-0001, 9010010-010-011-0002-0002, 9010010-010-011-0002-0003, 9010010-010-011-0002-0004, 9010010-010-011-0002-0005, 9010010-010-011-0002-0006')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'c6e13dda-639c-45b8-8bdb-cf2a5d6822ed', N'09', N'Kinh phí mua sắm trang thiết bị y tế', 2023, CAST(N'2023-12-06T10:41:00' AS SmallDateTime), CAST(N'2023-12-07T10:58:00' AS SmallDateTime), N'admin', N'admin', N'312321', 1, N'9010009', N'9010009, 9010009-010-011-0001, 9010009-010-011-0002')
GO
ALTER TABLE [dbo].[BH_DM_LoaiChi] ADD  CONSTRAINT [DF_Table_1_iID_BH_DanhMucLoaiCap]  DEFAULT (newid()) FOR [iID]
GO
