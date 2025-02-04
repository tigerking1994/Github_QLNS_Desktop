
update NS_DTDauNam_ChungTuChiTiet_CanCu
set iID_CTDTDauNam = (select top 1 iID_CTDTDauNam from NS_DTDauNam_ChungTu dt
where dt.iID_MaDonVi = NS_DTDauNam_ChungTuChiTiet_CanCu.iID_MaDonVi
and dt.iID_MaNguonNganSach=NS_DTDauNam_ChungTuChiTiet_CanCu.iID_MaNguonNganSach
and dt.iNamLamViec = NS_DTDauNam_ChungTuChiTiet_CanCu.iNamLamViec)
where iID_CTDTDauNam is null

update NS_DTDauNam_PhanCap
set iID_CTDTDauNam = (select iID_CTDTDauNam from NS_DTDauNam_ChungTuChiTiet ct
where ct.iID_CTDTDauNamChiTiet = NS_DTDauNam_PhanCap .iID_CTDTDauNamChiTiet)
where iID_CTDTDauNam is null



/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_index_2]    Script Date: 28/10/2022 1:52:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_donvi0_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 28/10/2022 1:52:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_create_data_report_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_create_data_report_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]    Script Date: 28/10/2022 1:52:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1]    Script Date: 28/10/2022 1:52:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]    Script Date: 28/10/2022 1:52:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]    Script Date: 28/10/2022 1:52:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]    Script Date: 28/10/2022 1:52:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]
@iID_CTDTDauNam AS uniqueidentifier ,
@sMLNS AS nvarchar(max),
@iNamLamViec int

As
Begin

	--Xóa chứng từ đầu năm chi tiết 
	Delete NS_DTDauNam_ChungTuChiTiet 
	where iID_CTDTDauNam = @iID_CTDTDauNam 
	and sLNS  in  (SELECT * FROM f_split(@sMLNS))
	and iNamLamViec = @iNamLamViec

End;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]    Script Date: 28/10/2022 1:52:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1] 
@Loai varchar(MAX),
@IdDonVi varchar(MAX),
@NamLamViec int, 
@Dvt int,
@LoaiChungTu int 
AS 
BEGIN
SELECT DISTINCT mucluc.iID AS Id,
                mucluc.iID_MLSKT AS IDMucLuc,
                mucluc.sSTT AS STT,
                mucluc.sMoTa AS MoTa,
                mucluc.iID_MLSKTCha AS IdParent,
                mucluc.sSTTBC AS STTBC,
                mucluc.sM AS M,
                CAST(0 AS BIT) AS IsHangCha,
                chitiet.*
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT KyHieu,
          QuyetToan =sum(QuyetToan) / @Dvt,
          DuToan =sum(DuToan) / @Dvt,
          TuChi =sum(fTuChi) / @Dvt,
          TuChi2 =sum(TuChi2) / @Dvt
   FROM
     (SELECT mucluc.sKyHieu AS KyHieu,
             QuyetToan =0,
             DuToan =0,
             fTuChi,
             TuChi2 =0
      FROM NS_SKT_ChungTuChiTiet chitiet
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.iID_MLSKT = mucluc.iID_MLSKT AND mucluc.iNamLamViec = @NamLamViec
      WHERE chitiet.iNamLamViec = @NamLamViec
        AND chitiet.iLoai in (select * from f_split(@Loai))
        AND chitiet.iLoaiChungTu = @LoaiChungTu
        AND (@IdDonVi IS NULL
             OR chitiet.iID_MaDonVi in
               (SELECT *
                FROM f_split(@IdDonVi)))
     

      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) AS KyHieu,
                       QuyetToan =0,
                       DuToan = 0,
                       TuChi = 0,
                       TuChi2 = TuChi
      FROM f_skt_dutoan(@NamLamViec, @IdDonVi, CAST(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) IS NOT NULL ) AS a
   GROUP BY KyHieu --order by Tuchi
 ) chitiet ON mucluc.sKyHieu = chitiet.KyHieu
WHERE chitiet.KyHieu IS NOT NULL
  AND mucluc.iNamLamViec = @NamLamViec
ORDER BY sSTTBC END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1]    Script Date: 28/10/2022 1:52:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1] 
@Loai varchar(MAX),
@IdDonVi varchar(MAX),
@NamLamViec int, 
@Dvt int,
@LoaiChungTu int 
AS
BEGIN
SELECT DISTINCT mucluc.iID AS Id,
                mucluc.iID_MLSKT AS IDMucLuc,
                mucluc.sSTT AS STT,
                mucluc.sMoTa AS MoTa,
                mucluc.iID_MLSKTCha AS IdParent,
                mucluc.sSTTBC AS STTBC,
                mucluc.sM AS M,
                CAST(0 AS BIT) AS IsHangCha,
                chitiet.*
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT KyHieu ,
          QuyetToan =sum(QuyetToan)/@Dvt ,
          DuToan =sum(DuToan)/@Dvt ,
          TuChi =sum(fTuChi)/@Dvt ,
          TuChi2 =sum(TuChi2)/@Dvt
   FROM
     (SELECT chitiet.sKyHieu AS KyHieu,
             QuyetToan = 0 ,
             DuToan = 0 ,
             fTuChi = SUM(ISNULL(chitiet.fMuaHangCapHienVat, 0) + ISNULL(chitiet.fPhanCap, 0)),
             TuChi2 = 0
      FROM NS_SKT_ChungTuChiTiet chitiet
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.iID_MLSKT = mucluc.iID_MLSKT AND mucluc.iNamLamViec = @NamLamViec
      WHERE  chitiet.iNamLamViec=@NamLamViec
        AND chitiet.iLoai in (SELECT * FROM f_split(@Loai))
        AND chitiet.iLoaiChungTu = @LoaiChungTu
        AND (@IdDonVi IS NULL
             OR chitiet.iID_MaDonVi in
               (SELECT *
                FROM f_split(@IdDonVi)))
	 GROUP BY chitiet.sKyHieu

      
      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) AS KyHieu ,
                       QuyetToan = 0 ,
                       DuToan = 0 ,
                       TuChi = 0 ,
                       TuChi2 = TuChi
      FROM(
		SELECT iID_MaDonVi AS Id_DonVi,
		   XauNoiMa,
		   TuChi =sum(TuChi)
			FROM
			  (SELECT XauNoiMa,
					  iID_MaDonVi,
					  TuChi
				FROM
					(SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG, --MoTa,
					iID_MaDonVi,
					TuChi =sum(ISNULL(fHangMua, 0) + ISNULL(fHangNhap, 0) + ISNULL(fPhanCap, 0))
						FROM NS_DTDauNam_ChungTuChiTiet
						WHERE iNamLamViec=@NamLamViec
						AND iLoaiChungTu = @LoaiChungTu
						AND iLoai=3
						AND (@IdDonVi IS NULL
								OR iID_MaDonVi in
								(SELECT *
								FROM f_split(@IdDonVi)))
						GROUP BY sLNS,
								sL,
								sK,
								sM,
								sTM,
								sTTM,
								sNG,
								iID_MaDonVi) AS dt) AS a
		WHERE XauNoiMa IS NOT NULL
		GROUP BY iID_MaDonVi,
					XauNoiMa
		HAVING sum(TuChi)<>0
		  ) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) IS NOT NULL ) AS a
   GROUP BY KyHieu --order by Tuchi
 ) chitiet ON mucluc.sKyHieu = chitiet.KyHieu
WHERE chitiet.KyHieu IS NOT NULL
  AND mucluc.iNamLamViec = @NamLamViec
ORDER BY sSTTBC END
;
;
;
-- exec [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]  '4', '112',2022,1000,'2'
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop_1]    Script Date: 28/10/2022 1:52:37 PM ******/
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
	DECLARE @CountCanCu int;

	SELECT @CountCanCu = count(*) 
	FROM NS_CauHinh_CanCu ctct 
	WHERE iNamCanCu = @NamLamViec - 1
	AND iNamLamViec = @NamLamViec
	AND iID_MaChucNang = 'BUDGET_ESTIMATE'
	AND sModule = 'BUDGET_DEMANDCHECK_PLAN'
	
if (@CountCanCu > 0) 
SELECT chitiet.*, mlns.iID_MLNS AS MlnsId, iID_MLNS_Cha AS MlnsIdParent FROM (	
SELECT LNS1 = Left(sLNS, 1),
       LNS3 = Left(sLNS, 3),
       LNS5 = Left(sLNS, 5),
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sMoTa AS MoTa ,
       sXauNoiMa AS XauNoiMa ,
       QuyetToan =sum(ISNULL(QuyetToan, 0))/@DonViTinh ,
       DuToan =sum(isnull(DuToan, 0))/@DonViTinh ,
       TuChi =sum(TuChi)/@DonViTinh ,
	   UocThucHien =sum(fUocThucHien)/@DonViTinh
FROM
  ( 
 
 SELECT ctct.sLNS,
        ctct.sL,
        ctct.sK,
        ctct.sM,
        ctct.sTM,
        ctct.sTTM,
        ctct.sNG,
        ctct.sMoTa,
        ctct.sXauNoiMa,
        QuyetToan = 0,
        DuToan = ctctcc.fTuChi,
        CASE
            WHEN @LoaiChungTu = '1' THEN ctct.fTuChi
            WHEN @LoaiChungTu = '2' THEN ctct.fHangNhap + ctctcc.fHangMua + ctctcc.fPhanCap
            ELSE 0
        END AS TuChi ,
		fUocThucHien
   FROM NS_DTDauNam_ChungTuChiTiet ctct
   JOIN NS_DTDauNam_ChungTuChiTiet_CanCu ctctcc ON ctctcc.sXauNoiMa = ctct.sXauNoiMa
   WHERE ctct.iNamLamViec = @NamLamViec
     AND ctct.iLoai = 3
     AND ctct.iLoaiChungTu = @LoaiChungTu
     AND (@IdDonvi IS NULL OR ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonvi)))-- lay can cu quyet toan, du toan

   UNION ALL SELECT LNS,
                    L,
                    K,
                    M,
                    TM,
                    TTM,
                    NG,
                    MoTa,
                    XauNoiMa,
                    QuyetToan,
                    DuToan,
                    TuChi = 0,
					UocThucHien = 0
   FROM f_skt_dulieu(@NamLamViec, @IdDonvi, @LoaiChungTu)) AS dt
GROUP BY sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sMoTa,
         sXauNoiMa
HAVING --sum(QuyetToan) <> 0
 --or sum(DuToan) <> 0
 --or sum(TuChi) <> 0
 sum(TuChi) <> 0
OR sum(fUocThucHien) <> 0) chitiet  
LEFT JOIN (select * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns on chitiet.XauNoiMa = mlns.sXauNoiMa


else


SELECT chitiet.*, mlns.iID_MLNS AS MlnsId, iID_MLNS_Cha AS MlnsIdParent FROM (	
SELECT LNS1 = Left(sLNS, 1),
       LNS3 = Left(sLNS, 3),
       LNS5 = Left(sLNS, 5),
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sMoTa AS MoTa ,
       sXauNoiMa AS XauNoiMa ,
       QuyetToan =sum(ISNULL(QuyetToan, 0))/@DonViTinh ,
       DuToan =sum(isnull(DuToan, 0))/@DonViTinh ,
       TuChi =sum(TuChi)/@DonViTinh ,
	   UocThucHien =sum(fUocThucHien)/@DonViTinh
FROM
  ( 
 
 SELECT sLNS,
        sL,
        sK,
        sM,
        sTM,
        sTTM,
        sNG,
        sMoTa ,
        sXauNoiMa ,
        QuyetToan = 0,
        DuToan = 0,
        CASE
            WHEN @LoaiChungTu = '1' THEN fTuChi
            WHEN @LoaiChungTu = '2' THEN fHangNhap + fHangMua + fPhanCap
            ELSE 0
        END AS TuChi ,
		fUocThucHien
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec=@NamLamViec
     AND iLoai=3
     AND iLoaiChungTu = @LoaiChungTu
     AND (@IdDonvi IS NULL
          OR iID_MaDonVi in
            (SELECT *
             FROM f_split(@IdDonvi)))-- lay can cu quyet toan, du toan

   UNION ALL SELECT LNS,
                    L,
                    K,
                    M,
                    TM,
                    TTM,
                    NG,
                    MoTa,
                    XauNoiMa,
                    QuyetToan,
                    DuToan,
                    TuChi = 0,
					UocThucHien = 0
   FROM f_skt_dulieu(@NamLamViec, @IdDonvi, @LoaiChungTu)) AS dt
GROUP BY sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sMoTa,
         sXauNoiMa
HAVING 
--sum(QuyetToan) <> 0
--or sum(DuToan) <> 0
--or sum(TuChi) <> 0
SUM(TuChi) <> 0
OR SUM(fUocThucHien) <> 0) chitiet
LEFT JOIN (select * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns on chitiet.XauNoiMa = mlns.sXauNoiMa

END
;
;
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 28/10/2022 1:52:37 PM ******/
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
update NS_DTDauNam_ChungTu set bDaTongHop = 0 
where iNamLamViec = @YearOfWork 
		and iNamNganSach = @YearOfBudget 
		and iID_MaNguonNganSach = @BudgetSource
		and iLoaiChungTu = @LoaiChungTu;
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
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_index_2]    Script Date: 28/10/2022 1:52:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_index_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@ListIdChungTu nvarchar(max),
	@Lns nvarchar(max)
AS
BEGIN

	DECLARE @TenDonVi nvarchar(max);
	SELECT 
		@TenDonVi = (SELECT sTenDonVi FROM DonVi WHERE iID_MaDonVi = @AgencyId AND iNamLamViec = @YearOfWork AND iTrangThai = 1)

	SET NOCOUNT ON;
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
                @AgencyId AS IdDonVi,
                @TenDonVi AS TenDonVi,
                chitiet.TuChi AS ChiTiet,
				chitiet.UocThucHien,
                chitiet.HangNhap,
                chitiet.HangMua,
                chitiet.PhanCap,
                chitiet.ChuaPhanCap,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT sXauNoiMa,
          SUM(fDuPhong) AS DuPhong,
		  SUM(fUocThucHien) AS UocThucHien,
          SUM(fTuChi) AS TuChi,
          SUM(fHangNhap) AS HangNhap,
          SUM(fHangMua) AS HangMua,
          SUM(fPhanCap) AS PhanCap,
          SUM(fChuaPhanCap) AS ChuaPhanCap
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND ((@Loai = 0
           AND iID_MaDonVi = @AgencyId AND @ListIdChungTu <> '' AND iID_CTDTDauNam IN (SELECT * from f_split(@ListIdChungTu)))
          OR (@Loai = 1
              AND iID_MaDonVi <> @AgencyId AND @ListIdChungTu <> '' AND iID_CTDTDauNam IN (SELECT * from f_split(@ListIdChungTu))))
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach = @BudgetSource
     AND iLoaiChungTu = @LoaiChungTu
   GROUP BY sXauNoiMa) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
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
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS]    Script Date: 1/25/2022 8:16:57 AM ******/
SET ANSI_NULLS ON
;
;
;
GO


