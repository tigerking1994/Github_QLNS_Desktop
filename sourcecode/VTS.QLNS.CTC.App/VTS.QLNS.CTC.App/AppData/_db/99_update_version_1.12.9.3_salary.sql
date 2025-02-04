/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export]    Script Date: 6/21/2023 10:23:25 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_giaithich_bangso_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_giaithich_bangso_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 6/21/2023 10:23:25 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 6/21/2023 10:23:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int,
	@isSummary bit,
	@maDonViTongHop nvarchar(100)
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu as tbl on tmp.Item = tbl.ID;

	select DISTINCT XauNoiMa  into #tmpPcMlns FROM TL_PhuCap_MLNS WHERE Nam = @nam;

	with ctct as (
	  select XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 ISNULL(dt.DDuToan, 0) As DDuToan, SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay
	  from  TL_QT_ChungTuChiTiet as dt 
	  left join TL_QT_ChungTu as tbl on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND (((dt.MaCachTl = '' or dt.MaCachTl is null) AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl, dt.DDuToan
	),
	--lstSoNguoi as (
	--	SELECT XauNoiMa,
	--		SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay
	--	from TL_QT_ChungTu as tbl
	--	INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	--	where tbl.ID in (SELECT ID FROM #tblMaxThang)
	--	AND ((@isHaveCachTinhLuong = 0 AND (dt.MaCachTl = '' or dt.MaCachTl is null)) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	--	group by XauNoiMa
	--)
	--,
	phucapMlns as (
		SELECT TL_PhuCap_MLNS.XauNoiMa, min(TL_PhuCap_MLNS.Ma_Cb) as Ma_Cb FROM #tmpPcMlns 
		left join TL_PhuCap_MLNS on #tmpPcMlns.XauNoiMa = TL_PhuCap_MLNS.XauNoiMa
		WHERE Nam = @nam
		group by TL_PhuCap_MLNS.XauNoiMa
	),
	ctTongHop as (
		select DDuToan, sum(DieuChinh) DieuChinh, SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay, XauNoiMa from TL_QT_ChungTuChiTiet ctct
		left join tl_qt_chungtu ct on ctct.Id_ChungTu = ct.ID where CHARINDEX(ct.sTongHop, @lstId, 0) > 0 and NamLamViec = @nam and @maDonViTongHop = ct.Ma_DonVi 
		AND (((ctct.MaCachTl = '' or ctct.MaCachTl is null) AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND ctct.MaCachTl in (SELECT id  FROM #tmp)))
		group by XauNoiMa, DDuToan
	)
SELECT 
       iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       mlns.sTNG AS TNG,
       mlns.sTNG1 AS TNG1,
       mlns.sTNG2 AS TNG2,
       mlns.sTNG3 AS TNG3,
       sMoTa AS Mota,
       MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
	   case when @isSummary = 1 then ctTongHop.SoNguoi when @isSummary = 0 then ctct.SoNguoi end as SoNguoi,
	   case when @isSummary = 1 then ctTongHop.SoNgay when @isSummary = 0 then ctct.SoNgay end as SoNgay,
       case when @isSummary = 1 then ctTongHop.DieuChinh when @isSummary = 0 then ctct.DieuChinh end as DieuChinh,
	   case when @isSummary = 1 then ctTongHop.DDuToan when @isSummary = 0 then  ctct.DDuToan end as DDuToan

FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
--LEFT JOIN lstSoNguoi as sn on mlns.sXauNoiMa = sn.XauNoiMa 
LEFT JOIN ctTongHop on mlns.sXauNoiMa = ctTongHop.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam

order by XauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
drop table #tmpPcMlns
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export]    Script Date: 6/21/2023 10:23:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_giaithich_bangso_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int,
	@isSummary bit,
	@maDonViTongHop nvarchar(100)
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu as tbl on tmp.Item = tbl.ID;


	with ctct as (
	  select dt.XauNoiMa,  MaCachTl, MaCb,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(ISNULL(dt.DDuToan, 0)) As DDuToan
	  from TL_QT_ChungTu as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND ((dt.MaCachTl = '' AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl, dt.MaCb
	),
	ctTongHop as (
		select Sum(DDuToan) as DDuToan, sum(DieuChinh) DieuChinh, XauNoiMa from TL_QT_ChungTuChiTiet ctct
		left join tl_qt_chungtu ct on ctct.Id_ChungTu = ct.ID where CHARINDEX(ct.sTongHop, @lstId, 0) > 0 and NamLamViec = @nam and @maDonViTongHop = ct.Ma_DonVi 
		AND (((ctct.MaCachTl = '' or ctct.MaCachTl is null) AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND ctct.MaCachTl in (SELECT id  FROM #tmp)))
		group by XauNoiMa
	)

SELECT 
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       mlns.sTNG AS TNG,
       mlns.sTNG1 AS TNG1,
       mlns.sTNG2 AS TNG2,
       mlns.sTNG3 AS TNG3,
	   ctct.MaCb as MaCb,
       sMoTa AS Mota,
       MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
       case when @isSummary = 1 then ctTongHop.DieuChinh when @isSummary = 0 then ctct.DieuChinh end as DieuChinh,
	   case when @isSummary = 1 then ctTongHop.DDuToan when @isSummary = 0 then  ctct.DDuToan end as DuToan,
	 ctct.MaCb as MaCb,
	 capbac.Parent as MaCbCha
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
LEFT JOIN TL_DM_CapBac capbac ON ctct.MaCb = capbac.Ma_Cb
LEFT JOIN ctTongHop on mlns.sXauNoiMa = ctTongHop.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam
  and bHangCha = 0

order by XauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
END
;
;
GO
