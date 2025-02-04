/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_kehoachvonung_detail]    Script Date: 7/28/2023 8:39:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_kehoachvonung_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_kehoachvonung_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_kehoachvonung_detail]    Script Date: 7/28/2023 8:39:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_kehoachvonung_detail]
@iIdKeHoachVonUngDeXuat uniqueidentifier
AS
BEGIN
	SELECT dt.iID_DuAnID, SUM(ISNULL(dt.fGiaTriDeNghi, 0)) as fGiaTriDeNghi, dt.ID_DuAn_HangMuc INTO #tmpDX
	FROM VDT_KHV_KeHoachVonUng_DX as tbl 
	INNER JOIN VDT_KHV_KeHoachVonUng_DX_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
	WHERE tbl.Id = @iIdKeHoachVonUngDeXuat
	GROUP BY dt.iID_DuAnID, dt.ID_DuAn_HangMuc;

	SELECT da.iID_DuAnID, SUM(ISNULL(tbl.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet, hm.iID_DuAn_HangMucID AS ID_DuAn_HangMuc INTO #tmp
	FROM #tmpDX as tmp
	INNER JOIN VDT_DA_QDDauTu as tbl on tmp.iID_DuAnID = tbl.iID_DuAnID
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID 
	LEFT JOIN VDT_DA_DuAn_HangMuc hm on tmp.ID_DuAn_HangMuc = hm.iID_DuAn_HangMucID OR da.iID_DuAnID = hm.iID_DuAnID
	WHERE tbl.bActive = 1
	GROUP BY da.iID_DuAnID, hm.iID_DuAn_HangMucID;

	SELECT tbl.*, da.sTenDuAn, da.sMaKetNoi, da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,da.sMaDuAn,
			da.iID_DonViTienTeID, da.iID_TienTeID, da.fTiGia, da.fTiGiaDonVi, null as sGhiChu, 
			NULL as iID_MucID, NULL as iID_TieuMucID, NULL as iID_TietMucID, NULL as iID_NganhID, 
			NULL as sLNS, NULL as sL, NULL as sK, NULL as sM, NULL as sTM, NULL as sTTM, NULL as sNG,
			NULL as fCapPhatBangLenhChi, NULL as fCapPhatTaiKhoBac, ISNULL(fGiaTriDeNghi, 0) as fGiaTriDeNghi,
			hm.sMaHangMuc, hm.sTenHangMuc as sTenHangMuc
	FROM #tmp as tbl 
	INNER JOIN #tmpDX as dx on tbl.iID_DuAnID = dx.iID_DuAnID
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DA_DuAn_HangMuc hm on tbl.ID_DuAn_HangMuc = hm.iID_DuAn_HangMucID

	DROP TABLE #tmp
	DROP TABLE #tmpDX
END
;
;
GO
