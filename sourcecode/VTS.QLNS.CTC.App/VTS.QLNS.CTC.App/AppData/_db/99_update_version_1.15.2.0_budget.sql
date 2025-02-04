/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_congkhai_thuchi_donvi]    Script Date: 12/5/2024 10:34:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qt_congkhai_thuchi_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qt_congkhai_thuchi_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_congkhai_thuchi]    Script Date: 12/5/2024 10:34:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qt_congkhai_thuchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qt_congkhai_thuchi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_congkhai_thuchi]    Script Date: 12/5/2024 10:34:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qt_congkhai_thuchi]
	@MaMucLucs nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Time int
AS
BEGIN

with 
DonViCha AS
(
	select iID_MaDonVi from DonVi 
	where iLoai = 0
	and iNamLamViec = @YearOfWork
	and iTrangThai = 1
),
MucLucCongKhaiCTE AS
(
	select *, case sMa when '01' then 1
			  else 0 end as isThu
	from NS_DanhMucCongKhai 
	where isnull(sMaCha, '') = '' and iNamLamViec = @YearOfWork
	union all
	select child.*, parent.isThu
	from NS_DanhMucCongKhai child
	JOIN MucLucCongKhaiCTE parent on parent.sMa = child.sMaCha
	where child.iNamLamViec = @YearOfWork
),
MucLucCongKhaiMap as 
(
	select * from NS_DMCongKhai_MLNS
	where iNamLamViec = @YearOfWork
),
MucLucNganSach as 
(
	select * from NS_MucLucNganSach
	where iNamLamViec = @YearOfWork
),
ChiTietDuToan as 
(
	select * from NS_DT_ChungTuChiTiet
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iID_MaNguonNganSach = @BudgetSource
),
ChiTietQuyetToan as 
(
	select * from NS_QT_ChungTuChiTiet
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iID_MaNguonNganSach = @BudgetSource
),
ChiTietDuToanThuNop as 
(
	select * from TN_DT_ChungTuChiTiet
	where NamLamViec = @YearOfWork
	and NamNganSach = @YearOfBudget
	and NguonNganSach = @BudgetSource
),
ChiTietQuyetToanThuNop as 
(
	select * from TN_QuyetToan_ChungTuChiTiet_HD4554
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iNguonNganSach = @BudgetSource
)

select 
mucluc.STT,
mucluc.sMoTa NoiDung,
mucluc.Id Id,
mucluc.iID_DMCongKhai_Cha ParentId,
mucluc.bHangCha IsHangCha,
mucluc.sMa MaMucLuc,
mucluc.sMaCha MaMucLucCha,
chitiet.DuToanDuocGiao, 
chitiet.SoBaoCaoQuyetToan,
chitiet.SoQuyetToanDuocDuyet 
from MucLucCongKhaiCTE mucluc

left join

(select 
mlck.Id,
(sum(isnull(dt.ftuchi, 0)) + sum(isnull(dt.fHangNhap, 0)) + sum(isnull(dt.fHangMua, 0)) + sum(isnull(dttn.TuChi, 0))) DuToanDuocGiao,
(sum(isnull(qt.fTuChi_DeNghi, 0)) + sum(isnull(qttn.fSoTien_DeNghi, 0))) SoBaoCaoQuyetToan,
(sum(isnull(qt.fTuChi_PheDuyet, 0)) + sum(isnull(qttn.fSoTien, 0))) SoQuyetToanDuocDuyet
from MucLucCongKhaiCTE mlck
left join MucLucCongKhaiMap map on map.iID_DMCongKhai = mlck.Id
left join 
(
select sXauNoiMa, sum(isnull(fTuChi, 0)) fTuChi, sum(isnull(fHangNhap, 0)) fHangNhap, sum(isnull(fHangMua, 0)) fHangMua from ChiTietDuToan
where iPhanCap = 0 and iID_MaDonVi in (select * from DonViCha)
group by sXauNoiMa
) dt on (dt.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 0)
left join 
(
select XauNoiMa, sum(isnull(TuChi, 0)) TuChi from ChiTietDuToanThuNop
where iPhanCap = 0 and Id_DonVi in (select * from DonViCha)
group by XauNoiMa
) dttn on (dttn.XauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 1)
left join 
(
select sXauNoiMa, sum(isnull(fTuChi_DeNghi, 0)) fTuChi_DeNghi, sum(isnull(fTuChi_PheDuyet, 0)) fTuChi_PheDuyet from ChiTietQuyetToan
where iID_MaDonVi in (select * from DonViCha)
group by sXauNoiMa
) qt on (qt.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 0)
left join
(
select sXauNoiMa, sum(isnull(fSoTien, 0)) fSoTien, sum(isnull(fSoTien_DeNghi, 0)) fSoTien_DeNghi from ChiTietQuyetToanThuNop
where iID_MaDonVi in (select * from DonViCha)
group by sXauNoiMa
) qttn on (qttn.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 1)
where isnull(@MaMucLucs, '') = '' or mlck.sMa in (select * from f_split(@MaMucLucs))
group by mlck.Id
) chitiet on chitiet.Id = mucluc.Id
order by sMa

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_congkhai_thuchi_donvi]    Script Date: 12/5/2024 10:34:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qt_congkhai_thuchi_donvi]
	@MaMucLucs nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@MaDonVis nvarchar(max)
AS
BEGIN
	

with 
MucLucCongKhaiCTE AS
(
	select *, case sMa when '01' then 1
			  else 0 end as isThu
	from NS_DanhMucCongKhai 
	where isnull(sMaCha, '') = '' and iNamLamViec = @YearOfWork
	union all
	select child.*, parent.isThu
	from NS_DanhMucCongKhai child
	JOIN MucLucCongKhaiCTE parent on parent.sMa = child.sMaCha
	where child.iNamLamViec = @YearOfWork
),
MucLucCongKhaiMap as 
(
	select * from NS_DMCongKhai_MLNS
	where iNamLamViec = @YearOfWork
),
MucLucNganSach as 
(
	select * from NS_MucLucNganSach
	where iNamLamViec = @YearOfWork
),
ChiTietDuToan as 
(
	select * from NS_DT_ChungTuChiTiet
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iID_MaNguonNganSach = @BudgetSource
),
ChiTietQuyetToan as 
(
	select * from NS_QT_ChungTuChiTiet
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iID_MaNguonNganSach = @BudgetSource
),
ChiTietDuToanThuNop as 
(
	select * from TN_DT_ChungTuChiTiet
	where NamLamViec = @YearOfWork
	and NamNganSach = @YearOfBudget
	and NguonNganSach = @BudgetSource
),
ChiTietQuyetToanThuNop as 
(
	select * from TN_QuyetToan_ChungTuChiTiet_HD4554
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iNguonNganSach = @BudgetSource
)

(select 
mlck.Id,
isnull(qt.iID_MaDonVi, qttn.iID_MaDonVi) MaDonVi,
(sum(isnull(qt.fTuChi_PheDuyet, 0)) + sum(isnull(qttn.fSoTien, 0))) SoTien
from MucLucCongKhaiCTE mlck
left join MucLucCongKhaiMap map on map.iID_DMCongKhai = mlck.Id
left join 
(
select sXauNoiMa, iID_MaDonVi, sum(isnull(fTuChi_PheDuyet, 0)) fTuChi_PheDuyet from ChiTietQuyetToan
where isnull(@MaDonVis, '') = '' or iID_MaDonVi in (select * from f_split(@MaDonVis))
group by sXauNoiMa, iID_MaDonVi
) qt on (qt.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 0)
left join
(
select sXauNoiMa, iID_MaDonVi, sum(isnull(fSoTien, 0)) fSoTien from ChiTietQuyetToanThuNop
where isnull(@MaDonVis, '') = '' or iID_MaDonVi in (select * from f_split(@MaDonVis))
group by sXauNoiMa, iID_MaDonVi
) qttn on (qttn.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 1)
where (coalesce(qt.iID_MaDonVi, qttn.iID_MaDonVi, '') <> '')
and (isnull(@MaMucLucs, '') = '' or mlck.sMa in (select * from f_split(@MaMucLucs)))
group by mlck.Id, mlck.sMa, qt.iID_MaDonVi, qttn.iID_MaDonVi) 

END
;
GO
