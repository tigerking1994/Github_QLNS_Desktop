/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_danh_muc_tdqt]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_danh_muc_tdqt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_danh_muc_tdqt]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data_update_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data_update_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 8/2/2024 3:07:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin

---Lấy thông tin chi tiết giai thich tro cap du toan
		SELECT gt.* ,dv.sTenDonVi
			into #TBL_TroCapOmDauDuToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0001%' )
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								--AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec
								AND ct.iQuyChungTu = @IQuy 

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapOmDauHachToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE ( gt.sXauNoiMa like '9010002-010-011-0001%')
								AND gt.iNamLamViec = @INamLamViec  
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								--AND dv.iKhoi=1
								AND ct.iQuyChungTu = @IQuy 

		select 
		N'I. Khối dự toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempDuToan

		-- SQ Du Toan
		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQDuToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNDuToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPDuToan

		select 
		N'4. HSQBS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSDuToan

		select 
		N'5. LDHD' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDDuToan

----- Hach Toan
		select 
		N'II. Khối hạch toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHachToan

		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQHachToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNHachToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPHachToan

		select 
		N'4. HSQBS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSHachToan

		select 
		N'5. LDHD' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDHachToan

--- Lay thong tin giai thich theo khoi du toan
		---Du Toan SQ
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalSQDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

				
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQDuToan 
				FROM #tempDetalSQDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

		---Du Toan QNCN
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalQNCNDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt

			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

								
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNDuToan 
				FROM #tempDetalQNCNDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Du Toan CNVCQP
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSK
				INTO #tempDetalCNVCQPDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt
		
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

	SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPDuToan 
				FROM #tempDetalCNVCQPDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Du Toan HSQBS
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalHSQBSDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSDuToan 
				FROM #tempDetalHSQBSDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Du Toan LDHD
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalLDHDDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDDuToan 
				FROM #tempDetalLDHDDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi


--- Lay thong tin giai thich theo khoi hach toan
		---Hạch Toan SQ
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalSQHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt

			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQHachToan 
				FROM #tempDetalSQHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi


		---Hạch Toan QNCN
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalQNCNHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNHachToan 
				FROM #tempDetalQNCNHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi
	---Hạch Toan CNVCQP
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalCNVCQPHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPHachToan 
				FROM #tempDetalCNVCQPHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Hạch Toan HSQBS
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalHSQBSHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSHachToan 
				FROM #tempDetalHSQBSHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Hạch Toan LDHD
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalLDHDHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDHachToan 
				FROM #tempDetalLDHDHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi

		----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempSQDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalSQDuToan) Detail
			where A.type=Detail.type

			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempQNCNDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalQNCNDuToan) Detail
			where A.type=Detail.type

	update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempCNVCQPDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalCNVCQPDuToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempHSQBSDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalHSQBSDuToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempLDHDDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalLDHDDuToan) Detail
			where A.type=Detail.type
		----- Update Hach Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempSQHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalSQHachToan) Detail
			where A.type=Detail.type

			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempQNCNHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalQNCNHachToan) Detail
			where A.type=Detail.type

	update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempCNVCQPHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalCNVCQPHachToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempHSQBSHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalHSQBSHachToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempLDHDHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalLDHDHachToan) Detail
			where A.type=Detail.type

---Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempSQDuToan
					UNION ALL
					SELECT * FROM  #tempDetalSQDuToan
					UNION ALL
					SELECT * FROM  #tempQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempLDHDDuToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDHachToan
			) TEMPDuToan

------ Update total khoi du toan
			SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 3 type
			INTO #tempTotalDuToan
			FROM #tempkqDuToan
			WHERE type=2

			update T1
			set T1.iSoNgayDuoi14BenhDaiNgay=T2.iSoNgayDuoi14BenhDaiNgay,
			T1.fSoTienDuoi14BenhDaiNgay=T2.fSoTienDuoi14BenhDaiNgay,
			T1.iSoNgayTren14BenhDaiNgay=T2.iSoNgayTren14BenhDaiNgay,
			T1.fSoTienTren14BenhDaiNgay=T2.fSoTienTren14BenhDaiNgay,
			T1.iSoNgayDuoi14OmKhac=T2.iSoNgayDuoi14OmKhac,
			T1.fSoTienDuoi14OmKhac=T2.fSoTienDuoi14OmKhac,
			T1.iSoNgayTren14OmKhac=T2.iSoNgayTren14OmKhac,
			T1.fSoTienTren14OmKhac=T2.fSoTienTren14OmKhac,
			T1.iSoNgayConOm=T2.iSoNgayConOm,
			T1.fSoTienConOm=T2.fSoTienConOm,
			T1.iSoNgayPHSK=T2.iSoNgayPHSK,
			T1.fSoTienPHSK=T2.fSoTienPHSK
			FROM #tempkqDuToan T1, #tempTotalDuToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'I. Khối dự toán'

---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempSQHachToan
					UNION ALL
					SELECT * FROM  #tempDetalSQHachToan
					UNION ALL
					SELECT * FROM  #tempQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempLDHDHachToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDHachToan
			) TEMPHachToan

------ Update total khoi du toan
			SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 3 type
			INTO #tempTotalHachToan
			FROM #tempkqHachToan
			WHERE type=2

			update T1
			set T1.iSoNgayDuoi14BenhDaiNgay=T2.iSoNgayDuoi14BenhDaiNgay,
			T1.fSoTienDuoi14BenhDaiNgay=T2.fSoTienDuoi14BenhDaiNgay,
			T1.iSoNgayTren14BenhDaiNgay=T2.iSoNgayTren14BenhDaiNgay,
			T1.fSoTienTren14BenhDaiNgay=T2.fSoTienTren14BenhDaiNgay,
			T1.iSoNgayDuoi14OmKhac=T2.iSoNgayDuoi14OmKhac,
			T1.fSoTienDuoi14OmKhac=T2.fSoTienDuoi14OmKhac,
			T1.iSoNgayTren14OmKhac=T2.iSoNgayTren14OmKhac,
			T1.fSoTienTren14OmKhac=T2.fSoTienTren14OmKhac,
			T1.iSoNgayConOm=T2.iSoNgayConOm,
			T1.fSoTienConOm=T2.fSoTienConOm,
			T1.iSoNgayPHSK=T2.iSoNgayPHSK,
			T1.fSoTienPHSK=T2.fSoTienPHSK
			FROM #tempkqHachToan T1, #tempTotalHachToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'II. Khối hạch toán'

			SELECT * INTO #tempKQAll FROM
			(
					SELECT * FROM #tempkqDuToan
					UNION ALL
					SELECT * FROM  #tempkqHachToan
					
			) TEMPKQAll

		select sTT
		, iID_MaDonVi
		,sTenDonVi
		,iSoNgayDuoi14BenhDaiNgay
		,fSoTienDuoi14BenhDaiNgay/@Donvitinh fSoTienDuoi14BenhDaiNgay
		,iSoNgayTren14BenhDaiNgay
		,fSoTienTren14BenhDaiNgay/@Donvitinh fSoTienTren14BenhDaiNgay
		,iSoNgayDuoi14OmKhac
		,fSoTienDuoi14OmKhac/@Donvitinh fSoTienDuoi14OmKhac
		,iSoNgayTren14OmKhac
		,fSoTienTren14OmKhac/@Donvitinh fSoTienTren14OmKhac

		,iSoNgayConOm
		,fSoTienConOm/@Donvitinh fSoTienConOm
		,iSoNgayPHSK
		,fSoTienPHSK/@Donvitinh fSoTienPHSK

		,(fSoTienDuoi14BenhDaiNgay+fSoTienTren14BenhDaiNgay+fSoTienDuoi14OmKhac+fSoTienTren14OmKhac+fSoTienConOm+fSoTienPHSK)/@Donvitinh as fTongTien
		,IsHangCha
		,BHangCha
		,type
		,IsParent
		from #tempKQAll

		drop table #TBL_TroCapOmDauDuToan;
		drop table #TBL_TroCapOmDauHachToan;
		drop table #tempDuToan
		drop table #tempSQDuToan
		drop table #tempQNCNDuToan
		drop table #tempCNVCQPDuToan
		drop table #tempHSQBSDuToan
		drop table #tempLDHDDuToan
		DROP TABLE #tempDetalSQDuToan
		DROP TABLE #tempDetalQNCNDuToan
		DROP TABLE #tempDetalCNVCQPDuToan
		DROP TABLE #tempDetalHSQBSDuToan
		DROP TABLE #tempDetalLDHDDuToan
		DROP TABLE #tempTotalDuToan
		DROP TABLE #tempDetalSQDuToanSum
		DROP TABLE #tempDetalQNCNDuToanSum
		DROP TABLE #tempDetalCNVCQPDuToanSum
		DROP TABLE #tempDetalHSQBSDuToanSum
		DROP TABLE #tempDetalLDHDDuToanSum

		drop table #tempHachToan
		drop table #tempSQHachToan
		drop table #tempQNCNHachToan
		drop table #tempCNVCQPHachToan
		drop table #tempHSQBSHachToan
		drop table #tempLDHDHachToan
		DROP TABLE #tempDetalSQHachToan
		DROP TABLE #tempDetalQNCNHachToan
		DROP TABLE #tempDetalCNVCQPHachToan
		DROP TABLE #tempDetalHSQBSHachToan
		DROP TABLE #tempDetalLDHDHachToan
		DROP TABLE #tempTotalHachToan
		DROP TABLE #tempDetalSQHachToanSum
		DROP TABLE #tempDetalQNCNHachToanSum
		DROP TABLE #tempDetalCNVCQPHachToanSum
		DROP TABLE #tempDetalHSQBSHachToanSum
		DROP TABLE #tempDetalLDHDHachToanSum

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap du toan
		SELECT gt.* ,dv.sTenDonVi
			into #TBL_TroCapThaiSanDuToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						Inner JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0002%' )
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								--AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec
								AND ct.iQuyChungTu = @IQuy 

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapThaiSanHachToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						Inner JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE ( gt.sXauNoiMa like '9010002-010-011-0002%')
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								--AND dv.iKhoi=1
								AND ct.iQuyChungTu = @IQuy 

		select 
		N'I. Khối dự toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempDuToan

		-- SQ Du Toan
		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQDuToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNDuToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPDuToan

		select 
		N'4. HSQ, BS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSDuToan

		select 
		N'5. LĐHĐ' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDDuToan

----- Hach Toan
		select 
		N'II. Khối hạch toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHachToan

		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQHachToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNHachToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPHachToan

		select 
		N'4. HSQ, BS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSHachToan

		select 
		N'5. LĐHĐ' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDHachToan

		--- Lay thong tin giai thich theo khoi du toan


		---Du Toan SQ
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSKThaiSan
				
				INTO #tempDetalSQDuToanSum
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


		SELECT 
			CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQDuToan 
				FROM #tempDetalSQDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		---Du Toan QNCN
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalQNCNDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNDuToan 
				FROM #tempDetalQNCNDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		---Du Toan CNVCQP
		SELECT 

				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalCNVCQPDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPDuToan 
				FROM #tempDetalCNVCQPDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		-- Du Toan HSQBS
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalHSQBSDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSDuToan 
				FROM #tempDetalHSQBSDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		-- Du Toan LDHD
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalLDHDDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDDuToan 
				FROM #tempDetalLDHDDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		--- Lay thong tin giai thich theo khoi Hach Toan
		---Hach Toan SQ
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalSQHachToanSum 
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQHachToan 
				FROM #tempDetalSQHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi
		---Hach Toan QNCN
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalQNCNHachToanSum 
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNHachToan 
				FROM #tempDetalQNCNHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		---Hach Toan CNVCQP
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalCNVCQPHachToanSum
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPHachToan 
				FROM #tempDetalCNVCQPHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		-- Hach Toan HSQBS
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalHSQBSHachToanSum
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSHachToan 
				FROM #tempDetalHSQBSHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		-- Hach Toan LDHD
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalLDHDHachToanSum
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDHachToan 
				FROM #tempDetalLDHDHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
					update SQ
			set SQ.iSoNgaySinhConNNuoiCon=ISNULL(DetailSQ.iSoNgaySinhConNNuoiCon,0),
			SQ.fSoTienSinhConNNuoiCon=ISNULL(DetailSQ.fSoTienSinhConNNuoiCon,0),
			SQ.iSoNgaySinhTroCapSinhCon=ISNULL(DetailSQ.iSoNgaySinhTroCapSinhCon,0),
			SQ.fSoTienSinhTroCapSinhCon=ISNULL(DetailSQ.fSoTienSinhTroCapSinhCon,0),
			SQ.iSoNgayKhamThaiKHHGD=ISNULL(DetailSQ.iSoNgayKhamThaiKHHGD,0),
			SQ.fSoTienKhamThaiKHHGD=ISNULL(DetailSQ.fSoTienKhamThaiKHHGD,0),
			SQ.iSoNgayPHSKThaiSan=ISNULL(DetailSQ.iSoNgayPHSKThaiSan,0),
			SQ.fSoTienPHSKThaiSan=ISNULL(DetailSQ.fSoTienPHSKThaiSan,0)
			FROM #tempSQDuToan SQ,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 2 type
			FROM #tempDetalSQDuToan) DetailSQ
			where DetailSQ.type=SQ.type

			update QNCN
			set QNCN.iSoNgaySinhConNNuoiCon=ISNULL(DetailQNCN.iSoNgaySinhConNNuoiCon,0),
			QNCN.fSoTienSinhConNNuoiCon=ISNULL(DetailQNCN.fSoTienSinhConNNuoiCon,0),
			QNCN.iSoNgaySinhTroCapSinhCon=ISNULL(DetailQNCN.iSoNgaySinhTroCapSinhCon,0),
			QNCN.fSoTienSinhTroCapSinhCon=ISNULL(DetailQNCN.fSoTienSinhTroCapSinhCon,0),
			QNCN.iSoNgayKhamThaiKHHGD=ISNULL(DetailQNCN.iSoNgayKhamThaiKHHGD,0),
			QNCN.fSoTienKhamThaiKHHGD=ISNULL(DetailQNCN.fSoTienKhamThaiKHHGD,0),
			QNCN.iSoNgayPHSKThaiSan=ISNULL(DetailQNCN.iSoNgayPHSKThaiSan,0),
			QNCN.fSoTienPHSKThaiSan=ISNULL(DetailQNCN.fSoTienPHSKThaiSan,0)
			FROM #tempQNCNDuToan QNCN,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalQNCNDuToan) DetailQNCN

			update CNVCQP
			set CNVCQP.iSoNgaySinhConNNuoiCon=ISNULL(DetailCNVCQP.iSoNgaySinhConNNuoiCon,0),
			CNVCQP.fSoTienSinhConNNuoiCon=ISNULL(DetailCNVCQP.fSoTienSinhConNNuoiCon,0),
			CNVCQP.iSoNgaySinhTroCapSinhCon=ISNULL(DetailCNVCQP.iSoNgaySinhTroCapSinhCon,0),
			CNVCQP.fSoTienSinhTroCapSinhCon=ISNULL(DetailCNVCQP.fSoTienSinhTroCapSinhCon,0),
			CNVCQP.iSoNgayKhamThaiKHHGD=ISNULL(DetailCNVCQP.iSoNgayKhamThaiKHHGD,0),
			CNVCQP.fSoTienKhamThaiKHHGD=ISNULL(DetailCNVCQP.fSoTienKhamThaiKHHGD,0),
			CNVCQP.iSoNgayPHSKThaiSan=ISNULL(DetailCNVCQP.iSoNgayPHSKThaiSan,0),
			CNVCQP.fSoTienPHSKThaiSan=ISNULL(DetailCNVCQP.fSoTienPHSKThaiSan,0)
			FROM #tempCNVCQPDuToan CNVCQP,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalCNVCQPDuToan) DetailCNVCQP

			update #tempHSQBSDuToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailHSQBS.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailHSQBS.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailHSQBS.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailHSQBS.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailHSQBS.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailHSQBS.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailHSQBS.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailHSQBS.fSoTienPHSKThaiSan,0)
			FROM #tempHSQBSDuToan HSQBS,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalHSQBSDuToan) DetailHSQBS

			update #tempLDHDDuToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailLDHD.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailLDHD.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailLDHD.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailLDHD.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailLDHD.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailLDHD.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailLDHD.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailLDHD.fSoTienPHSKThaiSan,0)
			FROM #tempLDHDDuToan LDHD,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalLDHDDuToan) DetailLDHD
		----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
					update SQ
			set SQ.iSoNgaySinhConNNuoiCon=ISNULL(DetailSQ.iSoNgaySinhConNNuoiCon,0),
			SQ.fSoTienSinhConNNuoiCon=ISNULL(DetailSQ.fSoTienSinhConNNuoiCon,0),
			SQ.iSoNgaySinhTroCapSinhCon=ISNULL(DetailSQ.iSoNgaySinhTroCapSinhCon,0),
			SQ.fSoTienSinhTroCapSinhCon=ISNULL(DetailSQ.fSoTienSinhTroCapSinhCon,0),
			SQ.iSoNgayKhamThaiKHHGD=ISNULL(DetailSQ.iSoNgayKhamThaiKHHGD,0),
			SQ.fSoTienKhamThaiKHHGD=ISNULL(DetailSQ.fSoTienKhamThaiKHHGD,0),
			SQ.iSoNgayPHSKThaiSan=ISNULL(DetailSQ.iSoNgayPHSKThaiSan,0),
			SQ.fSoTienPHSKThaiSan=ISNULL(DetailSQ.fSoTienPHSKThaiSan,0)
			FROM #tempSQHachToan SQ,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 2 type
			FROM #tempDetalSQHachToan) DetailSQ
			where DetailSQ.type=SQ.type

			update QNCN
			set QNCN.iSoNgaySinhConNNuoiCon=ISNULL(DetailQNCN.iSoNgaySinhConNNuoiCon,0),
			QNCN.fSoTienSinhConNNuoiCon=ISNULL(DetailQNCN.fSoTienSinhConNNuoiCon,0),
			QNCN.iSoNgaySinhTroCapSinhCon=ISNULL(DetailQNCN.iSoNgaySinhTroCapSinhCon,0),
			QNCN.fSoTienSinhTroCapSinhCon=ISNULL(DetailQNCN.fSoTienSinhTroCapSinhCon,0),
			QNCN.iSoNgayKhamThaiKHHGD=ISNULL(DetailQNCN.iSoNgayKhamThaiKHHGD,0),
			QNCN.fSoTienKhamThaiKHHGD=ISNULL(DetailQNCN.fSoTienKhamThaiKHHGD,0),
			QNCN.iSoNgayPHSKThaiSan=ISNULL(DetailQNCN.iSoNgayPHSKThaiSan,0),
			QNCN.fSoTienPHSKThaiSan=ISNULL(DetailQNCN.fSoTienPHSKThaiSan,0)
			FROM #tempQNCNHachToan QNCN,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalQNCNHachToan) DetailQNCN

			update CNVCQP
			set CNVCQP.iSoNgaySinhConNNuoiCon=ISNULL(DetailCNVCQP.iSoNgaySinhConNNuoiCon,0),
			CNVCQP.fSoTienSinhConNNuoiCon=ISNULL(DetailCNVCQP.fSoTienSinhConNNuoiCon,0),
			CNVCQP.iSoNgaySinhTroCapSinhCon=ISNULL(DetailCNVCQP.iSoNgaySinhTroCapSinhCon,0),
			CNVCQP.fSoTienSinhTroCapSinhCon=ISNULL(DetailCNVCQP.fSoTienSinhTroCapSinhCon,0),
			CNVCQP.iSoNgayKhamThaiKHHGD=ISNULL(DetailCNVCQP.iSoNgayKhamThaiKHHGD,0),
			CNVCQP.fSoTienKhamThaiKHHGD=ISNULL(DetailCNVCQP.fSoTienKhamThaiKHHGD,0),
			CNVCQP.iSoNgayPHSKThaiSan=ISNULL(DetailCNVCQP.iSoNgayPHSKThaiSan,0),
			CNVCQP.fSoTienPHSKThaiSan=ISNULL(DetailCNVCQP.fSoTienPHSKThaiSan,0)
			FROM #tempCNVCQPHachToan CNVCQP,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalCNVCQPHachToan) DetailCNVCQP

			update #tempHSQBSHachToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailHSQBS.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailHSQBS.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailHSQBS.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailHSQBS.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailHSQBS.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailHSQBS.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailHSQBS.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailHSQBS.fSoTienPHSKThaiSan,0)
			FROM #tempHSQBSHachToan HSQBS,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalHSQBSHachToan) DetailHSQBS

			update #tempLDHDHachToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailLDHD.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailLDHD.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailLDHD.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailLDHD.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailLDHD.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailLDHD.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailLDHD.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailLDHD.fSoTienPHSKThaiSan,0)
			FROM #tempLDHDHachToan LDHD,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalLDHDHachToan) DetailLDHD

---Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempSQDuToan
					UNION ALL
					SELECT * FROM  #tempDetalSQDuToan
					UNION ALL
					SELECT * FROM  #tempQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempLDHDDuToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDDuToan
			) TEMPDuToan

------ Update total khoi du toan
			SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			INTO #tempTotalDuToan
			FROM #tempkqDuToan
			WHERE type=2

			update T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqDuToan T1, #tempTotalDuToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'I. Khối dự toán'

---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempSQHachToan
					UNION ALL
					SELECT * FROM  #tempDetalSQHachToan
					UNION ALL
					SELECT * FROM  #tempQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempLDHDHachToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDHachToan
			) TEMPHachToan

			------ Update total khoi hach toan
			SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			INTO #tempTotalHachToan
			FROM #tempkqHachToan
			WHERE type=2

			UPDATE  T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqHachToan T1, #tempTotalHachToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'II. Khối hạch toán'

		SELECT * INTO #tempKQAll FROM
			(
					SELECT * FROM #tempkqDuToan
					UNION ALL
					SELECT * FROM  #tempkqHachToan
					
			) TEMPKQAll

		select sTT
		, iID_MaDonVi
		,sTenDonVi
		,iSoNgaySinhConNNuoiCon
		,fSoTienSinhConNNuoiCon/@Donvitinh fSoTienSinhConNNuoiCon
		,iSoNgaySinhTroCapSinhCon
		,fSoTienSinhTroCapSinhCon/@Donvitinh fSoTienSinhTroCapSinhCon
		,iSoNgayKhamThaiKHHGD
		,fSoTienKhamThaiKHHGD/@Donvitinh fSoTienKhamThaiKHHGD
		,iSoNgayPHSKThaiSan
		,fSoTienPHSKThaiSan/@Donvitinh fSoTienPHSKThaiSan
		,(fSoTienSinhConNNuoiCon+fSoTienSinhTroCapSinhCon+fSoTienKhamThaiKHHGD+fSoTienPHSKThaiSan)/@Donvitinh as fTongTien
		,IsHangCha
		,BHangCha
		,type
		,IsParent
		from #tempKQAll

		drop table #TBL_TroCapThaiSanDuToan;
		drop table #TBL_TroCapThaiSanHachToan;
		drop table #tempDuToan
		drop table #tempSQDuToan
		drop table #tempQNCNDuToan
		drop table #tempCNVCQPDuToan
		drop table #tempHSQBSDuToan
		drop table #tempLDHDDuToan
		DROP TABLE #tempDetalSQDuToan
		DROP TABLE #tempDetalQNCNDuToan
		DROP TABLE #tempDetalCNVCQPDuToan
		DROP TABLE #tempDetalHSQBSDuToan
		DROP TABLE #tempDetalLDHDDuToan
		DROP TABLE #tempTotalDuToan
		DROP TABLE #tempDetalSQDuToanSum
		DROP TABLE #tempDetalQNCNDuToanSum
		DROP TABLE #tempDetalCNVCQPDuToanSum
		DROP TABLE #tempDetalHSQBSDuToanSum
		DROP TABLE #tempDetalLDHDDuToanSum


		drop table #tempHachToan
		drop table #tempSQHachToan
		drop table #tempQNCNHachToan
		drop table #tempCNVCQPHachToan
		drop table #tempHSQBSHachToan
		drop table #tempLDHDHachToan
		DROP TABLE #tempDetalSQHachToan
		DROP TABLE #tempDetalQNCNHachToan
		DROP TABLE #tempDetalCNVCQPHachToan
		DROP TABLE #tempDetalHSQBSHachToan
		DROP TABLE #tempDetalLDHDHachToan
		DROP TABLE #tempTotalHachToan

		DROP TABLE #tempDetalSQHachToanSum
		DROP TABLE #tempDetalQNCNHachToanSum
		DROP TABLE #tempDetalCNVCQPHachToanSum
		DROP TABLE #tempDetalHSQBSHachToanSum
		DROP TABLE #tempDetalLDHDHachToanSum


		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap
	SELECT gt.*,dv.sTenDonVi into #TBL_TroCapTaiNan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
					AND dv.iNamLamViec=@INamLamViec

		SELECT
			ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC) AS STT
			,tbltctn.sTenCanBo
			,tbltctn.sTenDonVi sTenPhanHo
			, tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

			---- Chi giám định mức suy giảm KNLĐ (người)1
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
			
			---- Chi giám định mức suy giảm KNLĐ (người)1 truy lĩnh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL

			---- Trợ cấp 1 lần (người)2
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan

			---- Trợ cấp 1 lần (người)2 truy lĩnh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


			--- - Chi hỗ Trợ phòng người (người)
			-- - Chi h.trợ chuyển đổi n.nghiệp (người)
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
			--- - Chi hỗ Trợ phòng người (người) truy linh
			-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
			---- Trợ cấp hàng tháng (người)
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end ) FTienTCHangThang
			
			---- Trợ cấp hàng tháng (người) truy linh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end ) FTienTCHangThangTL
			
			--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV

			--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL

			--- - Trợ cấp chết do TNLD. BNN (người) 
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD

			--- - Trợ cấp chết do TNLD. BNN (người) truy linh
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL

			--- - DS, PHSK sau TNLĐ, BNN (người)
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

			--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
			,( CASE WHEN tbltctn.sXauNoiMa like '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa like '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END ) ISoNgayDSPHSKTL

			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK
			
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL,
			0 IsHangCha
			INTO #TEMPTroCapTaiNanDetail
		FROM #TBL_TroCapTaiNan tbltctn
		GROUP BY tbltctn.sTenCanBo,
			tbltctn.sTenDonVi,
			tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, tbltctn.dNgayQuyetDinh
			,tbltctn.sXauNoiMa 

		SELECT
			ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC) AS STT
			,tbltctn.sTenCanBo
			,tbltctn.sTenPhanHo
			, tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			,  tbltctn.SNgayQuyetDinh
			, 0 IsHangCha
			, SUM(FTienGiamDinh) FTienGiamDinh
			, SUM(FTienGiamDinhTL) FTienGiamDinhTL
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTCTP) FTienTCTP
			, SUM(FTienTCTPTL) FTienTCTPTL
			, SUM(FTienTCHangThang) FTienTCHangThang
			, SUM(FTienTCHangThangTL) FTienTCHangThangTL
			, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
			, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, SUM(FTienTCCDTNLD) FTienTCCDTNLD
			, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
			, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, SUM(FTienDSPHSK) FTienDSPHSK
			, SUM(FTienDSPHSKTL) FTienDSPHSKTL
			FROM  #TEMPTroCapTaiNanDetail tbltctn
			GROUP BY tbltctn.sTenCanBo,
			tbltctn.sTenPhanHo,
			tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, tbltctn.SNgayQuyetDinh
			

		DROP TABLE #TBL_TroCapTaiNan
		DROP TABLE #TEMPTroCapTaiNanDetail


end
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap du toan
	SELECT gt.*,dv.sTenDonVi into #TBL_TroCapTaiNanDuToan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
					--AND dv.iKhoi=2
					AND ct.iNamChungTu=@INamLamViec
	---Lấy thông tin chi tiết giai thich tro cap hach toan
	SELECT gt.*,dv.sTenDonVi into #TBL_TroCapTaiNanHachToan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE ( gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
					--AND dv.iKhoi=1
					AND ct.iNamChungTu=@INamLamViec

	---I. Khối dự toán
		SELECT 
		N'I. Khối dự toán' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,3 as type
		,1 IsHangCha
		,1 IsParent
		into #tempDuToan

		--- Total SQ DuToan
		SELECT 
		N'1. Sĩ quan' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempSQDuToan

		--- Total QNCN DuToan
		SELECT 
		N'2. QNCN' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempQNCNDuToan

		--- Total CNVCQP DuToan
		SELECT 
		N'3. CNVCQP' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempCNVCQPDuToan

		--- Total HSQ, BS  DuToan
		SELECT 
		N'4. HSQ, BS' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHSQBSDuToan

		---	Total LDHD DuToan
		SELECT 
		N'5. LĐHĐ' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempLDHDDuToan		

	---II. Khối hạch toán
		SELECT 
		N'II. Khối hạch toán' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,3 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHachToan
		--- Total SQ HachToan
		SELECT 
		N'1. Sĩ quan' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempSQHachToan

		--- Total QNCN HachToan
		SELECT 
		N'2. QNCN' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempQNCNHachToan

		--- Total CNVCQP HachToan
		SELECT 
		N'3. CNVCQP' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempCNVCQPHachToan

		--- Total HSQBS  HachToan
		SELECT 
		N'4. HSQ, BS' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHSQBSHachToan

		--- Total LĐHĐ HachToan
		SELECT 
		N'5. LĐHĐ' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempLDHDHachToan	

	----- Lay ra  du toan
		-- Du Toan SQ
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailSQDuToanSum
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '1%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				INTO #tempDetailSQDuToan
				FROM #tempDetailSQDuToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

		-- Du Toan QNCN
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into  #tempDetailQNCNDuToanSum
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '2%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				INTO #tempDetailQNCNDuToan
				FROM #tempDetailQNCNDuToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

		-- Du Toan CNVCQP
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailCNVCQPDuToanSum
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '3.1%' OR tbltctn.sMaCapBac LIKE '3.2%' OR tbltctn.sMaCapBac LIKE '3.3%'  OR tbltctn.sMaCapBac = '413' OR tbltctn.sMaCapBac = '415'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				INTO #tempDetailCNVCQPDuToan
				FROM #tempDetailCNVCQPDuToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

		-- Du Toan HSQBS
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK
				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				 into #tempDetailHSQBSDuToanSum
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '0%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				 into #tempDetailHSQBSDuToan
				FROM #tempDetailHSQBSDuToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.sNgayQuyetDinh

		-- Du Toan LDHD
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK
				
				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailLDHDDuToanSum
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac = '423' OR tbltctn.sMaCapBac = '425' OR tbltctn.sMaCapBac = '43'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailLDHDDuToan
				FROM #tempDetailLDHDDuToanSum tbltctn 
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
	----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD

			update SQ
			set SQ.FTienGiamDinh=ISNULL(DetailSQ.FTienGiamDinh,0),
			SQ.FTienTroCap1Lan=ISNULL(DetailSQ.FTienTroCap1Lan,0),
			SQ.FTienTCTP=ISNULL(DetailSQ.FTienTCTP,0),
			SQ.FTienTCHangThang=ISNULL(DetailSQ.FTienTCHangThang,0),
			SQ.FTienTCPHCNvPV=ISNULL(DetailSQ.FTienTCPHCNvPV,0),
			SQ.FTienTCCDTNLD=ISNULL(DetailSQ.FTienTCCDTNLD,0),
			SQ.ISoNgayDSPHSK=ISNULL(DetailSQ.ISoNgayDSPHSK,0),
			SQ.FTienDSPHSK=ISNULL(DetailSQ.FTienDSPHSK,0),
			SQ.FTienGiamDinhTL=ISNULL(DetailSQ.FTienGiamDinhTL,0),
			SQ.FTienTroCap1LanTL=ISNULL(DetailSQ.FTienTroCap1LanTL,0),
			SQ.FTienTCTPTL=ISNULL(DetailSQ.FTienTCTPTL,0),
			SQ.FTienTCHangThangTL=ISNULL(DetailSQ.FTienTCHangThangTL,0),
			SQ.FTienTCPHCNvPVTL=ISNULL(DetailSQ.FTienTCPHCNvPVTL,0),
			SQ.FTienTCCDTNLDTL=ISNULL(DetailSQ.FTienTCCDTNLDTL,0),
			SQ.ISoNgayDSPHSKTL=ISNULL(DetailSQ.ISoNgayDSPHSKTL,0),
			SQ.FTienDSPHSKTL=ISNULL(DetailSQ.FTienDSPHSKTL,0)
			FROM #tempSQDuToan SQ,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			, 2 type
			FROM #tempDetailSQDuToan) DetailSQ
			where DetailSQ.type=SQ.type

			update QNCN
			set QNCN.FTienGiamDinh=ISNULL(DetailQNCN.FTienGiamDinh,0),
			QNCN.FTienTroCap1Lan=ISNULL(DetailQNCN.FTienTroCap1Lan,0),
			QNCN.FTienTCTP=ISNULL(DetailQNCN.FTienTCTP,0),
			QNCN.FTienTCHangThang=ISNULL(DetailQNCN.FTienTCHangThang,0),
			QNCN.FTienTCPHCNvPV=ISNULL(DetailQNCN.FTienTCPHCNvPV,0),
			QNCN.FTienTCCDTNLD=ISNULL(DetailQNCN.FTienTCCDTNLD,0),
			QNCN.ISoNgayDSPHSK=ISNULL(DetailQNCN.ISoNgayDSPHSK,0),
			QNCN.FTienDSPHSK=ISNULL(DetailQNCN.FTienDSPHSK,0),
			QNCN.FTienGiamDinhTL=ISNULL(DetailQNCN.FTienGiamDinhTL,0),
			QNCN.FTienTroCap1LanTL=ISNULL(DetailQNCN.FTienTroCap1LanTL,0),
			QNCN.FTienTCTPTL=ISNULL(DetailQNCN.FTienTCTPTL,0),
			QNCN.FTienTCHangThangTL=ISNULL(DetailQNCN.FTienTCHangThangTL,0),
			QNCN.FTienTCPHCNvPVTL=ISNULL(DetailQNCN.FTienTCPHCNvPVTL,0),
			QNCN.FTienTCCDTNLDTL=ISNULL(DetailQNCN.FTienTCCDTNLDTL,0),
			QNCN.ISoNgayDSPHSKTL=ISNULL(DetailQNCN.ISoNgayDSPHSKTL,0),
			QNCN.FTienDSPHSKTL=ISNULL(DetailQNCN.FTienDSPHSKTL,0)
			FROM #tempQNCNDuToan QNCN,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailQNCNDuToan) DetailQNCN


			update CNVCQP
			set CNVCQP.FTienGiamDinh=ISNULL(DetailCNVCQP.FTienGiamDinh,0),
			CNVCQP.FTienTroCap1Lan=ISNULL(DetailCNVCQP.FTienTroCap1Lan,0),
			CNVCQP.FTienTCTP=ISNULL(DetailCNVCQP.FTienTCTP,0),
			CNVCQP.FTienTCHangThang=ISNULL(DetailCNVCQP.FTienTCHangThang,0),
			CNVCQP.FTienTCPHCNvPV=ISNULL(DetailCNVCQP.FTienTCPHCNvPV,0),
			CNVCQP.FTienTCCDTNLD=ISNULL(DetailCNVCQP.FTienTCCDTNLD,0),
			CNVCQP.ISoNgayDSPHSK=ISNULL(DetailCNVCQP.ISoNgayDSPHSK,0),
			CNVCQP.FTienDSPHSK=ISNULL(DetailCNVCQP.FTienDSPHSK,0),
			CNVCQP.FTienGiamDinhTL=ISNULL(DetailCNVCQP.FTienGiamDinhTL,0),
			CNVCQP.FTienTroCap1LanTL=ISNULL(DetailCNVCQP.FTienTroCap1LanTL,0),
			CNVCQP.FTienTCTPTL=ISNULL(DetailCNVCQP.FTienTCTPTL,0),
			CNVCQP.FTienTCHangThangTL=ISNULL(DetailCNVCQP.FTienTCHangThangTL,0),
			CNVCQP.FTienTCPHCNvPVTL=ISNULL(DetailCNVCQP.FTienTCPHCNvPVTL,0),
			CNVCQP.FTienTCCDTNLDTL=ISNULL(DetailCNVCQP.FTienTCCDTNLDTL,0),
			CNVCQP.ISoNgayDSPHSKTL=ISNULL(DetailCNVCQP.ISoNgayDSPHSKTL,0),
			CNVCQP.FTienDSPHSKTL=ISNULL(DetailCNVCQP.FTienDSPHSKTL,0)
			FROM #tempCNVCQPDuToan CNVCQP,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailCNVCQPDuToan) DetailCNVCQP

			update #tempHSQBSDuToan
			set FTienGiamDinh=ISNULL(DetailHSQBS.FTienGiamDinh,0),
			FTienTroCap1Lan=ISNULL(DetailHSQBS.FTienTroCap1Lan,0),
			FTienTCTP=ISNULL(DetailHSQBS.FTienTCTP,0),
			FTienTCHangThang=ISNULL(DetailHSQBS.FTienTCHangThang,0),
			FTienTCPHCNvPV=ISNULL(DetailHSQBS.FTienTCPHCNvPV,0),
			FTienTCCDTNLD=ISNULL(DetailHSQBS.FTienTCCDTNLD,0),
			ISoNgayDSPHSK=ISNULL(DetailHSQBS.ISoNgayDSPHSK,0),
			FTienDSPHSK=ISNULL(DetailHSQBS.FTienDSPHSK,0),
			FTienGiamDinhTL=ISNULL(DetailHSQBS.FTienGiamDinhTL,0),
			FTienTroCap1LanTL=ISNULL(DetailHSQBS.FTienTroCap1LanTL,0),
			FTienTCTPTL=ISNULL(DetailHSQBS.FTienTCTPTL,0),
			FTienTCHangThangTL=ISNULL(DetailHSQBS.FTienTCHangThangTL,0),
			FTienTCPHCNvPVTL=ISNULL(DetailHSQBS.FTienTCPHCNvPVTL,0),
			FTienTCCDTNLDTL=ISNULL(DetailHSQBS.FTienTCCDTNLDTL,0),
			ISoNgayDSPHSKTL=ISNULL(DetailHSQBS.ISoNgayDSPHSKTL,0),
			FTienDSPHSKTL=ISNULL(DetailHSQBS.FTienDSPHSKTL,0)
			FROM #tempHSQBSDuToan HSQBS,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailHSQBSDuToan) DetailHSQBS

			update #tempLDHDDuToan
			set FTienGiamDinh=ISNULL(DetailLDHD.FTienGiamDinh,0),
			FTienTroCap1Lan=ISNULL(DetailLDHD.FTienTroCap1Lan,0),
			FTienTCTP=ISNULL(DetailLDHD.FTienTCTP,0),
			FTienTCHangThang=ISNULL(DetailLDHD.FTienTCHangThang,0),
			FTienTCPHCNvPV=ISNULL(DetailLDHD.FTienTCPHCNvPV,0),
			FTienTCCDTNLD=ISNULL(DetailLDHD.FTienTCCDTNLD,0),
			ISoNgayDSPHSK=ISNULL(DetailLDHD.ISoNgayDSPHSK,0),
			FTienDSPHSK=ISNULL(DetailLDHD.FTienDSPHSK,0),
			FTienGiamDinhTL=ISNULL(DetailLDHD.FTienGiamDinhTL,0),
			FTienTroCap1LanTL=ISNULL(DetailLDHD.FTienTroCap1LanTL,0),
			FTienTCTPTL=ISNULL(DetailLDHD.FTienTCTPTL,0),
			FTienTCHangThangTL=ISNULL(DetailLDHD.FTienTCHangThangTL,0),
			FTienTCPHCNvPVTL=ISNULL(DetailLDHD.FTienTCPHCNvPVTL,0),
			FTienTCCDTNLDTL=ISNULL(DetailLDHD.FTienTCCDTNLDTL,0),
			ISoNgayDSPHSKTL=ISNULL(DetailLDHD.ISoNgayDSPHSKTL,0),
			FTienDSPHSKTL=ISNULL(DetailLDHD.FTienDSPHSKTL,0)
			FROM #tempLDHDDuToan LDHD,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailLDHDDuToan) DetailLDHD

	----- Lay ra hach toan
		-- Hạch Toan SQ
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailSQHachToanSum
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '1%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailSQHachToan
			FROM #tempDetailSQHachToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

		-- Hạch Toan QNCN
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailQNCNHachToanSum
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '2%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailQNCNHachToan
			FROM #tempDetailQNCNHachToanSum tbltctn
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
	
		-- Hạch Toan CNVCQP
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailCNVCQPHachToanSum
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '3.1%' OR tbltctn.sMaCapBac LIKE '3.2%' OR tbltctn.sMaCapBac LIKE '3.3%'  OR tbltctn.sMaCapBac = '413' OR tbltctn.sMaCapBac = '415'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailCNVCQPHachToan
			FROM #tempDetailCNVCQPHachToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

		-- Hạch Toan HSQBS
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailHSQBSHachToanSum
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '0%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailHSQBSHachToan
			FROM #tempDetailHSQBSHachToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

		-- Hạch Toan LDHD
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailLDHDHachToanSum
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac = '423' OR tbltctn.sMaCapBac = '425' OR tbltctn.sMaCapBac = '43'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailLDHDHachToan
			FROM #tempDetailLDHDHachToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

	----- Update Hach Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
			update SQHachToan
			set FTienGiamDinh=ISNULL(DetailSQHachToan.FTienGiamDinh,0),
			FTienTroCap1Lan=ISNULL(DetailSQHachToan.FTienTroCap1Lan,0),
			FTienTCTP=ISNULL(DetailSQHachToan.FTienTCTP,0),
			FTienTCHangThang=ISNULL(DetailSQHachToan.FTienTCHangThang,0),
			FTienTCPHCNvPV=ISNULL(DetailSQHachToan.FTienTCPHCNvPV,0),
			FTienTCCDTNLD=ISNULL(DetailSQHachToan.FTienTCCDTNLD,0),
			ISoNgayDSPHSK=ISNULL(DetailSQHachToan.ISoNgayDSPHSK,0),
			FTienDSPHSK=ISNULL(DetailSQHachToan.FTienDSPHSK,0),
			FTienGiamDinhTL=ISNULL(DetailSQHachToan.FTienGiamDinhTL,0),
			FTienTroCap1LanTL=ISNULL(DetailSQHachToan.FTienTroCap1LanTL,0),
			FTienTCTPTL=ISNULL(DetailSQHachToan.FTienTCTPTL,0),
			FTienTCHangThangTL=ISNULL(DetailSQHachToan.FTienTCHangThangTL,0),
			FTienTCPHCNvPVTL=ISNULL(DetailSQHachToan.FTienTCPHCNvPVTL,0),
			FTienTCCDTNLDTL=ISNULL(DetailSQHachToan.FTienTCCDTNLDTL,0),
			ISoNgayDSPHSKTL=ISNULL(DetailSQHachToan.ISoNgayDSPHSKTL,0),
			FTienDSPHSKTL=ISNULL(DetailSQHachToan.FTienDSPHSKTL,0)
			FROM #tempSQHachToan SQHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailSQHachToan) DetailSQHachToan

			update QNCNHachToan
			set QNCNHachToan.FTienGiamDinh=ISNULL(DetailQNCNHachToan.FTienGiamDinh,0),
			QNCNHachToan.FTienTroCap1Lan=ISNULL(DetailQNCNHachToan.FTienTroCap1Lan,0),
			QNCNHachToan.FTienTCTP=ISNULL(DetailQNCNHachToan.FTienTCTP,0),
			QNCNHachToan.FTienTCHangThang=ISNULL(DetailQNCNHachToan.FTienTCHangThang,0),
			QNCNHachToan.FTienTCPHCNvPV=ISNULL(DetailQNCNHachToan.FTienTCPHCNvPV,0),
			QNCNHachToan.FTienTCCDTNLD=ISNULL(DetailQNCNHachToan.FTienTCCDTNLD,0),
			QNCNHachToan.ISoNgayDSPHSK=ISNULL(DetailQNCNHachToan.ISoNgayDSPHSK,0),
			QNCNHachToan.FTienDSPHSK=ISNULL(DetailQNCNHachToan.FTienDSPHSK,0),
			QNCNHachToan.FTienGiamDinhTL=ISNULL(DetailQNCNHachToan.FTienGiamDinhTL,0),
			QNCNHachToan.FTienTroCap1LanTL=ISNULL(DetailQNCNHachToan.FTienTroCap1LanTL,0),
			QNCNHachToan.FTienTCTPTL=ISNULL(DetailQNCNHachToan.FTienTCTPTL,0),
			QNCNHachToan.FTienTCHangThangTL=ISNULL(DetailQNCNHachToan.FTienTCHangThangTL,0),
			QNCNHachToan.FTienTCPHCNvPVTL=ISNULL(DetailQNCNHachToan.FTienTCPHCNvPVTL,0),
			QNCNHachToan.FTienTCCDTNLDTL=ISNULL(DetailQNCNHachToan.FTienTCCDTNLDTL,0),
			QNCNHachToan.ISoNgayDSPHSKTL=ISNULL(DetailQNCNHachToan.ISoNgayDSPHSKTL,0),
			QNCNHachToan.FTienDSPHSKTL=ISNULL(DetailQNCNHachToan.FTienDSPHSKTL,0)
			FROM #tempQNCNHachToan QNCNHachToan, 
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailQNCNHachToan) DetailQNCNHachToan

			update CNVCQPHachToan
			set CNVCQPHachToan.FTienGiamDinh=ISNULL(DetailCNVCQPHachToan.FTienGiamDinh,0),
			CNVCQPHachToan.FTienTroCap1Lan=ISNULL(DetailCNVCQPHachToan.FTienTroCap1Lan,0),
			CNVCQPHachToan.FTienTCTP=ISNULL(DetailCNVCQPHachToan.FTienTCTP,0),
			CNVCQPHachToan.FTienTCHangThang=ISNULL(DetailCNVCQPHachToan.FTienTCHangThang,0),
			CNVCQPHachToan.FTienTCPHCNvPV=ISNULL(DetailCNVCQPHachToan.FTienTCPHCNvPV,0),
			CNVCQPHachToan.FTienTCCDTNLD=ISNULL(DetailCNVCQPHachToan.FTienTCCDTNLD,0),
			CNVCQPHachToan.ISoNgayDSPHSK=ISNULL(DetailCNVCQPHachToan.ISoNgayDSPHSK,0),
			CNVCQPHachToan.FTienDSPHSK=ISNULL(DetailCNVCQPHachToan.FTienDSPHSK,0),
			CNVCQPHachToan.FTienGiamDinhTL=ISNULL(DetailCNVCQPHachToan.FTienGiamDinhTL,0),
			CNVCQPHachToan.FTienTroCap1LanTL=ISNULL(DetailCNVCQPHachToan.FTienTroCap1LanTL,0),
			CNVCQPHachToan.FTienTCTPTL=ISNULL(DetailCNVCQPHachToan.FTienTCTPTL,0),
			CNVCQPHachToan.FTienTCHangThangTL=ISNULL(DetailCNVCQPHachToan.FTienTCHangThangTL,0),
			CNVCQPHachToan.FTienTCPHCNvPVTL=ISNULL(DetailCNVCQPHachToan.FTienTCPHCNvPVTL,0),
			CNVCQPHachToan.FTienTCCDTNLDTL=ISNULL(DetailCNVCQPHachToan.FTienTCCDTNLDTL,0),
			CNVCQPHachToan.ISoNgayDSPHSKTL=ISNULL(DetailCNVCQPHachToan.ISoNgayDSPHSKTL,0),
			CNVCQPHachToan.FTienDSPHSKTL=ISNULL(DetailCNVCQPHachToan.FTienDSPHSKTL,0)
			FROM #tempCNVCQPHachToan CNVCQPHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailCNVCQPHachToan)DetailCNVCQPHachToan

			update HSQBSHachToan
			set HSQBSHachToan.FTienGiamDinh=ISNULL(DetailHSQBSHachToan.FTienGiamDinh,0),
			HSQBSHachToan.FTienTroCap1Lan=ISNULL(DetailHSQBSHachToan.FTienTroCap1Lan,0),
			HSQBSHachToan.FTienTCTP=ISNULL(DetailHSQBSHachToan.FTienTCTP,0),
			HSQBSHachToan.FTienTCHangThang=ISNULL(DetailHSQBSHachToan.FTienTCHangThang,0),
			HSQBSHachToan.FTienTCPHCNvPV=ISNULL(DetailHSQBSHachToan.FTienTCPHCNvPV,0),
			HSQBSHachToan.FTienTCCDTNLD=ISNULL(DetailHSQBSHachToan.FTienTCCDTNLD,0),
			HSQBSHachToan.ISoNgayDSPHSK=ISNULL(DetailHSQBSHachToan.ISoNgayDSPHSK,0),
			HSQBSHachToan.FTienDSPHSK=ISNULL(DetailHSQBSHachToan.FTienDSPHSK,0),
			HSQBSHachToan.FTienGiamDinhTL=ISNULL(DetailHSQBSHachToan.FTienGiamDinhTL,0),
			HSQBSHachToan.FTienTroCap1LanTL=ISNULL(DetailHSQBSHachToan.FTienTroCap1LanTL,0),
			HSQBSHachToan.FTienTCTPTL=ISNULL(DetailHSQBSHachToan.FTienTCTPTL,0),
			HSQBSHachToan.FTienTCHangThangTL=ISNULL(DetailHSQBSHachToan.FTienTCHangThangTL,0),
			HSQBSHachToan.FTienTCPHCNvPVTL=ISNULL(DetailHSQBSHachToan.FTienTCPHCNvPVTL,0),
			HSQBSHachToan.FTienTCCDTNLDTL=ISNULL(DetailHSQBSHachToan.FTienTCCDTNLDTL,0),
			HSQBSHachToan.ISoNgayDSPHSKTL=ISNULL(DetailHSQBSHachToan.ISoNgayDSPHSKTL,0),
			HSQBSHachToan.FTienDSPHSKTL=ISNULL(DetailHSQBSHachToan.FTienDSPHSKTL,0)
			FROM #tempHSQBSHachToan HSQBSHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailHSQBSHachToan) DetailHSQBSHachToan

			update LDHDHachToan
			set LDHDHachToan.FTienGiamDinh=ISNULL(DetailLDHDHachToan.FTienGiamDinh,0),
			LDHDHachToan.FTienTroCap1Lan=ISNULL(DetailLDHDHachToan.FTienTroCap1Lan,0),
			LDHDHachToan.FTienTCTP=ISNULL(DetailLDHDHachToan.FTienTCTP,0),
			LDHDHachToan.FTienTCHangThang=ISNULL(DetailLDHDHachToan.FTienTCHangThang,0),
			LDHDHachToan.FTienTCPHCNvPV=ISNULL(DetailLDHDHachToan.FTienTCPHCNvPV,0),
			LDHDHachToan.FTienTCCDTNLD=ISNULL(DetailLDHDHachToan.FTienTCCDTNLD,0),
			LDHDHachToan.ISoNgayDSPHSK=ISNULL(DetailLDHDHachToan.ISoNgayDSPHSK,0),
			LDHDHachToan.FTienDSPHSK=ISNULL(DetailLDHDHachToan.FTienDSPHSK,0),
			LDHDHachToan.FTienGiamDinhTL=ISNULL(DetailLDHDHachToan.FTienGiamDinhTL,0),
			LDHDHachToan.FTienTroCap1LanTL=ISNULL(DetailLDHDHachToan.FTienTroCap1LanTL,0),
			LDHDHachToan.FTienTCTPTL=ISNULL(DetailLDHDHachToan.FTienTCTPTL,0),
			LDHDHachToan.FTienTCHangThangTL=ISNULL(DetailLDHDHachToan.FTienTCHangThangTL,0),
			LDHDHachToan.FTienTCPHCNvPVTL=ISNULL(DetailLDHDHachToan.FTienTCPHCNvPVTL,0),
			LDHDHachToan.FTienTCCDTNLDTL=ISNULL(DetailLDHDHachToan.FTienTCCDTNLDTL,0),
			LDHDHachToan.ISoNgayDSPHSKTL=ISNULL(DetailLDHDHachToan.ISoNgayDSPHSKTL,0),
			LDHDHachToan.FTienDSPHSKTL=ISNULL(DetailLDHDHachToan.FTienDSPHSKTL,0)
			FROM #tempLDHDHachToan LDHDHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailLDHDHachToan)DetailLDHDHachToan

---------  Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempSQDuToan
					UNION ALL
					SELECT * FROM  #tempDetailSQDuToan
					UNION ALL
					SELECT * FROM  #tempQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempDetailQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempDetailCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempDetailHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempLDHDDuToan
					UNION ALL
					SELECT * FROM  #tempDetailLDHDDuToan
			) TEMPDuToan
------ Update total khoi du toan
			SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			, 3 type
			INTO #tempTotalDuToan
			FROM #tempkqDuToan
			WHERE type=2

			update T1
			set T1.FTienGiamDinh=T2.FTienGiamDinh,
			T1.FTienTroCap1Lan=T2.FTienTroCap1Lan,
			T1.FTienTCTP=T2.FTienTCTP,
			T1.FTienTCHangThang=T2.FTienTCHangThang,
			T1.FTienTCPHCNvPV=T2.FTienTCPHCNvPV,
			T1.FTienTCCDTNLD=T2.FTienTCCDTNLD,
			T1.ISoNgayDSPHSK=T2.ISoNgayDSPHSK,
			T1.FTienDSPHSK=T2.FTienDSPHSK,
			T1.FTienGiamDinhTL=T2.FTienGiamDinhTL,
			T1.FTienTroCap1LanTL=T2.FTienTroCap1LanTL,
			T1.FTienTCTPTL=T2.FTienTCTPTL,
			T1.FTienTCHangThangTL=T2.FTienTCHangThangTL,
			T1.FTienTCPHCNvPVTL=T2.FTienTCPHCNvPVTL,
			T1.FTienTCCDTNLDTL=T2.FTienTCCDTNLDTL,
			T1.ISoNgayDSPHSKTL=T2.ISoNgayDSPHSKTL,
			T1.FTienDSPHSKTL=T2.FTienDSPHSKTL
			FROM #tempkqDuToan T1, #tempTotalDuToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'I. Khối dự toán'

---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempSQHachToan
					UNION ALL
					SELECT * FROM  #tempDetailSQHachToan
					UNION ALL
					SELECT * FROM  #tempQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempDetailQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempDetailCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempDetailHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempLDHDHachToan
					UNION ALL
					SELECT * FROM  #tempDetailLDHDHachToan
			) TEMPHachToan

------ Update total khoi hach toan
			SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			, 3 type
			INTO #tempTotalHachToan
			FROM #tempkqHachToan
			WHERE type=2

			UPDATE  T1
			set T1.FTienGiamDinh=T2.FTienGiamDinh,
			T1.FTienTroCap1Lan=T2.FTienTroCap1Lan,
			T1.FTienTCTP=T2.FTienTCTP,
			T1.FTienTCHangThang=T2.FTienTCHangThang,
			T1.FTienTCPHCNvPV=T2.FTienTCPHCNvPV,
			T1.FTienTCCDTNLD=T2.FTienTCCDTNLD,
			T1.ISoNgayDSPHSK=T2.ISoNgayDSPHSK,
			T1.FTienDSPHSK=T2.FTienDSPHSK,
			T1.FTienGiamDinhTL=T2.FTienGiamDinhTL,
			T1.FTienTroCap1LanTL=T2.FTienTroCap1LanTL,
			T1.FTienTCTPTL=T2.FTienTCTPTL,
			T1.FTienTCHangThangTL=T2.FTienTCHangThangTL,
			T1.FTienTCPHCNvPVTL=T2.FTienTCPHCNvPVTL,
			T1.FTienTCCDTNLDTL=T2.FTienTCCDTNLDTL,
			T1.ISoNgayDSPHSKTL=T2.ISoNgayDSPHSKTL,
			T1.FTienDSPHSKTL=T2.FTienDSPHSKTL
			FROM #tempkqHachToan T1, #tempTotalHachToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'II. Khối hạch toán'

--------- Tra Ve KQua ALL

		SELECT * INTO #tempKQAll FROM
			(
					SELECT * FROM #tempkqDuToan
					UNION ALL
					SELECT * FROM  #tempkqHachToan
					
			) TEMPKQAll

		select * from #tempKQAll

		DROP TABLE #TBL_TroCapTaiNanDuToan
		DROP TABLE #TBL_TroCapTaiNanHachToan

		DROP TABLE #tempDuToan
		DROP TABLE #tempSQDuToan
		DROP TABLE #tempQNCNDuToan
		DROP TABLE #tempCNVCQPDuToan
		DROP TABLE #tempHSQBSDuToan
		DROP TABLE #tempLDHDDuToan
		DROP TABLE #tempDetailSQDuToan
		DROP TABLE #tempDetailQNCNDuToan
		DROP TABLE #tempDetailCNVCQPDuToan
		DROP TABLE #tempDetailHSQBSDuToan
		DROP TABLE #tempDetailLDHDDuToan
		DROP TABLE #tempTotalDuToan

		DROP TABLE #tempHachToan
		DROP TABLE #tempSQHachToan
		DROP TABLE #tempQNCNHachToan
		DROP TABLE #tempCNVCQPHachToan
		DROP TABLE #tempHSQBSHachToan
		DROP TABLE #tempLDHDHachToan
		DROP TABLE #tempDetailSQHachToan
		DROP TABLE #tempDetailQNCNHachToan
		DROP TABLE #tempDetailCNVCQPHachToan
		DROP TABLE #tempDetailHSQBSHachToan
		DROP TABLE #tempDetailLDHDHachToan
		DROP TABLE #tempTotalHachToan

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_clone_data_update_new] 
	-- Add the parameters for the stored procedure here
	@userCreator varchar(100),
	@sourceYear int,
	@destinationYear int,
	@isUpdatedMLNS int,
	@isUpdatedNSDV int,
	@isUpdatedBQuanLy int,
	@isUpdateMLQS int,
	@isUpdateDanhMucChuyenNganh int,
	@isUpdateDanhMucNganh int,
	@isUpdateMuclucSkt int,
	@isUpdateDanhMucCapPhat int,
	@isUpdateCauHinhChiTieuLuongMLNS int,
	@isUpdateDmCapBacKh int,
	@isUpdateNSSKT int,
	@isUpdateCauHinhHeThong int,
	@isUpdateDanhMucDonViTinh int,
	@isUpdateDanhMucCanCu int,
	@isUpdateDanhMucCKTC int,
	@isUpdateDanhMucBHXH int,
	@isUpdateMucLucCacLoaiChi int,
	@isUpdateDanhMucCoSoYTe int,
	@isUpdateDanhMucTDQT int,
	@isUpdateDanhMucCHTSBHXH int,
	@isUpdateDanhMucNgayNghi int
	--@isUpdateDanhMucChuDauTu int,
	--@IsUpdateDanhMucDonviQuanLyDuAn int,
	--@isUpdateDanhMucNhaThau int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if (@isUpdateDanhMucNganh = 1)
		Begin
			DELETE FROM DanhMuc where INamLamViec = @destinationYear and [sType] = 'NS_Nganh_Nganh';
			INSERT INTO [dbo].[DanhMuc]
			   ([sType]
			   ,[iID_MaDanhMuc]
			   ,[sTen]
			   ,[sGiaTri]
			   ,[sMoTa]
			   ,[iThuTu]
			   ,[iNamLamViec]
			   ,[iTrangThai]
			   ,[dNgayTao]
			   ,[sNguoiTao]
			   ,[dNgaySua]
			   ,[sNguoiSua]
			   ,[Tag]
			   ,[Log]
			   ,[NganSachNganh])
			 SELECT
				[sType]
				,[iID_MaDanhMuc]
				,[sTen]
				,[sGiaTri]
				,[sMoTa]
				,[iThuTu]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[NganSachNganh]
		  FROM [dbo].[DanhMuc] where iNamLamViec = @sourceYear and [sType] = 'NS_Nganh_Nganh';
		End;

	if (@isUpdateDanhMucChuyenNganh = 1)
		Begin
			DELETE FROM DanhMuc where iNamLamViec = @destinationYear and [sType] = 'NS_Nganh';
			INSERT INTO [dbo].[DanhMuc]
				([sType]
				,[iID_MaDanhMuc]
				,[sTen]
				,[sGiaTri]
				,[sMoTa]
				,[iThuTu]
				,[iNamLamViec]
				,[iTrangThai]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[NganSachNganh])
			 SELECT
				[sType]
				,[iID_MaDanhMuc]
				,[sTen]
				,[sGiaTri]
				,[sMoTa]
				,[iThuTu]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
				,[NganSachNganh]
		  FROM [dbo].[DanhMuc] where iNamLamViec = @sourceYear and [sType] = 'NS_Nganh';
		End;

	if (@isUpdateMLQS = 1)
		Begin
			Delete FROM NS_QS_MucLuc where iNamLamViec = @destinationYear;
			INSERT INTO [NS_QS_MucLuc]
				([iID_MLNS]
				,[iID_MLNS_Cha]
				,[sM]
				,[sTM]
				,[sKyHieu]
				,[sMoTa]
				,[iThuTu]
				,[sHienThi]
				,[bHangCha]
				,[iTrangThai]
				,[iNamLamViec])
			SELECT
				[iID_MLNS]
				,[iID_MLNS_Cha]
				,[sM]
				,[sTM]
				,[sKyHieu]
				,[sMoTa]
				,[iThuTu]
				,[sHienThi]
				,[bHangCha]
				,[iTrangThai]
				,@destinationYear
			  FROM [NS_QS_MucLuc]  where iNamLamViec = @sourceYear;
		END;

	if (@isUpdatedBQuanLy = 1)
		Begin
			DELETE FROM DM_BQuanLy where iNamLamViec = @destinationYear;
			INSERT INTO [DM_BQuanLy]
				([iID_MaBQuanLy]
				,[sTenBQuanLy]
				,[sKyHieu]
				,[sMoTa]
				,[iNamLamViec]
				,[iTrangThai]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua])
			 SELECT
				[iID_MaBQuanLy]
				,[sTenBQuanLy]
				,[sKyHieu]
				,[sMoTa]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
			FROM [DM_BQuanLy] where iNamLamViec = @sourceYear;
		End;
	if (@isUpdatedNSDV = 1)
		Begin
			Delete FROM  [DonVi] where iNamLamViec = @destinationYear;
			INSERT INTO [dbo].[DonVi]
				([iID_Parent]
				,[iID_MaDonVi]
				,[sTenDonVi]
				,[sKyHieu]
				,[sMoTa]
				,[iLoai]
				,[iNamLamViec]
				,[iTrangThai]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[LoaiNganSach]
				,[bCoNSNganh]
				,[iKhoi])
			 SELECT [iID_Parent]
				,[iID_MaDonVi]
				,[sTenDonVi]
				,[sKyHieu]
				,[sMoTa]
				,[iLoai]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
				,[LoaiNganSach]
				,[bCoNSNganh]
				,[iKhoi]
			FROM [DonVi] where iNamLamViec = @sourceYear;
			INSERT INTO [NguoiDung_DonVi]
           ([iID_MaNguoiDung]
           ,[iID_MaDonVi]
           ,[iNamLamViec]
           ,[iSTT]
           ,[iTrangThai]
           ,[bPublic]
           ,[dNgayTao]
           ,[iSoLanSua]
           ,[dNgaySua]
           ,[sIPSua]
           ,[sTenDonVi])
			 SELECT [iID_MaNguoiDung]
			  ,[iID_MaDonVi]
			  ,@destinationYear
			  ,[iSTT]
			  ,[iTrangThai]
			  ,[bPublic]
			  ,[dNgayTao]
			  ,[iSoLanSua]
			  ,[dNgaySua]
			  ,[sIPSua]
			  ,[sTenDonVi]
		  FROM [NguoiDung_DonVi] where iNamLamViec = @sourceYear;
		End;

	if (@isUpdatedMLNS = 1)
		Begin
			DELETE FROM [NS_MucLucNganSach] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [NS_MucLucNganSach]
				([iID_MLNS]
				,[iID_MLNS_Cha]
				,[sXauNoiMa]
				,[sLNS]
				,[sL]
				,[sK]
				,[sM]
				,[sTM]
				,[sTTM]
				,[sNG]
				,[sTNG]
				,[sMoTa]
				,[iNamLamViec]
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi]
				,[sMaCB])
			 SELECT [iID_MLNS]
				,[iID_MLNS_Cha]
				,[sXauNoiMa]
				,[sLNS]
				,[sL]
				,[sK]
				,[sM]
				,[sTM]
				,[sTTM]
				,[sNG]
				,[sTNG]
				,[sMoTa]
				,@destinationYear
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi]
				,[sMaCB]
		  FROM [NS_MucLucNganSach] where iNamLamViec = @sourceYear;
		  DELETE FROM [NS_NguoiDung_LNS] WHERE iNamLamViec = @destinationYear;
		  INSERT INTO [NS_NguoiDung_LNS]
			   ([sMaNguoiDung]
			   ,[sLNS]
			   ,[iNamLamViec])
			   (SELECT [sMaNguoiDung]
				  ,[sLNS]
				  ,@destinationYear
				FROM [NS_NguoiDung_LNS] where iNamLamViec = @sourceYear)
		End;

	if (@isUpdateMuclucSkt = 1)
		Begin
			DELETE FROM [NS_SKT_MucLuc] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [dbo].[NS_SKT_MucLuc]
			   ([iID_MLSKT]
			   ,[SKyHieu]
			   ,[SKyHieuCu]
			   ,[sL]
			   ,[sK]
			   ,[sM]
			   ,[sNG_Cha]
			   ,[sNG]
			   ,[sSTT]
			   ,[sSTTBC]
			   ,[sMoTa]
			   ,[KyHieuCha]
			   ,[bHangCha]
			   ,[iTrangThai]
			   ,[iNamLamViec]
			   ,[dNgayTao]
			   ,[dNguoiTao]
			   ,[dNgaySua]
			   ,[dNguoiSua]
			   ,[Tag]
			   ,[Log]
			   ,[Muc]
			   ,[iID_MLSKTCha]
			   ,[sLoaiNhap])
			SELECT [iID_MLSKT]
			   ,[SKyHieu]
			   ,[SKyHieuCu]
			   ,[sL]
			   ,[sK]
			   ,[sM]
			   ,[sNG_Cha]
			   ,[sNG]
			   ,[sSTT]
			   ,[sSTTBC]
			   ,[sMoTa]
			   ,[KyHieuCha]
			   ,[bHangCha]
			   ,[iTrangThai]
			  ,@destinationYear
			  ,GETDATE()
			  ,@userCreator
			  ,null
			  ,null
			  ,[Tag]
			  ,[Log]
			  ,[Muc]
			  ,[iID_MLSKTCha]
			   ,[sLoaiNhap]
		  FROM [dbo].[ns_SKT_MucLuc] where iNamLamViec = @sourceYear;
		End;

	if (@isUpdateDanhMucCapPhat = 1)
		Begin
			DELETE FROM [CP_DanhMuc] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [dbo].[CP_DanhMuc]
			   ([iID_MaDMCapPhat]
			   ,[sTen]
			   ,[sTenThongTriCap]
			   ,[sTenThongTriThu]
			   ,[LNS]
			   ,[sMoTa]
			   ,[OrderIndex]
			   ,[iNamLamViec]
			   ,[iTrangThai]
			   ,[dNgayTao]
			   ,[sNguoiTao]
			   ,[dNgaySua]
			   ,[sNguoiSua]
			   ,[Tag]
			   ,[Log])
			SELECT [iID_MaDMCapPhat]
			   ,[sTen]
			   ,[sTenThongTriCap]
			   ,[sTenThongTriThu]
			   ,[LNS]
			   ,[sMoTa]
				,[OrderIndex]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
			FROM [dbo].[CP_DanhMuc] where iNamLamViec = @sourceYear;
		End;

	if (@isUpdateCauHinhChiTieuLuongMLNS = 1)
		Begin
			DELETE FROM [TL_PhuCap_MLNS] WHERE NAM = @destinationYear;
			INSERT INTO [dbo].[TL_PhuCap_MLNS]
			   ([Ma_PhuCap]
			   ,[Ten_PhuCap]
			   ,[Ma_CachTL]
			   ,[XauNoiMa]
			   ,[LNS]
			   ,[L]
			   ,[K]
			   ,[M]
			   ,[TM]
			   ,[TTM]
			   ,[NG]
			   ,[MoTa]
			   ,[Ma_NguonNganSach]
			   ,[NguonNganSach]
			   ,[DateCreated]
			   ,[UserCreator]
			   ,[DateModified]
			   ,[UserModifier]
			   ,[iTrangThai]
			   ,[idPhuCap]
			   ,[idCachTinhLuong]
			   ,[idNguonNganSach]
			   ,[idMlns]
			   ,[Ma_Cb]
			   ,[ChiTietToi]
			   ,[Nam])
		 SELECT tbl.[Ma_PhuCap]
			   ,tbl.[Ten_PhuCap]
			   ,tbl.[Ma_CachTL]
			   ,tbl.[XauNoiMa]
			   ,tbl.[LNS]
			   ,tbl.[L]
			   ,tbl.[K]
			   ,tbl.[M]
			   ,tbl.[TM]
			   ,tbl.[TTM]
			   ,tbl.[NG]
			   ,tbl.[MoTa]
			   ,tbl.[Ma_NguonNganSach]
			   ,tbl.[NguonNganSach]
			   ,GETDATE()
			   ,@userCreator
			   ,null
			   ,null
			   ,tbl.[iTrangThai]
			   ,tbl.[idPhuCap]
			   ,tbl.[idCachTinhLuong]
			   ,tbl.[idNguonNganSach]
			   ,ml.iID
			   ,tbl.[Ma_Cb]
			   ,tbl.[ChiTietToi]
			   ,@destinationYear 
			   FROM [dbo].[TL_PhuCap_MLNS] as tbl
			   INNER JOIN NS_MucLucNganSach as ml on tbl.XauNoiMa = ml.sXauNoiMa AND ml.iNamLamViec = @destinationYear
			   where nam = @sourceYear;
		End;

	if (@isUpdateDmCapBacKh = 1)
		Begin
			DELETE FROM [TL_DM_CapBac_KeHoach] WHERE [iNamLamViec] = @destinationYear;
			INSERT INTO [dbo].[TL_DM_CapBac_KeHoach]
			   ([Ma_Cb]
			   ,[Ten_Cb]
			   ,[Splits]
			   ,[Parent]
			   ,[Readonly]
			   ,[MoTa]
			   ,[LHT_HS]
			   ,[BHXH_CQ]
			   ,[BHXH_CN]
			   ,[BHYT_CQ]
			   ,[BHYT_CN]
			   ,[BHTN_CQ]
			   ,[BHTN_CN]
			   ,[KPCD_CQ]
			   ,[KPCD_CN]
			   ,[Thoi_Han_Tang]
			   ,[Ma_Cb_KeHoach]
			   ,[Ten_Cb_KeHoach]
			   ,[MoTa_KeHoach]
			   ,[Tuoi_Huu_Nam]
			   ,[Tuoi_Huu_Nu]
			   ,[PCRQ_TT]
			   ,[HsLuongKeHoach]
			   ,[IdHslKeHoach]
			   ,[IdHslHienTai]
			   ,[iNamLamViec])
		SELECT 
			[Ma_Cb]
           ,[Ten_Cb]
           ,[Splits]
           ,[Parent]
           ,[Readonly]
           ,[MoTa]
           ,[LHT_HS]
           ,[BHXH_CQ]
           ,[BHXH_CN]
           ,[BHYT_CQ]
           ,[BHYT_CN]
           ,[BHTN_CQ]
           ,[BHTN_CN]
           ,[KPCD_CQ]
           ,[KPCD_CN]
           ,[Thoi_Han_Tang]
           ,[Ma_Cb_KeHoach]
           ,[Ten_Cb_KeHoach]
           ,[MoTa_KeHoach]
           ,[Tuoi_Huu_Nam]
           ,[Tuoi_Huu_Nu]
           ,[PCRQ_TT]
           ,[HsLuongKeHoach]
           ,[IdHslKeHoach]
           ,[IdHslHienTai]
           ,@destinationYear FROM [TL_DM_CapBac_KeHoach] WHERE [iNamLamViec] = @sourceYear
		End;

	if (@isUpdateNSSKT = 1)
		begin
			DELETE FROM NS_MLSKT_MLNS WHERE [iNamLamViec] = @destinationYear;
			INSERT INTO [dbo].[NS_MLSKT_MLNS]
			   ([sSKT_KyHieu]
			   ,[sNS_XauNoiMa]
			   ,[iNamLamViec]
			   ,[dNgayTao]
			   ,[sNguoiTao]
			   ,[dNgaySua]
			   ,[sNguoiSua]
			   ,[Tag]
			   ,[Log]
			   ,[iTrangThai])
			   SELECT [sSKT_KyHieu]
				   ,[sNS_XauNoiMa]
				   ,@destinationYear
				   ,GETDATE()
				   ,@userCreator
				   ,null
				   ,null
				   ,[Tag]
				   ,[Log]
				   ,[iTrangThai] FROM [NS_MLSKT_MLNS] WHERE [iNamLamViec] = @sourceYear;
		end

	if (@isUpdateCauHinhHeThong = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM DanhMuc WHERE [iNamLamViec] = @destinationYear and sType = 'DM_CauHinh';
				INSERT INTO [dbo].[DanhMuc]
				   ([sType]
				   ,[iID_MaDanhMuc]
				   ,[sTen]
				   ,[sGiaTri]
				   ,[sMoTa]
				   ,[iThuTu]
				   ,[iNamLamViec]
				   ,[iTrangThai]
				   ,[dNgayTao]
				   ,[sNguoiTao]
				   ,[dNgaySua]
				   ,[sNguoiSua]
				   ,[Tag]
				   ,[Log]
				   ,[NganSachNganh])
				   SELECT [sType]
					   ,[iID_MaDanhMuc]
					   ,[sTen]
					   ,[sGiaTri]
					   ,[sMoTa]
					   ,[iThuTu]
					   ,@destinationYear
					   ,[iTrangThai]
					   ,[dNgayTao]
					   ,[sNguoiTao]
					   ,[dNgaySua]
					   ,[sNguoiSua]
					   ,[Tag]
					   ,[Log]
					   ,[NganSachNganh] FROM DanhMuc WHERE [iNamLamViec] = @sourceYear and sType = 'DM_CauHinh';
		end
	if (@isUpdateDanhMucDonViTinh = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM DanhMuc WHERE [iNamLamViec] = @destinationYear and sType = 'DM_DonViTinh';
				INSERT INTO [dbo].[DanhMuc]
				   ([sType]
				   ,[iID_MaDanhMuc]
				   ,[sTen]
				   ,[sGiaTri]
				   ,[sMoTa]
				   ,[iThuTu]
				   ,[iNamLamViec]
				   ,[iTrangThai]
				   ,[dNgayTao]
				   ,[sNguoiTao]
				   ,[dNgaySua]
				   ,[sNguoiSua]
				   ,[Tag]
				   ,[Log]
				   ,[NganSachNganh])
				   SELECT [sType]
					   ,[iID_MaDanhMuc]
					   ,[sTen]
					   ,[sGiaTri]
					   ,[sMoTa]
					   ,[iThuTu]
					   ,@destinationYear
					   ,[iTrangThai]
					   ,[dNgayTao]
					   ,[sNguoiTao]
					   ,[dNgaySua]
					   ,[sNguoiSua]
					   ,[Tag]
					   ,[Log]
					   ,[NganSachNganh] FROM DanhMuc WHERE [iNamLamViec] = @sourceYear and sType = 'DM_DonViTinh';
		end		
		
		if (@isUpdateDanhMucCanCu = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM [dbo].[NS_CauHinh_CanCu] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[NS_CauHinh_CanCu]
				   ([iID_CauHinh_CanCu]
					,[bChinhSua]
					,[iID_MaChucNang]
					,[iNamCanCu]
					,[iNamLamViec]
					,[iThietLap]
					,[sModule]
					,[sTenCot])
				   SELECT NEWID()
					,[bChinhSua]
					,[iID_MaChucNang]
					,[iNamCanCu]
					,@destinationYear
					,[iThietLap]
					,[sModule]
					,[sTenCot] FROM [dbo].[NS_CauHinh_CanCu] WHERE [iNamLamViec] = @sourceYear;
		end	

		if (@isUpdateDanhMucCKTC = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM [dbo].[NS_DanhMucCongKhai] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[NS_DanhMucCongKhai]
					  ([Id]
					  ,[dNgayTao]
					  ,[iNamLamViec]
					  ,[sMoTa]
					  ,[sNguoiSua]
					  ,[sNguoiTao]
					  ,[STT]
					  ,[bHangCha]
					  ,[iID_DMCongKhai_Cha]
					  ,[sMa]
					  ,[sMaCha])
				   SELECT NEWID()
					  ,GETDATE()
					  ,@destinationYear
					  ,[sMoTa]
					  ,[sNguoiSua]
					  ,[sNguoiTao]
					  ,[STT]
					  ,[bHangCha]
					  ,[iID_DMCongKhai_Cha]
					  ,[sMa]
					  ,[sMaCha] FROM [dbo].[NS_DanhMucCongKhai] WHERE [iNamLamViec] = @sourceYear;

				update con
				set con.iID_DMCongKhai_Cha = cha.Id 
				from NS_DanhMucCongKhai con
				join NS_DanhMucCongKhai cha on con.sMaCha = cha.sMa 
				and con.iNamLamViec = cha.iNamLamViec
				where con.iNamLamViec = @destinationYear

				DELETE FROM [dbo].[NS_DMCongKhai_MLNS] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[NS_DMCongKhai_MLNS]
					  ([Id]
					  ,[dNgaySua]
					  ,[dNgayTao]
					  ,[iID_DMCongKhai]
					  ,[iNamLamViec]
					  ,[sNS_XauNoiMa]
					  ,[sNguoiSua]
					  ,[sNguoiTao])
				   SELECT NEWID()
					,GETDATE()
					,GETDATE()
					,[iID_DMCongKhai_NEW]
					,@destinationYear
					,[sNS_XauNoiMa]
					,[sNguoiSua]
					,[sNguoiTao] 
				   FROM (
						select map.*, b.Id [iID_DMCongKhai_NEW] from NS_DMCongKhai_MLNS map
						join NS_DanhMucCongKhai a on map.iID_DMCongKhai = a.Id 
						and map.iNamLamViec = a.iNamLamViec
						join (select * from NS_DanhMucCongKhai where iNamLamViec = @destinationYear) b
						on a.sMa = b.sMa
						where map.iNamLamViec = @sourceYear
					) tab
				WHERE tab.[iNamLamViec] = @sourceYear;
		end	
		
/*			if (@isUpdateDanhMucChuDauTu = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM [dbo].[DM_ChuDauTu] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[DM_ChuDauTu]
				   ([iID_DonVi]
					,[bHangCha]
					,[ChiNhanhNuocNgoai]
					,[dNgaySua]
					,[dNgayTao]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,[iNamLamViec]
					,[iTrangThai]
					,[Loai]
					,[MaSoDVSDNS]
					,[sKyHieu]
					,[sMoTa]
					,[sNguoiSua]
					,[sNguoiTao]
					,[STKNuocNgoai]
					,[STKTrongNuoc]
					,[sTenDonVi])
				   SELECT NEWID()
					,[bHangCha]
					,[ChiNhanhNuocNgoai]
					,[dNgaySua]
					,[dNgayTao]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,@destinationYear
					,[iTrangThai]
					,[Loai]
					,[MaSoDVSDNS]
					,[sKyHieu]
					,[sMoTa]
					,[sNguoiSua]
					,[sNguoiTao]
					,[STKNuocNgoai]
					,[STKTrongNuoc]
					,[sTenDonVi] FROM [dbo].[DM_ChuDauTu] WHERE [iNamLamViec] = @sourceYear;
		end	

			if (@isUpdateDanhMucDonviQuanLyDuAn = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG   VdtDmDonViThucHienDuAn
				DELETE FROM [dbo].[VDT_DM_DonViThucHienDuAn];
				INSERT INTO [dbo].[VDT_DM_DonViThucHienDuAn]
				   ([iID_DonVi]
					,[BHangCha]
					,[iCapDonVi]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,[sDiaChi]
					,[sKyHieu]
					,[sTenDonVi])
				   SELECT NEWID()
					,[BHangCha]
					,[iCapDonVi]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,[sDiaChi]
					,[sKyHieu]
					,[sTenDonVi] FROM [dbo].[VDT_DM_DonViThucHienDuAn];
		end	

			if (@isUpdateDanhMucNhaThau = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG  VdtDmNhaThau
				DELETE FROM [dbo].[VDT_DM_NhaThau] ;
				INSERT INTO [dbo].[VDT_DM_NhaThau]
				   ([Id]
					,[sChucVu]
					,[sDaiDien]
					,[sDiaChi]
					,[sDienThoai]
					,[sDienThoaiLienHe]
					,[sEmail]
					,[sFax]
					,[sMaNganHang]
					,[sMaNhaThau]
					,[sMaSoThue]
					,[sNganHang]
					,[sNguoiLienHe]
					,[sSoTaiKhoan]
					,[sTenNhaThau]
					,[sWebsite]
					,[sSoTaiKhoan2]
					,[sSoTaiKhoan3])
				   SELECT NEWID()
					,[sChucVu]
					,[sDaiDien]
					,[sDiaChi]
					,[sDienThoai]
					,[sDienThoaiLienHe]
					,[sEmail]
					,[sFax]
					,[sMaNganHang]
					,[sMaNhaThau]
					,[sMaSoThue]
					,[sNganHang]
					,[sNguoiLienHe]
					,[sSoTaiKhoan]
					,[sTenNhaThau]
					,[sWebsite]
					,[sSoTaiKhoan2]
					,[sSoTaiKhoan3] FROM [dbo].[VDT_DM_NhaThau];
		end	
		*/

		if (@isUpdateDanhMucBHXH = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_MucLucNganSach WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_MucLucNganSach (
					iID,
					sXauNoiMa,
					sLNS,
					sL,
					sK,
					sM,
					sTM,
					sTTM,
					sNG,
					sTNG,
					sMoTa,
					bHangCha,
					iTrangThai,
					bDuPhong,
					bHangChaDuToan,
					bHangChaQuyetToan,
					bHangMua,
					bHangNhap,
					bHienVat,
					bNgay,
					bPhanCap,
					bSoNguoi,
					bTonKho,
					bTuChi,
					sChiTietToi,
					dNgaySua,
					dNgayTao,
					iLoai,
					iLock,
					iID_MaDonVi,
					iID_MaBQuanLy,
					[Log],
					iID_MLNS,
					iID_MLNS_Cha,
					iNamLamViec,
					sCPChiTietToi,
					sDuToanChiTietToi,
					sNguoiSua,
					sNguoiTao,
					sNhapTheoTruong,
					sQuyetToanChiTietToi,
					Tag,
					sTNG1,
					sTNG2,
					sTNG3,
					iLoaiNganSach,
					sMaCB,
					sMaPhuCap,
					bHangChaDuToanDieuChinh,
					sDuToanDieuChinhChiTietToi,
					iDonViTinh,
					fTyLe_BHXH_NSD,
					fTyLe_BHXH_NLD,
					fTyLe_BHYT_NSD,
					fTyLe_BHYT_NLD,
					fTyLe_BHTN_NSD,
					fTyLe_BHTN_NLD)
				select newid(),
					sXauNoiMa,
					sLNS,
					sL,
					sK,
					sM,
					sTM,
					sTTM,
					sNG,
					sTNG,
					sMoTa,
					bHangCha,
					iTrangThai,
					bDuPhong,
					bHangChaDuToan,
					bHangChaQuyetToan,
					bHangMua,
					bHangNhap,
					bHienVat,
					bNgay,
					bPhanCap,
					bSoNguoi,
					bTonKho,
					bTuChi,
					sChiTietToi,
					dNgaySua,
					getdate(),
					iLoai,
					iLock,
					iID_MaDonVi,
					iID_MaBQuanLy,
					[Log],
					iID_MLNS,
					iID_MLNS_Cha,
					@destinationYear,
					sCPChiTietToi,
					sDuToanChiTietToi,
					sNguoiSua,
					@userCreator,
					sNhapTheoTruong,
					sQuyetToanChiTietToi,
					Tag,
					sTNG1,
					sTNG2,
					sTNG3,
					iLoaiNganSach,
					sMaCB,
					sMaPhuCap,
					bHangChaDuToanDieuChinh,
					sDuToanDieuChinhChiTietToi,
					iDonViTinh,
					fTyLe_BHXH_NSD,
					fTyLe_BHXH_NLD,
					fTyLe_BHYT_NSD,
					fTyLe_BHYT_NLD,
					fTyLe_BHTN_NSD,
					fTyLe_BHTN_NLD
				from BH_DM_MucLucNganSach
				where iNamLamViec = @sourceYear;
		end	

		if (@isUpdateMucLucCacLoaiChi = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_LoaiChi WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_LoaiChi (
					iID,
					sMaLoaiChi,
					sTenDanhMucLoaiChi,
					iNamLamViec,
					dNgaySua,
					dNgayTao,
					sNguoiSua,
					sNguoiTao,
					sMoTa,
					iTrangThai,
					sLNS,
					sDSXauNoiMa)
				select NEWID(),
					sMaLoaiChi,
					sTenDanhMucLoaiChi,
					@destinationYear,
					dNgaySua,
					getdate(),
					sNguoiSua,
					@userCreator,
					sMoTa,
					iTrangThai,
					sLNS,
					sDSXauNoiMa
				from BH_DM_LoaiChi
				where iNamLamViec = @sourceYear
		end	

		if (@isUpdateDanhMucCoSoYTe = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM DM_CoSoYTe WHERE iNamLamViec = @destinationYear;
				INSERT INTO DM_CoSoYTe (
					iID_CoSoYTe,
					iID_MaCoSoYTe,
					iNamLamViec,
					sTenCoSoYTe,
					dNgaySua,
					dNgayTao,
					iTrangThai,
					sNguoiSua,
					sNguoiTao)
				select NEWID(),
					iID_MaCoSoYTe,
					@destinationYear,
					sTenCoSoYTe,
					dNgaySua,
					GETDATE(),
					iTrangThai,
					sNguoiSua,
					@userCreator
				from DM_CoSoYTe
				where iNamLamViec = @sourceYear
		end	

		if (@isUpdateDanhMucTDQT = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_ThamDinhQuyetToan (
					iID,
					iKieuChu,
					iMa,
					iMaCha,
					iNamLamViec,
					iTrangThai,
					sNguoiSua,
					sNguoiTao,
					sNoiDung,
					sSTT,
					iSTT,
					sXauNoiMa,
					ILock)
				select NEWID(),
					iKieuChu,
					iMa,
					iMaCha,
					@destinationYear,
					iTrangThai,
					sNguoiSua,
					@userCreator,
					sNoiDung,
					sSTT,
					iSTT,
					sXauNoiMa,
					ILock
				from BH_DM_ThamDinhQuyetToan
				where iNamLamViec = @sourceYear
		end	
		
		if (@isUpdateDanhMucCHTSBHXH = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_CauHinhThamSo WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_CauHinhThamSo (
					iID,
					bTrangThai,
					dNgaySua,
					dNgayTao,
					iNamLamViec,
					sMa,
					sMoTa,
					sNguoiSua,
					sNguoiTao,
					sTen,
					fGiaTri)
				select NEWID(),
					bTrangThai,
					dNgaySua,
					GETDATE(),
					@destinationYear,
					sMa,
					sMoTa,
					sNguoiSua,
					@userCreator,
					sTen,
					fGiaTri
				from BH_DM_CauHinhThamSo
				where iNamLamViec = @sourceYear
		end	
		if (@isUpdateDanhMucNgayNghi = 1)
		begin
			-- COPY DANH MUC NGAY NGHI
				
				DELETE FROM Tl_DM_NgayNghi WHERE iNamLamViec = @destinationYear;
				INSERT INTO Tl_DM_NgayNghi (
					Id,
					dTuNgay,
					dDenNgay,
					sMaNgayNghi,
					sTenNgayNghi,
					iNamLamViec
					)
				select 
					NEWID(),
					DATEADD(YEAR, @destinationYear-@sourceYear, dTuNgay),
					DATEADD(YEAR, @destinationYear-@sourceYear, dDenNgay),
					sMaNgayNghi,
					sTenNgayNghi,
					@destinationYear
				from Tl_DM_NgayNghi
				where iNamLamViec = @sourceYear
		end	

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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_danh_muc_tdqt]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_clone_year_danh_muc_tdqt]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
AS
BEGIN
	
	insert into BH_DM_ThamDinhQuyetToan (
		iID,
		iKieuChu,
		iMa,
		iMaCha,
		iNamLamViec,
		iTrangThai,
		sNguoiSua,
		sNguoiTao,
		sNoiDung,
		sSTT,
		iSTT,
		sXauNoiMa,
		ILock)
	select NEWID(),
		iKieuChu,
		iMa,
		iMaCha,
		@dest,
		iTrangThai,
		sNguoiSua,
		@userCreate,
		sNoiDung,
		sSTT,
		iSTT,
		sXauNoiMa,
		ILock
	from BH_DM_ThamDinhQuyetToan
	where iNamLamViec = @source and iMa not in (select iMa from BH_DM_ThamDinhQuyetToan where iNamLamViec = @dest)

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
@INamLamViec int,
@IdDonVi nvarchar(max),
@DonViTinh int
AS
BEGIN
	select 
		dv.sTenDonVi as sTenDonVi,
		ct.sMoTa,
		dv.iKhoi,
		sum(ct.fOmDau)/ @DonViTinh fOmDau,
		sum(ct.fThaiSan)/ @DonViTinh fThaiSan,
		sum(ct.fTNLDBNN)/ @DonViTinh fTNLDBNN,
		sum(ct.fHuuTri)/ @DonViTinh fHuuTri,
		sum(ct.fPhucVien)/ @DonViTinh fPhucVien,
		sum(ct.fXuatNgu)/ @DonViTinh fXuatNgu,
		sum(ct.fThoiViec)/ @DonViTinh fThoiViec,
		sum(ct.fTuTuat)/ @DonViTinh fTuTuat,
		iKieuChu = 3
	into #data
	from
	(select 
	iID_MaDonVi,
	case when iMa > 182 and iMa < 191 then N'Khối dự toán'
	when iMa > 192 and iMa < 201 then N'Khối hạch toán'
	else N'Khối khác' end as sMoTa,
	case when ima = 183 or ima = 193 then fSoThamDinh/ @DonViTinh
			else 0 end as fOmDau,
	case when ima = 184 or ima = 194 then fSoThamDinh/ @DonViTinh
			else 0 end as fThaiSan,
	case when ima = 185 or ima = 195 then fSoThamDinh/ @DonViTinh
			else 0 end as fTNLDBNN,
	case when ima = 186 or ima = 196 then fSoThamDinh/ @DonViTinh
			else 0 end as fHuuTri,
	case when ima = 187 or ima = 197 then fSoThamDinh/ @DonViTinh
			else 0 end as fPhucVien,
	case when ima = 188 or ima = 198 then fSoThamDinh/ @DonViTinh
			else 0 end as fXuatNgu,
	case when ima = 189 or ima = 199 then fSoThamDinh/ @DonViTinh
			else 0 end as fThoiViec,
	case when ima = 190 or ima = 200 then fSoThamDinh/ @DonViTinh
			else 0 end as fTuTuat
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where 
	iID_MaDonVi in (select * from f_split(@IdDonVi)) and 
	iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fOmDau > 0 or fThaiSan > 0 or fTNLDBNN > 0 or fHuuTri > 0 or fPhucVien > 0 or fXuatNgu > 0 or fThoiViec > 0 or fTuTuat > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi, ct.sMoTa, dv.iKhoi

	select 
	sSTT = '',
	sTenDonVi = N'A. Đơn vị dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA
	from (select * from #data where iKhoi = 2) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA1
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA2
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối hạch toán') ct

	select 
	sSTT = '',
	sTenDonVi = N'B. Đơn vị hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB
	from (select * from #data where iKhoi = 1) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB1
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB2
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối hạch toán') ct

				select 
	sSTT = '',
	sTenDonVi,
	iKhoi,
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 3
	into #rowDV
	from #data ct
	group by ct.sTenDonVi, ct.sTenDonVi, ct.iKhoi

	select * 
	into #rowDV1
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu 
		from #rowDV where iKhoi = 2) dv1
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 2) 
	order by sTenDonVi, sMoTa

	select *
	into #rowDV2
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
		from #rowDV where iKhoi = 1) dv2
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 1) 
	order by sTenDonVi, sMoTa

	select * from #rowA
	union all select * from #rowA1
	union all select * from #rowA2
	union all (select 
		sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV1)
	union all select * from #rowB
	union all select * from #rowB1
	union all select * from #rowB2
	union all (select 
			sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV2)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
	@DsMaDonVi nvarchar(1000),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI]') AND type in (N'U')) drop table TBL_HUUTRI;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_DOC]') AND type in (N'U')) drop table TBL_HUUTRI_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_DOC]') AND type in (N'U')) drop table TBL_PHUCVIEN_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_DOC]') AND type in (N'U')) drop table TBL_THOIVIEC_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_DOC]') AND type in (N'U')) drop table TBL_TUTUAT_DOC;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_SQ]') AND type in (N'U')) drop table TBL_HUUTRI_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_QNCN]') AND type in (N'U')) drop table TBL_HUUTRI_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_HSQBS]') AND type in (N'U')) drop table TBL_HUUTRI_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_VCQP]') AND type in (N'U')) drop table TBL_HUUTRI_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_LDHD]') AND type in (N'U')) drop table TBL_HUUTRI_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_SQ]') AND type in (N'U')) drop table TBL_PHUCVIEN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_QNCN]') AND type in (N'U')) drop table TBL_PHUCVIEN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_HSQBS]') AND type in (N'U')) drop table TBL_PHUCVIEN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_VCQP]') AND type in (N'U')) drop table TBL_PHUCVIEN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_LDHD]') AND type in (N'U')) drop table TBL_PHUCVIEN_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_SQ]') AND type in (N'U')) drop table TBL_THOIVIEC_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_QNCN]') AND type in (N'U')) drop table TBL_THOIVIEC_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_HSQBS]') AND type in (N'U')) drop table TBL_THOIVIEC_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_VCQP]') AND type in (N'U')) drop table TBL_THOIVIEC_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_LDHD]') AND type in (N'U')) drop table TBL_THOIVIEC_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_SQ]') AND type in (N'U')) drop table TBL_TUTUAT_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_QNCN]') AND type in (N'U')) drop table TBL_TUTUAT_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_HSQBS]') AND type in (N'U')) drop table TBL_TUTUAT_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_VCQP]') AND type in (N'U')) drop table TBL_TUTUAT_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_LDHD]') AND type in (N'U')) drop table TBL_TUTUAT_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_TRUYLINH]') AND type in (N'U')) drop table TBL_TUTUAT_TRUYLINH;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_RESULT]') AND type in (N'U')) drop table TBL_HUUTRI_RESULT;

	--Lay thong tin luong theo TC_HUUTRI, TC_PHUCVIEN, TC_THOIVIEC, TC_TUTUAT
	select * into TBL_HUUTRI from
	(select donvi.Ma_DonVi ,donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('HUUTRI_TROCAP1LAN', 'HUUTRI_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN', 'PHUCVIEN_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN', 'THOIVIEC_TROCAPKHUVUC', 'TUTUAT_TROCAP1LAN', 'TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG','TUTUAT_TROCAP1LAN_TRUYLINH','TUTUAT_TROCAPKHUVUC_TRUYLINH','TROCAPMAITANG_TRUYLINH')) HUUTRI


	-- Data tro cap Huu tri
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		HUUTRI_TROCAP1LAN.nGiaTri fHUUTRI_TROCAP1LAN,
		HUUTRI_TROCAPKHUVUC.nGiaTri fHUUTRI_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_HUUTRI_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('HUUTRI_TROCAPKHUVUC', 'HUUTRI_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'HUUTRI_TROCAPKHUVUC') HUUTRI_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'HUUTRI_TROCAP1LAN') HUUTRI_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAP1LAN.sMaDonVi

	-- Data tro cap Phuc vien
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		PHUCVIEN_TROCAP1LAN.nGiaTri fPHUCVIEN_TROCAP1LAN,
		PHUCVIEN_TROCAPKHUVUC.nGiaTri fPHUCVIEN_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_PHUCVIEN_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('PHUCVIEN_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'PHUCVIEN_TROCAPKHUVUC') PHUCVIEN_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'PHUCVIEN_TROCAP1LAN') PHUCVIEN_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAP1LAN.sMaDonVi

	-- Data tro cap Thoi Viec
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		THOIVIEC_TROCAP1LAN.nGiaTri fTHOIVIEC_TROCAP1LAN,
		THOIVIEC_TROCAPKHUVUC.nGiaTri fTHOIVIEC_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_THOIVIEC_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('THOIVIEC_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'THOIVIEC_TROCAPKHUVUC') THOIVIEC_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'THOIVIEC_TROCAP1LAN') THOIVIEC_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAP1LAN.sMaDonVi

	-- Data tro cap Tu tuat
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		TUTUAT_TROCAP1LAN.nGiaTri fTUTUAT_TROCAP1LAN,
		TUTUAT_TROCAPKHUVUC.nGiaTri fTUTUAT_TROCAPKHUVUC,
		TROCAPMAITANG.nGiaTri fTROCAPMAITANG,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_TUTUAT_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG', 'TUTUAT_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TUTUAT_TROCAPKHUVUC') TUTUAT_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI1.Id, HUUTRI1.sMaDonVi, HUUTRI1.nGiaTri, HUUTRI1.sMaCB, HUUTRI1.sMaCBo, HUUTRI1.sMaCheDo, HUUTRI1.sTenCbo
		from TBL_HUUTRI HUUTRI1
		where HUUTRI1.sMaCheDo = 'TROCAPMAITANG') TROCAPMAITANG
		on TBL_HUUTRI.sMaCBo = TROCAPMAITANG.sMaCBo and TBL_HUUTRI.sMaDonVi = TROCAPMAITANG.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'TUTUAT_TROCAP1LAN') TUTUAT_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAP1LAN.sMaDonVi


   -- lAY TRUY LINH TU TUAT 1 LAN , KHU VUC
   	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		TUTUAT_TROCAP1LAN_TRUYLINH.nGiaTri fTUTUAT_TROCAP1LAN_TRUYLINH,
		TUTUAT_TROCAPKHUVUC_TRUYLINH.nGiaTri fTUTUAT_TROCAPKHUVUC_TRUYLINH,
		TROCAPMAITANG_TRUYLINH.nGiaTri fTROCAPMAITANG_TRUYLINH,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_TUTUAT_TRUYLINH
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('TUTUAT_TROCAPKHUVUC_TRUYLINH', 'TROCAPMAITANG_TRUYLINH', 'TUTUAT_TROCAP1LAN_TRUYLINH')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TUTUAT_TROCAPKHUVUC_TRUYLINH') TUTUAT_TROCAPKHUVUC_TRUYLINH
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAPKHUVUC_TRUYLINH.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAPKHUVUC_TRUYLINH.sMaDonVi
		left join
		(select HUUTRI1.Id, HUUTRI1.sMaDonVi, HUUTRI1.nGiaTri, HUUTRI1.sMaCB, HUUTRI1.sMaCBo, HUUTRI1.sMaCheDo, HUUTRI1.sTenCbo
		from TBL_HUUTRI HUUTRI1
		where HUUTRI1.sMaCheDo = 'TROCAPMAITANG_TRUYLINH') TROCAPMAITANG_TRUYLINH
		on TBL_HUUTRI.sMaCBo = TROCAPMAITANG_TRUYLINH.sMaCBo and TBL_HUUTRI.sMaDonVi = TROCAPMAITANG_TRUYLINH.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'TUTUAT_TROCAP1LAN_TRUYLINH') TUTUAT_TROCAP1LAN_TRUYLINH
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAP1LAN_TRUYLINH.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAP1LAN_TRUYLINH.sMaDonVi


	--Lay tro cap Huu tri Si quan
	select * into TBL_HUUTRI_SQ from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi,null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB like '1%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_SQ) > 2
		update TBL_HUUTRI_SQ set bHasData = 1

	--Lay tro cap Huu tri QNCN
	select * into TBL_HUUTRI_QNCN from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB like '2%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_QNCN) > 2
		update TBL_HUUTRI_QNCN set bHasData = 1

	--Lay tro cap Huu tri HSQ_BS
	select * into TBL_HUUTRI_HSQBS from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB like '0%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_HSQBS) > 2
		update TBL_HUUTRI_HSQBS set bHasData = 1

	--Lay tro cap Huu tri VCQP
	select * into TBL_HUUTRI_VCQP from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_VCQP) > 2
		update TBL_HUUTRI_VCQP set bHasData = 1

	--Lay tro cap Huu tri HDLD
	select * into TBL_HUUTRI_LDHD from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB in ('43','423','425') and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_LDHD) > 2
		update TBL_HUUTRI_LDHD set bHasData = 1
	-----------------------------------
	--Lay tro cap Phuc vien Si quan
	select * into TBL_PHUCVIEN_SQ from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB like '1%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_SQ) > 2
		update TBL_PHUCVIEN_SQ set bHasData = 1

	--Lay tro cap Phuc vien QNCN
	select * into TBL_PHUCVIEN_QNCN from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB like '2%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_QNCN) > 2
		update TBL_PHUCVIEN_QNCN set bHasData = 1

	--Lay tro cap Phuc vien HSQ_BS
	select * into TBL_PHUCVIEN_HSQBS from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB like '0%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_HSQBS) > 2
		update TBL_PHUCVIEN_HSQBS set bHasData = 1

	--Lay tro cap Phuc vien VCQP
	select * into TBL_PHUCVIEN_VCQP from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_VCQP) > 2
		update TBL_PHUCVIEN_VCQP set bHasData = 1

	--Lay tro cap Phuc vien HDLD
	select * into TBL_PHUCVIEN_LDHD from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('43','423','425') and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_LDHD) > 2
		update TBL_PHUCVIEN_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Thoi viec Si quan
	select * into TBL_THOIVIEC_SQ from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB like '1%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_SQ) > 2
		update TBL_THOIVIEC_SQ set bHasData = 1

	--Lay tro cap Thoi viec QNCN
	select * into TBL_THOIVIEC_QNCN from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB like '2%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_QNCN) > 2
		update TBL_THOIVIEC_QNCN set bHasData = 1

	--Lay tro cap Thoi viec HSQ_BS
	select * into TBL_THOIVIEC_HSQBS from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB like '0%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_HSQBS) > 2
		update TBL_THOIVIEC_HSQBS set bHasData = 1

	--Lay tro cap Thoi viec VCQP
	select * into TBL_THOIVIEC_VCQP from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_VCQP) > 2
		update TBL_THOIVIEC_VCQP set bHasData = 1

	--Lay tro cap Thoi viec HDLD
	select * into TBL_THOIVIEC_LDHD from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB in ('43','423','425') and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_LDHD) > 2
		update TBL_THOIVIEC_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Tu tuat Si quan
	select * into TBL_TUTUAT_SQ from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 1 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'SQ' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH
	from TBL_TUTUAT_DOC  TBL
	lefT join TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB like '1%' and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0)
	where TBL.sMaCB like '1%' ) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_SQ) > 2
		update TBL_TUTUAT_SQ set bHasData = 1

	--Lay tro cap Tu tuat QNCN
	select * into TBL_TUTUAT_QNCN from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 2 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'QNCN' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH
	

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB like '2%' and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB like '2%' ) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_QNCN) > 2
		update TBL_TUTUAT_QNCN set bHasData = 1

	--Lay tro cap Tu tuat HSQ_BS
	select * into TBL_TUTUAT_HSQBS from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 3 RowNum, 
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'HSQ_BS' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB like '0%' and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB like '0%') tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_HSQBS) > 2
		update TBL_TUTUAT_HSQBS set bHasData = 1

	--Lay tro cap Tu tuat VCQP
	select * into TBL_TUTUAT_VCQP from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 4 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'VCQP' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB  in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB  in ('3.1', '3.2', '3.3', '413', '415') ) tvvcqp
	
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_VCQP) > 2
		update TBL_TUTUAT_VCQP set bHasData = 1

	--Lay tro cap Tu tuat HDLD
	select * into TBL_TUTUAT_LDHD from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 5 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'LDHD' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB in ('43','423','425')  and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB in ('43','423','425') ) tvldhd
	

	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_LDHD) > 2
		update TBL_TUTUAT_LDHD set bHasData = 1
	----------------------------------------------
	--Ket qua
	select result.* into TBL_HUUTRI_RESULT from
	(select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_HUUTRI_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_HUUTRI_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, 
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_HUUTRI_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_HUUTRI_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_HUUTRI_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, 
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_PHUCVIEN_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_THOIVIEC_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_THOIVIEC_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_THOIVIEC_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_THOIVIEC_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
		from TBL_THOIVIEC_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
		from TBL_TUTUAT_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
		from TBL_TUTUAT_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, 
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_TUTUAT_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_TUTUAT_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_TUTUAT_LDHD) result

	select distinct
		LoaiTC SLoaiTC,
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
		fTROCAP1LAN/@DonViTinh FTroCap1Lan,
		fTROCAPKHUVUC/@DonViTinh FTroCapKhuVuc,
		fTROCAPMAITANG/@DonViTinh FTroCapMaiTang,
		fTongSoTienTT/@DonViTinh FTongSoTienThangNay,
		fTROCAP1LANTRUYLINH/@DonViTinh FTroCap1LanTruyLinh,
		fTROCAPKHUVUCTRUYLINH/@DonViTinh FTroCapKhuVucTruyLinh,
		fTROCAPMAITANGTRUYLINH/@DonViTinh FTroCapMaiTangTruyLinh,
		fTongSoTienTL/@DonViTinh FTongSoTienTruyLinh,
		fTongSoTien/@DonViTinh FTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_HUUTRI_RESULT
	order by LoaiTC, RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI]') AND type in (N'U')) drop table TBL_HUUTRI;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_DOC]') AND type in (N'U')) drop table TBL_HUUTRI_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_DOC]') AND type in (N'U')) drop table TBL_PHUCVIEN_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_DOC]') AND type in (N'U')) drop table TBL_THOIVIEC_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_DOC]') AND type in (N'U')) drop table TBL_TUTUAT_DOC;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_SQ]') AND type in (N'U')) drop table TBL_HUUTRI_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_QNCN]') AND type in (N'U')) drop table TBL_HUUTRI_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_HSQBS]') AND type in (N'U')) drop table TBL_HUUTRI_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_VCQP]') AND type in (N'U')) drop table TBL_HUUTRI_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_LDHD]') AND type in (N'U')) drop table TBL_HUUTRI_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_SQ]') AND type in (N'U')) drop table TBL_PHUCVIEN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_QNCN]') AND type in (N'U')) drop table TBL_PHUCVIEN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_HSQBS]') AND type in (N'U')) drop table TBL_PHUCVIEN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_VCQP]') AND type in (N'U')) drop table TBL_PHUCVIEN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_LDHD]') AND type in (N'U')) drop table TBL_PHUCVIEN_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_SQ]') AND type in (N'U')) drop table TBL_THOIVIEC_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_QNCN]') AND type in (N'U')) drop table TBL_THOIVIEC_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_HSQBS]') AND type in (N'U')) drop table TBL_THOIVIEC_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_VCQP]') AND type in (N'U')) drop table TBL_THOIVIEC_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_LDHD]') AND type in (N'U')) drop table TBL_THOIVIEC_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_SQ]') AND type in (N'U')) drop table TBL_TUTUAT_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_QNCN]') AND type in (N'U')) drop table TBL_TUTUAT_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_HSQBS]') AND type in (N'U')) drop table TBL_TUTUAT_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_VCQP]') AND type in (N'U')) drop table TBL_TUTUAT_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_LDHD]') AND type in (N'U')) drop table TBL_TUTUAT_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_TRUYLINH]') AND type in (N'U')) drop table TBL_TUTUAT_TRUYLINH;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_RESULT]') AND type in (N'U')) drop table TBL_HUUTRI_RESULT;

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
SELECT gt.*,dv.sTenDonVi INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
left join DonVi dv on dv.iID_MaDonVi=gt.iiD_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1
	--- 9010001-010-011-0004 Tro cap Huu tri
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailHuuTriSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #TempDetailHuuTri
			from  #TempDetailHuuTriSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh


	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
			, N'TC Hưu Trí'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailHuuTri	
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

		UPDATE #tempHuuTri
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  bHangCha=0 ) detail
		where #tempHuuTri.bHangCha=1
	--- 9010001-010-011-0005 Tro cap phuc vien

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailPhucVienSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #tempDetailPhucVien
			from  #tempDetailPhucVienSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC

	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	) tblPhucVien

	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1

		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  bHangCha=0 ) detail
		where #tempPhucVien.bHangCha=1

	--- 9010001-010-011-0006 Tro cap xuat ngu

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #tempDetailXuatNgu
			from  #tempDetailXuatNguSum tbltc
			GROUP BY 
					 tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenPhanHo 
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh

	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
			, N'TC Xuất ngũ'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac		
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailXuatNgu
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1
 
 		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  bHangCha=0 ) detail
		where #tempXuatNgu.bHangCha=1
	--- 9010001-010-011-0007 tro cap thoi viec

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailThoiViecSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			into #tempDetailThoiViec
			from  #tempDetailThoiViecSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
			, N'TC Thôi việc'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  bHangCha=0 ) detail
		where #tempThoiViec.bHangCha=1
	--- 9010001-010-011-0008 tro cap tu tuat

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV

			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL

			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailTuTuatSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			into #tempDetailTuTuat
			from  #tempDetailTuTuatSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC



	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailTuTuat
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  bHangCha=0 ) detail
		where #tempTuTuat.bHangCha=1
		-- ket qua
	SELECT * INTO #tempRESULT  
	FROM
	(
		SELECT * FROM #tempHuuTri
		UNION ALL 
		SELECT * FROM #tempPhucVien
		UNION ALL 
		SELECT * FROM #tempXuatNgu
		UNION ALL
		SELECT * FROM #tempThoiViec
		UNION ALL
		SELECT * FROM #tempTuTuat
	) TBLRESULT

	SELECT * FROM #tempRESULT

	 DROP TABLE #tempHuuTri
	 DROP TABLE #tempPhucVien
	 DROP TABLE #tempXuatNgu
	 DROP TABLE #tempThoiViec
	 DROP TABLE #tempTuTuat
	 DROP TABLE #tempRESULT
	 DROP TABLE #TempDetailHuuTri
	 DROP TABLE #tempDetailPhucVien
	 DROP TABLE #TempDetailTuTuat
	 DROP TABLE #tempDetailThoiViec
	 DROP TABLE #tempDetailXuatNgu
	 DROP TABLE #tempDetailHuuTriSum
	 DROP TABLE #tempDetailPhucVienSum
	 DROP TABLE #TempDetailTuTuatSum
	 DROP TABLE #tempDetailThoiViecSum
	 DROP TABLE #tempDetailXuatNguSum

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN

	---IRemainRow 1: SQ, 2 : QNCN,3:CC, CN, VCQP,4:HSQBS, 5:LĐHĐ
	--- Type  :3 :Huu Tri,phuc vien,xuat ngu,thoi viec, tu tuat
	--- 2: Khoi du Toan, Khoi hach toan
	--- 1 SQ, QNCN,CC, CN, VCQP,HSQBS, LĐHĐ
	---- Ikhoi : 2  Khoi Du toan, 1 Khoi Hach Toan
	SELECT gt.*,dv.sTenDonVi INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

	SELECT gt.*,dv.sTenDonVi INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (
			gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

		--- Huu Tri Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriDuToan

		--- Phuc Vien Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienDuToan

		--- TC Xuất ngũ Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguDuToan

		--- TC Thôi việc  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecDuToan

		--- TC Tử tuất  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatDuToan

		--- Huu Tri Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriHachToan

		--- Phuc Vien Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienHachToan

		--- TC Xuất ngũ Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguHachToan

		--- TC Thôi việc  Khoi Hach Toan

			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecHachToan

		--- TC Tử tuất  Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatHachToan

		--- Khoi Du Toan
			SELECT 
					1 bHangCha
					, N'Khối dự toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 2 IKhoi
					into #tempKhoiDuToan

		--- Khoi Hach Toan
			SELECT 
					1 bHangCha
					, N'Khối hạch toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 1 IKhoi
					into #tempKhoiHachToan

		--- Sĩ quan
			SELECT 
				1 bHangCha
				, N'Sĩ quan' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 1 IRemainRow
				, 0 IKhoi
				into #tempSiQuan

		--- QNCN
			SELECT 
				1 bHangCha
				, N'QNCN' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 2 IRemainRow
				, 0 IKhoi
				into #tempQNCN

		--- CC, CN, VCQP
			SELECT 
				1 bHangCha
				, N'CC, CN, VCQP' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 3 IRemainRow
				, 0 IKhoi
				into #tempCNVCQP

        --- HSQBS
			SELECT 
				1 bHangCha
				, N'HSQBS' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 4 IRemainRow
				, 0 IKhoi
				into #tempHSQBS

		--- LĐHĐ
			SELECT 
				1 bHangCha
				, N'LĐHĐ' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 5 IRemainRow
				, 0 IKhoi
				into #tempLDHD
--- Du Toan
--- Huu Tri Du Toan
		--- Detal Si Quan DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
		--- Detal QNCN DuToan Huu Tri
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanHuuTri
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa)  Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	SELECT 0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanHuuTri
	FROM
			--- Detal CNVCQP DuToan Huu Tri
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanHuuTri
	FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanHuuTri
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Phục viên 
	SELECT 
		0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCapKV) FTienTroCapKV
				, SUM(FTienTroCapMT) FTienTroCapMT
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTroCapKVTL) FTienTroCapKVTL
				, SUM(FTienTroCapMTTL) FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 2 IRemainRow
				, 2 IKhoi
				into #tempDetailQNCNDuToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP DuToan Phuc Vien
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanPhucVien
		FROM (
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal HSQBS DuToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanPhucVien
		FROM
			( SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Phuc Vien
	SELECT  
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.sMaCapBac
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 1 IRemainRow
		, 2 IKhoi
		into #tempDetailLDHDDuToanPhucVien
			FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
		, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
		, Detail.sTenPhanHo
		, '' as SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 1 IRemainRow
		, 2 IKhoi
				into #tempDetailSiQuanDuToanXuatNgu
	FROM (
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac	

			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal QNCN DuToan Xuat Ngu 	
	SELECT 0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
		, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
		, Detail.sTenPhanHo
		, '' as SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 2 IRemainRow
		, 2 IKhoi
		into #tempDetailQNCNDuToanXuatNgu
		 FROM

			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	--- Detal CNVCQP DuToan Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
		, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
		, Detail.sTenPhanHo
		, '' as SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 3 IRemainRow
		, 2 IKhoi
	into #tempDetailCNVCQPDuToanXuatNgu
	FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY tbltc.sMa_Hieu_Can_Bo,  tbltc.sTenDonVi
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	--- Detal HSQBS DuToan Xuat Ngu
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanXuatNgu
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
					,tbltc.sTenDonVi
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sTenPhanHo
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

	--- Detal LDHD DuToan Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
		, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
		, Detail.sTenPhanHo
		, '' as SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 5 IRemainRow
		, 2 IKhoi
		into #tempDetailLDHDDuToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY tbltc.sMa_Hieu_Can_Bo,  tbltc.sTenDonVi
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				 ) Detail
		GROUP BY  Detail.sTenPhanHo
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanThoiViec
	FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP DuToan Thoi Viec
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.sMaCapBac
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 3 IRemainRow
		, 2 IKhoi
		into #tempDetailCNVCQPDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Thoi Viec
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
		into #tempDetailHSQBSDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal LDHD DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanThoiViec
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanTuTuat
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				 ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal CNVCQP DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
			) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

		--- Detal QNCN HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanHuuTri
		FROM 		
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa  ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal HSQBS HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanHuuTri
		FROM 		
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanHuuTri
		FROM 	
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanPhucVien
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal QNCN HachToan Phục viên 

	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanPhucVien
		FROM 		
		(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


--- Detal CNVCQP HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanPhucVien
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal HSQBS HachToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal LDHD HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanPhucVien
		FROM
			(	SELECT  
				0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi sTenPhanHo
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sMa_Hieu_Can_Bo
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
				, 0 AS FTienTroCapMT
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
				, 0 as FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 5 IRemainRow
				, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
			AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND tbltc.sMaCapBac like  '1%'
				GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	--- Detal QNCN HachToan Xuat Ngu 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
				FROM  #tblTroCapKhoiHachToan tbltc
				WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
				AND ( tbltc.sMaCapBac LIKE '3.1%' 
						OR tbltc.sMaCapBac LIKE '3.2%' 
						OR tbltc.sMaCapBac LIKE '3.3%'  
						OR tbltc.sMaCapBac = '413' 
						OR tbltc.sMaCapBac = '415')
				GROUP BY tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
						, tbltc.sSoQuyetDinh
						, tbltc.dNgayQuyetDinh
						, tbltc.sXauNoiMa) Detail
			GROUP BY  Detail.sTenPhanHo
					, Detail.sSoQuyetDinh
					, Detail.dNgayQuyetDinh


			--- Detal HSQBS HachToan Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanXuatNgu
		FROM
			(SELECT  
				0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
				, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
				, tbltc.sTenDonVi sTenPhanHo
				, '' as SMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sMa_Hieu_Can_Bo
				, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
				, 0 AS  FTienTroCapKV
				, 0 AS FTienTroCapMT
				, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
				, 0 as FTienTroCapKVTL
				, 0 as FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 4 IRemainRow
				, 1 IKhoi

				--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sTenPhanHo
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  Detail.sSoQuyetDinh is null or  Detail.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(Detail.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(Detail.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, Detail.sTenPhanHo
			, '' as SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanXuatNgu
		FROM
			(	
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
				GROUP BY  tbltc.sMa_Hieu_Can_Bo, tbltc.sTenDonVi
						, tbltc.sSoQuyetDinh
						, tbltc.dNgayQuyetDinh
						, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sTenPhanHo
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanThoiViec
		FROM
			(
				SELECT  
					0 bHangCha 
					, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi sTenPhanHo
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sMa_Hieu_Can_Bo
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
					, 0 AS FTienTroCapMT
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
					, 0 as FTienTroCapMTTL
					, 0 bHasData
					, 0 Type
					, null IsParent
					, 1 IRemainRow
					, 1 IKhoi

					--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
			AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
					) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN HachToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
					into #tempDetailQNCNHachToanThoiViec
		FROM
			(	SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
			AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
			) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa 			) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanThoiViec
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanThoiViec
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
			AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					,tbltc.sTenDonVi 
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
							) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal QNCN HachToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS HachToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,  tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa  ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

	--- 9010001-010-011-0004 Tro cap Huu tri
			SELECT * INTO #tempDetailDuToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanHuuTri
			)tblDetailHuuTri

			SELECT * INTO #tempDetailHachToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanHuuTri
			)tblDetailHuuTri

			--- UPDATE #tempDetailDuToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ##tempDetailHachToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempHuuTri FROM
			(
			SELECT * FROM #tempHuuTriDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanHuuTri
			UNION ALL
			SELECT * FROM #tempDetailHachToanHuuTri
			
			) tblHuutri

			--- UPDATE #tempHuuTri
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempHuuTri
				WHERE Type=0
			) B
			where  A.Type=3


	----- 9010001-010-011-0005 Tro cap phuc vien
			SELECT * INTO #tempDetailDuToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanPhucVien
			)tblDetailPhucVien

			SELECT * INTO #tempDetailHachToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanPhucVien
			)tblDetailPhucVien

		
			--- UPDATE ##tempDetailDuToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ###tempDetailHachToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempPhucVien FROM
			(
			SELECT * FROM #tempPhucVienDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanPhucVien
			UNION ALL
			SELECT * FROM #tempDetailHachToanPhucVien
			) tblPhucVien

			--- UPDATE #tempPhucVien
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempPhucVien
				WHERE Type=0
			) B
			where  A.Type=3

			
	----- 9010001-010-011-0006 Tro cap xuat ngu

			SELECT * INTO #tempDetailDuToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanXuatNgu
			)tblDetailPXuatNgu


			SELECT * INTO #tempDetailHachToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanXuatNgu
			)tblDetailXuatNgu

			--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2


			--- UPDATE #tempDetailHachToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- Update Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempXuatNgu FROM
			(
			SELECT * FROM #tempXuatNguDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanXuatNgu
			UNION ALL
			SELECT * FROM #tempDetailHachToanXuatNgu
			) tblXuatNgu

			--- UPDATE #tempXuatNgu
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempXuatNgu
				WHERE Type=0
			) B
			where  A.Type=3
	----- 9010001-010-011-0007 tro cap thoi viec
			SELECT * INTO #tempDetailDuToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanThoiViec
			)tblDetailThoiViec

			SELECT * INTO #tempDetailHachToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanThoiViec
			)tblDetailThoiViec

	 	--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanThoiViec
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempThoiViec FROM
			(
			SELECT * FROM #tempThoiViecDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanThoiViec
			UNION ALL
			SELECT * FROM #tempDetailHachToanThoiViec
			) tblThoiViec
			 
			 --- UPDATE #tempThoiViec
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempThoiViec
				WHERE Type=0
			) B
			where  A.Type=3
		
	----- 9010001-010-011-0008 tro cap tu tuat
			SELECT * INTO #tempDetailDuToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiDuToan 
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanTuTuat
			)tblDetailTuTuat

			SELECT * INTO #tempDetailHachToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanTuTuat
			)tblDetailTuTuat

			--- UPDATE #tempDetailDuToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempTuTuat FROM
			(
			SELECT * FROM #tempTuTuatDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanTuTuat
			UNION ALL
			SELECT * FROM #tempDetailHachToanTuTuat
			) tblTuTuat

			--- Update #tempTuTuat
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempTuTuat
				WHERE Type=0
			) B
			where  A.Type=3

			SELECT * INTO #tempRESULT  
			FROM
			(
				SELECT * FROM #tempHuuTri
				UNION ALL 
				SELECT * FROM #tempPhucVien
				UNION ALL 
				SELECT * FROM #tempXuatNgu
				UNION ALL
				SELECT * FROM #tempThoiViec
				UNION ALL
				SELECT * FROM #tempTuTuat
			) TBLRESULT

			SELECT * FROM #tempRESULT


		 DROP TABLE #tempRESULT
		 DROP TABLE #tblTroCapKhoiDuToan 
		 DROP TABLE #tblTroCapKhoiHachToan
		 DROP TABLE #tempHuuTri
		 DROP TABLE #tempDetailDuToanHuuTri
		 DROP TABLE #tempDetailHachToanHuuTri


		 DROP TABLE #tempPhucVien
		 DROP TABLE #tempDetailDuToanPhucVien
		 DROP TABLE #tempDetailHachToanPhucVien

		 DROP TABLE #tempXuatNgu 
		 DROP TABLE #tempDetailDuToanXuatNgu
		 DROP TABLE #tempDetailHachToanXuatNgu

		 DROP TABLE #tempThoiViec
		 DROP TABLE #tempDetailDuToanThoiViec
		 DROP TABLE #tempDetailHachToanThoiViec

		 DROP TABLE #tempTuTuat
		 DROP TABLE #tempDetailDuToanTuTuat
		 DROP TABLE #tempDetailHachToanTuTuat

		 DROP TABLE #tempKhoiDuToan
		 DROP TABLE #tempKhoiHachToan

		 DROP TABLE #tempHuuTriDuToan
		 DROP TABLE #tempPhucVienDuToan
		 DROP TABLE #tempXuatNguDuToan
		 DROP TABLE #tempThoiViecDuToan
		 DROP TABLE #tempTuTuatDuToan

		 DROP TABLE #tempSiQuan
		 DROP TABLE #tempQNCN
		 DROP TABLE #tempCNVCQP
		 DROP TABLE #tempHSQBS
		 DROP TABLE #tempLDHD

		 DROP TABLE #tempDetailSiQuanDuToanHuuTri
		 DROP TABLE #tempDetailQNCNDuToanHuuTri
		 DROP TABLE #tempDetailCNVCQPDuToanHuuTri
		 DROP TABLE #tempDetailHSQBSDuToanHuuTri
		 DROP TABLE #tempDetailLDHDDuToanHuuTri

		 DROP TABLE #tempDetailSiQuanDuToanPhucVien
		 DROP TABLE #tempDetailQNCNDuToanPhucVien
		 DROP TABLE #tempDetailCNVCQPDuToanPhucVien
		 DROP TABLE #tempDetailHSQBSDuToanPhucVien
		 DROP TABLE #tempDetailLDHDDuToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanDuToanXuatNgu
		 DROP TABLE #tempDetailQNCNDuToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPDuToanXuatNgu
		 DROP TABLE #tempDetailHSQBSDuToanXuatNgu
		 DROP TABLE #tempDetailLDHDDuToanXuatNgu

		 DROP TABLE #tempDetailSiQuanDuToanThoiViec
		 DROP TABLE #tempDetailQNCNDuToanThoiViec
		 DROP TABLE #tempDetailCNVCQPDuToanThoiViec
		 DROP TABLE #tempDetailHSQBSDuToanThoiViec
		 DROP TABLE #tempDetailLDHDDuToanThoiViec

		 DROP TABLE #tempDetailSiQuanDuToanTuTuat
		 DROP TABLE #tempDetailQNCNDuToanTuTuat
		 DROP TABLE #tempDetailCNVCQPDuToanTuTuat
		 DROP TABLE #tempDetailHSQBSDuToanTuTuat
		 DROP TABLE #tempDetailLDHDDuToanTuTuat

		 DROP TABLE #tempDetailSiQuanHachToanHuuTri
		 DROP TABLE #tempDetailQNCNHachToanHuuTri
		 DROP TABLE #tempDetailCNVCQPHachToanHuuTri
		 DROP TABLE #tempDetailHSQBSHachToanHuuTri
		 DROP TABLE #tempDetailLDHDHachToanHuuTri

		 DROP TABLE #tempDetailSiQuanHachToanPhucVien
		 DROP TABLE #tempDetailQNCNHachToanPhucVien
		 DROP TABLE #tempDetailCNVCQPHachToanPhucVien
		 DROP TABLE #tempDetailHSQBSHachToanPhucVien
		 DROP TABLE #tempDetailLDHDHachToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanHachToanXuatNgu
		 DROP TABLE #tempDetailQNCNHachToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPHachToanXuatNgu
		 DROP TABLE #tempDetailHSQBSHachToanXuatNgu
		 DROP TABLE #tempDetailLDHDHachToanXuatNgu

		 DROP TABLE #tempDetailSiQuanHachToanThoiViec
		 DROP TABLE #tempDetailQNCNHachToanThoiViec
		 DROP TABLE #tempDetailCNVCQPHachToanThoiViec
		 DROP TABLE #tempDetailHSQBSHachToanThoiViec
		 DROP TABLE #tempDetailLDHDHachToanThoiViec

		 DROP TABLE #tempDetailSiQuanHachToanTuTuat
		 DROP TABLE #tempDetailQNCNHachToanTuTuat
		 DROP TABLE #tempDetailCNVCQPHachToanTuTuat
		 DROP TABLE #tempDetailHSQBSHachToanTuTuat
		 DROP TABLE #tempDetailLDHDHachToanTuTuat


		 DROP TABLE #tempHuuTriHachToan
		 DROP TABLE #tempPhucVienHachToan
		 DROP TABLE #tempXuatNguHachToan
		 Drop table #tempThoiViecHachToan
		 Drop table #tempTuTuatHachToan

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN

	---IRemainRow 1: SQ, 2 : QNCN,3:CC, CN, VCQP,4:HSQBS, 5:LĐHĐ
	--- Type  :3 :Huu Tri,phuc vien,xuat ngu,thoi viec, tu tuat
	--- 2: Khoi du Toan, Khoi hach toan
	--- 1 SQ, QNCN,CC, CN, VCQP,HSQBS, LĐHĐ
	---- Ikhoi : 2  Khoi Du toan, 1 Khoi Hach Toan
	SELECT gt.*,dv.sTenDonVi INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			)
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

		order by gt.sTenCanBo

	SELECT gt.*,dv.sTenDonVi INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (
			--- Hoach toan
			gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

		order by gt.sTenCanBo
		--- Huu Tri Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriDuToan

		--- Phuc Vien Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienDuToan

		--- TC Xuất ngũ Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguDuToan

		--- TC Thôi việc  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecDuToan

		--- TC Tử tuất  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatDuToan

		--- Huu Tri Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriHachToan

		--- Phuc Vien Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienHachToan

		--- TC Xuất ngũ Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguHachToan

		--- TC Thôi việc  Khoi Hach Toan

			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecHachToan

		--- TC Tử tuất  Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatHachToan

		--- Khoi Du Toan
			SELECT 
					1 bHangCha
					, N'Khối dự toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 2 IKhoi
					into #tempKhoiDuToan

		--- Khoi Hach Toan
			SELECT 
					1 bHangCha
					, N'Khối hạch toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 1 IKhoi
					into #tempKhoiHachToan

		--- Si Quan
			SELECT 
				1 bHangCha
				, N'Sĩ quan' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 1 IRemainRow
				, 0 IKhoi
				into #tempSiQuan

		--- QNCN
			SELECT 
				1 bHangCha
				, N'QNCN' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 2 IRemainRow
				, 0 IKhoi
				into #tempQNCN

		--- CC, CN, VCQP
			SELECT 
				1 bHangCha
				, N'CC, CN, VCQP' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 3 IRemainRow
				, 0 IKhoi
				into #tempCNVCQP

        --- HSQBS
			SELECT 
				1 bHangCha
				, N'HSQBS' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 4 IRemainRow
				, 0 IKhoi
				into #tempHSQBS

		--- LĐHĐ
			SELECT 
				1 bHangCha
				, N'LĐHĐ' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 5 IRemainRow
				, 0 IKhoi
				into #tempLDHD
--- Du Toan
--- Huu Tri Du Toan
		--- Detal Si Quan DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
		--- Detal QNCN DuToan Huu Tri
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanHuuTri
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa)  Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	SELECT 0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanHuuTri
	FROM
			--- Detal CNVCQP DuToan Huu Tri
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanHuuTri
	FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanHuuTri
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Phục viên 
	SELECT 
		0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCapKV) FTienTroCapKV
				, SUM(FTienTroCapMT) FTienTroCapMT
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTroCapKVTL) FTienTroCapKVTL
				, SUM(FTienTroCapMTTL) FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 2 IRemainRow
				, 2 IKhoi
				into #tempDetailQNCNDuToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP DuToan Phuc Vien
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanPhucVien
		FROM (
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal HSQBS DuToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanPhucVien
		FROM
			( SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Phuc Vien
	SELECT  
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.sMaCapBac
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 1 IRemainRow
		, 2 IKhoi
		into #tempDetailLDHDDuToanPhucVien
			FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 1 IRemainRow
		, 2 IKhoi
				into #tempDetailSiQuanDuToanXuatNgu
	FROM (
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac

			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal QNCN DuToan Xuat Ngu 	
	SELECT 0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 2 IRemainRow
		, 2 IKhoi
		into #tempDetailQNCNDuToanXuatNgu
		 FROM
			(SELECT  
				0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi sTenPhanHo
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sMa_Hieu_Can_Bo
				, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
				, 0 AS  FTienTroCapKV
				, 0 AS FTienTroCapMT
				, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
				, 0 as FTienTroCapKVTL
				, 0 as FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 2 IRemainRow
						, 2 IKhoi
				FROM  #tblTroCapKhoiDuToan tbltc
				WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
				AND tbltc.sMaCapBac  LIKE '2%'
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
							, tbltc.sTenCanBo
							, tbltc.sTenDonVi
							, tbltc.sMaCapBac
							, tbltc.sSoQuyetDinh
							, tbltc.dNgayQuyetDinh
							, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


	--- Detal CNVCQP DuToan Phuc Vien
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 3 IRemainRow
		, 2 IKhoi
	into #tempDetailCNVCQPDuToanXuatNgu
	FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

	--- Detal HSQBS DuToan Xuat Ngu
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.SMaCapBac	
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanXuatNgu
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Xuat Ngu
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.SMaCapBac	
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 5 IRemainRow
		, 2 IKhoi
		into #tempDetailLDHDDuToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanThoiViec
	FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP DuToan Thoi Viec
	SELECT 
		0 bHangCha 
		, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
		, Detail.sTenCanBo
		, Detail.sTenPhanHo
		, Detail.sMaCapBac
		, Detail.sSoQuyetDinh
		, Detail.dNgayQuyetDinh
		, SUM(FTienTroCap1Lan) FTienTroCap1Lan
		, SUM(FTienTroCapKV) FTienTroCapKV
		, SUM(FTienTroCapMT) FTienTroCapMT
		, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
		, SUM(FTienTroCapKVTL) FTienTroCapKVTL
		, SUM(FTienTroCapMTTL) FTienTroCapMTTL
		, 0 bHasData
		, 0 Type
		, null IsParent
		, 3 IRemainRow
		, 2 IKhoi
		into #tempDetailCNVCQPDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Thoi Viec
	SELECT 
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
		into #tempDetailHSQBSDuToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal LDHD DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanThoiViec
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanTuTuat
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal QNCN DuToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				 ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal CNVCQP DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal HSQBS DuToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD DuToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanTuTuat
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
			) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

		--- Detal QNCN HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanHuuTri
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanHuuTri
		FROM 		
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa  ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal HSQBS HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanHuuTri
		FROM 		
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Huu Tri
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanHuuTri
		FROM 	
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanPhucVien
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal QNCN HachToan Phục viên 

	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanPhucVien
		FROM 		
		(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


--- Detal CNVCQP HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanPhucVien
		FROM 
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Detal HSQBS HachToan Phục viên 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanPhucVien
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh


			--- Detal LDHD HachToan Phuc Vien
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanPhucVien
		FROM
			(	SELECT  
				0 bHangCha 
				, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi sTenPhanHo
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sMa_Hieu_Can_Bo
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
				, 0 AS FTienTroCapMT
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
				,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
				, 0 as FTienTroCapMTTL
				, 0 bHasData
				, 0 Type
				, null IsParent
				, 5 IRemainRow
				, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
			AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				,tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa ) Detail
	GROUP BY  Detail.sMa_Hieu_Can_Bo
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh

--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND tbltc.sMaCapBac like  '1%'
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
						, tbltc.sTenCanBo
						, tbltc.sTenDonVi
						, tbltc.sMaCapBac
						, tbltc.sSoQuyetDinh
						, tbltc.dNgayQuyetDinh
						, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN HachToan Xuat Ngu 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
			AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					,tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa  ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Xuat Ngu 
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanXuatNgu
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
				FROM  #tblTroCapKhoiHachToan tbltc
				WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
				AND ( tbltc.sMaCapBac LIKE '3.1%' 
						OR tbltc.sMaCapBac LIKE '3.2%' 
						OR tbltc.sMaCapBac LIKE '3.3%'  
						OR tbltc.sMaCapBac = '413' 
						OR tbltc.sMaCapBac = '415')
				GROUP BY  tbltc.sMa_Hieu_Can_Bo
						, tbltc.sTenCanBo
						, tbltc.sTenDonVi
						, tbltc.sMaCapBac
						, tbltc.sSoQuyetDinh
						, tbltc.dNgayQuyetDinh
						, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal HSQBS HachToan Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanXuatNgu
		FROM
			(		SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Xuat Ngu
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanXuatNgu
		FROM
			(	
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanThoiViec
		FROM
			(
				SELECT  
					0 bHangCha 
					, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi sTenPhanHo
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sMa_Hieu_Can_Bo
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
					, 0 AS FTienTroCapMT
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
					,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
					+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
					, 0 as FTienTroCapMTTL
					, 0 bHasData
					, 0 Type
					, null IsParent
					, 1 IRemainRow
					, 1 IKhoi

					--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
					--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
			AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
					) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal QNCN HachToan Thoi Viec
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
					into #tempDetailQNCNHachToanThoiViec
		FROM
			(	SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
			AND tbltc.sMaCapBac  LIKE '2%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenDonVi
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
			) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanThoiViec
		FROM
			(SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa 			) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanThoiViec
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanThoiViec
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
				, tbltc.sTenCanBo
				, tbltc.sTenDonVi
				, tbltc.sMaCapBac
				, tbltc.sSoQuyetDinh
				, tbltc.dNgayQuyetDinh
				, tbltc.sXauNoiMa
				) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			FROM  #tblTroCapKhoiHachToan tbltc
			WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
			AND tbltc.sMaCapBac like  '1%'
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					,tbltc.sTenDonVi 
					, tbltc.sMaCapBac
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh
					, tbltc.sXauNoiMa
							) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh


			--- Detal QNCN HachToan Tu Tuat
	SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal CNVCQP HachToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal HSQBS HachToan Tu Tuat
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			,  tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

			--- Detal LDHD HachToan Thoi Viec
		SELECT 
		0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY Detail.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Detail.sTenCanBo
			, Detail.sTenPhanHo
			, Detail.sMaCapBac
			, Detail.sSoQuyetDinh
			, Detail.dNgayQuyetDinh
			, SUM(FTienTroCap1Lan) FTienTroCap1Lan
			, SUM(FTienTroCapKV) FTienTroCapKV
			, SUM(FTienTroCapMT) FTienTroCapMT
			, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
			, SUM(FTienTroCapKVTL) FTienTroCapKVTL
			, SUM(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanTuTuat
		FROM
			(
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi

			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa  ) Detail
		GROUP BY  Detail.sMa_Hieu_Can_Bo
				, Detail.sTenCanBo
				, Detail.sTenPhanHo
				, Detail.sMaCapBac
				, Detail.sSoQuyetDinh
				, Detail.dNgayQuyetDinh

	--- 9010001-010-011-0004 Tro cap Huu tri
			SELECT * INTO #tempDetailDuToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanHuuTri
			)tblDetailHuuTri

			SELECT * INTO #tempDetailHachToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanHuuTri
			)tblDetailHuuTri

			--- UPDATE #tempDetailDuToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ##tempDetailHachToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempHuuTri FROM
			(
			SELECT * FROM #tempHuuTriDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanHuuTri
			UNION ALL
			SELECT * FROM #tempDetailHachToanHuuTri
			
			) tblHuutri

			--- UPDATE #tempHuuTri
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempHuuTri
				WHERE Type=0
			) B
			where  A.Type=3


	----- 9010001-010-011-0005 Tro cap phuc vien
			SELECT * INTO #tempDetailDuToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanPhucVien
			)tblDetailPhucVien

			SELECT * INTO #tempDetailHachToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanPhucVien
			)tblDetailPhucVien

		
			--- UPDATE ##tempDetailDuToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ###tempDetailHachToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempPhucVien FROM
			(
			SELECT * FROM #tempPhucVienDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanPhucVien
			UNION ALL
			SELECT * FROM #tempDetailHachToanPhucVien
			) tblPhucVien

			--- UPDATE #tempPhucVien
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempPhucVien
				WHERE Type=0
			) B
			where  A.Type=3

			
	----- 9010001-010-011-0006 Tro cap xuat ngu

			SELECT * INTO #tempDetailDuToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanXuatNgu
			)tblDetailPXuatNgu


			SELECT * INTO #tempDetailHachToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanXuatNgu
			)tblDetailXuatNgu

			--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2


			--- UPDATE #tempDetailHachToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- Update Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempXuatNgu FROM
			(
			SELECT * FROM #tempXuatNguDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanXuatNgu
			UNION ALL
			SELECT * FROM #tempDetailHachToanXuatNgu
			) tblXuatNgu

			--- UPDATE #tempXuatNgu
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempXuatNgu
				WHERE Type=0
			) B
			where  A.Type=3
	----- 9010001-010-011-0007 tro cap thoi viec
			SELECT * INTO #tempDetailDuToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanThoiViec
			)tblDetailThoiViec

			SELECT * INTO #tempDetailHachToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanThoiViec
			)tblDetailThoiViec

	 	--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanThoiViec
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempThoiViec FROM
			(
			SELECT * FROM #tempThoiViecDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanThoiViec
			UNION ALL
			SELECT * FROM #tempDetailHachToanThoiViec
			) tblThoiViec
			 
			 --- UPDATE #tempThoiViec
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempThoiViec
				WHERE Type=0
			) B
			where  A.Type=3
		
	----- 9010001-010-011-0008 tro cap tu tuat
			SELECT * INTO #tempDetailDuToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiDuToan 
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanTuTuat
			)tblDetailTuTuat

			SELECT * INTO #tempDetailHachToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanTuTuat
			)tblDetailTuTuat

			--- UPDATE #tempDetailDuToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempTuTuat FROM
			(
			SELECT * FROM #tempTuTuatDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanTuTuat
			UNION ALL
			SELECT * FROM #tempDetailHachToanTuTuat
			) tblTuTuat

			--- Update #tempTuTuat
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempTuTuat
				WHERE Type=0
			) B
			where  A.Type=3

			SELECT * INTO #tempRESULT  
			FROM
			(
				SELECT * FROM #tempHuuTri
				UNION ALL 
				SELECT * FROM #tempPhucVien
				UNION ALL 
				SELECT * FROM #tempXuatNgu
				UNION ALL
				SELECT * FROM #tempThoiViec
				UNION ALL
				SELECT * FROM #tempTuTuat
			) TBLRESULT

			SELECT * FROM #tempRESULT


		 DROP TABLE #tempRESULT
		 DROP TABLE #tblTroCapKhoiDuToan 
		 DROP TABLE #tblTroCapKhoiHachToan
		 DROP TABLE #tempHuuTri
		 DROP TABLE #tempDetailDuToanHuuTri
		 DROP TABLE #tempDetailHachToanHuuTri


		 DROP TABLE #tempPhucVien
		 DROP TABLE #tempDetailDuToanPhucVien
		 DROP TABLE #tempDetailHachToanPhucVien

		 DROP TABLE #tempXuatNgu 
		 DROP TABLE #tempDetailDuToanXuatNgu
		 DROP TABLE #tempDetailHachToanXuatNgu

		 DROP TABLE #tempThoiViec
		 DROP TABLE #tempDetailDuToanThoiViec
		 DROP TABLE #tempDetailHachToanThoiViec

		 DROP TABLE #tempTuTuat
		 DROP TABLE #tempDetailDuToanTuTuat
		 DROP TABLE #tempDetailHachToanTuTuat

		 DROP TABLE #tempKhoiDuToan
		 DROP TABLE #tempKhoiHachToan

		 DROP TABLE #tempHuuTriDuToan
		 DROP TABLE #tempPhucVienDuToan
		 DROP TABLE #tempXuatNguDuToan
		 DROP TABLE #tempThoiViecDuToan
		 DROP TABLE #tempTuTuatDuToan

		 DROP TABLE #tempSiQuan
		 DROP TABLE #tempQNCN
		 DROP TABLE #tempCNVCQP
		 DROP TABLE #tempHSQBS
		 DROP TABLE #tempLDHD

		 DROP TABLE #tempDetailSiQuanDuToanHuuTri
		 DROP TABLE #tempDetailQNCNDuToanHuuTri
		 DROP TABLE #tempDetailCNVCQPDuToanHuuTri
		 DROP TABLE #tempDetailHSQBSDuToanHuuTri
		 DROP TABLE #tempDetailLDHDDuToanHuuTri

		 DROP TABLE #tempDetailSiQuanDuToanPhucVien
		 DROP TABLE #tempDetailQNCNDuToanPhucVien
		 DROP TABLE #tempDetailCNVCQPDuToanPhucVien
		 DROP TABLE #tempDetailHSQBSDuToanPhucVien
		 DROP TABLE #tempDetailLDHDDuToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanDuToanXuatNgu
		 DROP TABLE #tempDetailQNCNDuToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPDuToanXuatNgu
		 DROP TABLE #tempDetailHSQBSDuToanXuatNgu
		 DROP TABLE #tempDetailLDHDDuToanXuatNgu

		 DROP TABLE #tempDetailSiQuanDuToanThoiViec
		 DROP TABLE #tempDetailQNCNDuToanThoiViec
		 DROP TABLE #tempDetailCNVCQPDuToanThoiViec
		 DROP TABLE #tempDetailHSQBSDuToanThoiViec
		 DROP TABLE #tempDetailLDHDDuToanThoiViec

		 DROP TABLE #tempDetailSiQuanDuToanTuTuat
		 DROP TABLE #tempDetailQNCNDuToanTuTuat
		 DROP TABLE #tempDetailCNVCQPDuToanTuTuat
		 DROP TABLE #tempDetailHSQBSDuToanTuTuat
		 DROP TABLE #tempDetailLDHDDuToanTuTuat

		 DROP TABLE #tempDetailSiQuanHachToanHuuTri
		 DROP TABLE #tempDetailQNCNHachToanHuuTri
		 DROP TABLE #tempDetailCNVCQPHachToanHuuTri
		 DROP TABLE #tempDetailHSQBSHachToanHuuTri
		 DROP TABLE #tempDetailLDHDHachToanHuuTri

		 DROP TABLE #tempDetailSiQuanHachToanPhucVien
		 DROP TABLE #tempDetailQNCNHachToanPhucVien
		 DROP TABLE #tempDetailCNVCQPHachToanPhucVien
		 DROP TABLE #tempDetailHSQBSHachToanPhucVien
		 DROP TABLE #tempDetailLDHDHachToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanHachToanXuatNgu
		 DROP TABLE #tempDetailQNCNHachToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPHachToanXuatNgu
		 DROP TABLE #tempDetailHSQBSHachToanXuatNgu
		 DROP TABLE #tempDetailLDHDHachToanXuatNgu

		 DROP TABLE #tempDetailSiQuanHachToanThoiViec
		 DROP TABLE #tempDetailQNCNHachToanThoiViec
		 DROP TABLE #tempDetailCNVCQPHachToanThoiViec
		 DROP TABLE #tempDetailHSQBSHachToanThoiViec
		 DROP TABLE #tempDetailLDHDHachToanThoiViec

		 DROP TABLE #tempDetailSiQuanHachToanTuTuat
		 DROP TABLE #tempDetailQNCNHachToanTuTuat
		 DROP TABLE #tempDetailCNVCQPHachToanTuTuat
		 DROP TABLE #tempDetailHSQBSHachToanTuTuat
		 DROP TABLE #tempDetailLDHDHachToanTuTuat


		 DROP TABLE #tempHuuTriHachToan
		 DROP TABLE #tempPhucVienHachToan
		 DROP TABLE #tempXuatNguHachToan
		 Drop table #tempThoiViecHachToan
		 Drop table #tempTuTuatHachToan

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 8/2/2024 3:07:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
SELECT gt.*,dv.sTenDonVi INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
left join DonVi dv on dv.iID_MaDonVi=gt.iiD_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1

	--- 9010001-010-011-0004 Tro cap Huu tri
	--- 9010001-010-011-0004 Tro cap Huu tri

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
						, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #TempDetailHuuTriSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo 
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #TempDetailHuuTri
			from  #TempDetailHuuTriSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			--ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
			, N'TC Hưu Trí'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #TempDetailHuuTri
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

	
		UPDATE  A
		SET A.FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		A.FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		A.FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		A.FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		A.FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		A.FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri A ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  bHangCha=0 ) detail
		where A.bHangCha=1


		
	--- 9010001-010-011-0005 Tro cap phuc vien
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.sMaCapBac
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL

			INTO #tempDetailPhucVienSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #tempDetailPhucVien
			from  #tempDetailPhucVienSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	) tblPhucVien

	
	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1
		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  bHangCha=0 ) detail
		where #tempPhucVien.bHangCha=1
	--- 9010001-010-011-0006 Tro cap xuat ngu

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			--, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, '' as SMaCapBac		
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			--, 0 AS  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			 INTO #tempDetailXuatNguSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY 
			 tbltc.sTenDonVi 
			 , tbltc.sMa_Hieu_Can_Bo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa


			SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			,  CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			INTO #tempDetailXuatNgu
			from  #tempDetailXuatNguSum tbltc
			GROUP BY 
					 tbltc.sTenPhanHo 
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh


--ORDER BY  tbltc.sTenCanBo desc,tbltc.sMaCapBac desc
	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
			, N'TC Xuất ngũ'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac		
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailXuatNgu
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1
		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  bHangCha=0 ) detail
		where #tempXuatNgu.bHangCha=1
	--- 9010001-010-011-0007 tro cap thoi viec

	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailThoiViecSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			into #tempDetailThoiViec
			from  #tempDetailThoiViecSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
			, N'TC Thôi việc'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  bHangCha=0 ) detail
		where #tempThoiViec.bHangCha=1
	--- 9010001-010-011-0008 tro cap tu tuat

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  tbltc.sMa_Hieu_Can_Bo
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			into #TempDetailTuTuatSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa 
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			into #TempDetailTuTuat
			from  #TempDetailTuTuatSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh

				ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #TempDetailTuTuat
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  bHangCha=0 ) detail
				where #tempTuTuat.bHangCha=1
		-- ket qua
	SELECT * INTO #tempRESULT  
	FROM
	(
		SELECT * FROM #tempHuuTri
		UNION ALL 
		SELECT * FROM #tempPhucVien
		UNION ALL 
		SELECT * FROM #tempXuatNgu
		UNION ALL
		SELECT * FROM #tempThoiViec
		UNION ALL
		SELECT * FROM #tempTuTuat
	) TBLRESULT

	SELECT * FROM #tempRESULT

	 DROP TABLE #tempHuuTri
	 DROP TABLE #tempPhucVien
	 DROP TABLE #tempXuatNgu
	 DROP TABLE #tempThoiViec
	 DROP TABLE #tempTuTuat
	 DROP TABLE #tempRESULT
	 DROP TABLE #TempDetailHuuTri
	 DROP TABLE #tempDetailPhucVien
	 DROP TABLE #TempDetailTuTuat
	 DROP TABLE #tempDetailThoiViec
	 DROP TABLE #tempDetailXuatNgu

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

update BH_DM_MucLucNganSach
set fTyLe_BHXH_NLD=null, fTyLe_BHXH_NSD=null,fTyLe_BHYT_NSD=null
where sXauNoiMa='9020001-010-011-0001-0003'

update BH_DM_MucLucNganSach
set fTyLe_BHXH_NLD=null,fTyLe_BHXH_NSD=null,fTyLe_BHYT_NSD=null
where sXauNoiMa='9020002-010-011-0001-0003'