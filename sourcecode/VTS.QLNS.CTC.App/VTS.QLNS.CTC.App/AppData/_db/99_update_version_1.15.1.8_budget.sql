/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]    Script Date: 11/28/2024 10:18:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_nhan_quyettoankinhphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]    Script Date: 11/28/2024 10:18:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_nhan_quyettoankinhphi_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]    Script Date: 11/28/2024 6:00:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 11/28/2024 6:00:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 11/28/2024 6:00:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 11/28/2024 6:00:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]    Script Date: 11/28/2024 10:18:45 AM ******/
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
	where map.iNamLamViec = @YearOfWork
	and (isnull(@LNS, '') <> '' and mucluc.sMa in (select * from f_split(@LNS)))
	--and mucluc.iTrangThai = 1
),

donViDuToan as (
	select distinct donvi.* from DonVi donvi
	join NS_DT_ChungTuChiTiet chitiet on chitiet.iID_MaDonVi = donvi.iID_MaDonVi
	and chitiet.iNamLamViec = donvi.iNamLamViec
	where chitiet.iNamLamViec = @YearOfWork and chitiet.sXauNoiMa in (select * from xauNoiMa)
	and iNamNganSach in (select * from f_split(@YearOfBudget))
	and iID_MaNguonNganSach = @BudgetSource
),


donViQuyetToan as (
	select distinct donvi.* from DonVi donvi
	join NS_QT_ChungTuChiTiet chitiet on chitiet.iID_MaDonVi = donvi.iID_MaDonVi
	and chitiet.iNamLamViec = donvi.iNamLamViec
	where chitiet.iNamLamViec = @YearOfWork and chitiet.sXauNoiMa in (select * from xauNoiMa)
	and iNamNganSach in (select * from f_split(@YearOfBudget))
	and iID_MaNguonNganSach = @BudgetSource
),


donViCapPhat as (
	select distinct donvi.* from DonVi donvi
	join NS_CP_ChungTuChiTiet chitiet on chitiet.iID_MaDonVi = donvi.iID_MaDonVi
	and chitiet.iNamLamViec = donvi.iNamLamViec
	where chitiet.iNamLamViec = @YearOfWork and chitiet.sXauNoiMa in (select * from xauNoiMa)
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_nhan_quyettoankinhphi]    Script Date: 11/28/2024 10:18:45 AM ******/
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


/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 11/28/2024 6:00:47 PM ******/
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
		ISNULL(a.fDeNghi_ChuyenNamSau,0) / @dvt as XetDuyetDuToanConDuChuyenNamSau,
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 11/28/2024 6:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
	@YearOfWork int,
	@YearOfBudget nvarchar(20),
	@ListLNS nvarchar(MAX),
	@ListUnitId nvarchar(200),
	@DataType int,
	@BudgetSource int,
	@dvt int,
	@PrintType nvarchar(MAX)
as
begin

--Dự toán 
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
	-- dự toán năm trước chuyển sang
	case when (@DataType = 1 and a.iNamNganSach in (1,4)) then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt
		when (@DataType = 2 and a.iNamNganSach in (1,4)) then a.fHienVat / @dvt else 0 end as DuToanNamTruocChuyenSang,
	-- dự toán tổng số
	case when (@DataType = 1 and a.iNamNganSach = 2 and ct.iLoaiDuToan in (1,3,4,5)) then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt 
		when (@DataType = 2 and a.iNamNganSach = 2 and ct.iLoaiDuToan in (1,3,4,5)) then fHienVat / @dvt else 0 end as DuToanTongSo,
	-- dự toán bổ sung sau
	case when (@DataType = 1 and a.iNamNganSach = 2 and ct.iLoaiDuToan = 5) then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt 
		when (@DataType = 2 and a.iNamNganSach = 2 and ct.iLoaiDuToan = 5) then fHienVat / @dvt else 0 end as DuToanBoSungSau,

	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi,
	a.iNamNganSach as NamNganSach
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	inner join NS_DT_ChungTu ct on ct.iID_DTChungTu = a.iID_DTChungTu
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		and ct.bKhoa in (select * from f_split(@PrintType))

-- số đề nghị quyết toán năm & số chuyển năm sau
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
		0 as DuToanNamTruocChuyenSang,
		0 as DuToanTongSo,
		0 as DuToanBoSungSau,
		-- thiếu cột fHienVat trong bảng NS_QT_ChungTuChiTiet
		case when @DataType = 1 then a.fTuChi_PheDuyet when @DataType = 2 then 0 end as SoDeNghiQuyetToanNam,
		ISNULL(fDeNghi_ChuyenNamSau,0) / @dvt as DuToanChuyenNamSau,
		a.iID_MaDonVi as IdDonVi,
		b.sTenDonVi as TenDonVi,
		a.iNamNganSach as NamNganSach
	from NS_QT_ChungTuChiTiet a
		inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi
		inner join NS_QT_ChungTu ct on a.iID_QTChungTu = ct.iID_QTChungTu
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOfBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		and ct.bKhoa in (select * from f_split(@PrintType))
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 11/28/2024 6:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
	-- Add the parameters for the stored procedure here
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type nvarchar(10),
	@AgencyId nvarchar(100),
	@VoucherDate datetime,
	@UserName nvarchar(100),
	@QuarterMonth nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @YearOfBudgetCurrent int = 2,
		@YearOfBudgetAfter int = 4;
	Declare @voucherIndex int,
	@IsLocked bit,
	@STongHop nvarchar(500),
	@ILanDieuChinh int,
	@ILoaiChungTu int;
	SELECT @voucherIndex = iSoChungTuIndex, @IsLocked = bKhoa, @STongHop = sTongHop, @ILoaiChungTu = iLoaiChungTu, @ILanDieuChinh = iLanDieuChinh FROM NS_QT_ChungTu WHERE iID_QTChungTu = @VoucherId;

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT * INTO #temp FROM f_split(@LNS);

	SELECT 
			sXauNoiMa, iNamNganSach,iID_MaNguonNganSach,iID_QTCTChiTiet,sNguoiSua,sNguoiTao,dNgaySua,dNgayTao,
			sGhiChu,fTuChi_PheDuyet,fTuChi_DeNghi,fSoLuot,fSoNgay,fSoNguoi,iID_MaDonVi,iThangQuy,iThangQuyLoai,
			iNamLamViec,fDeNghi_ChuyenNamSau,iID_QTChungTu, fChuyenNamSau_DaCap
		INTO #temp2
		 FROM 
			NS_QT_ChungTuChiTiet 
		 WHERE 
			iID_QTChungTu = @VoucherId
			AND iNamLamViec = @YearOfWork
			AND (@YearOfBudget = @YearOfBudgetCurrent and (iNamNganSach = @YearOfBudgetCurrent or iNamNganSach = @YearOfBudgetAfter) or (iNamNganSach = @YearOfBudget))
			AND iID_MaNguonNganSach = @BudgetSource
	select 
		(case sLNS1
			when '1040200' then (SUM(fHangNhap) + SUM(fTuChi))
			when '1040300' then (SUM(fHangMua) + SUM(fTuChi))
			else SUM(fTuChi)
		end) as DuToan,
		iID_MLNS1 as iID_MLNS,
		@AgencyId as iID_MaDonVi,
		sXauNoiMa
		INTO #temp3
		from 
		(
			select 
				case 
					when ctct.sLNS = '1040100' then mlns.sLNS
					else ctct.sLNS
					end 
				as sLNS1,
				case 
					when ctct.sLNS = '1040100' then mlns.iID_MLNS
					else ctct.iID_MLNS
					end
				as iID_MLNS1,
				ctct.*
			from 
			(
				SELECT
				*
				FROM 
					NS_DT_ChungTuChiTiet
					WHERE
					iID_DTChungTu 
					IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu 
						where 
							((ISNULL(@STongHop, '') = '' AND sDSID_MaDonVi like '%' + @AgencyId + '%') OR (ISNULL(@STongHop, '') <> '' AND sDSID_MaDonVi = @AgencyId))
							AND (iLoai = 1 or iLoai = 0)
							AND iNamLamViec = @YearOfWork
							AND iNamNganSach = @YearOfBudget
							AND iID_MaNguonNganSach = @BudgetSource
							AND bKhoa = 1
							AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate as date)
							AND sSoQuyetDinh is not null AND sSoQuyetDinh <> ''
					)
					and iID_MaDonVi = @AgencyId
					and IDuLieuNhan = 0) ctct
				left join 
					(select '1040100' as sLNS1, * from NS_MucLucNganSach where sLNS in ('1040100', '1040200', '1040300') and iNamLamViec = @YearOfWork) mlns
				on mlns.sLNS1 = ctct.sLNS and mlns.sL = ctct.sL and mlns.sK = ctct.sK and mlns.sM = ctct.sM and mlns.sTM = ctct.sTM and mlns.sNG = ctct.sNG and mlns.sTTM = ctct.sTTM
			) dt
			GROUP BY iID_MLNS1, sLNS1, sXauNoiMa
	
	SELECT
			SUM(fTuChi_PheDuyet) AS DaQuyetToan,
			sXauNoiMa
			INTO #temp4
		 FROM 
			NS_QT_ChungTuChiTiet
		 WHERE
			iID_QTChungTu 
			IN (SELECT iID_QTChungTu FROM NS_QT_ChungTu 
				where 
					iID_MaDonVi = @AgencyId
					AND sLoai = @Type
					AND iNamLamViec = @YearOfWork
					AND (@YearOfBudget = @YearOfBudgetCurrent and (iNamNganSach = @YearOfBudgetCurrent or iNamNganSach = @YearOfBudgetAfter) or (iNamNganSach = @YearOfBudget))
					AND iID_MaNguonNganSach = @BudgetSource
					AND ((iThangQuy < @QuarterMonth) OR
					((iThangQuy = @QuarterMonth AND @ILoaiChungTu = 2 AND ISNULL(iLanDieuChinh, 0) < @ILanDieuChinh) 
					OR (iThangQuy = @QuarterMonth AND ISNULL(@ILoaiChungTu, 1) = 1 AND ISNULL(iLoaiChungTu, 1) = 1)))
					--AND cast(dNgayChungTu as date) <= cast(@voucherDate as Date)
					AND iID_QTChungTu <> @VoucherId
			)
		 GROUP BY sXauNoiMa

	SELECT 
		isnull(ctct.iID_QTCTChiTiet, NEWID()) AS iID_QTCTChiTiet,
		ctct.iID_QTChungTu,
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sTNG1,
		mlns.sTNG2,
		mlns.sTNG3,
		mlns.sMoTa,
		mlns.fTienAn,
		mlns.bHangCha,
		isnull(ctct.iNamNganSach, @YearOfBudget) as iNamNganSach,
		isnull(ctct.iID_MaNguonNganSach, @BudgetSource) as iID_MaNguonNganSach,
		isnull(ctct.iNamLamViec, @YearOfWork) as iNamLamViec,
		isnull(ctct.iThangQuyLoai, 0) as iThangQuyLoai,
		isnull(ctct.iThangQuy, 0) as iThangQuy,
		isnull(ctct.iID_MaDonVi, dtctct.iID_MaDonVi) as iID_MaDonVi,
		--ctct.iID_MaDonVi,
		sTenDonVi = case when @ILoaiChungTu = 2 then dv.sTenDonVi + N' (Điều chỉnh)' else dv.sTenDonVi end,
		isnull(ctct.fSoNguoi, 0) as fSoNguoi,
		isnull(ctct.fSoNgay, 0) as fSoNgay,
		isNull(ctct.fSoLuot, 0) as fSoLuot,
		isnull(ctct.fTuChi_DeNghi, 0) as fTuChi_DeNghi,
		isnull(ctct.fTuChi_PheDuyet, 0) as fTuChi_PheDuyet,
		ctct.sGhiChu,
		ctct.dNgayTao,
		ctct.dNgaySua,
		ctct.sNguoiTao,
		ctct.sNguoiSua,
		fDuToan = case when @ILoaiChungTu = 2 then 0 else dtctct.DuToan end,
		fDaQuyetToan = case when @ILoaiChungTu = 2 then 0 else ctctdqt.DaQuyetToan end,
		mlns.sChiTietToi,
		mlns.bHangChaQuyetToan,
		mlns.sMaCB,
		ctct.fDeNghi_ChuyenNamSau,
		ctct.fChuyenNamSau_DaCap
	FROM 
	(
		select sChiTietToi, bHangChaDuToan, bHangChaQuyetToan, sMaCB,iID_MLNS,
		iID_MLNS_Cha,
		sXauNoiMa,
		sLNS,
		sL,
		sK,
		sM,
		sTM,
		sTTM,
		sNG,
		sTNG,
		sTNG1,
		sTNG2,
		fTienAn,
		iTrangThai,
		iNamLamViec,
		sTNG3,
		sMoTa,
		bHangCha from NS_MucLucNganSach where iNamLamViec = @YearOfWork and iTrangThai = 1 and bHangChaQuyetToan is not null and
		(
			(@CountDonViCha > 0 and (@IsLocked = 1 or @STongHop is not null) and sLNS in (select * from #temp))
			or
			(
				sLNS in 
				(
					SELECT 
						DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(sLNS, 1) AS nvarchar(10)) sLNS1, 
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) sLNS3, 
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) sLNS5, 
							CAST(sLNS AS nvarchar(10)) sLNS 
						FROM
							NS_NguoiDung_LNS 
						WHERE 
							sMaNguoiDung = @UserName 
							AND iNamLamViec = @YearOfWork
							AND sLNS IN (SELECT * FROM #temp)
					) sLNS
					UNPIVOT
					(
					  value
					  FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
					) un
				)
			)
		)
	) mlns
	LEFT JOIN #temp2 ctct
	ON mlns.sXauNoiMa = ctct.sXauNoiMa 
	LEFT JOIN #temp3 dtctct
	on mlns.sXauNoiMa = dtctct.sXauNoiMa
	LEFT JOIN #temp4 ctctdqt
	ON mlns.sXauNoiMa = ctctdqt.sXauNoiMa
	LEFT JOIN
		(SELECT sTenDonVi, iID_MaDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi OR dv.iID_MaDonVi = dtctct.iID_MaDonVi
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
	Order by mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3
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
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]    Script Date: 11/28/2024 6:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfBudget int,
	@BudgetSource int,
	@YearOfWork int,
	@Type nvarchar(10),
	@QuarterMonthType int,
	@QuarterMonth int,
	@AgencyId nvarchar(10),
	@UserName nvarchar(100)
AS
BEGIN
	INSERT INTO [dbo].[NS_QT_ChungTuChiTiet]
           ([iID_QTChungTu]
           ,[iID_MLNS]
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
		   ,[sTNG1]
		   ,[sTNG2]
		   ,[sTNG3]
           ,[bHangCha]
           ,[iNamNganSach]
           ,[iID_MaNguonNganSach]
           ,[iNamLamViec]
           ,[iThangQuyLoai]
           ,[iThangQuy]
           ,[iID_MaDonVi]
           ,[fSoNguoi]
           ,[fSoNgay]
           ,[fSoLuot]
		   ,[fTuChi_DeNghi]
		   ,[fTuChi_PheDuyet]
		   ,[fDeNghi_ChuyenNamSau]
		   ,[fChuyenNamSau_DaCap]
           ,[sGhiChu]
           ,[dNgayTao]
           ,[sNguoiTao]
           ,[dNgaySua]
           ,[sNguoiSua])
    SELECT 
			@VoucherId
           ,mlns.iID_MLNS
           ,mlns.iID_MLNS_Cha
           ,mlns.sXauNoiMa
           ,mlns.sLNS
           ,mlns.sL
           ,mlns.sK
           ,mlns.sM
           ,mlns.sTM
           ,mlns.sTTM
           ,mlns.sNG
           ,mlns.sTNG
		   ,mlns.sTNG1
		   ,mlns.sTNG2
		   ,mlns.sTNG3
           ,mlns.bHangCha
           ,@YearOfBudget
           ,@BudgetSource
           ,@YearOfWork
           ,@QuarterMonthType
           ,@QuarterMonth
           ,@AgencyId
           ,Sum(isnull(fSoNguoi, 0))
           ,Sum(isnull(fSoNgay, 0))
           ,Sum(isnull(fSoLuot, 0))
		   ,Sum(isnull(fTuChi_DeNghi, 0))
		   ,Sum(isnull(fTuChi_PheDuyet, 0))
		   ,Sum(isnull(fDeNghi_ChuyenNamSau, 0))
		   ,Sum(isnull(fChuyenNamSau_DaCap, 0))
           ,null
           ,GetDate()
           ,@UserName
           ,null
           ,null
	FROM NS_QT_ChungTuChiTiet ctct
	INNER JOIN NS_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa AND mlns.iNamLamViec = ctct.iNamLamViec
	INNER JOIN NS_QT_ChungTu ct ON ctct.iID_QTChungTu = ct.iID_QTChungTu
	WHERE ct.iID_QTChungTu IN (SELECT * FROM f_split(@VoucherIds)) 
	GROUP BY mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3, mlns.bHangCha;

	-- Danh dau chung tu da tong hop
	UPDATE NS_QT_ChungTu SET bDaTongHop = 1 
	WHERE iID_QTChungTu in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
;
GO


IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_Nhan_QuyetToan_KinhPhi')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_Nhan_QuyetToan_KinhPhi', NULL, N'rptNS_Nhan_QuyetToan_KinhPhi', NULL, 
NULL, NULL, NULL, NULL, N'QUYET_TOAN', NULL, 
N'Báo cáo tình hình nhận và quyết toán kinh phí', NULL, NULL, 
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 
N'1', N'Báo cáo tình hình nhận và quyết toán kinh phí', NULL, NULL, NULL, NULL, NULL, 1, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
ELSE
UPDATE DM_ChuKy
set TieuDe1_MoTa = N'Báo cáo tình hình nhận và quyết toán kinh phí'
where Id_Type = 'rptNS_Nhan_QuyetToan_KinhPhi'
GO