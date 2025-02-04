
-- Lưu ra bảng backup
if not exists (select * from sys.objects
where object_id = object_id('ns_skt_mucluc_backup_2024') and type in (N'U'))
begin
  select * 
  into ns_skt_mucluc_backup_2024
  from ns_skt_mucluc
  where inamlamviec = 2024
end
go

if not exists (select * from sys.objects
where object_id = object_id('ns_mlskt_mlns_backup_2024') and type in (N'U'))
begin
  select * 
  into ns_mlskt_mlns_backup_2024
  from ns_mlskt_mlns
  where inamlamviec = 2024
end
go

if not exists (select * from sys.objects
where object_id = object_id('ns_skt_chungtuchitiet_backup_2024') and type in (N'U'))
begin
  select * 
  into ns_skt_chungtuchitiet_backup_2024
  from ns_skt_chungtuchitiet
  where inamlamviec = 2024
end
go


-- Chuẩn hóa dữ liệu số kiểm tra, số nhu cầu
update ct
set ct.sKyHieu = skt.sKyHieu
from ns_skt_chungtuchitiet ct
join ns_skt_mucluc skt on 
ct.iID_MLSKT = skt.iID_MLSKT and ct.iNamLamViec = skt.iNamLamViec
where ct.iNamLamViec = 2024 
and ct.sKyHieu <> skt.sKyHieu
go

-- Cập nhật cha con của mục lục hiện tại
update con
set con.KyHieuCha = cha.sKyHieu
from ns_skt_mucluc cha
join ns_skt_mucluc con 
on cha.iID_MLSKT = con.iID_MLSKTCha
go

-- Xóa dữ liệu mục lục hiện tại đã có trong master data
delete cu
from ns_skt_mucluc cu
join ns_skt_mucluc_2024 moi on 
(cu.sKyHieu = moi.sKyHieu or cu.skyhieu = moi.skyhieucu) 
and cu.iNamLamViec = moi.iNamLamViec
where cu.iNamLamViec = 2024
go

-- Thêm dữ liệu master data
insert into ns_skt_mucluc
select * from ns_skt_mucluc_2024
go

-- Cập nhật lại id cha con
update con
set con.iID_MLSKTCha = cha.iID_MLSKT
from ns_skt_mucluc con
join ns_skt_mucluc cha 
on cha.sKyHieu = con.KyHieuCha 
and cha.inamlamviec = con.inamlamviec 
and cha.iID_MLSKT <> con.iID_MLSKTCha
where cha.inamlamviec = 2024
go

-- Cập nhật lại số ký hiệu cũ
update sktcu
set sktcu.sKyHieuCu = sktcu.sKyHieu
from ns_skt_mucluc sktcu
where sktcu.inamlamviec <= 2024
and isnull(sktcu.sKyHieuCu, '') = ''
go

-- Cập nhật lại dữ liệu chứng từ chi tiết
update ctct
set ctct.sKyHieu = skt.sKyHieu
from NS_SKT_ChungTuChiTiet ctct
join NS_SKT_MucLuc skt 
on ctct.sKyHieu = skt.sKyHieuCu
and skt.iNamLamViec = ctct.iNamLamViec
where ctct.iNamLamViec = 2024
go

-- Cập nhật lại dữ liệu chứng từ chi tiết căn cứ
update ctct
set ctct.sKyHieu = skt.sKyHieu
from NS_SKT_ChungTuChiTiet_CanCu ctct
join NS_SKT_MucLuc skt 
on ctct.sKyHieu = skt.sKyHieuCu 
and skt.iNamLamViec = ctct.iNamLamViec
where ctct.iNamLamViec = 2024
go

-- Thêm dữ liệu map nếu thiếu
insert into ns_mlskt_mlns
select new.*
from ns_mlskt_mlns_2024 new
join 
(select sNS_XauNoiMa, sSKT_KyHieu from ns_mlskt_mlns_2024
except
select cu.sNS_XauNoiMa, cu.sSKT_KyHieu
from ns_mlskt_mlns cu
join ns_mlskt_mlns_2024 moi on 
(cu.sSKT_KyHieu = moi.sSKT_KyHieu and cu.sNS_XauNoiMa = moi.sNS_XauNoiMa) 
and cu.iNamLamViec = moi.iNamLamViec
where cu.iNamLamViec = 2024) dup
on new.sNS_XauNoiMa = dup.sNS_XauNoiMa and new.sSKT_KyHieu = dup.sSKT_KyHieu
go

-- Cập nhật lại bảng map
update map
set map.sSKT_KyHieu = skt.sKyHieu
from ns_mlskt_mlns map 
join ns_skt_mucluc skt 
on map.sSKT_KyHieu = skt.sKyHieuCu
and map.inamlamviec = skt.inamlamviec
and map.sSKT_KyHieu <> skt.sKyHieu
where map.inamlamviec = 2024
go

update ns_qt_chungtu
set iLanDieuChinh = 0, iLoaiChungTu = 1
where iLoaiChungTu is null
go

update ns_skt_chungtu
set iLoaiNguonNganSach = 1
where iLoai = 3 and iLoaiNguonNganSach is null
go