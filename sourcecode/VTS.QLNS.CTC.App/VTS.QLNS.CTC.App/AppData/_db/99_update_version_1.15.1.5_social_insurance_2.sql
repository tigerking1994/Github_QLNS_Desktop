/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]    Script Date: 11/21/2024 10:49:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]    Script Date: 11/21/2024 10:49:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 11/21/2024 10:49:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]    Script Date: 11/21/2024 10:49:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 11/21/2024 10:49:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 11/21/2024 10:49:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]    Script Date: 11/21/2024 10:49:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 11/21/2024 10:49:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 11/21/2024 10:49:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 11/21/2024 10:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.f_split(@SLN)) 

		---Lấy danh sách đơn vị được chọn
		select 
			iID_DonVi,
			iID_MaDonVi,
			sTenDonVi
		into #tblDonvi
		from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  in (select * from dbo.f_split(@IdMaDonVi)) 


		---Lấy thông tin chi tiết chứng từ
		select
			ROW_NUMBER() OVER(ORDER BY as_qtct_1.iID_MaDonVi ASC) AS sTT,
			as_qtct_1.iID_MaDonVi,
			dv.sTenDonVi,
			Sum(iSoNgayDuoi14BenhDaiNgay) as iSoNgayDuoi14BenhDaiNgay,
			Sum(fSoTienDuoi14BenhDaiNgay) as fSoTienDuoi14BenhDaiNgay,
			Sum(iSoNgayTren14BenhDaiNgay) as iSoNgayTren14BenhDaiNgay,
			Sum(fSoTienTren14BenhDaiNgay) as fSoTienTren14BenhDaiNgay,
			Sum(iSoNgayDuoi14OmKhac) as iSoNgayDuoi14OmKhac,
			Sum(fSoTienDuoi14OmKhac) as fSoTienDuoi14OmKhac,
			Sum(iSoNgayTren14OmKhac) as iSoNgayTren14OmKhac,
			Sum(fSoTienTren14OmKhac) as fSoTienTren14OmKhac,
			Sum(iSoNgayConOm) as iSoNgayConOm,
			Sum(fSoTienConOm) as fSoTienConOm,
			Sum(iSoNgayPHSK) as iSoNgayPHSK,
			Sum(fSoTienPHSK) as fSoTienPHSK,
			isnull(Sum(fSoTienDuoi14BenhDaiNgay),0) + isnull(Sum(fSoTienTren14BenhDaiNgay),0) + isnull(Sum(fSoTienDuoi14OmKhac),0) + isnull(Sum(fSoTienTren14OmKhac),0) + isnull(Sum(fSoTienConOm),0)
			+ isnull(Sum(fSoTienPHSK),0) as fTongTien
			
		from
			(
			select
				as_qtct.iID_MaDonVi,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-01-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-01-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-01-02' then iTongSo_DeNghi else 0 end iSoNgayTren14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-01-02' then fTongTien_DeNghi else 0 end fSoTienTren14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-02-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-02-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-02-02' then iTongSo_DeNghi else 0 end iSoNgayTren14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-02-02' then fTongTien_DeNghi else 0 end fSoTienTren14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0002' then iTongSo_DeNghi else 0 end iSoNgayConOm,
				case when as_qtct.sXauNoiMa = '010-011-0001-0002' then fTongTien_DeNghi else 0 end fSoTienConOm,
				case when as_qtct.sXauNoiMa = '010-011-0001-0003' then iTongSo_DeNghi else 0 end iSoNgayPHSK,
				case when as_qtct.sXauNoiMa = '010-011-0001-0003' then fTongTien_DeNghi else 0 end fSoTienPHSK
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14BenhDaiNgay,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14BenhDaiNgay,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-02' then iTongSo_DeNghi else 0 end iSoNgayTren14BenhDaiNgay,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-02' then fTongTien_DeNghi else 0 end fSoTienTren14BenhDaiNgay,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14OmKhac,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14OmKhac,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-02' then iTongSo_DeNghi else 0 end iSoNgayTren14OmKhac,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-02' then fTongTien_DeNghi else 0 end fSoTienTren14OmKhac,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0002-02-00' then iTongSo_DeNghi else 0 end iSoNgayConOm,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0002-02-00' then fTongTien_DeNghi else 0 end fSoTienConOm,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0003-03-00' then iTongSo_DeNghi else 0 end iSoNgayPHSK,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0003-03-00' then fTongTien_DeNghi else 0 end fSoTienPHSK
			from
			(
				select 
					qtcn.iID_MaDonVi,
					case when ml.sLNS = '9010001' then REPLACE(ml.sXauNoiMa,'9010001-' , '') else REPLACE(ml.sXauNoiMa,'9010002-' , '') end sXauNoiMa,
					Sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
					Sum(qtcn_ct.fTongTien_DeNghi)  as fTongTien_DeNghi
				from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
				inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
				inner join #tblMucLucNganSach ml on ml.iID_MLNS = qtcn_ct.iID_MucLucNganSach
				--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
				and qtcn.iQuyChungTu = @IQuy
				where qtcn.iNamChungTu=@INamLamViec
				group by qtcn.iID_MaDonVi,case when ml.sLNS = '9010001' then REPLACE(ml.sXauNoiMa,'9010001-' , '') else REPLACE(ml.sXauNoiMa,'9010002-' , '') end

			) as as_qtct) as as_qtct_1

			inner join #tblDonvi  dv on dv.iID_MaDonVi = as_qtct_1.iID_MaDonVi
			group by as_qtct_1.iID_MaDonVi, dv.sTenDonVi
		
		

		drop table #tblMucLucNganSach;
		drop table #tblDonvi


end
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 11/21/2024 10:49:46 AM ******/
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
								AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
								AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
					SELECT * FROM  #tempDetalLDHDDuToan
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]    Script Date: 11/21/2024 10:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]
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
								AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
								AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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

	
--- Lay thong tin giai thich theo khoi du toan
		---Du Toan SQ
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalDuToanSum 
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
				INTO #tempDetalDuToan 
				FROM #tempDetalDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

		---Du Toan QNCN
		SELECT 
								gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalHachToanSum 
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
				INTO #tempDetalHachToan
				FROM #tempDetalHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi

---------  Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
				(	SELECT * FROM  #tempDuToan
					UNION ALL
					SELECT * FROM  #tempDetalDuToan
			) TEMPHachToan


---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
				(	SELECT * FROM  #tempHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHachToan
			) TEMPHachToan

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
			FROM #tempkqDuToan A,
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
			, 3 type
			FROM #tempkqDuToan
			WHERE type=1) Detail
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
			FROM #tempkqHachToan A,
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
			, 3 type
			FROM #tempkqHachToan
			WHERE type=1) Detail
			where A.type=Detail.type



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

		DROP TABLE #tempDetalDuToanSum

		drop table #tempHachToan
		DROP TABLE #tempDetalHachToan
		DROP TABLE #tempDetalHachToanSum

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 11/21/2024 10:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]
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
		and iTrangThai = 1
		---Lấy danh sách đơn vị được chọn
		select 
			iID_DonVi,
			iID_MaDonVi,
			sTenDonVi
		into #tblDonVi
		from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  in (select * from dbo.f_split(@IdMaDonVi)) 

		---Lấy thông tin chi tiết chứng từ
		select
			ROW_NUMBER() OVER(ORDER BY as_qtct_1.iID_MaDonVi ASC) AS sTT,
			as_qtct_1.iID_MaDonVi,
			#tblDonVi.sTenDonVi,
			Sum(iSoNgaySinhConNNuoiCon) as iSoNgaySinhConNNuoiCon,
			Sum(fSoTienSinhConNNuoiCon) as fSoTienSinhConNNuoiCon,
			Sum(iSoNgaySinhTroCapSinhCon) as iSoNgaySinhTroCapSinhCon,
			Sum(fSoTienSinhTroCapSinhCon) as fSoTienSinhTroCapSinhCon,
			Sum(iSoNgayKhamThaiKHHGD) as ISoNgayKhamThaiKHHGD,
			Sum(fSoTienKhamThaiKHHGD) as fSoTienKhamThaiKHHGD,
			Sum(iSoNgayPHSKThaiSan) as iSoNgayPHSKThaiSan,
			Sum(fSoTienPHSKThaiSan) as fSoTienPHSKThaiSan,
			isnull(Sum(fSoTienSinhConNNuoiCon),0) + isnull(Sum(fSoTienSinhTroCapSinhCon),0) + isnull(Sum(fSoTienKhamThaiKHHGD),0) + isnull(Sum(fSoTienPHSKThaiSan),0)  as fTongTien
			,0 BHangCha
			,0 IsHangCha
			
		from
			(
			select
				as_qtct.iID_MaDonVi,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0001-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0001-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhConNNuoiCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0001-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0001-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhConNNuoiCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0002-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0002-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhTroCapSinhCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0002-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0002-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhTroCapSinhCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0003-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0003-00-02' then iTongSo_DeNghi else 0 end iSoNgayKhamThaiKHHGD,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0003-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0003-00-02' then fTongTien_DeNghi else 0 end fSoTienKhamThaiKHHGD,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0004-00' then iTongSo_DeNghi else 0 end iSoNgayPHSKThaiSan,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0004-00' then fTongTien_DeNghi else 0 end fSoTienPHSKThaiSan
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhConNNuoiCon,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhConNNuoiCon,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhTroCapSinhCon,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhTroCapSinhCon,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-02' then iTongSo_DeNghi else 0 end iSoNgayKhamThaiKHHGD,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-02' then fTongTien_DeNghi else 0 end fSoTienKhamThaiKHHGD,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-04-00' then iTongSo_DeNghi else 0 end iSoNgayPHSKThaiSan,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-04-00' then fTongTien_DeNghi else 0 end fSoTienPHSKThaiSan
			from
			(
				select 
					qtcn.iID_MaDonVi,
					case when ml.sLNS = '9010001' then REPLACE(ml.sXauNoiMa,'9010001-' , '') else REPLACE(ml.sXauNoiMa,'9010002-' , '') end sXauNoiMa,
					Sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
					Sum(qtcn_ct.fTongTien_DeNghi)  as fTongTien_DeNghi
				from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
				inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
				inner join #tblMucLucNganSach ml on ml.iID_MLNS = qtcn_ct.iID_MucLucNganSach
			
				--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
				and qtcn.iQuyChungTu = @IQuy
				where 	qtcn_ct.iNamLamViec=@INamLamViec
				group by qtcn.iID_MaDonVi,case when ml.sLNS = '9010001' then REPLACE(ml.sXauNoiMa,'9010001-' , '') else REPLACE(ml.sXauNoiMa,'9010002-' , '') end

			) as as_qtct) as as_qtct_1

			inner join #tblDonVi on #tblDonVi.iID_MaDonVi = as_qtct_1.iID_MaDonVi
			group by as_qtct_1.iID_MaDonVi, #tblDonVi.sTenDonVi
		
		

		drop table #tblMucLucNganSach;
		drop table #tblDonVi


end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 11/21/2024 10:49:46 AM ******/
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
								AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
								AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

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
				,1 as type
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]    Script Date: 11/21/2024 10:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]
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
								AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
								AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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

		
		--- Lay thong tin giai thich theo khoi du toan


		---Du Toan SQ
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienPHSKThaiSan
				
				INTO #tempDetalDuToanSum
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
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalDuToan 
				FROM #tempDetalDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		---Du Toan QNCN
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalHachToanSum 
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
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHachToan
				FROM #tempDetalHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi


---Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempDetalDuToan
			) TEMPDuToan

------ Update total khoi du toan

			update T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqDuToan T1, 
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			FROM #tempkqDuToan
			WHERE type=1) T2
			WHERE T1.type=T2.type


---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHachToan
					
			) TEMPHachToan


			update T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqHachToan T1, 
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			FROM #tempkqHachToan
			WHERE type=1) T2
			WHERE T1.type=T2.type

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

		DROP TABLE #tempDetalDuToan



		drop table #tempHachToan

		DROP TABLE #tempDetalHachToan

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 11/21/2024 10:49:46 AM ******/
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
					AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]    Script Date: 11/21/2024 10:49:46 AM ******/
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
					AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
					AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]    Script Date: 11/21/2024 10:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]
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
					AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
					AND dv.iID_MaDonVi  IN (select * from dbo.f_split(@IdMaDonVi)) 
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
				into #tempDetailDuToanSum
			FROM #TBL_TroCapTaiNanDuToan tbltctn
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
				INTO #tempDetailDuToan
				FROM #tempDetailDuToanSum tbltctn
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
				into  #tempDetailHachToanSum
			FROM #TBL_TroCapTaiNanHachToan tbltctn
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
				INTO #tempDetailHachToan
				FROM #tempDetailHachToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

		

---------  Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempDetailDuToan
					
			) TEMPDuToan

------ Update total khoi du toan
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
			FROM #tempkqDuToan T1, (			SELECT 
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
			FROM #tempkqDuToan
			WHERE type=1) T2
			WHERE T1.type=T2.type


---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempDetailHachToan
			) TEMPHachToan


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
			FROM #tempkqHachToan T1, (SELECT 
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
			FROM #tempkqHachToan
			WHERE type=1) T2
			WHERE T1.type=T2.type


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
		DROP TABLE #tempDetailDuToan
		DROP TABLE #tempDetailDuToanSum
		DROP TABLE #tempHachToan
		DROP TABLE #tempDetailHachToan
		DROP TABLE #tempDetailHachToanSum

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
;
GO
