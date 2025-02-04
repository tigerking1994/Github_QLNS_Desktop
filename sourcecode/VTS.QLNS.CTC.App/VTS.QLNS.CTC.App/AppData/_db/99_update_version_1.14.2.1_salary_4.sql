/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104]    Script Date: 3/26/2024 4:39:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ra_quan_nq104]    Script Date: 3/26/2024 4:39:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ra_quan_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ra_quan_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ra_quan_nq104]    Script Date: 3/26/2024 4:39:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_data_ra_quan_nq104]
As
SELECT TenCb,
		  MaCanBo,
		  MaCb,
		  Thang,
		  Nam,
		  NgayNn,
		  NgayXn,
		  MaDonVi,
		  TenDonVi,
		  SoSoLuong,
		  TenCapBac,
          TIENTAUXE_TT AS TienTauXe,
          TIENANDUONG_TT AS TienAnDuong,
          TIENCTLH_TT AS TienChiaTay,
          GTKHAC_TT AS GiamTruKhac
   FROM
     (SELECT canBo.Ma_CanBo MaCanBo,
             canBo.Ten_CanBo AS TenCb,
			 canbo.ma_cb104  as MaCb,
			 canBo.Thang, 
			 canBo.Nam,
			 canBo.Parent as MaDonVi,
			 donvi.Ten_DonVi as TenDonVi,
			 canBo.So_SoLuong as SoSoLuong,
			 canBo.Ngay_NN as NgayNn,
			 canBo.Ngay_XN as NgayXn,
			 capBac.ten_cb as TenCapBac,
             bangLuong.GIA_TRI AS GiaTri,
             bangLuong.MA_PHU_CAP AS MaPhuCap
      FROM TL_CanBo_PhuCap_Bridge_NQ104 bangLuong
      RIGHT JOIN TL_DM_CanBo canBo ON bangLuong.Ma_Can_Bo = canBo.Ma_CanBo
	  LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
	  Join TL_DM_CapBac_NQ104 capBac ON canBo.ma_cb104 = capBac.Ma_Cb
      WHERE
		canBo.IsDelete=1
        AND canBo.Khong_Luong=0
		And canBo.ma_cb104 like '0%') x PIVOT (SUM(GiaTri)
                                           FOR MaPhuCap IN (TIENTAUXE_TT, TIENANDUONG_TT, TIENCTLH_TT, GTKHAC_TT)) pvt
      Order By MaDonVi
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104]    Script Date: 3/26/2024 4:39:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                          @donViTinh int, @isSummary bit, @maCachTl nvarchar(100) AS DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN,TM';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.ma_can_bo AS MaCanBo,
          bangLuong.ma_phu_cap AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.ma_phu_cap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
          ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
          ISNULL(capBac.TenCapBac, '0') AS TenNgach,
          ISNULL(capBac.MaCapBacCha, '0') AS MaNgach
   FROM TL_DM_CanBo canBo
   INNER JOIN TL_DM_DonVi donVi ON canBo.Parent=donVi.Ma_DonVi AND canBo.Parent IN (SELECT * FROM f_split(@maDonVi))
   LEFT JOIN
     (SELECT capbaccon.Ma_Cb MaCapBac,
             capbaccha.Ma_Cb MaCapBacCha,
             capbaccha.ten_cb TenCapBac
      FROM TL_DM_CapBac_NQ104 capbaccon
      LEFT JOIN TL_DM_CapBac_NQ104 capbaccha ON capbaccon.Parent = capbaccha.Ma_Cb) capBac ON canBo.ma_cb104=capBac.MaCapBac
   LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.Ma_CV=chucVu.ma
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam)

	 SELECT TenNgach,
          MaNgach,
		  MaDonVi,
          COUNT(MaCanBo) AS SoNguoi,
          SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
       SUM(TA_TONG)/@donViTinh TA_TONG,
       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
	   SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaCapBac,
             canBo.TenNgach,
             canBo.MaNgach,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap,
			 CASE When @isSummary = 1 Then '1' Else bangLuong.MaDonVi end as MaDonVi
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN, TM)) pvt
	GROUP BY TenNgach,
         MaNgach, MaDonVi
	Order By MaDonVi, MaNgach
	
--UNION
--	SELECT TenNgach,
--		MaNgach,
--		MaDonVi,
--		COUNT(MaCanBo) AS SoNguoi,
--		SUM(LHT_TT)/@donViTinh LHT_TT,
--		SUM(HSBL_TT)/@donViTinh HSBL_TT,
--       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
--       SUM(PCCV_TT)/@donViTinh PCCV_TT,
--       SUM(PCTN_TT)/@donViTinh PCTN_TT,
--       SUM(PCKV_TT)/@donViTinh PCKV_TT,
--       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
--       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
--       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
--       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
--       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
--       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
--       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
--       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
--       SUM(TA_TONG)/@donViTinh TA_TONG,
--       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
--       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
--       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
--       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
--       SUM(THANHTIEN)/@donViTinh THANHTIEN
--	FROM 
--	(SELECT 
--		N'Tổng truy lĩnh' AS TenNgach,
--		'x' AS MaNgach,
--		bl.GiaTri, bl.MaPhuCap, 
--		CASE When @isSummary = 1 Then '1' Else bl.MaDonVi end as MaDonVi,
--		bl.MaCanBo
--		FROM BangLuongTruyLinh bl INNER JOIN ThongTinCanBo
--			ON bl.MaCanBo = ThongTinCanBo.MaCanBo) x PIVOT (SUM(GiaTri)
--															FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt
--	GROUP BY MaDonVi, TenNgach, MaNgach
;
;
;
;
GO
