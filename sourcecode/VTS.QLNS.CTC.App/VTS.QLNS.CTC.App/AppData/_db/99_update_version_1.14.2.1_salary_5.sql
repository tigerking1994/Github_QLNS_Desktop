/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]    Script Date: 3/26/2024 5:44:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]    Script Date: 3/26/2024 5:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                                 @donViTinh int, @maCachTl nvarchar(5) AS DECLARE @Cols AS NVARCHAR(MAX) DECLARE @IsParent AS Bit
SET @IsParent = 0;


SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN,TM';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.ma_can_bo AS MaCanBo,
          bangLuong.ma_phu_cap AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id --AND bangLuong.Ma_CB like @ngach + '%'

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
  (SELECT canbo.Ma_CanBo MaCanBo,
          donvi.Ma_DonVi MaDonVi,
          donvi.Ten_DonVi DoiTuong,
          case when capbac.Parent = '3.3' then '3' else capbac.Parent end as Ngach
   FROM TL_DM_CanBo canBo
   LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac_NQ104 capbac ON canBo.Ma_CB = capbac.Ma_Cb
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam),
     CanBoLuongNgach AS
  (SELECT MaCanBo,
          Ngach,
          DoiTuong,
          MaDonVi,
          LHT_TT,
          HSBL_TT,
          PCTNVK_TT,
          PCCV_TT,
          PCTN_TT,
          PCKV_TT,
          PCCOV_TT,
          PCDACTHU_SUM,
          PCTRA_SUM,
          PCKHAC_SUM,
          LUONGTHANG_SUM,
          BHXHCN_TT,
          BHYTCN_TT,
          BHTNCN_TT,
          TA_TONG,
          THUETNCN_TT,
          TRICHLUONG_TT,
          GTKHAC_TT,
          PHAITRU_SUM,
          THANHTIEN,
		  TM
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaDonVi,
             canBo.DoiTuong,
             canBo.Ngach,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN, TM)) pvt)

SELECT DoiTuong,
       MaDonVi,
       Ngach,
       COUNT(*) SoNguoi,
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
       SUM(GTKHAC_TT/@donViTinh) GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN,
       @IsParent IsParent
FROM CanBoLuongNgach
GROUP BY DoiTuong,
         MaDonVi,
         Ngach
;
;
;
;
;
GO
