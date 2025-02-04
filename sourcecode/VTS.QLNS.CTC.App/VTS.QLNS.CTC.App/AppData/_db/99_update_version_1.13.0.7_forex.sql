/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_mlns_by_khv]    Script Date: 04/08/2023 6:52:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_get_mlns_by_khv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_get_mlns_by_khv]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_mlns_by_khv]    Script Date: 04/08/2023 6:52:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_tt_get_mlns_by_khv]
	@iNamLamViec [int],
	@data [dbo].[t_tbl_tonghopdautu_v2] READONLY
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT tbl.iID_ChungTu as IidKeHoachVonId, khvn.iID_LoaiCongTrinh,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN 1 ELSE 2 END) as ILoaiKeHoachVon,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_MucID ELSE khvu.iID_MucID END) as iID_MucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TieuMucID ELSE khvu.iID_TieuMucID END) as iID_TieuMucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TietMucID ELSE khvu.iID_TietMucID END) as iID_TietMucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_NganhID ELSE khvu.iID_NganhID END) as iID_NganhID INTO #tmp
	FROM @data as tbl
	LEFT JOIN VDT_KHV_PhanBoVon_ChiTiet as khvn on tbl.iID_ChungTu = khvn.iID_PhanBoVonID AND tbl.sMaNguon = N'KHVN' AND khvn.iID_DuAnID = tbl.iID_DuAnID and khvn.iID_DuAn_HangMucID = tbl.IIdLoaiCongTrinh
	LEFT JOIN VDT_KHV_KeHoachVonUng_ChiTiet as khvu on tbl.iID_ChungTu = khvu.iID_KeHoachUngID AND tbl.sMaNguon = N'KHVU' AND khvu.iID_DuAnID = tbl.iID_DuAnID and khvu.ID_DuAn_HangMuc = tbl.IIdLoaiCongTrinh

	SELECT tmp.IidKeHoachVonId, tmp.ILoaiKeHoachVon, ml.LNS as LNS, ml.L as L, ml.K as K, ml.M as M, ml.TM as TM, ml.TTM as TTM, ml.NG as NG INTO #tmpMucLuc
	FROM #tmp as tmp
	INNER JOIN VDT_DM_LoaiCongTrinh as ml on ml.iID_LoaiCongTrinh = tmp.iID_LoaiCongTrinh
										

	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, '' as TM, '' as TTM, '' as NG INTO #tmpResult
	FROM #tmpMucLuc
	WHERE ISNULL(M, '') <> ''
	UNION ALL
	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, '' as TTM, '' as NG
	FROM #tmpMucLuc
	WHERE ISNULL(TM, '') <> ''
	UNION ALL
	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, TTM, '' as NG
	FROM #tmpMucLuc
	WHERE ISNULL(TTM, '') <> ''
	UNION ALL
	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, TTM, NG
	FROM #tmpMucLuc
	WHERE ISNULL(NG, '') <> ''

	SELECT DISTINCT tbl.*, sMoTa as SMoTa
	FROM #tmpResult as tbl
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @iNamLamViec
									AND tbl.LNS COLLATE DATABASE_DEFAULT = ml.sLNS COLLATE DATABASE_DEFAULT
									AND tbl.L COLLATE DATABASE_DEFAULT = ml.sL COLLATE DATABASE_DEFAULT
									AND tbl.K COLLATE DATABASE_DEFAULT = ml.sK COLLATE DATABASE_DEFAULT
									AND tbl.M COLLATE DATABASE_DEFAULT = ml.sM COLLATE DATABASE_DEFAULT
									AND tbl.TM COLLATE DATABASE_DEFAULT = ml.sTM COLLATE DATABASE_DEFAULT
									AND tbl.TTM COLLATE DATABASE_DEFAULT = ml.sTTM COLLATE DATABASE_DEFAULT
									AND tbl.NG COLLATE DATABASE_DEFAULT = ml.sNG COLLATE DATABASE_DEFAULT
	ORDER BY LNS, L, K, M, TM, TTM, NG

	 DROP TABLE #tmpResult
	 DROP TABLE #tmpMucLuc
	DROP TABLE #tmp
END
;
;
GO
