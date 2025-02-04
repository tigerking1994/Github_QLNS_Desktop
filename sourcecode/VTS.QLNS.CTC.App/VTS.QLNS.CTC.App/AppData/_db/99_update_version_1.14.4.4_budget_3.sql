--update nam2025
--set nam2025.sKyHieuCu = nam2024.sKyHieu
--from ns_skt_mucluc nam2025
--join ns_skt_mucluc nam2024 
--on nam2025.sKyHieuCu = nam2024.sKyHieuCu
--and nam2025.inamlamviec = 2025
--and nam2024.iNamLamViec = 2024 

--update NS_SKT_MucLuc
--set sSTTBC = replace(sKyHieu, '-', '') 
--where iNamLamViec = 2025

WITH CTE AS(
SELECT
	RN = ROW_NUMBER()OVER(PARTITION BY sSKT_KyHieu, sNS_XauNoiMa, iNamLamViec ORDER BY sSKT_KyHieu, sNS_XauNoiMa, iNamLamViec)
FROM NS_MLSKT_MLNS
WHERE iNamLamViec in (2023, 2024, 2025)
)

DELETE FROM CTE WHERE RN > 1







