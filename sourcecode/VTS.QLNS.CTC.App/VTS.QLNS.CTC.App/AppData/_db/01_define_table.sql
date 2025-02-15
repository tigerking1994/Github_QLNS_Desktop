/****** Object:  UserDefinedTableType [dbo].[t_tbl_uniqueidentifier]    Script Date: 5/20/2022 9:03:48 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_uniqueidentifier' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_uniqueidentifier]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_tonghopdautu]    Script Date: 5/20/2022 9:03:48 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_tonghopdautu' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_tonghopdautu]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_string]    Script Date: 5/20/2022 9:03:48 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_string' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_string]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet1]    Script Date: 5/20/2022 9:03:48 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_phanbovonchitiet1' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_phanbovonchitiet1]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet]    Script Date: 5/20/2022 9:03:48 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_phanbovonchitiet' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_phanbovonchitiet]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_pbv_string]    Script Date: 5/20/2022 9:03:48 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_pbv_string' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_pbv_string]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_importline]    Script Date: 5/20/2022 9:03:48 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_importline' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_importline]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_chiphihangmucnguonvon]    Script Date: 5/20/2022 9:03:48 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_chiphihangmucnguonvon' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_chiphihangmucnguonvon]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_chiphihangmucnguonvon]    Script Date: 5/20/2022 9:03:48 AM ******/
CREATE TYPE [dbo].[t_tbl_chiphihangmucnguonvon] AS TABLE(
	[Id] [uniqueidentifier] NULL,
	[iLoai] [int] NULL,
	[iId_ChiPhi] [uniqueidentifier] NULL,
	[iId_HangMuc] [uniqueidentifier] NULL,
	[iId_NguonVon] [int] NULL,
	[iId_ParentId] [uniqueidentifier] NULL,
	[sNoiDung] [nvarchar](1200) NULL,
	[fThanhTien] [float] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_importline]    Script Date: 5/20/2022 9:03:48 AM ******/
CREATE TYPE [dbo].[t_tbl_importline] AS TABLE(
	[iLine] [int] NULL,
	[iThang] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_pbv_string]    Script Date: 5/20/2022 9:03:48 AM ******/
CREATE TYPE [dbo].[t_tbl_pbv_string] AS TABLE(
	[sId] [nvarchar](1200) NULL,
	[sMoTa] [nvarchar](1200) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet]    Script Date: 5/20/2022 9:03:48 AM ******/
CREATE TYPE [dbo].[t_tbl_phanbovonchitiet] AS TABLE(
	[iID_PhanBoVonID] [uniqueidentifier] NULL,
	[iID_DuAnID] [uniqueidentifier] NULL,
	[sTrangThaiDuAnDangKy] [nvarchar](200) NULL,
	[fGiaTrDeNghi] [float] NULL,
	[fGiaTrPhanBo] [float] NULL,
	[fGiaTriThuHoi] [float] NULL,
	[iID_DonViTienTeID] [uniqueidentifier] NULL,
	[iID_TienTeID] [uniqueidentifier] NULL,
	[fTiGiaDonVi] [float] NULL,
	[fTiGia] [float] NULL,
	[sGhiChu] [nvarchar](max) NULL,
	[iID_LoaiNguonVonID] [uniqueidentifier] NULL,
	[sLNS] [nvarchar](50) NULL,
	[sL] [nvarchar](50) NULL,
	[sK] [nvarchar](50) NULL,
	[sM] [nvarchar](50) NULL,
	[sTM] [nvarchar](50) NULL,
	[sTTM] [nvarchar](50) NULL,
	[sNG] [nvarchar](50) NULL,
	[fCapPhatTaiKhoBac] [float] NULL,
	[fCapPhatBangLenhChi] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBac] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChi] [float] NULL,
	[fCapPhatTaiKhoBacDc] [float] NULL,
	[fCapPhatBangLenhChiDc] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBacDc] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChiDc] [float] NULL,
	[iID_Parent] [uniqueidentifier] NULL,
	[ILoaiDuAn] [int] NULL,
	[IIdLoaiCongTrinh] [uniqueidentifier] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet1]    Script Date: 5/20/2022 9:03:48 AM ******/
CREATE TYPE [dbo].[t_tbl_phanbovonchitiet1] AS TABLE(
	[iID_PhanBoVonID] [uniqueidentifier] NULL,
	[iID_DuAnID] [uniqueidentifier] NULL,
	[sTrangThaiDuAnDangKy] [nvarchar](200) NULL,
	[fGiaTrDeNghi] [float] NULL,
	[fGiaTrPhanBo] [float] NULL,
	[fGiaTriThuHoi] [float] NULL,
	[iID_DonViTienTeID] [uniqueidentifier] NULL,
	[iID_TienTeID] [uniqueidentifier] NULL,
	[fTiGiaDonVi] [float] NULL,
	[fTiGia] [float] NULL,
	[sGhiChu] [nvarchar](max) NULL,
	[iID_LoaiNguonVonID] [uniqueidentifier] NULL,
	[sL] [nvarchar](50) NULL,
	[sK] [nvarchar](50) NULL,
	[sM] [nvarchar](50) NULL,
	[sTM] [nvarchar](50) NULL,
	[sTTM] [nvarchar](50) NULL,
	[sNG] [nvarchar](50) NULL,
	[fCapPhatTaiKhoBac] [float] NULL,
	[fCapPhatBangLenhChi] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBac] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChi] [float] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_string]    Script Date: 5/20/2022 9:03:48 AM ******/
CREATE TYPE [dbo].[t_tbl_string] AS TABLE(
	[sId] [nvarchar](1200) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_tonghopdautu]    Script Date: 5/20/2022 9:03:48 AM ******/
CREATE TYPE [dbo].[t_tbl_tonghopdautu] AS TABLE(
	[Id] [uniqueidentifier] NOT NULL,
	[iID_ChungTu] [uniqueidentifier] NOT NULL,
	[iID_DuAnID] [uniqueidentifier] NULL,
	[sMaNguon] [nvarchar](100) NULL,
	[sMaNguonCha] [nvarchar](100) NULL,
	[sMaDich] [nvarchar](100) NULL,
	[fGiaTri] [float] NULL,
	[ILoaiUng] [int] NULL,
	[iStatus] [int] NULL,
	[bIsLog] [bit] NULL,
	[iId_MaNguonCha] [uniqueidentifier] NULL,
	[iThuHoiTUCheDo] [int] NULL,
	[IIDMucID] [uniqueidentifier] NULL,
	[IIDTieuMucID] [uniqueidentifier] NULL,
	[IIDTietMucID] [uniqueidentifier] NULL,
	[IIDNganhID] [uniqueidentifier] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_uniqueidentifier]    Script Date: 5/20/2022 9:03:48 AM ******/
CREATE TYPE [dbo].[t_tbl_uniqueidentifier] AS TABLE(
	[Id] [uniqueidentifier] NULL
)
GO
