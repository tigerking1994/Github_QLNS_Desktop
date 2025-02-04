update TL_DM_CapBac set Lht_Hs='0.5' where Ma_CB ='03'
update TL_DM_CapBac set Lht_Hs='0.6' where Ma_CB ='04'
update TL_Bao_Cao set Ma_Parent = 1 where Ma_BaoCao = '1.6'
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo]    Script Date: 22/07/2022 3:10:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_danhsach_canbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_danhsach_canbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_thue_tncn]    Script Date: 22/07/2022 3:14:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_thue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_thue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ra_quan]    Script Date: 22/07/2022 3:18:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ra_quan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ra_quan]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ra_quan]    Script Date: 22/07/2022 3:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_get_data_ra_quan]
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
			 canbo.Ma_CB as MaCb,
			 canBo.Thang, 
			 canBo.Nam,
			 canBo.Parent as MaDonVi,
			 donvi.Ten_DonVi as TenDonVi,
			 canBo.So_SoLuong as SoSoLuong,
			 canBo.Ngay_NN as NgayNn,
			 canBo.Ngay_XN as NgayXn,
			 capBac.Note as TenCapBac,
             bangLuong.GIA_TRI AS GiaTri,
             bangLuong.MA_PHUCAP AS MaPhuCap
      FROM Tl_CanBo_PhuCap bangLuong
      RIGHT JOIN TL_DM_CanBo canBo ON bangLuong.Ma_CBo = canBo.Ma_CanBo
	  LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
	  Join TL_DM_CapBac capBac ON canBo.Ma_CB = capBac.Ma_Cb
      WHERE
		canBo.IsDelete=1
        AND canBo.Khong_Luong=0
		And canBo.Ma_CB like '0%') x PIVOT (SUM(GiaTri)
                                           FOR MaPhuCap IN (TIENTAUXE_TT, TIENANDUONG_TT, TIENCTLH_TT, GTKHAC_TT)) pvt
      Order By MaDonVi
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_thue_tncn]    Script Date: 22/07/2022 3:14:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_get_data_thue_tncn]
As
SELECT TenCb,
		  MaCanBo,
		  MaCb,
		  Thang,
		  Nam,
		  MaDonVi,
		  TenDonVi,
		  SoSoLuong,
          THUONG_TT AS TienThuong,
          THUNHAPKHAC_TT AS LoiIchKhac,
          GIAMTHUE_TT AS TienThueDuocGiam,
          THUEDANOP_TT AS ThueTNCNDaNop
   FROM
     (SELECT canBo.Ma_CanBo MaCanBo,
             canBo.Ten_CanBo AS TenCb,
			 canbo.Ma_CB as MaCb,
			 canBo.Thang, 
			 canBo.Nam,
			 canBo.Parent as MaDonVi,
			 donvi.Ten_DonVi as TenDonVi,
			 canBo.So_SoLuong as SoSoLuong,
             bangLuong.GIA_TRI AS GiaTri,
             bangLuong.MA_PHUCAP AS MaPhuCap
      FROM Tl_CanBo_PhuCap bangLuong
      JOIN TL_DM_CanBo canBo ON bangLuong.Ma_CBo=canBo.Ma_CanBo
	  LEFT JOIN TL_DM_DonVi donvi ON canbo.Parent = donvi.Ma_DonVi
      WHERE
		canBo.IsDelete=1
        AND canBo.Khong_Luong=0) x PIVOT (SUM(GiaTri)
                                           FOR MaPhuCap IN (THUONG_TT, THUNHAPKHAC_TT, GIAMTHUE_TT, THUEDANOP_TT)) pvt
      Order By MaDonVi
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo]    Script Date: 22/07/2022 3:10:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_find_danhsach_canbo]
AS
	Select
	  canbo.[Id]
      ,[BHTN]
      ,[bNuocNgoai]
      ,[Cb_KeHoach]
      ,[Cccd]
      ,[DateCreated]
      ,[DateModified]
      ,[Dia_Chi]
      ,[Dien_Thoai]
      ,[GTGC]
      ,[HeSoLuong]
      ,[HsLuongKeHoach]
      ,[HsLuongTran]
      ,canbo.[iTrangThai]
      ,[IdLuongTran]
      ,[IsDelete]
      ,[IsLock]
      ,[Is_Nam]
      ,[Khong_Luong]
      ,[Ma_BL]
      ,[Ma_CanBo]
      ,canbo.[Ma_CB]
      ,[Ma_CbCu]
      ,canbo.[Ma_CV]
      ,[Ma_DiaBan_HC]
      ,[Ma_Hieu_CanBo]
      ,[Ma_KhoBac]
      ,[Ma_PBan]
      ,[MaSo_DV_SDNS]
      ,[MaSo_VAT]
      ,[Ma_TangGiam]
      ,[Ma_TangGiamCu]
      ,[MaTK_LQ]
      ,[Nam]
      ,[Nam_TN]
      ,[Nam_VK]
      ,[NgayCap_CMT]
      ,[Ngay_NhanCB]
      ,[Ngay_NN]
      ,[NgaySinh]
      ,[Ngay_TN]
      ,[NgayTruyLinh]
      ,[Ngay_XN]
      ,[Nhom]
      ,[NoiCap_CMT]
      ,[NoiCongTac]
      ,[PCCV]
      ,canbo.[Parent]
      ,canbo.[Readonly]
      ,[So_CMT]
      ,[So_SoLuong]
      ,[So_TaiKhoan]
      ,canbo.[Splits]
      ,[Ten_CanBo]
      ,[Ten_KhoBac]
      ,[Thang]
      ,[Thang_TNN]
      ,[ThoiHan_TangCb]
      ,[TM]
      ,[UserCreator]
      ,[UserModifier]
      ,[bHuongThangTnn],
	  donvi.Ten_DonVi as Ten_DonVi,
	  ISNULL(capbac.Note, '') CapBac,
	  ISNULL(chucvu.Ten_Cv, '') ChucVu,
	  dbo.f_split_empty(canbo.Ten_CanBo) AS Ten
	From TL_DM_CanBo canbo
	Join TL_DM_CapBac capbac on capbac.Ma_Cb = canbo.Ma_CB
	Left join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi 
	Left join TL_DM_ChucVu chucvu on canbo.Ma_CV = chucvu.Ma_Cv
	Where canbo.IsDelete = 1
	--Order By canbo.Parent, canbo.Ma_CV desc, canbo.Ma_CB desc
;
;
GO
