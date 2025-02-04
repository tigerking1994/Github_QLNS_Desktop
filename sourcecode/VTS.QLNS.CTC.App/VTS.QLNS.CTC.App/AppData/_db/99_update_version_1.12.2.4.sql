/****** Object:  StoredProcedure [dbo].[sp_skt_update_child_dutoandaunam]    Script Date: 03/11/2022 10:18:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_update_child_dutoandaunam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_update_child_dutoandaunam]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 03/11/2022 10:18:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 03/11/2022 10:18:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_create_data_report_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_create_data_report_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 03/11/2022 10:18:22 AM ******/
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
	
group by sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sMoTa, sChuong, iNamNganSach, iID_MaNguonNganSach, iNamLamViec,
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
AND iLoaiDuToan = 1

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
iID_MaNguonNganSach, iLoaiChungTu = 1, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
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
iID_MaNguonNganSach, iLoaiChungTu = 1, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
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

IF EXISTS (SELECT * FROM #QuyetToanNamTruoc)   

INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa, iID_CTDTDauNam)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
0, 0, 0, 0, SUM(fTuChi_PheDuyet),
iID_CanCu = @iIDCauHinhCanCuQuyetToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = 1, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
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
iID_MaNguonNganSach, iLoaiChungTu = 1, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa, @chungTuId

FROM NS_DTDauNam_ChungTuChiTiet_CanCu WHERE
iID_CanCu = @iIDCauHinhCanCuQuyetToan
AND iNamLamViec = @YearOfWork
AND iNamNganSach = @YearOfBudget
AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
-- AND iID_MaDonVi IN (SELECT iID_MaDonVi FROM NS_DTDauNam_ChungTu WHERE bDaTongHop = 1 AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop)))
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa


END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 03/11/2022 10:18:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@ListChungTuTongHop nvarchar(max),
	@Lns nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	select distinct iID_MaDonVi into #listDonVi from NS_DTDauNam_ChungTu where iID_CTDTDauNam IN (select * FROM f_split(@ListChungTuTongHop))
	SELECT DISTINCT NEWID() AS Id,
                NEWID() AS IdDb,
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
                --chitiet.iID_MaDonVi AS IdDonVi,
                mlns.iID_MaDonVi AS IdDonVi,
                donvi.sTenDonVi AS TenDonVi,
                chitiet.TuChi AS ChiTiet,
				chitiet.UocThucHien,
                chitiet.HangNhap,
                chitiet.HangMua,
                chitiet.PhanCap,
                chitiet.ChuaPhanCap,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM (select mlns1.iID_MLNS,
		     mlns1.iID_MLNS_Cha,
			 mlns1.sXauNoiMa,
             mlns1.sLNS,
             mlns1.sL,
             mlns1.sK,
             mlns1.sM,
             mlns1.sTM,
             mlns1.sTTM,
             mlns1.sNG,
             mlns1.sTNG,
             mlns1.sTNG1,
             mlns1.sTNG2,
             mlns1.sTNG3,
             mlns1.sMoTa,
             mlns1.bHangCha,
             mlns1.sChiTietToi,
             mlns1.bHangChaDuToan,
             mlns1.iNamLamViec,
			 mlns1.iID_MaDonVi
			 from NS_MucLucNganSach mlns1 where bHangChaDuToan = 1 and iNamLamViec = @YearOfWork
			 union all
	select	 mlns2.iID_MLNS,
		     mlns2.iID_MLNS_Cha,
			 mlns2.sXauNoiMa,
             mlns2.sLNS,
             mlns2.sL,
             mlns2.sK,
             mlns2.sM,
             mlns2.sTM,
             mlns2.sTTM,
             mlns2.sNG,
             mlns2.sTNG,
             mlns2.sTNG1,
             mlns2.sTNG2,
             mlns2.sTNG3,
             mlns2.sMoTa,
             mlns2.bHangCha,
             mlns2.sChiTietToi,
             mlns2.bHangChaDuToan,
             mlns2.iNamLamViec,
			 #listDonVi.iID_MaDonVi
			 from NS_MucLucNganSach mlns2, #listDonVi where bHangChaDuToan = 0 and iNamLamViec = @YearOfWork) mlns 
LEFT JOIN
  (SELECT sXauNoiMa,
          fDuPhong AS DuPhong,
		  fUocThucHien AS UocThucHien,
          fTuChi AS TuChi,
          fHangNhap AS HangNhap,
          fHangMua AS HangMua,
          fPhanCap AS PhanCap,
          fChuaPhanCap AS ChuaPhanCap,
		  iID_MaDonVi
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND (
			(@Loai = 0 AND iID_MaDonVi = @AgencyId)
            OR (@Loai = 1 AND iID_MaDonVi <> @AgencyId AND @ListChungTuTongHop <> '' AND iID_CTDTDauNam IN (select * FROM f_split(@ListChungTuTongHop)))
		 )
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach = @BudgetSource
     AND iLoaiChungTu = @LoaiChungTu
     ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) donvi on donvi.iID_MaDonVi = mlns.iID_MaDonVi

LEFT JOIN 
( select * FROM  NS_MLSKT_MLNS   where iNamLamViec = @YearOfWork
) MAP ON mlns.sXauNoiMa = map.sNS_XauNoiMa  

WHERE mlns.iNamLamViec = @YearOfWork
  --AND (map.iNamLamViec = @YearOfWork
  --     OR mlns.bHangCha =1) 
  AND mlns.bHangChaDuToan IS NOT NULL
	   and(mlns.sLNS = '1'
            OR ((mlns.sLNS like '104%'
                    AND @LoaiChungTu = '2')
                OR (mlns.sLNS not like '104%'
                    AND @LoaiChungTu = '1')))
					AND mlns.sLNS IN (SELECT * from f_split(@Lns))

ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG
		 END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_update_child_dutoandaunam]    Script Date: 03/11/2022 10:18:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_update_child_dutoandaunam]
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@ChungTuId nvarchar(max)
AS
BEGIN

DECLARE @SoChungTuTongHop  nvarchar(max);

SET @SoChungTuTongHop = (SELECT sDSSoChungTuTongHop FROM NS_DTDauNam_ChungTu WHERE iID_CTDTDauNam = @ChungTuId);

if (@SoChungTuTongHop <> null or @SoChungTuTongHop <> '')
update NS_DTDauNam_ChungTu set bDaTongHop = 0 
where iNamLamViec = @NamLamViec 
		and iNamNganSach = @NamNganSach 
		and iID_MaNguonNganSach = @NguonNganSach
		and sSoChungTu in
			(SELECT *
			 FROM f_split(@SoChungTuTongHop))
		and iLoaiChungTu = @LoaiChungTu


END;

GO
