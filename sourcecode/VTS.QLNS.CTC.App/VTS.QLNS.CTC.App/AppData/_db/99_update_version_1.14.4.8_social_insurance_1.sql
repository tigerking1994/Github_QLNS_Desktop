/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 5/17/2024 5:40:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 5/17/2024 5:40:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyet_toan_chitiet_bhxh]    Script Date: 5/17/2024 5:40:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyet_toan_chitiet_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyet_toan_chitiet_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]    Script Date: 5/17/2024 5:40:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]    Script Date: 5/17/2024 5:40:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]    Script Date: 5/17/2024 5:40:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 5/17/2024 5:40:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 5/17/2024 5:40:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 5/17/2024 5:40:24 PM ******/
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
	where isnull(ctct.MaCachTl,'') = ''
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
	  hsbl.fGiaTri FGiaTriHSBL
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
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL
	 into tbl_cancu_result_final_y
	 from tbl_cancu_result_y cancu
	 group by cancu.XauNoiMa, cancu.iKhoi

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
	  luong.XauNoiMa,
	  luong.IQSBQNam,
	  luong.iKhoi,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 5/17/2024 5:40:24 PM ******/
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
	where isnull(ctct.MaCachTl,'') = ''
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
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]    Script Date: 5/17/2024 5:40:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.fSoNgayHuongBHXH, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_CheDoBHXH as pc on cb.Ma_CanBo = pc.sMaCanBo
	INNER JOIN #tmpMapping as mp on pc.sMaCheDo = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.sMaCheDo			AS MaPhuCap,
		bangLuong.sMaCB				AS MaCapBac,
		case when bangLuong.sMaCB in ('3.1', '3.2', '3.3', '3.4', '3.5', '413', '415') then bangLuong.sMaCB else capBac.Parent end AS Ngach,
		--SUM(case when ISNULL(cbpc.HuongPC_SN, 0) = 0 then dbo.fnTotalDayOfMonth(@thang,@nam) else cbpc.HuongPC_SN end) AS SoNgay,
		SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
		SUM(bangLuong.nGiaTri) AS GiaTri,
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END)	AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_ThangBHXH WHERE iNam = @nam AND iThang = @thang AND nGiaTri != 0) bangLuong
	INNER JOIN TL_CanBo_CheDoBHXH as cbpc1 on bangLuong.sMaCBo = cbpc1.sMaCanBo AND bangLuong.sMaCheDo = cbpc1.sMaCheDo
	INNER JOIN TL_DM_CheDoBHXH as pc on bangLuong.sMaCheDo = pc.sMaCheDo
	LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.sMaCheDo = cbpc.Ma_Cot AND bangLuong.sMaCBo = cbpc.Ma_CanBo
	JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong WHERE NAM = @Nam AND THANG = @Thang AND Ma_CachTL IN (SELECT * FROM f_split(@maCachTl)) AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
	ON bangLuong.iID_Parent = dsCapNhapBangLuong.Id AND bangLuong.iThang = dsCapNhapBangLuong.Thang and bangLuong.iNam = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac capBac ON bangLuong.sMaCB = capBac.Ma_Cb		
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.sMaCheDo,bangLuong.sMaCB, capBac.Parent
		
	--Biay Nam chot sua
	update #LuongCapBac set Ngach = '3.3' where MaCapBac in ('413', '415');

	SELECT distinct
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTuChiTiet ctchitiet
	Join TL_QT_ChungTu chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		sXauNoiMa						AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
		AND luong.Thang = @thang
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]    Script Date: 5/17/2024 5:40:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	DECLARE @thangTruoc INT;
	DECLARE @namTruoc INT;

	IF @thang = 1
		SET @thangTruoc = 12;
		SET @namTruoc = @nam - 1;
	IF @thang <> 1
		SET @thangTruoc = @thang - 1;
		SET @namTruoc = @nam;

	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);

	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.fSoNgayHuongBHXH, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_CheDoBHXH as pc on cb.Ma_CanBo = pc.sMaCanBo
	INNER JOIN #tmpMapping as mp on pc.sMaCheDo = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.MA_PHUCAP			AS MaPhuCap,
		bangLuong.Ma_CB				AS MaCapBac,
		case when bangLuong.Ma_CB in ('3.1', '3.2', '3.3', '3.4', '3.5', '413', '415') then bangLuong.Ma_CB else capBac.Parent end AS Ngach,
		SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
		SUM(bangLuong.Gia_Tri) AS GiaTri,
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END) AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang WHERE NAM = @namTruoc AND THANG = @thangTruoc AND Gia_Tri != 0 AND Ma_PhuCap in ('LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')) bangLuong
	INNER JOIN (select * from TL_CanBo_CheDoBHXH where inam = @nam and  ithang = @thang and sMaCheDo in ('OMKHAC_T14NGAY', 'BENHDAINGAY_T14NGAY','CONOM', 'KHAMTHAI', 'KHHGD', 'NAMNGHIKHIVOSINHCON')) as cbpc1 on bangLuong.Ma_Hieu_CanBo = SUBSTRING(cbpc1.sMaCanBo, 7, 10)
	LEFT JOIN #tmpSoNgay as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong WHERE Status = 1 AND NAM = @namTruoc AND THANG = @thangTruoc AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
	ON bangLuong.parent = dsCapNhapBangLuong.Id AND bangLuong.THANG = dsCapNhapBangLuong.Thang and bangLuong.NAM = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac capBac ON bangLuong.Ma_CB = capBac.Ma_Cb		
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
		
	SELECT distinct
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTuChiTiet ctchitiet
	Join TL_QT_ChungTu chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		luong.XauNoiMa					AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
		AND luong.Thang = @thangTruoc
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, luong.XauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, luong.XauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]    Script Date: 5/17/2024 5:40:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.fSoNgayHuongBHXH, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_CheDoBHXH as pc on cb.Ma_CanBo = pc.sMaCanBo
	INNER JOIN #tmpMapping as mp on pc.sMaCheDo = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.sMaCheDo			AS MaPhuCap,
		bangLuong.sMaCB				AS MaCapBac,
		case when bangLuong.sMaCB in ('3.1', '3.2', '3.3', '3.4', '3.5', '413', '415') then bangLuong.sMaCB else capBac.Parent end AS Ngach,
		SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
		SUM(bangLuong.nGiaTri) AS GiaTri,
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END)	AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_ThangBHXH WHERE iNam = @nam AND iThang = @thang AND nGiaTri != 0) bangLuong
	INNER JOIN (select * from TL_CanBo_CheDoBHXH where iNam = @nam and iThang = @thang
		and sMaCheDo in ('BDN_D14N_LBH_TT','BDN_D14N_PCCVBH_TT','BDN_D14N_PCTNBH_TT','BDN_D14N_PCTNVKBH_TT','BDN_D14N_HSBLBH_TT','OK_D14N_LBH_TT','OK_D14N_PCCVBH_TT','OK_D14N_PCTNBH_TT','OK_D14N_PCTNVKBH_TT','OK_D14N_HSBLBH_TT')) cbpc1 
	on bangLuong.sMaCBo = cbpc1.sMaCanBo AND bangLuong.sMaCheDo = cbpc1.sMaCheDo
	INNER JOIN TL_DM_CheDoBHXH as pc on bangLuong.sMaCheDo = pc.sMaCheDo
	LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.sMaCheDo = cbpc.Ma_Cot AND bangLuong.sMaCBo = cbpc.Ma_CanBo
	JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong WHERE NAM = @Nam AND THANG = @Thang AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
	ON bangLuong.iID_Parent = dsCapNhapBangLuong.Id AND bangLuong.iThang = dsCapNhapBangLuong.Thang and bangLuong.iNam = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac capBac ON bangLuong.sMaCB = capBac.Ma_Cb		
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.sMaCheDo,bangLuong.sMaCB, capBac.Parent
		
	SELECT distinct
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTuChiTiet ctchitiet
	Join TL_QT_ChungTu chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		sXauNoiMa						AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
		AND luong.Thang = @thang
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyet_toan_chitiet_bhxh]    Script Date: 5/17/2024 5:40:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_quyet_toan_chitiet_bhxh] 
	@MaDonVi VARCHAR(MAX),
	@Thang INT,
	@Nam INT
AS
BEGIN
	select 
		ct.Ma_DonVi MaDonVi,
		ct.Nam INam, 
		ct.Thang IThang,
		ctct.XauNoiMa,
		ctct.MaCb,
		ctct.MoTa,
		ctct.DieuChinh,
		ctct.TongCong,
		ctct.SoNgay,
		ctct.SoNguoi,
		ctct.MaCachTl
	from 
	TL_QT_ChungTu ct
	join TL_QT_ChungTuChiTiet ctct on ct.ID = ctct.Id_ChungTu
	where ct.Nam = @Nam
		and ct.Thang = @Thang
		and ct.Ma_DonVi = @maDonVi
		and ctct.MaCachTl = 'CACH2'
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 5/17/2024 5:40:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int

IF @Thang = 1 
BEGIN
	SET @ThangTruoc = 12;
	SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'PCNU_TT,PCCOV_HS,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT * FROM TL_BangLuong_Thang
	WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTNVK_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''HSBL_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
--canBo.NTN,
canBo.XauNoiMa
into tbl_luong_bhxh
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)

UPDATE tbl_luong_bhxh SET 
PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) * isnull(PCCOV_HS, 0)

UPDATE tbl_luong_bhxh SET
LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0) + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

select * from tbl_luong_bhxh;

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_bhxh]') AND type in (N'U')) drop table tbl_luong_bhxh;

END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 5/17/2024 5:40:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'PCNU_TT,PCCOV_HS,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH 
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTNVK_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''HSBL_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM TL_BangLuong_Thang bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
into tbl_luong_bhxh_2
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)

UPDATE tbl_luong_bhxh_2 SET 
PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) * isnull(PCCOV_HS, 0)

UPDATE tbl_luong_bhxh_2 SET
LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0) + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)


select * from tbl_luong_bhxh_2;

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_bhxh_2]') AND type in (N'U')) drop table tbl_luong_bhxh_2;

END
;
;
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 5/20/2024 11:21:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 5/20/2024 11:21:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld] 
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD]') AND type in (N'U'))
	drop table TBL_TCTNLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_DOC]') AND type in (N'U'))
	drop table TBL_TCTNLD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_SQ]') AND type in (N'U'))
	drop table TBL_TCTNLD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_QNCN]') AND type in (N'U'))
	drop table TBL_TCTNLD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTNLD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_VCQP]') AND type in (N'U'))
	drop table TBL_TCTNLD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_LDHD]') AND type in (N'U'))
	drop table TBL_TCTNLD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_RESULT]') AND type in (N'U'))
	drop table TBL_TCTNLD_RESULT;

	DECLARE @MaCheDo nvarchar(1000) = 'CHIGIAMDINH,TAINANLD_TROCAP1LAN,TROCAPTHEOPHIEUTRUYTRA,TROCAPHANGTHANG,TROCAPPHCN,TROCAPPHUCVU,TROCAPCHETDOTNLD,TAINANLD_DUONGSUCPHSK,CHIGIAMDINH_TRUYLINH,TNLD_TROCAP1LAN_TRUYLINH,TROCAPHANGTHANG_TRUYLINH,TROCAPPHCN_TRUYLINH,TROCAPPHUCVU_TRUYLINH,TROCAPCHETDOTNLD_TRUYLINH,TNLD_DUONGSUCPHSK_TRUYLINH,HOTRO_CDNN_TRUYLINH,HOTRO_PHONGNGUA_TRUYLINH,HOTRO_CDNN,HOTRO_PHONGNGUA'
	--Lay thong tin luong theo tro cap tai nan lao dong
	select * into TBL_TCTNLD from
	(select donvi.Ma_DonVi, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in (select * from splitstring(@MaCheDo))) tctnld

	select distinct 
		TBL_TCTNLD.sMaCB,
		TBL_TCTNLD.sMaCBo,
		--TBL_TCTNLD.sMaCheDo,
		TBL_TCTNLD.sTenCbo,
		TBL_TCTNLD.Ma_DonVi,
		TBL_TCTNLD.Ten_DonVi,
		CHIGIAMDINH.nGiaTri fChiGiamDinh,
		TROCAP1LAN.nGiaTri fTroCap1Lan,
		TROCAPTHEOPHIEUTRUYTRA.nGiaTri fTroCapTheoPhieuTruyTra,
		TROCAPHANGTHANG.nGiaTri fTroCapHangThang,
		TROCAPPHCN.nGiaTri fTroCapPHCN,
		HOTROCDNN.nGiaTri fHoTroCdnn,
		HOTROPHONGNGUA.nGiaTri fHoTroPhongNgua,
		TROCAPCHETDOTNLD.nGiaTri fTroCapChetDoTNLD,
		TAINANLD_DUONGSUCPHSK.SoNgayDuongSucTNLD,
		TAINANLD_DUONGSUCPHSK.nGiaTri fDuongSucTNLD,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh,
		CHIGIAMDINH_TL.nGiaTri fChiGiamDinhTruyLinh,
		TROCAP1LAN_TL.nGiaTri fTroCap1LanTruyLinh,
		HOTRO_CDNN_TRUYLINH_TL.nGiaTri fHoTroCdnnTruyLinh,
		HOTRO_PHONGNGUA_TRUYLINH_TL.nGiaTri fHoTroPhongNguaTruyLinh,
		TROCAPHANGTHANG_TL.nGiaTri fTroCapHangThangTruyLinh,
		TROCAPPHCN_TL.nGiaTri fTroCapPHCNTruyLinh,
		TROCAPCHETDOTNLD_TL.nGiaTri fTroCapChetDoTNLDTruyLinh,
		TAINANLD_DUONGSUCPHSK_TL.SoNgayDuongSucTNLD SoNgayDuongSucTNLDTruyLinh,
		TAINANLD_DUONGSUCPHSK_TL.nGiaTri fDuongSucTNLDTruyLinh
		into TBL_TCTNLD_DOC
	from TBL_TCTNLD TBL_TCTNLD
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in (select * from splitstring(@MaCheDo))
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_TCTNLD.sMaCBo = chedocha.sMaCanBo
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'TROCAPTHEOPHIEUTRUYTRA') TROCAPTHEOPHIEUTRUYTRA
		on TBL_TCTNLD.sMaCBo = TROCAPTHEOPHIEUTRUYTRA.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPTHEOPHIEUTRUYTRA.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG') TROCAPHANGTHANG
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN', 'TROCAPPHUCVU')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN.sMaDonVi
		left join
		(select tnld_3_1.sMaDonVi, sum(tnld_3_1.nGiaTri) nGiaTri, tnld_3_1.sMaCB, tnld_3_1.sMaCBo, tnld_3_1.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayHOTROCDNN
		from TBL_TCTNLD tnld_3_1 left join TL_CanBo_CheDoBHXH chedo on tnld_3_1.sMaCBo = chedo.sMaCanBo and tnld_3_1.sTenCbo = chedo.sMaCheDo
		where tnld_3_1.sMaCheDo in ('HOTRO_CDNN')
		group by tnld_3_1.sMaDonVi, tnld_3_1.sMaCB, tnld_3_1.sMaCBo, tnld_3_1.sTenCbo) HOTROCDNN
		on TBL_TCTNLD.sMaCBo = HOTROCDNN.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTROCDNN.sMaDonVi
		left join
		(select tnld_3_2.sMaDonVi, sum(tnld_3_2.nGiaTri) nGiaTri, tnld_3_2.sMaCB, tnld_3_2.sMaCBo, tnld_3_2.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayHOTROPHONGNGUA
		from TBL_TCTNLD tnld_3_2 left join TL_CanBo_CheDoBHXH chedo on tnld_3_2.sMaCBo = chedo.sMaCanBo and tnld_3_2.sTenCbo = chedo.sMaCheDo
		where tnld_3_2.sMaCheDo in ('HOTRO_PHONGNGUA')
		group by tnld_3_2.sMaDonVi, tnld_3_2.sMaCB, tnld_3_2.sMaCBo, tnld_3_2.sTenCbo) HOTROPHONGNGUA
		on TBL_TCTNLD.sMaCBo = HOTROPHONGNGUA.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTROPHONGNGUA.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD') TROCAPCHETDOTNLD
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLD_DUONGSUCPHSK') TAINANLD_DUONGSUCPHSK
		on TBL_TCTNLD.sMaCBo = TAINANLD_DUONGSUCPHSK.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLD_DUONGSUCPHSK.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH') CHIGIAMDINH
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH.sMaDonVi

		-- TRUYLINH
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TNLD_TROCAP1LAN_TRUYLINH') TROCAP1LAN_TL
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN_TL.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'HOTRO_CDNN_TRUYLINH') HOTRO_CDNN_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_CDNN_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_CDNN_TRUYLINH_TL.sMaDonVi
		left join
		(select tnld_pn.sMaDonVi, tnld_pn.nGiaTri, tnld_pn.sMaCB, tnld_pn.sMaCBo, tnld_pn.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayHoTroPhongNgua
		from TBL_TCTNLD tnld_pn left join TL_CanBo_CheDoBHXH chedo on tnld_pn.sMaCBo = chedo.sMaCanBo and tnld_pn.sMaCheDo = chedo.sMaCheDo
		where tnld_pn.sMaCheDo = 'HOTRO_PHONGNGUA_TRUYLINH') HOTRO_PHONGNGUA_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_PHONGNGUA_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_PHONGNGUA_TRUYLINH_TL.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG_TRUYLINH') TROCAPHANGTHANG_TL
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG_TL.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN_TRUYLINH', 'TROCAPPHUCVU_TRUYLINH')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN_TL
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN_TL.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD_TRUYLINH') TROCAPCHETDOTNLD_TL
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD_TL.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TNLD_DUONGSUCPHSK_TRUYLINH ') TAINANLD_DUONGSUCPHSK_TL
		on TBL_TCTNLD.sMaCBo = TAINANLD_DUONGSUCPHSK_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLD_DUONGSUCPHSK_TL.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH_TRUYLINH') CHIGIAMDINH_TL
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH_TL.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTNLD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '1%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_SQ) > 1
		update TBL_TCTNLD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTNLD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '2%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_QNCN) > 1
		update TBL_TCTNLD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTNLD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '0%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		)  hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_HSQBS) > 1
		update TBL_TCTNLD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTNLD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_VCQP) > 1
		update TBL_TCTNLD_VCQP set bHasData = 1

	--Lấy lương Lao Dộng hợp Dông
	select * into TBL_TCTNLD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('43','423','425') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		)  ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_LDHD) > 1
		update TBL_TCTNLD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTNLD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_LDHD) result

	select 
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		Ma_DonVi MaDonVi,
		TenDonVi,
		SoNgayDuongSucTNLD,
		fChiGiamDinh/@DonViTinh fChiGiamDinh,
		fTroCap1Lan/@DonViTinh fTroCap1Lan,
		fTroCapTheoPhieuTruyTra/@DonViTinh fTroCapTheoPhieuTruyTra,
		fTroCapHangThang/@DonViTinh fTroCapHangThang,
		fTroCapPHCN/@DonViTinh fTroCapPHCN,
		fHoTroCdnn/@DonViTinh fHoTroCdnn,
		fHoTroPhongNgua/@DonViTinh fHoTroPhongNgua,
		fTroCapChetDoTNLD/@DonViTinh fTroCapChetDoTNLD,
		fDuongSucTNLD/@DonViTinh fDuongSucTNLD,
		fTongSoTienThangNay/@DonViTinh fTongSoTienThangNay,
		bHangCha IsHangCha,
		bHasData IsHasData,
		SoNgayDuongSucTNLDTruyLinh,
		fChiGiamDinhTruyLinh/@DonViTinh fChiGiamDinhTruyLinh,
		fTroCap1LanTruyLinh/@DonViTinh fTroCap1LanTruyLinh,
		fHoTroCdnnTruyLinh/@DonViTinh fHoTroCdnnTruyLinh,
		fHoTroPhongNguaTruyLinh/@DonViTinh fHoTroPhongNguaTruyLinh,
		fTroCapHangThangTruyLinh/@DonViTinh fTroCapHangThangTruyLinh,
		fTroCapPHCNTruyLinh/@DonViTinh fTroCapPHCNTruyLinh,
		fTroCapChetDoTNLDTruyLinh/@DonViTinh fTroCapChetDoTNLDTruyLinh,
		fDuongSucTNLDTruyLinh/@DonViTinh fDuongSucTNLDTruyLinh,
		fTongSoTienTruyLinh/@DonViTinh fTongSoTienTruyLinh,
		fTongSoTien/@DonViTinh fTongSoTien

	from TBL_TCTNLD_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD]') AND type in (N'U'))
	drop table TBL_TCTNLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_DOC]') AND type in (N'U'))
	drop table TBL_TCTNLD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_SQ]') AND type in (N'U'))
	drop table TBL_TCTNLD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_QNCN]') AND type in (N'U'))
	drop table TBL_TCTNLD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTNLD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_VCQP]') AND type in (N'U'))
	drop table TBL_TCTNLD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_LDHD]') AND type in (N'U'))
	drop table TBL_TCTNLD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_RESULT]') AND type in (N'U'))
	drop table TBL_TCTNLD_RESULT;

END
;
;
;
;
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 5/20/2024 2:15:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 5/20/2024 2:15:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
	@ListIdChungTuTongHop ntext, 
	@Nguoitao nvarchar(50), 
	@IdChungTu nvarchar(100), 
	@NamLamViec int,
	@MaDonVi NVARCHAR(255)
AS 
BEGIN 

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--Lấy data dự toán đầu năm
	SELECT 
		dtct.iID_MLNS, dtct.sMoTa, dtct.sXauNoiMa,
		sum(isnull(dtct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dtct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dtct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dtct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dtct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dtct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,dtct.iNamLamViec
	INTO #DuToanDauNam
	FROM BH_DTT_BHXH_ChungTu pb
	JOIN BH_DTT_BHXH_ChungTu_ChiTiet dtct ON pb.iID_DTT_BHXH = dtct.iID_DTT_BHXH
	WHERE
		 dtct.iID_MaDonVi = @MaDonVi
		 and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> ''
		 and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		 and pb.iNamLamViec = @NamLamViec
	GROUP BY
	  dtct.iID_MLNS, 
	  dtct.sMoTa,
	  dtct.sXauNoiMa,
	  dtct.iNamLamViec

	--Tổng hợp dữ liệu điều chỉnh
	SELECT 
		dcct.sLNS, dcct.sNoiDung, dcct.sXauNoiMa,
		sum(isnull(dcct.fThuBHXH_NLD_QTDauNam, 0)) fThuBHXH_NLD_QTDauNam,
		sum(isnull(dcct.fThuBHXH_NSD_QTDauNam, 0)) fThuBHXH_NSD_QTDauNam, 
		sum(isnull(dcct.fThuBHYT_NLD_QTDauNam, 0)) fThuBHYT_NLD_QTDauNam, 
		sum(isnull(dcct.fThuBHYT_NSD_QTDauNam, 0)) fThuBHYT_NSD_QTDauNam, 
		sum(isnull(dcct.fThuBHTN_NLD_QTDauNam, 0)) fThuBHTN_NLD_QTDauNam, 
		sum(isnull(dcct.fThuBHTN_NSD_QTDauNam, 0)) fThuBHTN_NSD_QTDauNam,
		sum(isnull(dcct.fThuBHXH_NLD_QTCuoiNam, 0)) fThuBHXH_NLD_QTCuoiNam, 
		sum(isnull(dcct.fThuBHXH_NSD_QTCuoiNam, 0)) fThuBHXH_NSD_QTCuoiNam, 
		sum(isnull(dcct.fThuBHYT_NLD_QTCuoiNam, 0)) fThuBHYT_NLD_QTCuoiNam, 
		sum(isnull(dcct.fThuBHYT_NSD_QTCuoiNam, 0)) fThuBHYT_NSD_QTCuoiNam, 
		sum(isnull(dcct.fThuBHTN_NLD_QTCuoiNam, 0)) fThuBHTN_NLD_QTCuoiNam, 
		sum(isnull(dcct.fThuBHTN_NSD_QTCuoiNam, 0)) fThuBHTN_NSD_QTCuoiNam,
		sum(isnull(dcct.fTongThuBHXH_NLD, 0)) fTongThuBHXH_NLD, 
		sum(isnull(dcct.fTongThuBHXH_NSD, 0)) fTongThuBHXH_NSD, 
		sum(isnull(dcct.fTongThuBHYT_NLD, 0)) fTongThuBHYT_NLD, 
		sum(isnull(dcct.fTongThuBHYT_NSD, 0)) fTongThuBHYT_NSD, 
		sum(isnull(dcct.fTongThuBHTN_NLD, 0)) fTongThuBHTN_NLD, 
		sum(isnull(dcct.fTongThuBHTN_NSD, 0)) fTongThuBHTN_NSD,
		sum(isnull(dcct.fThuBHXH_NLD_Tang, 0)) fThuBHXH_NLD_Tang, 
		sum(isnull(dcct.fThuBHXH_NSD_Tang, 0)) fThuBHXH_NSD_Tang, 
		sum(isnull(dcct.fThuBHXH_Tang, 0)) fThuBHXH_Tang, 
		sum(isnull(dcct.fThuBHYT_NLD_Tang, 0)) fThuBHYT_NLD_Tang, 
		sum(isnull(dcct.fThuBHYT_NSD_Tang, 0)) fThuBHYT_NSD_Tang, 
		sum(isnull(dcct.fThuBHYT_Tang, 0)) fThuBHYT_Tang, 
		sum(isnull(dcct.fThuBHTN_NLD_Tang, 0)) fThuBHTN_NLD_Tang, 
		sum(isnull(dcct.fThuBHTN_NSD_Tang, 0)) fThuBHTN_NSD_Tang, 
		sum(isnull(dcct.fThuBHTN_Tang, 0)) fThuBHTN_Tang,
		sum(isnull(dcct.fThuBHXH_NLD_Giam, 0)) fThuBHXH_NLD_Giam, 
		sum(isnull(dcct.fThuBHXH_NSD_Giam, 0)) fThuBHXH_NSD_Giam, 
		sum(isnull(dcct.fThuBHXH_Giam, 0)) fThuBHXH_Giam, 
		sum(isnull(dcct.fThuBHYT_NLD_Giam, 0)) fThuBHYT_NLD_Giam, 
		sum(isnull(dcct.fThuBHYT_NSD_Giam, 0)) fThuBHYT_NSD_Giam, 
		sum(isnull(dcct.fThuBHYT_Giam, 0)) fThuBHYT_Giam, 
		sum(isnull(dcct.fThuBHTN_NLD_Giam, 0)) fThuBHTN_NLD_Giam, 
		sum(isnull(dcct.fThuBHTN_NSD_Giam, 0)) fThuBHTN_NSD_Giam, 
		sum(isnull(dcct.fThuBHTN_Giam, 0)) fThuBHTN_Giam,
		sum(isnull(dcct.fTongCong, 0)) fTongCong
	INTO #DataDieuChinh
	FROM 
	BH_DTT_BHXH_DieuChinh_ChiTiet dcct 
	WHERE dcct.iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop)) 
		AND dcct.iNamLamViec = @NamLamViec
	GROUP BY 
	  dcct.sLNS,
	  dcct.sNoiDung,
	  dcct.sXauNoiMa

	--Insert dữ liệu chi tiết tổng hợp
	INSERT INTO [dbo].BH_DTT_BHXH_DieuChinh_ChiTiet (
    iID_DTT_BHXH_DieuChinh_ChiTiet, iID_DTT_BHXH_DieuChinh, iID_MucLucNganSach, sLNS, sNoiDung, sXauNoiMa,
	fThuBHXH_NLD, fThuBHXH_NSD, fThuBHYT_NLD, fThuBHYT_NSD, fThuBHTN_NLD, fThuBHTN_NSD,
	fThuBHXH_NLD_QTDauNam, fThuBHXH_NSD_QTDauNam, fThuBHYT_NLD_QTDauNam, fThuBHYT_NSD_QTDauNam, fThuBHTN_NLD_QTDauNam, fThuBHTN_NSD_QTDauNam,
	fThuBHXH_NLD_QTCuoiNam, fThuBHXH_NSD_QTCuoiNam, fThuBHYT_NLD_QTCuoiNam, fThuBHYT_NSD_QTCuoiNam, fThuBHTN_NLD_QTCuoiNam, fThuBHTN_NSD_QTCuoiNam,
	fTongThuBHXH_NLD, fTongThuBHXH_NSD, fTongThuBHYT_NLD, fTongThuBHYT_NSD, fTongThuBHTN_NLD, fTongThuBHTN_NSD,
	fThuBHXH_NLD_Tang, fThuBHXH_NSD_Tang, fThuBHXH_Tang, fThuBHYT_NLD_Tang, fThuBHYT_NSD_Tang, fThuBHYT_Tang, fThuBHTN_NLD_Tang, fThuBHTN_NSD_Tang, fThuBHTN_Tang,
	fThuBHXH_NLD_Giam, fThuBHXH_NSD_Giam, fThuBHXH_Giam, fThuBHYT_NLD_Giam, fThuBHYT_NSD_Giam, fThuBHYT_Giam, fThuBHTN_NLD_Giam, fThuBHTN_NSD_Giam, fThuBHTN_Giam, fTongCong,
    dNgaySua, dNgayTao, sNguoiSua, sNguoiTao)

	select NEWID(), @idChungTu,
	    dt.iID_MLNS, dc.sLNS, dc.sNoiDung, dt.sXauNoiMa,
	    dt.fThu_BHXH_NLD,
	    dt.fThu_BHXH_NSD,
	    dt.fThu_BHYT_NLD,
	    dt.fThu_BHYT_NSD,
	    dt.fThu_BHTN_NLD,
	    dt.fThu_BHTN_NSD,
	    dc.fThuBHXH_NLD_QTDauNam,
		dc.fThuBHXH_NSD_QTDauNam, 
		dc.fThuBHYT_NLD_QTDauNam, 
		dc.fThuBHYT_NSD_QTDauNam, 
		dc.fThuBHTN_NLD_QTDauNam, 
		dc.fThuBHTN_NSD_QTDauNam,
		dc.fThuBHXH_NLD_QTCuoiNam, 
		dc.fThuBHXH_NSD_QTCuoiNam, 
		dc.fThuBHYT_NLD_QTCuoiNam, 
		dc.fThuBHYT_NSD_QTCuoiNam, 
		dc.fThuBHTN_NLD_QTCuoiNam, 
		dc.fThuBHTN_NSD_QTCuoiNam,
		dc.fTongThuBHXH_NLD, 
		dc.fTongThuBHXH_NSD, 
		dc.fTongThuBHYT_NLD, 
		dc.fTongThuBHYT_NSD, 
		dc.fTongThuBHTN_NLD, 
		dc.fTongThuBHTN_NSD,
		dc.fThuBHXH_NLD_Tang, 
		dc.fThuBHXH_NSD_Tang, 
		dc.fThuBHXH_Tang, 
		dc.fThuBHYT_NLD_Tang, 
		dc.fThuBHYT_NSD_Tang, 
		dc.fThuBHYT_Tang, 
		dc.fThuBHTN_NLD_Tang, 
		dc.fThuBHTN_NSD_Tang, 
		dc.fThuBHTN_Tang,
		dc.fThuBHXH_NLD_Giam, 
		dc.fThuBHXH_NSD_Giam, 
		dc.fThuBHXH_Giam, 
		dc.fThuBHYT_NLD_Giam, 
		dc.fThuBHYT_NSD_Giam, 
		dc.fThuBHYT_Giam, 
		dc.fThuBHTN_NLD_Giam, 
		dc.fThuBHTN_NSD_Giam, 
		dc.fThuBHTN_Giam,
		dc.fTongCong,
		Null, GETDATE(), Null, @Nguoitao 
	  FROM #DuToanDauNam dt 
	  JOIN #DataDieuChinh dc ON dt.sXauNoiMa = dc.sXauNoiMa

	  --danh dau chung tu da tong hop
		update BH_DTT_BHXH_DieuChinh set bDaTongHop = 1
		where iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop))
END
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 5/20/2024 3:51:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]    Script Date: 5/20/2024 3:51:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]    Script Date: 5/20/2024 3:51:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi] 
	@NamLamViec int,
	@MaDonVi nvarchar(500),
	@LoaiChungTu int,
	@DVT int
AS
BEGIN

	select mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha IsHangCha,
		mlns.sXauNoiMa XauNoiMa,
		mlns.sMoTa STenBhMLNS,
		chungtuchitiet.iID_MaDonVi,
		donvi.sTenDonVi,
		chungtuchitiet.iQSBQNam,
		chungtuchitiet.fLuongChinh,
		chungtuchitiet.fPhuCapChucVu,
		chungtuchitiet.fPCTNNghe,
		chungtuchitiet.fPCTNVuotKhung,
		chungtuchitiet.fNghiOm,
		chungtuchitiet.fHSBL,
		chungtuchitiet.fTongQTLN,
		chungtuchitiet.fThu_BHXH_NLD,
		chungtuchitiet.fThu_BHXH_NSD,
		chungtuchitiet.fTongThuBHXH,
		chungtuchitiet.fThu_BHYT_NLD,
		chungtuchitiet.fThu_BHYT_NSD,
		chungtuchitiet.fTongThuBHYT,
		chungtuchitiet.fThu_BHTN_NLD,
		chungtuchitiet.fThu_BHTN_NSD,
		chungtuchitiet.fTongThuBHTN,
		chungtuchitiet.fTongCong
		into tbl_kht_report
		from
			(select
				iID_MLNS,
				iID_MLNS_Cha,
				bHangCha,
				sLNS,
				sXauNoiMa,
				sMoTa
			from BH_DM_MucLucNganSach 
			where sLNS in ('9020001','9020002')
			and iNamLamViec = @NamLamViec) mlns
			left join
			(select * from
			(select '9020001-010-011-0001-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0001-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0001-0002' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0002-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0002-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0002' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0002-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0002-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong

			union all
			select
				ctct.sXauNoiMa,
				ct.iID_MaDonVi,
				sum(ctct.iQSBQNam) iQSBQNam,
				sum(ctct.fLuongChinh) fLuongChinh,
				sum(ctct.fPhuCapChucVu) fPhuCapChucVu,
				sum(ctct.fPCTNNghe) fPCTNNghe,
				sum(ctct.fPCTNVuotKhung) fPCTNVuotKhung,
				sum(ctct.fNghiOm) fNghiOm,
				sum(ctct.fHSBL) fHSBL,
				(isnull(sum(ctct.fLuongChinh), 0) + isnull(sum(ctct.fPhuCapChucVu), 0) + isnull(sum(ctct.fPCTNNghe), 0) + isnull(sum(ctct.fPCTNVuotKhung), 0) + isnull(sum(ctct.fNghiOm), 0) + isnull(sum(ctct.fHSBL), 0)) fTongQTLN,
				sum(ctct.fThu_BHXH_NLD) fThu_BHXH_NLD,
				sum(ctct.fThu_BHXH_NSD) fThu_BHXH_NSD,
				(isnull(sum(ctct.fThu_BHXH_NLD), 0) + isnull(sum(ctct.fThu_BHXH_NSD), 0)) fTongThuBHXH,
				sum(ctct.fThu_BHYT_NLD) fThu_BHYT_NLD,
				sum(ctct.fThu_BHYT_NSD) fThu_BHYT_NSD,
				(isnull(sum(ctct.fThu_BHYT_NLD), 0) + isnull(sum(ctct.fThu_BHYT_NSD), 0)) fTongThuBHYT,
				sum(ctct.fThu_BHTN_NLD) fThu_BHTN_NLD,
				sum(ctct.fThu_BHTN_NSD) fThu_BHTN_NSD,
				(isnull(sum(ctct.fThu_BHTN_NLD), 0) + isnull(sum(ctct.fThu_BHTN_NSD), 0)) fTongThuBHTN,
				(isnull(sum(ctct.fThu_BHXH_NLD), 0) + isnull(sum(ctct.fThu_BHXH_NSD), 0) + isnull(sum(ctct.fThu_BHYT_NLD), 0) + isnull(sum(ctct.fThu_BHYT_NSD), 0) + isnull(sum(ctct.fThu_BHTN_NLD), 0) + isnull(sum(ctct.fThu_BHTN_NSD), 0)) fTongCong
			from 
			BH_KHT_BHXH_ChiTiet ctct
			join BH_KHT_BHXH ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
			where ct.iID_MaDonVi in ((SELECT * FROM f_split(@MaDonVi)))
				and ct.iNamLamViec = @NamLamViec
				and ct.iLoaiTongHop = @LoaiChungTu
			group by
				ctct.sXauNoiMa,
				ct.iID_MaDonVi
			) chungtu) chungtuchitiet on mlns.sXauNoiMa = chungtuchitiet.sXauNoiMa
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) donvi ON chungtuchitiet.iID_MaDonVi = donvi.iID_MaDonVi
	order by mlns.sXauNoiMa

	update tbl_kht_report
	set STenBhMLNS = sTenDonVi
	where (fTongCong is not null and fTongCong <> 0) or (fTongQTLN is not null and fTongQTLN <> 0) or (iQSBQNam is not null and iQSBQNam <> 0)

	update tbl_kht_report
	set IsHangCha = 1
	where XauNoiMa in ('9020001-010-011-0001-0000','9020001-010-011-0001-0001','9020001-010-011-0001-0002','9020001-010-011-0002-0000','9020001-010-011-0002-0001',
						'9020002-010-011-0001-0000','9020002-010-011-0001-0001','9020002-010-011-0001-0002','9020002-010-011-0002-0000','9020002-010-011-0002-0001')
	and iID_MaDonVi is null

	update tbl_kht_report
	set STenBhMLNS = '          ' + STenBhMLNS
	where IsHangCha = 0

	--Lay MLNS cha
	select iID_MLNS,
		iID_MLNS_Cha,
		IsHangCha,
		XauNoiMa,
		STenBhMLNS
	into tbl_kht_parent
	from tbl_kht_report
	where IsHangCha = 1

	--Result
	select 
		report.iID_MLNS iID_MucLucNganSach,
		case
			when report.IsHangCha = 1 then report.iID_MLNS_Cha
			else parent.iID_MLNS
		end as iID_MLNS_Cha,
		report.IsHangCha,
		report.XauNoiMa,
		report.STenBhMLNS,
		report.iID_MaDonVi,
		report.sTenDonVi,
		report.iQSBQNam,
		report.fLuongChinh fLuongChinh,
		report.fPhuCapChucVu fPhuCapChucVu,
		report.fPCTNNghe fPCTNNghe,
		report.fPCTNVuotKhung fPCTNVuotKhung,
		report.fNghiOm fNghiOm,
		report.fHSBL fHSBL,
		report.fTongQTLN fTongQTLN,
		report.fThu_BHXH_NLD fThu_BHXH_NLD,
		report.fThu_BHXH_NSD fThu_BHXH_NSD,
		report.fTongThuBHXH fTongThuBHXH,
		report.fThu_BHYT_NLD fThu_BHYT_NLD,
		report.fThu_BHYT_NSD fThu_BHYT_NSD,
		report.fTongThuBHYT fTongThuBHYT,
		report.fThu_BHTN_NLD fThu_BHTN_NLD,
		report.fThu_BHTN_NSD fThu_BHTN_NSD,
		report.fTongThuBHTN fTongThuBHTN,
		report.fTongCong
	into tbl_kht_result
	from tbl_kht_report report
		left join tbl_kht_parent parent on report.XauNoiMa = parent.XauNoiMa
	order by report.XauNoiMa

	update tbl_kht_result
	set iID_MucLucNganSach = newid()
	where IsHangCha = 0
	and iID_MaDonVi is not null
	---
	select * from tbl_kht_result
	order by XauNoiMa

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_report]') AND type in (N'U')) drop table tbl_kht_report;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_parent]') AND type in (N'U')) drop table tbl_kht_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_result]') AND type in (N'U')) drop table tbl_kht_result;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 5/20/2024 3:51:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop] 
	@NamLamViec int,
	@MaDonVi nvarchar(500),
	@LoaiChungTu int,
	@DVT int
AS
BEGIN

	select mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha IsHangCha,
		mlns.sXauNoiMa XauNoiMa,
		mlns.sMoTa STenBhMLNS,
		chungtuchitiet.iID_MaDonVi,
		donvi.sTenDonVi,
		chungtuchitiet.iQSBQNam,
		chungtuchitiet.fLuongChinh,
		chungtuchitiet.fPhuCapChucVu,
		chungtuchitiet.fPCTNNghe,
		chungtuchitiet.fPCTNVuotKhung,
		chungtuchitiet.fNghiOm,
		chungtuchitiet.fHSBL,
		chungtuchitiet.fTongQTLN,
		chungtuchitiet.fThu_BHXH_NLD,
		chungtuchitiet.fThu_BHXH_NSD,
		chungtuchitiet.fTongThuBHXH,
		chungtuchitiet.fThu_BHYT_NLD,
		chungtuchitiet.fThu_BHYT_NSD,
		chungtuchitiet.fTongThuBHYT,
		chungtuchitiet.fThu_BHTN_NLD,
		chungtuchitiet.fThu_BHTN_NSD,
		chungtuchitiet.fTongThuBHTN,
		chungtuchitiet.fTongCong
		into tbl_kht_report_th
		from
			(select
				iID_MLNS,
				iID_MLNS_Cha,
				bHangCha,
				sLNS,
				sXauNoiMa,
				sMoTa
			from BH_DM_MucLucNganSach 
			where sLNS in ('9020001','9020002')
			and iNamLamViec = @NamLamViec) mlns
			left join
			(select * from
			(select '9020001-010-011-0001-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0001-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0001-0002' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0002-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0002-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0002' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0002-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0002-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong

			union all
			select
				ctct.sXauNoiMa,
				ct.iID_MaDonVi,
				sum(ctct.iQSBQNam) iQSBQNam,
				sum(ctct.fLuongChinh) fLuongChinh,
				sum(ctct.fPhuCapChucVu) fPhuCapChucVu,
				sum(ctct.fPCTNNghe) fPCTNNghe,
				sum(ctct.fPCTNVuotKhung) fPCTNVuotKhung,
				sum(ctct.fNghiOm) fNghiOm,
				sum(ctct.fHSBL) fHSBL,
				(isnull(sum(ctct.fLuongChinh), 0) + isnull(sum(ctct.fPhuCapChucVu), 0) + isnull(sum(ctct.fPCTNNghe), 0) + isnull(sum(ctct.fPCTNVuotKhung), 0) + isnull(sum(ctct.fNghiOm), 0) + isnull(sum(ctct.fHSBL), 0)) fTongQTLN,
				sum(ctct.fThu_BHXH_NLD) fThu_BHXH_NLD,
				sum(ctct.fThu_BHXH_NSD) fThu_BHXH_NSD,
				(isnull(sum(ctct.fThu_BHXH_NLD), 0) + isnull(sum(ctct.fThu_BHXH_NSD), 0)) fTongThuBHXH,
				sum(ctct.fThu_BHYT_NLD) fThu_BHYT_NLD,
				sum(ctct.fThu_BHYT_NSD) fThu_BHYT_NSD,
				(isnull(sum(ctct.fThu_BHYT_NLD), 0) + isnull(sum(ctct.fThu_BHYT_NSD), 0)) fTongThuBHYT,
				sum(ctct.fThu_BHTN_NLD) fThu_BHTN_NLD,
				sum(ctct.fThu_BHTN_NSD) fThu_BHTN_NSD,
				(isnull(sum(ctct.fThu_BHTN_NLD), 0) + isnull(sum(ctct.fThu_BHTN_NSD), 0)) fTongThuBHTN,
				(isnull(sum(ctct.fThu_BHXH_NLD), 0) + isnull(sum(ctct.fThu_BHXH_NSD), 0) + isnull(sum(ctct.fThu_BHYT_NLD), 0) + isnull(sum(ctct.fThu_BHYT_NSD), 0) + isnull(sum(ctct.fThu_BHTN_NLD), 0) + isnull(sum(ctct.fThu_BHTN_NSD), 0)) fTongCong
			from 
			BH_KHT_BHXH_ChiTiet ctct
			join BH_KHT_BHXH ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
			where ct.iID_MaDonVi in ((SELECT * FROM f_split(@MaDonVi)))
				and ct.iNamLamViec = @NamLamViec
				and ct.bDaTongHop = 1
				and ct.iLoaiTongHop = @LoaiChungTu
			group by
				ctct.sXauNoiMa,
				ct.iID_MaDonVi
			) chungtu) chungtuchitiet on mlns.sXauNoiMa = chungtuchitiet.sXauNoiMa
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) donvi ON chungtuchitiet.iID_MaDonVi = donvi.iID_MaDonVi
	order by mlns.sXauNoiMa

	update tbl_kht_report_th
	set STenBhMLNS = sTenDonVi
	where (fTongCong is not null and fTongCong <> 0) or (fTongQTLN is not null and fTongQTLN <> 0) or (iQSBQNam is not null and iQSBQNam <> 0)

	update tbl_kht_report_th
	set IsHangCha = 1
	where XauNoiMa in ('9020001-010-011-0001-0000','9020001-010-011-0001-0001','9020001-010-011-0001-0002','9020001-010-011-0002-0000','9020001-010-011-0002-0001',
						'9020002-010-011-0001-0000','9020002-010-011-0001-0001','9020002-010-011-0001-0002','9020002-010-011-0002-0000','9020002-010-011-0002-0001')
	and iID_MaDonVi is null

	update tbl_kht_report_th
	set STenBhMLNS = '          ' + STenBhMLNS
	where IsHangCha = 0

	--Lay MLNS cha
	select iID_MLNS,
		iID_MLNS_Cha,
		IsHangCha,
		XauNoiMa,
		STenBhMLNS
	into tbl_kht_parent_th
	from tbl_kht_report_th
	where IsHangCha = 1

	--Result
	select 
		report.iID_MLNS iID_MucLucNganSach,
		case
			when report.IsHangCha = 1 then report.iID_MLNS_Cha
			else parent.iID_MLNS
		end as iID_MLNS_Cha,
		report.IsHangCha,
		report.XauNoiMa,
		report.STenBhMLNS,
		report.iID_MaDonVi,
		report.sTenDonVi,
		report.iQSBQNam,
		report.fLuongChinh fLuongChinh,
		report.fPhuCapChucVu fPhuCapChucVu,
		report.fPCTNNghe fPCTNNghe,
		report.fPCTNVuotKhung fPCTNVuotKhung,
		report.fNghiOm fNghiOm,
		report.fHSBL fHSBL,
		report.fTongQTLN fTongQTLN,
		report.fThu_BHXH_NLD fThu_BHXH_NLD,
		report.fThu_BHXH_NSD fThu_BHXH_NSD,
		report.fTongThuBHXH fTongThuBHXH,
		report.fThu_BHYT_NLD fThu_BHYT_NLD,
		report.fThu_BHYT_NSD fThu_BHYT_NSD,
		report.fTongThuBHYT fTongThuBHYT,
		report.fThu_BHTN_NLD fThu_BHTN_NLD,
		report.fThu_BHTN_NSD fThu_BHTN_NSD,
		report.fTongThuBHTN fTongThuBHTN,
		report.fTongCong
	into tbl_kht_result_th
	from tbl_kht_report_th report
		left join tbl_kht_parent_th parent on report.XauNoiMa = parent.XauNoiMa
	order by report.XauNoiMa

	update tbl_kht_result_th
	set iID_MucLucNganSach = newid()
	where IsHangCha = 0
	and iID_MaDonVi is not null
	---
	select * from tbl_kht_result_th
	order by XauNoiMa

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_report_th]') AND type in (N'U')) drop table tbl_kht_report_th;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_parent_th]') AND type in (N'U')) drop table tbl_kht_parent_th;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_result_th]') AND type in (N'U')) drop table tbl_kht_result_th;

END
;
GO



INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:52.723' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:56:16.363' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'Con ốm - PCCVBH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:04:33.123' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:19.820' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'KT_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Khám thai - Lương BH thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:36:51.863' AS DateTime), CAST(N'2024-05-20T16:00:57.140' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'40', N'admin', N'admin', N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:34:30.413' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:08.773' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2025, N'00', N'Phụ cấp chức vụ - CNQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:54:47.773' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'CONOM_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:58:43.100' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'KT_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Khám thai - Lương BH thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:34.857' AS DateTime), CAST(N'2024-05-20T15:42:32.423' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'20', N'admin', N'admin', N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:04.167' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.3', NULL, N'OK_D14N_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:43:49.007' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:49.337' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'Khám thai - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:46.740' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:27.343' AS DateTime), CAST(N'2024-05-20T15:42:32.427' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'10', N'admin', N'admin', N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:56:42.670' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'Con ốm - PCCVBH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:59.173' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'KT_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Khám thai - Lương BH thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:36:24.720' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:47.677' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'OK_D14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:24.827' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:35:46.503' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:05:06.243' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:57.463' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'1', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:30.590' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OK_T14N_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:34.477' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.2', NULL, N'OK_D14N_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:13.993' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:44:07.773' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:02:01.717' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'KHHGD_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'KHHGĐ - Lương BH thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:51.493' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'KT_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Khám thai - Lương BH thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:40.357' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Khám thai - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:55.983' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6400', N'CACH4', N'4', NULL, N'OK_D14N_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:01:30.420' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'KHHGD_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'KHHGĐ - Lương BH thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:42:03.567' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'BDN_T14N_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:46.483' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'BDN_D14N_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:57.913' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'BDN_D14N_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:55:04.223' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'CONOM_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Con ốm - Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:45.157' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'OK_T14N_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:30:42.810' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'2', NULL, N'BDN_D14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:05:56.653' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:57.463' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:12.010' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:50:35.733' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'OK_T14N_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:14.757' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'OK_D14N_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:43:18.263' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'BDN_T14N_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:45:13.590' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:48:49.220' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.3', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:29:37.330' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'1', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:43.643' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.1', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2025, N'00', N'Phụ cấp chức vụ - CNQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:33.177' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.1', NULL, N'BDN_D14N_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:09.927' AS DateTime), CAST(N'2024-05-20T15:42:32.427' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'30', N'admin', N'admin', N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:43.033' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:55:25.277' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'CONOM_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:29:37.330' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:01:52.060' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'KHHGD_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'KHHGĐ - Lương BH thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:28.350' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'KT_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Khám thai - Lương BH thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:34.477' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2025, N'00', N'Phụ cấp chức vụ - CNQP', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:05:12.470' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'NAMNGHIVIEC_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:20.517' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.2', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:52:20.157' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2025, N'00', N'Phụ cấp chức vụ - CNQP', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:53:09.397' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:53:31.990' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:16.270' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:48:49.220' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:54:17.893' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'CONOM_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:55.650' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:04:58.483' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:34.407' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:14.283' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'Khám thai - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:29:11.403' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'1', NULL, N'BDN_D14N_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:54:38.213' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'CONOM_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:25.027' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'KHHGĐ- PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:04:47.877' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:02:53.260' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:02.033' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:02:09.907' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'KHHGD_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'KHHGĐ - Lương BH thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:43.643' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2025, N'00', N'Phụ cấp chức vụ - CNQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:05.237' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2025, N'00', N'Phụ cấp chức vụ - CNQP', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:47.677' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'2', NULL, N'OK_D14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:56:04.270' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Con ốm - PCCVBH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:19.530' AS DateTime), CAST(N'2024-05-20T15:42:32.430' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'70', N'admin', N'admin', N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:36:36.483' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:45:56.973' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:04.167' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'OK_D14N_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:42.497' AS DateTime), CAST(N'2024-05-20T15:42:32.430' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'TAINANLD_DUONGSUCPHSK', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6449', N'10', N'admin', N'admin', N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:55.173' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:31.923' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6400', N'CACH4', N'4', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:02:24.587' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'KHHGD_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'KHHGĐ - Lương BH thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:40:33.213' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'BDN_T14N_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:52:58.237' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:34.477' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'OK_D14N_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:31:08.730' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:32:48.443' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'BDN_D14N_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:01:43.900' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'KHHGD_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'KHHGĐ - Lương BH thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:44:56.893' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:23.470' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'OK_T14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:35:55.943' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OMDAU_DUONGSUCPHSK', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:12.957' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:58.020' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'Khám thai - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:53:22.133' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:40:59.047' AS DateTime), CAST(N'2024-05-17T17:41:58.600' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'BDN_T14N_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', N'admin', N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:46.483' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.3', NULL, N'BDN_D14N_LBH_TT', N'Lương viên chức quốc phòng', 2025, N'00', N'Lương viên chức quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:01:10.037' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'Khám thai - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:50:07.500' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:07.810' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:31:08.730' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'2', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:33.177' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'BDN_D14N_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:36:14.633' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:12.317' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'OK_T14N_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:42:59.323' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'BDN_T14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:29.563' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2025, N'00', N'Phụ cấp chức vụ - CNQP', N'Khám thai - PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:55.650' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.3', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:33:57.300' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:31.923' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:55:54.213' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2025, N'00', N'Phụ cấp chức vụ - CNQP', N'Con ốm - PCCVBH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:44:18.903' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2025, N'00', N'Phụ cấp chức vụ - SQ', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:13.993' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.2', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:08.773' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.1', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2025, N'00', N'Phụ cấp chức vụ - CNQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:48:32.767' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'1', NULL, N'OK_D14N_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:55:39.550' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'Con ốm - PCCVBH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:32:48.443' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6400', N'CACH4', N'4', NULL, N'BDN_D14N_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:43.210' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:30:42.810' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'BDN_D14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:50:07.500' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'2', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2025, N'00', N'Phụ cấp chức vụ - QNCN', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:48:32.767' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'OK_D14N_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:55.983' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OK_D14N_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:14.757' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.1', NULL, N'OK_D14N_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:35.220' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'KT_LBH_TT', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Khám thai - Lương BH thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:56:26.247' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'Con ốm - PCCVBH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:05:27.197' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:33:57.300' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6400', N'CACH4', N'4', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2025, N'00', N'Phụ cấp chức vụ', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:57.913' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.2', NULL, N'BDN_D14N_LBH_TT', N'Lương công chức quốc phòng', 2025, N'00', N'Lương công chức quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:54:58.230' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'CONOM_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2025, N'00', N'Lương quân nhân chuyên nghiệp', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:39:21.200' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'THAISAN_DUONGSUCPHSK', N'Phụ cấp quân hàm', 2025, N'00', N'Phụ cấp quân hàm', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:29:11.403' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'BDN_D14N_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:01.460' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'OK_T14N_LBH_TT', N'Lương công nhân quốc phòng', 2025, N'00', N'Lương công nhân quốc phòng', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:52:47.797' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2025, N'00', N'Phụ cấp chức vụ - VCQP', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:42:33.750' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'BDN_T14N_LBH_TT', N'Lương sĩ quan', 2025, N'00', N'Lương sĩ quan', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:20.517' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2025, N'00', N'Phụ cấp chức vụ - CCQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_quyet_toan_chitiet_bhxh]    Script Date: 5/20/2024 6:21:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyet_toan_chitiet_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyet_toan_chitiet_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyet_toan_chitiet_bhxh]    Script Date: 5/20/2024 6:21:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_quyet_toan_chitiet_bhxh] 
	@MaDonVi VARCHAR(MAX),
	@Thang INT,
	@Nam INT
AS
BEGIN
	select 
		ct.Ma_DonVi MaDonVi,
		ct.Nam INam, 
		ct.Thang IThang,
		ctct.XauNoiMa,
		ctct.MaCb,
		capbac.Parent as MaCbCha,
		ctct.MoTa,
		ctct.DieuChinh,
		ctct.TongCong,
		ctct.SoNgay,
		ctct.SoNguoi,
		ctct.MaCachTl
	from 
	TL_QT_ChungTu ct
	join TL_QT_ChungTuChiTiet ctct on ct.ID = ctct.Id_ChungTu
	left join TL_DM_CapBac capbac ON ctct.MaCb = capbac.Ma_Cb
	where ct.Nam = @Nam
		and ct.Thang = @Thang
		and ct.Ma_DonVi = @maDonVi
		and ctct.MaCachTl = 'CACH2'
END
GO


update TL_DM_Cach_TinhLuong_BaoHiem
set CongThuc = N'LCS*THAISAN_DUONGSUCPHSK_HS*SONGAYHUONG'
where Ma_Cot = 'THAISAN_DUONGSUCPHSK';