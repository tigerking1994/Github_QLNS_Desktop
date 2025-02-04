/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_truy_thu]    Script Date: 5/22/2024 2:31:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_truy_thu]    Script Date: 5/22/2024 2:31:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_truy_thu]    Script Date: 5/22/2024 2:31:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_canbo_truy_thu]    Script Date: 5/22/2024 2:31:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_canbo_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_canbo_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_truy_thu_chitiet]    Script Date: 5/22/2024 2:31:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_luong_thang_truy_thu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_luong_thang_truy_thu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_truy_thu_chitiet]    Script Date: 5/22/2024 2:31:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_bang_luong_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_delete_bang_luong_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_bang_luong_truy_thu]    Script Date: 5/22/2024 2:45:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bang_luong_thang_truy_thu_chitiet]
	@Id UNIQUEIDENTIFIER
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);;

SET @cols = STUFF(
  (
    SELECT
      DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_BangLuong_Thang_TruyThu phucap where phucap.parent = @Id
        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = '
	WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo	AS MaCanBo,
			canBo.Ten_CanBo	AS TenCanBo,
			canBo.IsDelete AS IsDelete,
			canbo.Ngay_XN,
			canbo.Ma_TangGiam,
			donVi.Ma_DonVi	AS MaDonVi,
			donVi.Ten_DonVi	AS TenDonVi
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		WHERE canBo.Khong_Luong = 0
	)
	SELECT Thang, Nam, MaCanBo, TenCanBo, TenDonVi, ' + @cols + ' FROM (
		SELECT
			bangLuongThang.THANG		AS Thang,
			bangLuongThang.NAM			AS Nam,
			canBo.MaCanBo				AS MaCanBo,
			canBo.TenCanBo				AS TenCanBo,
			canBo.TenDonVi				AS TenDonVi,
			bangLuongThang.Gia_Tri		AS GiaTri,
			bangLuongThang.Ma_PhuCap	AS MaPhuCap
		FROM TL_BangLuong_Thang_TruyThu bangLuongThang
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.Ma_CBo = canBo.MaCanBo
		WHERE
			--(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = bangLuongThang.THANG and year(canbo.Ngay_XN) = bangLuongThang.NAM))
			 bangLuongThang.parent = + ''' + CONVERT(NVARCHAR(100), @Id) + '''
	) x
	PIVOT 
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @cols + ')
	) p '
execute(@query)
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_canbo_truy_thu]    Script Date: 5/22/2024 2:31:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_get_data_canbo_truy_thu] 
	-- Add the parameters for the stored procedure here
	@Thang int ,
	@Nam int ,
	@MaDonVi nvarchar(max) ,
	@iIdBangLuong uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	CREATE TABLE #tempDataTruyThu (MaHieuCanBo nvarchar(500), MaCanBo nvarchar(500),SoNgayTruyThu int, MaDonVi Nvarchar(50))


	--Lấy từ bảo hiểm
	select cb.Ma_Hieu_CanBo, cb.Ma_CanBo, sum(cb_bhxh.fSoNgayHuongBHXH) as SoNgayTruyThu,  cb.Parent as MaDonVi INTO #tmpBaoHiem
		from TL_DM_CanBo as cb
		inner join TL_CanBo_CheDoBHXH as cb_bhxh on cb.Ma_CanBo = cb_bhxh.sMaCanBo
		inner join TL_DM_CheDoBHXH as pc on pc.sMaCheDo = cb_bhxh.sMaCheDo
		where cb.Thang = @Thang and cb.Nam = @Nam and 
		pc.bDisplay =1 AND cb.Parent IN (SELECT * FROM f_split(@MaDonVi))
		group by  cb.Ma_Hieu_CanBo,cb.Ma_CanBo, cb.Parent;


	IF  EXISTS ( SELECT 1 FROM  #tmpBaoHiem)
	BEGIN
		INSERT INTO #tempDataTruyThu
		SELECT  * FROM #tmpBaoHiem
	END


	--Kiem tra  có truy thu theo cán bộ
	 SELECT cb.Ma_Hieu_CanBo, cb.Ma_CanBo,cbpc.GIA_TRI as SoNgayTruyThu, cb.Parent as MaDonVi INTO #tmpCanBo
	 FROM TL_DM_CanBo cb
	 INNER JOIN TL_CanBo_PhuCap  cbpc  ON cb.Ma_CanBo = cbpc.MA_CBO
	 WHERE  cb.Thang = @Thang AND cb.Nam =@Nam  and MA_PHUCAP = 'TRUYTHU_SN'AND cb.Parent IN (SELECT * FROM f_split(@MaDonVi)) AND cbpc.GIA_TRI > 0;


	IF  EXISTS ( SELECT 1 FROM  #tempDataTruyThu)
	BEGIN
			IF  EXISTS ( SELECT 1 FROM  #tmpCanBo)
			BEGIN
				INSERT INTO #tempDataTruyThu
				SELECT * FROM #tmpCanBo cb
				WHERE cb.Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM #tmpBaoHiem)
			END
	END
	ELSE
	BEGIN
			IF  EXISTS ( SELECT 1 FROM  #tmpCanBo)
			BEGIN
				INSERT INTO #tempDataTruyThu
				SELECT * FROM #tmpCanBo cb
			END
	END
	-----SELECT OUT
	IF(@Thang = 1)
	BEGIN
			SELECT 
			 NEWID() as Id
			,luongThang.Gia_Tri AS GiaTri
			,luongThang.Loai_BL AS LoaiBl
			,'CACH1' AS MaCachTl
			,luongThang.Ma_CB AS MaCb
			,truythu.MaCanBo AS MaCbo
			,luongThang.Ma_DonVi AS MaDonVi
			,luongThang.Ma_Hieu_CanBo AS MaHieuCanBo
			,luongThang.Ma_PhuCap AS MaPhuCap
			,@Nam AS Nam
			,luongThang.Ngay_HT AS NgayHt
			,@iIdBangLuong AS Parent
			,luongThang.So_TT AS SoTt
			,luongThang.Ten_CachTL AS TenCachTl
			,luongThang.Ten_Cbo AS TenCbo
			,@Thang AS Thang
			,luongThang.User_Name AS UserName
			,luongThang.HuongPC_SN  AS HuongPC_SN
			,CAST(truythu.SoNgayTruyThu as numeric(15,4))  as SoNgayTruyThu,
			CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated,
			cachTinhLuong.CongThuc AS CongThuc
			FROM TL_BangLuong_Thang luongthang
			INNER JOIN #tempDataTruyThu truythu ON luongthang.Ma_Hieu_CanBo = truythu.MaHieuCanBo
			LEFT JOIN (select Id, Ma_Cot, CongThuc from TL_DM_Cach_TinhLuong_Chuan) cachTinhLuong
			ON cachTinhLuong.Ma_Cot = luongthang.MA_PHUCAP
			WHERE  luongthang.THANG = 12 AND luongthang.NAM = @Nam - 1 AND luongthang.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))
	END
	ELSE
	BEGIN
			SELECT 
			 NEWID() as Id
			,luongThang.Gia_Tri AS GiaTri
			,luongThang.Loai_BL AS LoaiBl
			,'CACH1' AS MaCachTl
			,luongThang.Ma_CB AS MaCb
			,truythu.MaCanBo AS MaCbo
			,luongThang.Ma_DonVi AS MaDonVi
			,luongThang.Ma_Hieu_CanBo AS MaHieuCanBo
			,luongThang.Ma_PhuCap AS MaPhuCap
			,@Nam AS Nam
			,luongThang.Ngay_HT AS NgayHt
			,@iIdBangLuong AS Parent
			,luongThang.So_TT AS SoTt
			,luongThang.Ten_CachTL AS TenCachTl
			,luongThang.Ten_Cbo AS TenCbo
			,@Thang AS Thang
			,luongThang.User_Name AS UserName
			,luongThang.HuongPC_SN  AS HuongPC_SN
			,CAST(truythu.SoNgayTruyThu as numeric(15,4))  as SoNgayTruyThu,
			CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated,
			cachTinhLuong.CongThuc AS CongThuc
			FROM TL_BangLuong_Thang luongthang
			INNER JOIN #tempDataTruyThu truythu ON luongthang.Ma_Hieu_CanBo = truythu.MaHieuCanBo
			LEFT JOIN (select Id, Ma_Cot, CongThuc from TL_DM_Cach_TinhLuong_Chuan) cachTinhLuong
				ON cachTinhLuong.Ma_Cot = luongthang.MA_PHUCAP
			WHERE  luongthang.THANG = @Thang - 1 AND luongthang.NAM = @Nam AND luongthang.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))
	END
	DROP TABLE #tempDataTruyThu;
	DROP TABLE #tmpBaoHiem;
	DROP TABLE #tmpCanBo;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_truy_thu]    Script Date: 5/22/2024 2:31:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_truy_thu]
 @MaDonVi NVARCHAR(max),
@Thang int ,
@Nam AS int ,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int

IF @Thang = 1 
BEGIN
	SET @ThangTruoc = 12;
	SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT * FROM TL_BangLuong_Thang
	WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongTruyThu AS (
select truythu.Ma_CBo as Madonvi,bangluong.Ma_PhuCap, bangluong.Ma_Hieu_CanBo, bangluong.Ma_CBo, bangluong.Ma_DonVi, SUM(bangluong.Gia_Tri) as GiaTri, truythu.Thang as iThang,truythu.Nam as iNam
from TL_DS_CapNhap_BangLuong truythu
INNER JOIN TL_BangLuong_Thang_TruyThu bangluong ON truythu.id = bangluong.parent
where truythu.Ma_CachTL = ''CACH1''
	AND truythu.THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND truythu.NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND truythu.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
group by truythu.Ma_CBo, bangluong.Ma_Hieu_CanBo, bangluong.Ma_CBo, bangluong.Ma_DonVi,bangluong.Ma_PhuCap, truythu.Thang,truythu.Nam
),

BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_LBH_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_LBH_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)- isnull(truyThu.GiaTri, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)- isnull(truyThu.GiaTri, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)- isnull(truyThu.GiaTri, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)- isnull(truyThu.GiaTri, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0) - isnull(truyThu.GiaTri, 0)
	ELSE isnull(bangLuong.Gia_Tri, 0) - isnull(truyThu.GiaTri, 0)
END AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id

LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
LEFT JOIN BangLuongTruyThu truyThu ON bangLuong.Ma_CBo = truyThu.Ma_CBo AND bangLuong.Ma_DonVi = truyThu.MaDonVi AND bangLuong.NAM = truyThu.iNam AND bangLuong.THANG = truyThu.iThang AND bangLuong.Ma_PhuCap = truyThu.Ma_PhuCap

WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_truy_thu]    Script Date: 5/22/2024 2:31:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_truy_thu]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int
IF @Thang = 1 
BEGIN
SET @ThangTruoc = 12;
SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
SET @ThangTruoc = @Thang - 1;
SET @NamTruoc = @Nam;
END
SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCNU_TT,PCTHD_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
From TL_DM_CanBo canbo
Where 
canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongTruyThu AS (
select truythu.Ma_CBo as Madonvi,bangluong.Ma_PhuCap, bangluong.Ma_Hieu_CanBo, bangluong.Ma_CBo, bangluong.Ma_DonVi, SUM(bangluong.Gia_Tri) as GiaTri, truythu.Thang as iThang,truythu.Nam as iNam
from TL_DS_CapNhap_BangLuong truythu
INNER JOIN TL_BangLuong_Thang_TruyThu bangluong ON truythu.id = bangluong.parent
where truythu.Ma_CachTL = ''CACH1''
	AND truythu.THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND truythu.NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND truythu.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
group by truythu.Ma_CBo, bangluong.Ma_Hieu_CanBo, bangluong.Ma_CBo, bangluong.Ma_DonVi,bangluong.Ma_PhuCap, truythu.Thang,truythu.Nam
),
blt AS (
SELECT * FROM TL_BangLuong_Thang
WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI  - isnull(truyThu.GiaTri, 0) AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongTruyThu truyThu ON bangLuong.Ma_CBo = truyThu.Ma_CBo AND bangLuong.Ma_DonVi = truyThu.MaDonVi AND bangLuong.NAM = truyThu.iNam AND bangLuong.THANG = truyThu.iThang AND bangLuong.Ma_PhuCap = truyThu.Ma_PhuCap
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)
SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_truy_thu]    Script Date: 5/22/2024 2:31:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_truy_thu]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@DonViTinh int,
	@IsOrderChucVu bit = 0
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
			dsCapNhapBangLuong.Thang										AS Thang,
			dsCapNhapBangLuong.Nam											AS Nam,
			bangLuong.Ma_CBo												AS MaCanBo,
			bangLuong.MA_PHUCAP												AS MaPhuCap,
			bangLuong.GIA_TRI / ' + CAST(@DonViTinh AS VARCHAR(100)) + '	AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
			AND bangLuong.GIA_TRI != 0
	),	
	BangLuongTruyThu AS (
		select truythu.Ma_CBo as Madonvi,bangluong.Ma_PhuCap, bangluong.Ma_Hieu_CanBo, bangluong.Ma_CBo, bangluong.Ma_DonVi, SUM(bangluong.Gia_Tri) as GiaTri, truythu.Thang as iThang,truythu.Nam as iNam
		from TL_DS_CapNhap_BangLuong truythu
		INNER JOIN TL_BangLuong_Thang_TruyThu bangluong ON truythu.id = bangluong.parent
		where truythu.Ma_CachTL = ''CACH1''
			AND truythu.THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
			AND truythu.NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
			AND truythu.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		group by truythu.Ma_CBo, bangluong.Ma_Hieu_CanBo, bangluong.Ma_CBo, bangluong.Ma_DonVi,bangluong.Ma_PhuCap, truythu.Thang,truythu.Nam
	),
	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			donVi.Ma_DonVi		AS MaDonVi,
			capBac.Ma_Cb			AS MaCapBac,
			capBac.XauNoiMa
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac capBac
		  ON canBo.Ma_CB = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
		  ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, HSChucVu, MaCapBac, XauNoiMa, ' + @MaPhuCap + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.HSChucVu,
			canBo.MaCapBac,
			bangLuong.MaPhuCap,
			bangLuong.GiaTri  - isnull(truyThu.GiaTri, 0) AS GiaTri,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
		LEFT JOIN BangLuongTruyThu truyThu 
			ON bangLuong.MaCanBo = truyThu.Ma_CBo AND canBo.MaDonVi = truyThu.MaDonVi AND bangLuong.Nam = truyThu.iNam
			AND bangLuong.Thang = truyThu.iThang AND bangLuong.MaPhuCap = truyThu.Ma_PhuCap
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @MaPhuCap + ')
	) pvt'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY HSChucVu , MaCapBac , Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	execute(@Query)
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_bang_luong_truy_thu]    Script Date: 5/22/2024 2:45:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[sp_tl_delete_bang_luong_truy_thu]
	@thang int, 
	@nam int,
	@maDonVi nvarchar(MAX),
	@maCachTl nvarchar(10)
As
Begin
	Delete From [dbo].[TL_DS_CapNhap_BangLuong]
	Where thang = @thang and nam = @nam and ma_cbo in (select * from f_split(@maDonVi)) and Ma_CachTL = @maCachTl
	Delete From [dbo].[TL_BangLuong_Thang_TruyThu]
	Where thang = @thang and nam = @nam and Ma_DonVi in (select * from f_split(@maDonVi)) and Ma_CachTL = @maCachTl
End
;
;
;
GO

