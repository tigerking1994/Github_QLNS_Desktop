/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an]    Script Date: 2/10/2023 11:34:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tien_an]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tien_an]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]    Script Date: 2/14/2023 12:08:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]    Script Date: 2/14/2023 12:08:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]
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
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM TL_BangLuong_Thang bangLuong
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
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)
SELECT ROW_NUMBER() over(order by HSChucVu DESC, MaCapBac DESC, Ten ASC) as stt, MaDonVi, MaCanBo, TenCanBo, Ten, Tnn, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
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
ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC'
execute(@Query)
END
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an]    Script Date: 2/10/2023 11:34:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tien_an] @thang int, @nam int, @maDonVi nvarchar(MAX), @daysInMonth int AS
BEGIN
SELECT 
       canBo.Parent MaDonVi,
	   donvi.Ten_DonVi TenDonVi,
       PhuCapTienAn.MA_PHUCAP MaPhuCap,
       phucap.Ten_PhuCap TienAn,
       PhuCapTienAn.GIA_TRI DinhMuc,
	   'x' as Nhan,
	   CAST(COUNT(canBo.Ma_CanBo) as decimal) SoNguoi,
	   'x' as Nhan, 
		CASE 
			When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			WHEN (PhuCapTienAn.HuongPC_SN IS NULL or PhuCapTienAn.HuongPC_SN = 0) AND phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(@daysInMonth as decimal)
			WHEN phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(PhuCapTienAn.HuongPC_SN as decimal)
		End SoNgay,
	   'ngày' as Dv_tinh,
	   '=' Bang,
	   (PhuCapTienAn.GIA_TRI * COUNT(canBo.Ma_CanBo) * CASE 
			When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			WHEN (PhuCapTienAn.HuongPC_SN IS NULL or PhuCapTienAn.HuongPC_SN = 0) AND phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(@daysInMonth as decimal)
			WHEN phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(PhuCapTienAn.HuongPC_SN as decimal)
		End) ThanhTien
FROM Tl_Dm_CanBo canBo
JOIN
  (SELECT MA_CBO,
          cbopc.MA_PHUCAP,
          cbopc.GIA_TRI,
          cbopc.HuongPC_SN
   FROM TL_CanBo_PhuCap cbopc
   LEFT JOIN Tl_DM_PhuCap mapc ON cbopc.MA_PHUCAP = mapc.Ma_PhuCap
   WHERE mapc.Parent IN ('TIENAN', 'TIENAN2')
     AND cbopc.GIA_TRI > 0) PhuCapTienAn ON canBo.Ma_CanBo = PhuCapTienAn.MA_CBO
JOIN Tl_dm_PhuCap phucap ON PhuCapTienAn.MA_PHUCAP = phucap.Ma_PhuCap
JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
WHERE canBo.Thang = @thang
  AND canBo.Nam = @nam
  And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
  and canbo.IsDelete = 1
  Group By canBo.Parent,
	   donvi.Ten_DonVi,
       PhuCapTienAn.MA_PHUCAP,
	   PhuCapTienAn.HuongPC_SN,
       phucap.Ten_PhuCap,
	   phucap.Parent,
       PhuCapTienAn.GIA_TRI
End
;
GO
