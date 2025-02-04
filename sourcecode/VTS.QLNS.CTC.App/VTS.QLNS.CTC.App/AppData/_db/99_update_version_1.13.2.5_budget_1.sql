-- Xóa bản ghi trùng nhau
WITH CTE AS(
   SELECT [sSKT_KyHieu], [sNS_XauNoiMa],
       RN = ROW_NUMBER()OVER(PARTITION BY sSKT_KyHieu, sNS_XauNoiMa ORDER BY sSKT_KyHieu, sNS_XauNoiMa)
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = 2024
)

DELETE FROM CTE WHERE RN > 1
GO

