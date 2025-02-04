/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_cua_chungtu_phanbo]    Script Date: 12/10/2024 9:47:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_cua_chungtu_phanbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_cua_chungtu_phanbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 12/10/2024 9:47:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 12/10/2024 9:47:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
	@VoucherId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@AgencyId NVARCHAR(MAX),
	@YearOfWork INT,
	@BudgetOfYear INT,
	@SourceOfBudget INT,
	@UserName NVARCHAR(100),
	@IsGetAll BIT
AS
BEGIN

declare @CountRoot int;
declare @NgayQuyetDinh datetime;
declare @NgayChungTu datetime;
declare @SoChungTuIndex int;

select 
@NgayQuyetDinh = cast(dNgayQuyetdinh as date),
@NgayChungTu = cast(dNgayChungTu as date),
@SoChungTuIndex = iSoChungTuIndex
from NS_DT_ChungTu 
where iID_DTChungTu = @VoucherId;

select @CountRoot = count(*) from DonVi dv
join NguoiDung_DonVi nd 
on nd.iID_MaDonVi = dv.iID_MaDonVi and nd.iNamLamViec = dv.iNamLamViec
where dv.iNamLamViec = @YearOfWork
and iID_MaNguoiDung = @UserName;

with lns1 as 
(select distinct value
from
(select cast(left(Item, 1) as nvarchar(10)) LNS1,
		cast(left(Item, 3) as nvarchar(10)) LNS3,
		cast(left(Item, 5) as nvarchar(10)) LNS5,
		cast(Item as nvarchar(10)) LNS
from f_split(@LNS)) 
LNS unpivot (value for col in (LNS1, LNS3, LNS5, LNS)) un
),
lns2 as 
(select distinct value
from
(select cast(left(sLNS, 1) as nvarchar(10)) LNS1,
		cast(left(sLNS, 3) as nvarchar(10)) LNS3,
		cast(left(sLNS, 5) as nvarchar(10)) LNS5,
		cast(sLNS as nvarchar(10)) LNS
from NS_NguoiDung_LNS
where sMaNguoiDung = @UserName
AND iNamLamViec = @YearOfWork
AND sLNS IN (SELECT * FROM f_split('')))
LNS unpivot (value for col in (LNS1, LNS3, LNS5, LNS)) un
)

select * into #muclucngansachs from NS_MucLucNganSach
where iNamLamViec = @YearOfWork
and iTrangThai = 1
and bHangChaDuToan is not null
and ((@CountRoot <> 0 and sLNS in (select * from lns1)) or (@CountRoot = 0 and sLNS in (select * from lns2)));

with 

settings as
(
select sLNS, bTuChi, bHienVat, bHangNhap, bHangMua, bPhanCap, bDuPhong, bTonKho from #muclucngansachs where isnull(sL, '') = ''
),

iddonvis as
(
select * from f_split(@AgencyId)
),

donvis as 
(
select dv.iID_MaDonVi, dv.sTenDonVi from DonVi dv
join NguoiDung_DonVi nd on nd.iID_MaDonVi = dv.iID_MaDonVi and nd.iNamLamViec = dv.iNamLamViec
where dv.iNamLamViec = @YearOfWork
and dv.iID_MaDonVi in (select * from iddonvis)
),

xaunoimas as
(
select distinct sXauNoiMa from #muclucngansachs where bHangChaDuToan = 0 and @IsGetAll = 1
union all
select distinct sXauNoiMa from NS_DT_ChungTuChiTiet where iID_DTChungTu in (select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId)
union all 
select distinct sXauNoiMa from NS_DT_ChungTuChiTiet where iID_DTChungTu  = @VoucherId
),

soquyetdinh as
(
select iiD_DTChungTu, sSoQuyetDinh from NS_DT_ChungTu where 
iID_DTChungTu in (select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId)
),

idnhanphanbo as
(
select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId
),

mucluc_sqd as (
select * from xaunoimas, idnhanphanbo
),

mucluc_sqd_donvi as (
select * from xaunoimas, idnhanphanbo, iddonvis
),

nhanphanbo as
(
select sXauNoiMa, iID_DTChungTu, 
sum(isnull(fTuChi, 0)) fTuChi,
sum(isnull(fRutKBNN, 0)) fRutKBNN,
sum(isnull(fHienVat, 0)) fHienVat,
sum(isnull(fDuPhong, 0)) fDuPhong,
sum(isnull(fHangMua, 0)) fHangMua,
sum(isnull(fHangNhap, 0)) fHangNhap,
sum(isnull(fPhanCap, 0)) fPhanCap,
sum(isnull(fTonKho, 0)) fTonKho
from NS_DT_ChungTuChiTiet where iID_DTChungTu in (select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId) 
group by sXauNoiMa, iID_DTChungTu
),

daphanbo as
(
select sXauNoiMa, iID_CTDuToan_Nhan, 
0 - sum(isnull(fTuChi, 0)) fTuChi,
0 - sum(isnull(fRutKBNN, 0)) fRutKBNN,
0 - sum(isnull(fHienVat, 0)) fHienVat,
0 - sum(isnull(fDuPhong, 0)) fDuPhong,
0 - sum(isnull(fHangMua, 0)) fHangMua,
0 - sum(isnull(fHangNhap, 0)) fHangNhap,
0 - sum(isnull(fPhanCap, 0)) fPhanCap,
0 - sum(isnull(fTonKho, 0)) fTonKho 
from NS_DT_ChungTuChiTiet ctct
left join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_CTDuToan_Nhan
left join NS_DT_ChungTu dpb on dpb.iID_DTChungTu = ctct.iID_DTChungTu
where iID_CTDuToan_Nhan in (select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId) 
and 
(
	(
		(
		cast(isnull(dpb.dNgayQuyetDinh, dpb.dNgayChungTu) as date)
		= cast(isnull(@NgayQuyetDinh, @NgayChungTu) as date)
		) and (dpb.iSoChungTuIndex < @SoChungTuIndex)
	) or 
	(
		cast(isnull(dpb.dNgayQuyetDinh, dpb.dNgayChungTu) as date) 
		< cast(isnull(@NgayQuyetDinh, @NgayChungTu) as date)
	)
)
group by sXauNoiMa, iID_CTDuToan_Nhan
),

nhanphanbo_current as
(
select sXauNoiMa, iID_DTChungTu, 
sum(isnull(fTuChi, 0)) fTuChi,
sum(isnull(fRutKBNN, 0)) fRutKBNN,
sum(isnull(fHienVat, 0)) fHienVat,
sum(isnull(fDuPhong, 0)) fDuPhong,
sum(isnull(fHangMua, 0)) fHangMua,
sum(isnull(fHangNhap, 0)) fHangNhap,
sum(isnull(fPhanCap, 0)) fPhanCap,
sum(isnull(fTonKho, 0)) fTonKho
from 
(select * from nhanphanbo
union all
select * from daphanbo) npb_current
group by sXauNoiMa, iID_DTChungTu
),

phanbo as
(
select sXauNoiMa, iID_CTDuToan_Nhan, 
sum(isnull(fTuChi, 0)) fTuChi,
sum(isnull(fRutKBNN, 0)) fRutKBNN,
sum(isnull(fHienVat, 0)) fHienVat,
sum(isnull(fDuPhong, 0)) fDuPhong,
sum(isnull(fHangMua, 0)) fHangMua,
sum(isnull(fHangNhap, 0)) fHangNhap,
sum(isnull(fPhanCap, 0)) fPhanCap,
sum(isnull(fTonKho, 0)) fTonKho
from NS_DT_ChungTuChiTiet where iID_DTChungTu  = @VoucherId
group by sXauNoiMa, iID_CTDuToan_Nhan
),

phanbo_donvi as
(
select iID_DTCTChiTiet, sXauNoiMa, 
iID_CTDuToan_Nhan, iID_MaDonVi, sGhiChu,
sum(isnull(fTuChi, 0)) fTuChi,
sum(isnull(fRutKBNN, 0)) fRutKBNN,
sum(isnull(fHienVat, 0)) fHienVat,
sum(isnull(fDuPhong, 0)) fDuPhong,
sum(isnull(fHangMua, 0)) fHangMua,
sum(isnull(fHangNhap, 0)) fHangNhap,
sum(isnull(fPhanCap, 0)) fPhanCap,
sum(isnull(fTonKho, 0)) fTonKho
from NS_DT_ChungTuChiTiet where iID_DTChungTu  = @VoucherId
group by iID_DTCTChiTiet, sXauNoiMa, iID_CTDuToan_Nhan, iID_MaDonVi, sGhiChu
),

conlai_mucluc as
(
select 
iID_DTCTChiTiet = null,
iID_DTChungTu = null,
mucluc_sqd.sXauNoiMa, 
mucluc_sqd.iID_CTDuToan_Nhan,
iID_MaDonVi = null,
sTenDonVi = null,
soquyetdinh.iID_DTChungTu idSoQuyetDinh,
soquyetdinh.sSoQuyetDinh,
iRowType = 2,
sGhiChu = null,
(isnull(nhanphanbo.fTuChi, 0) - isnull(phanbo.fTuChi, 0)) fTuChi, 
(isnull(nhanphanbo.fRutKBNN, 0) - isnull(phanbo.fRutKBNN, 0)) fRutKBNN, 
(isnull(nhanphanbo.fHienVat, 0) - isnull(phanbo.fHienVat, 0)) fHienVat, 
(isnull(nhanphanbo.fDuPhong, 0) - isnull(phanbo.fDuPhong, 0)) fDuPhong, 
(isnull(nhanphanbo.fHangMua, 0) - isnull(phanbo.fHangMua, 0)) fHangMua, 
(isnull(nhanphanbo.fHangNhap, 0) - isnull(phanbo.fHangNhap, 0)) fHangNhap, 
(isnull(nhanphanbo.fPhanCap, 0) - isnull(phanbo.fPhanCap, 0)) fPhanCap, 
(isnull(nhanphanbo.fTonKho, 0) - isnull(phanbo.fTonKho, 0)) fTonKho
from mucluc_sqd
left join nhanphanbo_current nhanphanbo on nhanphanbo.sXauNoiMa = mucluc_sqd.sXauNoiMa and nhanphanbo.iID_DTChungTu = mucluc_sqd.iID_CTDuToan_Nhan
left join phanbo on phanbo.sXauNoiMa = mucluc_sqd.sXauNoiMa and phanbo.iID_CTDuToan_Nhan = mucluc_sqd.iID_CTDuToan_Nhan
left join soquyetdinh on soquyetdinh.iID_DTChungTu = mucluc_sqd.iID_CTDuToan_Nhan
),

nhanphanbo_mucluc as
(
select 
iID_DTCTChiTiet = null,
iID_DTChungTu = null,
mucluc_sqd.sXauNoiMa, 
mucluc_sqd.iID_CTDuToan_Nhan iID_CTDuToan_Nhan,
iID_MaDonVi = null,
sTenDonVi = null,
soquyetdinh.iID_DTChungTu idSoQuyetDinh,
soquyetdinh.sSoQuyetDinh,
iRowType = 1,
sGhiChu = null,
isnull(nhanphanbo.fTuChi, 0) fTuChi,
isnull(nhanphanbo.fRutKBNN, 0) fRutKBNN,
isnull(nhanphanbo.fHienVat, 0) fHienVat,
isnull(nhanphanbo.fDuPhong, 0) fDuPhong,
isnull(nhanphanbo.fHangMua, 0) fHangMua,
isnull(nhanphanbo.fHangNhap, 0) fHangNhap,
isnull(nhanphanbo.fPhanCap, 0) fPhanCap,
isnull(nhanphanbo.fTonKho, 0) fTonKho
from mucluc_sqd
left join nhanphanbo_current nhanphanbo on nhanphanbo.sXauNoiMa = mucluc_sqd.sXauNoiMa and nhanphanbo.iID_DTChungTu = mucluc_sqd.iID_CTDuToan_Nhan
left join soquyetdinh on soquyetdinh.iID_DTChungTu = mucluc_sqd.iID_CTDuToan_Nhan
),

phanbo_mucluc as
(
select 
phanbo_donvi.iID_DTCTChiTiet,
iID_DTChungTu  = cast(@VoucherId as uniqueidentifier),
mucluc_sqd_donvi.sXauNoiMa,
mucluc_sqd_donvi.iID_CTDuToan_Nhan,
mucluc_sqd_donvi.Item iID_MaDonVi,
donvis.iID_MaDonVi + ' - ' + donvis.sTenDonVi as sTenDonVi,
idSoQuyetDinh = soquyetdinh.iID_DTChungTu,
sSoQuyetDinh = soquyetdinh.sSoQuyetDinh,
iRowType = 3,
phanbo_donvi.sGhiChu,
isnull(phanbo_donvi.fTuChi, 0) fTuChi,
isnull(phanbo_donvi.fRutKBNN, 0) fRutKBNN,
isnull(phanbo_donvi.fHienVat, 0) fHienVat,
isnull(phanbo_donvi.fDuPhong, 0) fDuPhong,
isnull(phanbo_donvi.fHangMua, 0) fHangMua,
isnull(phanbo_donvi.fHangNhap, 0) fHangNhap,
isnull(phanbo_donvi.fPhanCap, 0) fPhanCap,
isnull(phanbo_donvi.fTonKho, 0) fTonKho
from mucluc_sqd_donvi
left join phanbo_donvi on 
phanbo_donvi.sXauNoiMa = mucluc_sqd_donvi.sXauNoiMa 
and phanbo_donvi.iID_CTDuToan_Nhan = mucluc_sqd_donvi.iID_CTDuToan_Nhan
and phanbo_donvi.iID_MaDonVi = mucluc_sqd_donvi.Item
left join donvis on donvis.iID_MaDonVi = mucluc_sqd_donvi.Item
left join soquyetdinh on soquyetdinh.iID_DTChungTu = mucluc_sqd_donvi.iID_CTDuToan_Nhan
),

muclucchas as
(
select 
iID_DTCTChiTiet = null,
iID_DTChungTu = null,
sXauNoiMa, 
iID_CTDuToan_Nhan = null,
iID_MaDonVi = null,
sTenDonVi = null,
idSoQuyetDinh = null,
sSoQuyetDinh = null,
iRowType = 0, 
sGhiChu = null,
fTuChi = 0,
fRutKBNN = 0,
fHienVat = 0,
fDuPhong = 0,
fHangMua = 0,
fHangNhap = 0,
fPhanCap = 0,
fTonKho = 0
from #muclucngansachs
where bHangChaDuToan = 1
),

datas as
(select * from muclucchas
union
select * from nhanphanbo_mucluc
union 
select * from conlai_mucluc
union 
select * from phanbo_mucluc)

select 
mucluc.iID_MLNS,
mucluc.iID_MLNS_Cha,
mucluc.sXauNoiMa,
mucluc.sLNS,
mucluc.sL,
mucluc.sK,
mucluc.sM,
mucluc.sTM,
mucluc.sTTM,
mucluc.sNG,
mucluc.sTNG,
mucluc.sTNG1,
mucluc.sTNG2,
mucluc.sTNG3,
case iRowType when 2 then N'Số chưa phân bổ'
else mucluc.sMoTa end as sMoTa,
case when iRowType < 3 then 1
else mucluc.bHangCha end as bHangCha,
case when iRowType < 3 then 1
else mucluc.bHangChaDuToan end as bHangChaDuToan,
datas.sGhiChu,
datas.iID_DTCTChiTiet,
datas.iID_CTDuToan_Nhan,
datas.iID_MaDonVi,
datas.sTenDonVi,
datas.fTuChi,
datas.fRutKBNN,
datas.fHienVat,
datas.fHangNhap,
datas.fHangMua,
datas.fPhanCap,
datas.fDuPhong,
datas.fTonKho,
datas.iRowType,
datas.idSoQuyetDinh,
datas.sSoQuyetDinh,
settings.bDuPhong,
settings.bHangMua,
settings.bHangNhap,
settings.bHienVat,
settings.bPhanCap,
settings.bTonKho,
settings.bTuChi,
isnull(settings.bTuChi, cast(0 as bit)) as IsEditTuChi,
isnull(settings.bTonKho, cast(0 as bit)) as IsEditTonKho,
isnull(settings.bHienVat, cast(0 as bit)) as IsEditHienVat,
isnull(settings.bHangNhap, cast(0 as bit)) as IsEditHangNhap,
isnull(settings.bHangMua, cast(0 as bit)) as IsEditHangMua,
isnull(settings.bPhanCap, cast(0 as bit)) as IsEditPhanCap,
isnull(settings.bDuPhong, cast(0 as bit)) as IsEditDuPhong
from #muclucngansachs mucluc
left join datas on datas.sXauNoiMa = mucluc.sXauNoiMa
left join settings on settings.sLNS = mucluc.sLNS
where datas.iRowType is not null
--order by sXauNoiMa, iRowType, iID_MaDonVi

drop table #muclucngansachs

END

;

GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_cua_chungtu_phanbo]    Script Date: 12/10/2024 9:47:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_cua_chungtu_phanbo]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@IdNhanPhanBos nvarchar(max)
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *, 
			   fTongTuChi + fTongHienVat + fTongPhanCap + fTongHangNhap + fTongHangMua + fTongDuPhong + fTongTonKho AS SoPhanBo
		FROM NS_DT_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iNamNganSach = @YearOfBudget 
			AND iID_MaNguonNganSach = @BudgetSource
			AND iID_DTChungTu in (select * from f_split(@IdNhanPhanBos))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(fTuChi) + SUM(fHienVat) + SUM(fPhanCap) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fDuPhong) + SUM(fTonKho) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 on convert(nvarchar(36), dtctct.iID_DTChungTu) = convert(nvarchar(36), dtct.iID_DTChungTu) 
			 WHERE dtctct.iNamLamViec = @YearOfWork 
				   AND dtctct.iNamNganSach = @YearOfBudget 
				   AND dtctct.iID_MaNguonNganSach = @BudgetSource) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)
	SELECT 
		   npb.iID_DTChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.SoPhanBo, 0) AS SoPhanBo,
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON convert(nvarchar(36), npb.iID_DTChungTu) = convert(nvarchar(36), dpb.iID_CTDuToan_Nhan) 
	ORDER BY npb.sSoChungTu
END
;
;
GO
