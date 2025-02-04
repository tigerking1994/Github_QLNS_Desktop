/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinduan_index]    Script Date: 24/05/2022 10:29:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtinduan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtinduan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 24/05/2022 10:29:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_index]    Script Date: 24/05/2022 10:29:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 24/05/2022 10:29:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 24/05/2022 10:29:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_trongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_trongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 24/05/2022 10:29:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chutruongdautu_index]    Script Date: 24/05/2022 10:29:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chutruongdautu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chutruongdautu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chutruongdautu_index]    Script Date: 24/05/2022 10:29:58 AM ******/
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
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 404 AND ObjectId = chuTruongDauTu.ID) AS TotalFiles 
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
	WHERE chuTruongDauTu.iLoai = @iLoai
	ORDER BY chuTruongDauTu.dNgayQuyetDinh DESC, chuTruongDauTu.sSoQuyetDinh DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 24/05/2022 10:29:58 AM ******/
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
		'' AS STenDonVi,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		duToanParent.sSoQuyetDinh AS SDieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 406 AND ObjectId = duToan.ID) AS TotalFiles
	FROM NH_DA_DuToan duToan		
	LEFT JOIN NH_DA_DuToan duToanParent
		ON duToan.iID_ParentID = duToanParent.ID
	--LEFT JOIN DonVi donVi
	--	ON duToan.iID_MaDonViQuanLy = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NH_DA_DuAn duAn
		ON duToan.iID_DuAnID = duAn.ID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = duToan.ID
	LEFT JOIN ThongTinNguonVon nguonVon
		ON nguonVon.iID_DuToanID = duToan.ID
	WHERE duToan.iLoai = @ILoai 
	ORDER BY dNgayQuyetDinh DESC, sSoQuyetDinh DESC
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 24/05/2022 10:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_goithau_trongnuoc_index]
	@ILoai int,
	@IThuocMenu int
as begin
	Select 
		goiThau.iID_GoiThauID as Id,
		goiThau.iID_GoiThauGocID as IIdGoiThauGocId,
		goiThau.iID_ParentID as IIdParentId,
		goiThau.iID_NhaThauID as IIdNhaThauId,
		goiThau.iID_CacQuyetDinhID as IIdCacQuyetDinhId,
		goiThau.iID_ParentAdjustID as IIdParentAdjustId,
		goiThau.iId_KHLCNhaThau as IIdKhlcnhaThau,
		goiThau.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUSDNgoaiTeKhacId,
		goiThau.iID_TiGiaUSD_VNDID as IIdTiGiaUSDVNDId,
		goiThau.iID_TiGiaUSD_EURID as IIdTiGiaUSDEURId,
		goiThau.iID_HinhThucChonNhaThauID as IIdHinhThucChonNhaThauId,
		goiThau.iID_PhuongThucDauThauID as IIdPhuongThucDauThauId,
		goiThau.iID_LoaiHopDongID as IIdLoaiHopDongId,
		DuAn.ID as IIdDuAnId,
		goiThau.sSoQuyetDinh as SSoQuyetDinh,
		goiThau.dNgayQuyetDinh as DNgayQuyetDinh,
		goiThau.sMaGoiThau as SMaGoiThau,
		goiThau.sTenGoiThau as STenGoiThau,
		goiThau.LoaiGoiThau as LoaiGoiThau,
		goiThau.dBatDauChonNhaThau as DBatDauChonNhaThau,
		goiThau.dKetThucChonNhaThau as DKetThucChonNhaThau,
		goiThau.iThoiGianThucHien as IThoiGianThucHien,
		goiThau.fGiaGoiThauEUR as FGiaGoiThauEUR,
		goiThau.fGiaGoiThauUSD as FGiaGoiThauUSD,
		goiThau.fGiaGoiThauVND as FGiaGoiThauVND,
		goiThau.fGiaGoiThauNgoaiTeKhac as fGiaGoiThauNgoaiTeKhac,
		goiThau.bIsGoc as BIsGoc,
		goiThau.bActive as BActive,
		goiThau.iLanDieuChinh as ILanDieuChinh,
		goiThau.bIsKhoa as BIsKhoa,
		goiThau.dNgayTao as DNgayTao,
		goiThau.sNguoiTao as SNguoiTao,
		goiThau.sNguoiSua as SNguoiSua,
		goiThau.dNgaySua as DNgaySua,
		goiThau.dNgayXoa as DNgayXoa,
		goiThau.sNguoiXoa as SNguoiXoa,
		goiThau.iID_TiGiaID as IIdTiGiaId,
		goiThau.sMaNgoaiTeKhac as SMaNgoaiTeKhac,
		goiThau.bIsXoa as BIsXoa,
		DonVi.sTenDonVi as STenDonVi,
		DuAn.sTenDuAn as STenDuAn,
		HinhThucChonNhaThau.sTenHinhThucChonNhaThau as STenHinhThucChonNhaThau,
		PhuongThucChonNhaThau.sTenPhuongThucChonNhaThau as STenPhuongThucChonNhaThau,
		ChuDauTu.sTenDonVi as STenChuDauTu,
		DuAn.sDiaDiem as SDiaDiem,
		QDDauTu.fGiaTriUSD as FQDDTTongPheDuyetUSD,
		QDDauTu.fGiaTriVND as FQDDTTongPheDuyetVND,
		QDDauTu.fGiaTriEUR as FQDDTTongPheDuyetEUR,
		QDDauTu.fGiaTriNgoaiTeKhac as FQDDTTongPheDuyetNgoaiTeKhac,
		LoaiHopDong.sTenLoaiHopDong as STenHopDong,
		DuToan.fGiaTriUSD as FDTTongPheDuyetUSD,
		DuToan.fGiaTriVND as FDTTongPheDuyetVND,
		DuToan.fGiaTriEUR as FDTTongPheDuyetEUR,
		DuToan.fGiaTriNgoaiTeKhac as FDTTongPheDuyetNgoaiTeKhac,
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 405 AND ObjectId = goiThau.iID_GoiThauID ) AS TotalFiles,
		CASE
		WHEN goiThau.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sMaGoiThau FROM NH_DA_GoiThau hdpr WHERE hdpr.iID_GoiThauID = goiThau.iID_ParentAdjustId ) 
		END DieuChinhTu ,
		LCNhaThau.sMoTa as SMota
	from NH_DA_KHLCNhaThau LCNhaThau
	inner join NH_DA_GoiThau goiThau
		on LCNhaThau.Id = GoiThau.iId_KHLCNhaThau
	inner join NH_DA_DuAn DuAn
		on LCNhaThau.iID_DuAnID = DuAn.ID
	inner join DonVi
		on DuAn.iID_DonViQuanLyID = DonVi.iID_DonVi
	inner join DM_ChuDauTu ChuDauTu
		on DuAn.iID_ChuDauTuID = ChuDauTu.iID_DonVi
	left join NH_DA_QDDauTu QDDauTu
		on LCNhaThau.iID_QDDauTuID = QDDauTu.ID
	left join NH_DA_DuToan DuToan
		on LCNhaThau.iID_DuToanID = DuToan.ID
	left join NH_DM_HinhThucChonNhaThau HinhThucChonNhaThau
		on goiThau.iID_HinhThucChonNhaThauID = HinhThucChonNhaThau.ID 
	left join NH_DM_PhuongThucChonNhaThau PhuongThucChonNhaThau
		on goiThau.iID_PhuongThucDauThauID = PhuongThucChonNhaThau.ID 
	left join NH_DM_LoaiHopDong LoaiHopDong
		on goiThau.iID_LoaiHopDongID = LoaiHopDong.iID_LoaiHopDongID 
	WHERE goiThau.iLoai = @ILoai and goiThau.iThuocMenu = @IThuocMenu
end
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 24/05/2022 10:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index]
	@iThuocMenu int
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa, da.iID_DonViQuanLyID as IIdDonViQuanLy, da.iID_MaDonViQuanLy as SMaDonViQuanLy, dv.STenDonVi, 
		tbl.iID_DuAnID as IIdDuAnID, da.STenDuAn, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu
	FROM NH_DA_KHLCNhaThau as tbl
	INNER JOIN NH_DA_DuAn as da on tbl.iID_DuAnID = da.ID
	LEFT JOIN DonVi as dv on da.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	WHERE tbl.iThuocMenu = @iThuocMenu
	ORDER BY tbl.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_index]    Script Date: 24/05/2022 10:29:58 AM ******/
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
		qdDauTuParent.sSoQuyetDinh AS SDieuChinhTu
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
	WHERE qdDauTu.iLoai = @iLoai
	ORDER BY qdDauTu.dNgayQuyetDinh DESC, qdDauTu.sSoQuyetDinh DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 24/05/2022 10:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT 
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.fGiaTriUSD AS FGiaTriUsd,
	hd.fGiaTriVND AS FGiaTriVnd,
	hd.fGiaTriEUR AS FGiaTriEur,
	hd.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	CASE
		WHEN hd.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId ) 
	END DieuChinhTu
	
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN DonVi donVi
ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh
		 FROM [QLNS_DV].[dbo].NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY
	hd.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinduan_index]    Script Date: 24/05/2022 10:29:58 AM ******/
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
		duAn.sTenDuAn AS STenDuAn,
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
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 409 AND ObjectId = duAn.ID ) AS TotalFiles
	FROM NH_DA_DuAn duAn
	LEFT JOIN DonVi 
		ON DonVi.iID_DonVi = duAn.iID_DonViQuanLyID
	LEFT JOIN DM_ChuDauTu 
		ON DM_ChuDauTu.iID_DonVi = duAn.iID_ChuDauTuID
	LEFT JOIN NH_DM_PhanCapPheDuyet AS pheDuyet
		on pheDuyet.ID = duAn.iID_CapPheDuyetID
	WHERE duAn.iLoai = @iLoai
END
GO
