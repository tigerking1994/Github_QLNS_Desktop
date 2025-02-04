/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 3/8/2024 4:08:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_dtt]    Script Date: 3/8/2024 4:08:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_tong_du_toan_dtt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_dtt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 3/8/2024 4:08:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
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
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;
	
	select
	 pc.Ma_PhuCap,
	 pc.Ma_Cb,
	 sum(ctct.DieuChinh) fGiaTri
	into tbl_cancu_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
	and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.Ma_DonVi = @MaDonVi
	and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.Ma_Cb

	 --Get so nguoi
	select
	 pc.Ma_Cb,
	 sum(ctct.SoNguoi) IQSBQNam
	 into tbl_cancu_so_nguoi_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
	and pc.Ma_PhuCap = 'LHT_TT'
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.Ma_DonVi = @MaDonVi
	and ct.bKhoa = 1
	group by
	 pc.Ma_Cb
	 ------------------------------------------
	 select distinct
	  cancu.Ma_Cb SMaCapBac,
	  songuoi.IQSBQNam IQSBQNam,
	  --songuoi.IQSBQNam/12 IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL
	 into tbl_cancu_result_y
	 from tbl_cancu_y cancu
	 left join tbl_cancu_y luongchinh on cancu.Ma_Cb = luongchinh.Ma_Cb and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_cancu_y pccv on cancu.Ma_Cb = pccv.Ma_Cb and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_cancu_y pctn on cancu.Ma_Cb = pctn.Ma_Cb and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_cancu_y pctnvk on cancu.Ma_Cb = pctnvk.Ma_Cb and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_cancu_y hsbl on cancu.Ma_Cb = hsbl.Ma_Cb and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_cancu_so_nguoi_y songuoi on cancu.Ma_Cb = songuoi.Ma_Cb

	 update tbl_cancu_result_y set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	 select distinct
	  cancu.SMaCapBac,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL
	 into tbl_cancu_result_final_y
	 from tbl_cancu_result_y cancu
	 group by cancu.SMaCapBac

	 --Luong BHXH
	 select
	  ctct.sMaCB SMaCapBac,
	  sum(ctct.nGiaTri) FNghiOm
	 into tbl_luong_can_cu_y
	 from TL_BangLuong_ThangBHXH ctct
	 join TL_DS_CapNhap_BangLuong ct on ctct.iID_Parent = ct.Id
	 where ctct.iNam = @Year
		and ctct.sMaDonVi = @MaDonVi
		and ctct.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
		and (ctct.sMaCB like '1%' or ctct.sMaCB like '2%' or ctct.sMaCB like '0%' or ctct.sMaCB = '43' or ctct.sMaCB in ('3.1', '3.2', '3.3'))
		and ct.KhoaBangLuong = 1
		and isnull(ct.STongHop, '') <> ''
	 group by ctct.sMaCB

	 update tbl_luong_can_cu_y set SMaCapBac = '1' where SMaCapBac like '1%'
	 update tbl_luong_can_cu_y set SMaCapBac = '2' where SMaCapBac like '2%'
	 update tbl_luong_can_cu_y set SMaCapBac = '4' where SMaCapBac like '0%'
	 update tbl_luong_can_cu_y set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	 select
	  SMaCapBac,
	  sum(FNghiOm) FNghiOm
	 into tbl_luong_can_cu_final_y
	 from tbl_luong_can_cu_y
	 group by SMaCapBac

	 --result
	 select
	  luong.SMaCapBac,
	  luong.IQSBQNam,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm
	 from tbl_cancu_result_final_y luong
	 left join tbl_luong_can_cu_final_y bhxh on luong.SMaCapBac = bhxh.SMaCapBac
	 -----------------------------------------------
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_y]') AND type in (N'U')) drop table tbl_cancu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 3/8/2024 4:08:39 PM ******/
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
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;
	
	select
	 pc.Ma_PhuCap,
	 pc.Ma_Cb,
	 sum(ctct.DieuChinh) fGiaTri
	into tbl_cancu
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
		and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
		and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.Ma_Cb
	 
	--Get so nguoi
	select
	 pc.Ma_Cb,
	 sum(ctct.SoNguoi) IQSBQNam
	 into tbl_cancu_so_nguoi
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
		and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.Ma_Cb
	 ------------------------------------------
	 select distinct
	  cancu.Ma_Cb SMaCapBac,
	  case
		when @LoaiQuyNam = 0 then songuoi.IQSBQNam
		else songuoi.IQSBQNam/3
	  end as IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL
	 into tbl_cancu_result
	 from tbl_cancu cancu
	 left join tbl_cancu luongchinh on cancu.Ma_Cb = luongchinh.Ma_Cb and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_cancu pccv on cancu.Ma_Cb = pccv.Ma_Cb and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_cancu pctn on cancu.Ma_Cb = pctn.Ma_Cb and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_cancu pctnvk on cancu.Ma_Cb = pctnvk.Ma_Cb and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_cancu hsbl on cancu.Ma_Cb = hsbl.Ma_Cb and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_cancu_so_nguoi songuoi on cancu.Ma_Cb = songuoi.Ma_Cb

	 update tbl_cancu_result set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	 select distinct
	  cancu.SMaCapBac,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL
	 into tbl_cancu_result_final
	 from tbl_cancu_result cancu
	 group by cancu.SMaCapBac

	 --Luong BHXH
	 select
	  ctct.sMaCB SMaCapBac,
	  sum(ctct.nGiaTri) FNghiOm
	 into tbl_luong_can_cu
	 from TL_BangLuong_ThangBHXH ctct
	 join TL_DS_CapNhap_BangLuong ct on ctct.iID_Parent = ct.Id
	 where ctct.iNam = @Year
		and ctct.sMaDonVi = @MaDonVi
		and ctct.iThang in (SELECT * FROM f_split(@Months))
		and ctct.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
		and (ctct.sMaCB like '1%' or ctct.sMaCB like '2%' or ctct.sMaCB like '0%' or ctct.sMaCB = '43' or ctct.sMaCB in ('3.1', '3.2', '3.3'))
		and ct.KhoaBangLuong = 1
		and isnull(ct.STongHop, '') <> ''
	 group by ctct.sMaCB

	update tbl_luong_can_cu set SMaCapBac = '1' where SMaCapBac like '1%'
	update tbl_luong_can_cu set SMaCapBac = '2' where SMaCapBac like '2%'
	update tbl_luong_can_cu set SMaCapBac = '4' where SMaCapBac like '0%'
	update tbl_luong_can_cu set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	select
	  SMaCapBac,
	  sum(FNghiOm) FNghiOm
	 into tbl_luong_can_cu_final
	 from tbl_luong_can_cu
	 group by SMaCapBac

	 --result
	 select
	  luong.SMaCapBac,
	  luong.IQSBQNam,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm
	 from tbl_cancu_result_final luong
	 left join tbl_luong_can_cu_final bhxh on luong.SMaCapBac = bhxh.SMaCapBac
	 -----------------------------------------------
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_dtt]    Script Date: 3/8/2024 4:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_dtt] 
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select 
		ctct.iID_MLNS,
		sum(ctct.fTongCong) fTongCong
	from
	BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	where ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
		ctct.iID_MLNS
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		into tbl_qtn_result
		from
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
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
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
		join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		where
			ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1) chungtudonvi 
		on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi = @IdDonVi
	group by sLNS

	--Update so truy thu
	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

	-- Ket qua
	select * from tbl_qtn_result order by sXauNoiMa;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int  
AS
BEGIN
DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr 	where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit,
sM nvarchar(200),
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
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu fPhuCapChucVu
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
			,mlns.sM
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
			and ct.iQuyNam = @IQuy
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
sM,
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
		mlns.sM,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD) fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD) fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong,
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
			AND iNamLamViec = @NamLamViec			
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
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
			and ct.iID_MaDonVi = @IdDonVis
			and ct.iLoaiTongHop = 2
--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
		)chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		ORDER BY mlns.sXauNoiMa;


----------------END INSERT DETAIL----------------
----------------INSERT DETAIL----------------
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sM,
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
sM,
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu,
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
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into #tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi = @IdDonVis
	group by sLNS

	--Update so truy thu
	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

-- Ket qua
SELECT * FROM #result ORDER BY sXauNoiMa, MaDonVi;

DROP TABLE #tempChiTietDonVi;
DROP TABLE #tbl_qtn_truythu;
DROP TABLE #result;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int
AS
BEGIN
CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit, 
sM nvarchar(200),
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
			,mlns.sM
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
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = 0;
--END chi tiet

INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sM,
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
		mlns.sM,
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
			AND iNamLamViec = @NamLamViec
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa

			) mlns

		LEFT JOIN #tempChiTietDonVi chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		GROUP BY
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sM,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa;
	--INSERT CHI TIẾT	
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
SM,
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
SM,
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
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
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;

-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into #tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
	group by sLNS

	--Update so truy thu
	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

-- Ket qua
SELECT * FROM #result ORDER BY sXauNoiMa , MaDonVi;

DROP TABLE #tempChiTietDonVi;
DROP TABLE #tbl_qtn_truythu;
DROP TABLE #result;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

	select
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sM,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
		sum(chungtudonvi.fPCChucVu)/@Donvitinh fPhuCapChucVu,
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
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh FTongSoPhaiThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
		into tbl_qtn_result
		from
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
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
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
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
				) chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		group by
			mlns.iID_MLNS,
			mlns.sMoTa,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sM,
			mlns.sXauNoiMa
		

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
	group by sLNS

	--Update so truy thu
	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

	-- Ket qua
	select * from tbl_qtn_result order by sXauNoiMa;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 3/8/2024 4:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		into tbl_qtn_result
		from
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
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
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
			where
			ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
				) chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi = @IdDonVi
	group by sLNS

	--Update so truy thu
	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

	-- Ket qua
	select * from tbl_qtn_result order by sXauNoiMa;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 3/8/2024 4:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@DVT int,
	@IsTongHop bit
AS
BEGIN
	CREATE TABLE #result(STT nvarchar(50), IIdChungTu uniqueidentifier, IIdParent uniqueidentifier, SNoiDung nvarchar(200), ILevel int, IThuTu int, FSoTien float)
	DECLARE @IIdQTT uniqueidentifier = NewID();
	DECLARE @IIdQTC uniqueidentifier = NewID();
	DECLARE @IIdThuBHYT uniqueidentifier = NewID();
	DECLARE @IIdChiKcbBHYT uniqueidentifier = NewID();

	INSERT INTO #result(STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien)
	(
		--I Quyết toán thu
		SELECT 'I', @IIdQTT, NULL, N'Quyết toán thu', 1, 1, 0
		
		UNION ALL
		SELECT '1', NEWID(), @IIdQTT, N'Thu bảo hiểm xã hội (Phụ lục II)', 2, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (7,8,9,10,12,13,14,15, 19, 20, 21, 22, 24, 25, 26, 27, 29, 30,34, 35, 36, 37, 39, 40, 41, 42, 46, 47, 48, 49, 51, 52, 53, 54, 56, 57)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdQTT, N'Thu bảo hiểm thất nghiệp (Phụ lục III)', 2, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (70, 71, 73, 74,77, 78, 80, 81)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '3', @IIdThuBHYT, @IIdQTT, N'Thu bảo hiểm y tế', 2, 3, 0

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT quân nhân (Phụ lục IV)', 3, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (133, 134, 135, 136, 138, 139, 140, 141)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT người lao động (Phụ lục V)', 3, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (95, 96, 98, 99, 102, 103, 105, 106, 110, 111, 113, 114, 117, 118, 120, 121)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT thân nhân quân nhân (Phụ lục VI)', 3, 3, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 151
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT thân nhân CN', 3, 4, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 155
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT học sinh', 3, 5, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 163
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT lưu học sinh (Phụ lục VII)', 3, 6, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 167
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT HV QS xã phường (Phụ lục VII)', 3, 7, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 159
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT SQ dự bị (Phụ lục VII)', 3, 8, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 171
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		--II Quyết toán chi
		UNION ALL
		SELECT 'II', @IIdQTC, NULL, N'Quyết toán chi', 1, 2, 0

		UNION ALL
		SELECT '1', NEWID(), @IIdQTC, N'Chi các chế độ BHXH (Phụ lục VIII)', 2, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (183, 184, 185, 186, 187, 188, 189, 190, 193, 194, 195, 196, 197, 198, 199, 200)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdQTC, N'Chi KP quản lý BHXH', 2, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 252
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '3', NEWID(), @IIdQTC, N'Chi mua sắm TTB y tế (Phụ lục X)', 2, 3, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 231
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '4', @IIdChiKcbBHYT, @IIdQTC, N'Chi KCB BHYT', 2, 4, 0

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB cho quân nhân tại TS-DK (Phụ lục XI)', 3, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 238
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB tại quân y đơn vị (Phụ lục XII)', 3, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 223
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)', 3, 3, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 209
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND dv.iTrangThai = 1
			AND dv.iKhoi = 2 --Khối dự toán

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)', 3, 4, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 215
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND dv.iTrangThai = 1
			AND dv.iKhoi = 2 --Khối dự toán

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB tại các cơ sở y tế (Phụ lục XV)', 3, 5, 
		CASE WHEN @IsTongHop = 1 THEN SUM(ISNULL(ctct.fQuyetToanQuyNay, 0))
		ELSE 0 END fQuyetToanQuyNay
		FROM BH_QTC_CapKinhPhi_KCB_ChiTiet ctct
		JOIN BH_QTC_CapKinhPhi_KCB ct ON ct.iID_ChungTu = ctct.iID_ChungTu
		WHERE ct.iNamLamViec = @NamLamViec 
			AND ct.iQuy = @NamLamViec
	)

	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien/@DVT FSoTien from #result

	DROP TABLE #result;

END
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong_tong_hop_don_vi]    Script Date: 3/8/2024 5:14:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong_tong_hop_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong_tong_hop_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong_tong_hop_don_vi]    Script Date: 3/8/2024 5:14:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong_tong_hop_don_vi]
	@NamLamViec int,
	@MaDonVis nvarchar(50)
AS
BEGIN
	select
	mlns.iID_MLGT,
	mlns.iSTT,
	mlns.sMoTa,
	mlns.sNoiDung,
	mlns.iLoai,
	sum(isnull(chungtudonvi.fSoPhaiThuNop, 0)) fSoPhaiThuNop,
	sum(isnull(chungtudonvi.fSoDaNopTrongNam, 0)) fSoDaNopTrongNam,
	sum(isnull(chungtudonvi.fSoDaNopSau31_12, 0)) fSoDaNopSau31_12,
	sum(isnull(chungtudonvi.fTongSoDaNop, 0)) fTongSoDaNop,
	sum(isnull(chungtudonvi.fSoConPhaiNop, 0)) fSoConPhaiNop,
	sum(isnull(chungtudonvi.iQuanSo, 0)) iQuanSo,
	sum(isnull(chungtudonvi.fQuyTienLuongCanCu, 0)) fQuyTienLuongCanCu,
	sum(isnull(chungtudonvi.fSoTienGiamDong, 0)) fSoTienGiamDong,
	null dTuNgay,
	null dDenNgay
	from
		(select
			iSTT,
			sNoiDung,
			iID_MLGT,
			concat(iSTT, '. ' , sNoiDung) sMoTa,
			iLoai
		from BH_QTT_MucLucGiaiThich) mlns
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
				ctgt.iID_MLNS,
				ctgt.sNoiDung,
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
				ctgt.iLoaiGiaiThich
			from BH_QTT_BHXH_CTCT_GiaiThich ctgt
			where ctgt.iQuyNam = @NamLamViec
			and ctgt.iLoaiGiaiThich = 4
			and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVis))) chungtudonvi
		on mlns.iID_MLGT = chungtudonvi.iID_MLNS
		group by
			mlns.iID_MLGT,
			mlns.iSTT,
			mlns.sMoTa,
			mlns.sNoiDung,
			mlns.iLoai
END
;
GO
