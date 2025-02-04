/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_tatca]    Script Date: 20/11/2023 1:32:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop_tatca]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_tatca]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_tatca]    Script Date: 20/11/2023 1:32:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_tatca]
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
  FROM NS_DTDauNam_ChungTuChiTiet ctct
  INNER JOIN NS_DTDauNam_ChungTu ct 
  ON ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
  AND ctct.iNamLamViec = ct.iNamLamViec
  AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
  AND ct.bDaTongHop = 1
  WHERE ctct.iNamLamViec = @NamLamViec
    AND ctct.iLoai = 3
    AND ctct.iLoaiChungTu = @LoaiChungTu
    AND (@IdDonvi IS NULL OR ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))

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
(SUM(TuChi) <> 0) or (SUM(fUocThucHien) <> 0)) chitiet 
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
  FROM NS_DTDauNam_ChungTuChiTiet ctct
  INNER JOIN NS_DTDauNam_ChungTu ct 
  ON ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
  AND ctct.iNamLamViec = ct.iNamLamViec
  AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
  WHERE ctct.iNamLamViec = @NamLamViec
    AND iLoai = 3
    AND ctct.iLoaiChungTu = @LoaiChungTu
    AND (@IdDonvi IS NULL OR ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))

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
(SUM(TuChi) <> 0) or (SUM(fUocThucHien) <> 0)) chitiet 
LEFT JOIN (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) mlns ON chitiet.XauNoiMa = mlns.sXauNoiMa

END
;
;
;
;
;
;
GO
