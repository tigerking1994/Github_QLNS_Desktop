/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 7/24/2024 10:03:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 7/24/2024 10:03:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 7/24/2024 10:03:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 7/24/2024 10:03:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 7/24/2024 10:03:28 AM ******/
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
						WHERE (gt.sXauNoiMa like '9010001-010-011-0001%' or gt.sXauNoiMa like '9010002-010-011-0001%')
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec
								AND ct.iQuyChungTu = @IQuy 

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapOmDauHachToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0001%' or gt.sXauNoiMa like '9010002-010-011-0001%')
								AND gt.iNamLamViec = @INamLamViec  
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								AND dv.iKhoi=1
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 7/24/2024 10:03:28 AM ******/
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
						WHERE (gt.sXauNoiMa like '9010001-010-011-0002%' or gt.sXauNoiMa like '9010002-010-011-0002%')
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec
								AND ct.iQuyChungTu = @IQuy 

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapThaiSanHachToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						Inner JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0002%' or gt.sXauNoiMa like '9010002-010-011-0002%')
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								AND dv.iKhoi=1
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
		N'4. HSQBS' as sTT
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
		N'5. LDHD' as sTT
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
		N'4. HSQBS' as sTT
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
		N'5. LDHD' as sTT
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 7/24/2024 10:03:28 AM ******/
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
	SELECT gt.* INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
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
		and dv.iKhoi=2

	SELECT gt.* INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
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
		and dv.iKhoi=1

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
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

		--- Detal QNCN DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanHuuTri
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanPhucVien
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac	
			--, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			--, tbltc.sTenCanBo
			--, tbltc.sTenPhanHo
			--, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Xuat Ngu 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY   tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY   tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanThoiViec
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanTuTuat
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

		--- Detal QNCN HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanHuuTri
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanPhucVien
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Xuat Ngu 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanXuatNgu
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanThoiViec
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanTuTuat
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 7/24/2024 10:03:28 AM ******/
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
	SELECT gt.* INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
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
		and dv.iKhoi=2
		order by gt.sTenCanBo

	SELECT gt.* INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
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
		and dv.iKhoi=1
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
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
		--- Detal QNCN DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanHuuTri
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanPhucVien
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Xuat Ngu 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanThoiViec
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPDuToanTuTuat
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
		--- Detal QNCN HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanHuuTri
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanPhucVien
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Xuat Ngu 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanXuatNgu
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanThoiViec
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailSiQuanHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailQNCNHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailCNVCQPHachToanTuTuat
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
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailHSQBSHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
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
			into #tempDetailLDHDHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 7/24/2024 10:06:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]    Script Date: 7/24/2024 10:06:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 7/24/2024 10:06:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 7/24/2024 10:06:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 7/24/2024 10:06:23 AM ******/
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
	@DotNhan int,
	@IsMillionRound bit
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
 and  A.iKhoi !=2
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 7/24/2024 10:06:23 AM ******/
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
	@Donvitinh int,
	@IsMillionRound bit
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
 and A.iKhoi! =2
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]    Script Date: 7/24/2024 10:06:23 AM ******/
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
	@DotNhan int,
	@IsMillionRound bit
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
		, SUM(CASE WHEN @IsMillionRound = 1 THEN ROUND(ctct.fTongTien/1000000, 0)* 1000000 ELSE ctct.fTongTien END) as fTongTienDuToan
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
		 and A.iKhoi!=2

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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 7/24/2024 10:06:23 AM ******/
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
	@DotNhan int,
	@IsMillionRound bit
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
		, SUM(CASE WHEN @IsMillionRound = 1 THEN ROUND(ctct.fTongTien/1000000, 0)* 1000000 ELSE ctct.fTongTien END) as fTongTienDuToan
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
		 and A.iKhoi!=2

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
;
;
GO
update BH_DM_MucLucNganSach
SET sCPChiTietToi = NULL
where sXauNoiMa = '9010003-010-013'
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 7/24/2024 5:27:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 7/24/2024 5:27:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @LoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi,bHangChaDuToan
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

	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CTCT.sXauNoiMa
		into #tblDataQuyetToanDaDuyetQuyTruoc
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @NamLamViec
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		  AND CT.iQuyChungTu<@iQuy
	GROUP BY CTCT.sXauNoiMa

IF @LoaiTongHop=1
BEGIN
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		mlns.sDuToanChiTietToi,
		mlns.bHangChaDuToan,
		ISNULL((dt.fTienDuToanDuocGiao), 0) / @Dvt as FTienDuToanDuocGiao,
		ISNULL((dataQuyTruoc.fTienQuyetToanDaDuyet), 0) / @Dvt + ISNULL(SUM(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt   as FTienThucChi, 
		ISNULL((dataQuyTruoc.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(SUM(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
		ISNULL(SUM(ctct.fTienXacNhanQuyetToanQuyNay), 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								--AND bIsKhoa=1
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach
	LEFT JOIN (
			-- lấy ra dữ liệu dự toán
				SELECT 
					  SUM(fTienTuChi) AS fTienDuToanDuocGiao,
					  sXauNoiMa
			   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
			   WHERE iID_DTC_PhanBoDuToanChi IN
				   (SELECT ID
					FROM BH_DTC_PhanBoDuToanChi
					WHERE sSoQuyetDinh <> ''
					  AND sSoQuyetDinh IS NOT NULL
					  AND iNamChungTu = @NamLamViec
					  AND bIsKhoa=1
					  --AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
				 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
				) GROUP BY sXauNoiMa)dt on dt.sXauNoiMa=mlns.sXauNoiMa
	LEFT JOIN #tblDataQuyetToanDaDuyetQuyTruoc  dataQuyTruoc ON mlns.sXauNoiMa=dataQuyTruoc.sXauNoiMa
	Group by mlns.iID_MLNS
			, mlns.iID_MLNS_Cha
			, mlns.sXauNoiMa
			, mlns.sLNS
			, mlns.sL
			, mlns.sK
			, mlns.sM
			, mlns.sTM
			, mlns.sTTM
			, mlns.sNG
			, mlns.sTNG
			, mlns.sMoTa
			, mlns.bHangCha
			, mlns.sDuToanChiTietToi
			,mlns.bHangChaDuToan
			, dt.fTienDuToanDuocGiao
			, dataQuyTruoc.fTienQuyetToanDaDuyet
	Order by mlns.sXauNoiMa
END
ELSE
BEGIN
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		mlns.sDuToanChiTietToi,
		mlns.bHangChaDuToan,
		ISNULL((dt.fTienTuChi), 0) / @Dvt as FTienDuToanDuocGiao,
		ISNULL((dataQuyTruoc.fTienQuyetToanDaDuyet), 0) / @Dvt + ISNULL(SUM(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt   as FTienThucChi, 
		ISNULL((dataQuyTruoc.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(SUM(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
		ISNULL(SUM(ctct.fTienXacNhanQuyetToanQuyNay), 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								--AND iLoaiTongHop=@LoaiTongHop
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	LEFT JOIN (
		SELECT ctct.sXauNoiMa,
					SUM(ctct.fTienTuChi) fTienTuChi
					FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
					JOIN BH_DTC_DuToanChiTrenGiao ct 
					ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
					WHERE ct.iID_MaDonVi = @IdDonVi
					AND BIsKhoa = 1
					AND ct.iNamLamViec = @NamLamViec
					GROUP BY ctct.sXauNoiMa)
		dt on dt.sXauNoiMa=mlns.sXauNoiMa
		LEFT JOIN #tblDataQuyetToanDaDuyetQuyTruoc  dataQuyTruoc ON mlns.sXauNoiMa=dataQuyTruoc.sXauNoiMa
	Group by mlns.iID_MLNS
		, mlns.iID_MLNS_Cha
		, mlns.sXauNoiMa
		, mlns.sLNS
		, mlns.sL
		, mlns.sK
		, mlns.sM
		, mlns.sTM
		, mlns.sTTM
		, mlns.sNG
		, mlns.sTNG
		, mlns.sMoTa
		, mlns.bHangCha
		, mlns.sDuToanChiTietToi
		, mlns.bHangChaDuToan
		, dt.fTienTuChi
		, dataQuyTruoc.fTienQuyetToanDaDuyet
	Order by mlns.sXauNoiMa
END
END
;
;
;
;
;
;
;
GO
