WITH CTE AS(
   SELECT [iiID_CTSoKiemTra], [iid_CanCu], [sKyHieu],
       RN = ROW_NUMBER()OVER(PARTITION BY iiID_CTSoKiemTra, iid_CanCu, sKyHieu ORDER BY iiID_CTSoKiemTra, iid_CanCu, sKyHieu)
   FROM NS_SKT_ChungTuChiTiet_CanCu
   WHERE iNamLamViec = 2024
)

DELETE FROM CTE WHERE RN > 1
GO