/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]    Script Date: 12/19/2024 10:32:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_nhan_quyettoankinhphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]    Script Date: 12/19/2024 10:32:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]
@YearOfWork int,
@AgencyId nvarchar(max),
@MaString nvarchar(max),
@YearOfBudget nvarchar(50),
@BudgetSource int,
@MaBQuanLy nvarchar(50),
@Dvt int
AS
BEGIN
select * into #tempMLQT from NS_MucLucQuyetToanNam
where iNamLamViec = @YearOfWork

select * into #tempMLNS from NS_MucLucNganSach
where iNamLamViec = @YearOfWork

select sum(isnull(fTuChi, 0)) fTuChi, sXauNoiMa 
into #tempDuToan from NS_DT_ChungTuChiTiet
where iNamLamViec = @YearOfWork
and iNamNganSach in (select * from f_split(@YearOfBudget))
and iID_MaNguonNganSach = @BudgetSource
and (isnull(@AgencyId, '') = '' or iID_MaDonVi in (select * from f_split(@AgencyId)))
group by sXauNoiMa

select sum(isnull(fTuChi, 0)) fTuChi, sXauNoiMa 
into #tempCapPhat from NS_CP_ChungTuChiTiet
where iNamLamViec = @YearOfWork
and iNamNganSach in (select * from f_split(@YearOfBudget))
and iID_MaNguonNganSach = @BudgetSource
and (isnull(@AgencyId, '') = '' or iID_MaDonVi in (select * from f_split(@AgencyId)))
group by sXauNoiMa

select 
	sum(isnull(fTuChi_DeNghi, 0)) fTuChi_DeNghi, 
	sum(isnull(fDeNghi_ChuyenNamSau, 0)) fDeNghi_ChuyenNamSau, 
	sum(isnull(fChuyenNamSau_DaCap, 0)) fChuyenNamSau_DaCap,
	sXauNoiMa 
into #tempQuyetToan from NS_QT_ChungTuChiTiet
where iNamLamViec = @YearOfWork
and iNamNganSach in (select * from f_split(@YearOfBudget))
and iID_MaNguonNganSach = @BudgetSource
and (isnull(@AgencyId, '') = '' or iID_MaDonVi in (select * from f_split(@AgencyId)))
group by sXauNoiMa;

with map as (
	select * from NS_MucLucQuyetToanNam_MLNS
	where iNamLamViec = @YearOfWork
	and (isnull(@MaString, '') = '' or sMaMLQT in (select * from f_split(@MaString)))
),

lns as (
	select distinct sLNS from #tempMLNS where isnull(@MaBQuanLy, '') = '' or iID_MaBQuanLy = @MaBQuanLy
),

chitiet as (
	select 
	map.sMaMLQT,
	sum(isnull(dutoan.fTuChi, 0)) / @Dvt DuToanNganSach, 
	sum(isnull(capphat.fTuChi, 0)) / @Dvt KinhPhiDuocCap,
	sum(isnull(quyettoan.fTuChi_DeNghi, 0)) / @Dvt KinhPhiDeNghi,
	sum(isnull(quyettoan.fDeNghi_ChuyenNamSau, 0)) / @Dvt ChuyenNamSauTongSo,
	sum(isnull(quyettoan.fChuyenNamSau_DaCap, 0)) / @Dvt ChuyenNamSauDaCap
	from map 
	left join #tempMLNS mlns on mlns.sXauNoiMa = map.sXauNoiMa
	left join #tempDuToan dutoan on dutoan.sXauNoiMa = mlns.sXauNoiMa
	left join #tempCapPhat capphat on capphat.sXauNoiMa = mlns.sXauNoiMa
	left join #tempQuyetToan quyettoan on quyettoan.sXauNoiMa = mlns.sXauNoiMa
	where mlns.sLNS in (select * from lns)
	group by map.sMaMLQT
)

select 
	mlqt.sSTT,
	mlqt.sMoTa,
	mlqt.sMa,
	mlqt.sMaCha,
	mlqt.bHangCha,
	chitiet.*

from #tempMLQT mlqt
left join chitiet on chitiet.sMaMLQT = mlqt.sMa


order by mlqt.sMa

drop table #tempMLQT
drop table #tempMLNS
drop table #tempDuToan
drop table #tempCapPhat
drop table #tempQuyetToan

END
;
;
GO
