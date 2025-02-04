/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 16/11/2023 9:57:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_plan_begin_year_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_plan_begin_year_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_donvi_has_solieu_chitiet]    Script Date: 16/11/2023 9:57:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_donvi_has_solieu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_donvi_has_solieu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_donvi_has_solieu_chitiet]    Script Date: 16/11/2023 9:57:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_get_donvi_has_solieu_chitiet]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetOfSource int,
	@LoaiChungTu nvarchar(50),
	@iLoaiNNS int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM DonVi
	WHERE iNamLamViec = @YearOfWork
	  AND iTrangThai = 1
	  AND iLoai <> 0
	  AND iID_MaDonVi in
		(SELECT DISTINCT chitiet.iID_MaDonVi
		 FROM NS_DTDauNam_ChungTuChiTiet chitiet
		 INNER JOIN NS_DTDauNam_ChungTu chungtu 
		 ON chitiet.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
		 AND chitiet.iNamLamViec = chungtu.iNamLamViec
		 AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS) 
		 WHERE chitiet.iNamLamViec = @YearOfWork
		   AND chitiet.iNamNganSach = @YearOfBudget
		   AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
		   AND chitiet.iLoai = 3
		   AND iTrangThai = 1
		   AND chitiet.iLoaiChungTu = @LoaiChungTu
		   AND (fTuChi <> 0
				OR fDuPhong <> 0
				OR fHangNhap <> 0
				OR fHangMua <> 0
				OR fPhanCap <> 0) )
ORDER BY iID_MaDonVi 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 16/11/2023 9:57:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_plan_begin_year_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai nvarchar(50),
	@iLoaiNNS int,
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
	 AND (@iLoaiNNS = 0 OR iLoaiNguonNganSach = @iLoaiNNS)
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
		AND (@iLoaiNNS = 0 OR skt_ct.iLoaiNguonNganSach = @iLoaiNNS)
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
		AND (@iLoaiNNS = 0 OR skt_ct.iLoaiNguonNganSach = @iLoaiNNS)
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
;
GO
