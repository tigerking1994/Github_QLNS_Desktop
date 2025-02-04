IF NOT EXISTS (SELECT * FROM TL_PhuCap_MLNS WHERE Ma_PhuCap=N'HSBL_TT' and Nam=2023 and Ma_Cb='2')
INSERT [dbo].[TL_PhuCap_MLNS] ([ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NULL, CAST(N'2023-05-29T10:05:48.833' AS DateTime), CAST(N'2023-04-07T07:29:52.517' AS DateTime), NULL, N'64118331-2dbc-4fc9-9208-361dba90393e', N'aea8436b-e84a-4d03-9975-e9d9c68b496b', N'ce2e2fbb-bc23-4c26-bf3c-89aa7d66f2fc', N'339e6274-c2ba-47b0-8a4c-377d35555c44', N'011', N'010', N'1010000', N'6000', N'CACH0', N'2', NULL, N'HSBL_TT', N'Lương quân nhân chuyên nghiệp', 2023, N'00', N'Lương quân nhân chuyên nghiệp', N'Hệ số bảo lưu (thành tiền)', N'6001', N'20', N'admin', N'admin', N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
IF NOT EXISTS (SELECT * FROM TL_PhuCap_MLNS WHERE Ma_PhuCap=N'HSBL_TT' and Nam=2023 and Ma_Cb='1')
INSERT [dbo].[TL_PhuCap_MLNS] ([ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NULL, CAST(N'2023-04-12T22:54:58.687' AS DateTime), NULL, NULL, N'64118331-2dbc-4fc9-9208-361dba90393e', N'b7972c9e-c957-44e0-87ac-0a562298fa6f', N'ce2e2fbb-bc23-4c26-bf3c-89aa7d66f2fc', N'339e6274-c2ba-47b0-8a4c-377d35555c44', N'011', N'010', N'1010000', N'6000', N'CACH0', N'1', NULL, N'HSBL_TT', N'Lương sĩ quan', 2023, N'00', N'Lương sĩ quan', N'Hệ số bảo lưu (thành tiền)', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]    Script Date: 5/29/2023 1:41:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 5/29/2023 6:02:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 5/29/2023 6:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int,
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
		 ISNULL(dt.DDuToan, 0) As DDuToan
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
    ctTongHop.SoNguoi,
	ctTongHop.SoNgay,
       ctTongHop.DieuChinh,
     ctTongHop.DDuToan

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
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]    Script Date: 5/29/2023 1:41:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 25/04/2022
-- Description:	Báo cáo giải thích chi tiết lương theo ngạch, cấp bậc
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@MaPhuCapCount NVARCHAR(MAX),
	@DonViTinh int,
	@IsSummary bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)
	SET @Query =
	'
    WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CBo			AS MaCanBo,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), 
	macapbac_top_parent as (
		select Ma_Cb, Parent, Ma_Cb AS top_parent 
		from TL_DM_CapBac
		where parent is null

		union all

		select o.ma_cb, o.parent, e.top_parent
		from TL_DM_CapBac o
		inner join macapbac_top_parent e on o.Parent = e.ma_cb
	),
	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			donVi.Ma_DonVi		AS MaDonVi,
			canBo.Ma_Hieu_CanBo	AS MaHieuCanBo,
			capBacParent.Ma_Cb			AS MaCapBac,
			CASE
				WHEN capBac.XauNoiMa LIKE ''1%'' OR capBac.XauNoiMa LIKE ''4%'' THEN capBac.Lht_Hs
				ELSE canBo.HeSoLuong
			END AS HeSoLuong
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		left join macapbac_top_parent capBacParent
			on capBac.Ma_CB = capBacParent.ma_cb
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND canBo.Parent IN (SELECT * FROM f_split( ''' + @MaDonVi + '''))
	), SoLieuBaoCao AS (
		SELECT
			CASE WHEN ' + CAST(@IsSummary AS VARCHAR(1)) + ' = 1 THEN NULL ELSE canBo.MaDonVi END	AS MaDonVi,
			canBo.MaCapBac																			AS MaCapBac,
			canBo.HeSoLuong																			AS HeSoLuong,
			bangLuongThang.MaPhuCap																	AS MaPhuCap,
			COUNT(canBo.MaCanBo)																	AS SoNguoi,
			SUM(bangLuongThang.GiaTri) / ' + CAST(@DonViTinh AS VARCHAR(100)) + '					AS GiaTri
		FROM ThongTinCanBo canBo
		INNER JOIN BangLuongThang bangLuongThang
			ON bangLuongThang.MaCanBo = canBo.MaCanBo
		GROUP BY MaDonVi, MaCapBac, HeSoLuong, MaPhuCap
	), CanBoLuongCapBac AS (
		SELECT
			bangLuong.Ma_DonVi,
			bangLuong.Ma_CB			AS MaCapBac,
			bangLuong.GIA_TRI		AS HeSoLuong
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap = ''LHT_HS''
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	)

	SELECT ''' + @MaCachTl + ''' AS CachTl, (SELECT COUNT(*) FROM CanBoLuongCapBac canBo INNER JOIN TL_DM_CapBac as cb on canBo.MaCapBac = cb.Ma_Cb WHERE ' + (CASE WHEN @IsSummary = 1 THEN '' ELSE ' canBo.Ma_DonVi = pvt.MaDonVi AND ' END)+ ' canBo.MaCapBac = pvt.MaCapBac AND ((cb.XauNoiMa LIKE ''1%'' OR cb.XauNoiMa LIKE ''4%'') OR canBo.HeSoLuong = pvt.HeSoLuong)) AS SoNguoi, pvt.* FROM (
		SELECT
			MaDonVi,
			MaCapBac,
			HeSoLuong,
			DATA,
			COLUMN_NAME + MaPhuCap AS PIV_COL
		FROM SoLieuBaoCao
		CROSS APPLY (
			VALUES (''COUNT_'', SoNguoi), ('''', GiaTri) 
		) CS(COLUMN_NAME, DATA)
	) a
	PIVOT (
		SUM(DATA)
		FOR PIV_COL IN (' + @MaPhuCap + ', ' + @MaPhuCapCount + ')
	) pvt ORDER BY MaDonVi, MaCapBac, HeSoLuong'

	execute(@Query)
END
;
;
;
GO
