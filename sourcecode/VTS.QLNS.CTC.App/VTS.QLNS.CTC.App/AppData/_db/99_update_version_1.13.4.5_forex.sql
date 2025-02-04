/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]    Script Date: 11/3/2023 7:30:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_nh_da_quyetdinhkhac_index]    Script Date: 11/3/2023 7:30:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_nh_da_quyetdinhkhac_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_nh_da_quyetdinhkhac_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_FindByIdKhTongTheAndMaDonViID]    Script Date: 11/3/2023 7:30:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_FindByIdKhTongTheAndMaDonViID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_FindByIdKhTongTheAndMaDonViID]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kehoachtongthe_index]    Script Date: 11/8/2023 10:27:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kehoachtongthe_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kehoachtongthe_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_FindByIdKhTongTheAndMaDonViID]    Script Date: 11/3/2023 7:30:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROC [dbo].[sp_FindByIdKhTongTheAndMaDonViID]
	@IdKhTongThe uniqueidentifier,
	@MaDonVi uniqueidentifier
AS BEGIN

	   SELECT 
            tt_nvc.ID,
            tt_nvc.iID_KHTongTheID, 
            tt_nvc.iID_NhiemVuChiID, 
            tt_nvc.iID_DonViThuHuongID,
            donvi.sTenDonVi AS STenDonVi,
            donvi.iID_MaDonVi AS SMaDonViThuHuong, 
            tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, 
            tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, 
            nvc.sMaNhiemVuChi, 
            nvc.sTenNhiemVuChi, 
            nvc.iLoaiNhiemVuChi  
        FROM NH_KHTongThe_NhiemVuChi tt_nvc 
        JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  
        JOIN DonVi donvi on DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID 
        WHERE 
         tt_nvc.iID_KHTongTheID = @IdKhTongThe 
         AND tt_nvc.iID_DonViThuHuongID = @MaDonVi 
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_get_nh_da_quyetdinhkhac_index]    Script Date: 11/3/2023 7:30:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_nh_da_quyetdinhkhac_index]
	-- Add the parameters for the stored procedure here
	@iThuocMenu int
AS
BEGIN

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *, 
			qdk.ID as Id,
			qdk.iID_ParentID as IIdParentId,
			qdk.iID_DonViQuanLyID,
			dv.iID_MaDonVi as IIdMaDonViQuanLy,
			dv.sTenDonVi as STenDonVi,
			qdk.sTenQuyetDinh as STenQuyetDinh,
			qdk.sSoQuyetDinh as SSoQuyetDinh,
			qdk.sMoTa as SMoTa,
			qdk.iID_KHTT_NhiemVuChiID,
			nvChi.STenChuongTrinh,
			qdk.bIsActive AS BIsActive,
			qdk.bIsGoc AS BIsGoc,
			qdk.bIsKhoa AS BIsKhoa,
			qdk.bIsXoa AS BIsXoa ,
			qdk.dNgayQuyetDinh as DNgayQuyetDinh,
			qdk.dNgayTao as DNgayTao,
			qdk.sNguoiTao as SNguoiTao,
			qdk.dNgaySua as DNgaySua,
			qdk.sNguoiSua as SNguoiSua,
			qdk.dNgayXoa as  DNgayXoa,
			qdk.sNguoiXoa as SNguoiXoa,
			qdk.iLanDieuChinh as ILanDieuChinh,
			qdk.iThuocMenu as IThuocMenu,
			qdk.fGiaTriUSD as FGiaTriUSD,
			qdk.fGiaTriVND as FGiaTriVND


	FROM NH_DA_QuyetDinhKhac qdk
	LEFT  JOIN DonVi dv on dv.iID_DonVi = qdk.iID_DonViQuanLyID
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON qdk.iID_KHTT_NhiemVuChiID = nvChi.ID

	WHERE qdk.iThuocMenu = @iThuocMenu
	ORDER BY qdk.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]    Script Date: 11/3/2023 7:30:30 AM ******/
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
		hopDong.sSoHopDong								AS SSoHopDong,
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
		ON khoiTaoCP_CT.iID_HopDongID = hopDong.ID
	LEFT JOIN NH_DA_QuyetDinhKhac qdk ON qdk.ID = khoiTaoCP_CT.iID_QuyetDinhKhacID
	LEFT JOIN NH_DM_ChiPhi chiphi ON chiphi.iID_ChiPhi = khoiTaoCP_CT.iID_ChiPhiID
	LEFT JOIN NH_KHTongThe_NhiemVuChi khtt on khtt.ID = khoiTaoCP_CT.iID_KHTT_NhiemVuChiID
	LEFT JOIN NH_DM_NhiemVuChi nvc on nvc.ID = khtt.iID_NhiemVuChiID

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
				chil.SSoHopDong,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kehoachtongthe_index]    Script Date: 11/8/2023 10:27:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ALTER TABLE NH_KHTongThe
--ADD iGiaiDoanTu_TTCP int,
--    iGiaiDoanDen_TTCP int,
--	iGiaiDoanTu_BQP int,
--	iGiaiDoanDen_BQP int;

CREATE PROC [dbo].[sp_nh_kehoachtongthe_index]
AS
BEGIN
	WITH ListAllTongThe AS (
		SELECT NEWID() AS id, NULL AS iNamKeHoach,
			NULL AS iGiaiDoanTu, NULL AS iGiaiDoanDen,
			NULL AS iGiaiDoanTu_TTCP, NULL AS iGiaiDoanDen_TTCP,
			NULL AS iGiaiDoanTu_BQP, NULL AS iGiaiDoanDen_BQP,
			NULL AS iIdParentId,
			NULL AS iIdParentAdjustId,
			NULL AS iIdGocId, t.sSoKeHoachTTCP,
			NULL AS dNgayKeHoachTTCP,
			NULL AS sMoTaChiTietKhttcp,
			SUM(t.fTongGiaTri_KHTTCP) fTongGiaTriKhttcp,
			NULL AS sSoKeHoachBQP, NULL AS dNgayKeHoachBQP,
			NULL AS sMoTaChiTietKhbqp,
			SUM(t.fTongGiaTri_KHBQP) AS fTongGiaTriKhbqp,
			SUM(t.fTongGiaTri_KHBQP_VND) AS fTongGiaTriKhbqpVnd,
			Max(t.dNgayTao) AS dNgayTao, NULL AS sNguoiTao,
			NULL AS dNgaySua, NULL AS sNguoiSua,
			NULL AS dNgayXoa, NULL AS sNguoiXoa,
			NULL AS bIsActive, NULL AS bIsGoc, NULL AS bIsKhoa,
			NULL AS iLanDieuChinh, NULL AS iLoai,
			NULL AS TotalFiles,
			NULL AS DieuChinhTu,
			NULL AS IdParentTongThe
		FROM NH_KHTongThe AS t
		GROUP BY t.sSoKeHoachTTCP
		
		UNION ALL

		SELECT  t.ID id, t.iNamKeHoach iNamKeHoach,
				t.iGiaiDoanTu_TTCP, t.iGiaiDoanDen_TTCP,
				t.iGiaiDoanTu, t.iGiaiDoanDen,
				t.iGiaiDoanTu_BQP, t.iGiaiDoanDen_BQP,
				t.iID_ParentID iIdParentId,
				t.iID_ParentAdjustID iIdParentAdjustId,
				t.iID_GocID iIdGocId, t.sSoKeHoachTTCP,
				t.dNgayKeHoachTTCP,
				t.sMoTaChiTiet_KHTTCP sMoTaChiTietKhttcp,
				t.fTongGiaTri_KHTTCP fTongGiaTriKhttcp,
				t.sSoKeHoachBQP, t.dNgayKeHoachBQP,
				t.sMoTaChiTiet_KHBQP sMoTaChiTietKhbqp,
				t.fTongGiaTri_KHBQP fTongGiaTriKhbqp,
				t.fTongGiaTri_KHBQP_VND fTongGiaTriKhbqpVnd,
				t.dNgayTao, t.sNguoiTao,
				t.dNgaySua, t.sNguoiSua,
				t.dNgayXoa, t.sNguoiXoa,
				t.bIsActive, t.bIsGoc, t.bIsKhoa,
				t.iLanDieuChinh, t.iLoai,
				(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 402 AND ObjectId = t.ID) AS TotalFiles,
				CASE WHEN t.iID_ParentAdjustID is null THEN '' ELSE ( SELECT khpr.sSoKeHoachBQP FROM NH_KHTongThe khpr WHERE khpr.ID = t.iID_ParentAdjustID ) END DieuChinhTu,
				t.ID AS IdParentTongThe
		FROM NH_KHTongThe AS t
	)
	SELECT * FROM ListAllTongThe Order by dNgayTao DESC;
END;
;
;
GO

