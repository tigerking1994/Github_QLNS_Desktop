/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 12/30/2024 3:02:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 12/30/2024 3:02:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 12/30/2024 3:02:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 12/30/2024 3:02:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@DotPB int
As

begin

-- Lấy danh mục Phân bổ thu BHXH
SELECT round(SUM(0.1*(isnull(pbctct.fBHYT_NLD,0)+ isnull(pbctct.fBHYT_NSD,0))) / 1000000,0) * 1000000 as fTienPhanBo, pbctct.iID_MaDonVi INTO #temPBThuBHXH
FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet as pbctct
JOIN BH_DTT_BHXH_PhanBo_ChungTu as pbct
ON pbctct.iID_DTT_BHXH_ChungTu = pbct.iID_DTT_BHXH_PhanBo_ChungTu
WHERE (pbctct.sXauNoiMa like '9020001-010-011-0001%' or pbctct.sXauNoiMa like '9020002-010-011-0001%')
AND pbctct.iNamLamViec = @NamLamViec
AND pbctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
AND pbct.iLoaiDuToan = @DotPB
GROUP BY pbctct.iID_MaDonVi;

select * into #MLBH
from BH_DM_MucLucNganSach where inamlamviec = @NamLamViec
and sLNS in (select * from f_split(@LNS))
and iTrangThai = 1;

declare @NgayQuyetDinh datetime;
declare @NgayChungTu datetime;

select 
@NgayQuyetDinh = cast(dNgayQuyetdinh as date),
@NgayChungTu = cast(dNgayChungTu as date)
from BH_DTC_PhanBoDuToanChi 
where ID = @ChungTuId;

with 

iddonvis as
(
select * from f_split(@IdDonVi)
),

donvis as 
(
select dv.iID_MaDonVi, dv.sTenDonVi from DonVi dv
join NguoiDung_DonVi nd on nd.iID_MaDonVi = dv.iID_MaDonVi and nd.iNamLamViec = dv.iNamLamViec
where dv.iNamLamViec = @NamLamViec
and dv.iID_MaDonVi in (select * from iddonvis)
),

Hierarchy as
(
select
mlns.*,
1 as level,
cast('' as nvarchar(max)) as space -- start with an empty string for root
from #MLBH mlns
where iID_MLNS_Cha IS NULL
union all
select
c.*,
ch.level + 1 as level,
cast(space + '     ' as nvarchar(max)) as space
from #MLBH c
inner join Hierarchy ch on c.iID_MLNS_Cha = ch.iID_MLNS
),

DefaultChild as
(
select sXauNoiMa from Hierarchy
where ((sXauNoiMa like '9010001%' or sXauNoiMa like '9010002%') and (level = 4))
or (sXauNoiMa not like '9010001%' and sXauNoiMa not like '9010002%' and level  = 2)
),

idnhanphanbo as
(
select iID_BHDTC_NhanPhanBo from BH_DTC_Nhan_PhanBo_Map where iID_BHDTC_PhanBo = @ChungTuId
),

soquyetdinh as
(
select ID, sSoQuyetDinh from BH_DTC_DuToanChiTrenGiao where 
ID in (select * from idnhanphanbo)
),

xaunoimas_data as
(
select distinct sXauNoiMa from BH_DTC_DuToanChiTrenGiao_ChiTiet where iID_DTC_DuToanChiTrenGiao in (select * from idnhanphanbo)
union all 
select distinct sXauNoiMa from BH_DTC_PhanBoDuToanChi_ChiTiet where iID_DTC_PhanBoDuToanChi = @ChungTuId
),

HierarchyCha as
(
select
mlns.*,
0 as isParent
from #MLBH mlns
where inamlamviec = @NamLamViec and sXauNoiMa in (select * from xaunoimas_data)
union all
select
c.*,
1 as isParent
from #MLBH c
inner join HierarchyCha ch on c.iID_MLNS = ch.iID_MLNS_Cha
),

xaunoimas as
(
select sXauNoiMa from HierarchyCha where isParent = 0
union all
select sXauNoiMa from DefaultChild where sXauNoiMa not in (select sXauNoiMa from HierarchyCha)
),

HierarchyFull as
(
select
mlns.*,
0 as isParent
from Hierarchy mlns
where inamlamviec = @NamLamViec and sXauNoiMa in (select * from xaunoimas)
union 
select
c.*,
1 as isParent
from Hierarchy c
inner join HierarchyCha ch on c.iID_MLNS = ch.iID_MLNS_Cha
),

mucluc_sqd as (
select * from xaunoimas, idnhanphanbo
),

mucluc_sqd_donvi as (
select * from xaunoimas, idnhanphanbo, iddonvis
),

nhanphanbo as
(
select sXauNoiMa, iID_DTC_DuToanChiTrenGiao, 
sum(isnull(fTienTuChi, 0)) fTienTuChi,
sum(isnull(fTienHienVat, 0)) fTienHienVat
from BH_DTC_DuToanChiTrenGiao_ChiTiet where iID_DTC_DuToanChiTrenGiao in (select * from idnhanphanbo)
group by sXauNoiMa, iID_DTC_DuToanChiTrenGiao
),

daphanbo as
(
select sXauNoiMa, iID_DTC_DuToanChiTrenGiao, 
0 - sum(isnull(fTienTuChi, 0)) fTienTuChi,
0 - sum(isnull(fTienHienVat, 0)) fTienHienVat
from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
left join BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
left join BH_DTC_PhanBoDuToanChi dpb on dpb.ID = ctct.iID_DTC_DuToanChiTrenGiao
where iID_DTC_DuToanChiTrenGiao in (select * from idnhanphanbo) 
and 
(
	cast(isnull(dpb.dNgayQuyetDinh, dpb.dNgayChungTu) as date) 
	< cast(isnull(@NgayQuyetDinh, @NgayChungTu) as date)
)
group by sXauNoiMa, iID_DTC_DuToanChiTrenGiao
),

nhanphanbo_current as
(
select sXauNoiMa, iID_DTC_DuToanChiTrenGiao, 
sum(isnull(fTienTuChi, 0)) fTienTuChi,
sum(isnull(fTienHienVat, 0)) fTienHienVat
from 
(select * from nhanphanbo
union all
select * from daphanbo) npb_current
group by sXauNoiMa, iID_DTC_DuToanChiTrenGiao
),

phanbo as
(
select sXauNoiMa, iID_DTC_DuToanChiTrenGiao, 
sum(isnull(fTienTuChi, 0)) fTienTuChi,
sum(isnull(fTienHienVat, 0)) fTienHienVat
from BH_DTC_PhanBoDuToanChi_ChiTiet where iID_DTC_PhanBoDuToanChi = @ChungTuId
group by sXauNoiMa, iID_DTC_DuToanChiTrenGiao
),

phanbo_donvi as
(
select ID, iID_DTC_PhanBoDuToanChi, sXauNoiMa, 
iID_DTC_DuToanChiTrenGiao, iID_MaDonVi, sGhiChu,
sum(isnull(fTienTuChi, 0)) fTienTuChi,
sum(isnull(fTienHienVat, 0)) fTienHienVat
from BH_DTC_PhanBoDuToanChi_ChiTiet 
where iID_DTC_PhanBoDuToanChi = @ChungTuId
group by ID, sXauNoiMa, iID_DTC_DuToanChiTrenGiao, iID_DTC_PhanBoDuToanChi, iID_MaDonVi, sGhiChu
),

conlai_mucluc as
(
select 
iID_DTCTChiTiet = null,
iID_DTChungTu = null,
mucluc_sqd.sXauNoiMa, 
mucluc_sqd.iID_BHDTC_NhanPhanBo,
iID_MaDonVi = null,
sTenDonVi = null,
soquyetdinh.ID idSoQuyetDinh,
soquyetdinh.sSoQuyetDinh,
iRowType = 2,
isRemainRow = 1,
sGhiChu = null,
(isnull(nhanphanbo.fTienTuChi, 0) - isnull(phanbo.fTienTuChi, 0)) fTienTuChi, 
(isnull(nhanphanbo.fTienHienVat, 0) - isnull(phanbo.fTienHienVat, 0)) fTienHienVat
from mucluc_sqd
left join nhanphanbo_current nhanphanbo on nhanphanbo.sXauNoiMa = mucluc_sqd.sXauNoiMa and nhanphanbo.iID_DTC_DuToanChiTrenGiao = mucluc_sqd.iID_BHDTC_NhanPhanBo
left join phanbo on phanbo.sXauNoiMa = mucluc_sqd.sXauNoiMa and phanbo.iID_DTC_DuToanChiTrenGiao = mucluc_sqd.iID_BHDTC_NhanPhanBo
left join soquyetdinh on soquyetdinh.ID = mucluc_sqd.iID_BHDTC_NhanPhanBo
),

nhanphanbo_mucluc as
(
select 
iID_DTCTChiTiet = null,
iID_DTChungTu = null,
mucluc_sqd.sXauNoiMa, 
mucluc_sqd.iID_BHDTC_NhanPhanBo iID_CTDuToan_Nhan,
iID_MaDonVi = null,
sTenDonVi = null,
soquyetdinh.ID idSoQuyetDinh,
soquyetdinh.sSoQuyetDinh,
iRowType = 2,
isRemainRow = 0,
sGhiChu = null,
isnull(nhanphanbo.fTienTuChi, 0) fTuChi,
isnull(nhanphanbo.fTienHienVat, 0) fTienHienVat
from mucluc_sqd
left join nhanphanbo_current nhanphanbo on nhanphanbo.sXauNoiMa = mucluc_sqd.sXauNoiMa and nhanphanbo.iID_DTC_DuToanChiTrenGiao = mucluc_sqd.iID_BHDTC_NhanPhanBo
left join soquyetdinh on soquyetdinh.ID = mucluc_sqd.iID_BHDTC_NhanPhanBo
),

phanbo_mucluc as
(
select 
phanbo_donvi.ID,
phanbo_donvi.iID_DTC_PhanBoDuToanChi,
mucluc_sqd_donvi.sXauNoiMa,
mucluc_sqd_donvi.iID_BHDTC_NhanPhanBo,
mucluc_sqd_donvi.Item iID_MaDonVi,
donvis.iID_MaDonVi + ' - ' + donvis.sTenDonVi as sTenDonVi,
idSoQuyetDinh = soquyetdinh.ID,
sSoQuyetDinh = soquyetdinh.sSoQuyetDinh,
iRowType = 3,
isRemainRow = 0,
phanbo_donvi.sGhiChu,
isnull(phanbo_donvi.fTienTuChi, 0) fTienTuChi,
isnull(phanbo_donvi.fTienHienVat, 0) fTienHienVat
from mucluc_sqd_donvi
left join phanbo_donvi on 
phanbo_donvi.sXauNoiMa = mucluc_sqd_donvi.sXauNoiMa 
and phanbo_donvi.iID_DTC_DuToanChiTrenGiao = mucluc_sqd_donvi.iID_BHDTC_NhanPhanBo
and phanbo_donvi.iID_MaDonVi = mucluc_sqd_donvi.Item
left join donvis on donvis.iID_MaDonVi = mucluc_sqd_donvi.Item
left join soquyetdinh on soquyetdinh.ID = mucluc_sqd_donvi.iID_BHDTC_NhanPhanBo
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
iRowType = 1,
isRemainRow = 0,
sGhiChu = null,
fTienTuChi = 0,
fTienHienVat = 0
from HierarchyFull
where isParent = 1
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
case iRowType when 2 then mucluc.space + N'Số chưa phân bổ'
else mucluc.space + mucluc.sMoTa end as sNoiDung,
case when iRowType < 3 then 1
else 0 end as bHangCha,
case when iRowType < 3 then 1
else 0 end as bHangChaDuToan,
isRemainRow IsRemainRow,
datas.sGhiChu,
datas.iID_DTCTChiTiet iID_DTC_PhanBoDuToanChiTiet,
datas.iID_CTDuToan_Nhan iID_DTC_DuToanChiTrenGiao,
datas.iID_DTChungTu,
datas.iID_MaDonVi,
datas.sTenDonVi,
case when mucluc.sXauNoiMa = '9010004' then pbbhxh.fTienPhanBo
else datas.fTienTuChi end as fTienTuChi,
datas.fTienTuChi fTienTuChiTruocDieuChinh,
datas.fTienHienVat,
datas.iRowType Type,
datas.idSoQuyetDinh,
datas.sSoQuyetDinh
from HierarchyFull mucluc
left join datas on datas.sXauNoiMa = mucluc.sXauNoiMa
left join #temPBThuBHXH pbbhxh on pbbhxh.iID_MaDonVi = datas.iID_MaDonVi
where datas.iRowType is not null
order by sXauNoiMa, iRowType, iID_MaDonVi

drop table #MLBH;

end
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 12/30/2024 3:02:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50),
@DotPb int

AS
BEGIN

declare @fTienDauNam float;
select @fTienDauNam = round(sum(0.1 * (ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD)) / 1000000,0) * 1000000
from BH_DTT_BHXH_ChungTu_ChiTiet as ctct
join BH_DTT_BHXH_ChungTu as ct
on ctct.iID_DTT_BHXH = ct.iID_DTT_BHXH
where (ctct.sXauNoiMa like '9020001-010-011-0001%' or ctct.sXauNoiMa like '9020002-010-011-0001%')
and ctct.iNamLamViec = @NamLamViec
and ct.iLoaiDuToan = @DotPb;



with 

MLBH as
(
select * from BH_DM_MucLucNganSach where inamlamviec = @NamLamViec
and iTrangThai = 1
and sLNS in (select * from f_split(@sLns))
),


Hierarchy as
(
select
mlns.*,
1 as level,
cast('' as nvarchar(max)) as space -- start with an empty string for root
from MLBH mlns
where inamlamviec = @NamLamViec and iID_MLNS_Cha IS NULL
union all
select
c.*,
ch.level + 1 as level,
cast(space + '     ' as nvarchar(max)) as space
from MLBH c
inner join Hierarchy ch on c.iID_MLNS_Cha = ch.iID_MLNS
),

DonViTongHop as
(
select iID_MaDonVi from DonVi
where iNamLamViec = @NamLamViec
and iLoai = 0
),

DuLieu as
(
select ctct.*
from BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
join BH_DTC_DuToanChiTrenGiao ct on ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
where ct.iID_MaDonVi = @IIDDonVi
and ct.iNamLamViec = @NamLamViec
and ct.ID = @iDNdtctg
),

CanCu as
(
select * from BH_DTC_DuToanChiTrenGiao_ChiTiet_XNM
where iID_DTC_DuToanChiTrenGiao = @iDNdtctg
),

KeHoach_1 as (
select isnull(fTienCNVQP, 0) 
+ isnull(fTienLDHD, 0) 
+ isnull(fTienQNCN, 0)
+ isnull(fTienSQ, 0) 
+ isnull(fTienHSQBS, 0) fTuChi,
sXauNoiMa
from BH_KHC_CheDoBHXH_ChiTiet ctct
join BH_KHC_CheDoBHXH ct on ct.Id = ctct.iID_KHC_CheDoBHXH
join DonViTongHop dv on ct.iID_MaDonVi = dv.iID_MaDonVi
where ct.bIsKhoa = 1
and ct.iNamLamViec = @NamLamViec
),

KeHoach_2 as (
select isnull(fTienCanBo, 0)
+ isnull(fTienQuanLuc, 0)
+ isnull(fTienTaiChinh, 0) 
+ isnull(fTienQuanY, 0) fTuChi,
sXauNoiMa
from BH_KHC_KinhPhiQuanLy_ChiTiet ctct
join BH_KHC_KinhPhiQuanLy ct on ct.iID_BH_KHC_KinhPhiQuanLy = ctct.iID_KHC_KinhPhiQuanLy
join DonViTongHop dv on ct.iID_MaDonVi = dv.iID_MaDonVi
where ct.bIsKhoa = 1
and ct.iNamLamViec = @NamLamViec
),

KeHoach_3 as (
select fTienKeHoachThucHienNamNay fTuChi,
sXauNoiMa
from BH_KHC_KCB_ChiTiet ctct
join BH_KHC_KCB ct on ct.iID_BH_KHC_KCB = ctct.iID_KHC_KCB
join DonViTongHop dv on ct.iID_MaDonVi = dv.iID_MaDonVi
where ct.bIsKhoa = 1
and ct.iNamLamViec = @NamLamViec
),

KeHoach_4 as (
select fTienKeHoachThucHienNamNay fTuChi,
sXauNoiMa
from BH_KHC_K_ChiTiet ctct
join BH_KHC_K ct on ct.iID_BH_KHC_K = ctct.iID_KHC_K
join DonViTongHop dv on ct.iID_MaDonVi = dv.iID_MaDonVi
where ct.bIsKhoa = 1
and ct.iNamLamViec = @NamLamViec
),

DauNam as
(
select ctct.*
from BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
join BH_DTC_DuToanChiTrenGiao ct on ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
where ct.iID_MaDonVi = @IIDDonVi
and ct.iNamLamViec = @NamLamViec
and ct.iLoaiDotNhanPhanBo = 1
and ct.ID <> @iDNdtctg
),

BoSung as (
select isnull(ctct.fTienThucHien06ThangDauNam, 0)
+ isnull(ctct.fTienUocThucHien06ThangCuoiNam, 0)
- isnull(daunam.fTienTuChi, 0)
- isnull(daunam.fTienHienVat, 0) fTuChi, 
ctct.sXauNoiMa
from BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
left join DauNam daunam on daunam.sXauNoiMa = ctct.sXauNoiMa
join BH_DTC_DieuChinhDuToanChi ct on ct.iID_BH_DTC = ctct.iID_BH_DTC
where ct.iID_MaDonVi = @IIDDonVi
and ct.iNamLamViec = @NamLamViec
)

select
mlns.sLNS,
mlns.sL,
mlns.sK,
mlns.sTM,
mlns.sTTM,
mlns.sM,
mlns.sNG,
space + mlns.sMoTa as sNoiDung,
mlns.sXauNoiMa,
mlns.iID_MLNS,
mlns.iID_MLNS_Cha,
dulieu.ID,
dulieu.iID_DTC_DuToanChiTrenGiao,
mlns.iID_MLNS AS iID_MucLucNganSach,
dulieu.fTongTien,
dulieu.fTienHienVat,
case 
	when (mlns.sXauNoiMa = '9010004' and isnull(dulieu.fTienTuChi, 0) = 0) 
	then @fTienDauNam 
	else dulieu.fTienTuChi 
end as fTienTuChi,
dulieu.fTienKeHoach,
dulieu.fTienBoSung,
cancu.fTuChi fTienTuChiTrenGiao,
mlns.sCPChiTietToi,
mlns.sDuToanChiTietToi,
mlns.iNamlamViec,
@IIDDonVi as iID_MaDonVi,
dulieu.dNgaySua,
dulieu.dNgayTao,
dulieu.sNguoiSua,
dulieu.sNguoiTao,
mlns.level
from Hierarchy as mlns
left join

(select 
dulieu.*, 
isnull(kehoach1.fTuChi, 0)
+ isnull(kehoach2.fTuChi, 0) 
+ isnull(kehoach3.fTuChi, 0) 
+ isnull(kehoach4.fTuChi, 0) fTienKeHoach,
bosung.fTuChi fTienBoSung
from DuLieu dulieu
left join KeHoach_1 kehoach1 on kehoach1.sXauNoiMa = dulieu.sXauNoiMa
left join KeHoach_2 kehoach2 on kehoach2.sXauNoiMa = dulieu.sXauNoiMa
left join KeHoach_3 kehoach3 on kehoach3.sXauNoiMa = dulieu.sXauNoiMa
left join KeHoach_4 kehoach4 on kehoach4.sXauNoiMa = dulieu.sXauNoiMa
left join BoSung bosung on bosung.sXauNoiMa = dulieu.sXauNoiMa
) as dulieu
on dulieu.iID_MucLucNganSach = mlns.iID_MLNS
left join CanCu cancu on cancu.sXauNoiMa = mlns.sXauNoiMa


order by mlns.sXauNoiMa


END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 12/30/2024 3:02:53 PM ******/
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
select iID_DTCTChiTiet, iID_DTChungTu, sXauNoiMa, 
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
group by iID_DTCTChiTiet, sXauNoiMa, iID_CTDuToan_Nhan, iID_DTChungTu, iID_MaDonVi, sGhiChu
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
phanbo_donvi.iID_DTChungTu,
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
datas.iID_DTChungTu,
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
;
;
;
GO
