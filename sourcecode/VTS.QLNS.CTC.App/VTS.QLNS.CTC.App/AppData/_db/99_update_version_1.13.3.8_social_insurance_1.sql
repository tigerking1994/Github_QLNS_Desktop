/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo]    Script Date: 10/27/2023 5:15:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_danhsach_canbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_danhsach_canbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_all_phu_cap_va_che_do_bhxh]    Script Date: 10/27/2023 5:15:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_all_phu_cap_va_che_do_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_all_phu_cap_va_che_do_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]    Script Date: 10/27/2023 5:15:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]    Script Date: 10/27/2023 5:15:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]
	@NamLamViec int,
	@VoucherID uniqueidentifier,
	@MaDonVi nvarchar(50),
	@LoaiGiaiThich int
AS
BEGIN
	
		select
			--ml.iSTT,
			chungtudonvi.iID_QT_CTCT_GiaiThich,
			ml.iID_MLGT IIDMLNS,
			ml.sNoiDung,
			chungtudonvi.fTruyThu_BHXH_NLD FTruyThuBHXHNLD,
			chungtudonvi.fTruyThu_BHXH_NSD FTruyThuBHXHNSD,
			chungtudonvi.fTruyThu_BHXH_TongCong FTruyThuBHXHTongCong,
			chungtudonvi.fTruyThu_BHYT_NLD FTruyThuBHYTNLD,
			chungtudonvi.fTruyThu_BHYT_NSD FTruyThuBHYTNSD,
			chungtudonvi.fTruyThu_BHYT_TongCong FTruyThuBHYTTongCong,
			chungtudonvi.fTruyThu_BHTN_NLD FTruyThuBHTNNLD,
			chungtudonvi.fTruyThu_BHTN_NSD FTruyThuBHTNNSD,
			chungtudonvi.fTruyThu_BHTN_TongCong FTruyThuBHTNTongCong,
			chungtudonvi.fTongTruyThu_BHXH FTongTruyThuBHXH,
			chungtudonvi.sKienNghi,
			chungtudonvi.fPhaiNop_BHXH FPhaiNopBHXH,
			chungtudonvi.fPhaiNop_TrongQuyNam FPhaiNopTrongQuyNam,
			chungtudonvi.fTruyThu_QuyNamTruoc FTruyThuQuyNamTruoc,
			chungtudonvi.fPhaiNop_QuyNamTruoc FPhaiNopQuyNamTruoc,
			chungtudonvi.fDaNop_TrongQuyNam FDaNopTrongQuyNam,
			chungtudonvi.fConPhaiNopTiep,
			chungtudonvi.fSoPhaiThuNop,
			chungtudonvi.fSoDaNopTrongNam,
			chungtudonvi.fSoDaNopSau31_12 FSoDaNopSau3112,
			chungtudonvi.fTongSoDaNop,
			chungtudonvi.fSoConPhaiNop,
			chungtudonvi.iQuanSo,
			chungtudonvi.fQuyTienLuongCanCu,
			chungtudonvi.fSoTienGiamDong,
			chungtudonvi.dTuNgay,
			chungtudonvi.dDenNgay,
			chungtudonvi.sNguoiTao,
			chungtudonvi.dNgayTao,
			chungtudonvi.sNguoiSua,
			chungtudonvi.sNguoiSua
		from
		(select
			iSTT,
			iID_MLGT,
			sNoiDung,
			iLoai
		from BH_QTT_MucLucGiaiThich
		where iLoai = @LoaiGiaiThich) ml
		left join
		(select distinct
			ctgt.iID_QT_CTCT_GiaiThich,
			ctgt.iID_QTT_BHXH_ChungTu,
			ctgt.sNguoiTao,
			ctgt.sNguoiSua,
			ctgt.dNgayTao,
			ctgt.dNgaySua,
			ctgt.iID_MaDonVi,
			ctgt.iNamLamViec,
			ctgt.iQuyNam,
			ctgt.iQuyNamLoai,
			ctgt.sQuyNamMoTa,
			ctgt.sNoiDung,
			ctgt.sKienNghi,
			ctgt.fPhaiNop_BHXH,
			ctgt.fPhaiNop_TrongQuyNam,
			ctgt.fTruyThu_QuyNamTruoc,
			ctgt.fPhaiNop_QuyNamTruoc,
			ctgt.fDaNop_TrongQuyNam,
			ctgt.fConPhaiNopTiep,
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.fTongTruyThu_BHXH,
			ctgt.fSoPhaiThuNop,
			ctgt.fSoDaNopTrongNam,
			ctgt.fSoDaNopSau31_12,
			ctgt.fTongSoDaNop,
			ctgt.fSoConPhaiNop,
			ctgt.iQuanSo,
			ctgt.fQuyTienLuongCanCu,
			ctgt.fSoTienGiamDong,
			ctgt.dTuNgay,
			ctgt.dDenNgay,
			ctgt.iID_MLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			join
			BH_QTT_BHXH_CTCT_GiaiThich ctgt on ct.iID_QTT_BHXH_ChungTu = ctgt.iID_QTT_BHXH_ChungTu
			where
			ct.iID_QTT_BHXH_ChungTu = @VoucherID
			and ctgt.iID_MaDonVi = @MaDonVi
				) chungtudonvi 
			on ml.iID_MLGT = chungtudonvi.iID_MLNS
		
		order by ml.iSTT

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_all_phu_cap_va_che_do_bhxh]    Script Date: 10/27/2023 5:15:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_find_all_phu_cap_va_che_do_bhxh] 
	
AS
BEGIN
	
	select
	Id
	--,bGiaTri GiaTri
	,bHuongPc_Sn HuongPCSN
	--,bSaoChep
	--,Chon
	--,Cong_Thuc
	--,Dinh_Dang
	--,Gia_Tri GiaTri
	--,He_So
	--,HuongPC_SN
	--,iDinhDang
	--,iLoai
	,Is_Formula IsFormula
	--,Is_Readonly
	--,IThang_ToiDa
	--,Ma_KMCP
	,Ma_PhuCap MaPhuCap
	--,Ma_TTM_Ng
	--,Numeric_Scale
	,Parent Parent
	--,PhanTram_CT
	--,Readonly
	--,Splits
	--,Ten_Ngan
	,Ten_PhuCap TenPhuCap
	--,Tinh_BHXH
	--,Tinh_TNCN
	--,Xau_Noi_Ma
	--,XSort
	--,fGiaTriNhoNhat
	--,fGiaTriLonNhat
	--,fGiaTriPhuCap_KemTheo
	--,iId_PhuCap_KemTheo
	--,iId_Ma_PhuCap_KemTheo
	--,Ten_NganHang
	from TL_DM_PhuCap
	union
	select
	Id
	--,null
	,null
	--,null
	--,null
	--,null
	--,null
	--,null
	--,null
	--,null
	--,null
	--,iLoaiCheDo
	,bTinhTheoCongThuc Is_Formula
	--,null
	--,null
	--,null
	,sMaCheDo Ma_PhuCap
	--,null
	--,null
	,null
	--,null
	--,null
	--,null
	--,null
	,sTenCheDo Ten_PhuCap
	--,null
	--,null
	--,sXauNoiMa Xau_Noi_Ma
	--,null
	--,null
	--,null
	--,null
	--,null
	--,null
	--,null
	from TL_DM_CheDoBHXH

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo]    Script Date: 10/27/2023 5:15:45 PM ******/
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
	  dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
	  canbo.bTinhBHXH BTinhBHXH --anhnh156
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
