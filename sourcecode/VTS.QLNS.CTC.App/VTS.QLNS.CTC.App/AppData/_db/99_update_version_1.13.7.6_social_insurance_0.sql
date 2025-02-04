/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 1/3/2024 4:17:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ke_hoach_thu_mua_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan]    Script Date: 1/3/2024 4:17:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_thamdinhquyettoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_thamdinhquyettoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]    Script Date: 1/3/2024 4:17:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_bhxh_index]    Script Date: 1/3/2024 4:17:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_thu_bhxh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_thu_bhxh_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_index]    Script Date: 1/3/2024 4:17:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dieu_chinh_du_toan_thu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_index]    Script Date: 1/3/2024 4:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_index]
	@YearOfWork int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		dcdt.iID_DTT_BHXH_DieuChinh
		  ,dcdt.iNamLamViec
		  ,dcdt.sSoChungTu
		  ,dcdt.dNgayChungTu
		  ,dcdt.sMoTa
		  ,dcdt.iID_MaDonVi
		  ,dcdt.iID_DonVi
		  ,dcdt.bIsKhoa
		  ,dcdt.sLNS
		  ,dcdt.iLoaiTongHop
		  ,dcdt.sTongHop
		  ,dcdt.sNguoiTao
		  ,dcdt.sNguoiSua
		  ,dcdt.dNgayTao
		  ,dcdt.dNgaySua
		  ,dcdt.fThuBHXH_NLD
		  ,dcdt.fThuBHXH_NSD
		  ,dcdt.fThuBHYT_NLD
		  ,dcdt.fThuBHYT_NSD
		  ,dcdt.fThuBHTN_NLD
		  ,dcdt.fThuBHTN_NSD
		  ,dcdt.fThuBHXH_NLD_QTDauNam
		  ,dcdt.fThuBHXH_NSD_QTDauNam
		  ,dcdt.fThuBHYT_NLD_QTDauNam
		  ,dcdt.fThuBHYT_NSD_QTDauNam
		  ,dcdt.fThuBHTN_NLD_QTDauNam
		  ,dcdt.fThuBHTN_NSD_QTDauNam
		  ,dcdt.fThuBHXH_NLD_QTCuoiNam
		  ,dcdt.fThuBHXH_NSD_QTCuoiNam
		  ,dcdt.fThuBHYT_NLD_QTCuoiNam
		  ,dcdt.fThuBHYT_NSD_QTCuoiNam
		  ,dcdt.fThuBHTN_NLD_QTCuoiNam
		  ,dcdt.fThuBHTN_NSD_QTCuoiNam
		  ,dcdt.fTongThuBHXH_NLD
		  ,dcdt.fTongThuBHXH_NSD
		  ,dcdt.fTongThuBHYT_NLD
		  ,dcdt.fTongThuBHYT_NSD
		  ,dcdt.fTongThuBHTN_NLD
		  ,dcdt.fTongThuBHTN_NSD
		  ,dcdt.fTongCong
		  ,dcdt.fThuBHXH_NLD_Tang
		  ,dcdt.fThuBHXH_NLD_Giam
		  ,dcdt.fThuBHXH_NSD_Tang
		  ,dcdt.fThuBHXH_NSD_Giam
		  ,dcdt.fThuBHXH_Tang
		  ,dcdt.fThuBHXH_Giam
		  ,dcdt.fThuBHYT_NLD_Tang
		  ,dcdt.fThuBHYT_NLD_Giam
		  ,dcdt.fThuBHYT_NSD_Tang
		  ,dcdt.fThuBHYT_NSD_Giam
		  ,dcdt.fThuBHYT_Tang
		  ,dcdt.fThuBHYT_Giam
		  ,dcdt.fThuBHTN_NLD_Tang
		  ,dcdt.fThuBHTN_NLD_Giam
		  ,dcdt.fThuBHTN_NSD_Tang
		  ,dcdt.fThuBHTN_NSD_Giam
		  ,dcdt.fThuBHTN_Tang
		  ,dcdt.fThuBHTN_Giam
		  ,dcdt.iID_TongHopID
		  ,dcdt.bDaTongHop
		  , donVi.sTenDonVi
		  , dcdt.bIsKhoa
		-- Tong dự toán todo
	FROM BH_DTT_BHXH_DieuChinh dcdt
	LEFT JOIN DonVi donVi
		ON dcdt.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = dcdt.iID_DonVi
	WHERE dcdt.iNamLamViec = @YearOfWork AND donVi.iNamLamViec = @YearOfWork;
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_bhxh_index]    Script Date: 1/3/2024 4:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_thu_bhxh_index] 
	@YearOfWork int
AS
BEGIN
	SELECT
	ct.iID_QTT_BHXH_ChungTu,
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
	ct.iQSBQNam,
	ct.fLuongChinh,
	ct.fPCChucVu,
	ct.fPCTNNghe,
	ct.fPCTNVuotKhung,
	ct.fNghiOm,
	ct.fHSBL,
	ct.fTongQTLN,
	ct.fDuToan,
	ct.fDaQuyetToan,
	ct.fConLai,
	ct.fThu_BHXH_NLD FThuBHXHNLD,
	ct.fThu_BHXH_NSD FThuBHXHNSD,
	ct.fTongSoPhaiThuBHXH,
	ct.fThu_BHYT_NLD FThuBHYTNLD,
	ct.fThu_BHYT_NSD FThuBHYTNSD,
	ct.fTongSoPhaiThuBHYT,
	ct.fThu_BHTN_NLD FThuBHTNNLD,
	ct.fThu_BHTN_NSD FThuBHTNNSD,
	ct.fTongSoPhaiThuBHTN,
	ct.fTongCong,
	(ct.fThu_BHXH_NLD + ct.fThu_BHYT_NLD + ct.fThu_BHTN_NLD) FSoPhaiThuBHXHNLD,
	(ct.fThu_BHXH_NSD + ct.fThu_BHYT_NSD + ct.fThu_BHTN_NSD) FSoPhaiThuBHXHNSD,
	(ct.fThu_BHXH_NLD + ct.fThu_BHYT_NLD + ct.fThu_BHTN_NLD + ct.fThu_BHXH_NSD + ct.fThu_BHYT_NSD + ct.fThu_BHTN_NSD) FTongPhaiThuBHXH
	
	FROM BH_QTT_BHXH_ChungTu ct
	LEFT JOIN DonVi dv
	ON ct.iID_MaDonVi = dv.iID_MaDonVi
	WHERE dv.iNamLamViec = @YearOfWork AND ct.iNamLamViec = @YearOfWork;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]    Script Date: 1/3/2024 4:17:23 PM ******/
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
END
;


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan]    Script Date: 1/3/2024 4:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_thamdinhquyettoan]
@INamLamViec int
AS
BEGIN
	SELECT
		ct.iID_BH_TDQT_ChungTu,
		ct.iNamLamViec,
		ct.iID_MaDonVi,
		ct.fSoBaoCao,
		ct.fSoThamDinh,
		ct.fQuanNhan,
		ct.fCNVLDHD,
		ct.sSoChungTu,
		ct.dNgayChungTu,
		ct.bDaTongHop,
		ct.sTongHop,
		ct.bKhoa,
		ct.sNguoiTao,
		ct.sNguoiSua,
		ct.dNgayTao,
		ct.dNgaySua,
		ct.sMoTa,
		dv.sTenDonVi
	FROM BH_ThamDinhQuyetToan_ChungTu ct
	INNER JOIN DonVi dv ON ct.iID_MaDonVi = dv.iID_MaDonVi AND ct.iNamLamViec = dv.iNamLamViec
	WHERE ct.iNamLamViec = @INamLamViec
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 1/3/2024 4:17:23 PM ******/
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
	ORDER BY KHTM.dNgayQuyetDinh DESC
END
;
GO
