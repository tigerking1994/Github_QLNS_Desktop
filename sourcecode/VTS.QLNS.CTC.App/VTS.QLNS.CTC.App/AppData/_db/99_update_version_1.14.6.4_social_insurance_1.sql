/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 6/21/2024 4:09:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 6/21/2024 4:09:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 6/21/2024 4:09:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
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
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'PCNU_TT,PCCOV_HS,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
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
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

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
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTNVK_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''HSBL_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
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
--canBo.NTN,
canBo.XauNoiMa
into tbl_luong_bhxh
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

UPDATE tbl_luong_bhxh SET 
PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) * isnull(PCCOV_HS, 0)

UPDATE tbl_luong_bhxh SET
LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0) + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

UPDATE tbl_luong_bhxh SET
PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(PHAITRUKHAC_SUM, 0)

UPDATE tbl_luong_bhxh SET
BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

UPDATE tbl_luong_bhxh SET
THANHTIEN = isnull(THANHTIEN, 0) - isnull(PHAITRU_SUM, 0)

select * from tbl_luong_bhxh;

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_bhxh]') AND type in (N'U')) drop table tbl_luong_bhxh;

END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 6/21/2024 4:09:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'PCNU_TT,PCCOV_HS,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH 
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
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
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

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
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTNVK_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''HSBL_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM TL_BangLuong_Thang bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
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
canBo.Tnn,
canBo.XauNoiMa
into tbl_luong_bhxh_2
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

UPDATE tbl_luong_bhxh_2 SET 
PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) * isnull(PCCOV_HS, 0)

UPDATE tbl_luong_bhxh_2 SET
LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0) + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

UPDATE tbl_luong_bhxh_2 SET
PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(PHAITRUKHAC_SUM, 0)

UPDATE tbl_luong_bhxh_2 SET
BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

UPDATE tbl_luong_bhxh_2 SET
THANHTIEN = isnull(THANHTIEN, 0) - isnull(PHAITRU_SUM, 0)

select * from tbl_luong_bhxh_2;

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_bhxh_2]') AND type in (N'U')) drop table tbl_luong_bhxh_2;

END
;
;
;
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha]    Script Date: 6/25/2024 11:26:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi]    Script Date: 6/25/2024 11:26:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi]    Script Date: 6/25/2024 11:26:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi] 
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX)
AS
BEGIN
	
	select
		ctct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iLoai, 
		isnull(sum(ctct.iQSBQNam), 0) iQSBQNam,
		isnull(sum(ctct.fLuongChinh), 0) fLuongChinh,
		isnull(sum(ctct.fPCChucVu), 0) fPCChucVu,
		isnull(sum(ctct.fPCTNNghe), 0) fPCTNNghe,
		isnull(sum(ctct.fPCTNVuotKhung), 0) fPCTNVuotKhung,
		isnull(sum(ctct.fNghiOm), 0) fNghiOm,
		isnull(sum(ctct.fHSBL), 0) fHSBL,
		(isnull(sum(ctct.fLuongChinh), 0) + isnull(sum(ctct.fPCChucVu), 0) + isnull(sum(ctct.fPCTNNghe), 0) + isnull(sum(ctct.fPCTNVuotKhung), 0) + isnull(sum(ctct.fNghiOm), 0) + isnull(sum(ctct.fHSBL), 0)) fTongQTLN
		from 
	BH_QTT_BHXH_ChungTu ct
	join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu =  ctct.iID_QTT_BHXH_ChungTu
	where ct.iNamLamViec = @INamLamViec
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and  ct.iQuyNamLoai <> 2
		and ct.bIsKhoa = 1
	group by ctct.sXauNoiMa, ct.iID_MaDonVi, ct.iLoai

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha]    Script Date: 6/25/2024 11:26:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha] 
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IdMaDonViCha nvarchar(MAX)
AS
BEGIN
	
	select
		ctct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iLoai, 
		isnull(sum(ctct.iQSBQNam), 0) iQSBQNam,
		isnull(sum(ctct.fLuongChinh), 0) fLuongChinh,
		isnull(sum(ctct.fPCChucVu), 0) fPCChucVu,
		isnull(sum(ctct.fPCTNNghe), 0) fPCTNNghe,
		isnull(sum(ctct.fPCTNVuotKhung), 0) fPCTNVuotKhung,
		isnull(sum(ctct.fNghiOm), 0) fNghiOm,
		isnull(sum(ctct.fHSBL), 0) fHSBL,
		(isnull(sum(ctct.fLuongChinh), 0) + isnull(sum(ctct.fPCChucVu), 0) + isnull(sum(ctct.fPCTNNghe), 0) + isnull(sum(ctct.fPCTNVuotKhung), 0) + isnull(sum(ctct.fNghiOm), 0) + isnull(sum(ctct.fHSBL), 0)) fTongQTLN
		from 
	BH_QTT_BHXH_ChungTu ct
	join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu =  ctct.iID_QTT_BHXH_ChungTu
	where ct.iNamLamViec = @INamLamViec
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdMaDonViCha))
		and ct.iQuyNamLoai <> 2
		and ct.bIsKhoa = 1
		and ct.iLoai = 1
	group by ctct.sXauNoiMa, ct.iID_MaDonVi, ct.iLoai

END
GO



/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_nhan_du_toan]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_nhan_du_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_nhan_du_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_du_toan]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_du_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_du_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop_qt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop_qt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_qt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_qt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv_qt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv_qt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop_qt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop_qt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_qt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_qt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 6/26/2024 5:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán

	union all

	select 6 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

	union all

	select 7 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 8 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 9 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 10 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
	union all
	select
	11 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	union all
	select 12 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
	union all
	select 13 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 14 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 15 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 16 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
	18 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 19 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 20 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 21 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 22 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 23 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	24 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 25 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 26 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 27 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 28 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 29 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	36 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	union all
	select
	40 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		41 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		42 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	union all
	select
	43 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		44 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		45 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union
	select
	47 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	49 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	union all
	select
		50 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		51 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

	union all
	select
	52 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		53 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		54 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
	--BHTN
	--Lấy dữ liệu khối dự toán
	union all
	select
	59 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		60 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
	union all
	select
		61 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
	union all
	select
		62 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
	union all
	select
		63 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		64 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	union all
	select
	65 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		66 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
	union all
	select
		67 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select
		68 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select
		69 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		70 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu khối hạch toán
	union all
	select
	72 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		73 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		74 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		75 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		76 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		77 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	78 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		79 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		80 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		81 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		82 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		83 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)

	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)

	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)

	union all
	select
	17 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)

	--BHYT
	union all
	select
	31 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	33 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	34 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	37 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and STT in (40, 43) and MaSo in (18, 19)

	union all
	select
		38 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (41,44)

	union all
	select
		39 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (42,45)

	union all
	select
	48 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and STT in (49,52) and MaSo in (23, 24)
	--BHTN
	union all
	select
	56 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	57 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	58 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	71 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')

	union all
	select
	55 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+30', '31=32+33')

	union all
	select
	32 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	and MaSo in ('13=18+23', '14=19+24')

	union all
	select
	35 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (36, 37) and MaSo in ('16','17=18+19')
	union all
	select
	46 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (47, 48) and MaSo in ('21','22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (36)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (36)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (36)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (47)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (47)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (47)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	30 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (35, 46) and MaSo in ('15=16+17','20=21+22')
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_qt]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all

	select 6 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

	union all

	select 7 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 8 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 9 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 10 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
	union all
	select
	11 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union all
	select 12 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
	union all
	select 13 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 14 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 15 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 16 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
	18 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 19 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 20 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 21 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 22 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 23 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	24 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 25 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 26 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 27 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 28 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 29 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	36 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	union all
	select
	40 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		41 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		42 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	union all
	select
	43 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		44 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		45 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union
	select
	47 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	49 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	union all
	select
		50 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		51 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

	union all
	select
	52 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		53 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		54 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
	--BHTN
	--Lấy dữ liệu khối dự toán
	union all
	select
	59 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		60 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
	union all
	select
		61 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
	union all
	select
		62 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
	union all
	select
		63 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		64 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	union all
	select
	65 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		66 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
	union all
	select
		67 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select
		68 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select
		69 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		70 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu khối hạch toán
	union all
	select
	72 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		73 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		74 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		75 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		76 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		77 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	78 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		79 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		80 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		81 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		82 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		83 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	BH_DTT_BHXH_DieuChinh_ChiTiet ctct 
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)

	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)

	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)

	union all
	select
	17 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)

	--BHYT
	union all
	select
	31 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	33 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	34 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	37 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and STT in (40, 43) and MaSo in (18, 19)

	union all
	select
		38 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (41,44)

	union all
	select
		39 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (42,45)

	union all
	select
	48 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and STT in (49,52) and MaSo in (23, 24)
	--BHTN
	union all
	select
	56 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	57 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	58 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	71 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')

	union all
	select
	55 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+30', '31=32+33')

	union all
	select
	32 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	and MaSo in ('13=18+23', '14=19+24')

	union all
	select
	35 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (36, 37) and MaSo in ('16','17=18+19')
	union all
	select
	46 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (47, 48) and MaSo in ('21','22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (36)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (36)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (36)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (47)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (47)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (47)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	30 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (35, 46) and MaSo in ('15=16+17','20=21+22')
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bIsKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán

	union all

	select 6 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

	union all

	select 7 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 8 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 9 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 10 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
	union all
	select
	11 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	union all
	select 12 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
	union all
	select 13 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 14 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 15 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 16 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
	18 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 19 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 20 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 21 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 22 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 23 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	24 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 25 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 26 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 27 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 28 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 29 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	36 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	union all
	select
	40 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		41 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		42 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	union all
	select
	43 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		44 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		45 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union
	select
	47 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	49 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	union all
	select
		50 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		51 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

	union all
	select
	52 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		53 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		54 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
	--BHTN
	--Lấy dữ liệu khối dự toán
	union all
	select
	59 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		60 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
	union all
	select
		61 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
	union all
	select
		62 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
	union all
	select
		63 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		64 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	union all
	select
	65 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		66 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
	union all
	select
		67 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select
		68 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select
		69 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		70 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu khối hạch toán
	union all
	select
	72 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		73 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		74 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		75 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		76 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		77 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	78 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		79 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		80 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		81 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		82 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		83 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pb.bIsKhoa = 1
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)

	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)

	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)

	union all
	select
	17 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)

	--BHYT
	union all
	select
	31 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	33 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	34 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	37 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and STT in (40, 43) and MaSo in (18, 19)

	union all
	select
		38 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (41,44)

	union all
	select
		39 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (42,45)

	union all
	select
	48 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and STT in (49,52) and MaSo in (23, 24)
	--BHTN
	union all
	select
	56 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	57 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	58 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	71 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')

	union all
	select
	55 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+30', '31=32+33')

	union all
	select
	32 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	and MaSo in ('13=18+23', '14=19+24')

	union all
	select
	35 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (36, 37) and MaSo in ('16','17=18+19')
	union all
	select
	46 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (47, 48) and MaSo in ('21','22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (36)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (36)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (36)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (47)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (47)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (47)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	30 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (35, 46) and MaSo in ('15=16+17','20=21+22')
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop_qt]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int,
	@LstMaDonVi NVARCHAR(MAX)
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all

	select 6 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

	union all

	select 7 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN

	union all

	select 8 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS

	union all

	select 9 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP

	union all

	select 10 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
	
	union all
	
	select
	11 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where  ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	
	union all
	
	select 12 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
	
	union all
	
	select 13 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	
	union all
	
	select 14 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	
	union all
	
	select 15 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where  ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	
	union all
	
	select 16 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	
	union all
	
	select
	18 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	
	union all
	
	select 19 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	
	union all
	
	select 20 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	
	union all
	
	select 21 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	
	union all
	
	select 22 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	
	union all
	
	select 23 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	
	select
	24 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where  ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	
	union all
	
	select 25 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	
	union all
	
	select 26 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	
	union all
	
	select 27 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	
	union all
	
	select 28 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	
	union all
	
	select 29 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	-----------------------------------------
	--BHYT
	-- Khối dự toán
	
	union all
	
	select
	36 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	
	union all
	
	select
	40 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	
	select
		41 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	
	union all
	
	select
		42 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	union all
	
	select
	43 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	
	select
		44 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	
	union all
	
	select
		45 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union all
	
	select
	47 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union all
	
	select
	49 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all

	select
		50 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	
	union all
	
	select
		51 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

	union all
	
	select
	52 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where  ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	
	select
		53 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	
	union all
	
	select
		54 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
	--BHTN
	--Lấy dữ liệu khối dự toán
	
	union all
	
	select
	59 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	
	select
		60 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
	
	union all
	
	select
		61 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
	
	union all
	
	select
		62 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
	
	union all
	
	select
		63 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	
	union all
	
	select
		64 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	union all
	
	select
	65 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	
	select
		66 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
	
	union all
	
	select
		67 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	
	union all
	
	select
		68 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	
	union all
	
	select
		69 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	
	union all
	
	select
		70 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu khối hạch toán
	union all
	
	select
	72 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	
	select
		73 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	
	union all
	
	select
		74 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	
	union all
	
	select
		75 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	
	union all
	
	select
		76 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	
	union all
	
	select
		77 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	
	select
	78 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	
	select
		79 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	
	union all
	
	select
		80 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	
	union all
	
	select
		81 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	
	union all
	
	select
		82 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	
	union all
	
	select
		83 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb ON ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)

	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)

	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)

	union all
	select
	17 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)

	--BHYT
	union all
	select
	31 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	33 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	34 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	37 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and STT in (40, 43) and MaSo in (18, 19)

	union all
	select
		38 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (41,44)

	union all
	select
		39 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (42,45)

	union all
	select
	48 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and STT in (49,52) and MaSo in (23, 24)
	--BHTN
	union all
	select
	56 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	57 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	58 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	71 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')

	union all
	select
	55 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+30', '31=32+33')

	union all
	select
	32 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	and MaSo in ('13=18+23', '14=19+24')

	union all
	select
	35 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (36, 37) and MaSo in ('16','17=18+19')
	union all
	select
	46 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (47, 48) and MaSo in ('21','22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (36)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (36)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (36)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (47)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (47)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (47)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	30 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (35, 46) and MaSo in ('15=16+17','20=21+22')
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		5 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
		6 STT,
		N'6' MaSo,
		N'- Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		6 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi
	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
		8 STT,
		N'8' MaSo,
		N'- Người lao động đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		8 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
		9 STT,
		N'9' MaSo,
		N'- Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		N'HT' Khoi,
		N'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		9 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		N'HT' Khoi,
		N'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toánand dv.iTrangThai = 1
	group by dv.sTenDonVi
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union all
	select
		16 STT,
		N'16' MaSo,
		N'- BHYT quân nhân' NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in
		(
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002'  -- HSQ, BS
		)

	union all
	select
		16 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in (
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002')  -- HSQ, BS
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
		18 STT,
		N'18' MaSo,
		N'+ Người lao động đóng' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)

	union all
	select
		18 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in (
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001')  -- LĐHĐ
	and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
		19 STT,
		N'19' MaSo,
		N'+ Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)

	union all
	select
		19 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in (
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001')  -- LĐHĐ
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	-- Khối hạch toán
	union all
	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union all
	select
		21 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
		and pbct.sXauNoiMa in (
		'9020002-010-011-0001-0000',  -- Sĩ quan
		'9020002-010-011-0001-0001'	, -- QNCN
		'9020002-010-011-0001-0002')  -- HSQ, BS
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		23 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
		and pbct.sXauNoiMa in (
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001')  -- LĐHĐ
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		24 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
		and pbct.sXauNoiMa in (
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001')  -- LĐHĐ
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	--BHTN
	--Lấy dữ liệu khối dự toán
	union all
	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		29 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		30 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	--Lấy dữ liệu khối hạch toán
	union all
	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		32 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		33 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)
	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)
	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)
	union all
	select
	7 STT,
	N'7=8+9' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)
	--BHYT
	union all
	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18, 19)

	union all
	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23, 24)
	--BHTN
	union all
	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	31 STT,
	N'31=32+33' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union all
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')
	union all
	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+330', '31=32+33')

	union all
	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	union all
	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (16, 17) and MaSo in ('16', '17=18+19')
	union all
	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (21, 22) and MaSo in ('21', '22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT = 16 and MaSo = 16),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT = 16 and MaSo = 16),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT = 16 and MaSo = 16),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT = 21 and MaSo = 21),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT = 21 and MaSo = 21),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT = 21 and MaSo = 21),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20 and MaSo = '20=21+22'

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20 and MaSo = '20=21+22'

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union all
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union all
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha 
	from tbl_ddt_bhxh
	order by STT

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv_qt]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		5 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
	group by dv.sTenDonVi

	union all
	select
		6 STT,
		N'6' MaSo,
		N'- Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		6 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
	group by dv.sTenDonVi
	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
		8 STT,
		N'8' MaSo,
		N'- Người lao động đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		8 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
	group by dv.sTenDonVi

	union all
	select
		9 STT,
		N'9' MaSo,
		N'- Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		N'HT' Khoi,
		N'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		9 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		N'HT' Khoi,
		N'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
	group by dv.sTenDonVi
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union all
	select
		16 STT,
		N'16' MaSo,
		N'- BHYT quân nhân' NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in
		(
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002'  -- HSQ, BS
		)

	union all
	select
		16 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in (
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002')  -- HSQ, BS
	group by dv.sTenDonVi

	union all
	select
		18 STT,
		N'18' MaSo,
		N'+ Người lao động đóng' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)

	union all
	select
		18 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in (
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001')  -- LĐHĐ
	group by dv.sTenDonVi

	union all
	select
		19 STT,
		N'19' MaSo,
		N'+ Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)

	union all
	select
		19 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in (
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001')  -- LĐHĐ
	group by dv.sTenDonVi

	-- Khối hạch toán
	union all
	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union all
	select
		21 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and ctct.sXauNoiMa in (
		'9020002-010-011-0001-0000',  -- Sĩ quan
		'9020002-010-011-0001-0001'	, -- QNCN
		'9020002-010-011-0001-0002')  -- HSQ, BS
	group by dv.sTenDonVi

	union all
	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		23 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and ctct.sXauNoiMa in (
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001')  -- LĐHĐ
	group by dv.sTenDonVi

	union all
	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		24 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and ctct.sXauNoiMa in (
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001')  -- LĐHĐ
	group by dv.sTenDonVi

	--BHTN
	--Lấy dữ liệu khối dự toán
	union all
	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		29 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
	group by dv.sTenDonVi

	union all
	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		30 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
	group by dv.sTenDonVi

	--Lấy dữ liệu khối hạch toán
	union all
	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		32 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
	group by dv.sTenDonVi

	union all
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		33 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and pb.bKhoa = 1 
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec and dv.iTrangThai = 1
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
	group by dv.sTenDonVi
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)
	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)
	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)
	union all
	select
	7 STT,
	N'7=8+9' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)
	--BHYT
	union all
	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18, 19)

	union all
	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23, 24)
	--BHTN
	union all
	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	31 STT,
	N'31=32+33' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union all
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')
	union all
	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+330', '31=32+33')

	union all
	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	union all
	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (16, 17) and MaSo in ('16', '17=18+19')
	union all
	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (21, 22) and MaSo in ('21', '22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT = 16 and MaSo = 16),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT = 16 and MaSo = 16),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT = 16 and MaSo = 16),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT = 21 and MaSo = 21),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT = 21 and MaSo = 21),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT = 21 and MaSo = 21),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20 and MaSo = '20=21+22'

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20 and MaSo = '20=21+22'

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union all
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union all
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha 
	from tbl_ddt_bhxh
	order by STT

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán

	union

	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union

	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán

	union

	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	-- Khối hạch toán
	union

	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	--BHTN
	--Lấy dữ liệu khối dự toán
	union

	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020001' -- Khối dự toán

	union

	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union

	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pb.bKhoa = 1
	and pbct.sLNS = '9020002' -- Khối hạch toán
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'

	union

	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'

	union

	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'

	union

	select
	7 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'

	--BHYT
	union

	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'

	union

	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'

	union

	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'

	union

	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18,19)

	union

	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23,24)

	--BHTN
	union

	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'

	union

	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'

	union

	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'

	union

	select
	31 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child

	union

	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'

	union

	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'

	union

	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'

	union

	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (16, 17)

	union

	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_qt]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union

	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union

	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union

	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	-- Khối hạch toán
	union

	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	--BHTN
	--Lấy dữ liệu khối dự toán
	union

	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union

	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union

	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	left join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct on pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'

	union

	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'

	union

	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'

	union

	select
	7 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'

	--BHYT
	union

	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'

	union

	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'

	union

	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'

	union

	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18,19)

	union

	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23,24)

	--BHTN
	union

	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'

	union

	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'

	union

	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'

	union

	select
	31 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child

	union

	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'

	union

	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'

	union

	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'

	union

	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (16, 17)

	union

	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int,
	@LstMaDonVi NVARCHAR(MAX)
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

	union

	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union

	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

	union

	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	-- Khối hạch toán
	union

	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	--BHTN
	--Lấy dữ liệu khối dự toán
	union

	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

	union

	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union

	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	left join 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'

	union

	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'

	union

	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'

	union

	select
	7 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'

	--BHYT
	union

	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'

	union

	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'

	union

	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'

	union

	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18,19)

	union

	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23,24)

	--BHTN
	union

	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'

	union

	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'

	union

	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'

	union

	select
	31 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child

	union

	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'

	union

	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'

	union

	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'

	union

	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (16, 17)

	union

	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop_qt]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop_qt]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int,
	@LstMaDonVi NVARCHAR(MAX)
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where 
	ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union

	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union

	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union

	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	-- Khối hạch toán
	union

	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	--BHTN
	--Lấy dữ liệu khối dự toán
	union

	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union

	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union

	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union

	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select ctct.*
		from
		BH_DTT_BHXH_DieuChinh ct join
		BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
		where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
		and ct.iNamLamViec = @NamLamViec
	) ctct 
	left join BH_DTT_BHXH_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join BH_DTT_BHXH_ChungTu_ChiTiet pbct on ctct.sXauNoiMa = pbct.sXauNoiMa and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and pb.bIsKhoa = 1
	where ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'

	union

	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'

	union

	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'

	union

	select
	7 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'

	--BHYT
	union

	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'

	union

	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'

	union

	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'

	union

	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18,19)

	union

	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23,24)

	--BHTN
	union

	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'

	union

	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'

	union

	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'

	union

	select
	31 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child

	union

	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'

	union

	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'

	union

	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'

	union

	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (16, 17)

	union

	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_du_toan]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_du_toan] 
	@MaDonVi NVARCHAR(255),
	@NamLamViec int
AS
BEGIN
	
	select count(1) ICountRow
	from (
		select ctct.*
		from 
		BH_DTT_BHXH_PhanBo_ChungTu ct
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		where ct.iNamLamViec = @NamLamViec
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
			and ct.bKhoa = 1) dt

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_nhan_du_toan]    Script Date: 6/26/2024 5:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_count_nhan_du_toan] 
	@MaDonVi NVARCHAR(255),
	@NamLamViec int
AS
BEGIN
	
	select count(1) ICountRow
	from (
		select ctct.*
		from 
		BH_DTT_BHXH_ChungTu ct
		join BH_DTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
		where ct.iNamLamViec = @NamLamViec
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
			and ct.bIsKhoa = 1) dt

END
GO
