/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 9/9/2024 4:51:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
GO
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

-- Update so phai tru BHXH, BHYT, BHTN khi hưởng chế dộ khác BENHDAINGAY_TD14NGAY và OMKHAC_D14NGAY
UPDATE tbl_luong_bhxh SET BHXHCN_TT = 0, BHYTCN_TT = 0, BHTNCN_TT = 0
WHERE MaCanBo in (
select sMaCBo from TL_BangLuong_ThangBHXH
where iNam = @Nam
and iThang = @Thang
and sMaCheDo in ('BENHDAINGAY_T14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'OMDAU_DUONGSUCPHSK', 'TAINANLD_DUONGSUCPHSK', 'KHAMTHAI', 'KHHGD', 'NAMNGHIKHIVOSINHCON', 'THAISAN_DUONGSUCPHSK'))

UPDATE tbl_luong_bhxh SET
BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

UPDATE tbl_luong_bhxh SET
PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(PHAITRUKHAC_SUM, 0)

UPDATE tbl_luong_bhxh SET
THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

IF @IsOrderChucVu = 1
select * from tbl_luong_bhxh
ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC
else
select * from tbl_luong_bhxh
ORDER BY MaCapBac DESC, Ten ASC

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

/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 9/25/2024 2:28:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]    Script Date: 9/25/2024 2:28:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]    Script Date: 9/25/2024 2:28:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 9/25/2024 2:28:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 9/25/2024 2:28:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]    Script Date: 9/25/2024 2:28:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]    Script Date: 9/25/2024 2:28:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
SELECT gt.*,dv.sTenDonVi INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
left join DonVi dv on dv.iID_MaDonVi=gt.iiD_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1
	--- 9010001-010-011-0004 Tro cap Huu tri
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailHuuTriSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #TempDetailHuuTri
			from  #TempDetailHuuTriSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh


	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
			, N'TC Hưu Trí'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailHuuTri	
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

		UPDATE #tempHuuTri
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  bHangCha=0 ) detail
		where #tempHuuTri.bHangCha=1
	--- 9010001-010-011-0005 Tro cap phuc vien

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailPhucVienSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #tempDetailPhucVien
			from  #tempDetailPhucVienSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC

	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	) tblPhucVien

	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1

		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  bHangCha=0 ) detail
		where #tempPhucVien.bHangCha=1

	--- 9010001-010-011-0006 Tro cap xuat ngu

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #tempDetailXuatNgu
			from  #tempDetailXuatNguSum tbltc
			GROUP BY 
					 tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenPhanHo 
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh

	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
			, N'TC Xuất ngũ'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac		
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailXuatNgu
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1
 
 		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  bHangCha=0 ) detail
		where #tempXuatNgu.bHangCha=1
	--- 9010001-010-011-0007 tro cap thoi viec

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailThoiViecSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			into #tempDetailThoiViec
			from  #tempDetailThoiViecSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
			, N'TC Thôi việc'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  bHangCha=0 ) detail
		where #tempThoiViec.bHangCha=1
	--- 9010001-010-011-0008 tro cap tu tuat

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV

			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL

			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailTuTuatSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			into #tempDetailTuTuat
			from  #tempDetailTuTuatSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC



	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailTuTuat
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  bHangCha=0 ) detail
		where #tempTuTuat.bHangCha=1
		-- ket qua
	SELECT * INTO #tempRESULT  
	FROM
	(
		SELECT * FROM #tempHuuTri
		UNION ALL 
		SELECT * FROM #tempPhucVien
		UNION ALL 
		SELECT * FROM #tempXuatNgu
		UNION ALL
		SELECT * FROM #tempThoiViec
		UNION ALL
		SELECT * FROM #tempTuTuat
	) TBLRESULT

	SELECT * FROM #tempRESULT

	 DROP TABLE #tempHuuTri
	 DROP TABLE #tempPhucVien
	 DROP TABLE #tempXuatNgu
	 DROP TABLE #tempThoiViec
	 DROP TABLE #tempTuTuat
	 DROP TABLE #tempRESULT
	 DROP TABLE #TempDetailHuuTri
	 DROP TABLE #tempDetailPhucVien
	 DROP TABLE #TempDetailTuTuat
	 DROP TABLE #tempDetailThoiViec
	 DROP TABLE #tempDetailXuatNgu
	 DROP TABLE #tempDetailHuuTriSum
	 DROP TABLE #tempDetailPhucVienSum
	 DROP TABLE #TempDetailTuTuatSum
	 DROP TABLE #tempDetailThoiViecSum
	 DROP TABLE #tempDetailXuatNguSum

END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 9/25/2024 2:28:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN

	---IRemainRow 1: SQ, 2 : QNCN,3:CC, CN, VCQP,4:HSQBS, 5:LĐHĐ
	--- Type  :3 :Huu Tri,phuc vien,xuat ngu,thoi viec, tu tuat
	--- 2: Khoi du Toan, Khoi hach toan
	--- 1 SQ, QNCN,CC, CN, VCQP,HSQBS, LĐHĐ
	---- Ikhoi : 2  Khoi Du toan, 1 Khoi Hach Toan
	SELECT gt.*,dv.sTenDonVi INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

	SELECT gt.*,dv.sTenDonVi INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (
			gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

		--- Huu Tri Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriDuToan

		--- Phuc Vien Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienDuToan

		--- TC Xuất ngũ Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguDuToan

		--- TC Thôi việc  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecDuToan

		--- TC Tử tuất  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatDuToan

		--- Huu Tri Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriHachToan

		--- Phuc Vien Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienHachToan

		--- TC Xuất ngũ Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguHachToan

		--- TC Thôi việc  Khoi Hach Toan

			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecHachToan

		--- TC Tử tuất  Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatHachToan

		--- Khoi Du Toan
			SELECT 
					1 bHangCha
					, N'Khối dự toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 2 IKhoi
					into #tempKhoiDuToan

		--- Khoi Hach Toan
			SELECT 
					1 bHangCha
					, N'Khối hạch toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 1 IKhoi
					into #tempKhoiHachToan

		--- Sĩ quan
			SELECT 
				1 bHangCha
				, N'Sĩ quan' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 1 IRemainRow
				, 0 IKhoi
				into #tempSiQuan

		--- QNCN
			SELECT 
				1 bHangCha
				, N'QNCN' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 2 IRemainRow
				, 0 IKhoi
				into #tempQNCN

		--- CC, CN, VCQP
			SELECT 
				1 bHangCha
				, N'CC, CN, VCQP' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 3 IRemainRow
				, 0 IKhoi
				into #tempCNVCQP

        --- HSQBS
			SELECT 
				1 bHangCha
				, N'HSQBS' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 4 IRemainRow
				, 0 IKhoi
				into #tempHSQBS

		--- LĐHĐ
			SELECT 
				1 bHangCha
				, N'LĐHĐ' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 5 IRemainRow
				, 0 IKhoi
				into #tempLDHD
--- Du Toan
--- Huu Tri Du Toan
		--- Detal Si Quan DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
		--- Detal QNCN DuToan Huu Tri
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanHuuTri
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa)  Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	SELECT 0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanHuuTri
	FROM
			--- Detal CNVCQP DuToan Huu Tri
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanHuuTri
	FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanHuuTri
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Phục viên 
	SELECT 
		0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCapKV) FTienTroCapKV
				, SUM(FTienTroCapMT) FTienTroCapMT
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTroCapKVTL) FTienTroCapKVTL
				, SUM(FTienTroCapMTTL) FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 2 IRemainRow
				, 2 IKhoi
				into #tempDetailQNCNDuToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP DuToan Phuc Vien
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanPhucVien
		FROM (
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal HSQBS DuToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanPhucVien
		FROM
			( SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Phuc Vien
	SELECT  
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.sMaCapBac
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 1 IRemainRow
		, 2 IKhoi
		into #tempDetailLDHDDuToanPhucVien
			FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
		, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
		, Detail.sTenPhanHo
		, '' as SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 1 IRemainRow
		, 2 IKhoi
				into #tempDetailSiQuanDuToanXuatNgu
	FROM (
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac	

			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal QNCN DuToan Xuat Ngu 	
	SELECT 0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
		, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
		, Detail.sTenPhanHo
		, '' as SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 2 IRemainRow
		, 2 IKhoi
		into #tempDetailQNCNDuToanXuatNgu
		 FROM

			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	--- Detal CNVCQP DuToan Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
		, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
		, Detail.sTenPhanHo
		, '' as SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 3 IRemainRow
		, 2 IKhoi
	into #tempDetailCNVCQPDuToanXuatNgu
	FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY tbltc.sMa_Hieu_Can_Bo,  tbltc.sTenDonVi
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	--- Detal HSQBS DuToan Xuat Ngu
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanXuatNgu
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
					,tbltc.sTenDonVi
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sTenPhanHo
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

	--- Detal LDHD DuToan Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
		, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
		, Detail.sTenPhanHo
		, '' as SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 5 IRemainRow
		, 2 IKhoi
		into #tempDetailLDHDDuToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY tbltc.sMa_Hieu_Can_Bo,  tbltc.sTenDonVi
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				 ) Detail
		GROUP BY  Detail.sTenPhanHo
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanThoiViec
	FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP DuToan Thoi Viec
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.sMaCapBac
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 3 IRemainRow
		, 2 IKhoi
		into #tempDetailCNVCQPDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Thoi Viec
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
		into #tempDetailHSQBSDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal LDHD DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanThoiViec
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanTuTuat
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				 ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal CNVCQP DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
			) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

		--- Detal QNCN HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanHuuTri
		FROM 		
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa  ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal HSQBS HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanHuuTri
		FROM 		
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanHuuTri
		FROM 	
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanPhucVien
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal QNCN HachToan Phục viên 

	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanPhucVien
		FROM 		
		(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


--- Detal CNVCQP HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanPhucVien
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal HSQBS HachToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal LDHD HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanPhucVien
		FROM
			(	SELECT  
				0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi sTenPhanHo
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sMa_Hieu_Can_Bo
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
				, 0 AS FTienTroCapMT
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
				, 0 as FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 5 IRemainRow
				, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
			AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND tbltc.sMaCapBac like  '1%'
				GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	--- Detal QNCN HachToan Xuat Ngu 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
				FROM  #tblTroCapKhoiHachToan tbltc
				WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
				AND ( tbltc.sMaCapBac LIKE '3.1%' 
						OR tbltc.sMaCapBac LIKE '3.2%' 
						OR tbltc.sMaCapBac LIKE '3.3%'  
						OR tbltc.sMaCapBac = '413' 
						OR tbltc.sMaCapBac = '415')
				GROUP BY tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
						, tbltc.sSoQuyetDinh
						, tbltc.dNgayQuyetDinh
						, tbltc.sXauNoiMa) Detail
			GROUP BY  Detail.sTenPhanHo
					, Detail.sSoQuyetDinh
					, Detail.dNgayQuyetDinh


			--- Detal HSQBS HachToan Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanXuatNgu
		FROM
			(SELECT  
				0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
				, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
				, tbltc.sTenDonVi sTenPhanHo
				, '' as SMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sMa_Hieu_Can_Bo
				, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
				, 0 AS  FTienTroCapKV
				, 0 AS FTienTroCapMT
				, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
				, 0 as FTienTroCapKVTL
				, 0 as FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 4 IRemainRow
				, 1 IKhoi

				--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sTenPhanHo
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanXuatNgu
		FROM
			(	
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
				GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
						, tbltc.sSoQuyetDinh
						, tbltc.dNgayQuyetDinh
						, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanThoiViec
		FROM
			(
				SELECT  
					0 bHangCha 
					, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi sTenPhanHo
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sMa_Hieu_Can_Bo
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
					, 0 AS FTienTroCapMT
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
					, 0 as FTienTroCapMTTL
					, 0 bHasData
					, 0 Type
					, null IsParent
					, 1 IRemainRow
					, 1 IKhoi

					--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
			AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
					) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN HachToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
					into #tempDetailQNCNHachToanThoiViec
		FROM
			(	SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
			AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
			) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa 			) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanThoiViec
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanThoiViec
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
			AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					,tbltc.sTenDonVi 
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
							) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal QNCN HachToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS HachToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,  tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa  ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

	--- 9010001-010-011-0004 Tro cap Huu tri
			SELECT * INTO #tempDetailDuToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanHuuTri
			)tblDetailHuuTri

			SELECT * INTO #tempDetailHachToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanHuuTri
			)tblDetailHuuTri

			--- UPDATE #tempDetailDuToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ##tempDetailHachToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempHuuTri FROM
			(
			SELECT * FROM #tempHuuTriDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanHuuTri
			UNION ALL
			SELECT * FROM #tempDetailHachToanHuuTri
			
			) tblHuutri

			--- UPDATE #tempHuuTri
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempHuuTri
				WHERE Type=0
			) B
			where  A.Type=3


	----- 9010001-010-011-0005 Tro cap phuc vien
			SELECT * INTO #tempDetailDuToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanPhucVien
			)tblDetailPhucVien

			SELECT * INTO #tempDetailHachToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanPhucVien
			)tblDetailPhucVien

		
			--- UPDATE ##tempDetailDuToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ###tempDetailHachToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempPhucVien FROM
			(
			SELECT * FROM #tempPhucVienDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanPhucVien
			UNION ALL
			SELECT * FROM #tempDetailHachToanPhucVien
			) tblPhucVien

			--- UPDATE #tempPhucVien
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempPhucVien
				WHERE Type=0
			) B
			where  A.Type=3

			
	----- 9010001-010-011-0006 Tro cap xuat ngu

			SELECT * INTO #tempDetailDuToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanXuatNgu
			)tblDetailPXuatNgu


			SELECT * INTO #tempDetailHachToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanXuatNgu
			)tblDetailXuatNgu

			--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2


			--- UPDATE #tempDetailHachToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- Update Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempXuatNgu FROM
			(
			SELECT * FROM #tempXuatNguDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanXuatNgu
			UNION ALL
			SELECT * FROM #tempDetailHachToanXuatNgu
			) tblXuatNgu

			--- UPDATE #tempXuatNgu
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempXuatNgu
				WHERE Type=0
			) B
			where  A.Type=3
	----- 9010001-010-011-0007 tro cap thoi viec
			SELECT * INTO #tempDetailDuToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanThoiViec
			)tblDetailThoiViec

			SELECT * INTO #tempDetailHachToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanThoiViec
			)tblDetailThoiViec

	 	--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanThoiViec
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempThoiViec FROM
			(
			SELECT * FROM #tempThoiViecDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanThoiViec
			UNION ALL
			SELECT * FROM #tempDetailHachToanThoiViec
			) tblThoiViec
			 
			 --- UPDATE #tempThoiViec
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempThoiViec
				WHERE Type=0
			) B
			where  A.Type=3
		
	----- 9010001-010-011-0008 tro cap tu tuat
			SELECT * INTO #tempDetailDuToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiDuToan 
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanTuTuat
			)tblDetailTuTuat

			SELECT * INTO #tempDetailHachToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanTuTuat
			)tblDetailTuTuat

			--- UPDATE #tempDetailDuToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempTuTuat FROM
			(
			SELECT * FROM #tempTuTuatDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanTuTuat
			UNION ALL
			SELECT * FROM #tempDetailHachToanTuTuat
			) tblTuTuat

			--- Update #tempTuTuat
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempTuTuat
				WHERE Type=0
			) B
			where  A.Type=3

			SELECT * INTO #tempRESULT  
			FROM
			(
				SELECT * FROM #tempHuuTri
				UNION ALL 
				SELECT * FROM #tempPhucVien
				UNION ALL 
				SELECT * FROM #tempXuatNgu
				UNION ALL
				SELECT * FROM #tempThoiViec
				UNION ALL
				SELECT * FROM #tempTuTuat
			) TBLRESULT

			SELECT * FROM #tempRESULT


		 DROP TABLE #tempRESULT
		 DROP TABLE #tblTroCapKhoiDuToan 
		 DROP TABLE #tblTroCapKhoiHachToan
		 DROP TABLE #tempHuuTri
		 DROP TABLE #tempDetailDuToanHuuTri
		 DROP TABLE #tempDetailHachToanHuuTri


		 DROP TABLE #tempPhucVien
		 DROP TABLE #tempDetailDuToanPhucVien
		 DROP TABLE #tempDetailHachToanPhucVien

		 DROP TABLE #tempXuatNgu 
		 DROP TABLE #tempDetailDuToanXuatNgu
		 DROP TABLE #tempDetailHachToanXuatNgu

		 DROP TABLE #tempThoiViec
		 DROP TABLE #tempDetailDuToanThoiViec
		 DROP TABLE #tempDetailHachToanThoiViec

		 DROP TABLE #tempTuTuat
		 DROP TABLE #tempDetailDuToanTuTuat
		 DROP TABLE #tempDetailHachToanTuTuat

		 DROP TABLE #tempKhoiDuToan
		 DROP TABLE #tempKhoiHachToan

		 DROP TABLE #tempHuuTriDuToan
		 DROP TABLE #tempPhucVienDuToan
		 DROP TABLE #tempXuatNguDuToan
		 DROP TABLE #tempThoiViecDuToan
		 DROP TABLE #tempTuTuatDuToan

		 DROP TABLE #tempSiQuan
		 DROP TABLE #tempQNCN
		 DROP TABLE #tempCNVCQP
		 DROP TABLE #tempHSQBS
		 DROP TABLE #tempLDHD

		 DROP TABLE #tempDetailSiQuanDuToanHuuTri
		 DROP TABLE #tempDetailQNCNDuToanHuuTri
		 DROP TABLE #tempDetailCNVCQPDuToanHuuTri
		 DROP TABLE #tempDetailHSQBSDuToanHuuTri
		 DROP TABLE #tempDetailLDHDDuToanHuuTri

		 DROP TABLE #tempDetailSiQuanDuToanPhucVien
		 DROP TABLE #tempDetailQNCNDuToanPhucVien
		 DROP TABLE #tempDetailCNVCQPDuToanPhucVien
		 DROP TABLE #tempDetailHSQBSDuToanPhucVien
		 DROP TABLE #tempDetailLDHDDuToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanDuToanXuatNgu
		 DROP TABLE #tempDetailQNCNDuToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPDuToanXuatNgu
		 DROP TABLE #tempDetailHSQBSDuToanXuatNgu
		 DROP TABLE #tempDetailLDHDDuToanXuatNgu

		 DROP TABLE #tempDetailSiQuanDuToanThoiViec
		 DROP TABLE #tempDetailQNCNDuToanThoiViec
		 DROP TABLE #tempDetailCNVCQPDuToanThoiViec
		 DROP TABLE #tempDetailHSQBSDuToanThoiViec
		 DROP TABLE #tempDetailLDHDDuToanThoiViec

		 DROP TABLE #tempDetailSiQuanDuToanTuTuat
		 DROP TABLE #tempDetailQNCNDuToanTuTuat
		 DROP TABLE #tempDetailCNVCQPDuToanTuTuat
		 DROP TABLE #tempDetailHSQBSDuToanTuTuat
		 DROP TABLE #tempDetailLDHDDuToanTuTuat

		 DROP TABLE #tempDetailSiQuanHachToanHuuTri
		 DROP TABLE #tempDetailQNCNHachToanHuuTri
		 DROP TABLE #tempDetailCNVCQPHachToanHuuTri
		 DROP TABLE #tempDetailHSQBSHachToanHuuTri
		 DROP TABLE #tempDetailLDHDHachToanHuuTri

		 DROP TABLE #tempDetailSiQuanHachToanPhucVien
		 DROP TABLE #tempDetailQNCNHachToanPhucVien
		 DROP TABLE #tempDetailCNVCQPHachToanPhucVien
		 DROP TABLE #tempDetailHSQBSHachToanPhucVien
		 DROP TABLE #tempDetailLDHDHachToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanHachToanXuatNgu
		 DROP TABLE #tempDetailQNCNHachToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPHachToanXuatNgu
		 DROP TABLE #tempDetailHSQBSHachToanXuatNgu
		 DROP TABLE #tempDetailLDHDHachToanXuatNgu

		 DROP TABLE #tempDetailSiQuanHachToanThoiViec
		 DROP TABLE #tempDetailQNCNHachToanThoiViec
		 DROP TABLE #tempDetailCNVCQPHachToanThoiViec
		 DROP TABLE #tempDetailHSQBSHachToanThoiViec
		 DROP TABLE #tempDetailLDHDHachToanThoiViec

		 DROP TABLE #tempDetailSiQuanHachToanTuTuat
		 DROP TABLE #tempDetailQNCNHachToanTuTuat
		 DROP TABLE #tempDetailCNVCQPHachToanTuTuat
		 DROP TABLE #tempDetailHSQBSHachToanTuTuat
		 DROP TABLE #tempDetailLDHDHachToanTuTuat


		 DROP TABLE #tempHuuTriHachToan
		 DROP TABLE #tempPhucVienHachToan
		 DROP TABLE #tempXuatNguHachToan
		 Drop table #tempThoiViecHachToan
		 Drop table #tempTuTuatHachToan

END
;
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 9/25/2024 2:28:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN

	---IRemainRow 1: SQ, 2 : QNCN,3:CC, CN, VCQP,4:HSQBS, 5:LĐHĐ
	--- Type  :3 :Huu Tri,phuc vien,xuat ngu,thoi viec, tu tuat
	--- 2: Khoi du Toan, Khoi hach toan
	--- 1 SQ, QNCN,CC, CN, VCQP,HSQBS, LĐHĐ
	---- Ikhoi : 2  Khoi Du toan, 1 Khoi Hach Toan
	SELECT gt.*,dv.sTenDonVi INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			)
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

		order by gt.sTenCanBo

	SELECT gt.*,dv.sTenDonVi INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (
			--- Hoach toan
			gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

		order by gt.sTenCanBo
		--- Huu Tri Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriDuToan

		--- Phuc Vien Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienDuToan

		--- TC Xuất ngũ Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguDuToan

		--- TC Thôi việc  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecDuToan

		--- TC Tử tuất  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatDuToan

		--- Huu Tri Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriHachToan

		--- Phuc Vien Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienHachToan

		--- TC Xuất ngũ Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguHachToan

		--- TC Thôi việc  Khoi Hach Toan

			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecHachToan

		--- TC Tử tuất  Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatHachToan

		--- Khoi Du Toan
			SELECT 
					1 bHangCha
					, N'Khối dự toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 2 IKhoi
					into #tempKhoiDuToan

		--- Khoi Hach Toan
			SELECT 
					1 bHangCha
					, N'Khối hạch toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 1 IKhoi
					into #tempKhoiHachToan

		--- Si Quan
			SELECT 
				1 bHangCha
				, N'Sĩ quan' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 1 IRemainRow
				, 0 IKhoi
				into #tempSiQuan

		--- QNCN
			SELECT 
				1 bHangCha
				, N'QNCN' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 2 IRemainRow
				, 0 IKhoi
				into #tempQNCN

		--- CC, CN, VCQP
			SELECT 
				1 bHangCha
				, N'CC, CN, VCQP' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 3 IRemainRow
				, 0 IKhoi
				into #tempCNVCQP

        --- HSQBS
			SELECT 
				1 bHangCha
				, N'HSQBS' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 4 IRemainRow
				, 0 IKhoi
				into #tempHSQBS

		--- LĐHĐ
			SELECT 
				1 bHangCha
				, N'LĐHĐ' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 5 IRemainRow
				, 0 IKhoi
				into #tempLDHD
--- Du Toan
--- Huu Tri Du Toan
		--- Detal Si Quan DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
		--- Detal QNCN DuToan Huu Tri
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanHuuTri
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa)  Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	SELECT 0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanHuuTri
	FROM
			--- Detal CNVCQP DuToan Huu Tri
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanHuuTri
	FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanHuuTri
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Phục viên 
	SELECT 
		0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCapKV) FTienTroCapKV
				, SUM(FTienTroCapMT) FTienTroCapMT
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTroCapKVTL) FTienTroCapKVTL
				, SUM(FTienTroCapMTTL) FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 2 IRemainRow
				, 2 IKhoi
				into #tempDetailQNCNDuToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP DuToan Phuc Vien
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanPhucVien
		FROM (
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal HSQBS DuToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanPhucVien
		FROM
			( SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Phuc Vien
	SELECT  
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.sMaCapBac
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 1 IRemainRow
		, 2 IKhoi
		into #tempDetailLDHDDuToanPhucVien
			FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 1 IRemainRow
		, 2 IKhoi
				into #tempDetailSiQuanDuToanXuatNgu
	FROM (
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac

			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal QNCN DuToan Xuat Ngu 	
	SELECT 0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 2 IRemainRow
		, 2 IKhoi
		into #tempDetailQNCNDuToanXuatNgu
		 FROM
			(SELECT  
				0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi sTenPhanHo
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sMa_Hieu_Can_Bo
				, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
				, 0 AS  FTienTroCapKV
				, 0 AS FTienTroCapMT
				, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
				, 0 as FTienTroCapKVTL
				, 0 as FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 2 IRemainRow
						, 2 IKhoi
				FROM  #tblTroCapKhoiDuToan tbltc
				WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
				AND tbltc.sMaCapBac  LIKE '2%'
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
							, tbltc.sTenCanBo
							, tbltc.sTenDonVi
							, tbltc.sMaCapBac
							, tbltc.sSoQuyetDinh
							, tbltc.dNgayQuyetDinh
							, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


	--- Detal CNVCQP DuToan Phuc Vien
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 3 IRemainRow
		, 2 IKhoi
	into #tempDetailCNVCQPDuToanXuatNgu
	FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	--- Detal HSQBS DuToan Xuat Ngu
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanXuatNgu
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 5 IRemainRow
		, 2 IKhoi
		into #tempDetailLDHDDuToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanThoiViec
	FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP DuToan Thoi Viec
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.sMaCapBac
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 3 IRemainRow
		, 2 IKhoi
		into #tempDetailCNVCQPDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Thoi Viec
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
		into #tempDetailHSQBSDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal LDHD DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanThoiViec
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanTuTuat
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				 ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal CNVCQP DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
			) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

		--- Detal QNCN HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanHuuTri
		FROM 		
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa  ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal HSQBS HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanHuuTri
		FROM 		
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanHuuTri
		FROM 	
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanPhucVien
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal QNCN HachToan Phục viên 

	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanPhucVien
		FROM 		
		(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


--- Detal CNVCQP HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanPhucVien
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal HSQBS HachToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal LDHD HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanPhucVien
		FROM
			(	SELECT  
				0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi sTenPhanHo
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sMa_Hieu_Can_Bo
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
				, 0 AS FTienTroCapMT
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
				, 0 as FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 5 IRemainRow
				, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
			AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND tbltc.sMaCapBac like  '1%'
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
						, tbltc.sTenCanBo
						, tbltc.sTenDonVi
						, tbltc.sMaCapBac
						, tbltc.sSoQuyetDinh
						, tbltc.dNgayQuyetDinh
						, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN HachToan Xuat Ngu 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					,tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa  ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Xuat Ngu 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
				FROM  #tblTroCapKhoiHachToan tbltc
				WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
				AND ( tbltc.sMaCapBac LIKE '3.1%' 
						OR tbltc.sMaCapBac LIKE '3.2%' 
						OR tbltc.sMaCapBac LIKE '3.3%'  
						OR tbltc.sMaCapBac = '413' 
						OR tbltc.sMaCapBac = '415')
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
						, tbltc.sTenCanBo
						, tbltc.sTenDonVi
						, tbltc.sMaCapBac
						, tbltc.sSoQuyetDinh
						, tbltc.dNgayQuyetDinh
						, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal HSQBS HachToan Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanXuatNgu
		FROM
			(		SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanXuatNgu
		FROM
			(	
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanThoiViec
		FROM
			(
				SELECT  
					0 bHangCha 
					, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi sTenPhanHo
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sMa_Hieu_Can_Bo
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
					, 0 AS FTienTroCapMT
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
					, 0 as FTienTroCapMTTL
					, 0 bHasData
					, 0 Type
					, null IsParent
					, 1 IRemainRow
					, 1 IKhoi

					--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
			AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
					) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN HachToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
					into #tempDetailQNCNHachToanThoiViec
		FROM
			(	SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
			AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
			) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa 			) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanThoiViec
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanThoiViec
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
			AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					,tbltc.sTenDonVi 
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
							) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal QNCN HachToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS HachToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,  tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa  ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

	--- 9010001-010-011-0004 Tro cap Huu tri
			SELECT * INTO #tempDetailDuToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanHuuTri
			)tblDetailHuuTri

			SELECT * INTO #tempDetailHachToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanHuuTri
			)tblDetailHuuTri

			--- UPDATE #tempDetailDuToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ##tempDetailHachToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempHuuTri FROM
			(
			SELECT * FROM #tempHuuTriDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanHuuTri
			UNION ALL
			SELECT * FROM #tempDetailHachToanHuuTri
			
			) tblHuutri

			--- UPDATE #tempHuuTri
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempHuuTri
				WHERE Type=0
			) B
			where  A.Type=3


	----- 9010001-010-011-0005 Tro cap phuc vien
			SELECT * INTO #tempDetailDuToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanPhucVien
			)tblDetailPhucVien

			SELECT * INTO #tempDetailHachToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanPhucVien
			)tblDetailPhucVien

		
			--- UPDATE ##tempDetailDuToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ###tempDetailHachToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempPhucVien FROM
			(
			SELECT * FROM #tempPhucVienDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanPhucVien
			UNION ALL
			SELECT * FROM #tempDetailHachToanPhucVien
			) tblPhucVien

			--- UPDATE #tempPhucVien
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempPhucVien
				WHERE Type=0
			) B
			where  A.Type=3

			
	----- 9010001-010-011-0006 Tro cap xuat ngu

			SELECT * INTO #tempDetailDuToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanXuatNgu
			)tblDetailPXuatNgu


			SELECT * INTO #tempDetailHachToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanXuatNgu
			)tblDetailXuatNgu

			--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2


			--- UPDATE #tempDetailHachToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- Update Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempXuatNgu FROM
			(
			SELECT * FROM #tempXuatNguDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanXuatNgu
			UNION ALL
			SELECT * FROM #tempDetailHachToanXuatNgu
			) tblXuatNgu

			--- UPDATE #tempXuatNgu
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempXuatNgu
				WHERE Type=0
			) B
			where  A.Type=3
	----- 9010001-010-011-0007 tro cap thoi viec
			SELECT * INTO #tempDetailDuToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanThoiViec
			)tblDetailThoiViec

			SELECT * INTO #tempDetailHachToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanThoiViec
			)tblDetailThoiViec

	 	--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanThoiViec
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempThoiViec FROM
			(
			SELECT * FROM #tempThoiViecDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanThoiViec
			UNION ALL
			SELECT * FROM #tempDetailHachToanThoiViec
			) tblThoiViec
			 
			 --- UPDATE #tempThoiViec
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempThoiViec
				WHERE Type=0
			) B
			where  A.Type=3
		
	----- 9010001-010-011-0008 tro cap tu tuat
			SELECT * INTO #tempDetailDuToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiDuToan 
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanTuTuat
			)tblDetailTuTuat

			SELECT * INTO #tempDetailHachToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanTuTuat
			)tblDetailTuTuat

			--- UPDATE #tempDetailDuToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempTuTuat FROM
			(
			SELECT * FROM #tempTuTuatDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanTuTuat
			UNION ALL
			SELECT * FROM #tempDetailHachToanTuTuat
			) tblTuTuat

			--- Update #tempTuTuat
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempTuTuat
				WHERE Type=0
			) B
			where  A.Type=3

			SELECT * INTO #tempRESULT  
			FROM
			(
				SELECT * FROM #tempHuuTri
				UNION ALL 
				SELECT * FROM #tempPhucVien
				UNION ALL 
				SELECT * FROM #tempXuatNgu
				UNION ALL
				SELECT * FROM #tempThoiViec
				UNION ALL
				SELECT * FROM #tempTuTuat
			) TBLRESULT

			SELECT * FROM #tempRESULT


		 DROP TABLE #tempRESULT
		 DROP TABLE #tblTroCapKhoiDuToan 
		 DROP TABLE #tblTroCapKhoiHachToan
		 DROP TABLE #tempHuuTri
		 DROP TABLE #tempDetailDuToanHuuTri
		 DROP TABLE #tempDetailHachToanHuuTri


		 DROP TABLE #tempPhucVien
		 DROP TABLE #tempDetailDuToanPhucVien
		 DROP TABLE #tempDetailHachToanPhucVien

		 DROP TABLE #tempXuatNgu 
		 DROP TABLE #tempDetailDuToanXuatNgu
		 DROP TABLE #tempDetailHachToanXuatNgu

		 DROP TABLE #tempThoiViec
		 DROP TABLE #tempDetailDuToanThoiViec
		 DROP TABLE #tempDetailHachToanThoiViec

		 DROP TABLE #tempTuTuat
		 DROP TABLE #tempDetailDuToanTuTuat
		 DROP TABLE #tempDetailHachToanTuTuat

		 DROP TABLE #tempKhoiDuToan
		 DROP TABLE #tempKhoiHachToan

		 DROP TABLE #tempHuuTriDuToan
		 DROP TABLE #tempPhucVienDuToan
		 DROP TABLE #tempXuatNguDuToan
		 DROP TABLE #tempThoiViecDuToan
		 DROP TABLE #tempTuTuatDuToan

		 DROP TABLE #tempSiQuan
		 DROP TABLE #tempQNCN
		 DROP TABLE #tempCNVCQP
		 DROP TABLE #tempHSQBS
		 DROP TABLE #tempLDHD

		 DROP TABLE #tempDetailSiQuanDuToanHuuTri
		 DROP TABLE #tempDetailQNCNDuToanHuuTri
		 DROP TABLE #tempDetailCNVCQPDuToanHuuTri
		 DROP TABLE #tempDetailHSQBSDuToanHuuTri
		 DROP TABLE #tempDetailLDHDDuToanHuuTri

		 DROP TABLE #tempDetailSiQuanDuToanPhucVien
		 DROP TABLE #tempDetailQNCNDuToanPhucVien
		 DROP TABLE #tempDetailCNVCQPDuToanPhucVien
		 DROP TABLE #tempDetailHSQBSDuToanPhucVien
		 DROP TABLE #tempDetailLDHDDuToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanDuToanXuatNgu
		 DROP TABLE #tempDetailQNCNDuToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPDuToanXuatNgu
		 DROP TABLE #tempDetailHSQBSDuToanXuatNgu
		 DROP TABLE #tempDetailLDHDDuToanXuatNgu

		 DROP TABLE #tempDetailSiQuanDuToanThoiViec
		 DROP TABLE #tempDetailQNCNDuToanThoiViec
		 DROP TABLE #tempDetailCNVCQPDuToanThoiViec
		 DROP TABLE #tempDetailHSQBSDuToanThoiViec
		 DROP TABLE #tempDetailLDHDDuToanThoiViec

		 DROP TABLE #tempDetailSiQuanDuToanTuTuat
		 DROP TABLE #tempDetailQNCNDuToanTuTuat
		 DROP TABLE #tempDetailCNVCQPDuToanTuTuat
		 DROP TABLE #tempDetailHSQBSDuToanTuTuat
		 DROP TABLE #tempDetailLDHDDuToanTuTuat

		 DROP TABLE #tempDetailSiQuanHachToanHuuTri
		 DROP TABLE #tempDetailQNCNHachToanHuuTri
		 DROP TABLE #tempDetailCNVCQPHachToanHuuTri
		 DROP TABLE #tempDetailHSQBSHachToanHuuTri
		 DROP TABLE #tempDetailLDHDHachToanHuuTri

		 DROP TABLE #tempDetailSiQuanHachToanPhucVien
		 DROP TABLE #tempDetailQNCNHachToanPhucVien
		 DROP TABLE #tempDetailCNVCQPHachToanPhucVien
		 DROP TABLE #tempDetailHSQBSHachToanPhucVien
		 DROP TABLE #tempDetailLDHDHachToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanHachToanXuatNgu
		 DROP TABLE #tempDetailQNCNHachToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPHachToanXuatNgu
		 DROP TABLE #tempDetailHSQBSHachToanXuatNgu
		 DROP TABLE #tempDetailLDHDHachToanXuatNgu

		 DROP TABLE #tempDetailSiQuanHachToanThoiViec
		 DROP TABLE #tempDetailQNCNHachToanThoiViec
		 DROP TABLE #tempDetailCNVCQPHachToanThoiViec
		 DROP TABLE #tempDetailHSQBSHachToanThoiViec
		 DROP TABLE #tempDetailLDHDHachToanThoiViec

		 DROP TABLE #tempDetailSiQuanHachToanTuTuat
		 DROP TABLE #tempDetailQNCNHachToanTuTuat
		 DROP TABLE #tempDetailCNVCQPHachToanTuTuat
		 DROP TABLE #tempDetailHSQBSHachToanTuTuat
		 DROP TABLE #tempDetailLDHDHachToanTuTuat


		 DROP TABLE #tempHuuTriHachToan
		 DROP TABLE #tempPhucVienHachToan
		 DROP TABLE #tempXuatNguHachToan
		 Drop table #tempThoiViecHachToan
		 Drop table #tempTuTuatHachToan

END
;
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]    Script Date: 9/25/2024 2:28:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
		SELECT gt.*,dv.sTenDonVi INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
		left join DonVi dv on dv.iID_MaDonVi=gt.iiD_MaDonVi
			WHERE (--- Du toan
					gt.sXauNoiMa like '9010001-010-011-0004%'
					or gt.sXauNoiMa like '9010001-010-011-0005%'
					or gt.sXauNoiMa like '9010001-010-011-0006%'
					or gt.sXauNoiMa like '9010001-010-011-0007%'
					or gt.sXauNoiMa like '9010001-010-011-0008%'
					--- Hoach toan
					or gt.sXauNoiMa like '9010002-010-011-0004%'
					or gt.sXauNoiMa like '9010002-010-011-0005%'
					or gt.sXauNoiMa like '9010002-010-011-0006%'
					or gt.sXauNoiMa like '9010002-010-011-0007%'
					or gt.sXauNoiMa like '9010002-010-011-0008%')
				and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
				and dv.iNamLamViec=@INamLamViec
				and dv.iTrangThai=1

			SELECT 1 bHangCha
			, N'Khối dự toán' STT
			, N'Khối dự toán'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, 1 IsParent
			, 0 Child
			into #tblDuToan

			SELECT 1 bHangCha
			, N'Khối hạch toán' STT
			, N'Khối hạch toán'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, 1 IsParent
			, 0 Child
			into #tblHachToan

	--- 9010001-010-011-0004 Tro cap Huu tri du toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailHuuTriSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	--- 9010001-010-011-0004 Tro cap Huu tri Hach toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailHuuTriHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,sum(FTienTroCap1Lan) FTienTroCap1Lan
			,sum(FTienTroCapKV) FTienTroCapKV
			,sum(FTienTroCapMT) FTienTroCapMT
			,sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #TempDetailHuuTri
			from  #TempDetailHuuTriSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailHuuTriHachToan
			from  #tempDetailHuuTriHachToanSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh



	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
			, N'TC Hưu Trí'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child

	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailHuuTri	
	UNION ALL
	SELECT * FROM #tblHachToan
	UNION ALL
	SELECT * FROM #tempDetailHuuTriHachToan	
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1
		--- update Khối dự toán
		UPDATE #tempHuuTri
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  Child=1 ) detail
		where #tempHuuTri.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempHuuTri
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  Child=2 ) detail
		where #tempHuuTri.STenCanBo=N'Khối hạch toán'

		--- update TC  Huu tri
		UPDATE #tempHuuTri
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  Child=2 or Child=1 ) detail
		where #tempHuuTri.STenCanBo=N'TC Hưu Trí'

	--- 9010001-010-011-0005 Tro cap phuc vien Du toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailPhucVienSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #tempDetailPhucVien
			from  #tempDetailPhucVienSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


--- 9010001-010-011-0005 Tro cap phuc vien hach toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailPhucVienHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE  tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailPhucVienHachToan
			from  #tempDetailPhucVienHachToanSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	UNION ALL 
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailPhucVienHachToan
	) tblPhucVien

	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1
	UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  Child=1 ) detail
		where #tempPhucVien.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  Child=2 ) detail
		where #tempPhucVien.STenCanBo=N'Khối hạch toán'

		--- update  Phục viên
		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  Child=2 or Child=1 ) detail
		where #tempPhucVien.STenCanBo=N'TC Phục viên'


	--- 9010001-010-011-0006 Tro cap xuat ngu du toan

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #tempDetailXuatNgu
			from  #tempDetailXuatNguSum tbltc
			GROUP BY tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh


	--- 9010001-010-011-0006 Tro cap xuat ngu hạch toán

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguHachToanSum
	FROM  #tblTroCap tbltc
	WHERE  tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailXuatNguHachToan
			from  #tempDetailXuatNguHachToanSum tbltc
			GROUP BY tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh

	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
			, N'TC Xuất ngũ'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac		
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL 
	SELECT * FROM #tempDetailXuatNgu
	UNION ALL 
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailXuatNguHachToan
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1
 
	UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  Child=1 ) detail
		where #tempXuatNgu.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  Child=2 ) detail
		where #tempXuatNgu.STenCanBo=N'Khối hạch toán'

		--- update  TC Xuất ngũ
		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  Child=2 or Child=1 ) detail
		where #tempXuatNgu.STenCanBo=N'TC Xuất ngũ'

	--- 9010001-010-011-0007 tro cap thoi viec du toan

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailThoiViecSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			into #tempDetailThoiViec
			from  #tempDetailThoiViecSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	--- 9010001-010-011-0007 tro cap thoi viec hach toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailThoiViecHachToanSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			into #tempDetailThoiViecHachToan
			from  #tempDetailThoiViecHachToanSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
			, N'TC Thôi việc'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	UNION ALL
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailThoiViecHachToan
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  Child=1 ) detail
		where #tempThoiViec.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  Child=2 ) detail
		where #tempThoiViec.STenCanBo=N'Khối hạch toán'

		--- update  TC Thôi việc
		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  Child=2 or Child=1 ) detail
		where #tempThoiViec.STenCanBo=N'TC Thôi việc'

	--- 9010001-010-011-0008 tro cap tu tuat du toan

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailTuTuatSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			into #tempDetailTuTuat
			from  #tempDetailTuTuatSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

--- 9010001-010-011-0008 tro cap tu tuat hach toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailTuTuatHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			into #tempDetailTuTuatHachToan
			from  #tempDetailTuTuatHachToanSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailTuTuat
	UNION ALL
	SELECT  * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailTuTuatHachToan
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  Child=1 ) detail
		where #tempTuTuat.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  Child=2 ) detail
		where #tempTuTuat.STenCanBo=N'Khối hạch toán'

		--- update  TC Tử tuất
		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  Child=2 or Child=1 ) detail
		where #tempTuTuat.STenCanBo=N'TC Tử tuất'
		-- ket qua
	SELECT * INTO #tempRESULT  
	FROM
	(
		SELECT * FROM #tempHuuTri
		UNION ALL 
		SELECT * FROM #tempPhucVien
		UNION ALL 
		SELECT * FROM #tempXuatNgu
		UNION ALL
		SELECT * FROM #tempThoiViec
		UNION ALL
		SELECT * FROM #tempTuTuat
	) TBLRESULT

	SELECT * FROM #tempRESULT

	 DROP TABLE #tempHuuTri
	 DROP TABLE #tempPhucVien
	 DROP TABLE #tempXuatNgu
	 DROP TABLE #tempThoiViec
	 DROP TABLE #tempTuTuat
	 DROP TABLE #tempRESULT
	 DROP TABLE #TempDetailHuuTri
	 DROP TABLE #tempDetailPhucVien
	 DROP TABLE #TempDetailTuTuat
	 DROP TABLE #tempDetailThoiViec
	 DROP TABLE #tempDetailXuatNgu
	 DROP TABLE #tempDetailHuuTriSum
	 DROP TABLE #tempDetailPhucVienSum
	 DROP TABLE #TempDetailTuTuatSum
	 DROP TABLE #tempDetailThoiViecSum
	 DROP TABLE #tempDetailXuatNguSum

	 DROP TABLE #tempDetailHuuTriHachToan
	 DROP TABLE #tempDetailPhucVienHachToan
	 DROP TABLE #TempDetailTuTuatHachToan
	 DROP TABLE #tempDetailThoiViecHachToan
	 DROP TABLE #tempDetailXuatNguHachToan
	 DROP TABLE #tempDetailHuuTriHachToanSum
	 DROP TABLE #tempDetailPhucVienHachToanSum
	 DROP TABLE #TempDetailTuTuatHachToanSum
	 DROP TABLE #tempDetailThoiViecHachToanSum
	 DROP TABLE #tempDetailXuatNguHachToanSum

END
;
;
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]    Script Date: 9/25/2024 2:28:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
		SELECT gt.*,dv.sTenDonVi INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
		left join DonVi dv on dv.iID_MaDonVi=gt.iiD_MaDonVi
			WHERE (--- Du toan
					gt.sXauNoiMa like '9010001-010-011-0004%'
					or gt.sXauNoiMa like '9010001-010-011-0005%'
					or gt.sXauNoiMa like '9010001-010-011-0006%'
					or gt.sXauNoiMa like '9010001-010-011-0007%'
					or gt.sXauNoiMa like '9010001-010-011-0008%'
					--- Hoach toan
					or gt.sXauNoiMa like '9010002-010-011-0004%'
					or gt.sXauNoiMa like '9010002-010-011-0005%'
					or gt.sXauNoiMa like '9010002-010-011-0006%'
					or gt.sXauNoiMa like '9010002-010-011-0007%'
					or gt.sXauNoiMa like '9010002-010-011-0008%')
				and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
				and dv.iNamLamViec=@INamLamViec
				and dv.iTrangThai=1

			SELECT 1 bHangCha
			, N'Khối dự toán' STT
			, N'Khối dự toán'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, 1 IsParent
			, 0 Child
			into #tblDuToan

			SELECT 1 bHangCha
			, N'Khối hạch toán' STT
			, N'Khối hạch toán'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, 1 IsParent
			, 0 Child
			into #tblHachToan

	--- 9010001-010-011-0004 Tro cap Huu tri du toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailHuuTriSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	--- 9010001-010-011-0004 Tro cap Huu tri Hach toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailHuuTriHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,sum(FTienTroCap1Lan) FTienTroCap1Lan
			,sum(FTienTroCapKV) FTienTroCapKV
			,sum(FTienTroCapMT) FTienTroCapMT
			,sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #TempDetailHuuTri
			from  #TempDetailHuuTriSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailHuuTriHachToan
			from  #tempDetailHuuTriHachToanSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh



	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
			, N'TC Hưu Trí'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child

	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailHuuTri	
	UNION ALL
	SELECT * FROM #tblHachToan
	UNION ALL
	SELECT * FROM #tempDetailHuuTriHachToan	
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1
		--- update Khối dự toán
		UPDATE #tempHuuTri
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  Child=1 ) detail
		where #tempHuuTri.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempHuuTri
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  Child=2 ) detail
		where #tempHuuTri.STenCanBo=N'Khối hạch toán'

		--- update TC  Huu tri
		UPDATE #tempHuuTri
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  Child=2 or Child=1 ) detail
		where #tempHuuTri.STenCanBo=N'TC Hưu Trí'

	--- 9010001-010-011-0005 Tro cap phuc vien Du toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailPhucVienSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #tempDetailPhucVien
			from  #tempDetailPhucVienSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


--- 9010001-010-011-0005 Tro cap phuc vien hach toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailPhucVienHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE  tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailPhucVienHachToan
			from  #tempDetailPhucVienHachToanSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	UNION ALL 
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailPhucVienHachToan
	) tblPhucVien

	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1
	UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  Child=1 ) detail
		where #tempPhucVien.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  Child=2 ) detail
		where #tempPhucVien.STenCanBo=N'Khối hạch toán'

		--- update  Phục viên
		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  Child=2 or Child=1 ) detail
		where #tempPhucVien.STenCanBo=N'TC Phục viên'


	--- 9010001-010-011-0006 Tro cap xuat ngu du toan

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #tempDetailXuatNgu
			from  #tempDetailXuatNguSum tbltc
			GROUP BY 
					 tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenPhanHo 
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh


	--- 9010001-010-011-0006 Tro cap xuat ngu hạch toán

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguHachToanSum
	FROM  #tblTroCap tbltc
	WHERE  tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailXuatNguHachToan
			from  #tempDetailXuatNguHachToanSum tbltc
			GROUP BY 
					 tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenPhanHo 
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh

	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
			, N'TC Xuất ngũ'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac		
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL 
	SELECT * FROM #tempDetailXuatNgu
	UNION ALL 
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailXuatNguHachToan
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1
 
	UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  Child=1 ) detail
		where #tempXuatNgu.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  Child=2 ) detail
		where #tempXuatNgu.STenCanBo=N'Khối hạch toán'

		--- update  TC Xuất ngũ
		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  Child=2 or Child=1 ) detail
		where #tempXuatNgu.STenCanBo=N'TC Xuất ngũ'

	--- 9010001-010-011-0007 tro cap thoi viec du toan

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailThoiViecSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			into #tempDetailThoiViec
			from  #tempDetailThoiViecSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	--- 9010001-010-011-0007 tro cap thoi viec hach toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailThoiViecHachToanSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			into #tempDetailThoiViecHachToan
			from  #tempDetailThoiViecHachToanSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
			, N'TC Thôi việc'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	UNION ALL
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailThoiViecHachToan
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  Child=1 ) detail
		where #tempThoiViec.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  Child=2 ) detail
		where #tempThoiViec.STenCanBo=N'Khối hạch toán'

		--- update  TC Thôi việc
		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  Child=2 or Child=1 ) detail
		where #tempThoiViec.STenCanBo=N'TC Thôi việc'

	--- 9010001-010-011-0008 tro cap tu tuat du toan

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailTuTuatSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			into #tempDetailTuTuat
			from  #tempDetailTuTuatSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

--- 9010001-010-011-0008 tro cap tu tuat hach toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailTuTuatHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			into #tempDetailTuTuatHachToan
			from  #tempDetailTuTuatHachToanSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, '' as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailTuTuat
	UNION ALL
	SELECT  * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailTuTuatHachToan
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  Child=1 ) detail
		where #tempTuTuat.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  Child=2 ) detail
		where #tempTuTuat.STenCanBo=N'Khối hạch toán'

		--- update  TC Tử tuất
		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  Child=2 or Child=1 ) detail
		where #tempTuTuat.STenCanBo=N'TC Tử tuất'
		-- ket qua
	SELECT * INTO #tempRESULT  
	FROM
	(
		SELECT * FROM #tempHuuTri
		UNION ALL 
		SELECT * FROM #tempPhucVien
		UNION ALL 
		SELECT * FROM #tempXuatNgu
		UNION ALL
		SELECT * FROM #tempThoiViec
		UNION ALL
		SELECT * FROM #tempTuTuat
	) TBLRESULT

	SELECT * FROM #tempRESULT

	 DROP TABLE #tempHuuTri
	 DROP TABLE #tempPhucVien
	 DROP TABLE #tempXuatNgu
	 DROP TABLE #tempThoiViec
	 DROP TABLE #tempTuTuat
	 DROP TABLE #tempRESULT
	 DROP TABLE #TempDetailHuuTri
	 DROP TABLE #tempDetailPhucVien
	 DROP TABLE #TempDetailTuTuat
	 DROP TABLE #tempDetailThoiViec
	 DROP TABLE #tempDetailXuatNgu
	 DROP TABLE #tempDetailHuuTriSum
	 DROP TABLE #tempDetailPhucVienSum
	 DROP TABLE #TempDetailTuTuatSum
	 DROP TABLE #tempDetailThoiViecSum
	 DROP TABLE #tempDetailXuatNguSum

	 DROP TABLE #tempDetailHuuTriHachToan
	 DROP TABLE #tempDetailPhucVienHachToan
	 DROP TABLE #TempDetailTuTuatHachToan
	 DROP TABLE #tempDetailThoiViecHachToan
	 DROP TABLE #tempDetailXuatNguHachToan
	 DROP TABLE #tempDetailHuuTriHachToanSum
	 DROP TABLE #tempDetailPhucVienHachToanSum
	 DROP TABLE #TempDetailTuTuatHachToanSum
	 DROP TABLE #tempDetailThoiViecHachToanSum
	 DROP TABLE #tempDetailXuatNguHachToanSum

END
;
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 9/25/2024 2:28:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
SELECT gt.*,dv.sTenDonVi INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
left join DonVi dv on dv.iID_MaDonVi=gt.iiD_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

	--- 9010001-010-011-0004 Tro cap Huu tri
	--- 9010001-010-011-0004 Tro cap Huu tri

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
						, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #TempDetailHuuTriSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo 
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #TempDetailHuuTri
			from  #TempDetailHuuTriSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			--ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
			, N'TC Hưu Trí'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #TempDetailHuuTri
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

	
		UPDATE  A
		SET A.FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		A.FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		A.FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		A.FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		A.FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		A.FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri A ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  bHangCha=0 ) detail
		where A.bHangCha=1


		
	--- 9010001-010-011-0005 Tro cap phuc vien
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.sMaCapBac
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL

			INTO #tempDetailPhucVienSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #tempDetailPhucVien
			from  #tempDetailPhucVienSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	) tblPhucVien

	
	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1
		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  bHangCha=0 ) detail
		where #tempPhucVien.bHangCha=1
	--- 9010001-010-011-0006 Tro cap xuat ngu

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			--, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac		
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			--, 0 AS  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			 INTO #tempDetailXuatNguSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY 
			 tbltc.sTenDonVi 
			 , tbltc.sMa_Hieu_Can_Bo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa


			SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			,  CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #tempDetailXuatNgu
			from  #tempDetailXuatNguSum tbltc
			GROUP BY 
					 tbltc.sTenPhanHo 
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh


--ORDER BY  tbltc.sTenCanBo desc,tbltc.sMaCapBac desc
	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
			, N'TC Xuất ngũ'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac		
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailXuatNgu
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1
		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  bHangCha=0 ) detail
		where #tempXuatNgu.bHangCha=1
	--- 9010001-010-011-0007 tro cap thoi viec

	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailThoiViecSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			into #tempDetailThoiViec
			from  #tempDetailThoiViecSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
			, N'TC Thôi việc'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  bHangCha=0 ) detail
		where #tempThoiViec.bHangCha=1
	--- 9010001-010-011-0008 tro cap tu tuat
	--- 9010001-010-011-0008-0001-0001-00
	--- 9010002-010-011-0008-0001-0003-00
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)   FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			into #TempDetailTuTuatSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa 
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			into #TempDetailTuTuat
			from  #TempDetailTuTuatSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh

				ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #TempDetailTuTuat
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  bHangCha=0 ) detail
				where #tempTuTuat.bHangCha=1
		-- ket qua
	SELECT * INTO #tempRESULT  
	FROM
	(
		SELECT * FROM #tempHuuTri
		UNION ALL 
		SELECT * FROM #tempPhucVien
		UNION ALL 
		SELECT * FROM #tempXuatNgu
		UNION ALL
		SELECT * FROM #tempThoiViec
		UNION ALL
		SELECT * FROM #tempTuTuat
	) TBLRESULT

	SELECT * FROM #tempRESULT

	 DROP TABLE #tempHuuTri
	 DROP TABLE #tempPhucVien
	 DROP TABLE #tempXuatNgu
	 DROP TABLE #tempThoiViec
	 DROP TABLE #tempTuTuat
	 DROP TABLE #tempRESULT
	 DROP TABLE #TempDetailHuuTri
	 DROP TABLE #tempDetailPhucVien
	 DROP TABLE #TempDetailTuTuat
	 DROP TABLE #tempDetailThoiViec
	 DROP TABLE #tempDetailXuatNgu

END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 9/30/2024 2:26:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 9/30/2024 2:26:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;


	SELECT 
		qtcn_ct.iID_MucLucNganSach,
		ml.sMoTa sNoiDung,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay) fTienThucChi,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay) fTienXacNhanQuyetToanQuyNay,
		qtcn_ct.sXauNoiMa,
		qtcn_ct.iNamLamViec,
		qtcn_ct.iIDMaDonVi,
		ml.sM,
		ml.sTM
		into #temp1
	FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KinhPhiQuanLy  as qtcn on qtcn_ct.iID_QTC_Quy_KinhPhiQuanLy = qtcn.ID_QTC_Quy_KinhPhiQuanLy
		left join BH_DM_MucLucNganSach ml on qtcn_ct.sXauNoiMa=ml.sXauNoiMa
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			and ml.iTrangThai=1
			and ml.iNamLamViec=@NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, ml.sMoTa,qtcn_ct.sXauNoiMa,
		qtcn_ct.iNamLamViec,
		qtcn_ct.iIDMaDonVi,ml.sM,
		ml.sTM

	INSERT INTO BH_QTC_Quy_KinhPhiQuanLy_ChiTiet
	( 
		ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		iID_QTC_Quy_KinhPhiQuanLy,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienThucChi,
		fTienQuyetToanDaDuyet,
		sXauNoiMa,
		iNamLamViec,
		iIDMaDonVi,
		sM,
		sTM
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		iID_MucLucNganSach,
		sNoiDung,
		GETDATE(),
		@Username,
		fTienThucChi,
		fTienXacNhanQuyetToanQuyNay,
		sXauNoiMa,
		iNamLamViec,
		iIDMaDonVi,
		sM,
		sTM

	FROM #temp1 
END
;
;
;
;
;
GO
