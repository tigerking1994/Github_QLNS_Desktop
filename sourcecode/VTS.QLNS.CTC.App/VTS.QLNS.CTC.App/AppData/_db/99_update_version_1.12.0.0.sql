/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet]    Script Date: 28/09/2022 4:37:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kt_khoitaocapphat_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoi_tao_cap_phat]    Script Date: 28/09/2022 4:37:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kt_khoi_tao_cap_phat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kt_khoi_tao_cap_phat]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoi_tao_cap_phat]    Script Date: 28/09/2022 4:37:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_nh_kt_khoi_tao_cap_phat]
AS
BEGIN
    SELECT
		khoiTaoCP.ID				AS Id,
		khoiTaoCP.[dNgayKhoiTao]	AS DNgayKhoiTao,
		khoiTaoCP.[iNamKhoiTao]		AS INamKhoiTao,
		khoiTaoCP.[iID_DonViID]		AS IIdDonViID,
		khoiTaoCP.[iID_MaDonVi]		AS IIdMaDonVi,
		khoiTaoCP.[iID_TiGiaID]		AS IIdTiGiaID,
		khoiTaoCP.[sMoTa]			AS SMoTa,
		khoiTaoCP.[dNgayTao]		AS DNgayTao,
		khoiTaoCP.[sNguoiTao]		AS SNguoiTao,
		khoiTaoCP.[dNgaySua]		AS DNgaySua,
		khoiTaoCP.[sNguoiSua]		AS SNguoiSua,
		khoiTaoCP.[dNgayXoa]		AS DNgayXoa,
		khoiTaoCP.[sNguoiXoa]		AS SNguoiXoa,
		khoiTaoCP.bIsKhoa			AS BIsKhoa,
		khoiTaoCP.[iID_TongHopID] 	AS IIdTongHopID,
		khoiTaoCP.[sTongHop] 		AS STongHop,
		khoiTaoCP.bIsXoa			AS BIsXoa,
		donVi.sTenDonVi				AS STenDonVi
		
	FROM NH_KT_KhoiTaoCapPhat khoiTaoCP
	LEFT JOIN DonVi donVi
		ON khoiTaoCP.iID_DonViID = donVi.iID_DonVi WHERE donvi.iNamLamViec = YEAR(GETDATE())
	ORDER BY khoiTaoCP.[dNgayKhoiTao] DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet]    Script Date: 28/09/2022 4:37:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet] @KhoiTaoCapPhatId NVARCHAR(2000)
AS
BEGIN
    SELECT
		khoiTaoCP_CT.ID									AS Id,
		khoiTaoCP_CT.iID_KhoiTaoCapPhatID				AS IIdKhoiTaoCapPhatID,
		khoiTaoCP_CT.iID_DuAnID							AS IIdDuAnID,
		khoiTaoCP_CT.iID_HopDongID						AS IIdHopDongID,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_USD		AS FQTKinhPhiDuyetCacNamTruoc_USD,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_VND		AS FQTKinhPhiDuyetCacNamTruoc_VND,
		khoiTaoCP_CT.fDeNghiQTNamNay_USD				AS FDeNghiQTNamNay_USD,
		khoiTaoCP_CT.fDeNghiQTNamNay_VND				AS FDeNghiQTNamNay_VND,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_USD			AS FLuyKeKinhPhiDuocCap_USD,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_VND			AS FLuyKeKinhPhiDuocCap_VND,
		khoiTaoCP_CT.iID_ParentID						AS IIdParentID,
		duAn.sTenDuAn									AS STenDuAn,
		hopDong.sTenHopDong								AS STenHopDong
		
	FROM NH_KT_KhoiTaoCapPhat_ChiTiet khoiTaoCP_CT
	LEFT JOIN NH_DA_DuAn duAn
		ON khoiTaoCP_CT.iID_DuAnID = duAn.Id
	LEFT JOIN NH_DA_HopDong hopDong
		ON khoiTaoCP_CT.iID_HopDongID = hopDong.Id
	WHERE khoiTaoCP_CT.iID_KhoiTaoCapPhatID = @KhoiTaoCapPhatId
END
GO
