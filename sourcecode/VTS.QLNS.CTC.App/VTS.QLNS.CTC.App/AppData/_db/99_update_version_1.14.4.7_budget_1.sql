

-- Xóa map của các mục lục cha
delete map
from NS_MLSKT_MLNS map
join NS_MucLucNganSach ml on map.sNS_XauNoiMa = ml.sXauNoiMa
and map.iNamLamViec = ml.iNamLamViec
where ml.bHangCha = 1
go


update NS_SKT_MucLuc_HD4554
set sKyHieuCu = '1-2-04-53-1-01'
where sKyHieu = '1-2-2-04-53-1-01'
and iNamLamViec = 2025
go

update NS_SKT_MucLuc
set sKyHieuCu = '1-2-04-53-1-01'
where sKyHieu = '1-2-2-04-53-1-01'
and iNamLamViec = 2025
go

update NS_SKT_ChungTuChiTiet_CanCu
set sKyHieu = '1-2-2-04-53-1-01'
where sKyHieu = '1-2-04-53-1-01'
and iNamLamViec = 2025
go

update NS_SKT_ChungTuChiTiet
set sKyHieu = '1-2-2-04-53-1-01', sMoTa = N'Bảo quản kỹ thuật Tàu thuyền'
where sKyHieu = '1-2-04-53-1-01'
and iNamLamViec = 2025
go

update NS_SKT_MucLuc_HD4554
set sMoTa = N'Chi khác', sM = '7750'
where sKyHieu = '1-2-1-02-23-1-00'
and iNamLamViec = 2025
go

update NS_SKT_MucLuc
set sMoTa = N'Chi khác', sM = '7750'
where sKyHieu = '1-2-1-02-23-1-00'
and iNamLamViec = 2025
go

update NS_SKT_ChungTuChiTiet
set sMoTa = N'Chi khác'
where sKyHieu = '1-2-1-02-23-1-00'
and iNamLamViec = 2025
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

-- Cập nhật lại trường stt một số mục
update NS_SKT_MucLuc
set sSTT = case sSTTBC when '1220451' then '53.'
					   when '1220452' then '54.'
					   when '1220453' then '55.'
					   when '1220454' then '56.'
					   when '1220455' then '57.'
					   when '1220456' then '58.'
					   when '1220457' then '59.'
					   when '1220458' then '60.'
					   when '1220459' then '61.'
					   when '1220460' then '62.'
					   when '1220461' then '63.'
					   when '1220462' then '64.'
					   when '1220463' then '65.'
					   when '1220464' then '66.'
					   when '1220466' then '67.'
					   when '1220467' then '68.'
					   when '1220468' then '69.'
					   when '1220769' then '72.'
					   else sSTT end
where iNamLamViec = 2025 and sSTTBC in (select * from f_split('1220451,1220452,1220453,1220454,1220455,1220456,1220457,1220458,1220459,1220460,1220461,1220462,1220463,1220464,1220466,1220467,1220468,1220769'))