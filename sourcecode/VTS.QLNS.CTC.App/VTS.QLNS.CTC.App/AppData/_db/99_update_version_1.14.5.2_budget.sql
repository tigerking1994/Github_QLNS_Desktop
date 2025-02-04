
--map luong, pc - BVTC
INSERT INTO [dbo].[NS_MLSKT_MLNS]
           ([iID_MLSKT_MLNS]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[Log]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sNS_XauNoiMa]
           ,[sSKT_KyHieu]
           ,[Tag])
select newid(), getdate(), getdate(), iNamLamViec, iTrangThai, [Log], 'namnth2', 'namnth2', REPLACE(sNS_XauNoiMa, '1010000-', '1010100-'), sSKT_KyHieu, Tag
from NS_MLSKT_MLNS
where iNamLamViec = 2025 and sNS_XauNoiMa like '1010000%'

--map kp nghiep vu - BVTC
INSERT INTO [dbo].[NS_MLSKT_MLNS]
           ([iID_MLSKT_MLNS]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[Log]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sNS_XauNoiMa]
           ,[sSKT_KyHieu]
           ,[Tag])
select newid(), getdate(), getdate(), iNamLamViec, iTrangThai, [Log], 'namnth2', 'namnth2', REPLACE(sNS_XauNoiMa, '1020000-', '1020902-'), sSKT_KyHieu, Tag
from NS_MLSKT_MLNS
where iNamLamViec = 2025 and sNS_XauNoiMa like '1020000%'


--map luong, pc - DN
INSERT INTO [dbo].[NS_MLSKT_MLNS]
           ([iID_MLSKT_MLNS]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[Log]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sNS_XauNoiMa]
           ,[sSKT_KyHieu]
           ,[Tag])
select newid(), getdate(), getdate(), iNamLamViec, iTrangThai, [Log], 'namnth2', 'namnth2', REPLACE(sNS_XauNoiMa, '1010000-', '1010400-'), sSKT_KyHieu, Tag
from NS_MLSKT_MLNS
where iNamLamViec = 2025 and sNS_XauNoiMa like '1010000%'

--map kp nghiep vu - DN
INSERT INTO [dbo].[NS_MLSKT_MLNS]
           ([iID_MLSKT_MLNS]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[Log]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sNS_XauNoiMa]
           ,[sSKT_KyHieu]
           ,[Tag])
select newid(), getdate(), getdate(), iNamLamViec, iTrangThai, [Log], 'namnth2', 'namnth2', REPLACE(sNS_XauNoiMa, '1020000-', '1020600-'), sSKT_KyHieu, Tag
from NS_MLSKT_MLNS
where iNamLamViec = 2025 and sNS_XauNoiMa like '1020000%'


delete t from (
select duprank = row_number() over (
partition by sns_xaunoima, sskt_kyhieu, inamlamviec order by (select null)
) from ns_mlskt_mlns) as t where duprank > 1
go