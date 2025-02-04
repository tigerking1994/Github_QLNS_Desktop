/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 7/8/2024 8:21:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ke_hoach_thu_mua_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]    Script Date: 7/8/2024 8:21:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 7/8/2024 8:21:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_thongtinkehoachthu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]    Script Date: 7/8/2024 8:21:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]    Script Date: 7/8/2024 8:21:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	ct.iID_QTTM_BHYT_ChungTu,
	ct.iID_MaDonVi IIDMaDonVi,
	dv.sTenDonVi AS sTenDonVi,
	ct.iNamLamViec,
	ct.iLoaiTongHop,
	ct.bDaTongHop,
	ct.bIsKhoa,
	ct.sSoChungTu,
	ct.dNgayChungTu,
	ct.iQuyNam,
	ct.iQuyNamLoai,
	ct.sQuyNamMoTa,
	ct.sMoTa,
	ct.sNguoiTao,
	ct.sNguoiSua,
	ct.dNgayTao,
	ct.dNgaySua,
	ct.sDS_MLNS SDsMlns,
	ct.sTongHop,
	ct.fDuToan,
	ct.fDaQuyetToan,
	ct.fConLai,
	ct.fSoPhaiThu
	
	FROM BH_QTTM_BHYT_Chung_Tu ct
	LEFT JOIN DonVi dv
	ON ct.iID_MaDonVi = dv.iID_MaDonVi
	WHERE dv.iNamLamViec = @YearOfWork AND ct.iNamLamViec = @YearOfWork
	ORDER BY ct.iQuyNamLoai, ct.iQuyNam
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 7/8/2024 8:21:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index] 
	@YearOfWork int
AS
BEGIN
	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamLamViec,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua,
	KHT.iLoaiTongHop,
	KHT.sTongHop,
	KHT.fBHXH_NLD FThuBHXHNLDDong,
	KHT.fBHXH_NSD FThuBHXHNSDDong,
	KHT.fTongBHXH FThuBHXH,
	KHT.fBHYT_NLD FThuBHYTNLDDong,
	KHT.fBHYT_NSD FThuBHYTNSDDong,
	KHT.fTongBHYT FTongBHYT,
	KHT.fBHTN_NLD FThuBHTNNLDDong,
	KHT.fBHTN_NSD FThuBHTNNSDDong,
	KHT.fTongBHTN FThuBHTN,
	KHT.fTong,
	KHT.bDaTongHop,
	KHT.sBangLuongKeHoach SBangLuongKeHoach
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHT.iNamLamViec = @YearOfWork
	ORDER BY KHT.sSoChungTu
END;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]    Script Date: 7/8/2024 8:21:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]
@NamLamViec int,
@Quy int,
@LoaiChungTu int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM (
	SELECT DISTINCT chungtu.* FROM 
	BH_QTC_Quy_CheDoBHXH chungtu
	join BH_QTC_Quy_CheDoBHXH_ChiTiet ctct ON chungtu.ID_QTC_Quy_CheDoBHXH = ctct.iID_QTC_Quy_CheDoBHXH
	WHERE chungtu.iNamChungTu = @NamLamViec
	  AND chungtu.iQuyChungTu = @Quy
	  AND isnull(chungtu.iID_MaDonVi, '') <> ''
	) chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 7/8/2024 8:21:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	KHTM.iID_KHTM_BHYT,
	KHTM.sSoChungTu,
	KHTM.iNamLamViec,
	KHTM.dNgayChungTu,
	KHTM.iID_MaDonVi IIDMaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHTM.sMoTa,
	KHTM.bKhoa,
	KHTM.fTongKeHoach,
	KHTM.sSoQuyetDinh,
	KHTM.dNgayQuyetDinh,
	KHTM.sNguoiTao,
	KHTM.sNguoiSua,
	KHTM.dNgayTao,
	KHTM.dNgaySua,
	KHTM.iLoaiTongHop,
	KHTM.sTongHop,
	KHTM.iTongSoNguoi,
	KHTM.iTongSoThang,
	KHTM.fTongDinhMuc,
	KHTM.fTongThanhTien,
	KHTM.bDaTongHop
	FROM BH_KHTM_BHYT KHTM
	LEFT JOIN DonVi DV
	ON KHTM.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHTM.iNamLamViec = @YearOfWork
	ORDER BY KHTM.sSoChungTu
END
;
;
GO



DELETE FROM DM_ChuKy WHERE sLoai = 'QTC_KINHPHIKHAC_QUY';
GO
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptBH_QTC_QKPK_MacDinh_KeHoach', NULL, N'rptBH_QTC_QKPK_MacDinh_KeHoach', NULL, NULL, NULL, NULL, NULL, N'QTC_KINHPHIKHAC_QUY', NULL, N'Báo cáo quyết toán chi quý kinh phí khác', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO




