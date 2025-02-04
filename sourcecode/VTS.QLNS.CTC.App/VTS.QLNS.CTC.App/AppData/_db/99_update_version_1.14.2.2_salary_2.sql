/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]    Script Date: 3/27/2024 11:02:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_truylinh_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]    Script Date: 3/27/2024 11:02:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int,
	@IsTruyLinh bit,
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'TLCBTL_TT,TNLCBTL_TT,TLBLCBTL_TT,TLCVCDTL_TT,TNLCVCDTL_TT,TLBLCVCDTL_TT,TTL,NTN,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCCOV_TT,TRUYLINHKHAC_SUM,LUONGTHANG_SUM,PHAITRU_SUM,THANHTIEN,TTL_LHT,TTL_PCCV,TTL_PCCOV'
	SET @Query =
	'
	DECLARE @IsTruyLinh bit
	SET @IsTruyLinh = '+ CAST(@IsTruyLinh AS VARCHAR(1)) + ';
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CAN_BO			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH5''
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
			ISNULL(canBo.ma_cb104, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			loaiNhom.ten_loai TenLoai,
			loaiNhom.ten_nhom TenNhom,
			capBacLuong.ten_dm CapBacLuong,
			chucVu.Ma AS MaChucVu,
			chucVu.Ten AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.Ma_CB104=capBac.Ma_Cb


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
WHERE cha.loai = 0 ) loaiNhom
ON  loaiNhom.loai = canBo.loai
	AND loaiNhom.nhom = canBo.nhom_chuyen_mon 
	AND canBo.loai_doi_tuong in (select * from f_split(loaiNhom.loai_doi_tuong))

LEFT JOIN (SELECT * FROM TL_DM_CapBac_Luong_NQ104 WHERE loai = 3 and nam = ' + CAST(@Nam AS VARCHAR(4)) + ') capBacLuong
ON (
	(
	canBo.loai_doi_tuong IN (''1'',''3.2'',''4'',''5'') 
	AND canBo.loai_doi_tuong IN 
	(SELECT * FROM f_split(capBacLuong.loai_doi_tuong)) 
	)
	OR 
	(
	canBo.loai_doi_tuong NOT IN (''1'',''3.2'',''4'',''5'') 
	AND (capBacLuong.ma_dm_cha = loaiNhom.nhom)  AND capBacLuong.xau_noi_ma like loaiNhom.xau_noi_ma + ''-%''
	)
)
AND capBacLuong.ma_dm = canBo.ma_bac_luong
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_CVd104 = chucVu.Ma
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND capBac.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
AND chucVu.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
)



	SELECT MaDonVi, MaCanBo, TenCanBo, TenNhom, TenLoai, CapBacLuong, Ten, Tnn, MaCapBac, CapBac, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ' + @Cols + ' FROM (
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
			canBo.MaChucVu,
			canBo.TenChucVu,
			canBo.TenNhom,
			canBo.TenLoai,
			canBo.CapBacLuong,
			canBo.Stk,
			canBo.NgayNhapNgu,
			canBo.NgayXuatNgu,
			canBo.NgayTaiNgu,
			canBo.Tnn,
			bangLuong.GiaTri,
			bangLuong.MaPhuCap
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt 
	WHERE 
		(@IsTruyLinh = 1 And THANHTIEN > 0) or
		(@IsTruyLinh = 0 And THANHTIEN < 0)'
	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
	execute(@Query)
END
;
;
;
GO
