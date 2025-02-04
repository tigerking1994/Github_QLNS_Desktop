/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 10/10/2022 6:40:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 10/10/2022 6:40:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 10/10/2022 6:40:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_dulieu]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_dulieu]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 10/10/2022 6:40:46 PM ******/
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
 (SELECT XauNoiMa = sLNS + '-' + sL + '-' + sK + '-' + sM + '-' + sTM + '-' + sTTM + '-' + sNG,
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
  GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, iID_MaDonVi


  UNION ALL SELECT XauNoiMa = sLNS + '-' + sL + '-' + sK + '-' + sM + '-' + sTM + '-' + sTTM + '-' + sNG,
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
  GROUP BY sLNS, sL,sK,sM, sTM, sTTM, sNG, sMoTa, iID_MaDonVi

  UNION ALL SELECT XauNoiMa = sLNS + '-' + sL + '-' + sK + '-' + sM + '-' + sTM + '-' + sTTM + '-' + sNG,
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
  GROUP BY sLNS, sL,sK,sM, sTM, sTTM, sNG, sMoTa, iID_MaDonVi


  UNION ALL SELECT XauNoiMa = sLNS + '-' + sL + '-' + sK + '-' + sM + '-' + sTM + '-' + sTTM + '-' + sNG,
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
  GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, iID_MaDonVi) AS A

WHERE sLNS like '1%'
GROUP BY iID_MaDonVi, sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, XauNoiMa
HAVING SUM(DuToan) <> 0
OR SUM(QuyetToan) <> 0
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 10/10/2022 6:40:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT DISTINCT
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.fGiaTriUSD AS FGiaTriUsd,
	hd.fGiaTriVND AS FGiaTriVnd,
	hd.fGiaTriEUR AS FGiaTriEur,
	hd.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	da.sTenDuAn AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	CASE
		WHEN hd.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId ) 
	END DieuChinhTu
	
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi
ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID

WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY
	hd.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 10/10/2022 6:40:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop]
	 @NamLamViec int,													
	 @IdDonvi nvarchar(2000),
	 @LoaiChungTu nvarchar(50),
	 @DonViTinh int
AS
BEGIN 
	SET NOCOUNT ON;
SELECT chitiet.*, mlns.iID_MLNS AS MlnsId, iID_MLNS_Cha AS MlnsIdParent FROM (	

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
    sMoTa AS MoTa,
    sXauNoiMa AS XauNoiMa,
    QuyetToan = SUM(ISNULL(QuyetToan, 0)) / @DonViTinh,
    DuToan = SUM(isnull(DuToan, 0)) / @DonViTinh,
    TuChi = SUM(TuChi) / @DonViTinh,
	UocThucHien = SUM(fUocThucHien) / @DonViTinh

FROM
 (SELECT 
    sLNS, sL, sK, sM, sTM, sTTM, sNG,
    sMoTa,
    sXauNoiMa,
	DuToan = 0,
    QuyetToan = 0,
    CASE
      WHEN @LoaiChungTu = '1' THEN fTuChi
      WHEN @LoaiChungTu = '2' THEN fHangNhap + fHangMua + fPhanCap
      ELSE 0
    END AS TuChi,
    fUocThucHien
  FROM NS_DTDauNam_ChungTuChiTiet
  WHERE iNamLamViec = @NamLamViec
    AND iLoai = 3
    AND iLoaiChungTu = @LoaiChungTu
    AND (@IdDonvi IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))

  UNION ALL SELECT LNS, L, K, M, TM, TTM, NG,
          MoTa,
          XauNoiMa,
		  DuToan,
          QuyetToan,
          TuChi = 0,
		  UocThucHien = 0
  FROM f_skt_dulieu(@NamLamViec, @IdDonvi, @LoaiChungTu)) AS dt


GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, sXauNoiMa
HAVING 
SUM(QuyetToan) <> 0
OR SUM(TuChi) <> 0
OR SUM(DuToan) <> 0
OR SUM(fUocThucHien) <> 0) chitiet 
LEFT JOIN (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) mlns ON chitiet.XauNoiMa = mlns.sXauNoiMa

END
;
GO
