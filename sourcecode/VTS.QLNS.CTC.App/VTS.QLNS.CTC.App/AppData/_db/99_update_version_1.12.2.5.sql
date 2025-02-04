/****** Object:  StoredProcedure [dbo].[sp_skt_update_child_dutoandaunam]    Script Date: 04/11/2022 7:14:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_update_child_dutoandaunam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_update_child_dutoandaunam]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 04/11/2022 7:14:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 04/11/2022 7:14:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_create_data_report_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_create_data_report_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]    Script Date: 04/11/2022 7:14:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra_1]    Script Date: 04/11/2022 7:14:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_nhan_so_kiem_tra_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]    Script Date: 04/11/2022 7:14:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]    Script Date: 04/11/2022 7:14:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]
	
@YearOfWork int,
@iID_CTDTDauNam uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON;
--SELECT iID_MLNS AS MucLucID,
--       sLNS AS LNS,
--       sL AS L,
--       sK AS K,
--       sM AS M,
--       sTM AS TM,
--       sTTM AS TTM,
--       sNG AS NG,
--	   sTNG AS TNG,
--       sTNG1 AS TNG1,
--       sTNG2 AS TNG2,
--       sTNG3 AS TNG3,
--       sMoTa AS MoTa,
--       sXauNoiMa AS XauNoiMa,
--       '' AS IdDonViMLNS,
--       NULL AS Id,
--       NULL AS SoLieuChiTietId,
--       '' AS IdDonVi,
--       '' AS TenDonVi,
--       NULL AS MLNSId,
--       0 AS TuChi,
--       '' AS GhiChu,
--       bHangCha
--FROM NS_MucLucNganSach
--WHERE iNamLamViec = @YearOfWork
--  AND bHangCha = 1
--  AND sLNS in
--    (SELECT *
--     FROM  NS_DTDauNam_PhanCap where iID_CTDTDauNam = @iID_CTDTDauNam)
--UNION ALL
SELECT MucLucID,
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
       mlns.sXauNoiMa AS XauNoiMa,
	   phancap.sXauNoiMaGoc AS XauNoiMaGoc,
       IdDonViMLNS,
       Id,
       SoLieuChiTietId,
       isnull(IdDonVi, IdDonViMLNS) AS IdDonVi,
       sTenDonVi TenDonVi,
       MLNSId,
       isnull(TuChi, 0) AS TuChi,
       GhiChu,
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
         FROM (select * from NS_MucLucNganSach where bHangCha = 0) mlns,
              DonVi donvi
         WHERE mlns.iNamLamViec = @YearOfWork
           AND donvi.iNamLamViec = @YearOfWork
           AND donvi.iLoai = '2'
           AND donvi.iTrangThai =1) mlns
      LEFT JOIN
        (SELECT NEWID() AS ID,
                NEWID() AS SoLieuChiTietId,
                iID_MaDonVi AS IdDonVi,
                iID_MLNS AS MLNSId,
                SUM(isnull(fTuChi, 0)) AS TuChi,
                '' AS GhiChu,
				NS_DTDauNam_PhanCap.sXauNoiMa,
				NS_DTDauNam_PhanCap.sXauNoiMaGoc
         FROM NS_DTDauNam_PhanCap
         WHERE iNamLamViec = @YearOfWork AND iID_CTDTDauNam = @iID_CTDTDauNam
         GROUP BY iID_MaDonVi,
                  iID_MLNS,
				  NS_DTDauNam_PhanCap.sXauNoiMa,
				  NS_DTDauNam_PhanCap.sXauNoiMaGoc) phancap ON mlns.sXauNoiMa = phancap.sXauNoiMa
      AND mlns.IdDonViMLNS = phancap.IdDonVi)
WHERE TuChi > 0
ORDER BY mlns.sXauNoiMa, IdDonViMLNS END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra_1]    Script Date: 04/11/2022 7:14:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra_1]
	@Loai nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiChungTu int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @NamLamViec AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT 
		ct.iID_CTSoKiemTra,
		ct.iID_MaDonVi,
		ct.SSoChungTu,
		ct.DNgayChungTu,
		ct.SSoQuyetDinh,
		ct.DNgayQuyetDinh,
		ct.SMoTa,
		ct.iLoai,
		ct.bKhoa,
		ct.iNamLamViec,
		ct.iNamNganSach,
		ct.iID_MaNguonNganSach,
		ct.DNgayTao,
		ct.DNgaySua,
		ct.SNguoiTao,
		ct.SNguoiSua,
		ct.iSoChungTuIndex,
		ct.iLoaiChungTu,
		ct.sDSSoChungTuTongHop,
		ctct.fTongTuChi,
		ctct.fTongPhanCap,
		ctct.fTongMuaHangCapHienVat,
		ct.sTenDonVi,
		ct.bDaTongHop,
		ctct.iLoai as iLoai_CTCT
	Into #Temp
	FROM
		(
			SELECT 
				ct.*, ddv.sTenDonVi 
			FROM 
				NS_SKT_ChungTu ct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE 
				ct.iNamLamViec = @NamLamViec 
				AND ct.iNamNganSach = @NamNganSach
				AND ct.iID_MaNguonNganSach = @NguonNganSach
				--AND ct.iLoai = @Loai 
				AND ct.iLoai in (select * from f_split(@Loai)) 
				AND ct.iLoaiChungTu = @LoaiChungTu
				-- AND EXISTS (SELECT * from f_split(ct.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1)
				-- AND ((@CountDonViCha = 0 and bKhoa = 1) OR (@CountDonViCha <> 0))

				AND ((EXISTS (SELECT * from f_split(ct.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1)
				AND (@CountDonViCha = 0 and bKhoa = 1)) OR (@CountDonViCha <> 0))

		) ct
	LEFT JOIN 
		(
			SELECT 
				iID_CTSoKiemTra,
				sum(fTuChi) as fTongTuChi,
				sum(fPhanCap) as fTongPhanCap,
				sum(fMuaHangCapHienVat) as fTongMuaHangCapHienVat,
				ctct.iLoai as iLoai
			FROM 
				NS_SKT_ChungTuChiTiet ctct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi
			WHERE 
				--ctct.iLoai = @Loai
				ctct.iLoai in (select * from f_split(@Loai))
				AND ctct.iNamLamViec = @NamLamViec
				AND ctct.iNamNganSach = @NamNganSach
				AND ctct.iID_MaNguonNganSach = @NguonNganSach
				AND 
				(
					(
						(dv.iLoai = '0' OR @LoaiChungTu = 1) 
						AND EXISTS (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap)))
					)
					OR 
					(
						EXISTS 
						(
							SELECT * FROM (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap))) ml
							JOIN DanhMuc dm ON dm.iID_MaDanhMuc = ml.sNG AND dm.sType = 'NS_Nganh' AND dm.sGiaTri = dv.iID_MaDonVi AND dm.iNamLamViec = @NamLamViec
						)
					)
				)
		GROUP BY iID_CTSoKiemTra, ctct.iLoai
		) ctct
	ON ctct.iID_CTSoKiemTra =  ct.iID_CTSoKiemTra;

select * from #Temp
except
select * from #Temp
where iLoai = 3 and iLoai_CTCT = 2 

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]    Script Date: 04/11/2022 7:14:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]
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
  FROM NS_DTDauNam_ChungTuChiTiet
  WHERE iNamLamViec = @NamLamViec
    AND iLoai = 3
    AND iLoaiChungTu = @LoaiChungTu
    AND (@IdDonvi IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))

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
(SUM(QuyetToan) <> 0 OR SUM(DuToan) <> 0 OR SUM(fUocThucHien) <> 0) AND (SUM(TuChi) <> 0)) chitiet 
LEFT JOIN (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) mlns ON chitiet.XauNoiMa = mlns.sXauNoiMa

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 04/11/2022 7:14:15 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 04/11/2022 7:14:15 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_skt_update_child_dutoandaunam]    Script Date: 04/11/2022 7:14:15 PM ******/
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
