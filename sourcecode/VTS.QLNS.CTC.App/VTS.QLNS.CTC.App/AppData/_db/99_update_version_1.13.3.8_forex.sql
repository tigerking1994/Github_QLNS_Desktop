/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]    Script Date: 10/27/2023 4:19:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao]    Script Date: 10/27/2023 4:19:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao]    Script Date: 10/27/2023 4:19:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao] 
	-- Add the parameters for the stored procedure here
	@iIdKhoiTaoQdk uniqueidentifier,
	@type int ---= 1 xoa KT, = 2 xoa ca nhung muc lien quan
AS
BEGIN

IF(@type = 2)
BEGIN
	--duan
	delete NH_DA_DuAn
	where ID IN (select iID_DuAnID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk AND iID_DuAnID is not null);
	
	--qddt
	delete NH_DA_QDDauTu
	where iID_DuAnID IN (select iID_DuAnID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk AND iID_DuAnID is not null);
	 
	 -- qdk
	 delete NH_DA_QuyetDinhKhac
	 where ID IN (select iID_QuyetDinhKhacID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk);
	 
	 --chi tiet qdk
	 delete NH_DA_QuyetDinhKhac_ChiPhi
	 where iID_QuyetDinhKhacID IN (select iID_QuyetDinhKhacID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk );

	 --delete hop dong
	delete NH_DA_HopDong
	where ID IN (select iID_HopDongID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk);

	--delete chi tiet
	delete NH_DA_HopDong_ChiPhi
	where iID_HopDongID IN (select iID_HopDongID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk );

	--delete khoi tao
	DELETE NH_KT_KhoiTaoCapPhat
	where ID = @iIdKhoiTaoQdk;

	DELETE NH_KT_KhoiTaoCapPhat_ChiTiet
	WHERE iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk;
END
ELSE
BEGIN
	DELETE NH_KT_KhoiTaoCapPhat
	where ID = @iIdKhoiTaoQdk;

	DELETE NH_KT_KhoiTaoCapPhat_ChiTiet
	WHERE iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk;
END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]    Script Date: 10/27/2023 4:19:50 PM ******/
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
		qdk.sSoQuyetDinh								AS SSoQuyetDinhKhac,
		qdk.sTenQuyetDinh								AS STenQuyetDinh,
		chiphi.sMaChiPhi								AS SMaChiPhi,
		chiphi.sTenChiPhi								AS STenChiPhi,
		(CASE
			WHEN chiphi.sMaChiPhi is not null AND chiphi.sTenChiPhi IS NOT NULL THEN CONCAT(chiphi.sMaChiPhi,' - ',chiphi.sTenVietTat)
			ELSE CONCAT(chiphi.sMaChiPhi,'',chiphi.sTenVietTat)
			END
		)																	AS STenChiPhiDetail,
		khoiTaoCP_CT.iLoaiNoiDung											AS ILoaiNoiDung,
		khoiTaoCP_CT.sTenNoiDung											AS STenNoiDung,
		khoiTaoCP_CT.sMaNoiDung												AS SMaNoiDung,
		qdDauTu.sSoQuyetDinh												AS SSoQuyetDinhDauTu,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD					AS FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganTrongNamNay_VND					AS FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD			AS FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_VND			AS FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD			AS FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_VND			AS FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD			AS FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_VND			AS FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
		khoiTaoCP_CT.fDeNghiChuyenNamSau_VND								AS FDeNghiChuyenNamSauVnd,
		khoiTaoCP_CT.fDeNghiChuyenNamSau_USD								AS FDeNghiChuyenNamSauUsd,
		khoiTaoCP_CT.fKinhPhiThuaNopNSNN_USD								AS FKinhPhiThuaNopNsnnUSD,
		khoiTaoCP_CT.fKinhPhiThuaNopNSNN_VND								AS FKinhPhiThuaNopNsnnVND,
		khoiTaoCP_CT.fConLaiChuaGiaiNgan_USD								AS FConLaiChuaGiaiNganUSD,
		khoiTaoCP_CT.fConLaiChuaGiaiNgan_VND								AS FConLaiChuaGiaiNganVND	
		INTO #tmppp
		
	FROM NH_KT_KhoiTaoCapPhat_ChiTiet khoiTaoCP_CT
	LEFT JOIN NH_DA_DuAn duAn
		ON khoiTaoCP_CT.iID_DuAnID = duAn.Id
	LEFT JOIN NH_DA_QDDauTu qdDauTu ON qdDauTu.iID_DuAnID = duAn.ID
	LEFT JOIN NH_DA_HopDong hopDong
		ON khoiTaoCP_CT.iID_HopDongID = hopDong.Id
	LEFT JOIN NH_DM_NhiemVuChi nvc on nvc.ID = khoiTaoCP_CT.iID_KHTT_NhiemVuChiID
	LEFT JOIN NH_DA_QuyetDinhKhac qdk ON qdk.ID = khoiTaoCP_CT.iID_QuyetDinhKhacID
	LEFT JOIN NH_DM_ChiPhi chiphi ON chiphi.iID_ChiPhi = khoiTaoCP_CT.iID_ChiPhiID
	WHERE khoiTaoCP_CT.iID_KhoiTaoCapPhatID = @KhoiTaoCapPhatId;
	--ORDER BY OrderName
	

	With #treeViews
	AS
	(
		SELECT *, CAST( ROW_NUMBER() OVER(ORDER by SMaNoiDung) AS nvarchar(max)) AS position FROM #tmppp where IIdParentID IS NULL OR ILoaiNoiDung = 1
		UNION ALL
		SELECT 
				chil.Id,
				chil.IIdKhoiTaoCapPhatID,
				chil.IIdDuAnID,
				chil.IIdHopDongID,
				chil.FQTKinhPhiDuyetCacNamTruocUSD,
				chil.FQTKinhPhiDuyetCacNamTruocVND,
				chil.FDeNghiQTNamNayUSD,
				chil.FDeNghiQTNamNayVND,
				chil.FLuyKeKinhPhiDuocCapUSD,
				chil.FLuyKeKinhPhiDuocCapVND,
				chil.IIdParentID,
				chil.SMaDuAn,
				chil.STenDuAn,
				chil.SMaHopDong,
				chil.STenHopDong,
				chil.SMaNhiemVuChi,
				chil.STenNhiemVuChi,
				chil.SSoQuyetDinhKhac,
				chil.STenQuyetDinh,
				chil.SMaChiPhi,
				chil.STenChiPhi,
				chil.STenChiPhiDetail,
				chil.ILoaiNoiDung,
				chil.STenNoiDung,
				chil.SMaNoiDung,
				chil.SSoQuyetDinhDauTu,
				chil.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
				chil.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
				chil.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
				chil.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
				chil.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
				chil.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
				chil.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
				chil.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
				chil.FDeNghiChuyenNamSauVnd,
				chil.FDeNghiChuyenNamSauUsd,
				chil.FKinhPhiThuaNopNsnnUSD,
				chil.FKinhPhiThuaNopNsnnVND,
				chil.FConLaiChuaGiaiNganUSD,
				chil.FConLaiChuaGiaiNganVND,
				CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.SMaNoiDung) AS NVARCHAR(MAX))) AS position
		
		FROM #treeViews pr
		INNER JOIN #tmppp chil ON pr.Id = chil.IIdParentID

	)

		SELECT *,
		 	cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		 FROM  #treeViews
		 ORDER  BY sort;

	DROP TABLE  #tmppp;
END
;
GO
