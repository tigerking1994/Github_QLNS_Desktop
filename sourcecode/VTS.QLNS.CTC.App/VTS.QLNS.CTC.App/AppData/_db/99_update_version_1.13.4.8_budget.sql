/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 14/11/2023 4:37:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_create_data_report_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_create_data_report_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 14/11/2023 4:37:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_plan_begin_year_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_plan_begin_year_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_mucluc_index_chungtu_bvtc]    Script Date: 14/11/2023 4:37:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_mucluc_index_chungtu_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_mucluc_index_chungtu_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_mucluc_index_chungtu_bvtc]    Script Date: 14/11/2023 4:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_mucluc_index_chungtu_bvtc]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetOfSource int,
	@VoucherId nvarchar(max),
	@Loai nvarchar(max),
	@LoaiChungTu int,
	@iLoaiNNS int,
	@AgencyId nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT mucluc.iID AS Id,
       mucluc.iID_MLSKT AS IdMucLuc,
       mucluc.sKyHieu AS KyHieu,
       mucluc.sM AS M,
       mucluc.sSTT AS STT,
       mucluc.sMoTa AS MoTa,
       mucluc.bHangCha ,
       mucluc.iNamLamViec AS NamLamViec,
       mucluc.dNgayTao AS DateCreated,
       mucluc.dNguoiTao AS UserCreator,
       mucluc.dNgaySua AS DateModified,
       mucluc.dNguoiSua AS UserModifier,
       mucluc.Muc,
       '' AS LNS,
       mucluc.iID_MLSKTCha AS IdParent ,
       datachitiet.TuChi ,
       ISNULL(datachitiet.HangMua, 0) AS HangMua ,
       ISNULL(datachitiet.HangNhap, 0) AS HangNhap ,
       ISNULL(datachitiet.PhanCap, 0) AS PhanCap ,
       ISNULL(datachitiet.MuaHangHienVat, 0) AS MuaHangHienVat ,
       ISNULL(datachitiet.DacThu, 0) AS DacThu,

	   ISNULL(dutoandaunam.TuChi, 0) AS DtTuChi,
	   ISNULL(dutoandaunam.HangNhap, 0) AS DtHangNhap,
	   ISNULL(dutoandaunam.HangMua, 0) AS DtHangMua,
	   ISNULL(dutoandaunam.PhanCap, 0) AS DtPhanCap,
	   ISNULL(dutoandaunam.DuPhong, 0) AS DtDuPhong,
	   ISNULL(dutoandaunam.ChuaPhanCap, 0) AS DtChuaPhanCap
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT SUM(fTuChi) AS TuChi,
          CAST(0 AS FLOAT) AS HangMua,
          CAST(0 AS FLOAT) AS HangNhap,
          SUM(fPhanCap) AS PhanCap,
          SUM(fMuaHangCapHienVat) AS MuaHangHienVat,
          SUM(fPhanCap) AS DacThu,
          sKyHieu
   FROM NS_SKT_ChungTuChiTiet as chitiet
   INNER JOIN NS_SKT_ChungTu as chungtu 
   ON chitiet.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
   AND chungtu.iNamLamViec = chitiet.iNamLamViec
   AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS)
   WHERE chitiet.iNamLamViec = @YearOfWork
     AND chitiet.iNamNganSach = @YearOfBudget
	 AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
     AND chitiet.iLoaiChungTu = @LoaiChungTu
     AND chitiet.iLoai in (select * from f_split(@loai))
     AND chitiet.iID_MaDonVi = @AgencyId
	 AND chungtu.bKhoa = 1
   GROUP BY sKyHieu) datachitiet ON mucluc.sKyHieu = datachitiet.sKyHieu

LEFT JOIN 
	(
	select 
	SUM(chitiet.fTuChi) AS TuChi, 
	SUM(chitiet.fHangNhap) AS HangNhap,
	SUM(chitiet.fHangMua) AS HangMua,
	SUM(chitiet.fPhanCap) AS PhanCap,
	SUM(chitiet.fDuPhong) AS DuPhong,
	SUM(chitiet.fChuaPhanCap) AS ChuaPhanCap,
	mucluc.sKyHieu

	FROM NS_DTDauNam_ChungTuChiTiet chitiet
	INNER JOIN NS_DTDauNam_ChungTu as chungtu 
	ON chitiet.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
	AND chungtu.iNamLamViec = chitiet.iNamLamViec
	AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS)
	left join (select * FROM NS_MLSKT_MLNS where iNamLamViec = @YearOfWork) map on chitiet.sXauNoiMa = map.sNS_XauNoiMa
	left join (select * FROM NS_SKT_MucLuc where iNamLamViec = @YearOfWork) mucluc on map.sSKT_KyHieu = mucluc.sKyHieu
	where
	chitiet.iNamLamViec = @YearOfWork
     AND chitiet.iNamNganSach = @YearOfBudget
	 AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
     AND chitiet.iLoaiChungTu = @LoaiChungTu
	 AND chitiet.iID_MaDonVi = @AgencyId
	 AND chitiet.iID_CTDTDauNam <> @VoucherId
	 AND mucluc.sKyHieu is not null
	group by mucluc.sKyHieu
	) dutoandaunam on dutoandaunam.sKyHieu = mucluc.sKyHieu

WHERE mucluc.iNamLamViec = @YearOfWork
  AND mucluc.iTrangThai = 1
ORDER BY mucluc.sKyHieu
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 14/11/2023 4:37:13 PM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 14/11/2023 4:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_create_data_report_2]
	@chungTuId nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@ChungTuTongHop nvarchar(max),
	@NguoiTao nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;


	SELECT TOP 1 * INTO #Table_A FROM NS_CauHinh_CanCu
	WHERE sModule = 'BUDGET_DEMANDCHECK_PLAN'
	AND iID_MaChucNang = 'BUDGET_ESTIMATE'
	AND iNamLamViec = @YearOfWork
	AND iNamCanCu = @YearOfWork - 1

    SELECT TOP 1 * INTO #Table_B FROM NS_CauHinh_CanCu
	WHERE sModule = 'BUDGET_DEMANDCHECK_PLAN'
	AND iID_MaChucNang = 'BUDGET_SETTLEMENT'
	AND iNamLamViec = @YearOfWork
	AND iNamCanCu = @YearOfWork - 2

  --DELETE NS_DTDauNam_ChungTuChiTiet
  --WHERE iNamLamViec = @YearOfWork
  --AND ((@TypeGet = 1
  --      AND iLoai = @Loai)
  --     OR (@TypeGet = 0
  --         AND iLoai <> @Loai))
  --AND iNamNganSach = @YearOfBudget
  --AND iID_MaNguonNganSach =@BudgetSource
  --AND iLoaiChungTu = @LoaiChungTu
  --AND iID_MaDonVi = @AgencyId; 

  	DECLARE @iIDCauHinhCanCuDuToan nvarchar(200)
	SET @iIDCauHinhCanCuDuToan = (SELECT iID_CauHinh_CanCu FROM #Table_A)
	DECLARE @iIDCauHinhCanCuQuyetToan nvarchar(200)
    SET @iIDCauHinhCanCuQuyetToan = (SELECT iID_CauHinh_CanCu FROM #Table_B)


	--DECLARE @chungTuId nvarchar(200)
	--SET @chungTuId = (select iID_CTDTDauNam FROM NS_DTDauNam_ChungTu   
	--WHERE iNamLamViec = @YearOfWork
	--AND iNamNganSach = @YearOfBudget
	--AND iID_MaNguonNganSach = @BudgetSource
	--AND iLoaiChungTu = @LoaiChungTu
	--and iID_MaDonVi = @AgencyId)

	DECLARE @TenDonVi nvarchar(max)
	SET @TenDonVi = (select sTenDonVi FROM DonVi where iNamLamViec = @YearOfWork and iID_MaDonVi = @AgencyId)

INSERT INTO NS_DTDauNam_ChungTuChiTiet(sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sMoTa, sChuong, iNamNganSach, iID_MaNguonNganSach,
iNamLamViec, bHangCha, iLoai, iID_MaDonVi, sTenDonVi, fTuChi, fHienVat, fHangNhap, fHangMua, fPhanCap, fChuaPhanCap,
fDuPhong, fUocThucHien, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNguoiSua, bKhoa, iLoaiChungTu, iID_CTDTDauNam)

SELECT 
	   sXauNoiMa,
       sLNS,
       sL,
       sK,
       sM,
       sTM,
       sTTM,
       sNG,
       sTNG,
       sMoTa,
       sChuong,
       iNamNganSach,
       iID_MaNguonNganSach,
       iNamLamViec,
       bHangCha,
       iLoai,
       @AgencyId,
       @TenDonVi,
       sum(fTuChi),
       sum(fHienVat),
       sum(fHangNhap),
       sum(fHangMua),
       sum(fPhanCap),
	   sum(fChuaPhanCap),
       sum(fDuPhong),
	   sum(fUocThucHien),
       '',
       GETDATE(),
       'sNguoiTao',
       GETDATE(),
       '',
       bKhoa,
       iLoaiChungTu,
	   @chungTuId

FROM NS_DTDauNam_ChungTuChiTiet

WHERE iNamLamViec = @YearOfWork
  AND ((@TypeGet = 1
        AND iLoai = @Loai)
       OR (@TypeGet = 0
           AND iLoai <> @Loai))
  AND iNamNganSach = @YearOfBudget
  AND iID_MaNguonNganSach =@BudgetSource
  AND iLoaiChungTu = @LoaiChungTu
  AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
 -- AND iID_MaDonVi IN (
	--select iID_MaDonVi FROM NS_DTDauNam_ChungTu
	--where iNamLamViec = @YearOfWork and iID_MaNguonNganSach = @BudgetSource and iNamNganSach = @YearOfBudget and iLoaiChungTu = @LoaiChungTu and bKhoa = 1 )
	--and iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
	
GROUP BY sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sMoTa, sChuong, iNamNganSach, iID_MaNguonNganSach, iNamLamViec,
bHangCha,bKhoa, iLoaiChungTu, iLoai

UPDATE NS_DTDauNam_ChungTuChiTiet
SET bKhoa = 1
WHERE iNamLamViec = @YearOfWork
  AND ((@TypeGet = 1
        AND iLoai = @Loai)
       OR (@TypeGet = 0
           AND iLoai <> @Loai))
  AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
  AND iNamNganSach = @YearOfBudget
  AND iID_MaNguonNganSach =@BudgetSource
  AND iLoaiChungTu = @LoaiChungTu;

  --danh dau chung tu da tong hop
--update NS_DTDauNam_ChungTu set bDaTongHop = 0 
--where iNamLamViec = @YearOfWork 
--		and iNamNganSach = @YearOfBudget 
--		and iID_MaNguonNganSach = @BudgetSource
--		and iID_CTDTDauNam in
--			(SELECT *
--			 FROM f_split(@ChungTuTongHop))
--		and iLoaiChungTu = @LoaiChungTu;
update NS_DTDauNam_ChungTu set bDaTongHop = 1 
where iID_CTDTDauNam in
    (SELECT *
     FROM f_split(@ChungTuTongHop))


IF (@LoaiChungTu = 2)
INSERT INTO NS_DTDauNam_PhanCap
           (
           [fTuChi]
           ,[iID_CTDTDauNamChiTiet]
           ,[iID_MaDonVi]
           ,[iID_MLNS]
           ,[iNamLamViec]
           ,[sGhiChu]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sTenDonVi]
           ,[sXauNoiMa]
           ,[iID_CTDTDauNam]
           ,[sXauNoiMaGoc])
SELECT 
           dtdn_pc.[fTuChi]
           ,dtdn_ctct.[iID_CTDTDauNamChiTiet]
           ,dtdn_pc.[iID_MaDonVi]
           ,dtdn_pc.[iID_MLNS]
           ,dtdn_pc.[iNamLamViec]
           ,dtdn_pc.[sGhiChu]
           ,@NguoiTao
           ,@NguoiTao
           ,dtdn_pc.[sTenDonVi]
           ,dtdn_pc.[sXauNoiMa]
           ,@chungTuId
           ,[sXauNoiMaGoc]
FROM NS_DTDauNam_PhanCap dtdn_pc JOIN NS_DTDauNam_ChungTuChiTiet dtdn_ctct ON dtdn_pc.sXauNoiMaGoc = dtdn_ctct.sXauNoiMa AND dtdn_ctct.iID_CTDTDauNam = @chungTuId
where dtdn_pc.iID_CTDTDauNam in
    (SELECT *
     FROM f_split(@ChungTuTongHop))
 


---------------------------------------------------------------------- Xoa NS_DTDauNam_ChungTuChiTiet_CanCu
--DELETE FROM NS_DTDauNam_ChungTuChiTiet_CanCu
--WHERE iNamLamViec = @YearOfWork
--AND iNamNganSach = @YearOfBudget
--AND iID_MaNguonNganSach = @BudgetSource
--AND iLoaiChungTu = @LoaiChungTu
--AND iID_MaDonVi = @AgencyId; 

---------------------------------------------------------------------- Du Toan Nam Truoc
SELECT * INTO #DuToanNamTruoc FROM NS_DT_ChungTu 
WHERE iNamLamViec = @YearOfWork - 1
AND bKhoa = 1
AND iLoai = 0
AND iNamNganSach = @YearOfBudget
AND iID_MaNguonNganSach = @BudgetSource
AND iLoaiDuToan = @LoaiChungTu

-- Chỉ lấy căn cứ cha theo con
INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa, iID_CTDTDauNam)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
SUM(fHangMua), SUM(fHangNhap), SUM(fHienVat), SUM(fPhanCap), SUM(fTuChi),
iID_CanCu = @iIDCauHinhCanCuDuToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = @LoaiChungTu, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa, @chungTuId

FROM NS_DTDauNam_ChungTuChiTiet_CanCu WHERE
iID_CanCu = @iIDCauHinhCanCuDuToan
AND iNamLamViec = @YearOfWork
AND iNamNganSach = @YearOfBudget
AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
-- AND iID_MaDonVi IN (SELECT iID_MaDonVi FROM NS_DTDauNam_ChungTu WHERE bDaTongHop = 1 AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop)))
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa

-- Chỉ lấy căn cứ cha theo con
INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa, iID_CTDTDauNam)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
SUM(fHangMua), SUM(fHangNhap), SUM(fHienVat), SUM(fPhanCap), SUM(fTuChi),
iID_CanCu = @iIDCauHinhCanCuQuyetToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = @LoaiChungTu, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa, @chungTuId

FROM NS_DTDauNam_ChungTuChiTiet_CanCu WHERE
iID_CanCu = @iIDCauHinhCanCuQuyetToan
AND iNamLamViec = @YearOfWork
AND iNamNganSach = @YearOfBudget
AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
-- AND iID_MaDonVi IN (SELECT iID_MaDonVi FROM NS_DTDauNam_ChungTu WHERE bDaTongHop = 1 AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop)))
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa

/*
IF EXISTS (SELECT * FROM #DuToanNamTruoc)   

INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa, iID_CTDTDauNam)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
SUM(fHangMua), SUM(fHangNhap), SUM(fHienVat), SUM(fPhanCap), SUM(fTuChi),
iID_CanCu = @iIDCauHinhCanCuDuToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = @LoaiChungTu, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa, @chungTuId
FROM NS_DT_ChungTuChiTiet WHERE iID_DTChungTu IN (SELECT iID_DTChungTu FROM #DuToanNamTruoc)
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa

ELSE 

INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa, iID_CTDTDauNam)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
SUM(fHangMua), SUM(fHangNhap), SUM(fHienVat), SUM(fPhanCap), SUM(fTuChi),
iID_CanCu = @iIDCauHinhCanCuDuToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = @LoaiChungTu, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa, @chungTuId

FROM NS_DTDauNam_ChungTuChiTiet_CanCu WHERE
iID_CanCu = @iIDCauHinhCanCuDuToan
AND iNamLamViec = @YearOfWork
AND iNamNganSach = @YearOfBudget
AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
-- AND iID_MaDonVi IN (SELECT iID_MaDonVi FROM NS_DTDauNam_ChungTu WHERE bDaTongHop = 1 AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop)))
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa


---------------------------------------------------------------------- Quyet Toan Nam Truoc
SELECT * INTO #QuyetToanNamTruoc FROM NS_QT_ChungTu 
WHERE iNamLamViec = @YearOfWork - 2
AND iNamNganSach = @YearOfBudget
AND iID_MaNguonNganSach = @BudgetSource
AND iID_MaDonVi = @AgencyId
AND bKhoa = 1
--AND (sDSLNS like '%1040100%' or sDSLNS like '%1040200%' or sDSLNS like '%1040300%')

IF EXISTS (SELECT * FROM #QuyetToanNamTruoc)   

INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa, iID_CTDTDauNam)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
case when (sLNS = '1040300') then  SUM(fTuChi_PheDuyet) else 0 end , 
case when (sLNS = '1040100' or sLNS = '1040200') then  SUM(fTuChi_PheDuyet) else 0 end , 
0, 0, SUM(fTuChi_PheDuyet),
iID_CanCu = @iIDCauHinhCanCuQuyetToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = @LoaiChungTu, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa, @chungTuId
FROM NS_QT_ChungTuChiTiet WHERE iID_QTChungTu IN (SELECT iID_QTChungTu FROM #QuyetToanNamTruoc)
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa

ELSE 

INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa, iID_CTDTDauNam)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
SUM(fHangMua), SUM(fHangNhap), SUM(fHienVat), SUM(fPhanCap), SUM(fTuChi),
iID_CanCu = @iIDCauHinhCanCuQuyetToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = @LoaiChungTu, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa, @chungTuId

FROM NS_DTDauNam_ChungTuChiTiet_CanCu WHERE
iID_CanCu = @iIDCauHinhCanCuQuyetToan
AND iNamLamViec = @YearOfWork
AND iNamNganSach = @YearOfBudget
AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
-- AND iID_MaDonVi IN (SELECT iID_MaDonVi FROM NS_DTDauNam_ChungTu WHERE bDaTongHop = 1 AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop)))
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa
*/

END
;
;
;
;
;
GO

update NS_DTDauNam_ChungTu
set iLoaiNguonNganSach = 1 where iLoaiNguonNganSach is null
go