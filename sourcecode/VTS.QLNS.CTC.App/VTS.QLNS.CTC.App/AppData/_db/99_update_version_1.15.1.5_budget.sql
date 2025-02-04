/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_sosanh_sktdtdn]    Script Date: 11/22/2024 8:10:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_sosanh_sktdtdn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_sosanh_sktdtdn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_sosanh_sktdtdn]    Script Date: 11/22/2024 8:10:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_sosanh_sktdtdn]
@NamLamViec int,
@NamNganSach int,
@NguonNganSach int,
@MaNguonNganSach int,
@MaDonVi nvarchar(max),
@LoaiChungTu int,
@InTheoTongHop bit,
@DonViTinh int
AS
BEGIN

with

tempMLSKT as
(select
IIDMLNS = null,
IIDMLNSCha = null,
skt.iID_MLSKT IIDMLSKT,
skt.iID_MLSKTCha IIDMLSKTCha,
skt.bHangCha IsHangCha,
(skt.sSTT + ' ' + skt.sMoTa) sLNS,
sL = null,
sK = null,
sM = null,
sTM = null,
sTTM = null,
sNG = null,
sTNG = null,
sXauNoiMa = null,
skt.sKyHieu,
skt.sMoTa
from NS_SKT_MucLuc skt
where skt.inamlamviec = @NamLamViec
and skt.iTrangThai = 1
),

tempMLNS as
(select
mlns.iID_MLNS IIDMLNS,
mlns.iID_MLNS_Cha IIDMLNSCha,
IIDMLSKT = null,
skt.iID_MLSKT IIDMLSKTCha,
mlns.bHangCha IsHangCha,
mlns.sLNS,
mlns.sL,
mlns.sK,
mlns.sM,
mlns.sTM,
mlns.sTTM,
mlns.sNG,
mlns.sTNG,
mlns.sXauNoiMa,
case when isnull(skt.sKyHieu, '') <> '' then skt.sKyHieu end sKyHieu,
mlns.sMoTa
from NS_MucLucNganSach mlns
left join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = mlns.sXauNoiMa and map.iNamLamViec = mlns.iNamLamViec
left join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = map.iNamLamViec
where mlns.inamlamviec = @NamLamViec
and mlns.iTrangThai = 1
and map.iTrangThai = 1
and skt.iTrangThai = 1
),

tempSKT as
(select chitiet.sKyHieu, sum(isnull(chitiet.fTuChi, 0)) fTuChi, sum(isnull(chitiet.fMuaHangCapHienVat, 0)) fMuaHangCapHienVat
from NS_SKT_ChungTuChiTiet chitiet
join NS_SKT_ChungTu chungtu on chungtu.iID_CTSoKiemTra = chitiet.iID_CTSoKiemTra and chungtu.iNamLamViec = chitiet.iNamLamViec
where chitiet.iNamLamViec = @NamLamViec
and ((chitiet.iLoai = 4 and @NguonNganSach <> 2 and @InTheoTongHop = 0)
or (chitiet.iLoai = 2 and @NguonNganSach = 2 and @InTheoTongHop = 0)
or (chitiet.iLoai = 3 and @InTheoTongHop = 1))
and chitiet.iID_MaDonVi in (select * from f_split(@MaDonVi))
and (@NguonNganSach = 0 or chungtu.iLoaiNguonNganSach = @NguonNganSach)
and chungtu.iID_MaNguonNganSach = @MaNguonNganSach
and chungtu.iNamNganSach = @NamNganSach
group by sKyHieu),

tempDuToan as
(select chitiet.sXauNoiMa, sum(isnull(chitiet.fTuChi, 0)) fTuChi, sum(isnull(chitiet.fHangNhap, 0)) fHangNhap, sum(isnull(chitiet.fHangMua, 0)) fHangMua
from NS_DTDauNam_ChungTuChiTiet chitiet
join NS_DTDauNam_ChungTu chungtu on chungtu.iID_CTDTDauNam = chitiet.iID_CTDTDauNam
where chitiet.iNamLamViec = @NamLamViec
and chitiet.iID_MaDonVi in (select * from f_split(@MaDonVi))
and (@NguonNganSach = 0 or chungtu.iLoaiNguonNganSach = @NguonNganSach)
and chungtu.iID_MaNguonNganSach = @MaNguonNganSach
and chungtu.iNamNganSach = @NamNganSach
and chungtu.iLoaiChungTu = @LoaiChungTu
group by sXauNoiMa)

select * from

(select mlskt.*,
case when @LoaiChungTu = 1 then chitiet.fTuChi
else chitiet.fMuaHangCapHienVat end as fMucTienPhanBo,
fTuChi = null
from tempMLSKT mlskt
left join tempSKT chitiet on chitiet.sKyHieu = mlskt.sKyHieu
union all
select mlns.*,
fMucTienPhanBo = null,
case when @LoaiChungTu = 1 then chitiet.fTuChi / @DonViTinh
else (chitiet.fHangNhap + chitiet.fHangMua) / @DonViTinh end as fTuChi
from tempMLNS mlns
left join tempDuToan chitiet on chitiet.sXauNoiMa = mlns.sXauNoiMa) as a

where isnull(sKyHieu, '') <> ''
and (fTuChi <> 0 or fMucTienPhanBo <> 0)
order by sKyHieu, sXauNoiMa

END
;
;

GO
