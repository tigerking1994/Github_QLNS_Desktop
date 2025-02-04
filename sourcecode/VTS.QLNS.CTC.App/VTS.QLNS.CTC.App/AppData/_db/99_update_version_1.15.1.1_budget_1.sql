GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_DuToan__PAPB_MauSo2_KetQua')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_DuToan__PAPB_MauSo2_KetQua', NULL, N'rptNS_DuToan__PAPB_MauSo2_KetQua', NULL, NULL, NULL, NULL, NULL, N'DU_TOAN_PHUONG_AN_PHAN_BO', NULL, N'Phương án phân bổ dự toán - Theo Công văn 2344/QĐ-CTC', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'THÔNG BÁO', N'2', N'Kết quả kiểm tra Phương án phân bổ dự toán ngân sách năm 2024', N'Kính gửi:', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_Plan_DuToan_NganSach_ChiTietDonVi_NSSD_Ngang')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_Plan_DuToan_NganSach_ChiTietDonVi_NSSD_Ngang', NULL, N'rptNS_Plan_DuToan_NganSach_ChiTietDonVi_NSSD_Ngang', NULL, NULL, NULL, NULL, NULL, N'LAP_DU_TOAN_NGAN_SACH', NULL, N'Báo cáo dự toán ngân sách - Các nội dung thực hiện định mức thí điểm', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CHI TIẾT DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG ĐỐI VỚI NỘI DUNG CHI THỰC HIỆN ĐỊNH MỨC THÍ ĐIỂM NĂM 2024', N'1', N'(Nội dung ngân sách chi nghiệp vụ ngành)', N'.../QĐ-BQP', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_Plan_DuToan_NganSach_ChiTietDonVi_NSDT_Ngang')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_Plan_DuToan_NganSach_ChiTietDonVi_NSDT_Ngang', NULL, N'rptNS_Plan_DuToan_NganSach_ChiTietDonVi_NSDT_Ngang', NULL, NULL, NULL, NULL, NULL, N'LAP_DU_TOAN_NGAN_SACH', NULL, N'Báo cáo dự toán ngân sách - Các nội dung thực hiện định mức thí điểm', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CHI TIẾT DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG ĐỐI VỚI NỘI DUNG CHI THỰC HIỆN ĐỊNH MỨC THÍ ĐIỂM NĂM 2024', N'1', N'(Nội dung chi mua hàng cấp hiện vật)', N'.../QĐ-BQP', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSSD')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSSD', NULL, N'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSSD', NULL, NULL, NULL, NULL, NULL, N'LAP_DU_TOAN_NGAN_SACH', NULL, N'Báo cáo dự toán ngân sách - Các nội dung thực hiện định mức thí điểm', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG ĐỐI VỚI NỘI DUNG CHI THỰC HIỆN ĐỊNH MỨC THÍ ĐIỂM NĂM 2024', N'1', N'(Nội dung ngân sách chi nghiệp vụ ngành)', N'.../QĐ-BQP', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
ELSE
UPDATE DM_ChuKy
set TieuDe1_MoTa = N'BÁO CÁO DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG ĐỐI VỚI NỘI DUNG CHI THỰC HIỆN ĐỊNH MỨC THÍ ĐIỂM NĂM 2024'
where Id_Type = 'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSSD'
GO

IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSDT')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSDT', NULL, N'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSDT', NULL, NULL, NULL, NULL, NULL, N'LAP_DU_TOAN_NGAN_SACH', NULL, N'Báo cáo dự toán ngân sách - Các nội dung thực hiện định mức thí điểm', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG ĐỐI VỚI NỘI DUNG CHI THỰC HIỆN ĐỊNH MỨC THÍ ĐIỂM NĂM 2024', N'1', N'(Nội dung chi mua hàng cấp hiện vật)', N'.../QĐ-BQP', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
UPDATE DM_ChuKy
set TieuDe1_MoTa = N'BÁO CÁO DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG ĐỐI VỚI NỘI DUNG CHI THỰC HIỆN ĐỊNH MỨC THÍ ĐIỂM NĂM 2024'
where Id_Type = 'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSDT'
GO


/****** Object:  StoredProcedure [dbo].[sp_clone_year_muc_luc_quyet_toan_nam]    Script Date: 11/15/2024 11:37:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_muc_luc_quyet_toan_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_muc_luc_quyet_toan_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 11/15/2024 11:37:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data_update_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data_update_new]
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 11/14/2024 2:44:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1
	
	CREATE TABLE #datatemp (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChi float
	);

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0)))/@DVT fTuChi
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo, sum(temp.fTuChi)/@DVT fTuChi
	from (
	select
		distinct
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sSKT_KyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fTuChi fMucTienPhanBo,
		0 fTuChi
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ctct.iLoai = 4
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

	select * from #datatemp order by sKyHieu, sXauNoiMa

END
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	CREATE TABLE #tbl_banthan (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChiBanThan float
	);

	insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChiBanThan)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChiBanThan
		from NS_DTDauNam_ChungTuChiTiet ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
		insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChiBanThan)
		select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
		sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.fTuChi, 0)) fTuChi
		from (
		select
			distinct
			null IIDMLNS,
			null IIDMLNSCha,
			ml.iID_MLSKT IIDMLSKT,
			ml.iID_MLSKTCha IIDMLSKTCha,
			ml.bHangCha IsHangCha,
			null sLNS,
			null sL,
			null sK,
			ml.sM,
			null sTM,
			null sTTM,
			null sNG,
			null sTNG,
			null sXauNoiMa,
			ml.sSKT_KyHieu,
			case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
			ctct.fTuChi fMucTienPhanBo,
			0 fTuChi
		from NS_SKT_ChungTuChiTiet ctct
		join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
		join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ctct.iLoai = 4
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
			and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			) temp
		group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

	--Don vi
	CREATE TABLE #datadonvi (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChi float
	);

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChi
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.fTuChi, 0)) fTuChi
	from (
	select
		distinct
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sSKT_KyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fTuChi fMucTienPhanBo,
		0 fTuChi
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ctct.iLoai = 4
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

	select ml.* into #mlns_base from
	(select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #tbl_banthan
	union
	select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #datadonvi) ml
	
	--Ket qua
	select
		ml.IIDMLNS,
		ml.IIDMLNSCha,
		ml.IsHangCha,
		ml.IIDMLSKT,
		ml.IIDMLSKTCha,
		ml.sLNS,
		ml.sL,
		ml.sK,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sNG,
		ml.sTNG,
		ml.sXauNoiMa,
		ml.sKyHieu,
		ml.sMoTa,
		(sum(isnull(dv.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(dv.fTuChi, 0)))/@DVT fTuChi,
		(sum(isnull(banthan.fTuChiBanThan, 0)))/@DVT fTuChiBanThan,
		((sum(isnull(banthan.fTuChiBanThan, 0))) + (sum(isnull(dv.fTuChi, 0))))/@DVT TongTuChi
	from #mlns_base ml
	left join #datadonvi dv on isnull(ml.sXauNoiMa, '') = isnull(dv.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	left join #tbl_banthan banthan on isnull(ml.sXauNoiMa, '') = isnull(banthan.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	group by ml.IIDMLNS, ml.IIDMLNSCha, ml.IsHangCha, ml.IIDMLSKT, ml.IIDMLSKTCha, ml.sLNS, ml.sL, ml.sK, ml.sM,
		ml.sTM, ml.sTTM, ml.sNG, ml.sTNG, ml.sXauNoiMa, ml.sKyHieu, ml.sMoTa

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	CREATE TABLE #tbl_banthan (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhapBanThan float, FHangMuaBanThan float
	);

	insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhapBanThan, FHangMuaBanThan)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhapBanThan,
		(sum(isnull(ctct.fHangMua, 0))) FHangMuaBanThan
		from NS_DTDauNam_ChungTuChiTiet ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
		insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhapBanThan, FHangMuaBanThan)
		select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
		sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhapBanThan, 0)) FHangNhapBanThan, sum(isnull(temp.FHangMuaBanThan, 0)) FHangNhapBanThan
		from (
		select distinct
			null IIDMLNS,
			null IIDMLNSCha,
			ml.iID_MLSKT IIDMLSKT,
			ml.iID_MLSKTCha IIDMLSKTCha,
			ml.bHangCha IsHangCha,
			null sLNS,
			null sL,
			null sK,
			ml.sM,
			null sTM,
			null sTTM,
			null sNG,
			null sTNG,
			null sXauNoiMa,
			ml.sSKT_KyHieu,
			case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
			else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
			ctct.fMuaHangCapHienVat fMucTienPhanBo,
			0 FHangNhapBanThan,
			0 FHangMuaBanThan
		from NS_SKT_ChungTuChiTiet ctct
		join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
		join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ctct.iLoai = 4
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa


	--Don vi
	CREATE TABLE #datadonvi (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhap float, FHangMua float
	);

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhap,
		(sum(isnull(ctct.fHangMua, 0))) FHangMua
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhap, 0)), sum(isnull(temp.FHangMua, 0))
	from (
	select
		distinct
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sSKT_KyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fMuaHangCapHienVat fMucTienPhanBo,
		0 FHangNhap,
		0 FHangMua
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ctct.iLoai = 4
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa


	select ml.* into #mlns_base from
	(select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #tbl_banthan
	union
	select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #datadonvi) ml
	
	--Ket qua
	select
		ml.IIDMLNS,
		ml.IIDMLNSCha,
		ml.IsHangCha,
		ml.IIDMLSKT,
		ml.IIDMLSKTCha,
		ml.sLNS,
		ml.sL,
		ml.sK,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sNG,
		ml.sTNG,
		ml.sXauNoiMa,
		ml.sKyHieu,
		ml.sMoTa,
		(sum(isnull(dv.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(dv.FHangNhap, 0)))/@DVT FHangNhap,
		(sum(isnull(dv.FHangMua, 0)))/@DVT FHangMua,
		(sum(isnull(banthan.FHangNhapBanThan, 0)))/@DVT FHangNhapBanThan,
		(sum(isnull(banthan.FHangMuaBanThan, 0)))/@DVT FHangMuaBanThan,
		((sum(isnull(banthan.FHangNhapBanThan, 0))) + (sum(isnull(dv.FHangNhap, 0))))/@DVT TongHangNhap,
		((sum(isnull(banthan.FHangMuaBanThan, 0))) + (sum(isnull(dv.FHangMua, 0))))/@DVT TongHangMua
	from #mlns_base ml
	left join #datadonvi dv on isnull(ml.sXauNoiMa, '') = isnull(dv.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	left join #tbl_banthan banthan on isnull(ml.sXauNoiMa, '') = isnull(banthan.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	group by ml.IIDMLNS, ml.IIDMLNSCha, ml.IsHangCha, ml.IIDMLSKT, ml.IIDMLSKTCha, ml.sLNS, ml.sL, ml.sK, ml.sM,
		ml.sTM, ml.sTTM, ml.sNG, ml.sTNG, ml.sXauNoiMa, ml.sKyHieu, ml.sMoTa

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	CREATE TABLE #tbl_banthan (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChiBanThan float
	);

	insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChiBanThan)
		select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChiBanThan
		from NS_DTDauNam_ChungTuChiTiet ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
		insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChiBanThan)
		select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
		(sum(isnull(temp.fMucTienPhanBo, 0))) fMucTienPhanBo, sum(temp.fTuChi) fTuChi
		from (
		select
			distinct
			null IIDMLNS,
			null IIDMLNSCha,
			ml.iID_MLSKT IIDMLSKT,
			ml.iID_MLSKTCha IIDMLSKTCha,
			ml.bHangCha IsHangCha,
			null sLNS,
			null sL,
			null sK,
			ml.sM,
			null sTM,
			null sTTM,
			null sNG,
			null sTNG,
			null sXauNoiMa,
			ml.sSKT_KyHieu,
			case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
			else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
			ctct.fTuChi fMucTienPhanBo,
			0 fTuChi
		from NS_SKT_ChungTuChiTiet ctct
		join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
		join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ctct.iLoai = 4
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

	--Don vi
	CREATE TABLE #datadonvi (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChi float, IIdMaDonVi nvarchar(500)
	);

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi, IIdMaDonVi)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChi,
		ctct.iID_MaDonVi IIdMaDonVi
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa, ctct.iID_MaDonVi

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi, IIdMaDonVi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0))) fMucTienPhanBo, sum(temp.fTuChi) fTuChi, temp.IIdMaDonVi
	from (
	select
		distinct
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sSKT_KyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fTuChi fMucTienPhanBo,
		0 fTuChi,
		ctct.iID_MaDonVi IIdMaDonVi
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ctct.iLoai = 4
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa, temp.IIdMaDonVi


	select ml.* into #mlns_base from
	(select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #tbl_banthan
	union
	select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #datadonvi) ml
	
	--Ket qua
	select
		ml.IIDMLNS,
		ml.IIDMLNSCha,
		ml.IsHangCha,
		ml.IIDMLSKT,
		ml.IIDMLSKTCha,
		ml.sLNS,
		ml.sL,
		ml.sK,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sNG,
		ml.sTNG,
		ml.sXauNoiMa,
		ml.sKyHieu,
		ml.sMoTa,
		(sum(isnull(dv.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(dv.fTuChi, 0)))/@DVT fTuChi,
		(sum(isnull(banthan.fTuChiBanThan, 0)))/@DVT fTuChiBanThan,
		((sum(isnull(banthan.fTuChiBanThan, 0))) + (sum(isnull(dv.fTuChi, 0))))/@DVT TongTuChi,
		dv.IIdMaDonVi
	from #mlns_base ml
	left join #datadonvi dv on isnull(ml.sXauNoiMa, '') = isnull(dv.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	left join #tbl_banthan banthan on isnull(ml.sXauNoiMa, '') = isnull(banthan.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	group by ml.IIDMLNS, ml.IIDMLNSCha, ml.IsHangCha, ml.IIDMLSKT, ml.IIDMLSKTCha, ml.sLNS, ml.sL, ml.sK, ml.sM,
		ml.sTM, ml.sTTM, ml.sNG, ml.sTNG, ml.sXauNoiMa, ml.sKyHieu, ml.sMoTa, dv.IIdMaDonVi

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1
	
	CREATE TABLE #datatemp (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fHangNhap float, fHangMua float
	);

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fHangNhap, fHangMua)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0)))/@DVT fHangNhap,
		(sum(isnull(ctct.fHangMua, 0)))/@DVT fHangMua
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fHangNhap, fHangMua)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo, sum(isnull(temp.fHangNhap, 0))/@DVT fTuChi, sum(isnull(temp.fHangMua, 0))/@DVT fTuChi
	from (
	select
		distinct
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sSKT_KyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fMuaHangCapHienVat fMucTienPhanBo,
		0 fHangNhap,
		0 fHangMua
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ctct.iLoai = 4
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

	select * from #datatemp order by sKyHieu, sXauNoiMa

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	CREATE TABLE #tbl_banthan (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhapBanThan float, FHangMuaBanThan float
	);

	insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhapBanThan, FHangMuaBanThan)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhapBanThan,
		(sum(isnull(ctct.fHangMua, 0))) FHangMuaBanThan
		from NS_DTDauNam_ChungTuChiTiet ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
		insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhapBanThan, FHangMuaBanThan)
		select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
		sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhapBanThan, 0)), sum(isnull(temp.FHangMuaBanThan, 0)) 
		from (
		select
			distinct
			null IIDMLNS,
			null IIDMLNSCha,
			ml.iID_MLSKT IIDMLSKT,
			ml.iID_MLSKTCha IIDMLSKTCha,
			ml.bHangCha IsHangCha,
			null sLNS,
			null sL,
			null sK,
			ml.sM,
			null sTM,
			null sTTM,
			null sNG,
			null sTNG,
			null sXauNoiMa,
			ml.sSKT_KyHieu,
			case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
			else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
			ctct.fMuaHangCapHienVat fMucTienPhanBo,
			0 FHangNhapBanThan,
			0 FHangMuaBanThan
		from NS_SKT_ChungTuChiTiet ctct
		join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
		join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ctct.iLoai = 4
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa


	--Don vi
	CREATE TABLE #datadonvi (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhap float, FHangMua float, IIdMaDonVi nvarchar(500)
	);

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua, IIdMaDonVi)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhap,
		(sum(isnull(ctct.fHangMua, 0))) FHangMua,
		ctct.iID_MaDonVi IIdMaDonVi
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa, ctct.iID_MaDonVi

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua, IIdMaDonVi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhap, 0)), sum(isnull(temp.FHangMua, 0)), temp.IIdMaDonVi
	from (
	select
		distinct
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sSKT_KyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fMuaHangCapHienVat fMucTienPhanBo,
		0 FHangNhap,
		0 FHangMua,
		ctct.iID_MaDonVi IIdMaDonVi
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ctct.iLoai = 4
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa, temp.IIdMaDonVi

	select ml.* into #mlns_base from
	(select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #tbl_banthan
	union
	select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #datadonvi) ml
	
	--Ket qua
	select
		ml.IIDMLNS,
		ml.IIDMLNSCha,
		ml.IsHangCha,
		ml.IIDMLSKT,
		ml.IIDMLSKTCha,
		ml.sLNS,
		ml.sL,
		ml.sK,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sNG,
		ml.sTNG,
		ml.sXauNoiMa,
		ml.sKyHieu,
		ml.sMoTa,
		(sum(isnull(dv.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(dv.FHangNhap, 0)))/@DVT FHangNhap,
		(sum(isnull(dv.FHangMua, 0)))/@DVT FHangMua,
		(sum(isnull(banthan.FHangNhapBanThan, 0)))/@DVT FHangNhapBanThan,
		(sum(isnull(banthan.FHangMuaBanThan, 0)))/@DVT FHangMuaBanThan,
		((sum(isnull(banthan.FHangNhapBanThan, 0))) + (sum(isnull(dv.FHangNhap, 0))))/@DVT TongHangNhap,
		((sum(isnull(banthan.FHangMuaBanThan, 0))) + (sum(isnull(dv.FHangMua, 0))))/@DVT TongHangMua,
		dv.IIdMaDonVi
	from #mlns_base ml
	left join #datadonvi dv on isnull(ml.sXauNoiMa, '') = isnull(dv.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	left join #tbl_banthan banthan on isnull(ml.sXauNoiMa, '') = isnull(banthan.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	group by ml.IIDMLNS, ml.IIDMLNSCha, ml.IsHangCha, ml.IIDMLSKT, ml.IIDMLSKTCha, ml.sLNS, ml.sL, ml.sK, ml.sM,
		ml.sTM, ml.sTTM, ml.sNG, ml.sTNG, ml.sXauNoiMa, ml.sKyHieu, ml.sMoTa, dv.IIdMaDonVi

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]
	@XauNoiMa nvarchar(max),
	@NamLamViec int
AS
BEGIN
	
	select distinct ck.*
	from NS_DanhMucCongKhai ck
	join NS_DMCongKhai_MLNS map on ck.id = map.iID_DMCongKhai
	where ck.iNamLamViec = @NamLamViec
		and map.iNamLamViec = @NamLamViec
		and map.sNS_XauNoiMa in (SELECT * FROM f_split(@XauNoiMa))

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	where --ct.NamLamViec = @YearOfWork and ct.NamNganSach = @YearOfBudget
		convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	where --ct.iNamLamViec = @YearOfWork and ct.iNamNganSach = @YearOfBudget
		convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)

	--Data Ban than
	select temp.* into #databanthan from(
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		left join #basedatathu cpb ON mlns.sXauNoiMa = cpb.XauNoiMa and cpb.iPhanCap = 1 and dtnsdg.NguonNganSach = 1
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha

		UNION

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		left join #basedatachi cpb ON mlns.sXauNoiMa = cpb.sXauNoiMa and cpb.iPhanCap = 1 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha
	) temp

	CREATE TABLE #datadonvi (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fChoPhanBo float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
	insert into #datadonvi (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, fChoPhanBo, FSoPhanBo, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
		   (sum(isnull(dtnsdg.TuChi, 0)) - sum(isnull(cpb.TuChi, 0)))/@DVT fChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		left join #basedatathu cpb ON mlns.sXauNoiMa = cpb.XauNoiMa and cpb.iPhanCap = 1 and dtnsdg.NguonNganSach = 1
		where mlns.sLNS like '8%'
		and donvi.Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha

		insert into #datadonvi (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, fChoPhanBo, FSoPhanBo, sLoai)

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
		   (sum(isnull(dtnsdg.fTuChi, 0)) - sum(isnull(cpb.fTuChi, 0)))/@DVT fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		left join #basedatachi cpb ON mlns.sXauNoiMa = cpb.sXauNoiMa and cpb.iPhanCap = 1 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha

	--Ket qua
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(donvi.fChoPhanBo, 0)) fChoPhanBo,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha

	union

	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(donvi.fChoPhanBo, 0)) fChoPhanBo,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN
	
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ct.Id = ctct.Id_ChungTu
	where --ct.NamLamViec = @YearOfWork and ct.NamNganSach = @YearOfBudget
		convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.Id_DonVi in (SELECT * FROM f_split(@MaDonVi))
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
	where --ct.iNamLamViec = @YearOfWork and ct.iNamNganSach = @YearOfBudget
		convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))

	--Data Ban than
	select temp.* into #databanthan from(
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		left join #basedatathu cpb ON mlns.sXauNoiMa = cpb.XauNoiMa and cpb.iPhanCap = 1 and dtnsdg.NguonNganSach = 1
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha,
			donvi.Id_donvi

		UNION

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		left join #basedatachi cpb ON mlns.sXauNoiMa = cpb.sXauNoiMa and cpb.iPhanCap = 1 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha
	) temp

	CREATE TABLE #datadonvi1 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fChoPhanBo float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
	insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, fChoPhanBo, FSoPhanBo, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
		   (sum(isnull(dtnsdg.TuChi, 0)) - sum(isnull(cpb.TuChi, 0)))/@DVT fChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		left join #basedatathu cpb ON mlns.sXauNoiMa = cpb.XauNoiMa and cpb.iPhanCap = 1 and dtnsdg.NguonNganSach = 1
		where mlns.sLNS like '8%'
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha

		insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, fChoPhanBo, FSoPhanBo, sLoai)

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
		   (sum(isnull(dtnsdg.fTuChi, 0)) - sum(isnull(cpb.fTuChi, 0)))/@DVT fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		left join #basedatachi cpb ON mlns.sXauNoiMa = cpb.sXauNoiMa and cpb.iPhanCap = 1 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha
	

	--Ket qua
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(donvi.fChoPhanBo, 0)) fChoPhanBo,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha

	union

	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(donvi.fChoPhanBo, 0)) fChoPhanBo,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 11/14/2024 2:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]
	@MaCongKhai nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select ck.* into #basemlns
	from NS_DanhMucCongKhai ck
	where ck.sMa in (select * from f_split(@MaCongKhai))
		and ck.iNamLamViec = @YearOfWork

	--Thu
	select ctct.*, ck.sMa sMaMlck into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.LNS like '8%'
		
	--Chi
	select ctct.*, ck.sMa sMaMlck into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.sLNS not like '8%'

	CREATE TABLE #databanthanm02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBoBanThan float,
	sLoai nvarchar(50));

	--Data Ban than
	insert into #databanthanm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT fSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatathu dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

		insert into #databanthanm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatachi dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

	CREATE TABLE #datadonvim02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
		insert into #datadonvim02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBo, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatathu dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where donvi.Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

		insert into #datadonvim02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBo, sLoai)

		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatachi dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where donvi.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

	--Ket qua

	CREATE TABLE #tblresultm02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBoBanThan float,
	fSoPhanBo float,
	fTongSoPhanBo float,
	sLoai nvarchar(50),
	iLoai int,
	iRoot int,
	hasData bit);

	--A. DỰ TOÁN THU
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, FSoPhanBo, fTongSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'A. DỰ TOÁN THU' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 fSoPhanBoBanThan, 0 fSoPhanBo, 0 FTongSoPhanBo, 'THU' sLoai, 1 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, fTongSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		mlns.sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) fSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) fTongSoPhanBo,
		'THU' sLoai,
		1 iLoai,
		2 iRoot,
		1 hasData
	from #basemlns mlns
	left join #datadonvim02 donvi on mlns.sMa = donvi.sMa and donvi.sLoai = 'THU'
	left join #databanthanm02 banthan on mlns.sMa = banthan.sMa and banthan.sLoai = 'THU'
	group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha
	
	--B. DỰ TOÁN CHI
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, fTongSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'B. DỰ TOÁN CHI' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 FSoPhanBoBanThan, 0 FSoPhanBo, 0 FTongSoPhanBo, 'CHI' sLoai, 2 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, fTongSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		mlns.sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'CHI' sLoai,
		2 iLoai,
		2 iRoot,
		1 hasData
	from #basemlns mlns
	left join #datadonvim02 donvi on mlns.sMa = donvi.sMa and donvi.sLoai = 'CHI'
	left join #databanthanm02 banthan on mlns.sMa = banthan.sMa and banthan.sLoai = 'CHI'
	group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

	update #tblresultm02 set hasData = 0
	where (fDuToanNSDuocGiao = 0 and FSoPhanBoBanThan = 0 and FSoPhanBo = 0 and iRoot = 2)
	or (iRoot = 1 and iLoai in (select iLoai from #tblresultm02
		where fDuToanNSDuocGiao = 0 and FSoPhanBoBanThan = 0 and FSoPhanBo = 0 and iRoot = 2))

	select * from #tblresultm02 where hasData = 1
	order by iLoai, iRoot
END
GO

/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 11/15/2024 11:37:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_clone_data_update_new] 
	-- Add the parameters for the stored procedure here
	@userCreator varchar(100),
	@sourceYear int,
	@destinationYear int,
	@isUpdatedMLNS int,
	@isUpdatedNSDV int,
	@isUpdatedBQuanLy int,
	@isUpdateMLQS int,
	@isUpdateDanhMucChuyenNganh int,
	@isUpdateDanhMucNganh int,
	@isUpdateMuclucSkt int,
	@isUpdateDanhMucCapPhat int,
	@isUpdateCauHinhChiTieuLuongMLNS int,
	@isUpdateDmCapBacKh int,
	@isUpdateNSSKT int,
	@isUpdateCauHinhHeThong int,
	@isUpdateDanhMucDonViTinh int,
	@isUpdateDanhMucCanCu int,
	@isUpdateDanhMucCKTC int,
	@isUpdateDanhMucBHXH int,
	@isUpdateMucLucCacLoaiChi int,
	@isUpdateDanhMucCoSoYTe int,
	@isUpdateDanhMucTDQT int,
	@isUpdateDanhMucCHTSBHXH int,
	@isUpdateDanhMucNgayNghi int,
	@isUpdateMucLucQuyetToanNam int
	--@isUpdateDanhMucChuDauTu int,
	--@IsUpdateDanhMucDonviQuanLyDuAn int,
	--@isUpdateDanhMucNhaThau int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if (@isUpdateDanhMucNganh = 1)
		Begin
			DELETE FROM DanhMuc where INamLamViec = @destinationYear and [sType] = 'NS_Nganh_Nganh';
			INSERT INTO [dbo].[DanhMuc]
			   ([sType]
			   ,[iID_MaDanhMuc]
			   ,[sTen]
			   ,[sGiaTri]
			   ,[sMoTa]
			   ,[iThuTu]
			   ,[iNamLamViec]
			   ,[iTrangThai]
			   ,[dNgayTao]
			   ,[sNguoiTao]
			   ,[dNgaySua]
			   ,[sNguoiSua]
			   ,[Tag]
			   ,[Log]
			   ,[NganSachNganh])
			 SELECT
				[sType]
				,[iID_MaDanhMuc]
				,[sTen]
				,[sGiaTri]
				,[sMoTa]
				,[iThuTu]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[NganSachNganh]
		  FROM [dbo].[DanhMuc] where iNamLamViec = @sourceYear and [sType] = 'NS_Nganh_Nganh';
		End;

	if (@isUpdateDanhMucChuyenNganh = 1)
		Begin
			DELETE FROM DanhMuc where iNamLamViec = @destinationYear and [sType] = 'NS_Nganh';
			INSERT INTO [dbo].[DanhMuc]
				([sType]
				,[iID_MaDanhMuc]
				,[sTen]
				,[sGiaTri]
				,[sMoTa]
				,[iThuTu]
				,[iNamLamViec]
				,[iTrangThai]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[NganSachNganh])
			 SELECT
				[sType]
				,[iID_MaDanhMuc]
				,[sTen]
				,[sGiaTri]
				,[sMoTa]
				,[iThuTu]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
				,[NganSachNganh]
		  FROM [dbo].[DanhMuc] where iNamLamViec = @sourceYear and [sType] = 'NS_Nganh';
		End;

	if (@isUpdateMLQS = 1)
		Begin
			Delete FROM NS_QS_MucLuc where iNamLamViec = @destinationYear;
			INSERT INTO [NS_QS_MucLuc]
				([iID_MLNS]
				,[iID_MLNS_Cha]
				,[sM]
				,[sTM]
				,[sKyHieu]
				,[sMoTa]
				,[iThuTu]
				,[sHienThi]
				,[bHangCha]
				,[iTrangThai]
				,[iNamLamViec])
			SELECT
				[iID_MLNS]
				,[iID_MLNS_Cha]
				,[sM]
				,[sTM]
				,[sKyHieu]
				,[sMoTa]
				,[iThuTu]
				,[sHienThi]
				,[bHangCha]
				,[iTrangThai]
				,@destinationYear
			  FROM [NS_QS_MucLuc]  where iNamLamViec = @sourceYear;
		END;

	if (@isUpdatedBQuanLy = 1)
		Begin
			DELETE FROM DM_BQuanLy where iNamLamViec = @destinationYear;
			INSERT INTO [DM_BQuanLy]
				([iID_MaBQuanLy]
				,[sTenBQuanLy]
				,[sKyHieu]
				,[sMoTa]
				,[iNamLamViec]
				,[iTrangThai]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua])
			 SELECT
				[iID_MaBQuanLy]
				,[sTenBQuanLy]
				,[sKyHieu]
				,[sMoTa]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
			FROM [DM_BQuanLy] where iNamLamViec = @sourceYear;
		End;
	if (@isUpdatedNSDV = 1)
		Begin
			Delete FROM  [DonVi] where iNamLamViec = @destinationYear;
			INSERT INTO [dbo].[DonVi]
				([iID_Parent]
				,[iID_MaDonVi]
				,[sTenDonVi]
				,[sKyHieu]
				,[sMoTa]
				,[iLoai]
				,[iNamLamViec]
				,[iTrangThai]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[LoaiNganSach]
				,[bCoNSNganh]
				,[iKhoi])
			 SELECT [iID_Parent]
				,[iID_MaDonVi]
				,[sTenDonVi]
				,[sKyHieu]
				,[sMoTa]
				,[iLoai]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
				,[LoaiNganSach]
				,[bCoNSNganh]
				,[iKhoi]
			FROM [DonVi] where iNamLamViec = @sourceYear;
			INSERT INTO [NguoiDung_DonVi]
           ([iID_MaNguoiDung]
           ,[iID_MaDonVi]
           ,[iNamLamViec]
           ,[iSTT]
           ,[iTrangThai]
           ,[bPublic]
           ,[dNgayTao]
           ,[iSoLanSua]
           ,[dNgaySua]
           ,[sIPSua]
           ,[sTenDonVi])
			 SELECT [iID_MaNguoiDung]
			  ,[iID_MaDonVi]
			  ,@destinationYear
			  ,[iSTT]
			  ,[iTrangThai]
			  ,[bPublic]
			  ,[dNgayTao]
			  ,[iSoLanSua]
			  ,[dNgaySua]
			  ,[sIPSua]
			  ,[sTenDonVi]
		  FROM [NguoiDung_DonVi] where iNamLamViec = @sourceYear;
		End;

	if (@isUpdatedMLNS = 1)
		Begin
			DELETE FROM [NS_MucLucNganSach] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [NS_MucLucNganSach]
				([iID_MLNS]
				,[iID_MLNS_Cha]
				,[sXauNoiMa]
				,[sLNS]
				,[sL]
				,[sK]
				,[sM]
				,[sTM]
				,[sTTM]
				,[sNG]
				,[sTNG]
				,[sMoTa]
				,[iNamLamViec]
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi]
				,[sMaCB])
			 SELECT [iID_MLNS]
				,[iID_MLNS_Cha]
				,[sXauNoiMa]
				,[sLNS]
				,[sL]
				,[sK]
				,[sM]
				,[sTM]
				,[sTTM]
				,[sNG]
				,[sTNG]
				,[sMoTa]
				,@destinationYear
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi]
				,[sMaCB]
		  FROM [NS_MucLucNganSach] where iNamLamViec = @sourceYear;
		  DELETE FROM [NS_NguoiDung_LNS] WHERE iNamLamViec = @destinationYear;
		  INSERT INTO [NS_NguoiDung_LNS]
			   ([sMaNguoiDung]
			   ,[sLNS]
			   ,[iNamLamViec])
			   (SELECT [sMaNguoiDung]
				  ,[sLNS]
				  ,@destinationYear
				FROM [NS_NguoiDung_LNS] where iNamLamViec = @sourceYear)
		End;

	if (@isUpdateMuclucSkt = 1)
		Begin
			DELETE FROM [NS_SKT_MucLuc] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [dbo].[NS_SKT_MucLuc]
			   ([iID_MLSKT]
			   ,[SKyHieu]
			   ,[SKyHieuCu]
			   ,[sL]
			   ,[sK]
			   ,[sM]
			   ,[sNG_Cha]
			   ,[sNG]
			   ,[sSTT]
			   ,[sSTTBC]
			   ,[sMoTa]
			   ,[KyHieuCha]
			   ,[bHangCha]
			   ,[iTrangThai]
			   ,[iNamLamViec]
			   ,[dNgayTao]
			   ,[dNguoiTao]
			   ,[dNgaySua]
			   ,[dNguoiSua]
			   ,[Tag]
			   ,[Log]
			   ,[Muc]
			   ,[iID_MLSKTCha]
			   ,[sLoaiNhap])
			SELECT [iID_MLSKT]
			   ,[SKyHieu]
			   ,[SKyHieuCu]
			   ,[sL]
			   ,[sK]
			   ,[sM]
			   ,[sNG_Cha]
			   ,[sNG]
			   ,[sSTT]
			   ,[sSTTBC]
			   ,[sMoTa]
			   ,[KyHieuCha]
			   ,[bHangCha]
			   ,[iTrangThai]
			  ,@destinationYear
			  ,GETDATE()
			  ,@userCreator
			  ,null
			  ,null
			  ,[Tag]
			  ,[Log]
			  ,[Muc]
			  ,[iID_MLSKTCha]
			   ,[sLoaiNhap]
		  FROM [dbo].[ns_SKT_MucLuc] where iNamLamViec = @sourceYear;
		End;

	if (@isUpdateDanhMucCapPhat = 1)
		Begin
			DELETE FROM [CP_DanhMuc] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [dbo].[CP_DanhMuc]
			   ([iID_MaDMCapPhat]
			   ,[sTen]
			   ,[sTenThongTriCap]
			   ,[sTenThongTriThu]
			   ,[LNS]
			   ,[sMoTa]
			   ,[OrderIndex]
			   ,[iNamLamViec]
			   ,[iTrangThai]
			   ,[dNgayTao]
			   ,[sNguoiTao]
			   ,[dNgaySua]
			   ,[sNguoiSua]
			   ,[Tag]
			   ,[Log])
			SELECT [iID_MaDMCapPhat]
			   ,[sTen]
			   ,[sTenThongTriCap]
			   ,[sTenThongTriThu]
			   ,[LNS]
			   ,[sMoTa]
				,[OrderIndex]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
			FROM [dbo].[CP_DanhMuc] where iNamLamViec = @sourceYear;
		End;

	if (@isUpdateCauHinhChiTieuLuongMLNS = 1)
		Begin
			DELETE FROM [TL_PhuCap_MLNS] WHERE NAM = @destinationYear;
			INSERT INTO [dbo].[TL_PhuCap_MLNS]
			   ([Ma_PhuCap]
			   ,[Ten_PhuCap]
			   ,[Ma_CachTL]
			   ,[XauNoiMa]
			   ,[LNS]
			   ,[L]
			   ,[K]
			   ,[M]
			   ,[TM]
			   ,[TTM]
			   ,[NG]
			   ,[MoTa]
			   ,[Ma_NguonNganSach]
			   ,[NguonNganSach]
			   ,[DateCreated]
			   ,[UserCreator]
			   ,[DateModified]
			   ,[UserModifier]
			   ,[iTrangThai]
			   ,[idPhuCap]
			   ,[idCachTinhLuong]
			   ,[idNguonNganSach]
			   ,[idMlns]
			   ,[Ma_Cb]
			   ,[ChiTietToi]
			   ,[Nam])
		 SELECT tbl.[Ma_PhuCap]
			   ,tbl.[Ten_PhuCap]
			   ,tbl.[Ma_CachTL]
			   ,tbl.[XauNoiMa]
			   ,tbl.[LNS]
			   ,tbl.[L]
			   ,tbl.[K]
			   ,tbl.[M]
			   ,tbl.[TM]
			   ,tbl.[TTM]
			   ,tbl.[NG]
			   ,tbl.[MoTa]
			   ,tbl.[Ma_NguonNganSach]
			   ,tbl.[NguonNganSach]
			   ,GETDATE()
			   ,@userCreator
			   ,null
			   ,null
			   ,tbl.[iTrangThai]
			   ,tbl.[idPhuCap]
			   ,tbl.[idCachTinhLuong]
			   ,tbl.[idNguonNganSach]
			   ,ml.iID
			   ,tbl.[Ma_Cb]
			   ,tbl.[ChiTietToi]
			   ,@destinationYear 
			   FROM [dbo].[TL_PhuCap_MLNS] as tbl
			   INNER JOIN NS_MucLucNganSach as ml on tbl.XauNoiMa = ml.sXauNoiMa AND ml.iNamLamViec = @destinationYear
			   where nam = @sourceYear;
		End;

	if (@isUpdateDmCapBacKh = 1)
		Begin
			DELETE FROM [TL_DM_CapBac_KeHoach] WHERE [iNamLamViec] = @destinationYear;
			INSERT INTO [dbo].[TL_DM_CapBac_KeHoach]
			   ([Ma_Cb]
			   ,[Ten_Cb]
			   ,[Splits]
			   ,[Parent]
			   ,[Readonly]
			   ,[MoTa]
			   ,[LHT_HS]
			   ,[BHXH_CQ]
			   ,[BHXH_CN]
			   ,[BHYT_CQ]
			   ,[BHYT_CN]
			   ,[BHTN_CQ]
			   ,[BHTN_CN]
			   ,[KPCD_CQ]
			   ,[KPCD_CN]
			   ,[Thoi_Han_Tang]
			   ,[Ma_Cb_KeHoach]
			   ,[Ten_Cb_KeHoach]
			   ,[MoTa_KeHoach]
			   ,[Tuoi_Huu_Nam]
			   ,[Tuoi_Huu_Nu]
			   ,[PCRQ_TT]
			   ,[HsLuongKeHoach]
			   ,[IdHslKeHoach]
			   ,[IdHslHienTai]
			   ,[iNamLamViec])
		SELECT 
			[Ma_Cb]
           ,[Ten_Cb]
           ,[Splits]
           ,[Parent]
           ,[Readonly]
           ,[MoTa]
           ,[LHT_HS]
           ,[BHXH_CQ]
           ,[BHXH_CN]
           ,[BHYT_CQ]
           ,[BHYT_CN]
           ,[BHTN_CQ]
           ,[BHTN_CN]
           ,[KPCD_CQ]
           ,[KPCD_CN]
           ,[Thoi_Han_Tang]
           ,[Ma_Cb_KeHoach]
           ,[Ten_Cb_KeHoach]
           ,[MoTa_KeHoach]
           ,[Tuoi_Huu_Nam]
           ,[Tuoi_Huu_Nu]
           ,[PCRQ_TT]
           ,[HsLuongKeHoach]
           ,[IdHslKeHoach]
           ,[IdHslHienTai]
           ,@destinationYear FROM [TL_DM_CapBac_KeHoach] WHERE [iNamLamViec] = @sourceYear
		End;

	if (@isUpdateNSSKT = 1)
		begin
			DELETE FROM NS_MLSKT_MLNS WHERE [iNamLamViec] = @destinationYear;
			INSERT INTO [dbo].[NS_MLSKT_MLNS]
			   ([sSKT_KyHieu]
			   ,[sNS_XauNoiMa]
			   ,[iNamLamViec]
			   ,[dNgayTao]
			   ,[sNguoiTao]
			   ,[dNgaySua]
			   ,[sNguoiSua]
			   ,[Tag]
			   ,[Log]
			   ,[iTrangThai])
			   SELECT [sSKT_KyHieu]
				   ,[sNS_XauNoiMa]
				   ,@destinationYear
				   ,GETDATE()
				   ,@userCreator
				   ,null
				   ,null
				   ,[Tag]
				   ,[Log]
				   ,[iTrangThai] FROM [NS_MLSKT_MLNS] WHERE [iNamLamViec] = @sourceYear;
		end

	if (@isUpdateCauHinhHeThong = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM DanhMuc WHERE [iNamLamViec] = @destinationYear and sType = 'DM_CauHinh';
				INSERT INTO [dbo].[DanhMuc]
				   ([sType]
				   ,[iID_MaDanhMuc]
				   ,[sTen]
				   ,[sGiaTri]
				   ,[sMoTa]
				   ,[iThuTu]
				   ,[iNamLamViec]
				   ,[iTrangThai]
				   ,[dNgayTao]
				   ,[sNguoiTao]
				   ,[dNgaySua]
				   ,[sNguoiSua]
				   ,[Tag]
				   ,[Log]
				   ,[NganSachNganh])
				   SELECT [sType]
					   ,[iID_MaDanhMuc]
					   ,[sTen]
					   ,[sGiaTri]
					   ,[sMoTa]
					   ,[iThuTu]
					   ,@destinationYear
					   ,[iTrangThai]
					   ,[dNgayTao]
					   ,[sNguoiTao]
					   ,[dNgaySua]
					   ,[sNguoiSua]
					   ,[Tag]
					   ,[Log]
					   ,[NganSachNganh] FROM DanhMuc WHERE [iNamLamViec] = @sourceYear and sType = 'DM_CauHinh';
		end
	if (@isUpdateDanhMucDonViTinh = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM DanhMuc WHERE [iNamLamViec] = @destinationYear and sType = 'DM_DonViTinh';
				INSERT INTO [dbo].[DanhMuc]
				   ([sType]
				   ,[iID_MaDanhMuc]
				   ,[sTen]
				   ,[sGiaTri]
				   ,[sMoTa]
				   ,[iThuTu]
				   ,[iNamLamViec]
				   ,[iTrangThai]
				   ,[dNgayTao]
				   ,[sNguoiTao]
				   ,[dNgaySua]
				   ,[sNguoiSua]
				   ,[Tag]
				   ,[Log]
				   ,[NganSachNganh])
				   SELECT [sType]
					   ,[iID_MaDanhMuc]
					   ,[sTen]
					   ,[sGiaTri]
					   ,[sMoTa]
					   ,[iThuTu]
					   ,@destinationYear
					   ,[iTrangThai]
					   ,[dNgayTao]
					   ,[sNguoiTao]
					   ,[dNgaySua]
					   ,[sNguoiSua]
					   ,[Tag]
					   ,[Log]
					   ,[NganSachNganh] FROM DanhMuc WHERE [iNamLamViec] = @sourceYear and sType = 'DM_DonViTinh';
		end		
		
		if (@isUpdateDanhMucCanCu = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM [dbo].[NS_CauHinh_CanCu] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[NS_CauHinh_CanCu]
				   ([iID_CauHinh_CanCu]
					,[bChinhSua]
					,[iID_MaChucNang]
					,[iNamCanCu]
					,[iNamLamViec]
					,[iThietLap]
					,[sModule]
					,[sTenCot])
				   SELECT NEWID()
					,[bChinhSua]
					,[iID_MaChucNang]
					,[iNamCanCu]
					,@destinationYear
					,[iThietLap]
					,[sModule]
					,[sTenCot] FROM [dbo].[NS_CauHinh_CanCu] WHERE [iNamLamViec] = @sourceYear;
		end	

		if (@isUpdateDanhMucCKTC = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM [dbo].[NS_DanhMucCongKhai] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[NS_DanhMucCongKhai]
					  ([Id]
					  ,[dNgayTao]
					  ,[iNamLamViec]
					  ,[sMoTa]
					  ,[sNguoiSua]
					  ,[sNguoiTao]
					  ,[STT]
					  ,[bHangCha]
					  ,[iID_DMCongKhai_Cha]
					  ,[sMa]
					  ,[sMaCha])
				   SELECT NEWID()
					  ,GETDATE()
					  ,@destinationYear
					  ,[sMoTa]
					  ,[sNguoiSua]
					  ,[sNguoiTao]
					  ,[STT]
					  ,[bHangCha]
					  ,[iID_DMCongKhai_Cha]
					  ,[sMa]
					  ,[sMaCha] FROM [dbo].[NS_DanhMucCongKhai] WHERE [iNamLamViec] = @sourceYear;

				update con
				set con.iID_DMCongKhai_Cha = cha.Id 
				from NS_DanhMucCongKhai con
				join NS_DanhMucCongKhai cha on con.sMaCha = cha.sMa 
				and con.iNamLamViec = cha.iNamLamViec
				where con.iNamLamViec = @destinationYear

				DELETE FROM [dbo].[NS_DMCongKhai_MLNS] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[NS_DMCongKhai_MLNS]
					  ([Id]
					  ,[dNgaySua]
					  ,[dNgayTao]
					  ,[iID_DMCongKhai]
					  ,[iNamLamViec]
					  ,[sNS_XauNoiMa]
					  ,[sNguoiSua]
					  ,[sNguoiTao])
				   SELECT NEWID()
					,GETDATE()
					,GETDATE()
					,[iID_DMCongKhai_NEW]
					,@destinationYear
					,[sNS_XauNoiMa]
					,[sNguoiSua]
					,[sNguoiTao] 
				   FROM (
						select map.*, b.Id [iID_DMCongKhai_NEW] from NS_DMCongKhai_MLNS map
						join NS_DanhMucCongKhai a on map.iID_DMCongKhai = a.Id 
						and map.iNamLamViec = a.iNamLamViec
						join (select * from NS_DanhMucCongKhai where iNamLamViec = @destinationYear) b
						on a.sMa = b.sMa
						where map.iNamLamViec = @sourceYear
					) tab
				WHERE tab.[iNamLamViec] = @sourceYear;
		end	
		
/*			if (@isUpdateDanhMucChuDauTu = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM [dbo].[DM_ChuDauTu] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[DM_ChuDauTu]
				   ([iID_DonVi]
					,[bHangCha]
					,[ChiNhanhNuocNgoai]
					,[dNgaySua]
					,[dNgayTao]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,[iNamLamViec]
					,[iTrangThai]
					,[Loai]
					,[MaSoDVSDNS]
					,[sKyHieu]
					,[sMoTa]
					,[sNguoiSua]
					,[sNguoiTao]
					,[STKNuocNgoai]
					,[STKTrongNuoc]
					,[sTenDonVi])
				   SELECT NEWID()
					,[bHangCha]
					,[ChiNhanhNuocNgoai]
					,[dNgaySua]
					,[dNgayTao]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,@destinationYear
					,[iTrangThai]
					,[Loai]
					,[MaSoDVSDNS]
					,[sKyHieu]
					,[sMoTa]
					,[sNguoiSua]
					,[sNguoiTao]
					,[STKNuocNgoai]
					,[STKTrongNuoc]
					,[sTenDonVi] FROM [dbo].[DM_ChuDauTu] WHERE [iNamLamViec] = @sourceYear;
		end	

			if (@isUpdateDanhMucDonviQuanLyDuAn = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG   VdtDmDonViThucHienDuAn
				DELETE FROM [dbo].[VDT_DM_DonViThucHienDuAn];
				INSERT INTO [dbo].[VDT_DM_DonViThucHienDuAn]
				   ([iID_DonVi]
					,[BHangCha]
					,[iCapDonVi]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,[sDiaChi]
					,[sKyHieu]
					,[sTenDonVi])
				   SELECT NEWID()
					,[BHangCha]
					,[iCapDonVi]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,[sDiaChi]
					,[sKyHieu]
					,[sTenDonVi] FROM [dbo].[VDT_DM_DonViThucHienDuAn];
		end	

			if (@isUpdateDanhMucNhaThau = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG  VdtDmNhaThau
				DELETE FROM [dbo].[VDT_DM_NhaThau] ;
				INSERT INTO [dbo].[VDT_DM_NhaThau]
				   ([Id]
					,[sChucVu]
					,[sDaiDien]
					,[sDiaChi]
					,[sDienThoai]
					,[sDienThoaiLienHe]
					,[sEmail]
					,[sFax]
					,[sMaNganHang]
					,[sMaNhaThau]
					,[sMaSoThue]
					,[sNganHang]
					,[sNguoiLienHe]
					,[sSoTaiKhoan]
					,[sTenNhaThau]
					,[sWebsite]
					,[sSoTaiKhoan2]
					,[sSoTaiKhoan3])
				   SELECT NEWID()
					,[sChucVu]
					,[sDaiDien]
					,[sDiaChi]
					,[sDienThoai]
					,[sDienThoaiLienHe]
					,[sEmail]
					,[sFax]
					,[sMaNganHang]
					,[sMaNhaThau]
					,[sMaSoThue]
					,[sNganHang]
					,[sNguoiLienHe]
					,[sSoTaiKhoan]
					,[sTenNhaThau]
					,[sWebsite]
					,[sSoTaiKhoan2]
					,[sSoTaiKhoan3] FROM [dbo].[VDT_DM_NhaThau];
		end	
		*/

		if (@isUpdateDanhMucBHXH = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_MucLucNganSach WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_MucLucNganSach (
					iID,
					sXauNoiMa,
					sLNS,
					sL,
					sK,
					sM,
					sTM,
					sTTM,
					sNG,
					sTNG,
					sMoTa,
					bHangCha,
					iTrangThai,
					bDuPhong,
					bHangChaDuToan,
					bHangChaQuyetToan,
					bHangMua,
					bHangNhap,
					bHienVat,
					bNgay,
					bPhanCap,
					bSoNguoi,
					bTonKho,
					bTuChi,
					sChiTietToi,
					dNgaySua,
					dNgayTao,
					iLoai,
					iLock,
					iID_MaDonVi,
					iID_MaBQuanLy,
					[Log],
					iID_MLNS,
					iID_MLNS_Cha,
					iNamLamViec,
					sCPChiTietToi,
					sDuToanChiTietToi,
					sNguoiSua,
					sNguoiTao,
					sNhapTheoTruong,
					sQuyetToanChiTietToi,
					Tag,
					sTNG1,
					sTNG2,
					sTNG3,
					iLoaiNganSach,
					sMaCB,
					sMaPhuCap,
					bHangChaDuToanDieuChinh,
					sDuToanDieuChinhChiTietToi,
					iDonViTinh,
					fTyLe_BHXH_NSD,
					fTyLe_BHXH_NLD,
					fTyLe_BHYT_NSD,
					fTyLe_BHYT_NLD,
					fTyLe_BHTN_NSD,
					fTyLe_BHTN_NLD)
				select newid(),
					sXauNoiMa,
					sLNS,
					sL,
					sK,
					sM,
					sTM,
					sTTM,
					sNG,
					sTNG,
					sMoTa,
					bHangCha,
					iTrangThai,
					bDuPhong,
					bHangChaDuToan,
					bHangChaQuyetToan,
					bHangMua,
					bHangNhap,
					bHienVat,
					bNgay,
					bPhanCap,
					bSoNguoi,
					bTonKho,
					bTuChi,
					sChiTietToi,
					dNgaySua,
					getdate(),
					iLoai,
					iLock,
					iID_MaDonVi,
					iID_MaBQuanLy,
					[Log],
					iID_MLNS,
					iID_MLNS_Cha,
					@destinationYear,
					sCPChiTietToi,
					sDuToanChiTietToi,
					sNguoiSua,
					@userCreator,
					sNhapTheoTruong,
					sQuyetToanChiTietToi,
					Tag,
					sTNG1,
					sTNG2,
					sTNG3,
					iLoaiNganSach,
					sMaCB,
					sMaPhuCap,
					bHangChaDuToanDieuChinh,
					sDuToanDieuChinhChiTietToi,
					iDonViTinh,
					fTyLe_BHXH_NSD,
					fTyLe_BHXH_NLD,
					fTyLe_BHYT_NSD,
					fTyLe_BHYT_NLD,
					fTyLe_BHTN_NSD,
					fTyLe_BHTN_NLD
				from BH_DM_MucLucNganSach
				where iNamLamViec = @sourceYear;
		end	

		if (@isUpdateMucLucCacLoaiChi = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_LoaiChi WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_LoaiChi (
					iID,
					sMaLoaiChi,
					sTenDanhMucLoaiChi,
					iNamLamViec,
					dNgaySua,
					dNgayTao,
					sNguoiSua,
					sNguoiTao,
					sMoTa,
					iTrangThai,
					sLNS,
					sDSXauNoiMa)
				select NEWID(),
					sMaLoaiChi,
					sTenDanhMucLoaiChi,
					@destinationYear,
					dNgaySua,
					getdate(),
					sNguoiSua,
					@userCreator,
					sMoTa,
					iTrangThai,
					sLNS,
					sDSXauNoiMa
				from BH_DM_LoaiChi
				where iNamLamViec = @sourceYear
		end	

		if (@isUpdateDanhMucCoSoYTe = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM DM_CoSoYTe WHERE iNamLamViec = @destinationYear;
				INSERT INTO DM_CoSoYTe (
					iID_CoSoYTe,
					iID_MaCoSoYTe,
					iNamLamViec,
					sTenCoSoYTe,
					dNgaySua,
					dNgayTao,
					iTrangThai,
					sNguoiSua,
					sNguoiTao)
				select NEWID(),
					iID_MaCoSoYTe,
					@destinationYear,
					sTenCoSoYTe,
					dNgaySua,
					GETDATE(),
					iTrangThai,
					sNguoiSua,
					@userCreator
				from DM_CoSoYTe
				where iNamLamViec = @sourceYear
		end	

		if (@isUpdateDanhMucTDQT = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_ThamDinhQuyetToan (
					iID,
					iKieuChu,
					iMa,
					iMaCha,
					iNamLamViec,
					iTrangThai,
					sNguoiSua,
					sNguoiTao,
					sNoiDung,
					sSTT,
					iSTT,
					sXauNoiMa,
					ILock)
				select NEWID(),
					iKieuChu,
					iMa,
					iMaCha,
					@destinationYear,
					iTrangThai,
					sNguoiSua,
					@userCreator,
					sNoiDung,
					sSTT,
					iSTT,
					sXauNoiMa,
					ILock
				from BH_DM_ThamDinhQuyetToan
				where iNamLamViec = @sourceYear
		end	
		
		if (@isUpdateDanhMucCHTSBHXH = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_CauHinhThamSo WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_CauHinhThamSo (
					iID,
					bTrangThai,
					dNgaySua,
					dNgayTao,
					iNamLamViec,
					sMa,
					sMoTa,
					sNguoiSua,
					sNguoiTao,
					sTen,
					fGiaTri)
				select NEWID(),
					bTrangThai,
					dNgaySua,
					GETDATE(),
					@destinationYear,
					sMa,
					sMoTa,
					sNguoiSua,
					@userCreator,
					sTen,
					fGiaTri
				from BH_DM_CauHinhThamSo
				where iNamLamViec = @sourceYear
		end	
		if (@isUpdateDanhMucNgayNghi = 1)
		begin
			-- COPY DANH MUC NGAY NGHI
				
				DELETE FROM Tl_DM_NgayNghi WHERE iNamLamViec = @destinationYear;
				INSERT INTO Tl_DM_NgayNghi (
					Id,
					dTuNgay,
					dDenNgay,
					sMaNgayNghi,
					sTenNgayNghi,
					iNamLamViec
					)
				select 
					NEWID(),
					DATEADD(YEAR, @destinationYear-@sourceYear, dTuNgay),
					DATEADD(YEAR, @destinationYear-@sourceYear, dDenNgay),
					sMaNgayNghi,
					sTenNgayNghi,
					@destinationYear
				from Tl_DM_NgayNghi
				where iNamLamViec = @sourceYear
		end
		if (@isUpdateMucLucQuyetToanNam = 1)
		begin
			-- COPY MUC LUC QUYET TOAN NAM
				DELETE FROM NS_MucLucQuyetToanNam WHERE iNamLamViec = @destinationYear;
				INSERT INTO NS_MucLucQuyetToanNam (
					iID,
					bHangCha,
					dNgaySua,
					dNgayTao,
					iTrangThai,
					sMa,
					sMaCha,
					sMoTa,
					iNamLamViec,
					sNguoiSua,
					sNguoiTao,
					sSTT)
				select 
					NEWID(),
					bHangCha,
					dNgaySua,
					GETDATE(),
					iTrangThai,
					sMa,
					sMaCha,
					sMoTa,
					@destinationYear,
					sNguoiSua,
					@userCreator,
					sSTT
				from NS_MucLucQuyetToanNam
				where iNamLamViec = @sourceYear

				-- COPY MUC LUC QUYET TOAN NAM MLNS
				DELETE FROM NS_MucLucQuyetToanNam_MLNS WHERE iNamLamViec = @destinationYear;
				INSERT INTO NS_MucLucQuyetToanNam_MLNS (
					iID,
					dNgaySua,
					dNgayTao,
					sMaMLQT,
					iNamLamViec,
					sNguoiSua,
					sNguoiTao,
					sXauNoiMa)
				select 
					NEWID(),
					dNgaySua,
					GETDATE(),
					sMaMLQT,
					@destinationYear,
					sNguoiSua,
					@userCreator,
					sXauNoiMa
				from NS_MucLucQuyetToanNam_MLNS
				where iNamLamViec = @sourceYear
		end	

END
;
;
;
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_muc_luc_quyet_toan_nam]    Script Date: 11/15/2024 11:37:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_clone_year_muc_luc_quyet_toan_nam]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
AS
BEGIN
	
	-- COPY MUC LUC QUYET TOAN NAM
	insert into NS_MucLucQuyetToanNam (
		iID,
		bHangCha,
		dNgaySua,
		dNgayTao,
		iTrangThai,
		sMa,
		sMaCha,
		sMoTa,
		iNamLamViec,
		sNguoiSua,
		sNguoiTao,
		sSTT)
	select 
		NEWID(),
		bHangCha,
		dNgaySua,
		GETDATE(),
		iTrangThai,
		sMa,
		sMaCha,
		sMoTa,
		@dest,
		sNguoiSua,
		@userCreate,
		sSTT
	from NS_MucLucQuyetToanNam
	where iNamLamViec = @source and sMa not in (select sMa from NS_MucLucQuyetToanNam where iNamLamViec = @dest)

	-- COPY MUC LUC QUYET TOAN NAM MLNS
	insert into NS_MucLucQuyetToanNam_MLNS (
		iID,
		dNgaySua,
		dNgayTao,
		sMaMLQT,
		iNamLamViec,
		sNguoiSua,
		sNguoiTao,
		sXauNoiMa)
	select 
		NEWID(),
		dNgaySua,
		GETDATE(),
		sMaMLQT,
		@dest,
		sNguoiSua,
		@userCreate,
		sXauNoiMa
	from NS_MucLucQuyetToanNam_MLNS
	where iNamLamViec = @source and sXauNoiMa not in (select sXauNoiMa from NS_MucLucQuyetToanNam_MLNS where iNamLamViec = @dest)

END
;
;
;
GO
