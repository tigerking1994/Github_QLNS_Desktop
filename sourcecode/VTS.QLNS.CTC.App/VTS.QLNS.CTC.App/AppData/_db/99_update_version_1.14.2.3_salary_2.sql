/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104]    Script Date: 3/28/2024 9:15:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104]    Script Date: 3/28/2024 9:15:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 25/04/2022
-- Description:	Báo cáo giải thích chi tiết lương theo ngạch, cấp bậc
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104] 
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
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP		AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), 
	macapbac_top_parent as (
		select Ma_Cb, Parent, Ma_Cb AS top_parent 
		from TL_DM_CapBac_NQ104
		where parent is null
		and nam = ' + CAST(@Nam AS VARCHAR(4)) + '

		union all

		select o.ma_cb, o.parent, e.top_parent
		from TL_DM_CapBac_NQ104 o
		inner join macapbac_top_parent e on o.Parent = e.ma_cb
		where nam =' + CAST(@Nam AS VARCHAR(4)) + '
	),
	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			donVi.Ma_DonVi		AS MaDonVi,
			canBo.Ma_Hieu_CanBo	AS MaHieuCanBo,
			capBacParent.Ma_Cb			AS MaCapBac,
			capBacParent.top_parent			AS MaCapBacParent,
			--CASE
				--WHEN capBac.Xau_Noi_Ma LIKE ''1%'' OR capBac.Xau_Noi_Ma LIKE ''4%'' THEN capbacluong.Lht_Hs
				--ELSE canBo.HeSoLuong
			--END AS HeSoLuong
			canBo.HeSoLuong HeSoLuong
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.ma_cb104 = capBac.Ma_Cb
		LEFT JOIN macapbac_top_parent capBacParent
			on capBac.Ma_CB = capBacParent.ma_cb
		--INNER JOIN TL_DM_CapBac_Luong_NQ104 capbacluong on capBac.xau_noi_ma = capbacluong.xau_noi_ma
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND canBo.Parent IN (SELECT * FROM f_split( ''' + @MaDonVi + '''))
			AND capBac.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
	), SoLieuBaoCao AS (
		SELECT
			CASE WHEN ' + CAST(@IsSummary AS VARCHAR(1)) + ' = 1 THEN NULL ELSE canBo.MaDonVi END	AS MaDonVi,
			canBo.MaCapBac																			AS MaCapBac,
			canBo.MaCapBacParent																	AS MaCapBacParent,
			canBo.HeSoLuong																			AS HeSoLuong,
			bangLuongThang.MaPhuCap																	AS MaPhuCap,
			COUNT(canBo.MaCanBo)																	AS SoNguoi,
			SUM(bangLuongThang.GiaTri) / ' + CAST(@DonViTinh AS VARCHAR(100)) + '					AS GiaTri
		FROM ThongTinCanBo canBo
		INNER JOIN BangLuongThang bangLuongThang
			ON bangLuongThang.MaCanBo = canBo.MaCanBo
		GROUP BY MaDonVi, MaCapBac, HeSoLuong, MaPhuCap, MaCapBacParent
	), CanBoLuongCapBac AS (
		SELECT
			bangLuong.Ma_Don_Vi AS Ma_DonVi,
			canbo.ma_cb104			AS MaCapBac,
			bangLuong.GIA_TRI	AS HeSoLuong
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CanBo canbo 
			ON bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
				AND canbo.Nam = ' + CAST(@Nam AS VARCHAR(4)) + '
				AND canbo.Thang = ' + CAST(@Thang AS VARCHAR(2)) + '
		WHERE
			bangLuong.Ma_Phu_Cap = ''LHT_HS''
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	)

	SELECT ''' + @MaCachTl + ''' AS CachTl, (SELECT COUNT(*) FROM CanBoLuongCapBac canBo INNER JOIN TL_DM_CapBac_NQ104 as cb on canBo.MaCapBac = cb.Ma_Cb WHERE ' + (CASE WHEN @IsSummary = 1 THEN '' ELSE ' canBo.Ma_DonVi = pvt.MaDonVi AND ' END)+ ' canBo.MaCapBac = pvt.MaCapBac AND cb.nam = ' + CAST(@Nam AS VARCHAR(4)) + ' AND ((cb.Xau_Noi_Ma LIKE ''1%'' OR cb.Xau_Noi_Ma LIKE ''4%'') OR canBo.HeSoLuong = pvt.HeSoLuong)) AS SoNguoi, pvt.* FROM (
		SELECT
			MaDonVi,
			MaCapBac,
			MaCapBacParent,
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
;
;
GO



/****** Object:  StoredProcedure [dbo].[sp_tl_get_bang_luongthang_bridge_nq104]    Script Date: 3/28/2024 4:24:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_bang_luongthang_bridge_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_bang_luongthang_bridge_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_bang_luongthang_bridge_nq104]    Script Date: 3/28/2024 4:24:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_bang_luongthang_bridge_nq104]
	@MaDonVi NVARCHAR(max),
	@Thang int,
	@Nam AS int,
	@MaCachTL NVARCHAR(20),
	@MaHieuCanBo NVARCHAR(500)
AS
BEGIN
	
	select 
		capnhat.Thang,
		capnhat.Nam,
		bangluong.ma_can_bo MaCbo,
		capnhat.Ma_CBo MaDonVi,
		capnhat.Ma_CachTL MaCachTl,
		bangluong.ma_phu_cap MaPhuCap,
		bangluong.gia_tri GiaTri,
		bangluong.ma_hieu_can_bo MaHieuCanBo
	from TL_DS_CapNhap_BangLuong_NQ104 capnhat
	join TL_BangLuong_Thang_Bridge_NQ104 bangluong on capnhat.Id = bangluong.parent
	where capnhat.Nam = @Nam
		and capnhat.Thang = @Thang
		and capnhat.Ma_CBo = @MaDonVi
		and capnhat.Ma_CachTL = @MaCachTL
		and bangluong.ma_hieu_can_bo = @MaHieuCanBo
END
GO
