/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]    Script Date: 10/25/2023 2:02:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]    Script Date: 10/25/2023 2:02:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById] @KhoiTaoCapPhatId NVARCHAR(500)
AS
BEGIN
    SELECT
		khoiTaoCP_CT.ID									AS Id,
		khoiTaoCP_CT.iID_KhoiTaoCapPhatID				AS IIdKhoiTaoCapPhatID,
		khoiTaoCP_CT.iID_DuAnID							AS IIdDuAnID,
		khoiTaoCP_CT.iID_HopDongID						AS IIdHopDongID,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_USD		AS FQTKinhPhiDuyetCacNamTruocUSD,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_VND		AS FQTKinhPhiDuyetCacNamTruocVND,
		khoiTaoCP_CT.fDeNghiQTNamNay_USD				AS FDeNghiQTNamNayUSD,
		khoiTaoCP_CT.fDeNghiQTNamNay_VND				AS FDeNghiQTNamNayVND,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_USD			AS FLuyKeKinhPhiDuocCapUSD,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_VND			AS FLuyKeKinhPhiDuocCapVND,
		khoiTaoCP_CT.iID_ParentID						AS IIdParentID,
		duAn.sMaDuAn									AS SMaDuAn,
		duAn.sTenDuAn									AS STenDuAn,
		hopDong.sSoHopDong								AS SMaHopDong,
		hopDong.sTenHopDong								AS STenHopDong,
		nvc.sMaNhiemVuChi                               AS SMaNhiemVuChi,
		nvc.sTenNhiemVuChi								AS STenNhiemVuChi,
		qdk.sSoQuyetDinh AS SSoQuyetDinhKhac,
		qdk.sTenQuyetDinh AS STenQuyetDinh,
		chiphi.sMaChiPhi AS SMaChiPhi,
		chiphi.sTenChiPhi as STenChiPhi,
		CONCAT(chiphi.sMaChiPhi,'-',chiphi.sTenVietTat) AS STenChiPhiDisplay,
		khoiTaoCP_CT.iLoaiNoiDung AS ILoaiNoiDung,
		khoiTaoCP_CT.sTenNoiDung  AS STenNoiDung,
		khoiTaoCP_CT.sMaNoiDung	AS SMaNoiDung,
		qdDauTu.sSoQuyetDinh AS SSoQuyetDinhDauTu,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD AS FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganTrongNamNay_VND AS FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD AS FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_VND AS FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD AS FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_VND AS FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD AS FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_VND AS FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
		khoiTaoCP_CT.fDeNghiChuyenNamSau_USD AS FDeNghiChuyenNamSauUsd,
		khoiTaoCP_CT.fDeNghiChuyenNamSau_VND AS FDeNghiChuyenNamSauVnd,
		khoiTaoCP_CT.fKinhPhiThuaNopNSNN_USD AS FKinhPhiThuaNopNsnnUSD,
		khoiTaoCP_CT.fKinhPhiThuaNopNSNN_VND AS FKinhPhiThuaNopNsnnVND,
		khoiTaoCP_CT.fConLaiChuaGiaiNgan_USD AS FConLaiChuaGiaiNganUSD,
		khoiTaoCP_CT.fConLaiChuaGiaiNgan_VND FConLaiChuaGiaiNganVND		
		
	FROM NH_KT_KhoiTaoCapPhat_ChiTiet khoiTaoCP_CT
	LEFT JOIN NH_DA_DuAn duAn
		ON khoiTaoCP_CT.iID_DuAnID = duAn.Id
	LEFT JOIN NH_DA_QDDauTu qdDauTu ON qdDauTu.iID_DuAnID = duAn.ID
	LEFT JOIN NH_DA_HopDong hopDong
		ON khoiTaoCP_CT.iID_HopDongID = hopDong.Id
	LEFT JOIN NH_DM_NhiemVuChi nvc on nvc.ID = khoiTaoCP_CT.iID_KHTT_NhiemVuChiID
	LEFT JOIN NH_DA_QuyetDinhKhac qdk ON qdk.ID = khoiTaoCP_CT.iID_QuyetDinhKhacID
	LEFT JOIN NH_DM_ChiPhi chiphi ON chiphi.iID_ChiPhi = khoiTaoCP_CT.iID_ChiPhiID
	WHERE khoiTaoCP_CT.iID_KhoiTaoCapPhatID = @KhoiTaoCapPhatId
	ORDER BY SMaNoiDung
END
;
GO
