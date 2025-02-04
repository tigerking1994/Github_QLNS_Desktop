/****** Object:  StoredProcedure [dbo].[sp_tl_find_tenloai_tennhom_canbo_nq104]    Script Date: 4/16/2024 9:49:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_tenloai_tennhom_canbo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_tenloai_tennhom_canbo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]    Script Date: 4/16/2024 9:49:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]    Script Date: 4/16/2024 9:49:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]
	@thang int,
	@nam int,
	@maDonVi nvarchar(250)
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
      ,canbo.[Nam]
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
	  ISNULL(capbac.ten_cb, '') CapBac,
	  ISNULL(chucvu.ten, '') ChucVu,
	   ISNULL(canbo.loai_doi_tuong, '') LoaiDoiTuong,
	  dbo.f_split_empty(canbo.Ten_CanBo) AS Ten
	From TL_DM_CanBo_NQ104 canbo
	Join TL_DM_CapBac_NQ104 capbac on capbac.Ma_Cb = canbo.Ma_CB104
	Left join TL_DM_DonVi_NQ104 donvi on canbo.Parent = donvi.Ma_DonVi 
	Left join TL_DM_ChucVu_NQ104 chucvu on canbo.ma_cvd104 = chucvu.ma
	Where canbo.IsDelete = 1 
		and canbo.Thang = @thang 
		and canbo.Nam = @nam
		and (@maDonVi = '' or canbo.Parent = @maDonVi)
	--Order By canbo.Parent, canbo.Ma_CV desc, canbo.Ma_CB desc
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_tenloai_tennhom_canbo_nq104]    Script Date: 4/16/2024 9:49:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_find_tenloai_tennhom_canbo_nq104]
	@thang int,
	@nam int,
	@maDonVi nvarchar(250),
	@maCanBo nvarchar(250)
AS
		select cb.Ten_CanBo as TenCanBo,
		loai_doituong.ma_dm as MaLoaiDoiTuong,
		loai_doituong.ten_dm as TenLoaiDoiTuong,
		loai_doituong.nam,
		loai.ma_dm  as MaLoai,
		loai.ten_dm as Tenloai,
		nhom.ma_dm as MaNhom,
		nhom.ten_dm as TenNhom,
		bac.ma_dm as MaBac,
		bac.ten_dm as TenBac
		from TL_DM_CanBo_NQ104 as cb 
		left join TL_DM_CapBac_Luong_NQ104 as loai_doituong on cb.loai_doi_tuong in 
		(SELECT * FROM f_split(loai_doituong.loai_doi_tuong))
		left join TL_DM_CapBac_Luong_NQ104 as loai on loai_doituong.ma_dm = loai.ma_dm_cha and loai.ma_dm = cb.loai 
		and loai.xau_noi_ma like ''+ loai_doituong.xau_noi_ma +'%' 
		left join TL_DM_CapBac_Luong_NQ104 as nhom on nhom.ma_dm_cha = loai.ma_dm and nhom.ma_dm = cb.nhom_chuyen_mon 
		and nhom.xau_noi_ma like ''+ loai.xau_noi_ma +'%' 
		left join TL_DM_CapBac_Luong_NQ104 as bac on( (cb.loai_doi_tuong in('1', '3.1', '4', '5' ) 
		and bac.ma_dm_cha = loai_doituong.ma_dm and cb.ma_bac_luong = bac.ma_dm) or (bac.ma_dm_cha = nhom.ma_dm and bac.ma_dm = cb.ma_bac_luong and bac.xau_noi_ma like ''+ nhom.xau_noi_ma +'%') )
		where loai_doituong.loai = 0 and loai_doituong.nam=@nam and cb.Thang=@thang and cb.Ma_CanBo=@maCanBo and cb.Parent=@maDonVi
	--Order By canbo.Parent, canbo.Ma_CV desc, canbo.Ma_CB desc
;
;
;
;
;
;
GO
