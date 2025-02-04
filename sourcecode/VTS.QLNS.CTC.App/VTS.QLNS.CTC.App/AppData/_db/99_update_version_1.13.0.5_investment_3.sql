/****** Object:  StoredProcedure [dbo].[sp_vdt_get_nhucauchiquy_detail]    Script Date: 7/26/2023 7:23:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_nhucauchiquy_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_nhucauchiquy_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_nhucauchiquy_detail]    Script Date: 7/26/2023 7:23:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_nhucauchiquy_detail]
@iIdMaDonVi nvarchar(100),
@iNamKeHoach int,
@iIdNguonVon int,
@iQuy int,
@DonviTinh int
AS
BEGIN
	SELECT dt.iID_DuAnID, SUM(dt.fCapPhatBangLenhChi) as fCapPhatBangLenhChi, SUM(dt.fCapPhatTaiKhoBac) as fCapPhatTaiKhoBac INTO #tmp
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
	WHERE tbl.iID_MaDonViQuanLy = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamKeHoach AND tbl.iID_NguonVonID = @iIdNguonVon
	GROUP BY dt.iID_DuAnID

	SELECT iID_DuAnId, 
		(CASE WHEN iCoQuanThanhToan = 2 THEN 'CQTC' ELSE N'Kho bạc' END ) as sLoaiThanhToan ,
		SUM(ISNULL(fThanhToanKLHTQuyTruoc, 0)) as fThanhToanKLHTQuyTruoc, SUM(ISNULL(fThanhToanTamUngQuyTruoc, 0)) as fThanhToanTamUngQuyTruoc INTO #tmpQuyTruoc
	FROM
	(
		SELECT tbl.iID_DuAnId, tbl.iCoQuanThanhToan,
			(CASE WHEN tbl.iLoaiThanhToan = 1 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE CAST(0 as float) END) as fThanhToanKLHTQuyTruoc,
			(CASE WHEN tbl.iLoaiThanhToan = 2 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE CAST(0 as float) END) as fThanhToanTamUngQuyTruoc
		FROM #tmp as tmp
		INNER JOIN VDT_TT_DeNghiThanhToan as tbl on tmp.iID_DuAnID = tbl.iID_DuAnId
		INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
		WHERE (
			(@iQuy = 1 AND MONTH(tbl.dNgayDeNghi) in (10,11,12) AND tbl.iNamKeHoach = (@iNamKeHoach - 1)) 
			OR (@iQuy = 2 AND MONTH(tbl.dNgayDeNghi) in (1,2,3) AND tbl.iNamKeHoach = @iNamKeHoach) 
			OR (@iQuy = 3 AND MONTH(tbl.dNgayDeNghi) in (4,5,6) AND tbl.iNamKeHoach = @iNamKeHoach) 
			OR (@iQuy = 4 AND MONTH(tbl.dNgayDeNghi) in (7,8,9) AND tbl.iNamKeHoach = @iNamKeHoach) 
		)
		GROUP BY tbl.iID_DuAnId, tbl.iLoaiThanhToan, tbl.iCoQuanThanhToan
	) as tbl
	GROUP BY tbl.iID_DuAnId, tbl.iCoQuanThanhToan

	SELECT iID_DuAnId, 
		(CASE WHEN iCoQuanThanhToan = 2 THEN 'CQTC' ELSE N'Kho bạc' END ) as sLoaiThanhToan ,
		SUM(ISNULL(fThanhToanKLHTQuyNay ,0)) as fThanhToanKLHTQuyNay, SUM(ISNULL(fThanhToanTamUngQuyNay ,0)) as fThanhToanTamUngQuyNay, SUM(ISNULL(fThuHoiUng ,0)) as fThuHoiUng  INTO #tmpQuyNay
	FROM
	(
		SELECT tbl.iID_DuAnId, tbl.iCoQuanThanhToan,
			(CASE WHEN tbl.iLoaiThanhToan = 1 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE CAST(0 as float) END) as fThanhToanKLHTQuyNay,
			(CASE WHEN tbl.iLoaiThanhToan = 2 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE CAST(0 as float) END) as fThanhToanTamUngQuyNay,
			SUM(ISNULL(dt.fGiaTriThuHoiNamNayNN, 0) + ISNULL(dt.fGiaTriThuHoiNamNayTN, 0)) as fThuHoiUng
		FROM #tmp as tmp
		INNER JOIN VDT_TT_DeNghiThanhToan as tbl on tmp.iID_DuAnID = tbl.iID_DuAnId
		INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
		WHERE tbl.iNamKeHoach = @iNamKeHoach AND 
			((@iQuy = 1 AND MONTH(tbl.dNgayDeNghi) in (1,2,3)) 
			OR (@iQuy = 2 AND MONTH(tbl.dNgayDeNghi) in (4,5,6)) 
			OR (@iQuy = 3 AND MONTH(tbl.dNgayDeNghi) in (7,8,9)) 
			OR (@iQuy = 4 AND MONTH(tbl.dNgayDeNghi) in (10,11,12))
		)
		GROUP BY tbl.iID_DuAnId, tbl.iLoaiThanhToan, tbl.iCoQuanThanhToan
	) as tbl
	GROUP BY tbl.iID_DuAnId, tbl.iCoQuanThanhToan

	
	if(@DonviTinh Is not null)
	BEGIN
	SELECT tmp.iID_DuAnID, da.sTenDuAn, tmp.sLoaiThanhToan, da.iID_LoaiCongTrinhID, da.sMaDuAn, ISNULL(tmp.fGiaTri, 0)/@DonviTinh as fKeHoachVonNam, 
		ISNULL(tblQuyTruoc.fThanhToanKLHTQuyTruoc, 0)/@DonviTinh as fThanhToanKLHTQuyTruoc, ISNULL(tblQuyTruoc.fThanhToanTamUngQuyTruoc, 0)/@DonviTinh as fThanhToanTamUngQuyTruoc, 
		ISNULL(tblQuyNay.fThanhToanKLHTQuyNay, 0)/@DonviTinh as fThanhToanKLHTQuyNay, ISNULL(tblQuyNay.fThanhToanTamUngQuyNay, 0)/@DonviTinh as fThanhToanTamUngQuyNay
		, ISNULL(tblQuyNay.fThuHoiUng, 0)/@DonviTinh as fThuHoiUng, CAST(0 as float)/@DonviTinh as fGiaTriDeNghi, null as sGhiChu
	FROM 
	(
		SELECT iID_DuAnID, (CASE colname WHEN 'fCapPhatBangLenhChi' THEN N'CQTC' WHEN 'fCapPhatTaiKhoBac' THEN N'Kho bạc' END) as sLoaiThanhToan, fGiaTri
		FROM #tmp
		UNPIVOT 
		(fGiaTri FOR colname in (fCapPhatBangLenhChi, fCapPhatTaiKhoBac)) as dt
	) as tmp
	INNER JOIN VDT_DA_DuAn as da on tmp.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN #tmpQuyTruoc as tblQuyTruoc on tmp.iID_DuAnID = tblQuyTruoc.iID_DuAnId AND tmp.sLoaiThanhToan = tblQuyTruoc.sLoaiThanhToan
	LEFT JOIN #tmpQuyNay as tblQuyNay on tmp.iID_DuAnID = tblQuyNay.iID_DuAnId AND tmp.sLoaiThanhToan = tblQuyNay.sLoaiThanhToan
	END
END
;
;
GO
