/****** Object:  StoredProcedure [dbo].[sp_skt_update_total_dutoandaunam]    Script Date: 15/11/2023 4:16:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_update_total_dutoandaunam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_update_total_dutoandaunam]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 15/11/2023 4:16:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_plan_begin_year_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_plan_begin_year_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 15/11/2023 4:16:07 PM ******/
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
 chungtu.iLoaiNguonNganSach AS ILoaiNguonNganSach,
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
          SUM(SoKiemTra) AS SoKiemTra,
		  iLoaiNguonNganSach
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
             END AS SoKiemTra,
			 skt_ct.iLoaiNguonNganSach
      FROM NS_SKT_ChungTuChiTiet skt_ctct
	  JOIN NS_SKT_ChungTu skt_ct ON skt_ctct.iID_CTSoKiemTra = skt_ct.iID_CTSoKiemTra
      WHERE skt_ctct.iNamLamViec = @YearOfWork
        AND (skt_ctct.iLoai = 4 OR skt_ctct.iLoai = 2)
        AND skt_ctct.iLoaiChungTu = cast(@Loai AS int)
		AND skt_ct.bKhoa = 1
      GROUP BY skt_ctct.iID_MaDonVi, skt_ct.iLoaiNguonNganSach) kiemtra 
	  ON donvi.Id_DonVi = kiemtra.Id_DonVi
   GROUP BY donvi.Id_DonVi,
            LoaiNganSach, iLoaiNguonNganSach
   
   UNION  ALL SELECT donvi.iID_MaDonVi,
                     LoaiNganSach,
                     SUM(SoKiemTra) AS SoKiemTra,
					 iLoaiNguonNganSach
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
             END AS SoKiemTra,
			 skt_ct.iLoaiNguonNganSach
      FROM NS_SKT_ChungTuChiTiet skt_ctct
	  JOIN NS_SKT_ChungTu skt_ct ON skt_ctct.iID_CTSoKiemTra = skt_ct.iID_CTSoKiemTra
      WHERE skt_ctct.iNamLamViec = @YearOfWork
        AND skt_ctct.iLoai = 3
        AND skt_ctct.iLoaiChungTu = cast(@Loai AS int)
		AND skt_ct.bKhoa = 1
      GROUP BY skt_ctct.iID_MaDonVi, skt_ct.iLoaiNguonNganSach) kiemtra ON donvi.iID_MaDonVi = kiemtra.iID_MaDonVi
   GROUP BY donvi.iID_MaDonVi,
            LoaiNganSach, iLoaiNguonNganSach) kiemtra ON DonVi.iID_MaDonVi = kiemtra.Id_DonVi
			AND kiemtra.iLoaiNguonNganSach = chungtu.iLoaiNguonNganSach
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_update_total_dutoandaunam]    Script Date: 15/11/2023 4:16:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_update_total_dutoandaunam]
	@IdDonVi nvarchar(100),
	@LoaiDonVi nvarchar(20),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@iLoaiNNS int,
	@NguonNganSach int
AS
BEGIN

DECLARE @TongDuToan float;

DECLARE @TongKiemTra float;

IF @LoaiDonVi ='0' BEGIN
SET @TongKiemTra =
  (SELECT SUM(fTuChi)
   FROM NS_SKT_ChungTuChiTiet chitiet
   INNER JOIN NS_SKT_ChungTu chungtu
   ON chitiet.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
   AND chitiet.iNamLamViec = chungtu.iNamLamViec
   AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS)
   WHERE chitiet.iNamLamViec = @NamLamViec
	 AND chitiet.iNamNganSach = @NamNganSach
	 AND chitiet.iID_MaNguonNganSach = @NguonNganSach
     AND chitiet.iLoai = 3
     AND chitiet.iLoaiChungTu = @LoaiChungTu
     AND chitiet.iID_MaDonVi = @IdDonVi)
SET @TongDuToan =
  (SELECT (CASE
               WHEN @LoaiChungTu = 1 THEN SUM(fTuChi)
               WHEN @LoaiChungTu = 2 THEN SUM(fHangMua) + SUM(fHangNhap) + SUM(fPhanCap) + SUM(fChuaPhanCap)
               ELSE cast(0 AS float)
           END)
   FROM NS_DTDauNam_ChungTuChiTiet chitiet
   INNER JOIN NS_DTDauNam_ChungTu chungtu 
   ON chitiet.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
   AND chitiet.iNamLamViec = chungtu.iNamLamViec
   AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS)
   WHERE chitiet.iNamLamViec = @NamLamViec
     AND chitiet.iNamNganSach = @NamNganSach
	 AND chitiet.iID_MaNguonNganSach = @NguonNganSach
     AND chitiet.iLoai <> 0
     AND chitiet.iLoaiChungTu = @LoaiChungTu
     AND chitiet.iID_MaDonVi = @IdDonVi) END 
ELSE BEGIN
SET @TongKiemTra =
  (SELECT SUM(fTuChi)
   FROM NS_SKT_ChungTuChiTiet chitiet
   INNER JOIN NS_SKT_ChungTu chungtu
   ON chitiet.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
   AND chitiet.iNamLamViec = chungtu.iNamLamViec
   AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS)
   WHERE chitiet.iNamLamViec = @NamLamViec
     AND chitiet.iNamNganSach = @NamNganSach
	 AND chitiet.iID_MaNguonNganSach = @NguonNganSach
     AND chitiet.iLoai = 4
     AND chitiet.iLoaiChungTu = @LoaiChungTu
     AND chitiet.iID_MaDonVi = @IdDonVi)
SET @TongDuToan =
  (SELECT (CASE
               WHEN @LoaiChungTu = 1 THEN SUM(fTuChi)
               WHEN @LoaiChungTu = 2 THEN SUM(fHangMua) + SUM(fHangNhap) + SUM(fPhanCap) + SUM(fChuaPhanCap)
               ELSE cast(0 AS float)
           END)
   FROM NS_DTDauNam_ChungTuChiTiet chitiet
   INNER JOIN NS_DTDauNam_ChungTu chungtu 
   ON chitiet.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
   AND chitiet.iNamLamViec = chungtu.iNamLamViec
   AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS)
   WHERE chitiet.iNamLamViec = @NamLamViec
     AND chitiet.iNamNganSach = @NamNganSach
	 AND chitiet.iID_MaNguonNganSach = @NguonNganSach
     AND chitiet.iLoai =3
     AND chitiet.iLoaiChungTu = @LoaiChungTu
     AND chitiet.iID_MaDonVi = @IdDonVi) END
UPDATE NS_DTDauNam_ChungTu
SET fTongTuChi = @TongDuToan,
    fTongHienVat = @TongKiemTra
WHERE iNamLamViec = @NamLamViec
  AND iNamNganSach = @NamNganSach
  AND iID_MaNguonNganSach = @NguonNganSach
  AND iID_MaDonVi = @IdDonVi
  AND iLoaiChungTu = @LoaiChungTu END
;
;
GO
