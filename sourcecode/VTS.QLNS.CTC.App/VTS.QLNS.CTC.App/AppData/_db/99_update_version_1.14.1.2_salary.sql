/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]    Script Date: 11/03/2024 5:25:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_truylinh_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]    Script Date: 11/03/2024 3:59:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]    Script Date: 11/03/2024 3:59:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]    Script Date: 11/03/2024 3:59:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bang_phucap_nq104]    Script Date: 11/03/2024 3:59:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_bang_phucap_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_bang_phucap_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bang_phucap_nq104]    Script Date: 11/03/2024 3:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_donvi_bang_phucap_nq104] @thang int, @nam int, @cachTinhLuong varchar(20)
AS
BEGIN

select Parent into #tmpCB from TL_DM_CanBo where left (Ma_CB104, 1) = 0 and Thang = @thang and Nam = @nam group by Parent

select donvi.* from #tmpCB canbo
left join TL_DS_CapNhap_BangLuong_NQ104 bangluong  on canbo.Parent = bangluong.Ma_CBo 
left join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
where bangluong.Thang = @thang and bangluong.Nam = @nam and bangluong.Ma_CachTL = @cachTinhLuong

drop table #tmpCB

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]    Script Date: 11/03/2024 3:59:41 PM ******/
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

SET @Cols = 'TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,PCCLKHAC_SUM,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
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
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.xau_noi_ma XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
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
ON canBo.Ma_Cvd104=chucVu.Ma
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
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]    Script Date: 11/03/2024 3:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]
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

	SET @Cols = 'TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,NTN,LHT_HS,LHT_TT,PCCV_TT,PCTHD_TT,PCKV_TT,PCKVCS_TT,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,PCTRA_SUM,LUONGTHANG_SUM,PHAITRU_SUM,TM,THANHTIEN,PCNU_TT,HSBL_HS,PCTNVK_HS'
	SET @Query =
	'
	WITH blt AS (
		SELECT * FROM TL_BangLuong_Thang_Bridge_NQ104
		WHERE Ma_Don_Vi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	), 
	BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM blt bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
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
			ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			chucVu.Ma AS MaChucVu,
			chucVu.Ten AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			canBo.Ngay_NN AS NgayNhapNguDate,
			canBo.Ngay_XN AS NgayXuatNguDate,
			canBo.Ngay_TN AS NgayTaiNguDate,
			CASE WHEN canBo.Thang_TNN IS NULL THEN 0 ELSE canBo.Thang_TNN END AS ThangTnn,
			capBac.Xau_Noi_Ma XauNoiMa
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.Ma_CB104 = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
			ON canBo.Ma_Cvd104 =chucVu.Ma
		WHERE
			(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
	)
	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, Tnn, MaCapBac, CapBac, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
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
	ORDER BY MaCapBac DESC, Ten ASC'
	execute(@Query)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]    Script Date: 11/03/2024 3:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                          @donViTinh int AS DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,THANHTIEN';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_Can_Bo AS MaCanBo,
          bangLuong.MA_PHU_CAP AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_Phu_Cap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),
     ThongTinCanBo AS
  (SELECT canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
          ISNULL(canBo.Ma_CB104, '0') AS MaCapBac,
          ISNULL(capBac.TenCapBac, '0') AS TenNgach,
          ISNULL(capBac.MaCapBacCha, '0') AS MaNgach
   FROM TL_DM_CanBo canBo
   INNER JOIN TL_DM_DonVi donVi ON canBo.Parent=donVi.Ma_DonVi AND canBo.Parent IN (SELECT * FROM f_split(@maDonVi))
   LEFT JOIN
     (SELECT capbaccon.Ma_Cb MaCapBac,
             capbaccha.Ma_Cb MaCapBacCha,
             capbaccha.Note TenCapBac
      FROM TL_DM_CapBac_NQ104 capbaccon
      LEFT JOIN TL_DM_CapBac_NQ104 capbaccha ON capbaccon.Parent = capbaccha.Ma_Cb) capBac ON canBo.Ma_CB104=capBac.MaCapBac
   LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.Ma_Cvd104=chucVu.Ma
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam)
	 SELECT TenNgach,
		MaNgach,
		MaDonVi,
		COUNT(MaCanBo) AS SoNguoi,
		CAST(COUNT(LHT_TT) as float) DemLHT_TT,
		SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,

	   SUM(TLCB_TT)/@donViTinh TLCB_TT,
	   CAST(COUNT(TLCB_TT) as float) DemTLCB_TT,
	   SUM(TNLCB_TT)/@donViTinh TNLCB_TT,
	   SUM(TLBLCB_TT)/@donViTinh TLBLCB_TT,

	   SUM(TLCV_CD_TT)/@donViTinh TLCV_CD_TT,
	   CAST(COUNT(TLCV_CD_TT) as float) DemTLCV_CD_TT,
	   SUM(TNLCV_CD_TT)/@donViTinh TNLCV_CD_TT,
	   SUM(TLBLCV_CD_TT)/@donViTinh TLBLCV_CD_TT,

	   CAST(COUNT(PCCV_TT) as float) DemPCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
	   CAST(COUNT(PCTN_TT) as float) DemPCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
	   CAST(COUNT(PCKV_TT) as float) DemPCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
	   CAST(COUNT(PCCOV_TT) as float) DemPCCOV_TT,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
	   CAST(COUNT(PCTRA_SUM) as float) DemPCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
	   CAST(COUNT(PCKHAC_SUM) as float) DemPCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(THANHTIEN)/@donViTinh THANHTIEN
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaCapBac,
             canBo.TenNgach,
             canBo.MaNgach,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap,
			 bangLuong.MaDonVi MaDonVi
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo
	  Where bangLuong.GiaTri > 0) x PIVOT (SUM(GiaTri)
      FOR MaPhuCap IN (TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt
	Where MaNgach = '4'
	GROUP BY TenNgach,
         MaNgach, MaDonVi
	Order By MaDonVi, MaNgach
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]    Script Date: 11/03/2024 5:25:27 PM ******/
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
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
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
			ON canBo.ma_cvd104 = chucVu.Ma
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
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
GO

