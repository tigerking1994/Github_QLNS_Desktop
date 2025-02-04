/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]    Script Date: 17/11/2023 2:05:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]    Script Date: 17/11/2023 2:05:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]    Script Date: 17/11/2023 2:05:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu_tonghop_loainns]    Script Date: 17/11/2023 2:05:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_dulieu_tonghop_loainns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_dulieu_tonghop_loainns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu_tonghop_loainns]    Script Date: 17/11/2023 2:05:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_skt_dulieu_tonghop_loainns] (
@NamLamViec int,
@id_donvi nvarchar(MAX),
@iLoaiNNS int,
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
	AND iNamCanCu = @NamLamViec - 2),

TABLE_C AS (SELECT dtdn_ctct.* from NS_DTDauNam_ChungTuChiTiet_CanCu dtdn_ctct
JOIN NS_DTDauNam_ChungTu dtdn_ct 
ON dtdn_ctct.iID_CTDTDauNam = dtdn_ct.iID_CTDTDauNam
AND (@iLoaiNNS = 0 OR dtdn_ct.iLoaiNguonNganSach = @iLoaiNNS)
WHERE dtdn_ct.bDaTongHop = 1)


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
  FROM TABLE_C                 
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
  FROM TABLE_C   
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

--WHERE sLNS like '1%'
GROUP BY iID_MaDonVi, sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, XauNoiMa
HAVING SUM(DuToan) <> 0
OR SUM(QuyetToan) <> 0
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]    Script Date: 17/11/2023 2:05:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]
	@LoaiChungTu int,
	@ILoai int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguoiNganSach int
AS
BEGIN
SELECT    iID_MLNS,
          SXauNoiMa,
          sum(fTuChi) TuChi,
          sum(fHangNhap) HangNhap,
          sum(fHangMua) HangMua,
          sum(fPhanCap) PhanCap,
          sum(fTuChi) MuaHangHienVat,
          sum(fTuChi) DacThu
   FROM NS_DT_ChungTuChiTiet ctct
   join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
   WHERE ct.iNamLamViec = @NamLamViec
     AND ct.iNamNganSach = 2
	 and ct.iID_MaNguonNganSach = @MaNguoiNganSach
	 and ct.bKhoa = 1
	 and ct.iLoai = @ILoai
	 --and ct.iLoaiDuToan != 2
	 and ct.iLoaiChungTu = @LoaiChungTu
	 and (@ILoai = 0 OR @IdDonVi = '-1' OR ctct.iID_MaDonVi = @IdDonVi)
   GROUP BY iID_MLNS, SXauNoiMa;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]    Script Date: 17/11/2023 2:05:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]
	 @NamLamViec int,													
	 @IdDonvi nvarchar(2000),
	 @LoaiChungTu nvarchar(50),
	 @iLoaiNNS int,
	 @DonViTinh int,
	 @IsInTheoTongHop bit
AS
BEGIN 
	SET NOCOUNT ON;

IF (@IsInTheoTongHop = 1)

SELECT chitiet.*, mlns.sMoTa AS MoTa, mlns.iID_MLNS AS MlnsId, iID_MLNS_Cha AS MlnsIdParent FROM (	

SELECT 
    LNS1 = Left(sLNS, 1),
    LNS3 = Left(sLNS, 3),
    LNS5 = Left(sLNS, 5),
    sLNS AS LNS,
    sL AS L,
    sK AS K,
    sM AS M,
    sTM AS TM,
    sTTM AS TTM,
    sNG AS NG,
    -- sMoTa AS MoTa,
    sXauNoiMa AS XauNoiMa,
    QuyetToan = SUM(ISNULL(QuyetToan, 0)) / @DonViTinh,
    DuToan = SUM(isnull(DuToan, 0)) / @DonViTinh,
    TuChi = SUM(TuChi) / @DonViTinh,
	UocThucHien = SUM(fUocThucHien) / @DonViTinh

FROM
 (SELECT 
    sLNS, sL, sK, sM, sTM, sTTM, sNG,
    sXauNoiMa,
	DuToan = 0,
    QuyetToan = 0,
    CASE
      WHEN @LoaiChungTu = '1' THEN fTuChi
      WHEN @LoaiChungTu = '2' THEN fHangNhap + fHangMua + fPhanCap
      ELSE 0
    END AS TuChi,
    fUocThucHien
  FROM NS_DTDauNam_ChungTuChiTiet chitiet
  INNER JOIN NS_DTDauNam_ChungTu chungtu 
ON chitiet.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
AND chitiet.iNamLamViec = chungtu.iNamLamViec
AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS)
  AND chungtu.bDaTongHop = 1
  WHERE chitiet.iNamLamViec = @NamLamViec
    AND chitiet.iLoai = 3
    AND chitiet.iLoaiChungTu = @LoaiChungTu
    AND (@IdDonvi IS NULL OR chitiet.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))

  --UNION ALL SELECT LNS, L, K, M, TM, TTM, NG,
  --        -- MoTa,
  --        XauNoiMa,
		--  DuToan,
  --        QuyetToan,
  --        TuChi = 0,
		--  UocThucHien = 0
  --FROM f_skt_dulieu(@NamLamViec, @IdDonvi, @LoaiChungTu)
  ) AS dt


GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sXauNoiMa
HAVING 
(SUM(TuChi) <> 0)) chitiet 
LEFT JOIN (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) mlns ON chitiet.XauNoiMa = mlns.sXauNoiMa

ELSE

SELECT chitiet.*, mlns.sMoTa AS MoTa, mlns.iID_MLNS AS MlnsId, iID_MLNS_Cha AS MlnsIdParent FROM (	

SELECT 
    LNS1 = Left(sLNS, 1),
    LNS3 = Left(sLNS, 3),
    LNS5 = Left(sLNS, 5),
    sLNS AS LNS,
    sL AS L,
    sK AS K,
    sM AS M,
    sTM AS TM,
    sTTM AS TTM,
    sNG AS NG,
    -- sMoTa AS MoTa,
    sXauNoiMa AS XauNoiMa,
    QuyetToan = SUM(ISNULL(QuyetToan, 0)) / @DonViTinh,
    DuToan = SUM(isnull(DuToan, 0)) / @DonViTinh,
    TuChi = SUM(TuChi) / @DonViTinh,
	UocThucHien = SUM(fUocThucHien) / @DonViTinh

FROM
 (SELECT 
    sLNS, sL, sK, sM, sTM, sTTM, sNG,
    sXauNoiMa,
	DuToan = 0,
    QuyetToan = 0,
    CASE
      WHEN @LoaiChungTu = '1' THEN fTuChi
      WHEN @LoaiChungTu = '2' THEN fHangNhap + fHangMua + fPhanCap
      ELSE 0
    END AS TuChi,
    fUocThucHien
  FROM NS_DTDauNam_ChungTuChiTiet chitiet
  INNER JOIN NS_DTDauNam_ChungTu chungtu 
  ON chitiet.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
  AND chitiet.iNamLamViec = chungtu.iNamLamViec
  AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS)
  WHERE chitiet.iNamLamViec = @NamLamViec
    AND iLoai = 3
    AND chitiet.iLoaiChungTu = @LoaiChungTu
    AND (@IdDonvi IS NULL OR chitiet.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))

  --UNION ALL SELECT LNS, L, K, M, TM, TTM, NG,
  --        -- MoTa,
  --        XauNoiMa,
		--  DuToan,
  --        QuyetToan,
  --        TuChi = 0,
		--  UocThucHien = 0
  --FROM f_skt_dulieu(@NamLamViec, @IdDonvi, @LoaiChungTu)
  ) AS dt


GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sXauNoiMa
HAVING 
(SUM(TuChi) <> 0)) chitiet 
LEFT JOIN (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) mlns ON chitiet.XauNoiMa = mlns.sXauNoiMa

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]    Script Date: 17/11/2023 2:05:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]
	 @NamLamViec int,													
	 @IdDonvi nvarchar(2000),
	 @LoaiChungTu nvarchar(50),
	 @iLoaiNNS int,
	 @DonViTinh int
AS
BEGIN 
	SET NOCOUNT ON;
SELECT chitiet.*, mlns.sMoTa AS MoTa, mlns.iID_MLNS AS MlnsId, iID_MLNS_Cha AS MlnsIdParent FROM (	

SELECT 
    LNS1 = Left(sLNS, 1),
    LNS3 = Left(sLNS, 3),
    LNS5 = Left(sLNS, 5),
    sLNS AS LNS,
    sL AS L,
    sK AS K,
    sM AS M,
    sTM AS TM,
    sTTM AS TTM,
    sNG AS NG,
    -- sMoTa AS MoTa,
    sXauNoiMa AS XauNoiMa,
    QuyetToan = SUM(ISNULL(QuyetToan, 0)) / @DonViTinh,
    DuToan = SUM(isnull(DuToan, 0)) / @DonViTinh,
    TuChi = SUM(TuChi) / @DonViTinh,
	UocThucHien = SUM(fUocThucHien) / @DonViTinh

FROM
 (SELECT 
    sLNS, sL, sK, sM, sTM, sTTM, sNG,
    sXauNoiMa,
	DuToan = 0,
    QuyetToan = 0,
    CASE
      WHEN @LoaiChungTu = '1' THEN fTuChi
      WHEN @LoaiChungTu = '2' THEN fHangNhap + fHangMua + fPhanCap
      ELSE 0
    END AS TuChi,
    fUocThucHien
  FROM NS_DTDauNam_ChungTuChiTiet dtdn_ctct
  JOIN NS_DTDauNam_ChungTu dtdn_ct 
  ON dtdn_ct.iID_CTDTDauNam = dtdn_ctct.iID_CTDTDauNam 
  AND dtdn_ct.bDaTongHop = 1
  AND (@iLoaiNNS = 0 OR dtdn_ct.iLoaiNguonNganSach = @iLoaiNNS)
  WHERE dtdn_ctct.iNamLamViec = @NamLamViec
    AND dtdn_ctct.iLoai = 3
    AND dtdn_ctct.iLoaiChungTu = @LoaiChungTu
    AND (@IdDonvi IS NULL OR dtdn_ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))

  UNION ALL SELECT LNS, L, K, M, TM, TTM, NG,
          -- MoTa,
          XauNoiMa,
		  DuToan,
          QuyetToan,
          TuChi = 0,
		  UocThucHien = 0
  FROM f_skt_dulieu_tonghop_loainns(@NamLamViec, @IdDonvi, @iLoaiNNS, @LoaiChungTu)) AS dt


GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sXauNoiMa
HAVING 
SUM(QuyetToan) <> 0
OR SUM(TuChi) <> 0
OR SUM(DuToan) <> 0
OR SUM(fUocThucHien) <> 0) chitiet 
LEFT JOIN (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) mlns ON chitiet.XauNoiMa = mlns.sXauNoiMa

END
;
;
;
;
GO
