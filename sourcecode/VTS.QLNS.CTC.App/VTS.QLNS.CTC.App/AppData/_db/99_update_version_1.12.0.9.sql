/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_3]    Script Date: 14/10/2022 9:54:45 AM ******/
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 14/10/2022 9:54:45 AM ******/
DROP FUNCTION [dbo].[f_skt_dulieu]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 14/10/2022 9:54:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_skt_dulieu] (
@NamLamViec int,
@id_donvi nvarchar(MAX),
@LoaiChungTu nvarchar(50)) 
RETURNS TABLE 
AS 
RETURN

WITH 

TABLE_A AS (SELECT * FROM NS_CauHinh_CanCu chcc
	WHERE sModule = 'BUDGET_DEMANDCHECK_PLAN'
	AND iID_MaChucNang = 'BUDGET_ESTIMATE'
	AND iNamLamViec = @NamLamViec
	AND iNamCanCu = @NamLamViec - 1),

TABLE_B AS (SELECT * FROM NS_CauHinh_CanCu
	WHERE sModule = 'BUDGET_DEMANDCHECK_PLAN'
	AND iID_MaChucNang = 'BUDGET_SETTLEMENT'
	AND iNamLamViec = @NamLamViec
	AND iNamCanCu = @NamLamViec - 2)

SELECT iID_MaDonVi AS Id_DonVi,
    sLNS AS LNS,
    sL AS L,
    sK AS K,
    sM AS M,
    sTM AS TM,
    sTTM AS TTM,
    sNG AS NG,
    sMoTa AS MoTa,
    XauNoiMa,
    DuToan = SUM(DuToan),
    QuyetToan = SUM(QuyetToan),
	UocThucHien = 0
FROM
 (SELECT 
     -- XauNoiMa = sLNS + '-' + sL + '-' + sK + '-' + sM + '-' + sTM + '-' + sTTM + '-' + sNG,
	 sXauNoiMa as XauNoiMa,
     sLNS, sL, sK, sM, sTM, sTTM, sNG,
     sMoTa,
     iID_MaDonVi,
     CASE
       WHEN @LoaiChungTu = '1' THEN SUM(fTuChi)
       WHEN @LoaiChungTu = '2' THEN SUM(fHangNhap) + SUM(fHangMua) + SUM(fPhanCap)
       ELSE 0
     END AS DuToan,
     QuyetToan = 0,
     UocThucHien = 0
  FROM NS_DT_ChungTuChiTiet nsdtctct 
  WHERE
	 NOT EXISTS (SELECT * FROM TABLE_A)
	 AND iNamNganSach = 2
     AND iNamLamViec = @NamLamViec - 1
     AND iID_DTChungTu IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu WHERE iNamLamViec = @NamLamViec - 1 AND iLoai = 1)
     AND (@id_donvi IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@id_donvi)))
  GROUP BY sXauNoiMa,sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, iID_MaDonVi


  UNION ALL SELECT 
	      -- XauNoiMa = sLNS + '-' + sL + '-' + sK + '-' + sM + '-' + sTM + '-' + sTTM + '-' + sNG,
          sXauNoiMa as XauNoiMa,
		  sLNS, sL, sK, sM, sTM, sTTM, sNG,
          sMoTa,
          iID_MaDonVi,
          CASE
            WHEN @LoaiChungTu = '1' THEN SUM(fTuChi)
            WHEN @LoaiChungTu = '2' THEN SUM(fHangNhap) + SUM (fHangMua) + SUM(fPhanCap)
            ELSE 0
          END AS DuToan,
          QuyetToan = 0,
		  UocThucHien = 0
  FROM NS_DTDauNam_ChungTuChiTiet_CanCu                 
  WHERE 
     EXISTS (SELECT * FROM TABLE_A)
	 AND iLoaiChungTu = @LoaiChungTu
	 AND iID_CanCu IN (SELECT iID_CauHinh_CanCu FROM TABLE_A)
     AND (@id_donvi IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@id_donvi)))
  GROUP BY sXauNoiMa,sLNS, sL,sK,sM, sTM, sTTM, sNG, sMoTa, iID_MaDonVi

  UNION ALL SELECT 
		  -- XauNoiMa = sLNS + '-' + sL + '-' + sK + '-' + sM + '-' + sTM + '-' + sTTM + '-' + sNG,
		  sXauNoiMa as XauNoiMa,
          sLNS, sL, sK, sM, sTM, sTTM, sNG,
          sMoTa,
          iID_MaDonVi,
		  DuToan = 0,
          CASE
             WHEN @LoaiChungTu = '1' THEN SUM(fTuChi)
             WHEN @LoaiChungTu = '2' THEN SUM(fHangNhap) + SUM (fHangMua) + SUM(fPhanCap)
             ELSE 0
          END AS QuyetToan,
		  UocThucHien = 0
  FROM NS_DTDauNam_ChungTuChiTiet_CanCu   
  WHERE 
     EXISTS (SELECT * FROM TABLE_B)
	 AND iLoaiChungTu = @LoaiChungTu
	 AND iID_CanCu IN (SELECT iID_CauHinh_CanCu FROM TABLE_B)
     AND (@id_donvi IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@id_donvi)))
  GROUP BY sXauNoiMa,sLNS, sL,sK,sM, sTM, sTTM, sNG, sMoTa, iID_MaDonVi


  UNION ALL SELECT 
          --XauNoiMa = sLNS + '-' + sL + '-' + sK + '-' + sM + '-' + sTM + '-' + sTTM + '-' + sNG,
		  sXauNoiMa as XauNoiMa,
          sLNS, sL, sK, sM, sTM, sTTM, sNG,
          sMoTa,
          iID_MaDonVi,
          DuToan = 0,
		  SUM(fTuChi_PheDuyet) as QuyetToan,
		  UocThucHien = 0
  FROM NS_QT_ChungTuChiTiet  
  WHERE 
	 NOT EXISTS (SELECT * FROM TABLE_B)
     AND iNamLamViec = @NamLamViec - 2
	 AND iNamNganSach = 2
     AND (@id_donvi IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@id_donvi)))
  GROUP BY sXauNoiMa,sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, iID_MaDonVi) AS A

WHERE sLNS like '1%'
GROUP BY iID_MaDonVi, sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, XauNoiMa
HAVING SUM(DuToan) <> 0
OR SUM(QuyetToan) <> 0
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_3]    Script Date: 14/10/2022 9:54:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@Lns nvarchar(max),
	@VoucherId nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select *  into #lnsTem  from f_split(@Lns);
	SELECT DISTINCT isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) AS Id,
                chitiet.iID_CTDTDauNamChiTiet AS IdDb,
                mlns.iID_MLNS AS MlnsId,
                mlns.iID_MLNS_Cha AS MlnsIdParent,
                mlns.sXauNoiMa AS XauNoiMa,
                mlns.sLNS AS LNS,
                mlns.sL AS L,
                mlns.sK AS K,
                mlns.sM AS M,
                mlns.sTM AS TM,
                mlns.sTTM AS TTM,
                mlns.sNG AS NG,
                mlns.sTNG AS TNG,
                mlns.sTNG1 AS TNG1,
                mlns.sTNG2 AS TNG2,
                mlns.sTNG3 AS TNG3,
                mlns.sMoTa AS MoTa,
                '' AS Chuong,
                mlns.bHangCha,
                mlns.sChiTietToi AS ChiTietToi,
                chitiet.iID_MaDonVi AS IdDonVi,
                chitiet.sTenDonVi AS TenDonVi,
                chitiet.fTuChi AS ChiTiet,
				chitiet.fUocThucHien AS UocThucHien,
                chitiet.fHangNhap AS HangNhap,
                chitiet.fHangMua AS HangMua,
                chitiet.fPhanCap AS PhanCap,
				case when chitiet_phancap.fTuChi is null then ISNULL(chitiet.fPhanCap,0) else ISNULL(chitiet.fPhanCap,0) - ISNULL(chitiet_phancap.fTuChi,0) end as PhanCapConLai,
                chitiet.fChuaPhanCap AS ChuaPhanCap,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT *
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach =@BudgetSource
     AND (iID_MaDonVi = @AgencyId
          OR (@AgencyId = '00'
              AND bKhoa = 0))
	 AND iID_CTDTDauNam = @VoucherId
     AND iLoaiChungTu = @LoaiChungTu ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
  (SELECT *
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = @YearOfWork ) map ON mlns.sXauNoiMa = map.sNS_XauNoiMa

LEFT JOIN
( select IsNULL(Sum(fTuChi),0) as fTuChi, iID_CTDTDauNamChiTiet
		from  NS_DTDauNam_PhanCap
		group by iID_CTDTDauNamChiTiet
		) as chitiet_phancap ON chitiet_phancap.iID_CTDTDauNamChiTiet = chitiet.iID_CTDTDauNamChiTiet
--inner join  lnsTem ON  mlns.sLNS  = LNSTEM.Item 
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1  
  AND mlns.bHangChaDuToan IS NOT NULL
 AND (mlns.sLNS = '1'
     OR ((mlns.sLNS like '104%'
          AND @LoaiChungTu = '2')
         OR (mlns.sLNS not like '104%'
             AND @LoaiChungTu = '1')))
--AND mlns.sLNS IN (SELECT * from f_split(@Lns))
AND mlns.sLNS IN (select * from #lnsTem)
--AND EXISTS (SELECT *  AS sLNS from f_split(@Lns) where Item = mlns.sLNS)
ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG;

drop table #lnsTem;

END
;
;
;
;
;
GO
