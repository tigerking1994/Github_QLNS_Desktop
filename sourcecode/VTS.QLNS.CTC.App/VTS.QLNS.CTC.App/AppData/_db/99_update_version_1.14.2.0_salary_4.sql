/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ra_quan_nq104]    Script Date: 3/26/2024 9:16:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ra_quan_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ra_quan_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ra_quan_nq104]    Script Date: 3/26/2024 9:16:56 AM ******/
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
			 canbo.ma_cb104 as MaCb,
			 canBo.Thang, 
			 canBo.Nam,
			 canBo.Parent as MaDonVi,
			 donvi.Ten_DonVi as TenDonVi,
			 canBo.So_SoLuong as SoSoLuong,
			 canBo.Ngay_NN as NgayNn,
			 canBo.Ngay_XN as NgayXn,
			 capBac.Note as TenCapBac,
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
