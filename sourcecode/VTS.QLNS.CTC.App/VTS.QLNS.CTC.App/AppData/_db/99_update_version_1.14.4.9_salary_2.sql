/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]    Script Date: 5/21/2024 4:50:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]    Script Date: 5/21/2024 4:50:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@DonViTinh int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @MaPhuCap NVARCHAR(MAX) SET @MaPhuCap = 'LCBHT_TT,HSBL_TT,PCTNVK_TT,PCTHD_TT';
	DECLARE @MaPhuCapVKTHD NVARCHAR(MAX) SET @MaPhuCapVKTHD = 'PCTNVK_TT,PCTHD_TT';

    WITH CanBoVK AS (
		SELECT
			bangLuong.ma_can_bo AS MaCanBo
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.ma_phu_cap IN (SELECT * FROM f_split(@MaPhuCapVKTHD))
			AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
			AND bangLuong.GIA_TRI != 0
	), BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.ma_can_bo			AS MaCanBo,
			bangLuong.ma_phu_cap			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.ma_phu_cap IN (SELECT * FROM f_split(@MaPhuCap))
			AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		 SELECT
		  canBo.Ma_CanBo		AS MaCanBo,
		  donVi.Ma_DonVi		AS MaDonVi,
		  capBacParent.Ma_Cb	AS Ngach,
		  capBacParent.Ten_Cb	AS DoiTuong,
		  capBac.Ma_Cb			AS MaCapBac
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac_NQ104 capBac
		  ON canBo.Ma_CB104 = capBac.Ma_Cb
		 INNER JOIN TL_DM_CapBac capBacParent
		  ON capBac.Parent = capBacParent.Ma_Cb
		WHERE
		  canBo.Thang = @Thang
		  AND canBo.Nam = @Nam
		  AND donVi.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))
		  AND canBo.Ma_CanBo in (SELECT * FROM CanBoVK)
	), SoLieuBaoCao AS (
		SELECT
			canBo.MaDonVi								AS MaDonVi,
			canBo.Ngach									AS Ngach,
			COUNT(bangLuongThang.MaCanBo)				AS SoNguoi,
			canBo.DoiTuong								AS DoiTuong,
			bangLuongThang.MaPhuCap						AS MaPhuCap,
			SUM(bangLuongThang.GiaTri) / @DonViTinh		AS GiaTri
		FROM BangLuongThang bangLuongThang
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.MaCanBo = canBo.MaCanBo
		GROUP BY MaDonVi, Ngach, DoiTuong, MaPhuCap
	)

	SELECT * FROM SoLieuBaoCao
	PIVOT (
		SUM(GiaTri)
		FOR MaPhuCap IN (LCBHT_TT,HSBL_TT,PCTNVK_TT,PCTHD_TT)
	) pvt ORDER BY MaDonVi, Ngach;
END
;
;
;
;
;
GO
