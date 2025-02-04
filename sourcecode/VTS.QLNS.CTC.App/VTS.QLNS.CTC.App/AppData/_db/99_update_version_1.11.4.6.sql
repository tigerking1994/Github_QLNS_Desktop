/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo]    Script Date: 28/07/2022 2:06:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_danhsach_canbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_danhsach_canbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo]    Script Date: 28/07/2022 2:06:35 PM ******/
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
      ,[bKhongTinhNTN],
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
;
GO

-- xoa bhuongthangtnn
delete TL_Map_Column_Config where id = 'ca5afcb5-52f8-4cb3-a13c-a4065acbc686' 
delete TL_DM_PhuCap where id = '69733f3a-9108-49b9-86cd-507bbeb18817'