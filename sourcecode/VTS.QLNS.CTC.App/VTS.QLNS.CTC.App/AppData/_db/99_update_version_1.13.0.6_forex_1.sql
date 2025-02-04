/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinduan_index]    Script Date: 7/31/2023 8:25:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtinduan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtinduan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 7/31/2023 8:25:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 7/31/2023 8:25:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 7/31/2023 8:25:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chutruongdautu_index]    Script Date: 7/31/2023 8:25:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chutruongdautu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chutruongdautu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chutruongdautu_index]    Script Date: 7/31/2023 8:25:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_chutruongdautu_index]
	@YearOfWork int,
	@iLoai int
AS BEGIN

	WITH SoLieuDieuChinh AS (
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM  NH_DA_ChuTruongDauTu ct 
		WHERE ct.iID_ParentId IS NOT NULL 
		UNION ALL 
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_ChuTruongDauTu ct 
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
			chuTruongDauTuNguonVon.iID_ChuTruongDauTuID AS iID_ChuTruongDauTuID, 
			SUM(chuTruongDauTuNguonVon.fGiaTriNgoaiTeKhac) AS fGiaTriNgoaiTeKhac,
			SUM(chuTruongDauTuNguonVon.fGiaTriUSD) AS fGiaTriUSD,
			SUM(chuTruongDauTuNguonVon.fGiaTriVND) AS fGiaTriVND,
			SUM(chuTruongDauTuNguonVon.fGiaTriEUR) AS fGiaTriEUR
		FROM NH_DA_ChuTruongDauTu_NguonVon chuTruongDauTuNguonVon
		GROUP BY 
			chuTruongDauTuNguonVon.iID_ChuTruongDauTuID
	)
	SELECT
		chuTruongDauTu.ID AS Id,
		chuTruongDauTu.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		chuTruongDauTu.iID_DuAnID AS IIdDuAnId,
		chuTruongDauTu.iID_ChuDauTuID AS IIdChuDauTuId,
		chuTruongDauTu.iID_MaChuDauTu AS IIdMaChuDauTu,
		chuTruongDauTu.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		chuTruongDauTu.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		chuTruongDauTu.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		chuTruongDauTu.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndId,
		chuTruongDauTu.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurId,
		chuTruongDauTu.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		chuTruongDauTu.iID_ParentID AS IIdParentId,
		chuTruongDauTu.sSoQuyetDinh AS SSoQuyetDinh,
		chuTruongDauTu.dNgayQuyetDinh AS DNgayQuyetDinh,
		chuTruongDauTu.sMota AS SMoTa,
		chuTruongDauTu.sKhoiCong AS SKhoiCong,
		chuTruongDauTu.sKetThuc AS SKetThuc,
		chuTruongDauTu.sDiaDiem AS SDiaDiem,
		chuTruongDauTu.sMucTieu AS SMucTieu,
		chuTruongDauTu.sQuyMo AS SQuyMo,
		ISNULL(nguonVon.fGiaTriNgoaiTeKhac, 0) AS FGiaTriNgoaiTeKhac,
		ISNULL(nguonVon.fGiaTriUSD, 0) AS FGiaTriUSD,
		ISNULL(nguonVon.fGiaTriVND, 0) AS FGiaTriVND,
		ISNULL(nguonVon.fGiaTriEUR, 0) AS FGiaTriEUR,
		chuTruongDauTu.dNgayTao AS DNgayTao,
		chuTruongDauTu.sNguoiTao AS SNguoiTao,
		chuTruongDauTu.dNgaySua AS DNgaySua,
		chuTruongDauTu.sNguoiSua AS SNguoiSua,
		chuTruongDauTu.dNgayXoa AS DNgayXoa,
		chuTruongDauTu.sNguoiXoa AS SNguoiXoa,
		chuTruongDauTu.bIsActive AS BIsActive,
		chuTruongDauTu.bIsGoc AS BIsGoc,
		chuTruongDauTu.bIsKhoa AS BIsKhoa,
		chuTruongDauTu.bIsXoa AS BIsXoa,
		CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		chuTruongDauTuParent.sSoQuyetDinh AS SDieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 404 AND ObjectId = chuTruongDauTu.ID) AS TotalFiles ,
		nvChi.iID_NhiemVuChiID as iIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
	FROM NH_DA_ChuTruongDauTu chuTruongDauTu
	LEFT JOIN NH_DA_ChuTruongDauTu chuTruongDauTuParent
		ON chuTruongDauTu.iID_ParentID = chuTruongDauTuParent.ID
	LEFT JOIN DonVi donVi
		ON chuTruongDauTu.iID_MaDonViQuanLy = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NH_DA_DuAn duAn
		ON chuTruongDauTu.iID_DuAnID = duAn.ID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = chuTruongDauTu.ID
	LEFT JOIN ThongTinNguonVon nguonVon
		ON nguonVon.iID_ChuTruongDauTuID = chuTruongDauTu.ID
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON chuTruongDauTu.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE chuTruongDauTu.iLoai = @iLoai
	ORDER BY chuTruongDauTu.dNgayQuyetDinh DESC, chuTruongDauTu.sSoQuyetDinh DESC;
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 7/31/2023 8:25:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_index] 
	@YearOfWork int,
	@ILoai int
AS
BEGIN
	
	WITH SoLieuDieuChinh AS (
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
		WHERE ct.iID_ParentId IS NOT NULL 
		UNION ALL 
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
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
			nguonVon.iID_DuToanID AS iID_DuToanID, 
			SUM(nguonVon.fGiaTriNgoaiTeKhac) AS fGiaTriNgoaiTeKhac,
			SUM(nguonVon.fGiaTriUSD) AS fGiaTriUSD,
			SUM(nguonVon.fGiaTriVND) AS fGiaTriVND,
			SUM(nguonVon.fGiaTriEUR) AS fGiaTriEUR
		FROM NH_DA_DuToan_NguonVon nguonVon
		GROUP BY 
			nguonVon.iID_DuToanID
	)
	
	SELECT
		duToan.ID AS Id,
		duToan.iID_QDDauTuID AS IIdQdDauTuId,
		duToan.iID_DuAnID AS IIdDuAnId,
		duToan.sSoQuyetDinh AS SSoQuyetDinh,
		duToan.dNgayQuyetDinh AS DNgayQuyetDinh,
		duToan.sMoTa AS SMoTa,
		duToan.sTenChuongTrinh AS STenChuongTrinh,
		duToan.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndId,
		duToan.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurId,
		duToan.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duToan.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		duToan.fGiaTriUSD AS FGiaTriUsd,
		duToan.fGiaTriVND AS FGiaTriVnd,
		duToan.fGiaTriEUR AS FGiaTriEur,
		duToan.dNgayTao AS DNgayTao,
		duToan.sNguoiTao AS SNguoiTao,
		duToan.dNgaySua AS DNgaySua,
		duToan.sNguoiSua AS SNguoiSua,
		duToan.dNgayXoa AS DNgayXoa,
		duToan.sNguoiXoa AS SNguoiXoa,
		duToan.bIsActive AS BIsActive,
		duToan.bIsGoc AS BIsGoc,
		duToan.bIsKhoa AS BIsKhoa,
		duToan.bIsXoa AS BIsXoa,
		duToan.iID_DuToanGocID AS IIdDuToanGocId,
		duToan.iID_TiGiaID AS IIdTiGiaId,
		duToan.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duToan.iID_ParentID AS IIdParentId,
		duToan.iLoaiDuToan AS IdLoaiDuToan,
		duToan.fTiGiaNhap AS FTiGiaNhap,
		--donvi.sTenDonVi AS STenDonVi, 
		CONCAT(donvi.iID_MaDonVi, ' - ', donvi.sTenDonVi) AS STenDonVi,
		donvi.iID_MaDonVi AS IIdMaDonViQuanLy,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		duToanParent.sSoQuyetDinh AS SDieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 406 AND ObjectId = duToan.ID) AS TotalFiles,
		nvChi.iID_NhiemVuChiID as IIdKHTTNhiemVuChiId,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
	FROM NH_DA_DuToan duToan		
	LEFT JOIN NH_DA_DuToan duToanParent
		ON duToan.iID_ParentID = duToanParent.ID
	LEFT JOIN DonVi donVi
		ON duToan.iID_MaDonViQuanLy = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NH_DA_DuAn duAn
		ON duToan.iID_DuAnID = duAn.ID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = duToan.ID
	LEFT JOIN ThongTinNguonVon nguonVon
		ON nguonVon.iID_DuToanID = duToan.ID
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON duToan.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE duToan.iLoai = @ILoai 
	ORDER BY dNgayQuyetDinh DESC, sSoQuyetDinh DESC
END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 7/31/2023 8:25:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index]
	@iThuocMenu int
AS
BEGIN
	SELECT
		tbl.Id, tbl.sSoQuyetDinh AS SSoQuyetDinh, tbl.dNgayQuyetDinh AS DNgayQuyetDinh, tbl.sMoTa AS SMoTa, da.iID_DonViQuanLyID as IIdDonViQuanLy, da.iID_MaDonViQuanLy as SMaDonViQuanLy, CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS STenDonVi,tbl.fTiGiaNhap as FTiGiaNhap,
		tbl.iID_DuAnID as IIdDuAnID, da.STenDuAn, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId,
		nvChi.iID_NhiemVuChiID as IIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
	FROM NH_DA_KHLCNhaThau as tbl
	INNER JOIN NH_DA_DuAn as da on tbl.iID_DuAnID = da.ID
	LEFT JOIN DonVi as dv on da.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON tbl.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE tbl.iThuocMenu = @iThuocMenu
	ORDER BY tbl.dNgayTao DESC
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 7/31/2023 8:25:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index_2]
AS
BEGIN
	SELECT tbl.Id, tbl.sSoQuyetDinh AS SSoQuyetDinh, tbl.dNgayQuyetDinh AS DNgayQuyetDinh, tbl.sMoTa AS SMoTa, nvc.iID_DonViThuHuongID as IIdDonViQuanLy, nvc.iID_MaDonViThuHuong as SMaDonViQuanLy,CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS STenDonVi, tbl.fTiGiaNhap as FTiGiaNhap,
		tbl.iID_DuAnID as IIdDuAnID, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,dutoan.sTenChuongTrinh as STenChuongTrinh,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId, tbl.fTiGiaNhap as FTiGiaNhap,
		nvChi.iID_NhiemVuChiID as IIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
	FROM NH_DA_KHLCNhaThau as tbl
	left join NH_KHTongThe_NhiemVuChi as nvc on tbl.iID_KHTT_NhiemVuChiID = nvc.ID  
	LEFT JOIN DonVi as dv on nvc.iID_DonViThuHuongID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	LEFT JOIN NH_DA_DuToan as dutoan on tbl.iID_DuToanID = dutoan.ID
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON tbl.iID_KHTT_NhiemVuChiID = nvChi.ID
	--WHERE tbl.bIsActive=1
	ORDER BY tbl.dNgayTao DESC
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinduan_index]    Script Date: 7/31/2023 8:25:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtinduan_index]
	@iLoai int
AS BEGIN
	SELECT 
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		CONCAT(duAn.sMaDuAn, ' - ',duAn.sTenDuAn ) AS STenDuAn,
		duAn.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		duAn.iID_ChuDauTuID AS IIdChuDauTuId,
		duAn.iID_MaChuDauTu AS IIdMaChuDauTu,
		duAn.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		duAn.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		duAn.sKhoiCong AS SKhoiCong,
		duAn.sKetThuc AS SKetThuc,
		duAn.bIsDuPhong AS BIsDuPhong,
		duAn.sDiaDiem AS SDiaDiem,
		duAn.sMucTieu AS SMucTieu,
		duAn.sQuyMo AS SQuyMo,
		duAn.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurid,
		duAn.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		duAn.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duAn.fUSD AS FUsd,
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaID,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		CONCAT(DonVi.iID_MaDonVi, ' - ', DonVi.sTenDonVi) AS STenDonVi,
		pheDuyet.sTen AS STenPheDuyet,
		DM_ChuDauTu.sTenDonVi AS STenChuDauTu,
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 409 AND ObjectId = duAn.ID ) AS TotalFiles,
		nvChi.ID as IIdKHTTNhiemVuChiId,
		nvChi.iID_NhiemVuChiID as iIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
	FROM NH_DA_DuAn duAn
	LEFT JOIN DonVi 
		ON DonVi.iID_DonVi = duAn.iID_DonViQuanLyID
	LEFT JOIN DM_ChuDauTu 
		ON DM_ChuDauTu.iID_DonVi = duAn.iID_ChuDauTuID
	LEFT JOIN NH_DM_PhanCapPheDuyet AS pheDuyet
		on pheDuyet.ID = duAn.iID_CapPheDuyetID
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON duAn.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE duAn.iLoai = @iLoai
END
;
GO
