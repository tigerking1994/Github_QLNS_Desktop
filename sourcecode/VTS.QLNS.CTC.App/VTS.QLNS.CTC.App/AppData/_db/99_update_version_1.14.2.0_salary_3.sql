/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]    Script Date: 3/26/2024 10:27:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]    Script Date: 3/26/2024 10:27:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]    Script Date: 3/26/2024 10:27:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]
GO
/****** Object:  StoredProcedure [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]    Script Date: 3/26/2024 10:27:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]    Script Date: 3/26/2024 10:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]
	-- Add the parameters for the stored procedure here
	@thang int,
	@nam int,
	@maDonVi varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		canBo.Parent MaDonVi,
		canBo.Ma_CanBo MaCanBo,
		canBo.Ten_CanBo TenCanBo,
		--ISNULL(chucVu.HeSo_Cv, 0) HSChucVu,
		capBac.ma_cb MaCapBac,
		canBo.So_TaiKhoan SoTaiKhoan,
		canBo.Ten_KhoBac NganHang,
		CEILING(ISNULL(bangLuong.Gia_Tri, 0)) THANHTIEN
	FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
	INNER JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhatBangLuong
		ON bangLuong.parent = dsCapNhatBangLuong.Id
	INNER JOIN TL_DM_CanBo canBo
		ON canBo.Ma_CanBo = bangLuong.ma_can_bo
	LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
		ON canBo.Ma_CVd104 = chucVu.ma
	LEFT JOIN TL_DM_CapBac_NQ104 capBac
		ON canBo.Ma_CB104 = capBac.Ma_Cb
	WHERE
		bangLuong.ma_phu_cap = 'THANHTIEN'
		AND canBo.TM = 1
		AND ISNULL(bangLuong.Gia_Tri, 0) > 0
		AND dsCapNhatBangLuong.Thang = @thang
		AND dsCapNhatBangLuong.Nam = @nam
		AND canBo.Parent in (SELECT * FROM dbo.splitstring(@maDonVi))
	ORDER BY MaDonVi DESC, MaCapBac DESC, TenCanBo DESC
END

/****** Object:  StoredProcedure [dbo].[sp_khluachonnhathau_get_nguonvon_by_lcnt_update]    Script Date: 15/12/2021 6:36:38 PM ******/
SET ANSI_NULLS ON
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]    Script Date: 3/26/2024 10:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]
@maDonVi NVARCHAR(max),
@thang int,
@nam int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'LHT_HS,PCCV_HS,LHT_TT,PCCV_TT,PCTNVK_TT,NTN,PCTNVK_HS,PCTRA_SUM,PCDACTHU_SUM,PCDACBIET_TT,PCANQP_TT,PCKV_TT,PC8_TT,PCCOV_TT,PCTHUHUT_TT,PCBVBG_TT,PCLAUNAMBG_TT,PCKHAC_SUM,BHCN_TT,THUETNCN_TT,TA_TT,PHAITRUKHAC_SUM,LUONGTHANG_SUM,PHAITRU_SUM,THANHTIEN'
SET @Query =
'
WITH BangLuongThang AS (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.ma_can_bo AS MaCanBo,
bangLuong.ma_phu_cap AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
--bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + ''')) AND
    dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
--ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.ma AS MaChucVu,
chucVu.ten AS TenChucVu,
CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
canBo.Ngay_NN AS NgayNhapNguDate,
canBo.Ngay_XN AS NgayXuatNguDate,
canBo.Ngay_TN AS NgayTaiNguDate,
CASE WHEN canBo.Thang_TNN IS NULL THEN 0 ELSE canBo.Thang_TNN END AS ThangTnn,
capBac.xau_noi_ma XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.ma_cvd104=chucVu.ma
WHERE
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)
SELECT ROW_NUMBER() over(order by MaCapBac DESC, Ten ASC) as stt, MaDonVi, MaCanBo, TenCanBo, Ten, Tnn, MaCapBac, CapBac, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
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
--canBo.HSChucVu,
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
CASE WHEN bangLuong.MaPhuCap = ' + '''NTN''' + ' THEN dbo.f_luong_ntn(canBo.NgayNhapNguDate, canBo.NgayXuatNguDate, canBo.NgayTaiNguDate, canBo.ThangTnn, 6, 2022) ELSE bangLuong.GiaTri END AS GiaTri,
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
--WHERE MaCapBac LIKE ''0%''
ORDER BY MaCapBac DESC, Ten ASC'
execute(@Query)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]    Script Date: 3/26/2024 10:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]
	@Thang int,
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
IF @IsOrderChucVu = 1
	WITH BangLuongThang AS( 
		SELECT 
			BangLuongThang.ma_can_bo AS MaCanBo,
			Sum(BangLuongThang.Gia_Tri) AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 BangLuongThang INNER JOIN TL_DS_CapNhap_BangLuong_NQ104 BangLuong
			ON BangLuongThang.parent = BangLuong.Id
		WHERE
			BangLuong.Ma_CachTL = 'CACH0'
			AND BangLuongThang.ma_phu_cap IN (SELECT TL_DM_PhuCap_NQ104.Ma_PhuCap FROM TL_DM_PhuCap_NQ104 WHERE iLoai = 3)
			AND BangLuong.Thang = @Thang
			AND BangLuong.Nam = @Nam
			AND BangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
		GROUP BY BangLuongThang.ma_can_bo
	), ThongTinCanBo AS
	(
		SELECT 
			CanBo.Ma_CanBo AS MaCanBo,
			CanBo.Parent AS MaDonVi,
			CanBo.Ten_CanBo AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
            --ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
			CanBo.So_TaiKhoan AS SoTaiKhoan
		FROM TL_DM_CanBo CanBo
		INNER JOIN TL_DM_DonVi DonVi
			ON CanBo.Parent = DonVi.Ma_DonVi
        LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.ma_cvd104=chucVu.ma
		WHERE CanBo.Nam = @Nam
		AND CanBo.Thang = @Thang
		AND CanBo.IsDelete = 1
		AND CanBo.Khong_Luong = 0
	)

	SELECT 
		BangLuongThang.GiaTri,
		ThongTinCanBo.MaCanBo,
		ThongTinCanBo.MaDonVi,
		ThongTinCanBo.TenCanBo,
		ThongTinCanBo.Ten,
		--ThongTinCanBo.HSChucVu,
		ThongTinCanBo.MaCapBac,
		ThongTinCanBo.SoTaiKhoan
	FROM BangLuongThang
	INNER JOIN ThongTinCanBo
	ON BangLuongThang.MaCanBo = ThongTinCanBo.MaCanBo
	ORDER BY 
         MaCapBac,
         Ten 
ELSE
	WITH BangLuongThang AS( 
		SELECT 
			BangLuongThang.ma_can_bo AS MaCanBo,
			Sum(BangLuongThang.Gia_Tri) AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 BangLuongThang INNER JOIN TL_DS_CapNhap_BangLuong_NQ104 BangLuong
			ON BangLuongThang.parent = BangLuong.Id
		WHERE
			BangLuong.Ma_CachTL = 'CACH0'
			AND BangLuongThang.Ma_Phu_Cap IN (SELECT TL_DM_PhuCap_NQ104.Ma_PhuCap FROM TL_DM_PhuCap_NQ104 WHERE iLoai = 3)
			AND BangLuong.Thang = @Thang
			AND BangLuong.Nam = @Nam
			AND BangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
		GROUP BY BangLuongThang.ma_can_bo
	), ThongTinCanBo AS
	(
		SELECT 
			CanBo.Ma_CanBo AS MaCanBo,
			CanBo.Parent AS MaDonVi,
			CanBo.Ten_CanBo AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
            --ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
			CanBo.So_TaiKhoan AS SoTaiKhoan
		FROM TL_DM_CanBo CanBo
		INNER JOIN TL_DM_DonVi DonVi
			ON CanBo.Parent = DonVi.Ma_DonVi
        LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.ma_cvd104=chucVu.ma
		WHERE CanBo.Nam = @Nam
		AND CanBo.Thang = @Thang
		AND CanBo.IsDelete = 1
		AND CanBo.Khong_Luong = 0
	)

	SELECT 
		BangLuongThang.GiaTri,
		ThongTinCanBo.MaCanBo,
		ThongTinCanBo.MaDonVi,
		ThongTinCanBo.TenCanBo,
		ThongTinCanBo.Ten,
		--ThongTinCanBo.HSChucVu,
		ThongTinCanBo.MaCapBac,
		ThongTinCanBo.SoTaiKhoan
	FROM BangLuongThang
	INNER JOIN ThongTinCanBo
	ON BangLuongThang.MaCanBo = ThongTinCanBo.MaCanBo
	Where GiaTri > 0
	ORDER BY MaCapBac, Ten 
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]    Script Date: 3/26/2024 10:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]
	@cachTinhLuong nvarchar(20),
	@maDonVi nvarchar(20),
	@thang int,
	@nam int
AS
BEGIN
	SELECT 
		NULL AS iStt,
		Ten_Cbo AS sTenCbo,
		So_TaiKhoan AS sSoTaiKhoan,
		Ten_KhoBac AS sTenKhoBac,
		--TL_BangLuong_Thang_NQ104.ThanhTien AS fThanhTien,
		NULL AS sNoiDung

	FROM TL_BangLuong_Thang_NQ104
	INNER JOIN TL_DM_CanBo
	ON TL_BangLuong_Thang_NQ104.Ma_CBo = TL_DM_CanBo.Ma_CanBo
	WHERE Ma_CachTL = @cachTinhLuong
	AND TL_BangLuong_Thang_NQ104.Ma_DonVi = @maDonVi
	AND TL_BangLuong_Thang_NQ104.THANG = @thang
	AND TL_BangLuong_Thang_NQ104.NAM = @nam
END
;
;
;
GO
