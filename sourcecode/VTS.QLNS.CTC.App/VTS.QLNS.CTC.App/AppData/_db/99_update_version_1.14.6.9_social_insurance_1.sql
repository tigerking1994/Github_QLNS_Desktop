/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_giaithich_trocap]    Script Date: 7/25/2024 3:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_giaithich_trocap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_giaithich_trocap]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha]    Script Date: 7/25/2024 3:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi]    Script Date: 7/25/2024 3:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 7/25/2024 3:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 7/25/2024 3:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 7/25/2024 3:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 7/25/2024 3:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 7/25/2024 3:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 7/25/2024 3:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 7/25/2024 3:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam] 
	@MaDonVi nvarchar(50),
	@Year int
AS
BEGIN
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_y]') AND type in (N'U')) drop table tbl_cancu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach3_y]') AND type in (N'U')) drop table tbl_cancu_cach3_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach4_y]') AND type in (N'U')) drop table tbl_cancu_cach4_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_base_y]') AND type in (N'U')) drop table tbl_base_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_c3_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_c3_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_so_nguoi_f_y]') AND type in (N'U')) drop table tbl_so_nguoi_f_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;
	
	-- Lấy dữ liệu quyết toán lương full công
	select
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 sum(ctct.DieuChinh) fGiaTri,
	 pc.Ma_Cb
	into tbl_cancu_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi
	where ctct.MaCachTl in ('CACH0', 'CACH5')
	and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.Ma_DonVi = @MaDonVi
	and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 pc.Ma_Cb

	 -- Lấy dữ liệu quyết toán quỹ lương không thuộc ốm dưới 14 ngày/tháng
	 select
		 pc.Ma_PhuCap,
		 pc.XauNoiMa,
		 donvi.iKhoi,
		 sum(isnull(ctct.DieuChinh, 0)) fGiaTri,
		 pc.Ma_Cb
	 into tbl_cancu_cach3_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where isnull(ctct.MaCachTl,'') = 'CACH3'
		and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 pc.Ma_Cb

	 -- Lấy dữ liệu quyết toán quỹ lương BHXH chi trả
	 select
		 ctct.XauNoiMa,
		 donvi.iKhoi,
		 sum(isnull(ctct.DieuChinh, 0)) fGiaTri
	 into tbl_cancu_cach4_y
	from TL_QT_ChungTuChiTiet ctct
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where isnull(ctct.MaCachTl,'') = 'CACH4'
		and ct.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 ctct.XauNoiMa,
		 donvi.iKhoi

	-- Lấy dữ liệu sang QTT = full công - BHXH chi trả - quỹ lương != ốm dưới 14 ngày
	 select distinct
		 c.Ma_PhuCap,
		 c.XauNoiMa,
		 c.iKhoi,
		 c.fGiaTri ChuaTru,
		 c3.fGiaTri Cach3,
		 (isnull(c.fGiaTri, 0) - isnull(c3.fGiaTri, 0) - isnull(c2.fGiaTri, 0)) as fGiaTri,
		 c.Ma_Cb
		  into tbl_base_y
	 from tbl_cancu_y c
	 left join tbl_cancu_cach3_y c3 on c.Ma_PhuCap = c3.Ma_PhuCap and c.XauNoiMa = c3.XauNoiMa and c.iKhoi = c3.iKhoi
	 left join tbl_cancu_cach4_y c2 on c.XauNoiMa = c2.XauNoiMa and c.iKhoi = c2.iKhoi
	---------------------------
	--Get so nguoi full công
	select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	  into tbl_cancu_so_nguoi_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.XauNoiMa

	 --Get so nguoi không thuộc ốm dưới 14 ngày/tháng
	 select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	  into tbl_cancu_so_nguoi_c3_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = 'CACH3'
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.XauNoiMa

	 --Get số người
	 select a.XauNoiMa, (isnull(a.IQSBQNam, 0) - isnull(b.IQSBQNam, 0)) IQSBQNam
	  into tbl_so_nguoi_f_y
	 from tbl_cancu_so_nguoi_y a
	 left join tbl_cancu_so_nguoi_c3_y b on a.XauNoiMa = b.XauNoiMa
	 ------------------------------------------

	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  songuoi.IQSBQNam IQSBQNam,
	  --songuoi.IQSBQNam/12 IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL,
	  cancu.Ma_Cb
	 into tbl_cancu_result_y
	 from tbl_base_y cancu
	 left join tbl_base_y luongchinh on cancu.XauNoiMa = luongchinh.XauNoiMa and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_base_y pccv on cancu.XauNoiMa = pccv.XauNoiMa and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_base_y pctn on cancu.XauNoiMa = pctn.XauNoiMa and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_base_y pctnvk on cancu.XauNoiMa = pctnvk.XauNoiMa and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_base_y hsbl on cancu.XauNoiMa = hsbl.XauNoiMa and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_cancu_so_nguoi_y songuoi on cancu.XauNoiMa = songuoi.XauNoiMa

	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL,
	  cancu.Ma_Cb
	 into tbl_cancu_result_final_y
	 from tbl_cancu_result_y cancu
	 group by cancu.XauNoiMa, cancu.iKhoi, cancu.Ma_Cb

	 --Luong BHXH
	 select
	  ctct.sMaCB XauNoiMa,
	  sum(ctct.nGiaTri) FNghiOm
	 into tbl_luong_can_cu_y
	 from TL_BangLuong_ThangBHXH ctct
	 join TL_DS_CapNhap_BangLuong ct on ctct.iID_Parent = ct.Id
	 where ctct.iNam = @Year
		and ctct.sMaDonVi = @MaDonVi
		and ctct.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
		and (ctct.sMaCB like '1%' or ctct.sMaCB like '2%' or ctct.sMaCB like '0%' or ctct.sMaCB = '43' or ctct.sMaCB = '423' or ctct.sMaCB = '425' or ctct.sMaCB in ('3.1', '3.2', '3.3', '413', '415'))
		and ct.KhoaBangLuong = 1
		and isnull(ct.STongHop, '') <> ''
	 group by ctct.sMaCB

	 update tbl_luong_can_cu_y set XauNoiMa = '1' where XauNoiMa like '1%'
	 update tbl_luong_can_cu_y set XauNoiMa = '2' where XauNoiMa like '2%'
	 update tbl_luong_can_cu_y set XauNoiMa = '4' where XauNoiMa like '0%'
	 update tbl_luong_can_cu_y set XauNoiMa = '3' where XauNoiMa in ('3.1', '3.2', '3.3', '413', '415')
	 update tbl_luong_can_cu_y set XauNoiMa = '43' where XauNoiMa in ('423', '425')

	 select
	  XauNoiMa,
	  sum(FNghiOm) FNghiOm
	 into tbl_luong_can_cu_final_y
	 from tbl_luong_can_cu_y
	 group by XauNoiMa

	 --result
	 select
	  luong.XauNoiMa sXauNoiMa,
	  luong.IQSBQNam,
	  luong.iKhoi,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm,
	  luong.Ma_Cb SMaCapBac
	 from tbl_cancu_result_final_y luong
	 left join tbl_luong_can_cu_final_y bhxh on luong.XauNoiMa = bhxh.XauNoiMa
	 -----------------------------------------------
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_y]') AND type in (N'U')) drop table tbl_cancu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach3_y]') AND type in (N'U')) drop table tbl_cancu_cach3_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach4_y]') AND type in (N'U')) drop table tbl_cancu_cach4_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_base_y]') AND type in (N'U')) drop table tbl_base_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_c3_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_c3_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_so_nguoi_f_y]') AND type in (N'U')) drop table tbl_so_nguoi_f_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 7/25/2024 3:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy] 
	@MaDonVi nvarchar(50),
	@Year int,
	@Months nvarchar(20),
	@LoaiQuyNam int
AS
BEGIN

	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach3]') AND type in (N'U')) drop table tbl_cancu_cach3;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach4]') AND type in (N'U')) drop table tbl_cancu_cach4;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_base]') AND type in (N'U')) drop table tbl_base;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_c3]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_c3;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_so_nguoi_f]') AND type in (N'U')) drop table tbl_so_nguoi_f;

	-- Lấy dữ liệu quyết toán lương full công
	select
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 sum(isnull(ctct.DieuChinh, 0)) fGiaTri,
	 pc.Ma_Cb
	 into tbl_cancu
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where ctct.MaCachTl in ('CACH0', 'CACH5')
		and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 pc.Ma_Cb

	 -----------------
	 -- Lấy dữ liệu quyết toán quỹ lương không thuộc ốm dưới 14 ngày/tháng
	 select
		 pc.Ma_PhuCap,
		 pc.XauNoiMa,
		 donvi.iKhoi,
		 sum(isnull(ctct.DieuChinh, 0)) fGiaTri,
		 pc.Ma_Cb
	 into tbl_cancu_cach3
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where isnull(ctct.MaCachTl,'') = 'CACH3'
		and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 pc.Ma_Cb

	 -----------------
	 -- Lấy dữ liệu quyết toán quỹ lương BHXH chi trả
	 select
		 ctct.XauNoiMa,
		 donvi.iKhoi,
		 sum(isnull(ctct.DieuChinh, 0)) fGiaTri
	 into tbl_cancu_cach4
	from TL_QT_ChungTuChiTiet ctct
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where isnull(ctct.MaCachTl,'') = 'CACH4'
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 ctct.XauNoiMa,
		 donvi.iKhoi
	 -------------------
	 -- Lấy dữ liệu sang QTT = full công - BHXH chi trả - quỹ lương != ốm dưới 14 ngày
	 select distinct
		 c.Ma_PhuCap,
		 c.XauNoiMa,
		 c.iKhoi,
		 c.fGiaTri ChuaTru,
		 c3.fGiaTri Cach3,
		 (isnull(c.fGiaTri, 0) - isnull(c3.fGiaTri, 0) - isnull(c2.fGiaTri, 0)) as fGiaTri,
		 c.Ma_Cb
		  into tbl_base
	 from tbl_cancu c
	 left join tbl_cancu_cach3 c3 on c.Ma_PhuCap = c3.Ma_PhuCap and c.XauNoiMa = c3.XauNoiMa and c.iKhoi = c3.iKhoi
	 left join tbl_cancu_cach4 c2 on c.XauNoiMa = c2.XauNoiMa and c.iKhoi = c2.iKhoi
	 --------------------------
	--Get so nguoi full công
	select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	  into tbl_cancu_so_nguoi
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.XauNoiMa

	 --Get so nguoi không thuộc ốm dưới 14 ngày/tháng
	 select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	  into tbl_cancu_so_nguoi_c3
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = 'CACH3'
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.XauNoiMa
	 --Get số người
	 select a.XauNoiMa, (isnull(a.IQSBQNam, 0) - isnull(b.IQSBQNam, 0)) IQSBQNam
	  into tbl_so_nguoi_f
	 from tbl_cancu_so_nguoi a
	 left join tbl_cancu_so_nguoi_c3 b on a.XauNoiMa = b.XauNoiMa

	 ------------------------------------------
	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  case
		when @LoaiQuyNam = 0 then songuoi.IQSBQNam
		else songuoi.IQSBQNam/3
	  end as IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL,
	  cancu.Ma_Cb
	  into tbl_cancu_result
	 from tbl_base cancu
	 left join tbl_base luongchinh on cancu.XauNoiMa = luongchinh.XauNoiMa and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_base pccv on cancu.XauNoiMa = pccv.XauNoiMa and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_base pctn on cancu.XauNoiMa = pctn.XauNoiMa and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_base pctnvk on cancu.XauNoiMa = pctnvk.XauNoiMa and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_base hsbl on cancu.XauNoiMa = hsbl.XauNoiMa and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_so_nguoi_f songuoi on cancu.XauNoiMa = songuoi.XauNoiMa

	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL,
	  cancu.Ma_Cb
	  into tbl_cancu_result_final
	 from tbl_cancu_result cancu
	 group by cancu.XauNoiMa, cancu.iKhoi, cancu.Ma_Cb

	 --Luong BHXH
	 select
	  ctct.sMaCB XauNoiMa,
	  sum(ctct.nGiaTri) FNghiOm
	  into tbl_luong_can_cu
	 from TL_BangLuong_ThangBHXH ctct
	 join TL_DS_CapNhap_BangLuong ct on ctct.iID_Parent = ct.Id
	 where ctct.iNam = @Year
		and ctct.sMaDonVi = @MaDonVi
		and ctct.iThang in (SELECT * FROM f_split(@Months))
		and ctct.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
		and (ctct.sMaCB like '1%' or ctct.sMaCB like '2%' or ctct.sMaCB like '0%' or ctct.sMaCB = '43' or ctct.sMaCB = '423' or ctct.sMaCB = '425' or ctct.sMaCB in ('3.1', '3.2', '3.3', '413', '415'))
		and ct.KhoaBangLuong = 1
		and isnull(ct.STongHop, '') <> ''
	 group by ctct.sMaCB

	update tbl_luong_can_cu set XauNoiMa = '1' where XauNoiMa like '1%'
	update tbl_luong_can_cu set XauNoiMa = '2' where XauNoiMa like '2%'
	update tbl_luong_can_cu set XauNoiMa = '4' where XauNoiMa like '0%'
	update tbl_luong_can_cu set XauNoiMa = '3' where XauNoiMa in ('3.1', '3.2', '3.3', '413', '415')
	update tbl_luong_can_cu set XauNoiMa = '43' where XauNoiMa in ('423', '425')

	select
	  XauNoiMa,
	  sum(FNghiOm) FNghiOm
	  into tbl_luong_can_cu_final
	 from tbl_luong_can_cu
	 group by XauNoiMa

	 --result
	 select
	  luong.XauNoiMa sXauNoiMa,
	  luong.IQSBQNam,
	  luong.iKhoi,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm,
	  luong.Ma_Cb SMaCapBac
	 from tbl_cancu_result_final luong
	 left join tbl_luong_can_cu_final bhxh on luong.Ma_Cb = bhxh.XauNoiMa
	 -----------------------------------------------

	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach3]') AND type in (N'U')) drop table tbl_cancu_cach3;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach4]') AND type in (N'U')) drop table tbl_cancu_cach4;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_base]') AND type in (N'U')) drop table tbl_base;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_c3]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_c3;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_so_nguoi_f]') AND type in (N'U')) drop table tbl_so_nguoi_f;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 7/25/2024 3:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;
	DECLARE @sSoChungTuTH nvarchar(1000)

	--Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		set @sSoChungTuTH = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iQuyNamLoai = @ILoaiQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
	end
	--In luy ke
	else
	begin
	set @sSoChungTuTH = (SELECT SUBSTRING(( SELECT ',' + pr.sTongHop  AS [text()] FROM BH_QTT_BHXH_ChungTu pr
														WHERE pr.iNamLamViec = @NamLamViec
															and pr.iQuyNam <= @IQuy
															and pr.iQuyNamLoai = @ILoaiQuy
															and pr.iID_MaDonVi = @IdDonVis
															and pr.iLoaiTongHop = 2
															and pr.bDaTongHop = 0
														FOR XML PATH (''), TYPE
													).value('text()[1]','nvarchar(max)'), 2, 1000) )
	end

	CREATE TABLE #result(
	iID_MLNS uniqueidentifier,
	iID_MLNS_Cha uniqueidentifier,
	bHangCha bit, 
	sXauNoiMa nvarchar(200), 
	sMoTa nvarchar(200), 
	iNamLamViec int,
	iQSBQNam int,
	fLuongChinh float,
	fPhuCapChucVu float,
	fPCTNNghe float,
	fPCTNVuotKhung float,
	fNghiOm float,
	fHSBL float,
	fTongQTLN float,
	fDuToan float,
	fDaQuyetToan float,
	fConLai float,
	fThu_BHXH_NLD float,
	fThu_BHXH_NSD float,
	fTongSoPhaiThuBHXH float,
	fThu_BHYT_NLD float,
	fThu_BHYT_NSD float,
	fTongSoPhaiThuBHYT float,
	fThu_BHTN_NLD float,
	fThu_BHTN_NSD float,
	fTongSoPhaiThuBHTN float,
	fTongNLD float,
	fTongNSD float,
	fTongCong float,
	MaDonVi nvarchar(50),
	TenDonVi nvarchar(50)
	);

----------------END DETAIL AGENCY----------------
--Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1;
	end
	--In luy ke
	else
	begin
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
			,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
			,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
			,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
			,sum(isnull(ctct.fHSBL, 0)) fHSBL
			,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
			,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
			,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
			,sum(isnull(ctct.fTongCong, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam <= @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1
			group by
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
	end
----------------END DETAIL----------------
----------------INSERT TOTAL----------------
--Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		INSERT INTO #result
		(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
		)
		select
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa,
				@NamLamViec iNamLamViec,
				chungtudonvi.iQSBQNam,
				chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
				chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
				chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
				chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
				chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
				chungtudonvi.fHSBL/@Donvitinh fHSBL,
				(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
				chungtudonvi.fDuToan,
				chungtudonvi.fDaQuyetToan,
				chungtudonvi.fConLai,
				chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
				chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
				chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
				chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
				(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
				chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
				chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
				(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)) fTongNLD,
				(isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)) fTongNSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong,
				null,
				null
				FROM
					(select
						BH_DM_MucLucNganSach.iID_MLNS,
						BH_DM_MucLucNganSach.iID_MLNS_Cha,
						BH_DM_MucLucNganSach.bHangCha,
						BH_DM_MucLucNganSach.sLNS,
						BH_DM_MucLucNganSach.sL,
						BH_DM_MucLucNganSach.sK,
						BH_DM_MucLucNganSach.sM,
						BH_DM_MucLucNganSach.sTM,
						BH_DM_MucLucNganSach.sTTM,
						BH_DM_MucLucNganSach.sNG,
						BH_DM_MucLucNganSach.sTNG,
						BH_DM_MucLucNganSach.sXauNoiMa,
						BH_DM_MucLucNganSach.sMoTa
					from BH_DM_MucLucNganSach 
					where sLNS like '902%'
					AND iNamLamViec = @NamLamViec) mlns
				LEFT JOIN(
					select
					ct.iID_MaDonVi,
					ct.iNamLamViec,
					ctct.iID_QTT_BHXH_ChungTu_ChiTiet
					,ctct.iID_QTT_BHXH_ChungTu
					,ctct.iQSBQNam
					,ctct.fLuongChinh
					,ctct.fPCChucVu
					,ctct.fPCTNNghe
					,ctct.fPCTNVuotKhung
					,ctct.fNghiOm
					,ctct.fHSBL
					,ctct.fTongQTLN
					,ctct.fDuToan
					,ctct.fDaQuyetToan
					,ctct.fConLai
					,ctct.fThu_BHXH_NLD
					,ctct.fThu_BHXH_NSD
					,ctct.fTongSoPhaiThuBHXH
					,ctct.fThu_BHYT_NLD
					,ctct.fThu_BHYT_NSD
					,ctct.fTongSoPhaiThuBHYT
					,ctct.fThu_BHTN_NLD
					,ctct.fThu_BHTN_NSD
					,ctct.fTongSoPhaiThuBHTN
					,ctct.fTongCong
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
					from
					BH_QTT_BHXH_ChungTu ct
					join
					BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
					where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam = @IQuy
					and ct.iQuyNamLoai = @ILoaiQuy
					and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
					--and ct.iID_MaDonVi = @IdDonVis
					and ct.iLoaiTongHop = 2
		--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
				)chungtudonvi 
					on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				ORDER BY mlns.sXauNoiMa;
		end
		--In luy ke
		else
		begin
		INSERT INTO #result
		(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
		)
		select
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa,
				@NamLamViec iNamLamViec,
				chungtudonvi.iQSBQNam,
				chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
				chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
				chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
				chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
				chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
				chungtudonvi.fHSBL/@Donvitinh fHSBL,
				(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
				chungtudonvi.fDuToan,
				chungtudonvi.fDaQuyetToan,
				chungtudonvi.fConLai,
				chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
				chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
				chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
				chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
				(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
				chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
				chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
				(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)) fTongNLD,
				(isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)) fTongNSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong,
				null,
				null
				FROM
					(select
						BH_DM_MucLucNganSach.iID_MLNS,
						BH_DM_MucLucNganSach.iID_MLNS_Cha,
						BH_DM_MucLucNganSach.bHangCha,
						BH_DM_MucLucNganSach.sLNS,
						BH_DM_MucLucNganSach.sL,
						BH_DM_MucLucNganSach.sK,
						BH_DM_MucLucNganSach.sM,
						BH_DM_MucLucNganSach.sTM,
						BH_DM_MucLucNganSach.sTTM,
						BH_DM_MucLucNganSach.sNG,
						BH_DM_MucLucNganSach.sTNG,
						BH_DM_MucLucNganSach.sXauNoiMa,
						BH_DM_MucLucNganSach.sMoTa
					from BH_DM_MucLucNganSach 
					where sLNS like '902%'
					AND iNamLamViec = @NamLamViec) mlns
				LEFT JOIN(
					select
					ct.iID_MaDonVi,
					ct.iNamLamViec
					,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
					,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
					,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
					,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
					,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
					,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
					,sum(isnull(ctct.fHSBL, 0)) fHSBL
					,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
					,sum(isnull(ctct.fDuToan, 0)) fDuToan
					,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
					,sum(isnull(ctct.fConLai, 0)) fConLai
					,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
					,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
					,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
					,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
					,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
					,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
					,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
					,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
					,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
					,sum(isnull(ctct.fTongCong, 0)) fTongCong
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
					from
					BH_QTT_BHXH_ChungTu ct
					join
					BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
					where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam <= @IQuy
					and ct.iQuyNamLoai = @ILoaiQuy
					and ((@isCha = 0 and ctct.iID_MaDonVi = @IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi = @IdDonVis))
					--and ct.iID_MaDonVi = @IdDonVis
					and ct.iLoaiTongHop = 2
		--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end	
					group by 
					ct.iID_MaDonVi,
					ct.iNamLamViec
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
				)chungtudonvi 
					on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				ORDER BY mlns.sXauNoiMa;
		end

----------------END INSERT DETAIL----------------
----------------INSERT DETAIL----------------
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	SELECT
	NULL ,
	NULL ,
	0 bHangCha , 
	sXauNoiMa , 
	'   ' + dv.sTenDonVi,
	tempChiTietDonVi.iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPCChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	0 fTongNLD ,
	0 fTongNSD ,
	fTongCong ,
	dv.iID_MaDonVi, 
	dv.sTenDonVi as TenDonVi 
	FROM tempChiTietDonVi 
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

DROP TABLE #result;
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 7/25/2024 3:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr where pr.iNamLamViec = @NamLamViec
																			and pr.iQuyNam in (SELECT * FROM f_split(@SMonths))
																			and pr.iQuyNamLoai = @ILoaiQuy
																			and pr.iID_MaDonVi = @IdDonVis
																			and pr.iLoaiTongHop = 2
																			and pr.bDaTongHop = 0)

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	----------------END DETAIL AGENCY----------------
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO #tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
				and ct.iLoaiTongHop = 1
				and ct.bDaTongHop = 1;

	----------------END DETAIL----------------
	----------------INSERT TOTAL----------------
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(isnull(chungtudonvi.iQSBQNam, 0)) iQSBQNam,
			sum(isnull(chungtudonvi.fLuongChinh, 0))/@Donvitinh fLuongChinh,
			sum(isnull(chungtudonvi.fPCChucVu, 0))/@Donvitinh fPCChucVu,
			sum(isnull(chungtudonvi.fPCTNNghe, 0))/@Donvitinh fPCTNNghe,
			sum(isnull(chungtudonvi.fPCTNVuotKhung, 0))/@Donvitinh fPCTNVuotKhung,
			sum(isnull(chungtudonvi.fNghiOm, 0))/@Donvitinh fNghiOm,
			sum(isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fHSBL,
			sum(isnull((chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL), 0))/@Donvitinh fTongQTLN,
			sum(isnull(chungtudonvi.fDuToan, 0))/@Donvitinh fDuToan,
			sum(isnull(chungtudonvi.fDaQuyetToan, 0))/@Donvitinh fDaQuyetToan,
			sum(isnull(chungtudonvi.fConLai, 0))/@Donvitinh fConLai,
			sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0))/@Donvitinh fThu_BHXH_NLD,
			sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fThu_BHXH_NSD,
			sum((isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0))/@Donvitinh fThu_BHYT_NLD,
			sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fThu_BHYT_NSD,
			sum((isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))/@Donvitinh fThu_BHTN_NLD,
			sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fThu_BHTN_NSD,
			sum((isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			sum((isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			sum((isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
			sum((isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
				(select
					BH_DM_MucLucNganSach.iID_MLNS,
					BH_DM_MucLucNganSach.iID_MLNS_Cha,
					BH_DM_MucLucNganSach.bHangCha,
					BH_DM_MucLucNganSach.sLNS,
					BH_DM_MucLucNganSach.sL,
					BH_DM_MucLucNganSach.sK,
					BH_DM_MucLucNganSach.sM,
					BH_DM_MucLucNganSach.sTM,
					BH_DM_MucLucNganSach.sTTM,
					BH_DM_MucLucNganSach.sNG,
					BH_DM_MucLucNganSach.sTNG,
					BH_DM_MucLucNganSach.sXauNoiMa,
					BH_DM_MucLucNganSach.sMoTa
				from BH_DM_MucLucNganSach 
				where sLNS like '902%'
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN(
				select
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.iQSBQNam iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi = @IdDonVis
				and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
				and ct.iLoaiTongHop = 2	
			)chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				group by
					mlns.iID_MLNS,
					mlns.iID_MLNS_Cha,
					mlns.bHangCha,
					mlns.sXauNoiMa,
					mlns.sMoTa
				ORDER BY mlns.sXauNoiMa;


	----------------END INSERT DETAIL----------------
	----------------INSERT DETAIL----------------
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	SELECT
		NULL ,
		NULL ,
		0 bHangCha , 
		dt.sXauNoiMa , 
		'   ' + dv.sTenDonVi,
		dt.iNamLamViec ,
		sum(isnull(dt.iQSBQNam, 0)) iQSBQNam,
		sum(isnull(dt.fLuongChinh, 0)) fLuongChinh,
		sum(isnull(dt.fPCChucVu, 0)) fPCChucVu,
		sum(isnull(dt.fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(dt.fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(dt.fNghiOm, 0)) fNghiOm,
		sum(isnull(dt.fHSBL, 0)) fHSBL,
		sum(isnull(dt.fTongQTLN, 0)) fTongQTLN,
		sum(isnull(dt.fDuToan, 0)) fDuToan,
		sum(isnull(dt.fDaQuyetToan, 0)) fDaQuyetToan,
		sum(isnull(dt.fConLai, 0)) fConLai,
		sum(isnull(dt.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dt.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH,
		sum(isnull(dt.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dt.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT,
		sum(isnull(dt.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dt.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN,
		0 fTongNLD ,
		0 fTongNSD ,
		sum(isnull(dt.fTongCong, 0)) ,
		dv.iID_MaDonVi, 
		dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi dt
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = dt.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
	group by
		dt.sXauNoiMa, 
		dv.sTenDonVi,
		dt.iNamLamViec,
		dv.iID_MaDonVi, 
		dv.sTenDonVi;

	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 7/25/2024 3:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	--- GET CHI TIẾT ĐƠN VỊ
	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam
				,ctct.fLuongChinh
				,ctct.fPCChucVu
				,ctct.fPCTNNghe
				,ctct.fPCTNVuotKhung
				,ctct.fNghiOm
				,ctct.fHSBL
				,ctct.fTongQTLN
				,ctct.fDuToan
				,ctct.fDaQuyetToan
				,ctct.fConLai
				,ctct.fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN
				,ctct.fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam = @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
				--and ct.bDaTongHop = 0;
		end
		--In luy ke
		else
		begin
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
				,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
				,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
				,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
				,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
				,sum(isnull(ctct.fHSBL, 0)) fHSBL
				,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
				,sum(isnull(ctct.fDuToan, 0)) fDuToan
				,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
				,sum(isnull(ctct.fConLai, 0)) fConLai
				,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
				,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
				,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
				,sum(isnull(ctct.fTongCong, 0)) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam <= @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
				group by 
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				--and ct.bDaTongHop = 0;
		end
	--END chi tiet

	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	--INSERT TOTAL
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPCChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan) fDuToan,
			sum(chungtudonvi.fDaQuyetToan) fDaQuyetToan,
			sum(chungtudonvi.fConLai) fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))) fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))) fTongNSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
				(select
					BH_DM_MucLucNganSach.iID_MLNS,
					BH_DM_MucLucNganSach.iID_MLNS_Cha,
					BH_DM_MucLucNganSach.bHangCha,
					BH_DM_MucLucNganSach.sLNS,
					BH_DM_MucLucNganSach.sL,
					BH_DM_MucLucNganSach.sK,
					BH_DM_MucLucNganSach.sM,
					BH_DM_MucLucNganSach.sTM,
					BH_DM_MucLucNganSach.sTTM,
					BH_DM_MucLucNganSach.sNG,
					BH_DM_MucLucNganSach.sTNG,
					BH_DM_MucLucNganSach.sXauNoiMa,
					BH_DM_MucLucNganSach.sMoTa
				from BH_DM_MucLucNganSach 
				where sLNS like '902%'
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN tempChiTietDonVi chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			GROUP BY
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa;
		--INSERT CHI TIẾT	
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	SELECT
	NULL ,
	NULL ,
	0 bHangCha , 
	sXauNoiMa , 
	'   ' + dv.sTenDonVi,
	tempChiTietDonVi.iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPCChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	0 fTongNLD ,
	0 fTongNSD ,
	fTongCong ,
	dv.iID_MaDonVi, 
	dv.sTenDonVi as TenDonVi 
	FROM tempChiTietDonVi 
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;


	DROP TABLE #result;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 7/25/2024 3:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	--- GET CHI TIẾT ĐƠN VỊ
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO #tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
	--END chi tiet

	INSERT INTO #result
	(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
	)
	--INSERT TOTAL
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPCChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
			sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
			sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
				(select
					BH_DM_MucLucNganSach.iID_MLNS,
					BH_DM_MucLucNganSach.iID_MLNS_Cha,
					BH_DM_MucLucNganSach.bHangCha,
					BH_DM_MucLucNganSach.sLNS,
					BH_DM_MucLucNganSach.sL,
					BH_DM_MucLucNganSach.sK,
					BH_DM_MucLucNganSach.sM,
					BH_DM_MucLucNganSach.sTM,
					BH_DM_MucLucNganSach.sTTM,
					BH_DM_MucLucNganSach.sNG,
					BH_DM_MucLucNganSach.sTNG,
					BH_DM_MucLucNganSach.sXauNoiMa,
					BH_DM_MucLucNganSach.sMoTa
				from BH_DM_MucLucNganSach 
				where sLNS like '902%'
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN #tempChiTietDonVi chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			GROUP BY
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa;
		--INSERT CHI TIẾT	
	INSERT INTO #result
	(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
	)
	SELECT
		NULL ,
		NULL ,
		0 bHangCha , 
		dt.sXauNoiMa , 
		'   ' + dv.sTenDonVi,
		dt.iNamLamViec ,
		sum(isnull(dt.iQSBQNam, 0)) iQSBQNam,
		sum(isnull(dt.fLuongChinh, 0)) fLuongChinh,
		sum(isnull(dt.fPCChucVu, 0)) fPCChucVu,
		sum(isnull(dt.fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(dt.fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(dt.fNghiOm, 0)) fNghiOm,
		sum(isnull(dt.fHSBL, 0)) fHSBL,
		sum(isnull(dt.fTongQTLN, 0)) fTongQTLN,
		sum(isnull(dt.fDuToan, 0)) fDuToan,
		sum(isnull(dt.fDaQuyetToan, 0)) fDaQuyetToan,
		sum(isnull(dt.fConLai, 0)) fConLai,
		sum(isnull(dt.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dt.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH,
		sum(isnull(dt.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dt.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT,
		sum(isnull(dt.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dt.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN,
		0 fTongNLD ,
		0 fTongNSD ,
		sum(isnull(dt.fTongCong, 0)) ,
		dv.iID_MaDonVi, 
		dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi dt
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = dt.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
	group by
		dt.sXauNoiMa, 
		dv.sTenDonVi,
		dt.iNamLamViec,
		dv.iID_MaDonVi, 
		dv.sTenDonVi;

	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi]    Script Date: 7/25/2024 3:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi] 
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX)
AS
BEGIN
	
	select
		ctct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iLoai, 
		case when ct.iQuyNamLoai = 0 then round((cast(isnull(sum(ctct.iQSBQNam), 0) as float) / 12), 0)
			when ct.iQuyNamLoai = 1 then round((cast(isnull(sum(ctct.iQSBQNam), 0) as float) / 4), 0)
			else isnull(sum(ctct.iQSBQNam), 0)
		end iQSBQNam,
		isnull(sum(ctct.fLuongChinh), 0) fLuongChinh,
		isnull(sum(ctct.fPCChucVu), 0) FPhuCapChucVu,
		isnull(sum(ctct.fPCTNNghe), 0) fPCTNNghe,
		isnull(sum(ctct.fPCTNVuotKhung), 0) fPCTNVuotKhung,
		isnull(sum(ctct.fNghiOm), 0) fNghiOm,
		isnull(sum(ctct.fHSBL), 0) fHSBL,
		(isnull(sum(ctct.fLuongChinh), 0) + isnull(sum(ctct.fPCChucVu), 0) + isnull(sum(ctct.fPCTNNghe), 0) + isnull(sum(ctct.fPCTNVuotKhung), 0) + isnull(sum(ctct.fNghiOm), 0) + isnull(sum(ctct.fHSBL), 0)) fTongQTLN
		from 
	BH_QTT_BHXH_ChungTu ct
	join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu =  ctct.iID_QTT_BHXH_ChungTu
	where ct.iNamLamViec = @INamLamViec
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and  ct.iQuyNamLoai <> 2
		and ct.bIsKhoa = 1
	group by ctct.sXauNoiMa, ct.iID_MaDonVi, ct.iLoai, ct.iQuyNamLoai

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha]    Script Date: 7/25/2024 3:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_qtt_get_data_thang_quy_donvi_cha] 
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IdMaDonViCha nvarchar(MAX)
AS
BEGIN
	
	select
		ctct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iLoai, 
		case when ct.iQuyNamLoai = 0 then round((cast(isnull(sum(ctct.iQSBQNam), 0) as float) / 12), 0)
			when ct.iQuyNamLoai = 1 then round((cast(isnull(sum(ctct.iQSBQNam), 0) as float) / 4), 0)
			else isnull(sum(ctct.iQSBQNam), 0)
		end iQSBQNam,
		isnull(sum(ctct.fLuongChinh), 0) fLuongChinh,
		isnull(sum(ctct.fPCChucVu), 0) FPhuCapChucVu,
		isnull(sum(ctct.fPCTNNghe), 0) fPCTNNghe,
		isnull(sum(ctct.fPCTNVuotKhung), 0) fPCTNVuotKhung,
		isnull(sum(ctct.fNghiOm), 0) fNghiOm,
		isnull(sum(ctct.fHSBL), 0) fHSBL,
		(isnull(sum(ctct.fLuongChinh), 0) + isnull(sum(ctct.fPCChucVu), 0) + isnull(sum(ctct.fPCTNNghe), 0) + isnull(sum(ctct.fPCTNVuotKhung), 0) + isnull(sum(ctct.fNghiOm), 0) + isnull(sum(ctct.fHSBL), 0)) fTongQTLN
		from 
	BH_QTT_BHXH_ChungTu ct
	join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu =  ctct.iID_QTT_BHXH_ChungTu
	where ct.iNamLamViec = @INamLamViec
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdMaDonViCha))
		and ct.iQuyNamLoai <> 2
		and ct.bIsKhoa = 1
		and ct.iLoai = 1
	group by ctct.sXauNoiMa, ct.iID_MaDonVi, ct.iLoai, ct.iLoai, ct.iQuyNamLoai

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_giaithich_trocap]    Script Date: 7/25/2024 3:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_giaithich_trocap]
	-- Add the parameters for the stored procedure here
	@lstmaCanbo nvarchar(max) ,
	@Thang int ,
	@Nam int ,
	@DonViTinh int ,
	@TypeOutPut int,  -- 2: Đơn vị; 1: theo đối tượng,
	@MaDonVi nvarchar(max) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	--Declare ma phu cap --
	DECLARE @LstMaPhuCapBDN_D14N nvarchar(1000) = 'BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY';
	DECLARE @LstMaPhuCapBDN_T14N nvarchar(1000) = 'BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY';
	DECLARE @LstMaPhuCapOK_D14N nvarchar(1000) = 'OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY';
	DECLARE @LstMaPhuCapOK_T14N nvarchar(1000) = 'OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY';
	DECLARE @LstMaPhuCap_CON_OM nvarchar(1000) = 'CONOM_PCCVBH_TT,CONOM_PCTNBH_TT,CONOM_HSBLBH_TT,CONOM_LBH_TT,CONOM_PCTNVKBH_TT,CONOM';

	DECLARE @NameBDN_D14 nvarchar(1000) = N'Bệnh dài ngày - Dưới 14 ngày';
	DECLARE @NameBDN_T14 nvarchar(1000)=N'Bệnh dài ngày - Từ 14 ngày trở lên';
	DECLARE @NameOK_D14 nvarchar(1000)=N'Ốm khác - Dưới 14 ngày';
	DECLARE @NameOK_T14 nvarchar(1000)=N'Ốm khác - Từ 14 ngày trở lên';
	DECLARE @NameCON_OM nvarchar(1000)=N'Con ốm';


	CREATE TABLE #tempResult(STT nvarchar(6) ,TenChiTieu nvarchar(1000), MaCapBac nvarchar(50),MaCanBo nvarchar(50), TenCanBo  nvarchar(500), MaDonVi nvarchar(50), PCCVBH_TT numeric, PCTNBH_TT numeric, HSBLBH_TT numeric,  LBH_TT numeric, PCTNVKBH_TT numeric, Total numeric, LoaiDoiTuong nvarchar(50), rowNumber int)
    -- Insert statements for procedure here
	-- Bệnh dài ngày dứoi 14 ngày
	INSERT INTO #tempResult(STT, TenChiTieu, MaCapBac,MaCanBo, TenCanBo, MaDonVi, PCCVBH_TT, PCTNBH_TT, HSBLBH_TT, LBH_TT, PCTNVKBH_TT, Total, LoaiDoiTuong,rowNumber)
	SELECT CAST('1' as nvarchar(6)), @NameBDN_D14 , sMaCB, sMaCBo, sTenCbo,sMaDonVi,
	  BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY,
	  CASE
		WHEN sMaCB LIKE '1%' THEN 'SQ'
		WHEN sMaCB LIKE '2%' THEN 'QNCN'
		WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
		WHEN sMaCB IN ('3.1','3.2','3.3') THEN 'VCQP'
		WHEN sMaCB IN ('43','425','423','3.4') THEN 'LDHD'
		ELSE
			NULL
	 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN ('3.1','3.2','3.3') THEN 4
		WHEN sMaCB IN ('43','425','423','3.4') THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo)) and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapBDN_D14N)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  AND iIdMaDonVi IN (SELECT Id FROM TL_DM_DonVi WHERE Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi)))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY)  
	) AS PivotTable
	UNION
	-- Bệnh dài ngày trên 14 ngày
	SELECT  '2',@NameBDN_T14, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB IN ('43','425','423','3.4') THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB IN ('43','425','423','3.4') THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapBDN_T14N)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  AND iIdMaDonVi IN (SELECT Id FROM TL_DM_DonVi WHERE Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi)))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY)  
	) AS PivotTable
		UNION

	--Ốm khác dưới 14 ngày
	SELECT  '3',@NameOK_D14,  sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB IN ('43','425','423','3.4') THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB IN ('43','425','423','3.4') THEN 5
		ELSE
			NULL
	 END
	 FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapOK_D14N)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  AND iIdMaDonVi IN (SELECT Id FROM TL_DM_DonVi WHERE Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi)))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY)  
	) AS PivotTable
		UNION

	-- Ốm khác trên 14 ngày
	SELECT '4', @NameOK_T14, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB IN ('43','425','423','3.4') THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB IN ('43','425','423','3.4') THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapOK_T14N)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  AND iIdMaDonVi IN (SELECT Id FROM TL_DM_DonVi WHERE Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi)))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY)  
	) AS PivotTable

	UNION
	-- con ốm
	SELECT '5', @NameCON_OM, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			CONOM_PCCVBH_TT,CONOM_PCTNBH_TT,CONOM_HSBLBH_TT,CONOM_LBH_TT,CONOM_PCTNVKBH_TT,CONOM,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB IN ('43','425','423','3.4') THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB IN ('43','425','423','3.4') THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCap_CON_OM)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  AND iIdMaDonVi IN (SELECT Id FROM TL_DM_DonVi WHERE Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi)))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (CONOM_PCCVBH_TT,CONOM_PCTNBH_TT,CONOM_HSBLBH_TT,CONOM_LBH_TT,CONOM_PCTNVKBH_TT,CONOM)  
	) AS PivotTable;  


	INSERT INTO #tempResult(STT, TenChiTieu, MaCapBac,MaCanBo, TenCanBo, MaDonVi, PCCVBH_TT, PCTNBH_TT, HSBLBH_TT, LBH_TT, PCTNVKBH_TT, Total,LoaiDoiTuong,rowNumber)
	SELECT '0',TenCanBo,MaCapBac, MaCanBo,TenCanBo, MaDonVi, 
			SUM(ISNULL(PCCVBH_TT,0)) PCCVBH_TT,
			SUM(ISNULL(PCTNBH_TT,0)) PCTNBH_TT, 
			SUM(ISNULL(HSBLBH_TT,0)) HSBLBH_TT, 
			SUM(ISNULL(LBH_TT,0)) LBH_TT, 
			SUM(ISNULL(PCTNVKBH_TT,0)) PCTNVKBH_TT,
			SUM(ISNULL(Total,0)) Total,
			LoaiDoiTuong,
			rowNumber
	FROM #tempResult group by MaCanBo, TenCanBo, MaCapBac,MaDonVi, LoaiDoiTuong,rowNumber;
	--SELECT LEFT(MaCapBac, 1),* FROM #tempResult ORDER BY MaCanBo , STT;
	IF(@TypeOutPut = 2)
		BEGIN
		--Lấy Đơn vị
			SELECT 
				0 as Level,
				CAST (NULL as nvarchar(6)) STT,
				CAST (NULL as nvarchar(50)) LoaiDoiTuong,
				donvi.Ten_DonVi as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				0 as rowNumber
				INTO #tempDonVi
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> 0
				GROUP BY rs.MaDonVi, donvi.Ten_DonVi
				ORDER BY rs.MaDonVi;
			-- Lấy Loại đối tượng
			SELECT 
				1 Level,
	  			CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 'I'
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 'II'
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 'III'
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 'IV'
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 'V'
					ELSE
						NULL
				END AS nvarchar(6))  STT,
				rs.LoaiDoiTuong,
				rs.LoaiDoiTuong as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 1
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 2
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 3
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 4
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 5
					ELSE
						NULL
				END AS int)  rowNumber
				INTO #tempLoaiDoiTuong
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> '0'
				GROUP BY rs.MaDonVi,rs.LoaiDoiTuong,donvi.Ten_DonVi

			-- lấy chi tiết từng đối tượng
			SELECT 
				CASE
					WHEN STT = '0' THEN 2 
					ELSE 3
				END AS Level,
				STT,
				LoaiDoiTuong,
				TenChiTieu,
				MaCapBac, 
				MaCanBo,
				TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				ISNULL(PCCVBH_TT,0)/@DonViTinh PCCVBH_TT,
				ISNULL(PCTNBH_TT,0)/@DonViTinh PCTNBH_TT, 
				ISNULL(HSBLBH_TT,0)/@DonViTinh HSBLBH_TT, 
				ISNULL(LBH_TT,0)/@DonViTinh LBH_TT, 
				ISNULL(PCTNVKBH_TT,0)/@DonViTinh PCTNVKBH_TT,
				ISNULL(Total,0)/@DonViTinh Total,
				rs.rowNumber
				INTO #tempCanBo 
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi;

				--OUTPUT---
			SELECT rs.*  FROM
			(
			SELECT * FROM #tempDonVi
			UNION 
			SELECT * FROM #tempLoaiDoiTuong
			UNION
			SELECT * FROM #tempCanBo
			) rs
			ORDER BY rs.MaDonVi,rs.rowNumber,rs.MaCanBo

			DROP TABLE #tempDonVi;
			DROP TABLE #tempLoaiDoiTuong;
			DROP TABLE #tempCanBo;
		END
	ELSE
		BEGIN 
-- Lấy Loại đối tượng
			SELECT 
				1 Level,
	  			CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 'I'
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 'II'
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 'III'
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 'IV'
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 'V'
					ELSE
						NULL
				END AS nvarchar(6))  STT,
				rs.LoaiDoiTuong,
				rs.LoaiDoiTuong as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				CAST (NULL as nvarchar(50)) MaDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 1
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 2
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 3
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 4
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 5
					ELSE
						NULL
				END AS int)  rowNumber
				INTO #tempLoaiDoiTuong2
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> '0'
				GROUP BY rs.LoaiDoiTuong

			-- lấy chi tiết từng đối tượng
			SELECT 
				CASE
					WHEN STT = '0' THEN 2 
					ELSE 3
				END AS Level,
				STT,
				LoaiDoiTuong,
				TenChiTieu,
				MaCapBac, 
				MaCanBo,
				TenCanBo,
				CASE
					WHEN STT = '0' THEN rs.MaDonVi 
					ELSE NULL
				END AS MaDonVi,
				ISNULL(PCCVBH_TT,0)/@DonViTinh PCCVBH_TT,
				ISNULL(PCTNBH_TT,0)/@DonViTinh PCTNBH_TT, 
				ISNULL(HSBLBH_TT,0)/@DonViTinh HSBLBH_TT, 
				ISNULL(LBH_TT,0)/@DonViTinh LBH_TT, 
				ISNULL(PCTNVKBH_TT,0)/@DonViTinh PCTNVKBH_TT,
				ISNULL(Total,0)/@DonViTinh Total,
				rs.rowNumber
				INTO #tempCanBo2 
				FROM #tempResult rs

				--OUTPUT---
			SELECT KQ.* INTO #resuld_tbl FROM
			(
			SELECT rs.*,donvi.Ten_DonVi as TenDonVi  FROM
			(
			SELECT * FROM #tempLoaiDoiTuong2
			UNION
			SELECT * FROM #tempCanBo2
			) rs
			LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
			) KQ

			UPDATE #resuld_tbl SET Total = ISNULL(PCCVBH_TT, 0) + ISNULL(PCTNBH_TT, 0) + ISNULL(HSBLBH_TT, 0) + ISNULL(LBH_TT, 0) + ISNULL(PCTNVKBH_TT, 0)

			SELECT * FROM #resuld_tbl ORDER BY rowNumber, MaCanBo

			DROP TABLE #tempLoaiDoiTuong2;
			DROP TABLE #tempCanBo2;	
			DROP TABLE #resuld_tbl;	
		END
	DROP TABLE #tempResult;
END
;
;
;
;
GO


update TL_BangLuong_ThangBHXH
set iIdMaDonVi = (select top 1 Id from TL_DM_DonVi dv where dv.Ma_DonVi = sMaDonVi and dv.iTrangThai = 1)
where iID_Parent in (
select Id from TL_DS_CapNhap_BangLuong
where isnull(STongHop, '') = '')
and iIdMaDonVi is null;

GO


/****** Object:  StoredProcedure [dbo].[sp_bhxh_qttm_get_data_quy]    Script Date: 7/29/2024 9:40:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_qttm_get_data_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_qttm_get_data_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 7/29/2024 9:40:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 7/29/2024 9:40:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfWork int,
	@UserName nvarchar(100)
AS
BEGIN
	INSERT INTO BH_QTTM_BHYT_Chung_Tu_ChiTiet
           (iID_QTTM_BHYT_ChungTu,
		  fDuToan,
		  fDaQuyetToan,
		  fConLai,
		  fSoPhaiThu,
		  sGhiChu,
		  iID_MLNS,
		  iID_MLNS_Cha,
		  sXauNoiMa,
		  sLNS)
    SELECT 
			@VoucherId,
			Sum(isnull(ctct.fDuToan, 0)),
			Sum(isnull(ctct.fDaQuyetToan, 0)),
			Sum(isnull(ctct.fConLai, 0)),
			Sum(isnull(ctct.fSoPhaiThu, 0)),
			null,
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS
	FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	JOIN BH_DM_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa
	WHERE ctct.iID_QTTM_BHYT_ChungTu IN (SELECT * FROM f_split(@VoucherIds))
	AND mlns.iNamLamViec = @YearOfWork
	GROUP BY mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS;

	-- Danh dau chung tu da tong hop
	UPDATE BH_QTTM_BHYT_Chung_Tu SET bDaTongHop = 1 
	WHERE iID_QTTM_BHYT_ChungTu in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qttm_get_data_quy]    Script Date: 7/29/2024 9:40:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_bhxh_qttm_get_data_quy] 
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX)
AS
BEGIN
	
	select
		ctct.sXauNoiMa,
		ct.iID_MaDonVi,
		isnull(sum(ctct.fDaQuyetToan), 0) fDaQuyetToan,
		isnull(sum(ctct.fConLai), 0) fConLai,
		isnull(sum(ctct.fSoPhaiThu), 0) fSoPhaiThu
	from BH_QTTM_BHYT_Chung_Tu ct
	join BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu =  ctct.iID_QTTM_BHYT_ChungTu
	where ct.iNamLamViec = @INamLamViec
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and  ct.iQuyNamLoai <> 2
		and ct.bIsKhoa = 1
	group by ctct.sXauNoiMa, ct.iID_MaDonVi, ct.iQuyNamLoai

END
GO

DELETE FROM TL_DM_CapBac WHERE Ma_Cb = '3.4';
GO
INSERT INTO [dbo].[TL_DM_CapBac] ([Id], [Bhcs_Cq], [Bhtn_Cq], [Bhxh_Cq], [Bhyt_Cq], [Hs_Bhcs], [Hs_Bhtn], [Hs_Bhxh], [Hs_Bhyt], [Hs_Kpcd], [Kpcd_Cq], [Lht_Hs], [Ma_Cb], [Note], [Parent], [PhuCapRaQuan], [Readonly], [Splits], [Ten_Cb], [TiLeHuong], [XauNoiMa], [HS_TroCapOmDau]) 
VALUES (NEWID(), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'3.4', N'Hợp đồng lao động', N'3', CAST(0 AS Numeric(16, 0)), NULL, 1, N'Hợp đồng lao động', CAST(1.00 AS Numeric(5, 2)), N'3-3.3', CAST(0.7500 AS Numeric(15, 4)))
GO
Update [dbo].[TL_DM_CapBac] set Parent='3.4' where Ma_Cb in ('423','425','43');
GO
Update [dbo].[TL_DM_CapBac] set Note = N'Công nhân viên chức quốc phòng & HĐLĐ', Ten_Cb=N'Công nhân viên chức quốc phòng & HĐLĐ' where Ma_Cb='3';
GO