/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_phucluc_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_phanbodutoanchi_index_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_phanbodutoanchi_index_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_phanbodutoanchi_index_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiao_index_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiao_index_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiao_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_ndt_ctg_get_khc_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_getdata_chungtu_theoloaichi]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_getdata_chungtu_theoloaichi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_getdata_chungtu_theoloaichi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_nkp_ql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khc_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khc_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dtdc_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_dtdc_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_dtdc_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 3/20/2024 3:01:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As

begin
	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID()  as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
	BH_DM_MucLucNganSach.iID_MLNS_Cha,
	BH_DM_MucLucNganSach.sLNS,
	BH_DM_MucLucNganSach.sL,
	BH_DM_MucLucNganSach.sK,
	BH_DM_MucLucNganSach.sM,
	BH_DM_MucLucNganSach.sTM,
	BH_DM_MucLucNganSach.sTTM,
	BH_DM_MucLucNganSach.sNG,
	BH_DM_MucLucNganSach.sTNG,
	BH_DM_MucLucNganSach.sXauNoiMa,
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	0 as fTienTuChi,
	--0 as fTienTuChiTruocDieuChinh,
	--0 as fTienHienVat,
	--0 as fTienHienVatTruocDieuChinh,
	BH_DM_MucLucNganSach.sCPChiTietToi,
	BH_DM_MucLucNganSach.sDuToanChiTietToi,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	BH_DM_MucLucNganSach.bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	--where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '901%'
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  
	and bHangChaDuToan is not null
	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  #tblDonVi
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into #tblChungTuNhanPhanBo
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	
	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			nhanpb_chitiet.fTienTuChi as fTuChi,
			--nhanpb_chitiet.fTienHienVat as fHienVat,
			nhanpb.sSoQuyetDinh
	into #tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join BH_DTC_DuToanChiTrenGiao as nhanpb on nhanpb.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao
	where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)

	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		#tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		#tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
		#tblMucLucNganSach.iID_MLNS_Cha,
		#tblMucLucNganSach.sLNS,
		#tblMucLucNganSach.sL,
		#tblMucLucNganSach.sK, 
		#tblMucLucNganSach.sM,
		#tblMucLucNganSach.sTM,
		#tblMucLucNganSach.sTTM,
		#tblMucLucNganSach.sNG,
		#tblMucLucNganSach.sTNG,
		#tblMucLucNganSach.sXauNoiMa,
		#tblMucLucNganSach.sNoiDung,
		#tblChiTietDuToanNhan.sSoQuyetDinh,
		#tblChiTietDuToanNhan.fTuChi as fTienTuChi ,
		--#tblChiTietDuToanNhan.fHienVat as fTienHienVat,
		#tblMucLucNganSach.sCPChiTietToi,
		#tblMucLucNganSach.sDuToanChiTietToi,
		3 as Type
	into #tbl_tblChiTietDuToanNhan_MucLuc
	from #tblMucLucNganSach
	inner join #tblChiTietDuToanNhan on #tblMucLucNganSach.iID_MLNS = #tblChiTietDuToanNhan.iID_MucLucNganSach

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  #tbl_tblChiTietDuToanNhan_MucLuc.*,#tblDonVi.iID_MaDonVi, concat(#tblDonVi.iID_MaDonVi, '-', #tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from #tbl_tblChiTietDuToanNhan_MucLuc cross join #tblDonVi 


	---Map với bảng BH_DTC_PhanBoDuToanChi_ChiTiet để lấy thông tin fTuChi đã được phân bổ
	select 
		#temp.iID_DTC_DuToanChiTrenGiao, 
		chitiet_phanbo.ID as iID_DTC_PhanBoDuToanChiTiet,
		#temp.iID_MLNS,
		#temp.iID_MLNS_Cha,
		#temp.sLNS,
		#temp.sL,
		#temp.sK,
		#temp.sM,
		#temp.sTM,
		#temp.sTTM,
		#temp.sNG,
		#temp.sTNG,
		#temp.sXauNoiMa,
		#temp.sNoiDung as sNoiDung,
		chitiet_phanbo.fTienTuChi as fTienTuChi,
		#temp.fTienTuChi as fTienTuChiTruocDieuChinh,
		--chitiet_phanbo.fTienHienVat as fTienHienVat,
		--#temp.fTienHienVat as fTienHienVatTruocDieuChinh,
		#temp.sCPChiTietToi,
		#temp.sDuToanChiTietToi,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as bHangChaDuToan,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTC_PhanBoDuToanChi_ChiTiet 
			where iID_DTC_PhanBoDuToanChi = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTC_DuToanChiTrenGiao = #temp.iID_DTC_DuToanChiTrenGiao and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MucLucNganSach = #temp.iID_MLNS



	-----Lấy danh sách số chưa phân bổ
	select 
	npb.ID as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số chưa phân bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	--chitiet_chuaphanbo.fHienVat as fTienHienVat,
	--chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	muluc_ngansach.sCPChiTietToi,
	muluc_ngansach.sDuToanChiTietToi,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as bHangChaDuToan,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (
		
		ISNULL(ct_npb.fTienTuChi,0) -  ISNULL(ct_pb_t.fTuChi,0) ) as fTuChi ,
		ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		--select (ISNULL(ct_npb.fTienTuChi,0) - ISNULL(ct_pb_t.fTuChi,0)) as fTuChi , (ISNULL(ct_npb.fTienHienVat,0) - ISNULL(ct_pb_t.fHienVat,0)) as fHienVat, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select  sum(  fTienTuChi) as fTuChi , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo);
	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	#tblSoChuaPhanBo.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	#tblMucLucNganSach.iID_MLNS as iID_MLNS,
	#tblMucLucNganSach.iID_MLNS_Cha,
	#tblMucLucNganSach.sLNS,
	#tblMucLucNganSach.sL,
	#tblMucLucNganSach.sK,
	#tblMucLucNganSach.sM,
	#tblMucLucNganSach.sTM,
	#tblMucLucNganSach.sTTM,
	#tblMucLucNganSach.sNG,
	#tblMucLucNganSach.sTNG,
	#tblMucLucNganSach.sXauNoiMa,
	#tblMucLucNganSach.sNoiDung as sNoiDung,
	#tblSoChuaPhanBo.fTienTuChi as fTienTuChi,
	#tblSoChuaPhanBo.fTienTuChiTruocDieuChinh as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	#tblMucLucNganSach.sCPChiTietToi,
	#tblMucLucNganSach.sDuToanChiTietToi,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1
	---Tính lại dự toán, số đã phân bổ
		-- Dữ liệu nhận phân bổ

declare @iiDotNhan nvarchar(500) =( select  iID_DotNhan from  BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, fTienTuChi INTO #tmpNhanDuToan from BH_DTC_DuToanChiTrenGiao_ChiTiet ct
INNER JOIN BH_DTC_DuToanChiTrenGiao dt on dt.ID = ct.iID_DTC_DuToanChiTrenGiao
where dt.ID IN (select * from splitstring( @iiDotNhan))


-- Dữ liệu đã phân bổ
declare @dNgayQuyetDinh Datetime = (select dNgayQuyetDinh from BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, 0 - SUM(ISNULL(fTienTuChi,0)) fTuChi   INTO #tmpDaPhanBo from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
INNER JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID
where 
ct.iNamChungTu = @NamLamViec
AND ct.dNgayQuyetDinh < @dNgayQuyetDinh
group by iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao;
	--Hiển thị kết quả trả về
	select * INTO #result from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	--order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow

	----============
	SELECT
	rs.iID_DTC_DuToanChiTrenGiao,
	rs.iID_DTC_PhanBoDuToanChiTiet,
	rs.iID_MLNS,
	rs.iID_MLNS_Cha,
	rs.sLNS,
	rs.sL,
	rs.sK,
	rs.sM,
	rs.sTM,
	rs.sTTM,
	rs.sNG,
	rs.sTNG,
	rs.sXauNoiMa,
	rs.sNoiDung as sNoiDung,

	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
	END as fTienTuChi,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
	END as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	rs.sCPChiTietToi,
	rs.sDuToanChiTietToi,
	rs.Type,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh,
	rs.bHangCha,
	rs.bHangChaDuToan,
	rs.IsRemainRow
	FROM #result rs
	LEFT JOIN #tmpNhanDuToan dt ON rs.iID_MLNS = dt.iID_MucLucNganSach 
	LEFT JOIN #tmpDaPhanBo dpb ON dpb.iID_MucLucNganSach = rs.iID_MLNS and dpb.iID_DTC_DuToanChiTrenGiao = rs.iID_DTC_DuToanChiTrenGiao
	LEFT JOIN (
	SELECT SUM(fTienTuChi) fTuChi, iID_MucLucNganSach FROM BH_DTC_PhanBoDuToanChi_ChiTiet WHERE iID_DTC_PhanBoDuToanChi = @ChungTuId GROUP BY iID_MucLucNganSach

	) ct ON ct.iID_MucLucNganSach = rs.iID_MLNS
		order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi,rs.Type,rs.IsRemainRow

	--SELECT * from #tblSoChuaPhanBo


drop table #tblMucLucNganSach;
drop table #tblDonVi;
drop table #tblChungTuNhanPhanBo;
drop table #tblChiTietDuToanNhan;
drop table #tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table #tblSoChuaPhanBo;
drop table #tblMucLucNganSach_duplicate;

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As
begin
	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID()  as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
	BH_DM_MucLucNganSach.iID_MLNS_Cha,
	BH_DM_MucLucNganSach.sLNS,
	BH_DM_MucLucNganSach.sL,
	BH_DM_MucLucNganSach.sK,
	BH_DM_MucLucNganSach.sM,
	BH_DM_MucLucNganSach.sTM,
	BH_DM_MucLucNganSach.sTTM,
	BH_DM_MucLucNganSach.sNG,
	BH_DM_MucLucNganSach.sTNG,
	BH_DM_MucLucNganSach.sXauNoiMa,
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	0 as fTienTuChi,
	0 as fTienTuChiTruocDieuChinh,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec 

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  #tblDonVi
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into #tblChungTuNhanPhanBo
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	
	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			nhanpb_chitiet.fTongTien as fDuToan,
			nhanpb.sSoQuyetDinh
	into #tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join BH_DTC_DuToanChiTrenGiao as nhanpb on nhanpb.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao
	where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)

	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		#tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		#tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
		#tblMucLucNganSach.iID_MLNS_Cha,
		#tblMucLucNganSach.sLNS,
		#tblMucLucNganSach.sL,
		#tblMucLucNganSach.sK, 
		#tblMucLucNganSach.sM,
		#tblMucLucNganSach.sTM,
		#tblMucLucNganSach.sTTM,
		#tblMucLucNganSach.sNG,
		#tblMucLucNganSach.sTNG,
		#tblMucLucNganSach.sXauNoiMa,
		#tblMucLucNganSach.sNoiDung,
		#tblChiTietDuToanNhan.sSoQuyetDinh,
		#tblChiTietDuToanNhan.fDuToan as fDuToan ,
		3 as Type
	into #tbl_tblChiTietDuToanNhan_MucLuc
	from #tblMucLucNganSach
	inner join #tblChiTietDuToanNhan on #tblMucLucNganSach.iID_MLNS = #tblChiTietDuToanNhan.iID_MucLucNganSach

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  #tbl_tblChiTietDuToanNhan_MucLuc.*,#tblDonVi.iID_MaDonVi, concat(#tblDonVi.iID_MaDonVi, '-', #tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from #tbl_tblChiTietDuToanNhan_MucLuc cross join #tblDonVi 


	---Map với bảng BH_DTC_PhanBoDuToanChi_ChiTiet để lấy thông tin fTuChi đã được phân bổ
	select 
		#temp.iID_DTC_DuToanChiTrenGiao, 
		chitiet_phanbo.ID as iID_DTC_PhanBoDuToanChiTiet,
		#temp.iID_MLNS,
		#temp.iID_MLNS_Cha,
		#temp.sLNS,
		#temp.sL,
		#temp.sK,
		#temp.sM,
		#temp.sTM,
		#temp.sTTM,
		#temp.sNG,
		#temp.sTNG,
		#temp.sXauNoiMa,
		#temp.sNoiDung as sNoiDung,
		chitiet_phanbo.fTienTuChi as fTienTuChi,
		#temp.fDuToan as fTienTuChiTruocDieuChinh,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTC_PhanBoDuToanChi_ChiTiet 
			where iID_DTC_PhanBoDuToanChi = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTC_DuToanChiTrenGiao = #temp.iID_DTC_DuToanChiTrenGiao and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MucLucNganSach = #temp.iID_MLNS



	-----Lấy danh sách số chưa phân bổ
	select 
	npb.ID as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số Chưa Phân Bổ' as sNoiDung,
	chitiet_chuaphanbo.fTongTien as fTienTuChi,
	chitiet_chuaphanbo.fTongTien as fTienTuChiTruocDieuChinh,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fTongTien,0) - ISNULL(ct_pb_t.fTongTien,0)) as fTongTien, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select sum(fTongTien) as fTongTien  , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)


	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	#tblSoChuaPhanBo.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	#tblMucLucNganSach.iID_MLNS as iID_MLNS,
	#tblMucLucNganSach.iID_MLNS_Cha,
	#tblMucLucNganSach.sLNS,
	#tblMucLucNganSach.sL,
	#tblMucLucNganSach.sK,
	#tblMucLucNganSach.sM,
	#tblMucLucNganSach.sTM,
	#tblMucLucNganSach.sTTM,
	#tblMucLucNganSach.sNG,
	#tblMucLucNganSach.sTNG,
	#tblMucLucNganSach.sXauNoiMa,
	#tblMucLucNganSach.sNoiDung as sNoiDung,
	#tblSoChuaPhanBo.fTienTuChi as fTienTuChi,
	#tblSoChuaPhanBo.fTienTuChiTruocDieuChinh as fTienTuChiTruocDieuChinh,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1

	--Hiển thị kết quả trả về
	select * from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow


drop table #tblMucLucNganSach;
drop table #tblDonVi;
drop table #tblChungTuNhanPhanBo;
drop table #tblChiTietDuToanNhan;
drop table #tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table #tblSoChuaPhanBo;
drop table #tblMucLucNganSach_duplicate;

end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all_clone]
@YearOfWork int ,
@Date DateTime,
@LoaiDuToanNhan int
AS
BEGIN
	DECLARE @DieuChinh int = 2;
	--Lấy danh sách dự toán nhận phân bổ
		SELECT ID as iID_DTC_DuToanChiTrenGiao,
			   sSoChungTu,
			   sLNS,
			   iID_MaDonVi,
			   dNgayChungTu,
			   sSoQuyetDinh,
			   dNgayQuyetDinh,
			   fTongTienTuChi + fTongTienHienVat AS fSoPhanBo,
			   sMaLoaiChi
		INTO  #tblNhanPhanBo
		FROM BH_DTC_DuToanChiTrenGiao 
		WHERE iNamLamViec = @YearOfWork 
			AND iLoaiDotNhanPhanBo = @LoaiDuToanNhan
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))



		--Lấy danh sách dự toán đã được phân bổ
		SELECT ISNULL(sum(pb_ct.fTienTuChi),0) + ISNULL(sum(pb_ct.fTienHienVat),0)  as fDaPhanBo, pb_ct.iID_DTC_DuToanChiTrenGiao AS iID_DTC_DuToanChiTrenGiao
		INTO #tblChungTuNhanPhanBoMap
		FROM  BH_DTC_PhanBoDuToanChi_ChiTiet as pb_ct 
		WHERE pb_ct.iID_DTC_DuToanChiTrenGiao in (select iID_DTC_DuToanChiTrenGiao from  #tblNhanPhanBo)
		and iNamLamViec=@YearOfWork
		GROUP BY pb_ct.iID_DTC_DuToanChiTrenGiao

		-----Lay danh sach du toan nhan phan bo, so phan bo, chua phan bo
		SELECT  npb.iID_DTC_DuToanChiTrenGiao as Id,
			    npb.sSoChungTu, 
				npb.sLNS,
				npb.iID_MaDonVi,
				npb.dNgayChungTu, 
				npb.sSoQuyetDinh, 
				npb.dNgayQuyetDinh, 
				npb.sMaLoaiChi,
				npb.fSoPhanBo, 
				npbm.fDaPhanBo,
				ISNULL(npb.fSoPhanBo,0) - ISNULL(npbm.fDaPhanBo,0) AS fSoChuaPhanBo
		FROM #tblNhanPhanBo AS npb
		left join #tblChungTuNhanPhanBoMap AS npbm ON npb.iID_DTC_DuToanChiTrenGiao = npbm.iID_DTC_DuToanChiTrenGiao

	   DROP TABLE #tblNhanPhanBo;	
       DROP TABLE #tblChungTuNhanPhanBoMap;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dtdc_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_bh_get_data_dtdc_clone]
	@NamLamViec int,
	@SoChungTu nvarchar(500),
	@SLNS nvarchar(500),
	@IDLoaiChi nvarchar(500)
AS
BEGIN
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		CASE WHEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0)) >0 THEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0))
				ELSE -(((ISNULL(ctct.fTienSoSanhGiam,0))-ISNULL(ctct.fTienSoSanhTang,0))) END FTienTangGiam
		into #temp
	from
	BH_DM_MucLucNganSach mlns
	left join
	BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_DTC_DieuChinhDuToanChi
		where iNamLamViec = @NamLamViec and bDaTongHop=1
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
	where mlns.iNamLamViec=@NamLamViec

	--- Get data
	select * INTO #result from
	(
		--- che do bao hiem
		select SUBSTRING(A.sXauNoiMa,1,20) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		where A.sLNS in (select * from splitstring('9010001,9010002'))
		Group by SUBSTRING(A.sXauNoiMa,1,20),A.IIdMaDonVi
		--- Cssk hssv va nld
		UNION ALL
		select SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		where A.sLNS in (select * from splitstring('905,9050001,9050002'))
		Group by SUBSTRING(A.sXauNoiMa,1,3),A.IIdMaDonVi
		--- KPQL, KCB quan y, KCB truong sa,  KCB BHYT , TTB Y Te, BHTN
		UNION ALL
		select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		where A.sLNS in (select * from splitstring('9010004,9010003,9010006,9010008,9010009,9010010'))
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi

		) as test

	select * from #result

	drop table #temp
	drop table #result

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_khc_clone]
	@NamLamViec int,
	@SoChungTu nvarchar(500),
	@SLNS nvarchar(500)
AS
BEGIN
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		ctct.fTienKeHoachThucHienNamNay
		into #temp
	from
	BH_DM_MucLucNganSach mlns
	left join
	BH_KHC_CheDoBHXH_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_CheDoBHXH
		where iNamLamViec = @NamLamViec and bDaTongHop=1
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_CheDoBHXH = ct.id
	--join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	----------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns  
	 left join 
	BH_KHC_KinhPhiQuanLy_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_KinhPhiQuanLy
		where iNamLamViec = @NamLamViec and bDaTongHop=1
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KinhPhiQuanLy = ct.iID_BH_KHC_KinhPhiQuanLy
	--join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	------------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns 
	left join
	BH_KHC_KCB_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_KCB
		where iNamLamViec = @NamLamViec and bDaTongHop=1
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KCB = ct.iID_BH_KHC_KCB
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns 
	left join BH_KHC_K_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_K
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_K = ct.iID_BH_KHC_K

	--- Get data
	select * INTO #result from
	(
		--- che do bao hiem
		select SUBSTRING(A.sXauNoiMa,1,20) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.fTienKeHoachThucHienNamNay) fTienKeHoachThucHienNamNay
		from #temp A
		where A.sLNS in (select * from splitstring('9010001,9010002'))
		Group by SUBSTRING(A.sXauNoiMa,1,20),A.IIdMaDonVi
		--- Cssk hssv va nld
		UNION ALL
		select SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.fTienKeHoachThucHienNamNay) fTienKeHoachThucHienNamNay
		from #temp A
		where A.sLNS in (select * from splitstring('905,9050001,9050002'))
		Group by SUBSTRING(A.sXauNoiMa,1,3),A.IIdMaDonVi
		--- KPQL, KCB quan y, KCB truong sa,  KCB BHYT , TTB Y Te, BHTN
		UNION ALL
		select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.fTienKeHoachThucHienNamNay) fTienKeHoachThucHienNamNay
		from #temp A
		where A.sLNS in (select * from splitstring('9010004,9010003,9010006,9010008,9010009,9010010'))
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi

		) as test

	select * from #result
	drop table #temp
	drop table #result

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';

	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = fSoThamDinh
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 254

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
		@iD as IID_QTC_Nam_KinhPhiQuanLy,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.sDuToanChiTietToi as SDuToanChiTietToi,
		ctct.iID_MaDonVi as IIdMaDonVi,
		ddv.sTenDonVi as STenDonVi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(daDuToan.fTienDuToan, 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
		ISNULL(ctct.fTienThua, 0)  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Nam_KinhPhiQuanLy in
					( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
								WHERE iNamLamViec=@NamLamViec
								AND ID_QTC_Nam_KinhPhiQuanLy=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	LEFT JOIN 
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ctct.iID_MaDonVi = ddv.iID_MaDonVi
	LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa,
				  iID_MaDonVi
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY iID_MaDonVi, 
		   sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
	Order by mlns.sXauNoiMa
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from dbo.splitstring(@Lns))


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoDuToanDuocDuyet) as iSoDuToanDuocDuyet,
			Sum(qtcn_ct.fTongTien_ThucChi) as fTongTien_ThucChi,
			Sum(qtcn_ct.iTongSo_ThucChi) as iTongSo_ThucChi,
			Sum(qtcn_ct.iSoSQ_ThucChi) as iSoSQ_ThucChi,
			Sum(qtcn_ct.fTienSQ_ThucChi) as fTienSQ_ThucChi,
			Sum(qtcn_ct.iSoQNCN_ThucChi) as iSoQNCN_ThucChi,
			Sum(qtcn_ct.fTienQNCN_ThucChi) as fTienQNCN_ThucChi,
			Sum(qtcn_ct.iSoCNVCQP_ThucChi) as iSoCNVCQP_ThucChi,
			Sum(qtcn_ct.fTienCNVCQP_ThucChi) as fTienCNVCQP_ThucChi,
			Sum(qtcn_ct.iSoLDHD_ThucChi) as iSoLDHD_ThucChi,
			Sum(qtcn_ct.fTienLDHD_ThucChi) as fTienLDHD_ThucChi,
			Sum(qtcn_ct.iSoHSQBS_ThucChi) as iSoHSQBS_ThucChi,
			Sum(qtcn_ct.fTienHSQBS_ThucChi) as fTienHSQBS_ThucChi,
			Sum(qtcn_ct.fTienDuToanDuyet) - Sum(qtcn_ct.fTongTien_ThucChi) as fTienThua,
			Sum(qtcn_ct.fTongTien_ThucChi) - Sum(qtcn_ct.fTienDuToanDuyet) as fTienThieu
			
		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_CheDoBHXH_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_CheDoBHXH as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sLoaiTroCap

IF @IsTongHop=0

		---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sDuToanChiTietToi,
			mucluc.sMoTa as sLoaiTroCap,
			daDuToan.fTienDuToan as fTienDuToanDuyet, 
			chi_tiet.fTongTien_ThucChi,
			chi_tiet.iTongSo_ThucChi,
			chi_tiet.iSoSQ_ThucChi,
			chi_tiet.fTienSQ_ThucChi,
			chi_tiet.iSoQNCN_ThucChi,
			chi_tiet.fTienQNCN_ThucChi,
			chi_tiet.iSoCNVCQP_ThucChi,
			chi_tiet.fTienCNVCQP_ThucChi,
			chi_tiet.iSoLDHD_ThucChi,
			chi_tiet.fTienLDHD_ThucChi,
			chi_tiet.iSoHSQBS_ThucChi,
			chi_tiet.fTienHSQBS_ThucChi
		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa,
				  iID_MaDonVi
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @INamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
		   GROUP BY iID_MaDonVi, 
		   sXauNoiMa
		) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
		order by mucluc.sXauNoiMa
ELSE
		---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sDuToanChiTietToi,
			mucluc.sMoTa as sLoaiTroCap,
			daDuToan.fTienDuToan as fTienDuToanDuyet, 
			chi_tiet.fTongTien_ThucChi,
			chi_tiet.iTongSo_ThucChi,
			chi_tiet.iSoSQ_ThucChi,
			chi_tiet.fTienSQ_ThucChi,
			chi_tiet.iSoQNCN_ThucChi,
			chi_tiet.fTienQNCN_ThucChi,
			chi_tiet.iSoCNVCQP_ThucChi,
			chi_tiet.fTienCNVCQP_ThucChi,
			chi_tiet.iSoLDHD_ThucChi,
			chi_tiet.fTienLDHD_ThucChi,
			chi_tiet.iSoHSQBS_ThucChi,
			chi_tiet.fTienHSQBS_ThucChi
		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		LEFT JOIN (
		-- lấy ra dữ liệu dự toán tren giao
		SELECT 
			  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
			  sXauNoiMa,
			  iID_MaDonVi
	   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
	   WHERE iID_DTC_DuToanChiTrenGiao IN
		   (SELECT ID
			FROM BH_DTC_DuToanChiTrenGiao
			WHERE sSoQuyetDinh <> ''
			  AND sSoQuyetDinh IS NOT NULL
			  AND iNamLamViec = @INamLamViec
			  AND bIsKhoa=1)
		 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
	   GROUP BY iID_MaDonVi, 
	   sXauNoiMa
	) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
		order by mucluc.sXauNoiMa

		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))
				and danhmuc.iTrangThai=1


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang)/@Donvitinh as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay)/@Donvitinh as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao)/@Donvitinh as fTien_TongDuToanDuocGiao,
			Sum(qtcn_ct.fTien_ThucChi)/@Donvitinh as fTien_ThucChi,
			Sum(qtcn_ct.fTienThua)/@Donvitinh as fTienThua,
			Sum(qtcn_ct.fTienThieu)/@Donvitinh as fTienThieu,
			Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan
			--CASE WHEN ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) >  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) THEN 
			--ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) -  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) ELSE 0 END fTienThua,

			--CASE WHEN ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) <  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) THEN 
			--ISNULL(Sum(qtcn_ct.fTien_ThucChi),0)- ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0)  ELSE 0 END fTienThieu,
			--Sum(qtcn_ct.fTien_TongDuToanDuocGiao) - Sum(qtcn_ct.fTien_ThucChi)  as fTienThua,
			--Sum(qtcn_ct.fTien_ThucChi) - Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTienThieu,
			--CASE WHEN ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) > 0 and ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) > 0 THEN 
			--ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) / ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0)  ELSE 0 END fTiLeThucHienTrenDuToan
			--Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan

		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_KCB_QuanYDonVi as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung



		---Kết quả hiển thị trả về
		select 

			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sDuToanChiTietToi,
			mucluc.sMoTa as sNoiDung,
			chi_tiet.fTien_DuToanNamTruocChuyenSang, 
			chi_tiet.fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTien_ThucChi,
			chi_tiet.fTienThua,
			chi_tiet.fTienThieu,
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa


		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.sDuToanChiTietToi,
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sXauNoiMa,
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoLuyKeCuoiQuyNay) as iSoLuyKeCuoiQuyNay,
			Sum(qtcn_ct.fTienLuyKeCuoiQuyNay) as fTienLuyKeCuoiQuyNay,
			sum(qtcn_ct.iSoSQ_DeNghi) as iSoSQ_DeNghi ,
			sum(qtcn_ct.fTienSQ_DeNghi) as fTienSQ_DeNghi ,
			sum(qtcn_ct.iSoQNCN_DeNghi) as iSoQNCN_DeNghi,
			sum(qtcn_ct.fTienQNCN_DeNghi) as fTienQNCN_DeNghi ,
			sum(qtcn_ct.iSoCNVCQP_DeNghi) as iSoCNVCQP_DeNghi ,
			sum(qtcn_ct.fTienCNVCQP_DeNghi) as fTienCNVCQP_DeNghi,
			sum(qtcn_ct.iSoHSQBS_DeNghi) as iSoHSQBS_DeNghi,
			sum(qtcn_ct.fTienHSQBS_DeNghi) as fTienHSQBS_DeNghi,
			sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
			sum(qtcn_ct.fTongTien_DeNghi) as fTongTien_DeNghi,
			sum(qtcn_ct.fTongTien_PheDuyet) as fTongTien_PheDuyet,
			sum(qtcn_ct.iSoLDHD_DeNghi) as iSoLDHD_DeNghi,
			sum(qtcn_ct.fTienLDHD_DeNghi) as fTienLDHD_DeNghi
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sXauNoiMa

		--- Get tien du toan 
		SELECT ctct.sXauNoiMa,
			SUM(ctct.fTienTuChi)  fTienDuToanDuyet
			into #tempDuToan
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @INamLamViec
				GROUP BY ctct.sXauNoiMa
	--- lay luy kê quy truoc
	SELECT SUM
			(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi,
			SUM (isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi,
			SUM (isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi,
			SUM (isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi,
			SUM (isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi,
			SUM (isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi,
			SUM (isnull(ctct.fTongTien_PheDuyet, 0)) fTongTien_PheDuyet,
			SUM (isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi,
			SUM (isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi,
			SUM (isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi,
			SUM (isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi,
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
			into #tempDuLieuQuyTruoc
		FROM
			BH_QTC_Quy_CheDoBHXH_ChiTiet ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @IQuy 
			and ct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
			and ct.iNamChungTu=@INamLamViec
		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa

	---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.sDuToanChiTietToi,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sLoaiTroCap,
			duToan.fTienDuToanDuyet,
			(
			isnull(duLieuQuyTruoc.fTienCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienSQ_DeNghi, 0) 
			) fTienLuyKeCuoiQuyTruoc,
			(
				isnull(duLieuQuyTruoc.iSoCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoSQ_DeNghi, 0) 
			) iSoLuyKeCuoiQuyTruoc,
			chi_tiet.iSoSQ_DeNghi,
			chi_tiet.fTienSQ_DeNghi,
			chi_tiet.iSoQNCN_DeNghi,
			chi_tiet.fTienQNCN_DeNghi,
			chi_tiet.iSoCNVCQP_DeNghi,
			chi_tiet.fTienCNVCQP_DeNghi,
			chi_tiet.iSoHSQBS_DeNghi,
			chi_tiet.fTienHSQBS_DeNghi,
			chi_tiet.iTongSo_DeNghi,
			chi_tiet.fTongTien_DeNghi,
			chi_tiet.fTongTien_PheDuyet,
			chi_tiet.iSoLDHD_DeNghi,
			chi_tiet.fTienLDHD_DeNghi

		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tempDuToan duToan on mucluc.sXauNoiMa=duToan.sXauNoiMa
		left join #tempDuLieuQuyTruoc duLieuQuyTruoc on chi_tiet.sXauNoiMa=duLieuQuyTruoc.sXauNoiMa
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
	drop table #tempDuToan;
	drop table #tempDuLieuQuyTruoc;

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.sXauNoiMa,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as FTienDuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as FTienDuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as FTienTongDuToanDuocGiao,
			sum(qtcn_ct.fTienThucChi) as FTienThucChi ,
			sum(qtcn_ct.fTienQuyetToanDaDuyet) as FTienQuyetToanDaDuyet ,
			sum(qtcn_ct.fTienDeNghiQuyetToanQuyNay) as FTienDeNghiQuyetToanQuyNay,
			sum(qtcn_ct.fTienXacNhanQuyetToanQuyNay) as FTienXacNhanQuyetToanQuyNay
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_KCB_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.sXauNoiMa,qtcn_ct.sNoiDung

   		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS FTienDuToanGiaoNamNay,
		  sXauNoiMa
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @INamLamViec
		  AND bIsKhoa=1
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
   GROUP BY sXauNoiMa
   --- Get nhan phan bo tren giao
   	SELECT 
		  SUM(fTienTuChi) AS FTienDuToanGiaoNamNay,
		  sXauNoiMa
		  into #tblNhanPhanBoTrenGiao
   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
   WHERE iID_DTC_DuToanChiTrenGiao IN
       (SELECT ID
        FROM BH_DTC_DuToanChiTrenGiao
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamLamViec = @INamLamViec
		  AND bIsKhoa=1
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
   GROUP BY sXauNoiMa

   SELECT SUM(CTCT.fTien_DuToanNamTruocChuyenSang)  fTien_DuToanNamTruocChuyenSang,
					CTCT.sXauNoiMa
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)
	group by CTCT.sXauNoiMa

   -- chung tu thuong
		if @IsTongHop=1
	---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			quy_truoc.fTien_DuToanNamTruocChuyenSang as FTienDuToanNamTruocChuyenSang,
			dt.FTienDuToanGiaoNamNay FTienDuToanGiaoNamNay,
			chi_tiet.FTienTongDuToanDuocGiao FTienTongDuToanDuocGiao,
			chi_tiet.fTienThucChi fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tblPhanBoDuToan as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		order by mucluc.sXauNoiMa
	else
		---- chung tu tong hop
		---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			quy_truoc.fTien_DuToanNamTruocChuyenSang as FTienDuToanNamTruocChuyenSang,
			dt.FTienDuToanGiaoNamNay FTienDuToanGiaoNamNay,
			chi_tiet.FTienTongDuToanDuocGiao FTienTongDuToanDuocGiao,
			chi_tiet.fTienThucChi fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tblNhanPhanBoTrenGiao as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		order by mucluc.sXauNoiMa
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int,
@IsTongHop4Quy bit,
@Loai int,
@MaDonVi nvarchar(100)
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.sDuToanChiTietToi,
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9010001', '9010002')
				and danhmuc.iTrangThai=1
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Nam_CheDoBHXH_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			qtcn_ct.iID_MaDonVi,
			qtcn_ct.iNamLamViec,
			qtcn_ct.sXauNoiMa,
			qtcn_ct.fTienDuToanDuyet, ---3
			qtcn_ct.iSoDuToanDuocDuyet, --2

			qtcn_ct.iTongSo_ThucChi,
			qtcn_ct.fTongTien_ThucChi, ---5

			qtcn_ct.iSoSQ_ThucChi, ---6
			qtcn_ct.fTienSQ_ThucChi, ---7

			qtcn_ct.iSoQNCN_ThucChi, ----8
			qtcn_ct.fTienQNCN_ThucChi,---9

			qtcn_ct.iSoCNVCQP_ThucChi,---10
			qtcn_ct.fTienCNVCQP_ThucChi, ----11

			qtcn_ct.iSoLDHD_ThucChi, ----13
			qtcn_ct.fTienLDHD_ThucChi, ---14

			qtcn_ct.iSoHSQBS_ThucChi, ----15
			qtcn_ct.fTienHSQBS_ThucChi, ---16

			Case when isnull(qtcn_ct.fTienDuToanDuyet,0) > isnull(qtcn_ct.fTongTien_ThucChi,0) then isnull(qtcn_ct.fTienDuToanDuyet,0) - isnull(qtcn_ct.fTongTien_ThucChi,0)  ELSE  0 end fTienThua,
			Case when isnull(qtcn_ct.fTongTien_ThucChi,0) > isnull(qtcn_ct.fTienDuToanDuyet,0) then isnull(qtcn_ct.fTongTien_ThucChi,0) - isnull(qtcn_ct.fTienDuToanDuyet,0)  ELSE  0 end fTienThieu,
			Case when isnull(qtcn_ct.fTienDuToanDuyet,0)>0 then isnull(qtcn_ct.fTongTien_ThucChi,0)/ isnull(qtcn_ct.fTienDuToanDuyet,0)  ELSE  0 end fTiLeThucHienTrenDuToan
		into #tblQuyetToanNamChiTiet
		from BH_QTC_Nam_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		where qtcn_ct.iID_QTC_Nam_CheDoBHXH = @IdChungTu;

IF @Loai=1
	---Kết quả hiển thị trả về
	select 
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		chi_tiet.ID_QTC_Nam_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,
		mucluc.sDuToanChiTietToi,
		chi_tiet.iNamLamViec,
		chi_tiet.iID_MaDonVi,
		ddv.sTenDonVi,
		daDuToan.fTienDuToan as fTienDuToanDuyet, 
		chi_tiet.iSoDuToanDuocDuyet,

		chi_tiet.iTongSo_ThucChi, 
		chi_tiet.fTongTien_ThucChi,

		chi_tiet.iSoSQ_ThucChi,
		chi_tiet.fTienSQ_ThucChi,

		chi_tiet.iSoQNCN_ThucChi,
		chi_tiet.fTienQNCN_ThucChi,

		chi_tiet.iSoCNVCQP_ThucChi,
		chi_tiet.fTienCNVCQP_ThucChi,

		chi_tiet.iSoLDHD_ThucChi,
		chi_tiet.fTienLDHD_ThucChi,

		chi_tiet.iSoHSQBS_ThucChi,
		chi_tiet.fTienHSQBS_ThucChi,

		chi_tiet.fTienThua,
		chi_tiet.fTienThieu,
		chi_tiet.fTiLeThucHienTrenDuToan
	from #tblMucLucNganSach as mucluc
	left join #tblQuyetToanNamChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	LEFT JOIN 
		(SELECT * FROM DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1 ) ddv ON chi_tiet.iID_MaDonVi = ddv.iID_MaDonVi
	LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa,
				  iID_MaDonVi
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @INamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@MaDonVi))-- donvi
		   GROUP BY iID_MaDonVi, 
		   sXauNoiMa
		) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
	order by mucluc.sXauNoiMa
ELSE
	select 
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		chi_tiet.ID_QTC_Nam_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,
		mucluc.sDuToanChiTietToi,
		chi_tiet.iNamLamViec,
		chi_tiet.iID_MaDonVi,
		ddv.sTenDonVi,
		daDuToan.fTienDuToan as fTienDuToanDuyet, 
		chi_tiet.iSoDuToanDuocDuyet,

		chi_tiet.iTongSo_ThucChi, 
		chi_tiet.fTongTien_ThucChi,

		chi_tiet.iSoSQ_ThucChi,
		chi_tiet.fTienSQ_ThucChi,

		chi_tiet.iSoQNCN_ThucChi,
		chi_tiet.fTienQNCN_ThucChi,

		chi_tiet.iSoCNVCQP_ThucChi,
		chi_tiet.fTienCNVCQP_ThucChi,

		chi_tiet.iSoLDHD_ThucChi,
		chi_tiet.fTienLDHD_ThucChi,

		chi_tiet.iSoHSQBS_ThucChi,
		chi_tiet.fTienHSQBS_ThucChi,

		chi_tiet.fTienThua,
		chi_tiet.fTienThieu,
		chi_tiet.fTiLeThucHienTrenDuToan
	from #tblMucLucNganSach as mucluc
	LEFT JOIN #tblQuyetToanNamChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	LEFT JOIN 
		(SELECT * FROM DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1 ) ddv ON chi_tiet.iID_MaDonVi = ddv.iID_MaDonVi
	LEFT JOIN (
		-- lấy ra dữ liệu dự toán tren giao
		SELECT 
			  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
			  sXauNoiMa,
			  iID_MaDonVi
	   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
	   WHERE iID_DTC_DuToanChiTrenGiao IN
		   (SELECT ID
			FROM BH_DTC_DuToanChiTrenGiao
			WHERE sSoQuyetDinh <> ''
			  AND sSoQuyetDinh IS NOT NULL
			  AND iNamLamViec = @INamLamViec
			  AND bIsKhoa=1)
		 AND iID_MaDonVi in  (SELECT * FROM f_split(@MaDonVi))-- donvi
	   GROUP BY iID_MaDonVi, 
	   sXauNoiMa
	) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
	order by mucluc.sXauNoiMa

	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanNamChiTiet;
end
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
	@IdChungTu uniqueidentifier,
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@SMaLoaiChi nvarchar(50),
	@IIdMaDonVi nvarchar(500),
	@DNgayChungTu datetime,
	@iQuyChungTu int,
	@INamLamViec int,
	@Loai int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@SLNS);
	SELECT * into #tempAgency from  f_split(@IIdMaDonVi);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_QUY_KCB_Chitiet 
									WHERE iID_QTC_Quy_KCB =@IdChungTu

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @INamLamViec 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @INamLamViec
 
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa

   	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblNhanPhanBoTrenGiao
   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
   WHERE iID_DTC_DuToanChiTrenGiao IN
       (SELECT ID
        FROM BH_DTC_DuToanChiTrenGiao
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamLamViec = @INamLamViec

		  AND bIsKhoa=1
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa


	SELECT DISTINCT iID_MucLucNganSach
	--, iID_MLNS_Cha 
	into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MucLucNganSach 


	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (SELECT * FROM #tempLNS)

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @INamLamViec 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = ''))
		OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		WHERE sTM !='0'
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	-- map bảng mlns ns và đơn vị
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns

	SELECT CTCT.* 
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IIdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)

	-- lay du lieu da quyet toan 
	SELECT  
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CT.iID_MaDonVi,
		CTCT.sXauNoiMa
		into #TemptblTienDaDuyet
	FROM
	BH_QTC_QUY_KCB CT
	INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	ON CT.ID_QTC_Quy_KCB=CTCT.iID_QTC_Quy_KCB
	WHERE CT.iNamChungTu = @INamLamViec
		  --AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IIdMaDonVi))
		  --AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu<@iQuyChungTu
	GROUP BY  CT.iID_MaDonVi,CTCT.sXauNoiMa

	--- chung tu thuong
	if 	@Loai=1	
	-- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS,
		mlnsPhanBo.iID_MLNS_Cha,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(tblQuyTruoc.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	------ Chứng từ tong hop
	ELSE 
	---- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS ,
		mlnsPhanBo.iID_MLNS_Cha ,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(ctct.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblNhanPhanBoTrenGiao daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa 
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 3/20/2024 3:01:09 PM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]
@idDonvi nvarchar(50),
@sLns nvarchar(max),
@NamLamViec int,
@DotNhan int,
@MaLoaichi nvarchar(100),
@DVT int
AS
BEGIN

SELECT 
	mucluc.iID_MLNS,
	mucluc.iID_MLNS_Cha,
	mucluc.sXauNoiMa,
	mucluc.sLNS,
	mucluc.sL,
	mucluc.sK,
	mucluc.sM,
	mucluc.sTM,
	mucLuc.sTTM,
	mucluc.sNG,
	mucluc.sMoTa AS sNoiDung,
	mucluc.sDuToanChiTietToi,
	mucluc.bHangChaDuToan as bHangCha,
	d.fTienHienVat/ @DVT as fTienHienVat,
	d.fTienTuChi/ @DVT as fTienTuChi,
	d.fTongTien/ @DVT as fTongTien

	FROM BH_DM_MucLucNganSach AS mucluc
		left join (SELECT  
						sum(c.fTienHienVat) AS fTienHienVat,
						sum(c.fTienTuChi) AS fTienTuChi,
						sum(c.fTongTien) AS fTongTien,
						c.sXauNoiMa,
						c.iID_MaDonVi
		
						FROM 
							(SELECT 
								b.fTongTien,
								b.fTienHienVat,
								b.fTienTuChi,
								b.sXauNoiMa,
								a.sLNS,
								a.iID_MaDonVi
								FROM BH_DTC_DuToanChiTrenGiao AS a
								left join BH_DTC_DuToanChiTrenGiao_ChiTiet AS b on a.ID = b.iID_DTC_DuToanChiTrenGiao
								WHERE a.iID_MaDonVi = @idDonvi
								and a.iNamLamViec = @NamLamViec
								and a.iLoaiDotNhanPhanBo=@DotNhan
								and b.sMaLoaiChi=@MaLoaichi) AS c
						GROUP BY c.sXauNoiMa, c.iID_MaDonVi) AS d on  mucluc.sXauNoiMa = d.sXauNoiMa
	WHERE mucluc.sLNS in  (SELECT * FROM f_split(@sLns)) and mucluc.iNamLamViec = @NamLamViec and mucluc.bHangChaDuToan is not null
	ORDER BY mucluc.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_getdata_chungtu_theoloaichi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE  [dbo].[sp_bhxh_getdata_chungtu_theoloaichi]
	@YearOfWork int,
	@DotNhat int,
	@MaLoaiChi nvarchar(50)
AS
BEGIN
	if @DotNhat=0
		select ct.* from BH_DTC_PhanBoDuToanChi ct
		where ID in (
			select iID_DTC_PhanBoDuToanChi from BH_DTC_PhanBoDuToanChi_ChiTiet
			where iNamLamViec=@YearOfWork and sMaLoaiChi=@MaLoaiChi
		)
		and iNamChungTu=@YearOfWork
		ORDER BY ct.dNgayQuyetDinh DESC
	else
	select ct.* from BH_DTC_PhanBoDuToanChi ct
		where ID in (
			select iID_DTC_PhanBoDuToanChi from BH_DTC_PhanBoDuToanChi_ChiTiet
			where iNamLamViec=@YearOfWork and sMaLoaiChi=@MaLoaiChi
		)
		and iNamChungTu=@YearOfWork
		and iLoaiDotNhanPhanBo=@DotNhat
		ORDER BY ct.dNgayQuyetDinh DESC
END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	a.bHangChaDuToan,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTienTuChi,
	A.iNamlamViec,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
		LEFT JOIN (
					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_CheDoBHXH_ChiTiet ctct
					left join BH_KHC_CheDoBHXH ct on ctct.iID_KHC_CheDoBHXH=ct.ID
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1
					UNION All

					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_KinhPhiQuanLy_ChiTiet ctct
					left join BH_KHC_KinhPhiQuanLy ct on ctct.iID_KHC_KinhPhiQuanLy=ct.iID_BH_KHC_KinhPhiQuanLy
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1

					UNION ALL
					select
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_KCB_ChiTiet ctct
					left join BH_KHC_KCB ct on ctct.iID_KHC_KCB=ct.iID_BH_KHC_KCB
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1

					UNION ALL
					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_K_ChiTiet ctct
					left join BH_KHC_K ct on ctct.iID_KHC_K=ct.iID_BH_KHC_K
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1
			
			) AS B
		ON A.sXauNoiMa = B.sXauNoiMa
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
		--AND a.bHangChaDuToan IS NOT NULL
	order by A.sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50),
@DNgayChungTu datetime

AS
BEGIN
		-- Get dieu chinh
		SELECT 
			ct.sXauNoiMa,
			Sum(isnull(ct.fTienThucHien06ThangDauNam,0)) fTienThucHien06ThangDauNam,
			Sum(isnull(ct.fTienUocThucHien06ThangCuoiNam,0)) fTienUocThucHien06ThangCuoiNam
			into #tempDieuChinh
		FROM
			(
				SELECT
					--bh.iID_MaDonVi,
					bh.sMoTa,
					ddv.sTenDonVi,
					bhct.*
				FROM 
					BH_DTC_DieuChinhDuToanChi bh
				JOIN 
					BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
				LEFT JOIN 
					(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
				WHERE
					bh.iID_MaDonVi in (SELECT * FROM splitstring(@IIDDonVi))
					AND	bh.iNamLamViec=@NamLamViec
					--and bh.bIsKhoa=1
					and bh.iLoaiTongHop=2
					--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
			) ct
			Group by ct.sXauNoiMa

		-- get phan bo đầu nam 
		SELECT 
		ct.iNamLamViec,
		ct.sXauNoiMa,
		Sum(ISNULL(ct.fTienTuChi, 0))as FTienTuChi
		into #tempNhanpbt
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DuToanChiTrenGiao bh
			JOIN 
				BH_DTC_DuToanChiTrenGiao_ChiTiet bhct ON bh.ID = bhct.iID_DTC_DuToanChiTrenGiao 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IIDDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@DNgayChungTu as date)
				AND bh.iNamLamViec=@NamLamViec
		) ct
		
		Group by 
		ct.sXauNoiMa,
		ct.iNamLamViec

		-- get data
		SELECT 
		A.sLNS,
		A.sTM,
		A.sTTM,
		A.sM,
		A.sNG,
		A.sMoTa AS sNoiDung,
		A.sXauNoiMa,
		A.iID_MLNS as IID_MucLucNganSach,
		A.iID_MLNS_Cha as IdParent,
		A.bHangChaDuToan bHangCha,
		A.bHangChaDuToan as isHangCha,
		A.iID_MLNS AS iID_MucLucNganSach,
		C.FTienTuChi fTienDuToanDuocGiao,
		B.fTienThucHien06ThangDauNam,
		B.fTienUocThucHien06ThangCuoiNam,
		A.sCPChiTietToi,
		A.sDuToanChiTietToi,
		A.bHangCha as IsHangCha,
		A.bHangCha ,
		A.bHangChaDuToan,
		@NamLamViec iNamlamViec,
		@IIDDonVi as iID_MaDonVi
		FROM BH_DM_MucLucNganSach AS A
		LEFT JOIN #tempDieuChinh AS B
		ON A.sXauNoiMa = B.sXauNoiMa
		LEFT JOIN #tempNhanpbt AS C
		ON A.sXauNoiMa=C.sXauNoiMa
		WHERE   A.sLNS IN (SELECT * FROM f_split( @sLns))
			AND A.iNamlamViec=@NamLamViec
			--AND A.bHangChaDuToan IS NOT NULL
	order by A.sXauNoiMa


END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	DTC.ID,
	DTC.sSoChungTu,
	DTC.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	DTC.dNgayChungTu,
	DTC.sSoQuyetDinh,
	DTC.dNgayQuyetDinh,
	DTC.sLNS,
	DTC.fTongTienTuChi,
	DTC.fTongTienHienVat,
	DTC.fTongTien,
	DTC.iLoaiDotNhanPhanBo,
	DTC.sMoTa,
	DTC.iNamLamViec,
	DTC.sNguoiTao,
	DTC.bIsKhoa,
	BH_DM_LoaiChi.sTenDanhMucLoaiChi AS sLoaiChi,
	DTC.sMaLoaiChi,
	DTC.iID_LoaiDanhMucChi
	FROM BH_DTC_DuToanChiTrenGiao DTC
	LEFT JOIN DonVi DV ON DTC.iID_MaDonVi = DV.iID_MaDonVi
	INNER JOIN BH_DM_LoaiChi ON DTC.iID_LoaiDanhMucChi = BH_DM_LoaiChi.iID
	WHERE DV.iNamLamViec = @YearOfWork and DTC.iNamLamViec = @YearOfWork
	ORDER BY DTC.dNgayQuyetDinh DESC
END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[sp_bhxh_nhandutoanchitrengiao_index_clone]
	@YearOfWork int
AS
BEGIN
	SELECT
	DTC.ID,
	DTC.sSoChungTu,
	DTC.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	DTC.dNgayChungTu,
	DTC.sSoQuyetDinh,
	DTC.dNgayQuyetDinh,
	DTC.sLNS,
	DTC.fTongTienTuChi,
	DTC.fTongTienHienVat,
	DTC.fTongTien,
	DTC.iLoaiDotNhanPhanBo,
	DTC.sMoTa,
	DTC.iNamLamViec,
	DTC.sNguoiTao,
	DTC.bIsKhoa,
	--BH_DM_LoaiChi.sTenDanhMucLoaiChi AS sLoaiChi,
	DTC.sMaLoaiChi,
	DTC.iID_LoaiDanhMucChi
	FROM BH_DTC_DuToanChiTrenGiao DTC
	LEFT JOIN DonVi DV ON DTC.iID_MaDonVi = DV.iID_MaDonVi
	--INNER JOIN BH_DM_LoaiChi ON DTC.iID_LoaiDanhMucChi = BH_DM_LoaiChi.iID
	WHERE DV.iNamLamViec = @YearOfWork and DTC.iNamLamViec = @YearOfWork
	ORDER BY DTC.dNgayQuyetDinh DESC
END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangChaDuToan bHangCha,
	A.bHangChaDuToan as isHangCha,
	B.ID,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTongTien,
	B.fTienHienVat,
	B.fTienTuChi,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi,
	 B.dNgaySua,
	 B.dNgayTao,
	 B.sNguoiSua,
	 B.sNguoiTao
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN ( SELECT ctct.*
					FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
				LEFT JOIN BH_DTC_DuToanChiTrenGiao CT ON ctct.iID_DTC_DuToanChiTrenGiao=CT.ID 
				WHERE ctct.iID_DTC_DuToanChiTrenGiao = @iDNdtctg
					 and ct.iID_MaDonVi=@IIDDonVi 
					 And CT.iNamLamViec=@NamLamViec) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split( @sLns))
		AND A.iNamlamViec=@NamLamViec
		AND A.bHangChaDuToan IS NOT NULL
	order by A.sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_phanbodutoanchi_index_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE  [dbo].[sp_bhxh_phanbodutoanchi_index_clone]
	@YearOfWork int
AS
BEGIN
	SELECT
	PBDTC.ID,
	PBDTC.sSoChungTu,
	PBDTC.dNgayChungTu,
	PBDTC.sSoQuyetDinh,
	PBDTC.dNgayQuyetDinh,
	PBDTC.iNamChungTu,
	PBDTC.iLoaiDotNhanPhanBo,
	PBDTC.sMoTa,
	PBDTC.sLNS,
	PBDTC.fTongTien,
	PBDTC.fTongTienTuChi,
	PBDTC.fTongTienHienVat,
	PBDTC.bIsKhoa,
	PBDTC.sNguoiTao,
	PBDTC.sNguoiSua,
	PBDTC.dNgayTao,
	PBDTC.bIsKhoa,
	PBDTC.sDotNhan,
	PBDTC.sID_MaDonVi,
	PBDTC.iLoaiChungTu,
	PBDTC.iID_DotNhan,
	--BH_DM_LoaiChi.sTenDanhMucLoaiChi AS sLoaiChi,
	PBDTC.sMaLoaiChi,
	PBDTC.iID_LoaiDanhMucChi
	FROM BH_DTC_PhanBoDuToanChi PBDTC
	--INNER JOIN BH_DM_LoaiChi ON PBDTC.iID_LoaiDanhMucChi = BH_DM_LoaiChi.iID
	WHERE
	 PBDTC.iNamChungTu = @YearOfWork
	ORDER BY PBDTC.dNgayQuyetDinh DESC
END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
	@VoucherId uniqueidentifier ,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId

	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 



		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

	SELECT DISTINCT iID_MucLucNganSach
	--, iID_MLNS_Cha 
	into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MucLucNganSach 


	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (SELECT * FROM #tempLNS)

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		WHERE sTM !='0'
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	-- map bảng mlns ns và đơn vị
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		WHERE sLNS in (select * from splitstring(@LNS)) and  bHangCha = 1
			and( sL <>'' or sL is not null) and (sCPChiTietToi <>'' or sCPChiTietToi is not null)
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE sLNS in (select * from splitstring(@LNS)) and  bHangCha = 1
			and( sL ='' or sL is null) 
			and (sCPChiTietToi ='' or sCPChiTietToi is null)
	) mlns

	SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap) fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
	

	---- lấy dữ liệu đã cấp
	--SELECT SUM(fTienDuToan) AS fTienDuToan,
	--	  SUM(fTienKeHoachCap) AS fTienKeHoachCap,
	--	  SUM(fTienDaCap) AS fTienDaCap,
 --         iID_MaDonVi,
 --         iID_MucLucNganSach
	--	  into #tblDataDaCapExist
	--FROM BH_CP_ChungTu_ChiTiet
	--WHERE iID_CP_ChungTu IN
 --      (
	--	SELECT iID_CP_ChungTu
 --       FROM BH_CP_ChungTu
 --       WHERE iNamChungTu = @YearOfWork
	--	  AND iID_LoaiCap = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
	--	  AND iID_CP_ChungTu=@VoucherId
	--	)
	--	AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach

	--SELECT sum(fTienDuToan) AS fTienDuToan, sum(fTienKeHoachCap) AS fTienKeHoachCap, sum(fTienDaCap) fTienDaCap, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
	--	SELECT * FROM #tblDataDaCapExist
	--	) data
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	----select * from  #tblMlnsRoot
	--SELECT mlns.iID_MLNS,
	--	   mlns.iID_MLNS_Cha,
	--	   mlns.sXauNoiMa,
	--	   fTienDuToan,
	--	   fTienDaCap,
	--	   fTienKeHoachCap,
	--	   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
	--	   INTO #tblDaCapDuToanExist
	--FROM #tblMlnsRoot mlns
	--LEFT JOIN
	--  #tblDataDaCapDuToanExist daCapDuToan
	--ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	--SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToan) AS fTienDuToan, sum(fTienDaCap) AS fTienDaCap, SUM(fTienKeHoachCap) as fTienKeHoachCap INTO #tblDaCapDuToanResultExist FROM (
	--	SELECT distinct T.iID_MLNS,  
	--		   T.iID_MLNS_Cha,
	--		   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
	--		   T.iID_MaDonVi,
	--		   T.fTienDuToan,
	--		   T.fTienDaCap,
	--		   T.fTienKeHoachCap
	--	FROM #tblDaCapDuToanExist T 
	--	WHERE T.fTienDaCap <> 0 OR T.fTienDuToan <> 0 OR t.fTienKeHoachCap<>0) data
	--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--option (maxrecursion 0)



	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		isnull(daCapDuToan.fTienDuToan, 0) as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	on mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MucLucNganSach and daCapDuToan.iID_MaDonVi = mlnsPhanBo.iID_MaDonVi 
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	where mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	
END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]
	@VoucherId uniqueidentifier ,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId
	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and iTrangThai=1
	and sLNS in (select * from splitstring(@LNS))
	--and  bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

   SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap)  fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa

	-- tao bang tam chua don vi
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	
	
	-- Tao bang tam luu chu mlns cha co don vi
	SELECT * INTO #tblMlnsExistDonVi
	FROM #tblNsMlns ,#tblDonVi dv
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'
	
	-- Tao bang tam luu chu mlns va tien du toan chi che do bhxh
	SELECT ml.sXauNoiMa,
		SUM(cast(pb.fTienDuToan as float)) fTienDuToan ,
		ml.iID_MaDonVi 
		INTO #tempMlnsbhxh 
		FROM #tblMlnsExistDonVi ml
   LEFT JOIN #tblPhanBoDuToan pb ON pb.iID_MucLucNganSach=ml.iID_MLNS and ml.iID_MaDonVi=pb.iID_MaDonVi
   GROUP BY ml.sXauNoiMa,ml.iID_MaDonVi
		ORDER BY ml.sXauNoiMa

	SELECT SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa,
		A.iID_MaDonVi, 
		SUM(cast(A.fTienDuToan as float)) fTienDuToan
		INTO #tblDaCapDuToanResult
		FROM #tempMlnsbhxh  A
		GROUP BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi
		ORDER BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi

	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		daCapDuToan.fTienDuToan as FTienDuToan,
		(cast(isnull(ctct.fTienKeHoachCap, 0) as float)) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsExistDonVi AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	on mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa and daCapDuToan.iID_MaDonVi= mlnsPhanBo.iID_MaDonVi
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	where mlnsPhanBo.sLNS ='901'
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;

	drop table #tempDuToanCapQuyTruoc
	drop table #tblDaCapDuToanResult
	drop table #tblMlnsExistDonVi
	drop table #tempMlnsbhxh
	drop table #tblNsMlns
	drop table #tblDonVi
END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]
	@VoucherId uniqueidentifier ,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId
	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and iTrangThai=1
	and sLNS in (select * from splitstring(@LNS))
	--and  bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

	-- lấy dữ liệu đã cấp
	   SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap) fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
	--SELECT ISNULL(SUM(fTienDuToan),0) AS fTienDuToan,
	--	  ISNULL(SUM(fTienKeHoachCap),0)  AS fTienKeHoachCap,
	--	  ISNULL(SUM(fTienDaCap),0)  AS fTienDaCap,
 --         iID_MaDonVi,
 --         iID_MucLucNganSach
	--	  into #tblDataDaCap
	--FROM BH_CP_ChungTu_ChiTiet
	--WHERE iID_CP_ChungTu IN
 --      (
	--	SELECT iID_CP_ChungTu
 --       FROM BH_CP_ChungTu
 --       WHERE iNamChungTu = @YearOfWork
	--	  AND iID_LoaiCap = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
	--	  AND iID_CP_ChungTu=@VoucherId
	--	)
	--	AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach

	-- tao bang tam chua don vi
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	
	
	
	-- Tao bang tam luu chu mlns cha co don vi
	SELECT *,'' AS iID_MaDonVi, '' AS sTenDonVi   INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblNsMlns 
	WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) 
	
		-- Tao bang tam luu chu mlns cha co don vi
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblNsMlns ,#tblDonVi dv
		WHERE  bHangCha=0

		-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	
	---- Tao bang tam luu chu mlns va tien du toan chi che do bhxh
	--SELECT ml.sXauNoiMa,
	--	SUM(pb.fTienDuToan) fTienDuToan ,
	--	ml.iID_MaDonVi 
	--	INTO #tempMlnsbhxh 
	--	FROM #tblMlnsExistDonVi ml
 --  LEFT JOIN #tblPhanBoDuToan pb ON pb.iID_MucLucNganSach=ml.iID_MLNS and ml.iID_MaDonVi=pb.iID_MaDonVi
 --  GROUP BY ml.sXauNoiMa,ml.iID_MaDonVi
	--	ORDER BY ml.sXauNoiMa

	--SELECT SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa,
	--	A.iID_MaDonVi, 
	--	SUM(A.fTienDuToan) fTienDuToan
	--	INTO #tblDaCapDuToanResult
	--	FROM #tempMlnsbhxh  A
	--	GROUP BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi
	--	ORDER BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi

	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		0 as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMLNS AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
				
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	--LEFT JOIN #tempMlnsbhxh daCapDuToan
	--on mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa and daCapDuToan.iID_MaDonVi= mlnsPhanBo.iID_MaDonVi
	
	order by mlnsPhanBo.sXauNoiMa ,mlnsPhanBo.iID_MaDonVi;

END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_kehoach_bh]
	@IdMaDonVi NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@Donvitinh int,
	@iIdLoaiCap uniqueidentifier,
	@MaLoaiChi NVARCHAR(100)
AS
BEGIN
		select 
			row_number() over (order by ctct.iID_MaDonVi) as STT,
			isnull(dt.fTienDuToan,0) /@Donvitinh as FTienDuToan,
			sum(ctct.fTienDaCap)/@Donvitinh as FTienDaCap, 
			sum(ctct.fTienKeHoachCap)/@Donvitinh as FTienKeHoachCap,
			ctct.sGhiChu,
			ctct.iID_MaDonVi 
			into #tblkehoach
		from BH_CP_ChungTu_ChiTiet as ctct
		left join BH_CP_ChungTu as ct on ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
		left join 
		(
				SELECT 
				  SUM(fTienTuChi) AS fTienDuToan,

				  iID_MaDonVi
				FROM BH_DTC_PhanBoDuToanChi_ChiTiet
				   WHERE iID_DTC_PhanBoDuToanChi IN
					   (SELECT ID
						FROM BH_DTC_PhanBoDuToanChi
						WHERE sSoQuyetDinh <> ''
						  AND sSoQuyetDinh IS NOT NULL
						  AND iNamChungTu = @NamLamViec
						  AND bIsKhoa=1
						  )
					 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
					 AND sMaLoaiChi=@MaLoaiChi
				   GROUP BY iID_MaDonVi
		) dt on dt.iID_MaDonVi=ctct.iID_MaDonVi
		where ctct.iID_MaDonVi In (SELECT * FROM f_split(@IdMaDonVi))
			and ct.iNamChungTu = @NamLamViec
			--and ct.iLoaiTongHop <> 2
			and ct.iQuy = @IQuy
			and ct.sNguoiTao=@UserName
			and ct.iID_LoaiCap=@iIdLoaiCap
			group by ctct.iID_MaDonVi,dt.fTienDuToan,ctct.sGhiChu
			--,ct.sLNS


		select kh.*,dv.iID_MaDonVi,dv.sTenDonVi from #tblkehoach kh
		left join DonVi dv on kh.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iNamLamViec=@NamLamViec
		order by STT

		drop table #tblkehoach
	
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]
	 @NamLamViec int,
	 @IDLoaiCap nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @Dvt int,
	 @Quy int,
	 @MaLoaiChi nvarchar(100)
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
		SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblLNS
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		
		SELECT 
				ctct.iID_MaDonVi,
				dv.sTenDonVi,
				mlns.sLNS as SDSLNS,
				ctct.sGhiChu,
				SUM(ctct.fTienDaCap)/@Dvt FTienDuToan,
				SUM(ctct.fTienDuToan)/@Dvt FTienDaCap,
				SUM(ctct.fTienKeHoachCap)/@Dvt FTienKeHoachCap
		FROM 
				#tblLNS mlns
		LEFT JOIN 
				(SELECT * FROM BH_CP_ChungTu_ChiTiet 
						WHERE iID_CP_ChungTu IN
						(SELECT iID_CP_ChungTu FROM BH_CP_ChungTu
							WHERE iID_LoaiCap=@IDLoaiCap
								AND iQuy=@Quy
								AND iNamChungTu=@NamLamViec)
						AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))) ctct
			ON mlns.iID_MLNS = ctct.iID_MucLucNganSach 
		LEFT JOIN 
			(
					SELECT 
					  SUM(fTienTuChi) AS fTienDuToan,
					  sXauNoiMa,
					  iID_MaDonVi
					FROM BH_DTC_PhanBoDuToanChi_ChiTiet
					   WHERE iID_DTC_PhanBoDuToanChi IN
						   (SELECT ID
							FROM BH_DTC_PhanBoDuToanChi
							WHERE sSoQuyetDinh <> ''
							  AND sSoQuyetDinh IS NOT NULL
							  AND iNamChungTu = @NamLamViec
							  --AND iID_LoaiDanhMucChi = @iIdLoaiCap
							  AND bIsKhoa=1
							  )
						 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
						 AND sMaLoaiChi=@MaLoaiChi
					   GROUP BY iID_MaDonVi,sXauNoiMa
			) dt on dt.sXauNoiMa=ctct.sXauNoiMa
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi=dv.iID_MaDonVi
		WHERE dv.iNamLamViec=@NamLamViec
		GROUP BY CTCT.iID_MaDonVi,dv.sTenDonVi,mlns.sLNS,ctct.sGhiChu

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@SLNS nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec,
	
		Sum(ISNULL(ct.fTienTuChi, 0))as FTienTuChi
		into #temp1
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DuToanChiTrenGiao bh
			JOIN 
				BH_DTC_DuToanChiTrenGiao_ChiTiet bhct ON bh.ID = bhct.iID_DTC_DuToanChiTrenGiao 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
				--AND bh.iID_LoaiDanhMucChi=@loaiDanhMucCapChi
				AND bh.iNamLamViec=@NamLamViec
		) ct
		Group by 
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec
		;

		SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as iID_MucLucNganSach,
			   tbl.FTienTuChi as fTienTuChi,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung,
			   dm.sXauNoiMa
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #temp1  tbl 
			on dm.sXauNoiMa=tbl.sXauNoiMa
			where dm.iNamLamViec=@NamLamViec 
			and dm.sLNS in (select * from splitstring(@SLNS))
			and dm.bHangChaDuToan is not null
			order by dm.sXauNoiMa

			drop table #temp1

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone]
	@NamLamViec int,
	@IdDonVi nvarchar(200),
	@DNgayChungTu Datetime
AS
BEGIN
	DECLARE @CountIndex INT;
	
					Select 
					fThu_BHYT_NLD,
					fThu_BHYT_NSD,
					iID_MaDonVi,
					sXauNoiMa,
					iNamLamViec
					into #temp1
					from  BH_DTT_BHXH_ChungTu_ChiTiet
					where iID_DTT_BHXH 
					in ( select iID_DTT_BHXH from BH_DTT_BHXH_ChungTu
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
									AND iNamLamViec=@NamLamViec
									AND bIsKhoa=1
									AND CONVERT(DATE,dNgayChungTu)<= CONVERT(DATE,@DNgayChungTu)
					)
					and (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec


					SELECT 
					fThuBHYT_NSD_QTCuoiNam,
					fThuBHYT_NLD_QTCuoiNam,
					fThuBHYT_NSD_QTDauNam,
					fThuBHYT_NLD_QTDauNam,
					iID_MaDonVi,
					sXauNoiMa,
					iNamLamViec
					into #temp2
					 FROM BH_DTT_BHXH_DieuChinh_ChiTiet 
					WHERE iID_DTT_BHXH_DieuChinh IN
					(	
							SELECT iID_DTT_BHXH_DieuChinh FROM BH_DTT_BHXH_DieuChinh
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND bIsKhoa=1
							AND iNamLamViec=@NamLamViec
							AND CONVERT(DATE,dNgayChungTu)<= CONVERT(DATE,@DNgayChungTu)

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec

			SELECT 
                    (sum
						(
						(	
						((ISNULL(B.fThuBHYT_NSD_QTCuoiNam,0))  +  (ISNULL(B.fThuBHYT_NLD_QTCuoiNam,0)) 
						+ (ISNULL(B.fThuBHYT_NSD_QTDauNam ,0))   + (ISNULL(B.fThuBHYT_NLD_QTDauNam,0))
						)
						- (A.fThu_BHYT_NLD+A.fThu_BHYT_NSD)
							)
					*10)
					)
					/100 as fTienTuChi,
					A.iID_MaDonVi
					into #tempTotal
				 FROM  #temp2 B
				 LEFT JOIN #temp1 A ON A.iID_MaDonVi=B.iID_MaDonVi and a.sXauNoiMa=B.sXauNoiMa
				  group by B.fThuBHYT_NSD_QTCuoiNam,B.fThuBHYT_NLD_QTCuoiNam
				, B.fThuBHYT_NSD_QTDauNam,B.fThuBHYT_NLD_QTDauNam,A.fThu_BHYT_NLD,A.fThu_BHYT_NSD,A.iID_MaDonVi
			
			SELECT @CountIndex = COUNT(*) 
				FROM #tempTotal
		IF @CountIndex>0
		BEGIN
		SELECT 
                    NEWID() as iID_MLNS,
                    N'10 % số điều chỉnh thu BHYT quân nhân' as SNoiDung,
                    Sum(ISNULL(A.fTienTuChi,0))   as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    2 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM,
					'9010004' sXauNoiMa

				 FROM #tempTotal A
					 group by A.iID_MaDonVi
              UNION
              SELECT 
                    NEWID() as iID_MLNS,
                    N'Số còn lại' as SNoiDung,
                    0 as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    1 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM,
					'9010004' sXauNoiMa
					;
	END
	ELSE
	BEGIN 
		SELECT 
                    NEWID() as iID_MLNS,
                    N'10 % số điều chỉnh thu BHYT quân nhân' as SNoiDung,
                    0   as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    2 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM,
					'9010004' sXauNoiMa
              UNION
              SELECT 
                    NEWID() as iID_MLNS,
                    N'Số còn lại' as SNoiDung,
                    0 as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    1 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM,
					'9010004' sXauNoiMa
					;
	END
			drop table #temp1
			drop table #temp2
			drop table #tempTotal
END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@SLNS nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec,
	
		Sum(ISNULL(ct.fTienTuChi, 0))as FTienTuChi
		into #temp1
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_PhanBoDuToanChi bh
			JOIN 
				BH_DTC_PhanBoDuToanChi_ChiTiet bhct ON bh.ID = bhct.iID_DTC_PhanBoDuToanChi 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
				AND bh.iNamChungTu=@NamLamViec
		) ct
		Group by 
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec
		;

		SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as iID_MLNS,
			   tbl.FTienTuChi as fTienTuChi,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung,
			   dm.sXauNoiMa
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #temp1  tbl 
			on dm.sXauNoiMa=tbl.sXauNoiMa
			where dm.iNamLamViec=@NamLamViec 
			and dm.sLNS in (select * from splitstring(@SLNS))
			and dm.bHangChaDuToan is not null
			order by dm.sXauNoiMa

			drop table #temp1

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@IdChungTu nvarchar(4000)
AS BEGIN
SET NOCOUNT ON;

SELECT DISTINCT 
		donvi.iID_DonVi AS Id,
		donvi.iID_MaDonVi as IIDMaDonVi,
		donvi.sTenDonVi as TenDonVi,
		donvi.sKyHieu as KyHieu,
		donvi.sMoTa as MoTa,
		donvi.iLoai as Loai,
		donvi.iNamLamViec as NamLamViec,
		donvi.iTrangThai as iTrangThai

FROM BH_DTC_PhanBoDuToanChi chungtu
INNER JOIN  BH_DTC_PhanBoDuToanChi_ChiTiet  chitiet ON chungtu.ID = chitiet.iID_DTC_PhanBoDuToanChi
INNER JOIN
(
SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @NamLamViec
) donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chitiet.iNamLamViec = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiDotNhanPhanBo=@DotNhan
  AND chitiet.sMaLoaiChi=@MaLoaichi
  AND chungTu.ID in (select * from f_split(@IdChungTu))
	END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@IdChungTu nvarchar(4000),
@DotNhan int
AS BEGIN
SET NOCOUNT ON;

SELECT DISTINCT 
		donvi.iID_DonVi AS Id,
		donvi.iID_MaDonVi as IIDMaDonVi,
		donvi.sTenDonVi as TenDonVi,
		donvi.sKyHieu as KyHieu,
		donvi.sMoTa as MoTa,
		donvi.iLoai as Loai,
		donvi.iNamLamViec as NamLamViec,
		donvi.iTrangThai as iTrangThai


FROM BH_DTC_PhanBoDuToanChi chungtu
INNER JOIN  BH_DTC_PhanBoDuToanChi_ChiTiet  chitiet ON chungtu.ID = chitiet.iID_DTC_PhanBoDuToanChi
INNER JOIN
(
SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @NamLamViec
) donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chitiet.iNamLamViec = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.iLoaiDotNhanPhanBo=@DotNhan
  AND chitiet.sMaLoaiChi=@MaLoaichi
  AND chungTu.ID in (select * from f_split(@IdChungTu))
	END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]
	@VoucherId uniqueidentifier,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi uniqueidentifier,
	@iQuyChungTu int,
	@Loai int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
									WHERE iID_QTC_Quy_KinhPhiQuanLy =@VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToanDuocGiao,
          0 AS fTienDeNghiQuyetToanQuyNay,
          0 AS fTienQuyetToanDaDuyet,
		  0 AS fTienThucChi,
		  0 AS fTienXacNhanQuyetToanQuyNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          --AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa

	SELECT DISTINCT iID_MucLucNganSach
	--, iID_MLNS_Cha 
	into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MucLucNganSach 


	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (SELECT * FROM #tempLNS)

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		WHERE sTM !='0'
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	-- map bảng mlns ns và đơn vị
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns

	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CT.iID_MaDonVi,
		CTCT.sXauNoiMa
		into #tblDataQuyetToanDaDuyetQuyTruoc
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  --AND CT.ID_QTC_Quy_KinhPhiQuanLy=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.sXauNoiMa

	SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							into #tempDuToanTrenGiao
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct 
							ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
		WHERE ct.iID_MaDonVi = @AgencyId
		AND BIsKhoa = 1
		AND ct.iNamLamViec = @YearOfWork
		GROUP BY ctct.sXauNoiMa

	-- Get data
	IF (@Loai=1)
	SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienDuToanDuocGiao, 0) as fTienDuToanDuocGiao,
		isnull(dataQuyTruoc.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(ctct.fTienThucChi, 0) as fTienThucChi,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa 
	LEFT JOIN #tblDataQuyetToanDaDuyetQuyTruoc dataQuyTruoc
	ON mlnsPhanBo.sXauNoiMa = dataQuyTruoc.sXauNoiMa and  mlnsPhanBo.iID_MaDonVi=dataQuyTruoc.iID_MaDonVi
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi

	ELSE
		SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienTuChi, 0) as fTienDuToanDuocGiao,
		isnull(dataQuyTruoc.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(ctct.fTienThucChi, 0) as fTienThucChi,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tempDuToanTrenGiao daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa
	LEFT JOIN #tblDataQuyetToanDaDuyetQuyTruoc dataQuyTruoc
	ON mlnsPhanBo.sXauNoiMa = dataQuyTruoc.sXauNoiMa and  mlnsPhanBo.iID_MaDonVi=dataQuyTruoc.iID_MaDonVi
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int 
	
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ctct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.ID in (select * from f_split(@IdChungTu))
select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;

With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int ,
	@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ctct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.iLoaiDotNhanPhanBo=@DotNhan
and ct.ID in (select * from f_split(@IdChungTu))
select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;

With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau  END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan  ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int,
	@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ctct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.ID in (select * from f_split(@IdChungTu))
and ct.iLoaiDotNhanPhanBo=@DotNhan

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;


With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
			into #resultAllKhoi
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;


	--Mục A: Khối dự toán
	Select 
	newid() id,
	N'Đơn vị dự toán' sTenDonVi,
	'' iID_MaDonVi,
	N'A' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	 into #tempDonViDuToan

	--Mục B: Khối hạch toán
	Select 
	newid() id,
	N'Đơn vị hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	N'B' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempDonViHachToan

	---Khoi du toán
	Select 
	newid() id,
	N'Khối dự toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiDuToan

	---Khoi hach toan
	Select 
	newid() id,
	N'Khối hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiHachToan


--Mục B: Khối hạch toán --> Hiển thị các đơn vị có iKhoi = 1: Khối doanh nghiệp
	SELECT B.* into #tempDvKDN
	from DonVi A
LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=1
 order by B.position


 -- Create bang tam stt cho Mục B: Khối hạch toán
 SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDN.sTenDonVi) AS VARCHAR(6))  STT,
 #tempDvKDN.sTenDonVi,
 #tempDvKDN.iID_MaDonVi
 INTO #tempSTTKDN
 FROM #tempDvKDN
 WHERE #tempDvKDN.IsHangCha=1


 --- update stt cho Mục B: Khối hạch toán
 UPDATE #tempDvKDN SET STT= A.STT
 FROM #tempSTTKDN A
 WHERE #tempDvKDN.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDN.iID_MaDonVi=A.iID_MaDonVi

 --Mục A: Khối dự toán --> hiển thị các đơn vị có iKhoi = 2: Khối dự toán
	select B.* 
	into #tempDvKDT 
	from DonVi A
 LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=2
 order by B.position

  -- Create bang tam stt cho Mục A: Khối dự toán
	SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDT.sTenDonVi) AS VARCHAR(6))  STT,
	#tempDvKDT.sTenDonVi,
	#tempDvKDT.iID_MaDonVi
	INTO #tempSTTKDT
	FROM #tempDvKDT
	WHERE #tempDvKDT.IsHangCha=1

 --- update stt cho Mục A: Khối dự toán
	UPDATE #tempDvKDT SET STT= A.STT
 FROM #tempSTTKDT A
 WHERE #tempDvKDT.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi


 --- Create data khoi du toan doanh nghiep ( Mục B: Khối hạch toán )
	SELECT 2 iLoai,* INTO #tempKhoiDN
from
(
	SELECT * FROM #tempDonViHachToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDN
	
) tempRESULTKhoiDonViHoachToan order by  tempRESULTKhoiDonViHoachToan.position

 --- Create data khoi du toan  (Mục A: Khối dự toán)
	SELECT 1 iLoai, * INTO #tempKhoiDT
from
(
	SELECT * FROM #tempDonViDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDT

)  tempKhoiDonViDuToan 	order by  tempKhoiDonViDuToan.position



--- Mục B: Khối hạch toán
--- Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

	--- Mục B: Sum Don vi theo khoi hach toan(khoi doanh nghiep) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDN 
		from  #tempKhoiDN
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0

--- update Total Mục B: Đon vi khoi doanh nghiep
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDN A
	where #tempKhoiDN.STT=N'B'
		and #tempKhoiDN.sTenDonVi=N'Đơn vị hạch toán'	
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is  null)


--- update Total Mục B: khoi du toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)


--- Mục A: Khối dự toán
--- Sum khoi du toan cua khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDT
		from  #tempKhoiDT
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum khoi hach toan cua Khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDT 
		from  #tempKhoiDT
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum Don vi theo khoi du toan(khoi du toan) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDT 
		from  #tempKhoiDT
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0


--- update Total Mục B: Don vị khối dự toán
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDT A
	where #tempKhoiDT.STT=N'A'
		and #tempKhoiDT.sTenDonVi=N'Đơn vị dự toán'	
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is  null)

--- update Total Mục B: khoi du toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

SELECT  * 
from
(
	SELECT * FROM #tempKhoiDT
	UNION ALL 
	SELECT * FROM #tempKhoiDN
	
) tempRESULTALL order by iLoai, position;


DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempKhoiDuToan
DROP TABLE #tempKhoiHachToan
DROP TABLE #tempDvKDN
DROP TABLE #tempDvKDT
DROP TABLE #tempSTTKDN
DROP TABLE #tempSTTKDT
DROP TABLE #SumTotalKhoiDuToanOfKhoiDN
DROP TABLE #SumTotalKhoiHachToanOfKhoiDN
DROP TABLE #SumTotalForDonViKhoiDN
DROP TABLE #SumTotalKhoiDuToanOfKhoiDT
DROP TABLE #SumTotalKhoiHachToanOfKhoiDT
DROP TABLE #SumTotalForDonViKhoiDT
DROP TABLE #tempKhoiDN
DROP TABLE #tempKhoiDT
DROP TABLE #resultAllKhoi
DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int
	--@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ctct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.ID in (select * from f_split(@IdChungTu))
--and ct.iLoaiDotNhanPhanBo=@DotNhan

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;


With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
			into #resultAllKhoi
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;


	--Mục A: Khối dự toán
	Select 
	newid() id,
	N'Đơn vị dự toán' sTenDonVi,
	'' iID_MaDonVi,
	N'A' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	 into #tempDonViDuToan

	--Mục B: Khối hạch toán
	Select 
	newid() id,
	N'Đơn vị hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	N'B' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempDonViHachToan

	---Khoi du toán
	Select 
	newid() id,
	N'Khối dự toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiDuToan

	---Khoi hach toan
	Select 
	newid() id,
	N'Khối hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiHachToan


--Mục B: Khối hạch toán --> Hiển thị các đơn vị có iKhoi = 1: Khối doanh nghiệp
	SELECT B.* into #tempDvKDN
	from DonVi A
LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=1
 order by B.position


 -- Create bang tam stt cho Mục B: Khối hạch toán
 SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDN.sTenDonVi) AS VARCHAR(6))  STT,
 #tempDvKDN.sTenDonVi,
 #tempDvKDN.iID_MaDonVi
 INTO #tempSTTKDN
 FROM #tempDvKDN
 WHERE #tempDvKDN.IsHangCha=1


 --- update stt cho Mục B: Khối hạch toán
 UPDATE #tempDvKDN SET STT= A.STT
 FROM #tempSTTKDN A
 WHERE #tempDvKDN.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDN.iID_MaDonVi=A.iID_MaDonVi

 --Mục A: Khối dự toán --> hiển thị các đơn vị có iKhoi = 2: Khối dự toán
	select B.* 
	into #tempDvKDT 
	from DonVi A
 LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=2
 order by B.position

  -- Create bang tam stt cho Mục A: Khối dự toán
	SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDT.sTenDonVi) AS VARCHAR(6))  STT,
	#tempDvKDT.sTenDonVi,
	#tempDvKDT.iID_MaDonVi
	INTO #tempSTTKDT
	FROM #tempDvKDT
	WHERE #tempDvKDT.IsHangCha=1

 --- update stt cho Mục A: Khối dự toán
	UPDATE #tempDvKDT SET STT= A.STT
 FROM #tempSTTKDT A
 WHERE #tempDvKDT.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi


 --- Create data khoi du toan doanh nghiep ( Mục B: Khối hạch toán )
	SELECT 2 iLoai,* INTO #tempKhoiDN
from
(
	SELECT * FROM #tempDonViHachToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDN
	
) tempRESULTKhoiDonViHoachToan order by  tempRESULTKhoiDonViHoachToan.position

 --- Create data khoi du toan  (Mục A: Khối dự toán)
	SELECT 1 iLoai, * INTO #tempKhoiDT
from
(
	SELECT * FROM #tempDonViDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDT

)  tempKhoiDonViDuToan 	order by  tempKhoiDonViDuToan.position



--- Mục B: Khối hạch toán
--- Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

	--- Mục B: Sum Don vi theo khoi hach toan(khoi doanh nghiep) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDN 
		from  #tempKhoiDN
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0

--- update Total Mục B: Đon vi khoi doanh nghiep
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDN A
	where #tempKhoiDN.STT=N'B'
		and #tempKhoiDN.sTenDonVi=N'Đơn vị hạch toán'	
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is  null)


--- update Total Mục B: khoi du toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)


--- Mục A: Khối dự toán
--- Sum khoi du toan cua khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDT
		from  #tempKhoiDT
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum khoi hach toan cua Khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDT 
		from  #tempKhoiDT
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum Don vi theo khoi du toan(khoi du toan) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDT 
		from  #tempKhoiDT
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0


--- update Total Mục B: Don vị khối dự toán
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDT A
	where #tempKhoiDT.STT=N'A'
		and #tempKhoiDT.sTenDonVi=N'Đơn vị dự toán'	
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is  null)

--- update Total Mục B: khoi du toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

SELECT  * 
from
(
	SELECT * FROM #tempKhoiDT
	UNION ALL 
	SELECT * FROM #tempKhoiDN
	
) tempRESULTALL order by iLoai, position;


DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempKhoiDuToan
DROP TABLE #tempKhoiHachToan
DROP TABLE #tempDvKDN
DROP TABLE #tempDvKDT
DROP TABLE #tempSTTKDN
DROP TABLE #tempSTTKDT
DROP TABLE #SumTotalKhoiDuToanOfKhoiDN
DROP TABLE #SumTotalKhoiHachToanOfKhoiDN
DROP TABLE #SumTotalForDonViKhoiDN
DROP TABLE #SumTotalKhoiDuToanOfKhoiDT
DROP TABLE #SumTotalKhoiHachToanOfKhoiDT
DROP TABLE #SumTotalForDonViKhoiDT
DROP TABLE #tempKhoiDN
DROP TABLE #tempKhoiDT
DROP TABLE #resultAllKhoi
DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int
	--@DotNhan int
AS
BEGIN

		select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
		right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
		where ct.iNamChungTu=@INamLamViec
		--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
		and ctct.sMaLoaiChi=@MaLoaiChi
		--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
		and ct.ID in (select * from f_split(@IdChungTu))
		--and ct.iLoaiDotNhanPhanBo=@DotNhan
		--and ct.sSoQuyetDinh=@SoQuyetdinh
		and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

		select 
			 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
			, dv.sTenDonVi
			, dv.iID_MaDonVi
			, SUM(ctct.fTongTien) as fTongTienDuToan
			, 0 IsHangCha
			, 0 RowNumber
			into #temp1
		from 
		#tempall ctct
		left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
		where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
		and
		dv.iNamLamViec=@INamLamViec
		and
		ctct.iNamLamViec=@INamLamViec
		group by  dv.iID_MaDonVi, dv.sTenDonVi 

		select rs.fTongTienDuToan/@Donvitinh fTongTienDuToan
			, rs.iID_MaDonVi
			, rs.IsHangCha
			, rs.RowNumber
			, rs.sTenDonVi
			, rs.STT
			from #temp1 rs;
DROP TABLE #tempall
DROP TABLE #temp1
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@DotNhan int
AS
BEGIN

		select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
		right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
		where ct.iNamChungTu=@INamLamViec
		--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
		and ctct.sMaLoaiChi=@MaLoaiChi
		--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
		and ct.ID in (select * from f_split(@IdChungTu))
		and ct.iLoaiDotNhanPhanBo=@DotNhan
		--and ct.sSoQuyetDinh=@SoQuyetdinh
		and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

		select 
			 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
			, dv.sTenDonVi
			, dv.iID_MaDonVi
			, SUM(ctct.fTongTien) as fTongTienDuToan
			, 0 IsHangCha
			, 0 RowNumber
			into #temp1
		from 
		#tempall ctct
		left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
		where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
		and
		dv.iNamLamViec=@INamLamViec
		and
		ctct.iNamLamViec=@INamLamViec
		group by  dv.iID_MaDonVi, dv.sTenDonVi 

		select rs.fTongTienDuToan/@Donvitinh fTongTienDuToan
					, rs.iID_MaDonVi
					, rs.IsHangCha
					, rs.RowNumber
					, rs.sTenDonVi
					, rs.STT
					from #temp1 rs;

DROP TABLE #tempall
DROP TABLE #temp1
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@DotNhan int
AS
BEGIN

	select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
	right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
	where ct.iNamChungTu=@INamLamViec
	--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
	and ctct.sMaLoaiChi=@MaLoaiChi
	--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
	and ct.iLoaiDotNhanPhanBo=@DotNhan
	and ct.ID in (select * from f_split(@IdChungTu))
	--and ct.sSoQuyetDinh=@SoQuyetdinh
	and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

	select 
		 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
		, dv.sTenDonVi
		, dv.iID_MaDonVi
		, SUM(ctct.fTongTien) as fTongTienDuToan
		, 0 IsHangCha
		, 0 RowNumber
		into #temp1
	from 
	#tempall ctct
	left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
	where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
	and
	dv.iNamLamViec=@INamLamViec
	and
	ctct.iNamLamViec=@INamLamViec
	group by  dv.iID_MaDonVi, dv.sTenDonVi 

	select 
	N'A' STT,
	N'Đơn vị dự toán' sTenDonVi,
	'' iID_MaDonVi,
	0 fTongTienDuToan,
	1 IsHangCha,
	0 RowNumber
	into #tempDonViDuToan

	select 
	N'B' STT,
	N'Đơn vị hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	0 fTongTienDuToan,
	1 IsHangCha,
	0 RowNumber
	into #tempDonViHachToan

	------ create data don vi du toan
	CREATE TABLE #tempDvKDT(STT VARCHAR(6), sTenDonVi nvarchar(50), iID_MaDonVi varchar(50), fTongTienDuToan float, IsHangCha int, RowNumber int)
	INSERT INTO #tempDvKDT(STT, sTenDonVi, iID_MaDonVi, fTongTienDuToan, IsHangCha, RowNumber)
		SELECT B.* 
		from #temp1 B
	LEFT JOIN DonVi A ON A.iID_MaDonVi=B.iID_MaDonVi
	where A.iNamLamViec=@INamLamViec
	 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
	 and A.iKhoi=2
	 ORDER BY B.STT;
	------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
			dv.iID_MaDonVi,
			dv.sTenDonVi
			into #tempSttKDT
			From #tempDvKDT dv
	------ Update stt 
		Update #tempDvKDT set #tempDvKDT.STT=A.STT
			From #tempSttKDT A
			where #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi
				and #tempDvKDT.sTenDonVi=A.sTenDonVi
	------ create data don vi hach toan
		SELECT B.* into #tempDvKHT
		From DonVi A
		LEFT JOIN #temp1 B ON A.iID_MaDonVi=B.iID_MaDonVi
		where A.iNamLamViec=@INamLamViec
		 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
		 and A.iKhoi=1

	 ------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
			dv.iID_MaDonVi,
			dv.sTenDonVi
			into #tempSttKHT
			From #tempDvKHT dv
	------ Update stt 
		Update #tempDvKHT set #tempDvKHT.STT=A.STT
			From #tempSttKHT A
			where #tempDvKHT.iID_MaDonVi=A.iID_MaDonVi
				and #tempDvKHT.sTenDonVi=A.sTenDonVi

	 --- Create data merge don vi du toan
		SELECT  1 iLoai, * INTO #tempDataDVDT
		FROM
		(
			SELECT * FROM #tempDonViDuToan
			UNION ALL 
			SELECT * FROM #tempDvKDT
		)tempDataDVDT

		--- Tinh tong theo don vi du toan
		SELECT SUM(fTongTienDuToan) fTongTienDuToan
		INTO #SumTotalDuToan
		FROM #tempDvKDT
		--- update tong tien don vị du toan
		UPDATE #tempDataDVDT SET #tempDataDVDT.fTongTienDuToan=A.fTongTienDuToan
		FROM #SumTotalDuToan A
		WHERE #tempDataDVDT.sTenDonVi=N'Đơn vị dự toán' 
		AND #tempDataDVDT.STT=N'A'
	
		 --- Create data merge don vi hach toan
		SELECT  2 iLoai,* INTO #tempDataDVHT
		FROM
		(
			SELECT * FROM #tempDonViHachToan
			UNION ALL 
			SELECT * FROM #tempDvKHT
		)tempDataDVHT

		--- Tinh tong theo don vi hach toan
		SELECT SUM(fTongTienDuToan) fTongTienDuToan
		INTO #SumTotalHachToan
		FROM #tempDvKHT
		--- update tong tien don vị hach toan
		UPDATE #tempDataDVHT SET #tempDataDVHT.fTongTienDuToan=A.fTongTienDuToan
		FROM #SumTotalHachToan A
		WHERE #tempDataDVHT.sTenDonVi=N'Đơn vị hạch toán'
		AND #tempDataDVHT.STT=N'B'

		--- create merge don vi du toan voi don vi hach toan vào
		SELECT * into #tblresult
		FROM
		(
			SELECT * FROM #tempDataDVDT
			UNION ALL 
			SELECT * FROM #tempDataDVHT
		)tempDataAll

		select rs.STT
			 , rs.iID_MaDonVi
			 , rs.IsHangCha
			 , rs.RowNumber
			 , rs.sTenDonVi
			 , rs.fTongTienDuToan/@Donvitinh fTongTienDuToan
			 , rs.iLoai
		FROM  #tblresult rs

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempDvKDT
DROP TABLE #tempDvKHT
DROP TABLE #tempSttKDT
DROP TABLE #tempSttKHT
DROP TABLE #SumTotalDuToan
DROP TABLE #SumTotalHachToan
DROP TABLE #tempDataDVDT
DROP TABLE #tempDataDVHT
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@DotNhan int
AS
BEGIN

	select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
	right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
	where ct.iNamChungTu=@INamLamViec
	--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
	and ctct.sMaLoaiChi=@MaLoaiChi
	--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
	and ct.iLoaiDotNhanPhanBo=@DotNhan
	and ct.ID in (select * from f_split(@IdChungTu))
	--and ct.sSoQuyetDinh=@SoQuyetdinh
	and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

	select 
		 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
		, dv.sTenDonVi
		, dv.iID_MaDonVi
		, SUM(ctct.fTongTien) as fTongTienDuToan
		, 0 IsHangCha
		, 0 RowNumber
		into #temp1
	from 
	#tempall ctct
	left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
	where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
	and
	dv.iNamLamViec=@INamLamViec
	and
	ctct.iNamLamViec=@INamLamViec
	group by  dv.iID_MaDonVi, dv.sTenDonVi 

	select 
	N'A' STT,
	N'Đơn vị dự toán' sTenDonVi,
	'' iID_MaDonVi,
	0 fTongTienDuToan,
	1 IsHangCha,
	0 RowNumber
	into #tempDonViDuToan

	select 
	N'B' STT,
	N'Đơn vị hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	0 fTongTienDuToan,
	1 IsHangCha,
	0 RowNumber
	into #tempDonViHachToan

	------ create data don vi du toan
	CREATE TABLE #tempDvKDT(STT VARCHAR(6), sTenDonVi nvarchar(50), iID_MaDonVi varchar(50), fTongTienDuToan float, IsHangCha int, RowNumber int)
	INSERT INTO #tempDvKDT(STT, sTenDonVi, iID_MaDonVi, fTongTienDuToan, IsHangCha, RowNumber)
		SELECT B.* 
		from #temp1 B
	LEFT JOIN DonVi A ON A.iID_MaDonVi=B.iID_MaDonVi
	where A.iNamLamViec=@INamLamViec
	 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
	 and A.iKhoi=2
	 ORDER BY B.STT;
	------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
			dv.iID_MaDonVi,
			dv.sTenDonVi
			into #tempSttKDT
			From #tempDvKDT dv
	------ Update stt 
		Update #tempDvKDT set #tempDvKDT.STT=A.STT
			From #tempSttKDT A
			where #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi
				and #tempDvKDT.sTenDonVi=A.sTenDonVi
	------ create data don vi hach toan
		SELECT B.* into #tempDvKHT
		From DonVi A
		LEFT JOIN #temp1 B ON A.iID_MaDonVi=B.iID_MaDonVi
		where A.iNamLamViec=@INamLamViec
		 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
		 and A.iKhoi=1

	 ------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
			dv.iID_MaDonVi,
			dv.sTenDonVi
			into #tempSttKHT
			From #tempDvKHT dv
	------ Update stt 
		Update #tempDvKHT set #tempDvKHT.STT=A.STT
			From #tempSttKHT A
			where #tempDvKHT.iID_MaDonVi=A.iID_MaDonVi
				and #tempDvKHT.sTenDonVi=A.sTenDonVi

	 --- Create data merge don vi du toan
		SELECT  1 iLoai, * INTO #tempDataDVDT
		FROM
		(
			SELECT * FROM #tempDonViDuToan
			UNION ALL 
			SELECT * FROM #tempDvKDT
		)tempDataDVDT

		--- Tinh tong theo don vi du toan
		SELECT SUM(fTongTienDuToan) fTongTienDuToan
		INTO #SumTotalDuToan
		FROM #tempDvKDT
		--- update tong tien don vị du toan
		UPDATE #tempDataDVDT SET #tempDataDVDT.fTongTienDuToan=A.fTongTienDuToan
		FROM #SumTotalDuToan A
		WHERE #tempDataDVDT.sTenDonVi=N'Đơn vị dự toán' 
		AND #tempDataDVDT.STT=N'A'
	
		 --- Create data merge don vi hach toan
		SELECT  2 iLoai,* INTO #tempDataDVHT
		FROM
		(
			SELECT * FROM #tempDonViHachToan
			UNION ALL 
			SELECT * FROM #tempDvKHT
		)tempDataDVHT

		--- Tinh tong theo don vi hach toan
		SELECT SUM(fTongTienDuToan) fTongTienDuToan
		INTO #SumTotalHachToan
		FROM #tempDvKHT
		--- update tong tien don vị hach toan
		UPDATE #tempDataDVHT SET #tempDataDVHT.fTongTienDuToan=A.fTongTienDuToan
		FROM #SumTotalHachToan A
		WHERE #tempDataDVHT.sTenDonVi=N'Đơn vị hạch toán'
		AND #tempDataDVHT.STT=N'B'

		--- create merge don vi du toan voi don vi hach toan vào
		SELECT * into #tblresult
		FROM
		(
			SELECT * FROM #tempDataDVDT
			UNION ALL 
			SELECT * FROM #tempDataDVHT
		)tempDataAll

	select rs.STT
		 , rs.iID_MaDonVi
		 , rs.IsHangCha
		 , rs.RowNumber
		 , rs.sTenDonVi
		 , rs.fTongTienDuToan/@Donvitinh fTongTienDuToan
		 , rs.iLoai
	FROM  #tblresult rs

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempDvKDT
DROP TABLE #tempDvKHT
DROP TABLE #tempSttKDT
DROP TABLE #tempSttKHT
DROP TABLE #SumTotalDuToan
DROP TABLE #SumTotalHachToan
DROP TABLE #tempDataDVDT
DROP TABLE #tempDataDVHT
DROP TABLE #tblresult
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(max),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int,
	@Loai int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM splitstring(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
			AND ML.bHangCha IS NOT NULL
	ORDER BY sXauNoiMa

	--- Chung tu thuong
	IF (@Loai=0)
	SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ml.sDuToanChiTietToi,
		dt.fTienTuChi
		ORDER BY sXauNoiMa
		--- Chung tu tong hop
		ELSE 
		SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
					SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
							WHERE ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND BIsKhoa = 1
							--AND ct.iID_LoaiDanhMucChi=@IDLoaiChi
							AND ct.iNamLamViec = @NamLamViec
							GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ml.sDuToanChiTietToi,
		dt.fTienTuChi
		ORDER BY sXauNoiMa;

		DROP TABLE #tblml

END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int,
	@Loai int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM splitstring(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
			ANd ML.bHangChaDuToanDieuChinh is not null
	ORDER BY sXauNoiMa

	SELECT 
						chitiet.fTienDuToanDuocGiao, 
						chitiet.fTienSoSanhGiam, chitiet.fTienSoSanhTang, 
						dtct_by_donvi.fTienThucHien06ThangDauNam as fTienThucHien06ThangDauNam, 
						dtct_by_donvi.fTienUocThucHien06ThangCuoiNam as fTienUocThucHien06ThangCuoiNam, 
						dtct_by_donvi.fTienUocThucHienCaNam as fTienUocThucHienCaNam,
						chitiet.iID_BH_DTC,
						chitiet.iID_MucLucNganSach,
						dtct_by_donvi.sNoiMa,
						chitiet.sXauNoiMa,
						chitiet.iNamLamViec
					into #bh_dtc_dieuchinh_chitiet
					FROM BH_DTC_DieuChinhDuToanChi_ChiTiet chitiet
					LEFT JOIN (
						SELECT sTM, 
							iID_MaDonVi, 
							sNoiMa,
							sM,
							SUM(fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam, 
							SUM(fTienUocThucHien06ThangCuoiNam) as fTienUocThucHien06ThangCuoiNam,
							SUM(fTienUocThucHienCaNam) as fTienUocThucHienCaNam 
						FROM (
								SELECT *, 
									CASE WHEN sXauNoiMa LIKE '9010001%' THEN '9010001' 
									WHEN sXauNoiMa LIKE '9010002%' THEN '9010002' END as sNoiMa
								FROM BH_DTC_DieuChinhDuToanChi_ChiTiet 
							) BH_DTC_DieuChinhDuToanChi_ChiTiet
						WHERE iNamLamViec=@NamLamViec
						AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
						AND sTM != ''
						GROUP BY sTM, iID_MaDonVi, sNoiMa, sM
					) dtct_by_donvi ON chitiet.sTM = '' 
						AND chitiet.sM = dtct_by_donvi.sM 
						AND chitiet.iID_MaDonVi = dtct_by_donvi.iID_MaDonVi
						AND chitiet.sXauNoiMa LIKE (dtct_by_donvi.sNoiMa + '%')

					ORDER BY chitiet.sXauNoiMa

IF @Loai=0
	SELECT 
		--ct.iID_MaDonVi,
		ct.sTenDonVi as STenDonVi,
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh bHangCha,
		--ml.bHangChaDuToanDieuChinh IsHangCha,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(ct.fTienSoSanhGiam),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				bh.sMoTa,
				CASE 
					WHEN (SELECT COUNT(*) FROM splitstring(@IdDonVi)) > 1 THEN ''
					ELSE ddv.sTenDonVi
				END as sTenDonVi,
				--ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				#bh_dtc_dieuchinh_chitiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		--and ml.bHangChaDuToanDieuChinh = 1
		and ml.sTM = ''
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ct.sTenDonVi,
		dt.fTienTuChi
		--ct.iID_MaDonVi,
		ORDER BY sXauNoiMa;
ELSE
SELECT 
		--ct.iID_MaDonVi,
		ct.sTenDonVi as STenDonVi,
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh bHangCha,
		--ml.bHangChaDuToanDieuChinh IsHangCha,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(ct.fTienSoSanhGiam),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				bh.sMoTa,
				CASE 
					WHEN (SELECT COUNT(*) FROM splitstring(@IdDonVi)) > 1 THEN ''
					ELSE ddv.sTenDonVi
				END as sTenDonVi,
				--ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				#bh_dtc_dieuchinh_chitiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
			JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamLamViec = @NamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa  
		WHERE ml.iNamLamViec=@NamLamViec
		--and ml.bHangChaDuToanDieuChinh = 1
		and ml.sTM = ''
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ct.sTenDonVi,
		dt.fTienTuChi
		--ct.iID_MaDonVi,
		ORDER BY sXauNoiMa;
		
		DROP TABLE #tblml		
		DROP TABLE #bh_dtc_dieuchinh_chitiet
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @LoaiChi uniqueidentifier
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

SELECT   isnull(ctct.ID_QTC_Nam_KPK_ChiTiet, @iDCT) as ID_QTC_Nam_KPK_ChiTiet,
		@iD as iID_QTC_Nam_KPK,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.sDuToanChiTietToi as SDuToanChiTietToi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(Sum(ctct.fTien_TongDuToanDuocGiao), 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(Sum(ctct.fTien_ThucChi), 0)  as fTien_ThucChi, 
		ISNULL(Sum(ctct.fTienThua), 0)  as fTienThua, 
		ISNULL(Sum(ctct.fTienThieu), 0)  as fTienThieu, 
		ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KPK_ChiTiet 
				WHERE iID_QTC_Nam_KPK in
					( SELECT ID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
								WHERE iNamLamViec=@NamLamViec
								AND iID_LoaiChi=@LoaiChi
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 

	Group by ctct.ID_QTC_Nam_KPK_ChiTiet,
		ctct.iID_QTC_Nam_KPK,
		mlns.iID_MLNS ,
		mlns.iID_MLNS_Cha ,
		mlns.sXauNoiMa ,
		mlns.sLNS ,
		mlns.sL ,
		mlns.sK ,
		mlns.sM ,
		mlns.sTM ,
		mlns.sTTM ,
		mlns.sNG ,
		mlns.sTNG ,
		mlns.sMoTa ,
		mlns.bHangCha ,
		mlns.sDuToanChiTietToi ,
		ctct.sNguoiTao ,
		ctct.sNguoiSua, 
		ctct.dNgayTao,
		ctct.dNgaySua
	Order by mlns.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]
	@listTenDonVi ntext,
	@namLamViec int,
	@LNS nvarchar(200)
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),Loai int,
FTienDaThucHienNamTruoc float,FTienNamNay float,FTienCong float,FTienQuyetToan float, FTienThua float,FTienThieu float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , Loai,
FTienDaThucHienNamTruoc ,FTienNamNay , FTienCong ,FTienQuyetToan , FTienThua ,FTienThieu 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   dt_dv.iLoai Loai,
	   FTienDaThucHienNamTruoc=SUM(IsNull(A.fTien_DuToanNamTruocChuyenSang,0)),
	   FTienNamNay=SUM(IsNull(A.fTien_DuToanGiaoNamNay,0)),
	   FTienQuyetToan=SUM(IsNull(A.fTien_TongDuToanDuocGiao,0)),
	   FTienCong=SUM(IsNull(A.fTien_ThucChi,0)),
	   FTienThua=SUM(IsNull(A.fTienThua,0)),
	   FTienThieu=SUM(IsNull(A.fTienThieu,0))
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTien_DuToanNamTruocChuyenSang,
				ctct.fTien_DuToanGiaoNamNay,
				ctct.fTien_TongDuToanDuocGiao,
				ctct.fTien_ThucChi,
				ctct.fTienThua,
				ctct.fTienThieu,
				ctct.fTiLeThucHienTrenDuToan
   FROM BH_QTC_Nam_KPK_ChiTiet ctct
   LEFT JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS IN  (SELECT * FROM f_split(@LNS))
   WHERE ct.iNamLamViec = @namLamViec--@namLamViec
	---
	) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		dt_dv.iLoai;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.Loai,
IsNull(dt.FTienDaThucHienNamTruoc, 0) FTienDaThucHienNamTruoc,
IsNull(dt.FTienNamNay, 0) FTienNamNay,
IsNull(dt.FTienCong, 0) FTienCong,
IsNull(dt.FTienQuyetToan, 0) FTienQuyetToan,
IsNull(dt.FTienThua, 0) FTienThua,
IsNull(dt.FTienThieu, 0) FTienThieu
FROM @DataKhoi dt
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @DonViTnh int,
	 @Loai int
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND sLNS IN (SELECT * FROM f_split(@LNS))

IF @Loai=0
	SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			mlns.iID_MLNS as iID_MucLucNganSach,
			mlns.iID_MLNS_Cha as IdParent,
			mlns.sXauNoiMa as sXauNoiMa,
			mlns.sLNS as SLNS,
			mlns.sL as SL,
			mlns.sK as SK,
			mlns.sM as SM,
			mlns.sTM as STM,
			mlns.sTTM as STTM,
			mlns.sNG as SNG,
			mlns.sTNG as STNG,
			mlns.sMoTa as SNoiDung,
			mlns.sDuToanChiTietToi,
			--ctct.iID_MaDonVi,
			@NamLamViec AS INamLamViec,
			mlns.bHangCha as IsHangCha,
			isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
			isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
			ctct.sNguoiTao AS sNguoiTao,
			ctct.sNguoiSua AS sNguoiSua,
			ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh  as fTien_DuToanNamTruocChuyenSang, 
			ISNULL((daDuToan.fTienDuToan), 0) / @DonViTnh  as fTien_DuToanGiaoNamNay,
			ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh + ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)/ @DonViTnh  as fTien_TongDuToanDuocGiao, 
			ISNULL(Sum(ctct.fTien_ThucChi), 0) / @DonViTnh  as fTien_ThucChi, 
			ISNULL(Sum(ctct.fTienThua), 0) / @DonViTnh  as fTienThua, 
			ISNULL(Sum(ctct.fTienThieu), 0) / @DonViTnh  as fTienThieu, 
			--ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0) / @DonViTnh  as fTiLeThucHienTrenDuToan
			ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)   as fTiLeThucHienTrenDuToan
		FROM 
			#tblMlnsByPhanCap mlns
		LEFT JOIN 
			(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
					WHERE iID_QTC_Nam_KinhPhiQuanLy in
						( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
									WHERE iNamLamViec=@NamLamViec
									AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
									)) ctct
		on mlns.iID_MLNS = ctct.iID_MucLucNganSach
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa,
				  iID_MaDonVi
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY iID_MaDonVi, 
		   sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
		Group by ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			--ctct.iID_MaDonVi,
			mlns.iID_MLNS ,
			mlns.iID_MLNS_Cha ,
			mlns.sXauNoiMa ,
			mlns.sLNS ,
			mlns.sL ,
			mlns.sK ,
			mlns.sM ,
			mlns.sTM ,
			mlns.sTTM ,
			mlns.sNG ,
			mlns.sTNG ,
			mlns.sMoTa ,
			mlns.bHangCha ,
			ctct.sNguoiTao ,
			ctct.sNguoiSua, 
			ctct.dNgayTao,
			mlns.sDuToanChiTietToi,
			ctct.dNgaySua,
			daDuToan.fTienDuToan
	Order by mlns.sXauNoiMa
	ELSE

		SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			mlns.iID_MLNS as iID_MucLucNganSach,
			mlns.iID_MLNS_Cha as IdParent,
			mlns.sXauNoiMa as sXauNoiMa,
			mlns.sLNS as SLNS,
			mlns.sL as SL,
			mlns.sK as SK,
			mlns.sM as SM,
			mlns.sTM as STM,
			mlns.sTTM as STTM,
			mlns.sNG as SNG,
			mlns.sTNG as STNG,
			mlns.sMoTa as SNoiDung,
			mlns.sDuToanChiTietToi,
			--ctct.iID_MaDonVi,
			@NamLamViec AS INamLamViec,
			mlns.bHangCha as IsHangCha,
			isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
			isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
			ctct.sNguoiTao AS sNguoiTao,
			ctct.sNguoiSua AS sNguoiSua,
			ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh  as fTien_DuToanNamTruocChuyenSang, 
			ISNULL((daDuToan.fTienDuToan), 0) / @DonViTnh  as fTien_DuToanGiaoNamNay,
			ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh + ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)/ @DonViTnh  as fTien_TongDuToanDuocGiao, 
			ISNULL(Sum(ctct.fTien_ThucChi), 0) / @DonViTnh  as fTien_ThucChi, 
			ISNULL(Sum(ctct.fTienThua), 0) / @DonViTnh  as fTienThua, 
			ISNULL(Sum(ctct.fTienThieu), 0) / @DonViTnh  as fTienThieu, 
			--ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0) / @DonViTnh  as fTiLeThucHienTrenDuToan
			ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)   as fTiLeThucHienTrenDuToan
		FROM 
			#tblMlnsByPhanCap mlns
		LEFT JOIN 
			(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
					WHERE iID_QTC_Nam_KinhPhiQuanLy in
						( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
									WHERE iNamLamViec=@NamLamViec
									AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
									)) ctct
		on mlns.iID_MLNS = ctct.iID_MucLucNganSach
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa,
				  iID_MaDonVi
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY iID_MaDonVi, 
		   sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
		Group by ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			--ctct.iID_MaDonVi,
			mlns.iID_MLNS ,
			mlns.iID_MLNS_Cha ,
			mlns.sXauNoiMa ,
			mlns.sLNS ,
			mlns.sL ,
			mlns.sK ,
			mlns.sM ,
			mlns.sTM ,
			mlns.sTTM ,
			mlns.sNG ,
			mlns.sTNG ,
			mlns.sMoTa ,
			mlns.bHangCha ,
			ctct.sNguoiTao ,
			ctct.sNguoiSua, 
			ctct.dNgayTao,
			mlns.sDuToanChiTietToi,
			ctct.dNgaySua,
			daDuToan.fTienDuToan
	Order by mlns.sXauNoiMa

	drop table #tblMlnsByPhanCap;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]    Script Date: 3/20/2024 3:01:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]
	@listTenDonVi ntext,
	@namLamViec int
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),Loai int,
FTienDaThucHienNamTruoc float,FTienNamNay float,FTienCong float,FTienQuyetToan float, FTienThua float,FTienThieu float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , Loai,
FTienDaThucHienNamTruoc ,FTienNamNay , FTienCong ,FTienQuyetToan , FTienThua ,FTienThieu 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   dt_dv.iLoai Loai,
	   FTienDaThucHienNamTruoc=SUM(IsNull(A.fTien_DuToanNamTruocChuyenSang,0)),
	   FTienNamNay=(IsNull(daDuToan.fTienDuToan,0)),
	   FTienQuyetToan=SUM(IsNull(A.fTien_TongDuToanDuocGiao,0)),
	   FTienCong=SUM(IsNull(A.fTien_ThucChi,0)),
	   FTienThua=SUM(IsNull(A.fTienThua,0)),
	   FTienThieu=SUM(IsNull(A.fTienThieu,0))
FROM
  (SELECT		
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTien_DuToanNamTruocChuyenSang,
				ctct.fTien_DuToanGiaoNamNay,
				ctct.fTien_TongDuToanDuocGiao,
				ctct.fTien_ThucChi,
				ctct.fTienThua,
				ctct.fTienThieu,
				ctct.fTiLeThucHienTrenDuToan,
				ml.sXauNoiMa
	FROM BH_DM_MucLucNganSach ml
		   LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct ON ml.sXauNoiMa = ctct.sXauNoiMa
		   LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		   AND ml.iNamLamViec = @namLamViec--@namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS =9010003 --9020000
		   WHERE ct.iNamLamViec = @namLamViec--@namLamViec
	) AS A 
   LEFT JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   LEFT JOIN (
		SELECT 
			  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
			  sXauNoiMa,
			  iID_MaDonVi
	   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
	   WHERE iID_DTC_DuToanChiTrenGiao IN
		   (SELECT ID
			FROM BH_DTC_DuToanChiTrenGiao
			WHERE sSoQuyetDinh <> ''
			  AND sSoQuyetDinh IS NOT NULL
			  AND iNamLamViec = @namLamViec
			  AND bIsKhoa=1)
		 AND iID_MaDonVi in  (SELECT * FROM f_split(@listTenDonVi))-- donvi
	   GROUP BY iID_MaDonVi, 
	   sXauNoiMa
	) daDuToan  on A.sXauNoiMa=daDuToan.sXauNoiMa
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		dt_dv.iLoai,
		daDuToan.fTienDuToan;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.Loai,
IsNull(dt.FTienDaThucHienNamTruoc, 0) FTienDaThucHienNamTruoc,
IsNull(dt.FTienNamNay, 0) FTienNamNay,
IsNull(dt.FTienCong, 0) FTienCong,
IsNull(dt.FTienQuyetToan, 0) FTienQuyetToan,
IsNull(dt.FTienThua, 0) FTienThua,
IsNull(dt.FTienThieu, 0) FTienThieu
FROM @DataKhoi dt
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;
;
GO
