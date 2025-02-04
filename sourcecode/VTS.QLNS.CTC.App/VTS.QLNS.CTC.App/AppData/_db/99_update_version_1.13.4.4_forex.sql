GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_TiGia___ID__7D0E9093]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_TiGia_ChiTiet] DROP CONSTRAINT [DF__NH_DM_TiGia___ID__7D0E9093]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_TiGia__ID__7A3223E8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_TiGia] DROP CONSTRAINT [DF__NH_DM_TiGia__ID__7A3223E8]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_Phuong__ID__7755B73D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_PhuongThucChonNhaThau] DROP CONSTRAINT [DF__NH_DM_Phuong__ID__7755B73D]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_PhanCa__ID__74794A92]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_PhanCapPheDuyet] DROP CONSTRAINT [DF__NH_DM_PhanCa__ID__74794A92]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_NhaTha__Id__681373AD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_NhaThau] DROP CONSTRAINT [DF__NH_DM_NhaTha__Id__681373AD]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_LoaiTi__ID__65370702]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_LoaiTienTe] DROP CONSTRAINT [DF__NH_DM_LoaiTi__ID__65370702]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_LoaiTa__ID__625A9A57]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_LoaiTaiSan] DROP CONSTRAINT [DF__NH_DM_LoaiTa__ID__625A9A57]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_Loa__iID_L__5F7E2DAC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_LoaiHopDong] DROP CONSTRAINT [DF__NH_DM_Loa__iID_L__5F7E2DAC]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_LoaiCo__ID__5CA1C101]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_LoaiCongTrinh] DROP CONSTRAINT [DF__NH_DM_LoaiCo__ID__5CA1C101]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_HinhTh__ID__59C55456]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_HinhThucChonNhaThau] DROP CONSTRAINT [DF__NH_DM_HinhTh__ID__59C55456]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_Chi__bHang__540C7B00]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_ChiPhi] DROP CONSTRAINT [DF__NH_DM_Chi__bHang__540C7B00]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NH_DM_Chi__iID_C__531856C7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NH_DM_ChiPhi] DROP CONSTRAINT [DF__NH_DM_Chi__iID_C__531856C7]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__DM_ChuDau__iID_D__2D27B809]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DM_ChuDauTu] DROP CONSTRAINT [DF__DM_ChuDau__iID_D__2D27B809]
END
GO
/****** Object:  Table [dbo].[[DM_ChuDauTu]]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DM_ChuDauTu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DM_ChuDauTu](
	[iID_DonVi] [uniqueidentifier] NOT NULL,
	[bHangCha] [bit] NOT NULL,
	[ChiNhanhNuocNgoai] [nvarchar](500) NULL,
	[ChiNhanhTrongNuoc] [nvarchar](500) NULL,
	[dNgaySua] [datetime] NULL,
	[dNgayTao] [datetime] NULL,
	[iID_DonViCha] [uniqueidentifier] NULL,
	[iID_MaDonVi] [nvarchar](50) NULL,
	[iNamLamViec] [int] NULL,
	[iTrangThai] [int] NULL,
	[Loai] [nvarchar](50) NULL,
	[MaSoDVSDNS] [nvarchar](500) NULL,
	[sKyHieu] [nvarchar](20) NULL,
	[sMoTa] [nvarchar](500) NULL,
	[sNguoiSua] [nvarchar](50) NULL,
	[sNguoiTao] [nvarchar](50) NULL,
	[STKNuocNgoai] [nvarchar](500) NULL,
	[STKTrongNuoc] [nvarchar](500) NULL,
	[sTenDonVi] [nvarchar](500) NULL,
 CONSTRAINT [PK_DM_ChuDauTu] PRIMARY KEY CLUSTERED 
(
	[iID_DonVi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[DM_ChuDauTu] ADD  DEFAULT (newid()) FOR [iID_DonVi]
END
GO
/****** Object:  Table [dbo].[[NH_DM_ChiPhi]]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_ChiPhi]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_ChiPhi](
	[iID_ChiPhi] [uniqueidentifier] NOT NULL,
	[bHangCha] [bit] NULL,
	[dNgayTao] [datetime] NULL,
	[iThuTu] [int] NOT NULL,
	[sID_MaNguoiDungTao] [nvarchar](200) NULL,
	[sMaChiPhi] [nvarchar](50) NOT NULL,
	[sMoTa] [nvarchar](500) NULL,
	[sTenChiPhi] [nvarchar](300) NOT NULL,
	[sTenVietTat] [nvarchar](100) NULL,
 CONSTRAINT [PK_NH_DM_ChiPhi] PRIMARY KEY CLUSTERED 
(
	[iID_ChiPhi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_ChiPhi] ADD  DEFAULT (newid()) FOR [iID_ChiPhi]
ALTER TABLE [dbo].[NH_DM_ChiPhi] ADD  DEFAULT ((0)) FOR [bHangCha]
END
GO
/****** Object:  Table [dbo].[NH_DM_PhuongThucChonNhaThau]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_PhuongThucChonNhaThau]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_PhuongThucChonNhaThau](
	[ID] [uniqueidentifier] NOT NULL,
	[iThuTu] [int] NULL,
	[sMaPhuongThucChonNhaThau] [nvarchar](max) NULL,
	[sMoTa] [nvarchar](max) NULL,
	[sTenPhuongThucChonNhaThau] [nvarchar](max) NULL,
	[sTenVietTat] [nvarchar](max) NULL,
 CONSTRAINT [PK_NH_DM_PhuongThucChonNhaThau] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_PhuongThucChonNhaThau] ADD  DEFAULT (newid()) FOR [ID]
END
GO
/****** Object:  Table [dbo].[NH_DM_PhanCapPheDuyet]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_PhanCapPheDuyet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_PhanCapPheDuyet](
	[ID] [uniqueidentifier] NOT NULL,
	[bActive] [bit] NULL,
	[iThuTu] [int] NULL,
	[sMa] [nvarchar](100) NULL,
	[sMoTa] [ntext] NULL,
	[sTen] [nvarchar](300) NULL,
	[sTenVietTat] [nvarchar](300) NULL,
 CONSTRAINT [PK_NH_DM_PhanCapPheDuyet] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_PhanCapPheDuyet] ADD  DEFAULT (newid()) FOR [ID]
END
GO
/****** Object:  Table [dbo].[NH_DM_NhaThau]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_NhaThau]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_NhaThau](
	[Id] [uniqueidentifier] NOT NULL,
	[dNgayCapCMND] [datetime2](7) NULL,
	[iLoai] [int] NULL,
	[sChucVu] [nvarchar](max) NULL,
	[sDaiDien] [nvarchar](max) NULL,
	[sDiaChi] [nvarchar](max) NULL,
	[sDienThoai] [nvarchar](max) NULL,
	[sDienThoaiLienHe] [nvarchar](max) NULL,
	[sEmail] [nvarchar](max) NULL,
	[sFax] [nvarchar](max) NULL,
	[sMaNganHang] [nvarchar](max) NULL,
	[sMaNhaThau] [nvarchar](max) NULL,
	[sMaSoThue] [nvarchar](max) NULL,
	[sNganHang] [nvarchar](max) NULL,
	[sNguoiLienHe] [nvarchar](max) NULL,
	[sNoiCapCMND] [nvarchar](max) NULL,
	[sSoCMND] [nvarchar](max) NULL,
	[sSoTaiKhoan] [nvarchar](max) NULL,
	[sTenNhaThau] [nvarchar](max) NULL,
	[sWebsite] [nvarchar](max) NULL,
	[dNgayTao] [datetime2](7) NULL,
	[sNguoiTao] [nvarchar](max) NULL,
 CONSTRAINT [PK_NH_DM_NhaThau] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_NhaThau] ADD  DEFAULT (newid()) FOR [Id]
END
GO
/****** Object:  Table [dbo].[NH_DM_LoaiTienTe]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_LoaiTienTe]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_LoaiTienTe](
	[ID] [uniqueidentifier] NOT NULL,
	[sMaTienTe] [nvarchar](10) NULL,
	[sMoTaChiTiet] [nvarchar](max) NULL,
	[sTenTienTe] [nvarchar](255) NULL,
 CONSTRAINT [PK_NH_DM_LoaiTienTe] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_LoaiTienTe] ADD  DEFAULT (newid()) FOR [ID]
END
GO
/****** Object:  Table [dbo].[NH_DM_LoaiTaiSan]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_LoaiTaiSan]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_LoaiTaiSan](
	[ID] [uniqueidentifier] NOT NULL,
	[sMaLoaiTaiSan] [nvarchar](max) NULL,
	[sMoTa] [nvarchar](max) NULL,
	[sTenLoaiTaiSan] [nvarchar](max) NULL,
	[DNgayTao] [datetime2](7) NULL,
	[SNguoiTao] [nvarchar](max) NULL,
 CONSTRAINT [PK_NH_DM_LoaiTaiSan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_LoaiTaiSan] ADD  DEFAULT (newid()) FOR [ID]
END
GO
/****** Object:  Table [dbo].[NH_DM_LoaiHopDong]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_LoaiHopDong]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_LoaiHopDong](
	[iID_LoaiHopDongID] [uniqueidentifier] NOT NULL,
	[iThuTu] [int] NULL,
	[sMaLoaiHopDong] [nvarchar](max) NULL,
	[sMoTa] [nvarchar](max) NULL,
	[sTenLoaiHopDong] [nvarchar](max) NULL,
	[sTenVietTat] [nvarchar](max) NULL,
 CONSTRAINT [PK_NH_DM_LoaiHopDong] PRIMARY KEY CLUSTERED 
(
	[iID_LoaiHopDongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_LoaiHopDong] ADD  DEFAULT (newid()) FOR [iID_LoaiHopDongID]
END
GO
/****** Object:  Table [dbo].[NH_DM_LoaiCongTrinh]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_LoaiCongTrinh]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_LoaiCongTrinh](
	[ID] [uniqueidentifier] NOT NULL,
	[bActive] [bit] NULL,
	[dNgaySua] [datetime] NULL,
	[dNgayTao] [datetime] NULL,
	[iID_Parent] [uniqueidentifier] NULL,
	[iSoLanSua] [int] NULL,
	[iThuTu] [int] NULL,
	[K] [nvarchar](200) NULL,
	[L] [nvarchar](200) NULL,
	[LNS] [nvarchar](200) NULL,
	[M] [nvarchar](200) NULL,
	[NG] [nvarchar](200) NULL,
	[sID_MaNguoiDungSua] [nvarchar](200) NULL,
	[sID_MaNguoiDungTao] [nvarchar](200) NULL,
	[sIPSua] [nvarchar](20) NULL,
	[sMaLoaiCongTrinh] [nvarchar](50) NULL,
	[sMoTa] [nvarchar](max) NULL,
	[sTenLoaiCongTrinh] [nvarchar](300) NULL,
	[sTenVietTat] [nvarchar](100) NULL,
	[TM] [nvarchar](200) NULL,
	[TNG] [nvarchar](200) NULL,
	[TNG1] [nvarchar](200) NULL,
	[TNG2] [nvarchar](200) NULL,
	[TNG3] [nvarchar](200) NULL,
	[TTM] [nvarchar](200) NULL,
	[XAUNOIMA] [nvarchar](200) NULL,
 CONSTRAINT [PK_NH_DM_LoaiCongTrinh] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_LoaiCongTrinh] ADD  DEFAULT (newid()) FOR [ID]
END
GO
/****** Object:  Table [dbo].[NH_DM_HinhThucChonNhaThau]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_HinhThucChonNhaThau]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_HinhThucChonNhaThau](
	[ID] [uniqueidentifier] NOT NULL,
	[iThuTu] [int] NULL,
	[sMaHinhThucChonNhaThau] [nvarchar](max) NULL,
	[sMoTa] [nvarchar](max) NULL,
	[sTenHinhThucChonNhaThau] [nvarchar](max) NULL,
	[sTenVietTat] [nvarchar](max) NULL,
 CONSTRAINT [PK_NH_DM_HinhThucChonNhaThau] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_HinhThucChonNhaThau] ADD  DEFAULT (newid()) FOR [ID]
END
GO
/****** Object:  Table [dbo].[[NH_DM_TiGia]]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_TiGia]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_TiGia](
	[ID] [uniqueidentifier] NOT NULL,
	[dNgaySua] [datetime2](7) NULL,
	[dNgayTao] [datetime2](7) NULL,
	[iID_TienTeGocID] [uniqueidentifier] NULL,
	[sMaTiGia] [nvarchar](max) NULL,
	[sMaTienTeGoc] [nvarchar](max) NULL,
	[sMoTaTiGia] [nvarchar](max) NULL,
	[sNguoiSua] [nvarchar](max) NULL,
	[sNguoiTao] [nvarchar](max) NULL,
	[sTenTiGia] [nvarchar](max) NULL,
	[dNgayBanHanhKBNN] [datetime2](7) NULL,
	[iNamTiGia] [int] NULL,
	[iThangTiGia] [int] NULL,
	[sSoThongBaoKBNN] [nvarchar](max) NULL,
 CONSTRAINT [PK_NH_DM_TiGia] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_TiGia] ADD  DEFAULT (newid()) FOR [ID]
END
GO
/****** Object:  Table [dbo].[[NH_DM_TiGia_ChiTiet]]    Script Date: 11/7/2023 3:49:17 PM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NH_DM_TiGia_ChiTiet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NH_DM_TiGia_ChiTiet](
	[ID] [uniqueidentifier] NOT NULL,
	[fTiGia] [float] NULL,
	[iID_TiGiaID] [uniqueidentifier] NOT NULL,
	[iID_TienTeID] [uniqueidentifier] NOT NULL,
	[sMaTienTeQuyDoi] [nvarchar](max) NULL,
 CONSTRAINT [PK_NH_DM_TiGia_ChiTiet] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[NH_DM_TiGia_ChiTiet] ADD  DEFAULT (newid()) FOR [ID]
END
GO

/**
*Check data and Insert master data
**/

/****** Object:  Table [dbo].[[DM_ChuDauTu]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuDauTu] where iID_DonVi = 'c382d1a6-8bdb-487a-89b9-0ab7b060d94a')
BEGIN 
INSERT [dbo].[DM_ChuDauTu] ([iID_DonVi], [bHangCha], [ChiNhanhNuocNgoai], [ChiNhanhTrongNuoc], [dNgaySua], [dNgayTao], [iID_DonViCha], [iID_MaDonVi], [iNamLamViec], [iTrangThai], [Loai], [MaSoDVSDNS], [sKyHieu], [sMoTa], [sNguoiSua], [sNguoiTao], [STKNuocNgoai], [STKTrongNuoc], [sTenDonVi]) VALUES (N'c382d1a6-8bdb-487a-89b9-0ab7b060d94a', 0, NULL, NULL, NULL, NULL, NULL, N'000', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'DV cấp 2')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuDauTu] where iID_DonVi = 'a35d8af7-a12b-4961-b99d-391ff7253a58')
BEGIN 
INSERT [dbo].[DM_ChuDauTu] ([iID_DonVi], [bHangCha], [ChiNhanhNuocNgoai], [ChiNhanhTrongNuoc], [dNgaySua], [dNgayTao], [iID_DonViCha], [iID_MaDonVi], [iNamLamViec], [iTrangThai], [Loai], [MaSoDVSDNS], [sKyHieu], [sMoTa], [sNguoiSua], [sNguoiTao], [STKNuocNgoai], [STKTrongNuoc], [sTenDonVi]) VALUES (N'a35d8af7-a12b-4961-b99d-391ff7253a58', 0, NULL, NULL, NULL, NULL, NULL, N'003', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Cấp 3 - DV 3')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuDauTu] where iID_DonVi = 'ee7d016a-3856-472d-97d4-52d58785574d')
BEGIN 
INSERT [dbo].[DM_ChuDauTu] ([iID_DonVi], [bHangCha], [ChiNhanhNuocNgoai], [ChiNhanhTrongNuoc], [dNgaySua], [dNgayTao], [iID_DonViCha], [iID_MaDonVi], [iNamLamViec], [iTrangThai], [Loai], [MaSoDVSDNS], [sKyHieu], [sMoTa], [sNguoiSua], [sNguoiTao], [STKNuocNgoai], [STKTrongNuoc], [sTenDonVi]) VALUES (N'ee7d016a-3856-472d-97d4-52d58785574d', 0, NULL, NULL, NULL, NULL, NULL, N'002', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Cấp 3 - DV 2')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuDauTu] where iID_DonVi = '9404412b-05c0-4ad9-b732-cc60fb67a238')
BEGIN 
INSERT [dbo].[DM_ChuDauTu] ([iID_DonVi], [bHangCha], [ChiNhanhNuocNgoai], [ChiNhanhTrongNuoc], [dNgaySua], [dNgayTao], [iID_DonViCha], [iID_MaDonVi], [iNamLamViec], [iTrangThai], [Loai], [MaSoDVSDNS], [sKyHieu], [sMoTa], [sNguoiSua], [sNguoiTao], [STKNuocNgoai], [STKTrongNuoc], [sTenDonVi]) VALUES (N'9404412b-05c0-4ad9-b732-cc60fb67a238', 0, NULL, NULL, NULL, NULL, NULL, N'001', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Cấp 3 - DV 1')
END
GO

/****** Object:  Table [dbo].[[[NH_DM_ChiPhi]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_ChiPhi] where iID_ChiPhi = '2aaeb76a-119e-4997-8ba7-354e83e8fd33')
BEGIN 
INSERT [dbo].[NH_DM_ChiPhi] ([iID_ChiPhi], [bHangCha], [dNgayTao], [iThuTu], [sID_MaNguoiDungTao], [sMaChiPhi], [sMoTa], [sTenChiPhi], [sTenVietTat]) VALUES (N'2aaeb76a-119e-4997-8ba7-354e83e8fd33', 0, CAST(N'2023-11-02T08:46:51.647' AS DateTime), 0, N'admin', N'GTHHNK', NULL, N'Giá trị hàng hóa nhập khẩu', N'GTHHNK')
END

GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_ChiPhi] where iID_ChiPhi = '65e15633-9016-4dc7-a998-87bba1621efe')
BEGIN 
INSERT [dbo].[NH_DM_ChiPhi] ([iID_ChiPhi], [bHangCha], [dNgayTao], [iThuTu], [sID_MaNguoiDungTao], [sMaChiPhi], [sMoTa], [sTenChiPhi], [sTenVietTat]) VALUES (N'65e15633-9016-4dc7-a998-87bba1621efe', 0, CAST(N'2023-11-02T08:46:51.647' AS DateTime), 0, N'admin', N'CPTHTN', NULL, N'Chi phí thực hiện trong nước', N'CPTHTN')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_ChiPhi] where iID_ChiPhi = 'b4d411b7-4d14-418b-90be-900a1ba3abf5')
BEGIN 
INSERT [dbo].[NH_DM_ChiPhi] ([iID_ChiPhi], [bHangCha], [dNgayTao], [iThuTu], [sID_MaNguoiDungTao], [sMaChiPhi], [sMoTa], [sTenChiPhi], [sTenVietTat]) VALUES (N'b4d411b7-4d14-418b-90be-900a1ba3abf5', 0, CAST(N'2023-11-07T15:06:31.077' AS DateTime), 0, N'admin', N'CPDVDR', NULL, N'Chi phí đoàn vào đoàn ra', N'CPDVDR')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_ChiPhi] where iID_ChiPhi = '97ae1624-67d8-4a5d-9014-90b3c3ea6ece')
BEGIN 
INSERT [dbo].[NH_DM_ChiPhi] ([iID_ChiPhi], [bHangCha], [dNgayTao], [iThuTu], [sID_MaNguoiDungTao], [sMaChiPhi], [sMoTa], [sTenChiPhi], [sTenVietTat]) VALUES (N'97ae1624-67d8-4a5d-9014-90b3c3ea6ece', 0, CAST(N'2023-11-07T15:06:31.077' AS DateTime), 0, N'admin', N'CPTV', NULL, N'Chi phí tư vấn', N'CPTV')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_ChiPhi] where iID_ChiPhi = 'e842126c-2e80-4627-96e2-c1ce049aff3a')
BEGIN 
INSERT [dbo].[NH_DM_ChiPhi] ([iID_ChiPhi], [bHangCha], [dNgayTao], [iThuTu], [sID_MaNguoiDungTao], [sMaChiPhi], [sMoTa], [sTenChiPhi], [sTenVietTat]) VALUES (N'e842126c-2e80-4627-96e2-c1ce049aff3a', 0, CAST(N'2023-11-02T08:46:51.647' AS DateTime), 0, N'admin', N'CPQLDA', NULL, N'Chi phí quản lý dự án', N'CPQLDA')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_ChiPhi] where iID_ChiPhi = '92e934f0-f3db-4300-a83e-c4079e5d3247')
BEGIN 
INSERT [dbo].[NH_DM_ChiPhi] ([iID_ChiPhi], [bHangCha], [dNgayTao], [iThuTu], [sID_MaNguoiDungTao], [sMaChiPhi], [sMoTa], [sTenChiPhi], [sTenVietTat]) VALUES (N'92e934f0-f3db-4300-a83e-c4079e5d3247', 0, CAST(N'2023-11-07T15:06:31.077' AS DateTime), 0, N'admin', N'CPTT', NULL, N'Chi phí thẩm tra', N'CPTT')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_ChiPhi] where iID_ChiPhi = '9c8878fb-b1c0-4ea4-b583-d388e3baca30')
BEGIN 
INSERT [dbo].[NH_DM_ChiPhi] ([iID_ChiPhi], [bHangCha], [dNgayTao], [iThuTu], [sID_MaNguoiDungTao], [sMaChiPhi], [sMoTa], [sTenChiPhi], [sTenVietTat]) VALUES (N'9c8878fb-b1c0-4ea4-b583-d388e3baca30', 0, CAST(N'2023-11-02T08:46:51.647' AS DateTime), 0, N'admin', N'CPUT', NULL, N'Chi phí uỷ thác', N'CPUT')
END
GO

/****** Object:  Table [dbo].[[[NH_DM_HinhThucChonNhaThau]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_HinhThucChonNhaThau] where ID = 'a347fa26-0d85-4929-8b41-141d16ec9363')
BEGIN 
INSERT [dbo].[NH_DM_HinhThucChonNhaThau] ([ID], [iThuTu], [sMaHinhThucChonNhaThau], [sMoTa], [sTenHinhThucChonNhaThau], [sTenVietTat]) VALUES (N'a347fa26-0d85-4929-8b41-141d16ec9363', NULL, N'DTRR', NULL, N'Đấu thầu rộng rãi', N'DTRR')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_HinhThucChonNhaThau] where ID = '94b28a25-4406-4b57-84f9-149cc346aed9')
BEGIN 
INSERT [dbo].[NH_DM_HinhThucChonNhaThau] ([ID], [iThuTu], [sMaHinhThucChonNhaThau], [sMoTa], [sTenHinhThucChonNhaThau], [sTenVietTat]) VALUES (N'94b28a25-4406-4b57-84f9-149cc346aed9', NULL, N'DTHC', NULL, N'Đấu thầu hạn chế', N'DTHC')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_HinhThucChonNhaThau] where ID = '41f1d21f-cde7-49f2-bb44-16c3ceab7ffe')
BEGIN 
INSERT [dbo].[NH_DM_HinhThucChonNhaThau] ([ID], [iThuTu], [sMaHinhThucChonNhaThau], [sMoTa], [sTenHinhThucChonNhaThau], [sTenVietTat]) VALUES (N'41f1d21f-cde7-49f2-bb44-16c3ceab7ffe', NULL, N'CHCT', NULL, N'Chào hàng cạnh tranh', N'CHCT')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_HinhThucChonNhaThau] where ID = '97d53b27-f711-42c8-ad3f-194ad4cd480a')
BEGIN 
INSERT [dbo].[NH_DM_HinhThucChonNhaThau] ([ID], [iThuTu], [sMaHinhThucChonNhaThau], [sMoTa], [sTenHinhThucChonNhaThau], [sTenVietTat]) VALUES (N'97d53b27-f711-42c8-ad3f-194ad4cd480a', NULL, N'TTH', NULL, N'Tự thực hiện', N'TTH')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_HinhThucChonNhaThau] where ID = '20d663c3-1edd-4370-910f-7448e2c00086')
BEGIN 
INSERT [dbo].[NH_DM_HinhThucChonNhaThau] ([ID], [iThuTu], [sMaHinhThucChonNhaThau], [sMoTa], [sTenHinhThucChonNhaThau], [sTenVietTat]) VALUES (N'20d663c3-1edd-4370-910f-7448e2c00086', NULL, N'LCNTTTHDB', NULL, N'Lựa chọn nhà thầu trong trường hợp đặc biệt', N'LCNTTTHDB')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_HinhThucChonNhaThau] where ID = '6db0c59c-4e58-46b6-a4ec-8ee07d3ecddf')
BEGIN 
INSERT [dbo].[NH_DM_HinhThucChonNhaThau] ([ID], [iThuTu], [sMaHinhThucChonNhaThau], [sMoTa], [sTenHinhThucChonNhaThau], [sTenVietTat]) VALUES (N'6db0c59c-4e58-46b6-a4ec-8ee07d3ecddf', NULL, N'MSTT', NULL, N'Mua sắm trực tiếp', N'MSTT')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_HinhThucChonNhaThau] where ID = 'f25efafe-4ca2-4f1b-bc66-912f119861d5')
BEGIN 
INSERT [dbo].[NH_DM_HinhThucChonNhaThau] ([ID], [iThuTu], [sMaHinhThucChonNhaThau], [sMoTa], [sTenHinhThucChonNhaThau], [sTenVietTat]) VALUES (N'f25efafe-4ca2-4f1b-bc66-912f119861d5', NULL, N'CDT', NULL, N'Chỉ định thầu', N'CDT')
END
GO

/****** Object:  Table [dbo].[[[[NH_DM_LoaiCongTrinh]]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiCongTrinh] where ID = '32ece8f1-459f-4119-b89f-197e9a21b1f1')
BEGIN 
INSERT [dbo].[NH_DM_LoaiCongTrinh] ([ID], [bActive], [dNgaySua], [dNgayTao], [iID_Parent], [iSoLanSua], [iThuTu], [K], [L], [LNS], [M], [NG], [sID_MaNguoiDungSua], [sID_MaNguoiDungTao], [sIPSua], [sMaLoaiCongTrinh], [sMoTa], [sTenLoaiCongTrinh], [sTenVietTat], [TM], [TNG], [TNG1], [TNG2], [TNG3], [TTM], [XAUNOIMA]) VALUES (N'32ece8f1-459f-4119-b89f-197e9a21b1f1', NULL, CAST(N'2023-11-02T09:00:51.927' AS DateTime), CAST(N'2023-11-02T08:53:54.957' AS DateTime), N'106f10b0-831b-4e66-a0bb-7d68c6577fe5', NULL, 8, NULL, NULL, NULL, NULL, NULL, N'admin', N'admin', NULL, N'2.4', NULL, N'Công trình thông tin', N'CTTT', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiCongTrinh] where ID = '4836a439-784d-4a94-abfd-22682f0d24a3')
BEGIN 
INSERT [dbo].[NH_DM_LoaiCongTrinh] ([ID], [bActive], [dNgaySua], [dNgayTao], [iID_Parent], [iSoLanSua], [iThuTu], [K], [L], [LNS], [M], [NG], [sID_MaNguoiDungSua], [sID_MaNguoiDungTao], [sIPSua], [sMaLoaiCongTrinh], [sMoTa], [sTenLoaiCongTrinh], [sTenVietTat], [TM], [TNG], [TNG1], [TNG2], [TNG3], [TTM], [XAUNOIMA]) VALUES (N'4836a439-784d-4a94-abfd-22682f0d24a3', NULL, CAST(N'2023-11-02T09:01:07.053' AS DateTime), CAST(N'2023-11-02T08:51:06.833' AS DateTime), N'2aa30933-a530-43df-a53d-9d42c61489b5', NULL, 3, NULL, NULL, NULL, NULL, NULL, N'admin', N'admin', NULL, N'1.2', NULL, N'Dự án khác', N'DAK', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiCongTrinh] where ID = 'c411cb88-3d56-4093-a01b-7a77837c501b')
BEGIN 
INSERT [dbo].[NH_DM_LoaiCongTrinh] ([ID], [bActive], [dNgaySua], [dNgayTao], [iID_Parent], [iSoLanSua], [iThuTu], [K], [L], [LNS], [M], [NG], [sID_MaNguoiDungSua], [sID_MaNguoiDungTao], [sIPSua], [sMaLoaiCongTrinh], [sMoTa], [sTenLoaiCongTrinh], [sTenVietTat], [TM], [TNG], [TNG1], [TNG2], [TNG3], [TTM], [XAUNOIMA]) VALUES (N'c411cb88-3d56-4093-a01b-7a77837c501b', NULL, CAST(N'2023-11-02T09:00:51.927' AS DateTime), CAST(N'2023-11-02T08:53:54.957' AS DateTime), N'106f10b0-831b-4e66-a0bb-7d68c6577fe5', NULL, 5, NULL, NULL, NULL, NULL, NULL, N'admin', N'admin', NULL, N'2.1', NULL, N'Công trình chiến đấu', N'CTCD', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiCongTrinh] where ID = '106f10b0-831b-4e66-a0bb-7d68c6577fe5')
BEGIN 
INSERT [dbo].[NH_DM_LoaiCongTrinh] ([ID], [bActive], [dNgaySua], [dNgayTao], [iID_Parent], [iSoLanSua], [iThuTu], [K], [L], [LNS], [M], [NG], [sID_MaNguoiDungSua], [sID_MaNguoiDungTao], [sIPSua], [sMaLoaiCongTrinh], [sMoTa], [sTenLoaiCongTrinh], [sTenVietTat], [TM], [TNG], [TNG1], [TNG2], [TNG3], [TTM], [XAUNOIMA]) VALUES (N'106f10b0-831b-4e66-a0bb-7d68c6577fe5', NULL, CAST(N'2023-11-02T09:00:51.927' AS DateTime), CAST(N'2023-11-02T08:51:06.833' AS DateTime), NULL, NULL, 4, NULL, NULL, NULL, NULL, NULL, N'admin', N'admin', NULL, N'2', NULL, N'Đầu tư có mục tiêu', N'CT2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiCongTrinh] where ID = 'c1418074-37d4-43ca-9745-8972e0ee5d7e')
BEGIN 
INSERT [dbo].[NH_DM_LoaiCongTrinh] ([ID], [bActive], [dNgaySua], [dNgayTao], [iID_Parent], [iSoLanSua], [iThuTu], [K], [L], [LNS], [M], [NG], [sID_MaNguoiDungSua], [sID_MaNguoiDungTao], [sIPSua], [sMaLoaiCongTrinh], [sMoTa], [sTenLoaiCongTrinh], [sTenVietTat], [TM], [TNG], [TNG1], [TNG2], [TNG3], [TTM], [XAUNOIMA]) VALUES (N'c1418074-37d4-43ca-9745-8972e0ee5d7e', NULL, CAST(N'2023-11-02T09:01:07.053' AS DateTime), CAST(N'2023-11-02T08:51:06.833' AS DateTime), N'2aa30933-a530-43df-a53d-9d42c61489b5', NULL, 2, NULL, NULL, NULL, NULL, NULL, N'admin', N'admin', NULL, N'1.1', NULL, N'Điều tra cơ bản', N'DTCB', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiCongTrinh] where ID = '973e3754-46c7-471d-a7be-9c30a20d8bf3')
BEGIN 
INSERT [dbo].[NH_DM_LoaiCongTrinh] ([ID], [bActive], [dNgaySua], [dNgayTao], [iID_Parent], [iSoLanSua], [iThuTu], [K], [L], [LNS], [M], [NG], [sID_MaNguoiDungSua], [sID_MaNguoiDungTao], [sIPSua], [sMaLoaiCongTrinh], [sMoTa], [sTenLoaiCongTrinh], [sTenVietTat], [TM], [TNG], [TNG1], [TNG2], [TNG3], [TTM], [XAUNOIMA]) VALUES (N'973e3754-46c7-471d-a7be-9c30a20d8bf3', NULL, CAST(N'2023-11-02T09:00:51.927' AS DateTime), CAST(N'2023-11-02T08:53:54.957' AS DateTime), N'106f10b0-831b-4e66-a0bb-7d68c6577fe5', NULL, 9, NULL, NULL, NULL, NULL, NULL, N'admin', N'admin', NULL, N'2.5', NULL, N'Công trình văn hóa', N'CTVH', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiCongTrinh] where ID = '2aa30933-a530-43df-a53d-9d42c61489b5')
BEGIN 
INSERT [dbo].[NH_DM_LoaiCongTrinh] ([ID], [bActive], [dNgaySua], [dNgayTao], [iID_Parent], [iSoLanSua], [iThuTu], [K], [L], [LNS], [M], [NG], [sID_MaNguoiDungSua], [sID_MaNguoiDungTao], [sIPSua], [sMaLoaiCongTrinh], [sMoTa], [sTenLoaiCongTrinh], [sTenVietTat], [TM], [TNG], [TNG1], [TNG2], [TNG3], [TTM], [XAUNOIMA]) VALUES (N'2aa30933-a530-43df-a53d-9d42c61489b5', NULL, NULL, CAST(N'2023-11-02T08:51:06.833' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'admin', NULL, N'1', NULL, N'Công trình phổ thông', N'CT1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiCongTrinh] where ID = '316aa799-34e8-4ab1-90f8-bf1777025c54')
BEGIN 
INSERT [dbo].[NH_DM_LoaiCongTrinh] ([ID], [bActive], [dNgaySua], [dNgayTao], [iID_Parent], [iSoLanSua], [iThuTu], [K], [L], [LNS], [M], [NG], [sID_MaNguoiDungSua], [sID_MaNguoiDungTao], [sIPSua], [sMaLoaiCongTrinh], [sMoTa], [sTenLoaiCongTrinh], [sTenVietTat], [TM], [TNG], [TNG1], [TNG2], [TNG3], [TTM], [XAUNOIMA]) VALUES (N'316aa799-34e8-4ab1-90f8-bf1777025c54', NULL, CAST(N'2023-11-02T09:00:51.927' AS DateTime), CAST(N'2023-11-02T08:53:54.957' AS DateTime), N'106f10b0-831b-4e66-a0bb-7d68c6577fe5', NULL, 6, NULL, NULL, NULL, NULL, NULL, N'admin', N'admin', NULL, N'2.2', NULL, N'Công trình sân bay', N'CTSB', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiCongTrinh] where ID = 'e60693e0-b9b3-41d2-a521-e482a36b4685')
BEGIN 
INSERT [dbo].[NH_DM_LoaiCongTrinh] ([ID], [bActive], [dNgaySua], [dNgayTao], [iID_Parent], [iSoLanSua], [iThuTu], [K], [L], [LNS], [M], [NG], [sID_MaNguoiDungSua], [sID_MaNguoiDungTao], [sIPSua], [sMaLoaiCongTrinh], [sMoTa], [sTenLoaiCongTrinh], [sTenVietTat], [TM], [TNG], [TNG1], [TNG2], [TNG3], [TTM], [XAUNOIMA]) VALUES (N'e60693e0-b9b3-41d2-a521-e482a36b4685', NULL, CAST(N'2023-11-02T09:00:51.927' AS DateTime), CAST(N'2023-11-02T08:53:54.957' AS DateTime), N'106f10b0-831b-4e66-a0bb-7d68c6577fe5', NULL, 7, NULL, NULL, NULL, NULL, NULL, N'admin', N'admin', NULL, N'2.3', NULL, N'Công trình Trường bắn, thao trường huấn luyện', N'CTTBTTHL', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

GO
/****** Object:  Table [dbo].[[[[NH_DM_LoaiHopDong]]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiHopDong] where [iID_LoaiHopDongID] = 'cbeb82fb-0512-4075-b93f-074a9178dcae')
BEGIN 
INSERT [dbo].[NH_DM_LoaiHopDong] ([iID_LoaiHopDongID], [iThuTu], [sMaLoaiHopDong], [sMoTa], [sTenLoaiHopDong], [sTenVietTat]) VALUES (N'cbeb82fb-0512-4075-b93f-074a9178dcae', NULL, N'DTDGCD', NULL, N'Hợp đồng theo đơn giá cố định', N'DTDGCD')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiHopDong] where [iID_LoaiHopDongID] = 'e66de074-0486-4f7e-949b-1da434a3f560')
BEGIN 
INSERT [dbo].[NH_DM_LoaiHopDong] ([iID_LoaiHopDongID], [iThuTu], [sMaLoaiHopDong], [sMoTa], [sTenLoaiHopDong], [sTenVietTat]) VALUES (N'e66de074-0486-4f7e-949b-1da434a3f560', NULL, N'HDTV', NULL, N'Hợp đồng tư vấn', N'HDTV')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiHopDong] where [iID_LoaiHopDongID] = 'd2ecc2bb-5308-438c-9093-23ab5d45844d')
BEGIN 
INSERT [dbo].[NH_DM_LoaiHopDong] ([iID_LoaiHopDongID], [iThuTu], [sMaLoaiHopDong], [sMoTa], [sTenLoaiHopDong], [sTenVietTat]) VALUES (N'd2ecc2bb-5308-438c-9093-23ab5d45844d', NULL, N'HDTDGDC', NULL, N'Hợp đồng theo đơn giá điều chỉnh', N'HDTDGDC')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiHopDong] where [iID_LoaiHopDongID] = 'cbaf441f-c768-4ea3-a122-3fbbe311e9d4')
BEGIN 
INSERT [dbo].[NH_DM_LoaiHopDong] ([iID_LoaiHopDongID], [iThuTu], [sMaLoaiHopDong], [sMoTa], [sTenLoaiHopDong], [sTenVietTat]) VALUES (N'cbaf441f-c768-4ea3-a122-3fbbe311e9d4', NULL, N'HDTC', NULL, N'Hợp đồng thi công', N'HDTC')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiHopDong] where [iID_LoaiHopDongID] = 'db7d4982-a8f3-4d3c-afd3-c4fc4edb7a16')
BEGIN 
INSERT [dbo].[NH_DM_LoaiHopDong] ([iID_LoaiHopDongID], [iThuTu], [sMaLoaiHopDong], [sMoTa], [sTenLoaiHopDong], [sTenVietTat]) VALUES (N'db7d4982-a8f3-4d3c-afd3-c4fc4edb7a16', NULL, N'HDMB', NULL, N'Hợp đồng bảo hành và bảo trì', N'HDMB')
END
GO
/****** Object:  Table [dbo].[[[[NH_DM_LoaiTaiSan]]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTaiSan] where [ID] = 'fcab2a96-c438-45b1-89af-1874ac539d8c')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTaiSan] ([ID], [sMaLoaiTaiSan], [sMoTa], [sTenLoaiTaiSan], [DNgayTao], [SNguoiTao]) VALUES (N'fcab2a96-c438-45b1-89af-1874ac539d8c', N'TS1', NULL, N'Máy bay', CAST(N'2023-11-02T08:55:57.3938953' AS DateTime2), N'admin')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTaiSan] where [ID] = '4977c594-14cb-4146-9530-19eda25fe45f')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTaiSan] ([ID], [sMaLoaiTaiSan], [sMoTa], [sTenLoaiTaiSan], [DNgayTao], [SNguoiTao]) VALUES (N'4977c594-14cb-4146-9530-19eda25fe45f', N'TS3', NULL, N'Súng', CAST(N'2023-11-02T08:55:57.3938953' AS DateTime2), N'admin')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTaiSan] where [ID] = '809b1f2d-8c79-4728-8f16-42de390a308f')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTaiSan] ([ID], [sMaLoaiTaiSan], [sMoTa], [sTenLoaiTaiSan], [DNgayTao], [SNguoiTao]) VALUES (N'809b1f2d-8c79-4728-8f16-42de390a308f', N'TS4', NULL, N'Tàu', CAST(N'2023-11-02T08:55:57.3938953' AS DateTime2), N'admin')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTaiSan] where [ID] = 'aeea7c87-762d-4644-9791-961118f6564d')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTaiSan] ([ID], [sMaLoaiTaiSan], [sMoTa], [sTenLoaiTaiSan], [DNgayTao], [SNguoiTao]) VALUES (N'aeea7c87-762d-4644-9791-961118f6564d', N'TS2', NULL, N'Ô tô', CAST(N'2023-11-02T08:55:57.3938953' AS DateTime2), N'admin')
END
GO
/****** Object:  Table [dbo].[[[[NH_DM_LoaiTienTe]]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = '2784508d-3cb2-4cd2-aed9-07fda8c38e89')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'2784508d-3cb2-4cd2-aed9-07fda8c38e89', N'USD', NULL, N'Đô la Hoa Kỳ')
END

GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = '34f75937-439b-4d8b-81a1-1966d9817e68')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'34f75937-439b-4d8b-81a1-1966d9817e68', N'VND', NULL, N'Việt Nam Đồng')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = '37371fdb-b691-4915-b03e-6b391e35190a')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'37371fdb-b691-4915-b03e-6b391e35190a', N'CAD', NULL, N'Đô la Canada')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = 'e924d3fd-9955-4d9d-9074-9cde04dc7b5c')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'e924d3fd-9955-4d9d-9074-9cde04dc7b5c', N'RUB', NULL, N'Rúp Nga')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = 'b349e0bf-fb1c-4275-a3b4-9f144e5d4eb9')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'b349e0bf-fb1c-4275-a3b4-9f144e5d4eb9', N'EUR', NULL, N'Euro')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = '188fedf9-3557-4323-a21b-a071fcd2628d')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'188fedf9-3557-4323-a21b-a071fcd2628d', N'AUD', NULL, N'Đô la Úc')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = '102308b2-d7c9-4ead-8fde-b603368da6cb')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'102308b2-d7c9-4ead-8fde-b603368da6cb', N'NZD', NULL, N'Đô la New Zealand')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = '771671c3-59c1-4cce-859f-dbbff66bd652')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'771671c3-59c1-4cce-859f-dbbff66bd652', N'JPY', NULL, N'Yên Nhật Bản')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = '19a9ec84-412d-451c-b868-e7204aff01d9')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'19a9ec84-412d-451c-b868-e7204aff01d9', N'CHF', NULL, N'Franc Thụy Sỹ')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = 'f1106fcd-1946-432f-8108-eb8cc3493708')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'f1106fcd-1946-432f-8108-eb8cc3493708', N'CNY', NULL, N'Nhân dân tệ Trung Quốc')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_LoaiTienTe] where [ID] = 'b1634662-0926-48a2-975b-f6c21a875ba6')
BEGIN 
INSERT [dbo].[NH_DM_LoaiTienTe] ([ID], [sMaTienTe], [sMoTaChiTiet], [sTenTienTe]) VALUES (N'b1634662-0926-48a2-975b-f6c21a875ba6', N'JBP', NULL, N'Bảng Anh')
END
GO

/****** Object:  Table [dbo].[[[[NH_DM_NhaThau]]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_NhaThau] where [Id] = '215371e5-58ae-4d94-910a-7420e13fff14')
BEGIN 
INSERT [dbo].[NH_DM_NhaThau] ([Id], [dNgayCapCMND], [iLoai], [sChucVu], [sDaiDien], [sDiaChi], [sDienThoai], [sDienThoaiLienHe], [sEmail], [sFax], [sMaNganHang], [sMaNhaThau], [sMaSoThue], [sNganHang], [sNguoiLienHe], [sNoiCapCMND], [sSoCMND], [sSoTaiKhoan], [sTenNhaThau], [sWebsite], [dNgayTao], [sNguoiTao]) VALUES (N'215371e5-58ae-4d94-910a-7420e13fff14', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'NT01', NULL, NULL, NULL, NULL, NULL, NULL, N'Tổng công ty 789', NULL, CAST(N'2023-11-07T15:00:44.7294174' AS DateTime2), N'admin')
END

GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_NhaThau] where [Id] = 'e9a26861-65af-4128-b17d-d3ca8490c594')
BEGIN 
INSERT [dbo].[NH_DM_NhaThau] ([Id], [dNgayCapCMND], [iLoai], [sChucVu], [sDaiDien], [sDiaChi], [sDienThoai], [sDienThoaiLienHe], [sEmail], [sFax], [sMaNganHang], [sMaNhaThau], [sMaSoThue], [sNganHang], [sNguoiLienHe], [sNoiCapCMND], [sSoCMND], [sSoTaiKhoan], [sTenNhaThau], [sWebsite], [dNgayTao], [sNguoiTao]) VALUES (N'e9a26861-65af-4128-b17d-d3ca8490c594', NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'UT01', NULL, NULL, NULL, NULL, NULL, NULL, N'VASUCO', NULL, CAST(N'2023-11-07T15:00:44.7294174' AS DateTime2), N'admin')
END
GO

/****** Object:  Table [dbo].[[[[NH_DM_PhanCapPheDuyet]]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_PhanCapPheDuyet] where [ID] = 'a82b17aa-8fac-4242-9b3b-59dac4d21d32')
BEGIN 
INSERT [dbo].[NH_DM_PhanCapPheDuyet] ([ID], [bActive], [iThuTu], [sMa], [sMoTa], [sTen], [sTenVietTat]) VALUES (N'a82b17aa-8fac-4242-9b3b-59dac4d21d32', 1, 1, N'BUQ', NULL, N'Bộ uỷ quyền', N'BUQ')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_PhanCapPheDuyet] where [ID] = '37173926-4417-4b9d-a93b-712b6c92cde0')
BEGIN 
INSERT [dbo].[NH_DM_PhanCapPheDuyet] ([ID], [bActive], [iThuTu], [sMa], [sMoTa], [sTen], [sTenVietTat]) VALUES (N'37173926-4417-4b9d-a93b-712b6c92cde0', 1, 1, N'BPD', NULL, N'Bộ phê duyệt', N'BPD')
END
GO

/****** Object:  Table [dbo].[[[[NH_DM_PhuongThucChonNhaThau]]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_PhuongThucChonNhaThau] where [ID] = 'f2abfba7-9b5d-4eda-8b63-0752f6266273')
BEGIN 
INSERT [dbo].[NH_DM_PhuongThucChonNhaThau] ([ID], [iThuTu], [sMaPhuongThucChonNhaThau], [sMoTa], [sTenPhuongThucChonNhaThau], [sTenVietTat]) VALUES (N'f2abfba7-9b5d-4eda-8b63-0752f6266273', NULL, N'2GD1THS', NULL, N'2 Giai đoạn 1 túi hồ sơ', N'2GD1THS')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_PhuongThucChonNhaThau] where [ID] = '03949736-ebd7-4f1d-a0cd-31796ff0c8e9')
BEGIN 
INSERT [dbo].[NH_DM_PhuongThucChonNhaThau] ([ID], [iThuTu], [sMaPhuongThucChonNhaThau], [sMoTa], [sTenPhuongThucChonNhaThau], [sTenVietTat]) VALUES (N'03949736-ebd7-4f1d-a0cd-31796ff0c8e9', NULL, N'1GD2THS', NULL, N'1 Giai đoạn 2 túi hồ sơ', N'1GD2THS')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_PhuongThucChonNhaThau] where [ID] = 'dc99e832-853d-4914-96c4-c7fd55f09511')
BEGIN 
INSERT [dbo].[NH_DM_PhuongThucChonNhaThau] ([ID], [iThuTu], [sMaPhuongThucChonNhaThau], [sMoTa], [sTenPhuongThucChonNhaThau], [sTenVietTat]) VALUES (N'dc99e832-853d-4914-96c4-c7fd55f09511', NULL, N'2GD2THS', NULL, N'2 Giai đoạn 2 túi hồ sơ', N'2GD2THS')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_PhuongThucChonNhaThau] where [ID] = '3ebd4bb4-be31-432e-9ac7-cc927f9efc3a')
BEGIN 
INSERT [dbo].[NH_DM_PhuongThucChonNhaThau] ([ID], [iThuTu], [sMaPhuongThucChonNhaThau], [sMoTa], [sTenPhuongThucChonNhaThau], [sTenVietTat]) VALUES (N'3ebd4bb4-be31-432e-9ac7-cc927f9efc3a', NULL, N'1GD1THS', NULL, N'1 Giai đoạn 1 túi hồ sơ', N'1GD1THS')
END

GO

/****** Object:  Table [dbo].[[[[NH_DM_TiGia]]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia] where [ID] = '8d7d5fd6-5ea5-4c65-bd11-1373450e8320')
BEGIN 
INSERT [dbo].[NH_DM_TiGia] ([ID], [dNgaySua], [dNgayTao], [iID_TienTeGocID], [sMaTiGia], [sMaTienTeGoc], [sMoTaTiGia], [sNguoiSua], [sNguoiTao], [sTenTiGia], [dNgayBanHanhKBNN], [iNamTiGia], [iThangTiGia], [sSoThongBaoKBNN]) VALUES (N'8d7d5fd6-5ea5-4c65-bd11-1373450e8320', CAST(N'2023-11-07T14:54:07.7874882' AS DateTime2), CAST(N'2023-11-02T08:44:38.6121457' AS DateTime2), N'2784508d-3cb2-4cd2-aed9-07fda8c38e89', N'TG_T10_23', N'USD', NULL, N'admin', N'admin', N'Tỉ giá tháng 10 năm 2023', CAST(N'2023-11-02T00:00:00.0000000' AS DateTime2), 2023, 10, N'3298/TB-KBNN')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia] where [ID] = '3bdfae73-8c70-404d-ac0b-9544227a27f7')
BEGIN 
INSERT [dbo].[NH_DM_TiGia] ([ID], [dNgaySua], [dNgayTao], [iID_TienTeGocID], [sMaTiGia], [sMaTienTeGoc], [sMoTaTiGia], [sNguoiSua], [sNguoiTao], [sTenTiGia], [dNgayBanHanhKBNN], [iNamTiGia], [iThangTiGia], [sSoThongBaoKBNN]) VALUES (N'3bdfae73-8c70-404d-ac0b-9544227a27f7', NULL, CAST(N'2023-11-07T14:59:52.6187846' AS DateTime2), N'2784508d-3cb2-4cd2-aed9-07fda8c38e89', N'TG_T11_23', N'USD', NULL, NULL, N'admin', N'Tỉ giá tháng 11 năm 2023', CAST(N'2023-11-01T00:00:00.0000000' AS DateTime2), 2023, 11, N'4011/TB-KBNN')
END
GO

/****** Object:  Table [dbo].[[[[NH_DM_TiGia_ChiTiet]]]]    Script Date: 11/15/2023 2:20:17 PM ******/
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = '623a131e-c1b6-4915-8aa0-14e264817fcc')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'623a131e-c1b6-4915-8aa0-14e264817fcc', 10, N'8d7d5fd6-5ea5-4c65-bd11-1373450e8320', N'00000000-0000-0000-0000-000000000000', N'EUR')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = '645e4e5d-5982-4d5f-8f18-37aeb9fce23a')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'645e4e5d-5982-4d5f-8f18-37aeb9fce23a', 0.9, N'3bdfae73-8c70-404d-ac0b-9544227a27f7', N'00000000-0000-0000-0000-000000000000', N'CHF')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = '2b159a8f-5bdf-42ef-a946-487b06cf0150')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'2b159a8f-5bdf-42ef-a946-487b06cf0150', 92.39, N'3bdfae73-8c70-404d-ac0b-9544227a27f7', N'00000000-0000-0000-0000-000000000000', N'RUB')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = '19232422-724f-419b-a8a0-4c19cda5e418')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'19232422-724f-419b-a8a0-4c19cda5e418', 150, N'3bdfae73-8c70-404d-ac0b-9544227a27f7', N'00000000-0000-0000-0000-000000000000', N'JPY')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = 'af0953ad-966d-4ce4-a39c-63f2cedccae1')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'af0953ad-966d-4ce4-a39c-63f2cedccae1', 0.93, N'3bdfae73-8c70-404d-ac0b-9544227a27f7', N'00000000-0000-0000-0000-000000000000', N'EUR')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = 'f336dd9f-8f15-4bbd-a5db-68185f274f12')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'f336dd9f-8f15-4bbd-a5db-68185f274f12', 1.37, N'3bdfae73-8c70-404d-ac0b-9544227a27f7', N'00000000-0000-0000-0000-000000000000', N'CAD')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = '0723fec2-a99b-4e09-89bb-75b9d9669353')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'0723fec2-a99b-4e09-89bb-75b9d9669353', 10, N'8d7d5fd6-5ea5-4c65-bd11-1373450e8320', N'00000000-0000-0000-0000-000000000000', N'RUP')
END

GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = '679ccd70-16ce-456f-8189-8069a0710892')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'679ccd70-16ce-456f-8189-8069a0710892', 24328, N'3bdfae73-8c70-404d-ac0b-9544227a27f7', N'00000000-0000-0000-0000-000000000000', N'VND')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = 'ee90b553-49df-4cb8-8725-83255de7d925')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'ee90b553-49df-4cb8-8725-83255de7d925', 23000, N'8d7d5fd6-5ea5-4c65-bd11-1373450e8320', N'00000000-0000-0000-0000-000000000000', N'VND')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = 'a61337e6-95be-4484-af52-b87714f9e2d9')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'a61337e6-95be-4484-af52-b87714f9e2d9', 7.28, N'3bdfae73-8c70-404d-ac0b-9544227a27f7', N'00000000-0000-0000-0000-000000000000', N'CNY')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = 'b1df778c-10ae-476c-8656-c621017af4dd')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'b1df778c-10ae-476c-8656-c621017af4dd', 1.69, N'3bdfae73-8c70-404d-ac0b-9544227a27f7', N'00000000-0000-0000-0000-000000000000', N'NZD')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[NH_DM_TiGia_ChiTiet] where [ID] = 'd11a67b8-ed34-4d16-9098-f04c59aa5553')
BEGIN 
INSERT [dbo].[NH_DM_TiGia_ChiTiet] ([ID], [fTiGia], [iID_TiGiaID], [iID_TienTeID], [sMaTienTeQuyDoi]) VALUES (N'd11a67b8-ed34-4d16-9098-f04c59aa5553', 1.56, N'3bdfae73-8c70-404d-ac0b-9544227a27f7', N'00000000-0000-0000-0000-000000000000', N'AUD')
END
GO