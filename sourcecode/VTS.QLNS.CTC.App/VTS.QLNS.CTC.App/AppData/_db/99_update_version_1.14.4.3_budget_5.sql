alter table NS_SKT_MucLuc alter column sl nvarchar(10) null;
alter table NS_SKT_MucLuc alter column sl nvarchar(10) null;

-- Lưu ra bảng backup
if not exists (select * from sys.objects
where object_id = object_id('ns_skt_mucluc_backup_2025') and type in (N'U'))
begin
  select * 
  into ns_skt_mucluc_backup_2025
  from ns_skt_mucluc
  where inamlamviec = 2025
end
go

if not exists (select * from sys.objects
where object_id = object_id('ns_mlskt_mlns_backup_2025') and type in (N'U'))
begin
  select * 
  into ns_mlskt_mlns_backup_2025
  from ns_mlskt_mlns
  where inamlamviec = 2025
end
go

if not exists (select * from sys.objects
where object_id = object_id('ns_skt_chungtuchitiet_backup_2025') and type in (N'U'))
begin
  select * 
  into ns_skt_chungtuchitiet_backup_2025
  from ns_skt_chungtuchitiet
  where inamlamviec = 2025
end
go


-- Chuẩn hóa dữ liệu số kiểm tra, số nhu cầu
update ct
set ct.sKyHieu = skt.sKyHieu
from ns_skt_chungtuchitiet ct
join ns_skt_mucluc skt on 
ct.iID_MLSKT = skt.iID_MLSKT and ct.iNamLamViec = skt.iNamLamViec
where ct.iNamLamViec = 2025 
and ct.sKyHieu <> skt.sKyHieu
go

-- Cập nhật cha con của mục lục hiện tại
update con
set con.KyHieuCha = cha.sKyHieu
from ns_skt_mucluc cha
join ns_skt_mucluc con 
on cha.iID_MLSKT = con.iID_MLSKTCha
and cha.inamlamviec = con.inamlamviec
where con.iNamLamViec = 2025 
go

-- Xóa dữ liệu mục lục hiện tại đã có trong master data
delete cu
from ns_skt_mucluc cu
join ns_skt_mucluc_hd4554 moi on 
(cu.sKyHieu = moi.sKyHieu or cu.skyhieu = moi.skyhieucu or cu.iID = moi.iID) 
and cu.iNamLamViec = moi.iNamLamViec
where cu.iNamLamViec = 2025
go

-- Thêm dữ liệu master data
INSERT INTO [dbo].[NS_SKT_MucLuc]
           ([iID]
           ,[bHangCha]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[dNguoiSua]
           ,[dNguoiTao]
           ,[iID_MLSKT]
           ,[iID_MLSKTCha]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[KyHieuCha]
           ,[Log]
           ,[Muc]
           ,[sKyHieu]
           ,[sLoaiNhap]
           ,[sM]
           ,[sMoTa]
           ,[sNG_Cha]
           ,[sNG]
           ,[sSTT]
           ,[sSTTBC]
           ,[Tag]
           ,[sKyHieuCu]
           ,[SL]
           ,[SK])
SELECT [iID]
      ,[bHangCha]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[dNguoiSua]
      ,[dNguoiTao]
      ,[iID_MLSKT]
      ,[iID_MLSKTCha]
	  ,[iNamLamViec]
      ,[iTrangThai]
      ,[KyHieuCha]
      ,[Log]
      ,[Muc]
      ,[sKyHieu]
      ,[sLoaiNhap]
      ,[sM]
      ,[sMoTa]
      ,[sNG_Cha]
      ,[sNG]
      ,[sSTT]
      ,[sSTTBC]
      ,[Tag]
      ,[sKyHieuCu]
	  ,[sL]
      ,[sK]
  FROM [dbo].[NS_SKT_MucLuc_HD4554]
go

-- Cập nhật lại id cha con
update con
set con.iID_MLSKTCha = cha.iID_MLSKT
from ns_skt_mucluc con
join ns_skt_mucluc cha 
on cha.sKyHieu = con.KyHieuCha 
and cha.inamlamviec = con.inamlamviec 
and cha.iID_MLSKT <> con.iID_MLSKTCha
where cha.inamlamviec = 2025
go

-- Cập nhật lại số ký hiệu cũ
update sktcu
set sktcu.sKyHieuCu = sktcu.sKyHieu
from ns_skt_mucluc sktcu
where sktcu.inamlamviec <= 2025
and isnull(sktcu.sKyHieuCu, '') = ''
go

-- Cập nhật lại dữ liệu chứng từ chi tiết
update ctct
set ctct.sKyHieu = skt.sKyHieu, ctct.sMoTa = skt.sMoTa
from NS_SKT_ChungTuChiTiet ctct
join NS_SKT_MucLuc skt 
on ctct.sKyHieu = skt.sKyHieuCu
and skt.iNamLamViec = ctct.iNamLamViec
where ctct.iNamLamViec = 2025
go

-- Cập nhật lại dữ liệu chứng từ chi tiết căn cứ
update ctct
set ctct.sKyHieu = skt.sKyHieu
from NS_SKT_ChungTuChiTiet_CanCu ctct
join NS_SKT_MucLuc skt 
on ctct.sKyHieu = skt.sKyHieuCu 
and skt.iNamLamViec = ctct.iNamLamViec
where ctct.iNamLamViec = 2025
go

-- Xóa dữ liệu map
delete cu
from ns_mlskt_mlns cu
join ns_mlskt_mlns_2025 moi on 
(cu.sSKT_KyHieu = moi.sSKT_KyHieu or cu.iID_MLSKT_MLNS = moi.iID_MLSKT_MLNS) 
and cu.iNamLamViec = 2025


-- Thêm dữ liệu map nếu thiếu
insert into ns_mlskt_mlns
select new.*
from ns_mlskt_mlns_2025 new
join 
(select sNS_XauNoiMa, sSKT_KyHieu from ns_mlskt_mlns_2025
except
select cu.sNS_XauNoiMa, cu.sSKT_KyHieu
from ns_mlskt_mlns cu
join ns_mlskt_mlns_2025 moi on 
(cu.sSKT_KyHieu = moi.sSKT_KyHieu and cu.sNS_XauNoiMa = moi.sNS_XauNoiMa) 
and cu.iNamLamViec = moi.iNamLamViec
where cu.iNamLamViec = 2025) dup
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
where map.inamlamviec = 2025
go

WITH CTE AS(
   SELECT [iiID_CTSoKiemTra], [iid_CanCu], [sKyHieu],
       RN = ROW_NUMBER()OVER(PARTITION BY iiID_CTSoKiemTra, iid_CanCu, sKyHieu ORDER BY iiID_CTSoKiemTra, iid_CanCu, sKyHieu)
   FROM NS_SKT_ChungTuChiTiet_CanCu
   WHERE iNamLamViec = 2025
)

DELETE FROM CTE WHERE RN > 1
GO

-- Xóa bản ghi trùng nhau
WITH CTE AS(
   SELECT [sSKT_KyHieu], [sNS_XauNoiMa],
       RN = ROW_NUMBER()OVER(PARTITION BY sSKT_KyHieu, sNS_XauNoiMa ORDER BY sSKT_KyHieu, sNS_XauNoiMa)
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = 2025
)

DELETE FROM CTE WHERE RN > 1
GO


