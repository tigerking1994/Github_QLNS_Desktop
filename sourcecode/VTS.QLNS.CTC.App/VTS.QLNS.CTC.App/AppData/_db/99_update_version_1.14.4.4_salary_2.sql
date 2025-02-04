/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]    Script Date: 5/13/2024 2:53:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_chitiet_nq104]    Script Date: 5/13/2024 2:53:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_chitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_chitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_chitiet_nq104]    Script Date: 5/13/2024 2:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_chitiet_nq104]
	@Id UNIQUEIDENTIFIER
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);;

SET @cols = STUFF(
  (
    SELECT
      DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap_NQ104 phucap
       FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = '
	WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo	AS MaCanBo,
			canBo.Ten_CanBo	AS TenCanBo,
			canBo.IsDelete AS IsDelete,
			canbo.Ngay_XN,
			canbo.Ma_TangGiam,
			donVi.Ma_DonVi	AS MaDonVi,
			donVi.Ten_DonVi	AS TenDonVi
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent = donVi.Ma_DonVi
		WHERE canBo.Khong_Luong = 0
	)
	SELECT Thang, Nam, MaCanBo, TenCanBo, TenDonVi, ' + @cols + ' FROM (
		SELECT
			bangLuongThang.THANG		AS Thang,
			bangLuongThang.NAM			AS Nam,
			canBo.MaCanBo				AS MaCanBo,
			canBo.TenCanBo				AS TenCanBo,
			canBo.TenDonVi				AS TenDonVi,
			bridge.gia_tri GiaTri,
			bridge.ma_phu_cap MaPhuCap
		FROM TL_BangLuong_Thang_NQ104 bangLuongThang
		LEFT JOIN TL_BangLuong_Thang_Bridge_NQ104 bridge ON bangLuongThang.Ma_CBo = bridge.ma_can_bo
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.Ma_CBo = canBo.MaCanBo
		WHERE
			(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = bangLuongThang.THANG and year(canbo.Ngay_XN) = bangLuongThang.NAM))
			and bangLuongThang.parent = + ''' + CONVERT(NVARCHAR(100), @Id) + '''
	) x
	PIVOT 
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @cols + ')
	) p '
execute(@query)
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]    Script Date: 5/13/2024 2:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                          @donViTinh int AS DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LCVCD_SUM,LCB_SUM,TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,THANHTIEN';

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
   FROM TL_DM_CanBo_NQ104 canBo
   INNER JOIN TL_DM_DonVi_NQ104 donVi ON canBo.Parent=donVi.Ma_DonVi AND canBo.Parent IN (SELECT * FROM f_split(@maDonVi))
   LEFT JOIN
     (SELECT capbaccon.Ma_Cb MaCapBac,
             capbaccha.Ma_Cb MaCapBacCha,
             capbaccha.ten_cb TenCapBac
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
		CAST(COUNT(LCB_SUM) as float) DemTLCB_TT,
		SUM(LCB_SUM)/@donViTinh LCB_SUM,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(LCVCD_SUM)/@donViTinh LCVCD_SUM,

	   SUM(TLCB_TT)/@donViTinh TLCB_TT,
	   CAST(COUNT(TLCB_TT) as float) DemTLCB_TT,
	   SUM(TNLCB_TT)/@donViTinh TNLCB_TT,
	   SUM(TLBLCB_TT)/@donViTinh TLBLCB_TT,

	   SUM(TLCV_CD_TT)/@donViTinh TLCV_CD_TT,
	   CAST(COUNT(LCVCD_SUM) as float) DemTLCV_CD_TT,
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
      FOR MaPhuCap IN (LCVCD_SUM,LCB_SUM,TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,THANHTIEN)) pvt
	Where MaNgach = '4'
	GROUP BY TenNgach,
         MaNgach, MaDonVi
	Order By MaDonVi, MaNgach
;
;
;
;
GO
