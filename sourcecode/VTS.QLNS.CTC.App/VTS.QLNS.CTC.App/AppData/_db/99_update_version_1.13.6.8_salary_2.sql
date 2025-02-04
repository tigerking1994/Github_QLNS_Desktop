/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_thu_nop_bhxh_TongHop_theoDonvi]    Script Date: 12/14/2023 5:03:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_thu_nop_bhxh_TongHop_theoDonvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_thu_nop_bhxh_TongHop_theoDonvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_thunop_bhxh]    Script Date: 12/14/2023 5:03:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_thunop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_thunop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]    Script Date: 12/14/2023 5:03:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quanly_thunop_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_thu_nop_bhxh]    Script Date: 12/14/2023 5:03:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_thu_nop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_thu_nop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_thu_nop_bhxh]    Script Date: 12/14/2023 5:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[sp_tl_get_donvi_thu_nop_bhxh] @thang int, @nam int, @cachTinhLuong varchar(20)
AS
BEGIN

--select Parent into #tmpCB from TL_DM_CanBo where left (Ma_CB, 1) <> 0 and Thang = @thang and Nam = @nam group by Parent
select Parent into #tmpCB from TL_DM_CanBo where Thang = @thang and Nam = @nam group by Parent

select donvi.* from #tmpCB canbo
left join TL_QuanLyThuNop_BHXH thunop  on canbo.Parent = thunop.iID_MaDonVi 
left join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
where thunop.iThang = @thang and thunop.iNam = @nam and thunop.sMa_CachTL = @cachTinhLuong

drop table #tmpCB

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]    Script Date: 12/14/2023 5:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]
  	@Id UNIQUEIDENTIFIER = 'CB2C5E4E-D6FC-4D8B-B188-E481B42381A0'
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);;

SET @cols = STUFF(
  (
    SELECT
      DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap phucap
	where phucap.Ma_PhuCap in ('BHXHCN_TT', 'BHXHDV_TT', 'BHTNCN_TT', 'BHTNDV_TT','BHYTCN_TT', 'BHYTDV_TT' , 'BHCN_TT' , 'BHYTCNCS_TT','BHYTDVCS_TT', 'BHCN', 'LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')

        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = '
	WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo	AS MaCanBo,
			canBo.Ten_CanBo	AS TenCanBo,
			donVi.Ma_DonVi	AS MaDonVi,
			donVi.Ten_DonVi	AS TenDonVi
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
	)
	SELECT Thang, Nam, MaCanBo, TenCanBo, TenDonVi, ' + @cols + ' FROM (
		SELECT
			bangLuongThang.iThang		AS Thang,
			bangLuongThang.iNam			AS Nam,
			canBo.MaCanBo				AS MaCanBo,
			canBo.TenCanBo				AS TenCanBo,
			canBo.TenDonVi				AS TenDonVi,
			bangLuongThang.Gia_Tri		AS GiaTri,
			bangLuongThang.sMa_PhuCap	AS MaPhuCap
		FROM TL_QuanLyThuNop_BHXH_ChiTiet bangLuongThang
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.sMa_CBo = canBo.MaCanBo
		WHERE
			bangLuongThang.iId_ParentId = + ''' + CONVERT(NVARCHAR(100), @Id) + '''
	) x
	PIVOT 
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @cols + ')
	) p '
SET @Query = @Query +' ORDER BY TenDonVi ASC, TenCanBo ASC';
execute(@query)
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_thunop_bhxh]    Script Date: 12/14/2023 5:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_thunop_bhxh]
@MaDonVi NVARCHAR(max) = '000',
@Thang int = 11,
@Nam AS int = 2023,
@IsOrderChucVu AS bit = 1,
@IsTongHop AS bit = 0,
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

SET @Cols = 'BHXHCN_TT,BHXHDV_TT,BHTNCN_TT,BHTNDV_TT,BHYTCN_TT,BHYTDV_TT,BHCN_TT,BHYTCNCS_TT,BHYTDVCS_TT,BHCN,LHT_TT,PCCV_TT,PCTN_TT,PCTNVK_TT,HSBL_TT';
IF(@IsTongHop = 0)
BEGIN
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
	SELECT * FROM TL_QuanLyThuNop_BHXH_ChiTiet
	WHERE iThang = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND iID_MaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (sMa_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = iID_MaDonVi)))
),
ThuNopBHXH AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
thunopbhxh.iThang AS Thang,
thunopbhxh.iNam AS Nam,
chitiet.sMa_CBo AS MaCanBo,
chitiet.sMA_PHUCAP AS MaPhuCap,
chitiet.GIA_TRI AS GiaTri
FROM blt chitiet
JOIN TL_QuanLyThuNop_BHXH thunopbhxh
ON chitiet.iId_ParentId = thunopbhxh.Id
WHERE
chitiet.sMa_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND thunopbhxh.sMa_CachTL=''CACH0''
AND thunopbhxh.iID_MaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND thunopbhxh.iThang=' + CAST(@Thang AS VARCHAR(2)) + '
AND thunopbhxh.iNam=' + CAST(@Nam AS VARCHAR(4)) + '
AND thunopbhxh.Status=1
AND thunopbhxh.sTongHop IS  NULL
AND thunopbhxh.IsTongHop = 0
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
chitiet.*,
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
FROM ThuNopBHXH chitiet
INNER JOIN ThongTinCanBo canBo
ON chitiet.MaCanBo = canBo.MaCanBo'
END
ELSE
BEGIN
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
	SELECT * FROM TL_QuanLyThuNop_BHXH_ChiTiet
	WHERE iThang = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND iID_MaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (sMa_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = iID_MaDonVi)))
),
ThuNopBHXH AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
thunopbhxh.iThang AS Thang,
thunopbhxh.iNam AS Nam,
chitiet.sMa_CBo AS MaCanBo,
chitiet.sMA_PHUCAP AS MaPhuCap,
chitiet.GIA_TRI AS GiaTri
FROM blt chitiet
JOIN TL_QuanLyThuNop_BHXH thunopbhxh
ON chitiet.iId_ParentId = thunopbhxh.Id
WHERE
chitiet.sMa_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND thunopbhxh.sMa_CachTL=''CACH0''
AND thunopbhxh.iID_MaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND thunopbhxh.iThang=' + CAST(@Thang AS VARCHAR(2)) + '
AND thunopbhxh.iNam=' + CAST(@Nam AS VARCHAR(4)) + '
AND thunopbhxh.Status=1
AND thunopbhxh.sTongHop IS NOT NULL
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
chitiet.*,
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
FROM ThuNopBHXH chitiet
INNER JOIN ThongTinCanBo canBo
ON chitiet.MaCanBo = canBo.MaCanBo'

END
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
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_thu_nop_bhxh_TongHop_theoDonvi]    Script Date: 12/14/2023 5:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_thu_nop_bhxh_TongHop_theoDonvi]
	@MaDonVi NVARCHAR(max)= '17000001',
	@Thang int = 11,
	@Nam AS int = 2023,
	@IsOrderChucVu AS bit = 0,
	@IsGiaTriAm AS bit = 0,
	@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'BHXHCN_TT,BHXHDV_TT,BHTNCN_TT,BHTNDV_TT,BHYTCN_TT,BHYTDV_TT,BHCN_TT,BHYTCNCS_TT,BHYTDVCS_TT,BHCN,LHT_TT,PCCV_TT,PCTN_TT,PCTNVK_TT,HSBL_TT';
SET @Query =
'
	WITH BangChiTiet AS (
	SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
	SELECT
	thunopbhxh.iThang AS Thang,
	thunopbhxh.iNam AS Nam,
	chitiet.sMa_CBo AS MaCanBo,
	chitiet.sMa_PhuCap AS MaPhuCap,
	chitiet.GIA_TRI AS GiaTri
	FROM TL_QuanLyThuNop_BHXH_ChiTiet chitiet
	JOIN TL_QuanLyThuNop_BHXH thunopbhxh
	ON chitiet.iId_ParentId = thunopbhxh.Id
	WHERE
	chitiet.sMa_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
	AND thunopbhxh.sMa_CachTL=''CACH0''
	AND thunopbhxh.iID_MaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND thunopbhxh.iThang=' + CAST(@Thang AS VARCHAR(2)) + '
	AND thunopbhxh.iNam=' + CAST(@Nam AS VARCHAR(4)) + '
	AND thunopbhxh.Status=1
	AND thunopbhxh.sTongHop IS NOT NULL
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
	canBo.IsDelete = 1
	AND canBo.Khong_Luong = 0
	)
	
	SELECT
	chitiet.*,
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
	FROM BangChiTiet chitiet
	INNER JOIN ThongTinCanBo canBo
	ON chitiet.MaCanBo = canBo.MaCanBo'
--If @IsGiaTriAm = 1
--SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
GO
