/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 27/07/2022 4:17:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 27/07/2022 4:17:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]
	@YearOfWork int,
	@YearOrBudget nvarchar(20),
	@ListLNS nvarchar(MAX),
	@ListUnitId nvarchar(MAX),
	@DataType int,
	@BudgetSource int,
	@dvt int,
	@PrintType nvarchar(20)
as
begin

select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt
		when @DataType = 2 then (a.fHienVat / @dvt) end as DuToanSoBaoCao,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt
		when @DataType = 2 then (a.fHienVat)/@dvt end as DuToanSoXetDuyet,
	0 as QuyetToanSoBaoCao,
	0 as QuyetToanSoXetDuyet,
	0 as XetDuyetDuToanConDuChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi
	inner join NS_DT_ChungTu ct on a.iID_DTChungTu = ct.iID_DTChungTu
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOrBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		and ct.bKhoa in (select * from f_split(@PrintType))

	union all

	select
		a.iID_MLNS,
		a.iID_MLNS_Cha as MLNS_Id_Parent,
		a.sLNS as LNS,
		a.sL as L,
		a.sK as K,
		a.sM as M,
		a.sTM as TM,
		a.sTTM as TTM,
		a.sNG as NG,
		a.sTNG1 as TNG1,
		a.sTNG2 as TNG2,
		a.sTNG3 as TNG3,
		a.sXauNoiMa as XauNoiMa,
		a.sMoTa as MoTa,
		cast (0 as bit) as IsHangCha,
		0 as DuToanSoBaoCao,
		0 as DuToanSoXetDuyet,
		case when @DataType = 1 then a.fTuChi_DeNghi / @dvt
		when @DataType = 2 then 0 end as QuyetToanSoBoCao,
		fTuChi_PheDuyet / @dvt as QuyetToanSoXetDuyet,
		(ISNULL(a.fChuyenNamSau_DaCap,0) + ISNULL(a.fChuyenNamSau_ChuaCap,0)) / @dvt as XetDuyetDuToanConDuChuyenNamSau,
		a.iID_MaDonVi as IdDonVi,
		b.sTenDonVi as TenDonVi
	from NS_QT_ChungTuChiTiet a
		inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi
		inner join NS_QT_ChungTu ct on a.iID_QTChungTu = ct.iID_QTChungTu
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOrBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		and ct.bKhoa in (select * from f_split(@PrintType))
		
end
GO
