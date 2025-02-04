/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]    Script Date: 5/24/2024 5:22:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]    Script Date: 5/24/2024 5:22:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]    Script Date: 5/24/2024 5:22:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]
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

SET @Cols = 'NLCBHT_TT,NLCVCDHT_TT,LCVCDHT_TT,LCBHT_TT,LBLCBHT_TT,LBLCVCDHT_TT,PCCLKHAC_SUM,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM,LUONGCOBAN_SUM,CACLOAIPC_SUM'
SET @Query =
'
WITH BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_Can_Bo AS MaCanBo,
bangLuong.MA_PHU_CAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
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
loaiNhom.ten_loai TenLoai,
loaiNhom.ten_nhom TenNhom,
capBacLuong.ten_dm CapBacLuong,
canBo.lan_nang_luong_cb LanNangLuongCb,
canBo.lan_nang_luong_cvd LanNangLuongCvd,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
chucVu.Ma AS MaChucVu,
chucVu.Ten AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.Xau_Noi_Ma XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104 = capBac.Ma_Cb
LEFT JOIN (SELECT
	con.ten_dm ten_loai,
	con.ma_dm loai,
	chau.ten_dm ten_nhom,
	chau.ma_dm nhom,
	cha.loai_doi_tuong loai_doi_tuong,
	chau.xau_noi_ma
FROM TL_DM_CapBac_Luong_NQ104 cha
LEFT JOIN TL_DM_CapBac_Luong_NQ104 con
ON cha.ma_dm = con.ma_dm_cha 
AND cha.loai_doi_tuong = con.loai_doi_tuong 
AND con.xau_noi_ma like cha.xau_noi_ma + ''-%''
AND con.nam = cha.nam
LEFT JOIN TL_DM_CapBac_Luong_NQ104 chau
ON con.ma_dm = chau.ma_dm_cha 
AND con.loai_doi_tuong = chau.loai_doi_tuong 
AND chau.xau_noi_ma like con.xau_noi_ma + ''-%''
AND chau.nam = con.nam
WHERE cha.loai = 0 AND con.loai = 1 AND chau.loai = 2) loaiNhom
ON loaiNhom.loai = canBo.loai AND loaiNhom.nhom = canBo.nhom_chuyen_mon 
AND canBo.loai_doi_tuong in (select * from f_split(loaiNhom.loai_doi_tuong))

LEFT JOIN (SELECT * FROM TL_DM_CapBac_Luong_NQ104 WHERE loai = 3 and nam = ' + CAST(@Nam AS VARCHAR(4)) + ') capBacLuong
ON (
	(
	canBo.loai_doi_tuong IN (''1'',''3.2'',''4'',''5'') 
	AND canBo.loai_doi_tuong IN 
	(SELECT * FROM f_split(capBacLuong.loai_doi_tuong)) AND capBacLuong.loai_doi_tuong = loaiNhom.loai_doi_tuong
	)
	OR 
	(
	canBo.loai_doi_tuong NOT IN (''1'',''3.2'',''4'',''5'') 
	AND (capBacLuong.ma_dm_cha = loaiNhom.nhom) AND capBacLuong.loai_doi_tuong = loaiNhom.loai_doi_tuong AND capBacLuong.xau_noi_ma like loaiNhom.xau_noi_ma + ''-%''
	)
)
AND capBacLuong.ma_dm = canBo.ma_bac_luong
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_Cvd104 = chucVu.Ma AND chucVu.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
WHERE
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.LanNangLuongCb,
canBo.LanNangLuongCvd,
canBo.TenNhom,
canBo.TenLoai,
canBo.CapBacLuong,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]    Script Date: 5/24/2024 5:22:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]
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

SET @Cols = 'TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,PCCLKHAC_SUM,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM,CACLOAIPC_SUM'
SET @Query =
'
WITH 
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
bangLuong.Ma_Can_Bo AS MaCanBo,
bangLuong.MA_PHU_CAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_Phu_Cap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_Can_Bo = bhxh.sMaCbo AND bangLuong.Ma_Don_Vi = bhxh.sMaDonVi
WHERE
bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
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
canBo.lan_nang_luong_cb LanNangLuongCb,
canBo.lan_nang_luong_cvd LanNangLuongCvd,
loaiNhom.ten_loai TenLoai,
loaiNhom.ten_nhom TenNhom,
capBacLuong.ten_dm CapBacLuong,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
chucVu.Ma AS MaChucVu,
chucVu.Ten AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.xau_noi_ma XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104=capBac.Ma_Cb
LEFT JOIN (SELECT
	cha.ten_dm ten_loai,
	cha.loai loai,
	con.ten_dm ten_nhom,
	con.nhom nhom
FROM TL_DM_CapBac_Luong_NQ104 cha
LEFT JOIN TL_DM_CapBac_Luong_NQ104 con
ON cha.loai = con.loai
where cha.loai != ''00'' AND con.loai != ''00'' 
AND cha.nhom = ''00'' AND con.nhom <> ''00''
AND ISNULL(cha.ma_dm, '''') = '''' AND ISNULL(con.ma_dm, '''') = '''') loaiNhom
ON loaiNhom.loai = canBo.loai AND loaiNhom.nhom = canBo.nhom_chuyen_mon
LEFT JOIN TL_DM_CapBac_Luong_NQ104 capBacLuong
ON (
	(
	canBo.loai_doi_tuong IN (''1'',''3.2'',''4'',''5'') 
	AND canBo.loai_doi_tuong IN 
	(SELECT * FROM f_split(capBacLuong.loai_doi_tuong))
	)
	OR 
	(
	canBo.loai_doi_tuong NOT IN (''1'',''3.2'',''4'',''5'') 
	AND (capBacLuong.loai = canBo.loai AND capBacLuong.nhom = canBo.nhom_chuyen_mon)
	)
)
AND capBacLuong.ma_dm = canBo.ma_bac_luong
AND capBacLuong.ma_dm is not null
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_CVd104=chucVu.Ma
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
canBo.MaChucVu,
canBo.TenChucVu,
canBo.LanNangLuongCb,
canBo.LanNangLuongCvd,
canBo.TenNhom,
canBo.TenLoai,
canBo.CapBacLuong,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
;
GO



delete [HT_Quyen_ChucNang] where [iID_MaChucNang] = 'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX';
delete [HT_ChucNang] where [iID_MaChucNang] = 'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX';

GO
INSERT [dbo].[HT_ChucNang] ([iID_MaChucNang], [BHangCha], [iID_ChucNangCha], [ITrangThai], [iID_ChucNang], [sSTT], [sTenChucNang]) VALUES (N'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX', 1, N'b9ccf0f5-f42b-453a-b14a-f66211a9c2c8', 1, N'8095b6cc-a8f3-4d5b-bcc5-05c93bee3716', N'06-02-04-00-00', N'Bảng lương tháng truy thu')
GO
INSERT [dbo].[HT_Quyen_ChucNang] ([iID_MaChucNang], [iID_MaQuyen]) VALUES (N'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX', N'ADMIN')



