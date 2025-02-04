/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]    Script Date: 6/3/2024 4:52:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]    Script Date: 6/3/2024 4:52:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]    Script Date: 6/3/2024 4:52:40 PM ******/
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
@IsCheckedMaHuongLuong AS bit = 0,
@TyLeHuong AS float
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
And (canbo.bNuocNgoai= (CASE WHEN '+ CAST(@TyLeHuong AS VARCHAR(4))  + ' = 0 then 0
										ELSE 1
									END ))
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
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]    Script Date: 6/3/2024 4:52:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0,
@TyLeHuong AS float
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

SET @Cols = 'LUONGCOBAN_SUM,LCBHT_TT,NLCBHT_TT,LBLCBHT_TT,LCVCDHT_TT,NLCVCDHT_TT,LBLCVCDHT_TT,PCCLKHAC_SUM,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM,CACLOAIPC_SUM,NCVCDHT_TT'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo_NQ104 canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And (canbo.bNuocNgoai= (CASE WHEN '+ CAST(@TyLeHuong AS VARCHAR(4))  + ' = 0 then 0
										ELSE 1
									END ))
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND (canbo.ty_le_huong_nn = ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' or ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' = 0)
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT gia_tri, ma_phu_cap ma_phucap, ma_can_bo ma_cbo, ma_hieu_can_bo Ma_Hieu_CanBo, parent FROM TL_BangLuong_Thang_Bridge_NQ104
	--WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	--AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	--AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	WHERE ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (ma_hieu_can_bo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_Don_Vi)))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
--bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
 dsCapNhapBangLuong.Ma_CachTL=''CACH0''
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
), 
ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
canBo.lan_nang_luong_cb LanNangLuongCb,
canBo.lan_nang_luong_cvd LanNangLuongCvd,
loaiNhom.ten_loai TenLoai,
loaiNhom.ten_nhom TenNhom,
capBacLuong.ten_dm CapBacLuong,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
--ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma AS MaChucVu,
chucVu.Ten AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, ''  '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.xau_noi_ma XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104=capBac.Ma_Cb AND capBac.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
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
AND (canbo.ty_le_huong_nn = ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' or ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' = 0)
AND canBo.Khong_Luong = 0
And (canbo.bNuocNgoai= (CASE WHEN '+ CAST(@TyLeHuong AS VARCHAR(4))  + ' = 0 then 0
										ELSE 1
									END ))
--
--
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
--canBo.HSChucVu,
canBo.TenNhom,
canBo.TenLoai,
canBo.CapBacLuong,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.LanNangLuongCb,
canBo.LanNangLuongCvd,
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
-- select @Query
END
;
;
;
GO
