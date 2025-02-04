/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]    Script Date: 11/26/2024 6:18:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_nhan_quyettoankinhphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]    Script Date: 11/26/2024 6:18:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_nhan_quyettoankinhphi_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]    Script Date: 11/26/2024 6:18:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]
@YearOfWork int,
@YearOfBudget nvarchar(50),
@BudgetSource int,
@LNS nvarchar(max)
AS
BEGIN


with 

xauNoiMa as (
	select map.sXauNoiMa from NS_MucLucQuyetToanNam_MLNS map
	join NS_MucLucQuyetToanNam mucluc on mucluc.sMa = map.sMaMLQT
	and mucluc.iNamLamViec = map.iNamLamViec
	where map.iNamLamViec = 2024
	and (isnull(@LNS, '') = '' or mucluc.sMa in (select * from f_split(@LNS)))
	--and mucluc.iTrangThai = 1
),

donViDuToan as (
	select distinct donvi.* from DonVi donvi
	join NS_DT_ChungTuChiTiet chitiet on chitiet.iID_MaDonVi = donvi.iID_MaDonVi
	and chitiet.iNamLamViec = donvi.iNamLamViec
	where chitiet.iNamLamViec = 2024 and chitiet.sXauNoiMa in (select * from xauNoiMa)
	and iNamNganSach in (select * from f_split(@YearOfBudget))
	and iID_MaNguonNganSach = @BudgetSource
),


donViQuyetToan as (
	select distinct donvi.* from DonVi donvi
	join NS_QT_ChungTuChiTiet chitiet on chitiet.iID_MaDonVi = donvi.iID_MaDonVi
	and chitiet.iNamLamViec = donvi.iNamLamViec
	where chitiet.iNamLamViec = 2024 and chitiet.sXauNoiMa in (select * from xauNoiMa)
	and iNamNganSach in (select * from f_split(@YearOfBudget))
	and iID_MaNguonNganSach = @BudgetSource
),


donViCapPhat as (
	select distinct donvi.* from DonVi donvi
	join NS_CP_ChungTuChiTiet chitiet on chitiet.iID_MaDonVi = donvi.iID_MaDonVi
	and chitiet.iNamLamViec = donvi.iNamLamViec
	where chitiet.iNamLamViec = 2024 and chitiet.sXauNoiMa in (select * from xauNoiMa)
	and iNamNganSach in (select * from f_split(@YearOfBudget))
	and iID_MaNguonNganSach = @BudgetSource
)

select * from donViDuToan
union 
select * from donViQuyetToan
union 
select * from donviCapPhat
where iLoai = 1

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]    Script Date: 11/26/2024 6:18:17 PM ******/
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
and (isnull(@AgencyId, '') = '' or iID_MaDonVi in (select * from f_split(@AgencyId)))
group by sXauNoiMa

select sum(isnull(fTuChi, 0)) fTuChi, sXauNoiMa 
into #tempCapPhat from NS_CP_ChungTuChiTiet
where iNamLamViec = @YearOfWork
and iNamNganSach in (select * from f_split(@YearOfBudget))
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
and (isnull(@AgencyId, '') = '' or iID_MaDonVi in (select * from f_split(@AgencyId)))
group by sXauNoiMa;

with map as (
	select * from NS_MucLucQuyetToanNam_MLNS
	where iNamLamViec = @YearOfWork
	and (isnull(@MaString, '') = '' or sMaMLQT in (select * from f_split(@MaString)))

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
GO
