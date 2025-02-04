/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_index]    Script Date: 11/14/2023 5:52:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_index]    Script Date: 11/14/2023 5:52:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_qddautu_index]
	@YearOfWork int,
	@iLoai int
AS BEGIN
	WITH SoLieuDieuChinh AS (
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM  NH_DA_QDDauTu ct 
		WHERE ct.iID_ParentId IS NOT NULL 
		UNION ALL 
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_QDDauTu ct 
		JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.ID 
	  WHERE 
		ct.iID_ParentId IS NOT NULL
	), 
	SoLanDieuChinh AS (
		SELECT 
			sdc.Id, 
			sdc.iID_ParentId, 
			COUNT(sdc.Id) AS iSoLanDieuChinh 
		FROM SoLieuDieuChinh sdc 
		GROUP BY 
			sdc.iID_ParentId, 
			sdc.ID
	),
	ThongTinNguonVon AS (
		SELECT 
			quyetDinhTuNguonVon.iID_QDDauTuID AS iID_QDDauTuID, 
			SUM(quyetDinhTuNguonVon.fGiaTriNgoaiTeKhac) AS fGiaTriNgoaiTeKhac,
			SUM(quyetDinhTuNguonVon.fGiaTriUSD) AS fGiaTriUSD,
			SUM(quyetDinhTuNguonVon.fGiaTriVND) AS fGiaTriVND,
			SUM(quyetDinhTuNguonVon.fGiaTriEUR) AS fGiaTriEUR
		FROM NH_DA_QDDauTu_NguonVon quyetDinhTuNguonVon
		GROUP BY 
			quyetDinhTuNguonVon.iID_QDDauTuID
	)
	SELECT
		qdDauTu.ID AS Id,
		qdDauTu.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		qdDauTu.iID_DuAnID AS IIdDuAnId,
		qdDauTu.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		qdDauTu.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		qdDauTu.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		qdDauTu.sSoQuyetDinh AS SSoQuyetDinh,
		qdDauTu.dNgayQuyetDinh AS DNgayQuyetDinh,
		qdDauTu.sMota AS SMota,
		qdDauTu.iID_ChuDauTuID AS IIdChuDauTuId,
		qdDauTu.iID_MaChuDauTu AS IIdMaChuDauTu,
		qdDauTu.sKhoiCong AS SKhoiCong,
		qdDauTu.sKetThuc AS SKetThuc,
		qdDauTu.sDiaDiem AS SDiaDiem,
		thongTinNguonVon.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		thongTinNguonVon.fGiaTriUSD AS FGiaTriUsd,
		thongTinNguonVon.fGiaTriVND AS FGiaTriVnd,
		thongTinNguonVon.fGiaTriEUR AS FGiaTriEur,
		qdDauTu.dNgayTao AS DNgayTao,
		qdDauTu.sNguoiTao AS sNguoiTao,
		qdDauTu.dNgaySua AS DNgaySua,
		qdDauTu.sNguoiSua AS SNguoiSua,
		qdDauTu.dNgayXoa AS DNgayXoa,
		qdDauTu.sNguoiXoa AS SNguoiXoa,
		qdDauTu.bIsActive AS BIsActive,
		qdDauTu.bIsGoc AS BIsGoc,
		qdDauTu.bIsKhoa AS BIsKhoa,
		qdDauTu.bIsXoa AS BIsXoa,
		qdDauTu.iID_TiGiaID AS IIdTiGiaId,
		qdDauTu.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		qdDauTu.iID_ParentID AS IIdParentId,
		CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 405 AND ObjectId = qdDauTu.ID) AS TotalFiles,
		qdDauTuParent.sSoQuyetDinh AS SDieuChinhTu,
		nvc.sMaNhiemVuChi as STenChuongTrinh
	FROM NH_DA_QDDauTu qdDauTu
	LEFT JOIN NH_DA_QDDauTu qdDauTuParent
		ON qdDauTu.iID_ParentID = qdDauTuParent.ID
	LEFT JOIN ThongTinNguonVon thongTinNguonVon
		ON qdDauTu.ID = thongTinNguonVon.iID_QDDauTuID
	INNER JOIN donVi 
		ON donVi.iID_DonVi = qdDauTu.iID_DonViQuanLyID
	INNER JOIN NH_DA_DuAn duAn 
		ON duAn.ID = qdDauTu.iID_DuAnID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = qdDauTu.ID
	LEFT JOIN NH_KHTongThe_NhiemVuChi khtt ON qdDauTu.iID_KHTT_NhiemVuChiID = khtt.ID
	LEFT JOIN NH_DM_NhiemVuChi nvc ON nvc.ID = khtt.iID_NhiemVuChiID
	WHERE qdDauTu.iLoai = @iLoai
	ORDER BY qdDauTu.dNgayQuyetDinh DESC, qdDauTu.sSoQuyetDinh DESC
END
;
;
GO
