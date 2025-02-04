/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 7/6/2023 2:05:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_capphat_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 7/6/2023 2:05:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 7/6/2023 2:05:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
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

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH blt AS (
	SELECT * FROM TL_BangLuong_Thang
	WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
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
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
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
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 7/6/2023 2:05:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'NTN,LHT_HS,LHT_TT,PCCV_TT,PCTHD_TT,PCKV_TT,PCKVCS_TT,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,PCTRA_SUM,LUONGTHANG_SUM,PHAITRU_SUM,TM,THANHTIEN,PCNU_TT,HSBL_HS,PCTNVK_HS'
	SET @Query =
	'
	WITH blt AS (
		SELECT * FROM TL_BangLuong_Thang
		WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
		AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
		AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	), 
	BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CBo			AS MaCanBo,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM blt bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonVi,
			donVi.Ten_Donvi		AS TenDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			canBo.Thang_TNN		AS Tnn,
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			chucVu.Ma_Cv AS MaChucVu,
			chucVu.Ten_Cv AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			canBo.Ngay_NN AS NgayNhapNguDate,
			canBo.Ngay_XN AS NgayXuatNguDate,
			canBo.Ngay_TN AS NgayTaiNguDate,
			CASE WHEN canBo.Thang_TNN IS NULL THEN 0 ELSE canBo.Thang_TNN END AS ThangTnn,
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
	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, Tnn, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
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
			canBo.NgayNhapNguDate,
			canBo.NgayXuatNguDate,
			canBo.NgayTaiNguDate,
			canBo.ThangTnn,
			canBo.Tnn,
			CASE WHEN bangLuong.MaPhuCap = ' + '''NTN''' + ' THEN dbo.f_luong_ntn(canBo.NgayNhapNguDate, canBo.NgayXuatNguDate, canBo.NgayTaiNguDate, canBo.ThangTnn, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') ELSE bangLuong.GiaTri END AS GiaTri,
			bangLuong.MaPhuCap,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt
	WHERE MaCapBac LIKE ''0%''
	ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC'
	execute(@Query)
END
;
GO
