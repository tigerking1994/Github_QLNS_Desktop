
delete from NS_MLSKT_MLNS where iNamLamViec = 2025 and substring(sNS_XauNoiMa, 0, 8) in ('1010100', '1020902', '1010400', '1020600')
go

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

---delete các bản ghi trùng
DELETE T FROM
(
SELECT *
, DupRank = ROW_NUMBER() OVER (
              PARTITION BY sns_xaunoima, sskt_kyhieu, inamlamviec
              ORDER BY (SELECT NULL)
            )
FROM NS_MLSKT_MLNS
) AS T
WHERE DupRank > 1 
