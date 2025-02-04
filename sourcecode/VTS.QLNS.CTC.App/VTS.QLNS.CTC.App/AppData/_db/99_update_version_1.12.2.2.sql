/****** Object:  StoredProcedure [dbo].[sp_skt_dutoan_daunam_phancap_dtdn]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_dutoan_daunam_phancap_dtdn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_dutoan_daunam_phancap_dtdn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_export_kehoachvonung_duocduyet]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_export_kehoachvonung_duocduyet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_export_kehoachvonung_duocduyet]
GO
/****** Object:  StoredProcedure [dbo].[sp_export_kehoachvonung_donvi]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_export_kehoachvonung_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_export_kehoachvonung_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]    Script Date: 31/10/2022 6:23:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 31/10/2022 6:23:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_plan_begin_year_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_plan_begin_year_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 31/10/2022 6:23:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_qddautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_qddautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]    Script Date: 01/11/2022 11:33:07 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu_tonghop]    Script Date: 01/11/2022 11:54:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_dulieu_tonghop]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_dulieu_tonghop]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 01/11/2022 11:54:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_dulieu]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_dulieu]
GO


/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 01/11/2022 11:54:38 AM ******/
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

--WHERE sLNS like '1%'
GROUP BY iID_MaDonVi, sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, XauNoiMa
HAVING SUM(DuToan) <> 0
OR SUM(QuyetToan) <> 0
;
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu_tonghop]    Script Date: 01/11/2022 11:54:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[f_skt_dulieu_tonghop] (
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
	AND iNamCanCu = @NamLamViec - 2),

TABLE_C AS (SELECT dtdn_ctct.* from NS_DTDauNam_ChungTuChiTiet_CanCu dtdn_ctct
JOIN NS_DTDauNam_ChungTu dtdn_ct ON dtdn_ctct.iID_CTDTDauNam = dtdn_ct.iID_CTDTDauNam
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
GO

/****** Object:  StoredProcedure [dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]    Script Date: 01/11/2022 11:33:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]
@iIdsChungTu t_tbl_uniqueidentifier READONLY
AS
BEGIN
	SELECT dt.*, cc.iID_MaChucNang as IIDMaChucNang, cc.iNamCanCu as INamCanCu, cc.SModule, cn.STenChucNang
	FROM @iIdsChungTu as tbl
	INNER JOIN NS_DTDauNam_ChungTuChiTiet_CanCu as dt on tbl.Id = dt.iID_CTDTDauNam
	INNER JOIN NS_CauHinh_CanCu cc on dt.iID_CanCu = cc.iID_CauHinh_CanCu
	INNER JOIN HT_ChucNang as cn on cc.iID_MaChucNang = cn.iID_MaChucNang
END
GO


/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 31/10/2022 6:23:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 16/03/2022
-- Description:	Lấy danh sách dự án cho màn quyết định đầu tư
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_duan_find_from_qddautu]
	@YearOfWork INT, 
	@MaDonVi NVARCHAR(50),
	@ILoai INT,
	@QdDauTuId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DuAnId UNIQUEIDENTIFIER;
	SELECT @DuAnId = iID_DuAnID FROM NH_DA_QDDauTu WHERE ID = @QdDauTuId;

    SELECT
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		duAn.sTenDuAn AS STenDuAn,
		duAn.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		duAn.iID_ChuDauTuID AS IIdChuDauTuId,
		duAn.iID_MaChuDauTu AS IIdMaChuDauTu,
		duAn.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		duAn.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		duAn.sKhoiCong AS SKhoiCong,
		duAn.sKetThuc AS SKetThuc,
		duAn.bIsDuPhong AS BIsDuPhong,
		duAn.sDiaDiem AS SDiaDiem,
		duAn.sMucTieu AS SMucTieu,
		duAn.sQuyMo AS SQuyMo,
		duAn.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurid,
		duAn.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		duAn.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duAn.fUSD AS FUsd,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaId,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		NULL AS STenDonVi,
		NULL AS STenPheDuyet,
		NULL AS STenChuDauTu
	FROM NH_DA_DuAn duAn
	WHERE
		1=1
		AND duAn.iID_MaDonViQuanLy = @MaDonVi
		AND duAn.ID IN (SELECT iID_DuAnID FROM NH_DA_ChuTruongDauTu) -- Lấy dự án đã có chủ trương đầu tư
		AND duAn.ID NOT IN (SELECT DISTINCT(iID_DuAnID) FROM NH_DA_QDDauTu WHERE iID_DuAnID IS NOT NULL AND ILoai = @ILoai AND (@QdDauTuId IS NULL OR iID_DuAnID <> @DuAnId)) -- Lấy dự án chưa có quyết định đầu tư
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 31/10/2022 6:23:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_plan_begin_year_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai nvarchar(50),
	@UserName nvarchar(100)
as
begin
DECLARE @CountDonViCha int;

SELECT @CountDonViCha = count(*)
FROM
  (SELECT *
   FROM NguoiDung_DonVi
   WHERE iID_MaNguoiDung = @UserName
     AND iNamLamViec = @YearOfWork
     AND iTrangThai = 1) nddv
INNER JOIN
  (SELECT *
   FROM DonVi
   WHERE iNamLamViec = @YearOfWork
     AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

SELECT DonVi.iID_MaDonVi AS Id_DonVi,
       DonVi.sTenDonVi AS TenDonVi,
       ISNULL(kiemtra.SoKiemTra, 0) AS SoKiemTra,  ISNULL(chungtu.fTongTuChi,0) as SoDuToan,
   CASE
 	WHEN ISNULL(chungtu.fTongTuChi,0) > ISNULL(kiemtra.SoKiemTra,0) THEN ISNULL(chungtu.fTongTuChi,0) - ISNULL(kiemtra.SoKiemTra,0)
 	ELSE 0
 END AS Tang,
 CASE
 	WHEN ISNULL(chungtu.fTongTuChi,0) < ISNULL(kiemtra.SoKiemTra,0) THEN ISNULL(kiemtra.SoKiemTra,0) -ISNULL(chungtu.fTongTuChi,0)
 	ELSE 0
 END AS Giam,
 chungtu.iID_CTDTDauNam AS Id,
 chungtu.sMoTa AS MoTa,
 chungtu.iLoaiChungTu AS LoaiNganSach,
 DonVi.iLoai AS Loai,
 chungtu.sSoChungTu,
 chungtu.dNgayChungTu,
 chungtu.sDSDonViTongHop AS DSDonViTongHop,
 chungtu.sDSSoChungTuTongHop AS DSSoChungTuTongHop,
 chungtu.sNguoiTao AS NguoiTao,
 chungtu.sDSLNS AS DsLNS,
 chungtu.bDaTongHop AS BDaTongHop,
 isnull(chungtu.bKhoa, 0) AS IsLocked
FROM
   (SELECT *
   FROM NS_DTDauNam_ChungTu
   WHERE iLoaiChungTu = cast(@Loai AS int)
     AND iNamLamViec = @YearOfWork
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach = @BudgetSource ) chungtu
LEFT JOIN DonVi  ON chungtu.iID_MaDonVi = DonVi.iID_MaDonVi
LEFT JOIN
  (SELECT donvi.Id_DonVi,
          LoaiNganSach,
          SUM(SoKiemTra) AS SoKiemTra
   FROM
     (SELECT DonVi.iID_MaDonVi AS Id_DonVi,
             DonVi.sTenDonVi AS TenDonVi,
             DonVi.sMoTa AS MoTa,
             DonVi.LoaiNganSach
      FROM DonVi
      WHERE iLoai <> '0'
        AND iNamLamViec = @YearOfWork
        AND iTrangThai =1) donvi
   LEFT JOIN
     (SELECT skt_ctct.iID_MaDonVi AS Id_DonVi,
             CASE
                 WHEN cast(@Loai AS int) = 2 THEN SUM(fPhanCap) + SUM(fMuaHangCapHienVat)
                 ELSE SUM(fTuChi)
             END AS SoKiemTra
      FROM NS_SKT_ChungTuChiTiet skt_ctct
	  JOIN NS_SKT_ChungTu skt_ct ON skt_ctct.iID_CTSoKiemTra = skt_ct.iID_CTSoKiemTra
      WHERE skt_ctct.iNamLamViec = @YearOfWork
        AND (skt_ctct.iLoai = 4 OR skt_ctct.iLoai = 2)
        AND skt_ctct.iLoaiChungTu = cast(@Loai AS int)
		AND skt_ct.bKhoa = 1
      GROUP BY skt_ctct.iID_MaDonVi) kiemtra ON donvi.Id_DonVi = kiemtra.Id_DonVi
   GROUP BY donvi.Id_DonVi,
            LoaiNganSach
   UNION  ALL SELECT donvi.iID_MaDonVi,
                     LoaiNganSach,
                     SUM(SoKiemTra) AS SoKiemTra
   FROM
     (SELECT DonVi.iID_MaDonVi,
             DonVi.sTenDonVi,
             DonVi.sMoTa,
             DonVi.LoaiNganSach
      FROM DonVi
      WHERE iLoai = '0'
        AND iNamLamViec = @YearOfWork) donvi
   LEFT JOIN
     (SELECT skt_ctct.iID_MaDonVi,
             CASE
                 WHEN CAST(@Loai AS int) = 2 THEN SUM(fPhanCap) + SUM(fMuaHangCapHienVat)
                 ELSE SUM(fTuChi)
             END AS SoKiemTra
      FROM NS_SKT_ChungTuChiTiet skt_ctct
	  JOIN NS_SKT_ChungTu skt_ct ON skt_ctct.iID_CTSoKiemTra = skt_ct.iID_CTSoKiemTra
      WHERE skt_ctct.iNamLamViec = @YearOfWork
        AND skt_ctct.iLoai = 3
        AND skt_ctct.iLoaiChungTu = cast(@Loai AS int)
		AND skt_ct.bKhoa = 1
      GROUP BY skt_ctct.iID_MaDonVi) kiemtra ON donvi.iID_MaDonVi = kiemtra.iID_MaDonVi
   GROUP BY donvi.iID_MaDonVi,
            LoaiNganSach) kiemtra ON DonVi.iID_MaDonVi = kiemtra.Id_DonVi
WHERE DonVi.iNamLamViec = @YearOfWork
  AND DonVi.iTrangThai =1
  AND ((@Loai <> '2'
        AND (DonVi.iLoai = @Loai
             OR DonVi.iLoai = '0'))
       OR (@Loai = '2'
           AND (DonVi.iLoai = '1'
                AND DonVi.bCoNSNganh = 1
                OR DonVi.iLoai = '0')))
  AND (
	  
		  (
			(
				EXISTS (SELECT * from f_split(chungtu.sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
				or chungtu.sDSLNS = '' or chungtu.sDSLNS is null 
			)
			AND EXISTS (SELECT * from f_split(chungtu.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha = 0)
		)


	  OR (@CountDonViCha <> 0 and chungtu.bKhoa = 1)
	  OR	(
			(
				EXISTS (SELECT * from f_split(chungtu.sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
				or chungtu.sDSLNS = '' or chungtu.sDSLNS is null 
			)
			AND EXISTS (SELECT * from f_split(chungtu.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha <> 0)
			)
	)
ORDER BY Id_DonVi;

END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]    Script Date: 31/10/2022 6:23:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_2]
	 @NamLamViec int,													
	 @IdDonvi nvarchar(2000),
	 @LoaiChungTu nvarchar(50),
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
  JOIN NS_DTDauNam_ChungTu dtdn_ct ON dtdn_ct.iID_CTDTDauNam = dtdn_ctct.iID_CTDTDauNam AND dtdn_ct.bDaTongHop = 1
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
  FROM f_skt_dulieu_tonghop(@NamLamViec, @IdDonvi, @LoaiChungTu)) AS dt


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
GO
/****** Object:  StoredProcedure [dbo].[sp_export_kehoachvonung_donvi]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_export_kehoachvonung_donvi]
@Ids t_tbl_uniqueidentifier READONLY
AS
BEGIN
	SELECT dt.iID_DuAnID as IIDDuAnID, da.sTenDuAn, da.sMaDuAn, dt.fGiaTriDeNghi as FGiaTriDeNghi, dt.sGhiChu as SGhiChu, da.iID_MaDonViThucHienDuAnID as IIDMaDonViQuanLy, dt.iID_KeHoachUngID ,
	(SELECT SUM(ISNULL(fTongMucDauTuPheDuyet,0)) FROM VDT_DA_QDDauTu WHERE iID_DuAnID =dt.iID_DuAnID AND dNgayQuyetDinh <= tbl.dNgayDeNghi AND bActive = 1) as FTongMucDauTuPheDuyet
	FROM @Ids as tmp
	INNER JOIN VDT_KHV_KeHoachVonUng_DX_ChiTiet as dt on tmp.Id = dt.iID_KeHoachUngID
	INNER JOIN VDT_KHV_KeHoachVonUng_DX as tbl on dt.iID_KeHoachUngID = tbl.Id
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID	
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_nguonvon_by_duan]    Script Date: 26/11/2021 10:19:14 PM ******/
SET ANSI_NULLS ON
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_export_kehoachvonung_duocduyet]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_export_kehoachvonung_duocduyet]
@Ids t_tbl_uniqueidentifier READONLY
AS
BEGIN
	select da.sTenDuAn as sTenDuAn, da.sMaDuAn as sMaDuAn, dt.fCapPhatTaiKhoBac, dt.fCapPhatBangLenhChi as fCapPhatBangLenhChi, dt.iID_DuAnID as IIDDuAnID, dt.iID_KeHoachUngID as iID_KeHoachUngID, dt.sGhiChu, dv.sTenDonVi,
	(SELECT SUM(ISNULL(fTongMucDauTuPheDuyet,0)) FROM VDT_DA_QDDauTu WHERE iID_DuAnID =dt.iID_DuAnID AND dNgayQuyetDinh <= tbl.dNgayQuyetDinh AND bActive = 1) as fTongMucDauTuPheDuyet,
	ml.sLNS as sLNS, ml.sL as sL, ml.sK as sK, ml.sM as sM, ml.sTM as sTM, ml.sTTM as sTTM, ml.sNG as sNG
	FROM @Ids as tmp
	INNER JOIN VDT_KHV_KeHoachVonUng_ChiTiet as dt on tmp.Id = dt.iID_KeHoachUngID
	INNER JOIN VDT_KHV_KeHoachVonUng as tbl on dt.iID_KeHoachUngID = tbl.Id
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN DonVi dv on tbl.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iId
									OR dt.iID_TieuMucID = ml.iId
									OR dt.iID_TietMucID = ml.iId
									OR dt.iID_NganhID = ml.iId	
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_nguonvon_by_duan]    Script Date: 26/11/2021 10:19:14 PM ******/
SET ANSI_NULLS ON
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]
@iID_CTDTDauNam AS uniqueidentifier ,
@sMLNS AS nvarchar(max),
@iNamLamViec int

As
Begin

	Delete NS_DTDauNam_ChungTuChiTiet_CanCu
	where iID_CTDTDauNam = @iID_CTDTDauNam
	and sLNS  in  (SELECT * FROM f_split(@sMLNS))
	and iNamLamViec = @iNamLamViec


	--Xóa chứng từ đầu năm chi tiết 
	Delete NS_DTDauNam_ChungTuChiTiet 
	where iID_CTDTDauNam = @iID_CTDTDauNam 
	and sLNS  in  (SELECT * FROM f_split(@sMLNS))
	and iNamLamViec = @iNamLamViec
	--

End;

GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoan_daunam_phancap_dtdn]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_skt_dutoan_daunam_phancap_dtdn] 
@YearOfWork int,
@XauNoiMaString nvarchar(MAX),
@XauNoiMa nvarchar(MAX),
@ChiTietId nvarchar(200),
@iID_CTDTDauNam uniqueidentifier,
@XauNoiMaGoc nvarchar(MAX)
AS 
BEGIN
SET NOCOUNT ON;

SELECT iID_MLNS AS MucLucID,
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
	   sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS MoTa,
       sXauNoiMa AS XauNoiMa,
       '' AS IdDonViMLNS,
       NULL AS Id,
       NULL AS SoLieuChiTietId,
       '' AS IdDonVi,
       '' AS TenDonVi,
       NULL AS MLNSId,
       0 AS TuChi,
       '' AS GhiChu,
       bHangCha
FROM NS_MucLucNganSach
WHERE iNamLamViec = @YearOfWork
  AND bHangCha =1
  AND sXauNoiMa in
    (SELECT *
     FROM f_split(@XauNoiMaString))
UNION ALL
SELECT MucLucID,
       sLNS,
       sL,
       sK,
       sM,
       sTM,
       sTTM,
       sNG,
	   sTNG,
       sTNG1,
       sTNG2,
       sTNG3,
       sMoTa,
       sXauNoiMa,
       IdDonViMLNS,
       iID_DTDauNam_PhanCap,
       SoLieuChiTietId,
       isnull(IdDonVi, IdDonViMLNS) AS IdDonVi,
       sTenDonVi,
       MLNSId,
       isnull(TuChi, 0) AS TuChi,
       sGhiChu,
       cast(0 AS bit) AS bHangCha
FROM (
        (SELECT mlns.iID_MLNS AS MucLucID,
                mlns.sLNS,
                mlns.sL,
                mlns.sK,
                mlns.sM,
                mlns.sTM,
                mlns.sTTM,
                mlns.sNG,
				mlns.sTNG,
                mlns.sTNG1,
                mlns.sTNG2,
                mlns.sTNG3,
                mlns.sMoTa,
                mlns.sXauNoiMa,
                donvi.iID_MaDonVi AS IdDonViMLNS,
                donvi.sTenDonVi
         FROM NS_MucLucNganSach mlns,
              DonVi donvi
         WHERE mlns.iNamLamViec = @YearOfWork
           AND mlns.sXauNoiMa = @XauNoiMa
           AND donvi.iNamLamViec = @YearOfWork
           AND donvi.iLoai = '2'
           AND donvi.iTrangThai =1) mlns
      LEFT JOIN
        (SELECT iID_DTDauNam_PhanCap,
                iID_CTDTDauNamChiTiet AS SoLieuChiTietId,
                iID_MaDonVi AS IdDonVi,
                iID_MLNS AS MLNSId,
                isnull(fTuChi, 0) AS TuChi,
                sGhiChu
         FROM NS_DTDauNam_PhanCap
         WHERE (@ChiTietId is null or iID_CTDTDauNamChiTiet = @ChiTietId) and  iID_CTDTDauNam = @iID_CTDTDauNam And sXauNoiMaGoc = @XauNoiMaGoc
           AND iNamLamViec = @YearOfWork ) phancap ON mlns.MucLucID = phancap.MLNSId
      AND mlns.IdDonViMLNS = phancap.IdDonVi)
ORDER BY sXauNoiMa END
;
;
GO

update NS_DTDauNam_ChungTuChiTiet_CanCu
set iID_CTDTDauNam = (select top 1 iID_CTDTDauNam from NS_DTDauNam_ChungTu dt
where dt.iID_MaDonVi = NS_DTDauNam_ChungTuChiTiet_CanCu.iID_MaDonVi
and dt.iID_MaNguonNganSach=NS_DTDauNam_ChungTuChiTiet_CanCu.iID_MaNguonNganSach
and dt.iNamLamViec = NS_DTDauNam_ChungTuChiTiet_CanCu.iNamLamViec)
where (iID_CTDTDauNam is null or iID_CTDTDauNam ='00000000-0000-0000-0000-000000000000')
GO

update NS_DTDauNam_PhanCap
set iID_CTDTDauNam = (select iID_CTDTDauNam from NS_DTDauNam_ChungTuChiTiet ct
where ct.iID_CTDTDauNamChiTiet = NS_DTDauNam_PhanCap .iID_CTDTDauNamChiTiet)
where (iID_CTDTDauNam is null or iID_CTDTDauNam ='00000000-0000-0000-0000-000000000000')
GO

update pc
set pc.sXauNoiMaGoc = ct.sXauNoiMa
from NS_DTDauNam_PhanCap pc
inner join NS_DTDauNam_ChungTuChiTiet ct
on pc.iID_CTDTDauNamChiTiet = ct.iID_CTDTDauNamChiTiet
GO