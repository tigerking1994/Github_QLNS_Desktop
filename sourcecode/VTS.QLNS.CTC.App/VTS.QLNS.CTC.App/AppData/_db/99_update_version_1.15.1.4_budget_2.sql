/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]    Script Date: 11/21/2024 9:43:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_nhan_quyettoankinhphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]    Script Date: 11/21/2024 9:43:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]
@YearOfWork int,
@AgencyId nvarchar(max),
@MaString nvarchar(max),
@YearOfBudget int,
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
and iNamNganSach = @YearOfBudget
group by sXauNoiMa

select sum(isnull(fTuChi, 0)) fTuChi, sXauNoiMa 
into #tempCapPhat from NS_CP_ChungTuChiTiet
where iNamLamViec = @YearOfWork
and iNamNganSach = @YearOfBudget
group by sXauNoiMa

select 
	sum(isnull(fTuChi_DeNghi, 0)) fTuChi_DeNghi, 
	sum(isnull(fChuyenNamSau_DaCap, 0)) fChuyenNamSau_DaCap, 
	sum(isnull(fChuyenNamSau_ChuaCap, 0)) fChuyenNamSau_ChuaCap,
	sXauNoiMa 
into #tempQuyetToan from NS_QT_ChungTuChiTiet
where iNamLamViec = @YearOfWork
and iNamNganSach = @YearOfBudget
group by sXauNoiMa;

with map as (
	select * from NS_MucLucQuyetToanNam_MLNS
	where iNamLamViec = @YearOfWork
)

select 
	mlqt.sSTT,
	mlqt.sMoTa,
	mlqt.sMa,
	mlqt.sMaCha,
	mlqt.bHangCha,
	sum(dutoan.fTuChi) / @Dvt DuToanNganSach, 
	sum(capphat.fTuChi) / @Dvt KinhPhiDuocCap,
	sum(quyettoan.fTuChi_DeNghi) / @Dvt KinhPhiDeNghi,
	sum(quyettoan.fChuyenNamSau_DaCap) / @Dvt ChuyenNamSau,
	sum(quyettoan.fChuyenNamSau_ChuaCap) / @Dvt ChuyenNamSauChuaCap

from #tempMLQT mlqt
left join map on map.sMaMLQT = mlqt.sMa
left join #tempMLNS mlns on mlns.sXauNoiMa = map.sXauNoiMa
left join #tempDuToan dutoan on dutoan.sXauNoiMa = mlns.sXauNoiMa
left join #tempCapPhat capphat on capphat.sXauNoiMa = mlns.sXauNoiMa
left join #tempQuyetToan quyettoan on quyettoan.sXauNoiMa = mlns.sXauNoiMa

group by mlqt.sSTT, mlqt.sMoTa, mlqt.sMa, mlqt.sMaCha, mlqt.bHangCha

order by mlqt.sMa

drop table #tempMLQT
drop table #tempMLNS
drop table #tempDuToan
drop table #tempCapPhat
drop table #tempQuyetToan

END
GO
