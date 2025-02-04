/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]    Script Date: 6/3/2024 5:30:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]    Script Date: 6/5/2024 5:42:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 6/5/2024 5:42:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 6/5/2024 5:42:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 6/5/2024 5:42:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[bh_qtcq_ctct_gttrocap_index]    Script Date: 6/5/2024 5:42:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_qtcq_ctct_gttrocap_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_qtcq_ctct_gttrocap_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]    Script Date: 6/3/2024 5:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt] 
	@IdCsYTe NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@Donvitinh int,
	@XauNoiMa NVARCHAR(50)
AS
BEGIN
		select 
			row_number() over (order by ctct.iID_MaCoSoYTe) as sTT,
			sum(ctct.fDaQuyetToan)/@Donvitinh as fDaQuyetToan, 
			(sum(ctct.fDaCapUng)/@Donvitinh - sum(ctct.fDaQuyetToan)/@Donvitinh) as fThuaThieu, 
			sum(ctct.fSoCapBoSung)/@Donvitinh as fSoCapBoSung, 
			ctct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ctct
		inner join BH_CP_CapBoSung_KCB_BHYT as ct on ctct.iID_CTCapPhatBS = ct.iID_CTCapPhatBS
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = ctct.iID_MaCoSoYTe
		where ctct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			and ct.iNamLamViec = @NamLamViec
			--and ct.iLoaiTongHop = 2 and ct.sDSSoChungTuTongHop is not null
			and ct.iQuy = @IQuy
			and csyt.iNamLamViec = @NamLamViec
			and ctct.sXauNoiMa = @XauNoiMa
		group by ctct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[bh_qtcq_ctct_gttrocap_index]    Script Date: 6/5/2024 5:42:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị giai thich 
-- =============================================
CREATE PROCEDURE [dbo].[bh_qtcq_ctct_gttrocap_index]
@YearWork int,
@IdQTCQuyCheDoBHXH uniqueidentifier,
@SXauNoiMa nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 gttc.iiD_QTC_Quy_CTCT_GiaiThichTroCap
		, gttc.iID_QTC_Quy_ChungTu as IID_QTC_Quy_ChungTu
		, gttc.iNamLamViec
		, gttc.iQuy
		, gttc.sNguoiSua
		, gttc.sNguoiTao
		, gttc.dNgaySua
		, gttc.dNgayTao
		, gttc.iSoNgayHuong
		, gttc.sMa_Hieu_Can_Bo AS SMaHieuCanBo
		, gttc.iiD_MaPhanHo AS  ID_MaPhanHo
		, gttc.sMaCapBac
		, gttc.sTenCapBac
		, gttc.fSoTien fSoTien
		, gttc.iiD_MaPhanHo
		, gttc.sSoQuyetDinh
		, gttc.sTenCanBo
		, gttc.sXauNoiMa
		, gttc.dNgayQuyetDinh
		, gttc.iiD_MaDonVi AS ID_MaDonVi
		, gttc.sSoSoBHXH
		, gttc.dTuNgay
		, gttc.dDenNgay
		, gttc.fTienLuongThangDongBHXH
		,gttc.sTenPhanHo

		-- Tong dự toán todo
	FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
	LEFT JOIN (select  ID_QTC_Quy_CheDoBHXH from BH_QTC_Quy_CheDoBHXH
	where sSoChungTu in (select sSoChungTu from BH_QTC_Quy_CheDoBHXH where ID_QTC_Quy_CheDoBHXH=@IdQTCQuyCheDoBHXH OR ID_QTC_Quy_CheDoBHXH=@IdQTCQuyCheDoBHXH and iNamChungTu=@YearWork )) TEMP ON gttc.iID_QTC_Quy_ChungTu=TEMP.ID_QTC_Quy_CheDoBHXH
	WHERE gttc.iNamLamViec=@YearWork
			AND gttc.sXauNoiMa=@SXauNoiMa
	
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 6/5/2024 5:42:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN

	select 
			ctct.iID_MLNS,
			sum(ctct.fDuToan) fDuToan,
			ctct.sXauNoiMa
			into #tempDuToan
		from
			BH_DTTM_BHYT_ThanNhan_ChiTiet ctct
			join BH_DTTM_BHYT_ThanNhan ct on ctct.iID_DTTM_BHYT_ThanNhan = ct.iID_DTTM_BHYT_ThanNhan
		where ct.iNamLamViec = @NamLamViec
			and ct.iID_MaDonVi = @IdDonVi
			group by
			ctct.iID_MLNS,ctct.sXauNoiMa

	select
		chungtudonvi.iID_QTTM_BHYT_ChungTu_ChiTiet,
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
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		dutoan.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		(isnull(dutoan.fDuToan, 0) - isnull(chungtudonvi.fDaQuyetToan, 0) - isnull(chungtudonvi.fSoPhaiThu, 0))/@Donvitinh fConLai,
		chungtudonvi.fSoPhaiThu/@Donvitinh fSoPhaiThu
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
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
			ctct.fConLai,
			ctct.fDaQuyetToan,
			ctct.fDuToan,
			ctct.fSoPhaiThu,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sGhiChu,
			ctct.sLNS,
			ctct.sXauNoiMa,
			ctct.iID_QTTM_BHYT_ChungTu
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		left join
		(select ctct.sXauNoiMa, isnull(sum(ctct.fDuToan), 0) fDuToan 
			from BH_DTTM_BHYT_ThanNhan_ChiTiet ctct
			join BH_DTTM_BHYT_ThanNhan ct on ctct.iID_DTTM_BHYT_ThanNhan = ct.iID_DTTM_BHYT_ThanNhan
			where ct.iNamLamViec = @NamLamViec
			and ct.iID_MaDonVi = @IdDonVi
			and ct.bIsKhoa = 1
			group by ctct.sXauNoiMa) dutoan on mlns.sXauNoiMa = dutoan.sXauNoiMa
			
		order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 6/5/2024 5:42:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN


		select 
	ctct.sXauNoiMa,
	sum(ctct.fDuToan) fDuToan,
	ctct.iID_MaDonVi
	into #tempDuToan
	from
	BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	where ctct.iNamLamViec = @NamLamViec
	and ctct.iID_MaDonVi = @IdDonVi
	group by
	ctct.sXauNoiMa ,ctct.iID_MaDonVi

	select
		chungtudonvi.iID_QTTM_BHYT_ChungTu_ChiTiet,
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
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		dtoan.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		(isnull(chungtudonvi.fDuToan, 0) - isnull(chungtudonvi.fDaQuyetToan, 0) - isnull(chungtudonvi.fSoPhaiThu, 0))/@Donvitinh fConLai,
		chungtudonvi.fSoPhaiThu/@Donvitinh fSoPhaiThu
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
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
			ctct.fConLai,
			ctct.fDaQuyetToan,
			ctct.fDuToan,
			ctct.fSoPhaiThu,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sGhiChu,
			ctct.sLNS,
			ctct.sXauNoiMa,
			ctct.iID_QTTM_BHYT_ChungTu
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				Left join #tempDuToan dtoan on dtoan.sXauNoiMa=mlns.sXauNoiMa
		order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 6/5/2024 5:42:11 PM ******/
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 

		---Lấy danh sách đơn vị được chọn
		select 
			iID_DonVi,
			iID_MaDonVi,
			sTenDonVi
		into #tblDonvi
		from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  in (select * from dbo.splitstring(@IdMaDonVi)) 


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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]    Script Date: 6/5/2024 5:42:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi] 
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int
AS
BEGIN

	select 
	ctct.sXauNoiMa,
	sum(ctct.fDuToan) fDuToan
	into #tempDuToan
	from
	BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	where ctct.iNamLamViec = @NamLamViec
	and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
	group by
	ctct.sXauNoiMa ,ctct.iID_MaDonVi

	select
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		sum(dToan.fDuToan)/@Donvitinh fDuToan,
		sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
		sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
		sum(chungtudonvi.fSoPhaiThu)/@Donvitinh fSoPhaiThu
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
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
			ctct.fConLai,
			ctct.fDaQuyetToan,
			ctct.fDuToan,
			ctct.fSoPhaiThu,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sGhiChu,
			ctct.sLNS,
			ctct.sXauNoiMa,
			ctct.iID_QTTM_BHYT_ChungTu
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = 0
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			left join #tempDuToan dToan on dToan.sXauNoiMa=mlns.sXauNoiMa
			group by
				mlns.iID_MLNS,
				mlns.sMoTa,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa
		order by mlns.sXauNoiMa
END
;
;
;
GO
