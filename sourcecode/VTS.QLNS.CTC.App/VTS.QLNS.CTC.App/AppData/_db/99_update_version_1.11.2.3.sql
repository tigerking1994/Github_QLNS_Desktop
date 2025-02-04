/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 27/06/2022 3:47:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_capphat_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 27/06/2022 3:47:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_luong_ntn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_luong_ntn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 27/06/2022 6:48:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_2]    Script Date: 27/06/2022 7:03:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_index_2]    Script Date: 27/06/2022 7:03:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_donvi0_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 27/06/2022 7:03:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_so_lieu_chi_tiet_mlns_for_fill_budget]    Script Date: 27/06/2022 7:03:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_so_lieu_chi_tiet_mlns_for_fill_budget]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_so_lieu_chi_tiet_mlns_for_fill_budget]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 27/06/2022 7:03:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoandaunam_report_chingansach]    Script Date: 27/06/2022 7:03:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_dutoandaunam_report_chingansach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_dutoandaunam_report_chingansach]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 27/06/2022 7:03:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_dulieu]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_dulieu]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 27/06/2022 7:03:21 PM ******/
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
SELECT iID_MaDonVi AS Id_DonVi,
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sMoTa AS MoTa ,
       XauNoiMa ,
       DuToan =sum(DuToan) ,
       QuyetToan =sum(QuyetToan),
	   UocThucHien = 0
FROM
  (SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG,
          sLNS,
          sL,
          sK,
          sM,
          sTM,
          sTTM,
          sNG,
          sMoTa,
          iID_MaDonVi,
          DuToan =sum(fTuChi),
          QuyetToan=0,
		  UocThucHien = 0
   FROM NS_DT_ChungTuChiTiet
   WHERE
       (SELECT count(*)
        FROM NS_DTDauNam_ChungTuChiTiet
        WHERE iNamLamViec=@NamLamViec-1
          AND iLoai=2
          AND iLoaiChungTu = @LoaiChungTu)=0
     AND iNamLamViec=(@NamLamViec-1)
     AND iID_DTChungTu in
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE iNamLamViec=@NamLamViec-1
          AND iLoai=1)
     AND (@id_donvi IS NULL
          OR iID_MaDonVi in
            (SELECT *
             FROM f_split(@id_donvi)))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sMoTa,
            iID_MaDonVi -- Lấy số dự toán đã đẩy vào làm căn cứ

   UNION ALL SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG,
                    sLNS,
                    sL,
                    sK,
                    sM,
                    sTM,
                    sTTM,
                    sNG,
                    sMoTa,
                    iID_MaDonVi,
                    CASE
                        WHEN @LoaiChungTu = '1' THEN sum(fTuChi)
                        WHEN @LoaiChungTu = '2' THEN sum(fHangNhap) + SUM (fHangMua) + sum(fPhanCap)
                        ELSE 0
                    END AS DuToan,
                    QuyetToan=0,
					UocThucHien = 0
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iLoaiChungTu = @LoaiChungTu
     AND iNamLamViec=(@NamLamViec-1)
     AND (@id_donvi IS NULL
          OR iID_MaDonVi in
            (SELECT *
             FROM f_split(@id_donvi)))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sMoTa,
            iID_MaDonVi
   UNION ALL SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG,
                    sLNS,
                    sL,
                    sK,
                    sM,
                    sTM,
                    sTTM,
                    sNG,
                    sMoTa,
                    iID_MaDonVi,
                    DuToan=0,
                    CASE
                        WHEN @LoaiChungTu = '1' THEN sum(fTuChi)
                        WHEN @LoaiChungTu = '2' THEN sum(fHangNhap) + SUM (fHangMua) + sum(fPhanCap)
                        ELSE 0
                    END AS QuyetToan,
				    UocThucHien = 0
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec=(@NamLamViec-2)
     AND iLoai=1
     AND iLoaiChungTu = @LoaiChungTu
     AND (@id_donvi IS NULL
          OR iID_MaDonVi in
            (SELECT *
             FROM f_split(@id_donvi)))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sMoTa,
            iID_MaDonVi) AS a
WHERE sLNS like '1%'
GROUP BY iID_MaDonVi,
         sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sMoTa ,
         XauNoiMa
HAVING sum(DuToan)<>0
OR sum(QuyetToan)<>0
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoandaunam_report_chingansach]    Script Date: 27/06/2022 7:03:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_dutoandaunam_report_chingansach]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT 
				NEWID() AS Id,
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
                isnull(chitiet.bHangCha, mlns.bHangCha) as bHangCha,
                mlns.sChiTietToi AS ChiTietToi,
                '' AS IdDonVi,
                '' AS TenDonVi,
                isnull(chitiet.fTuChi,0) AS ChiTiet,
                isnull(chitiet.fUocThucHien,0) AS UocThucHien,
                isnull(chitiet.fHangNhap,0) AS HangNhap,
                isnull(chitiet.fHangMua,0) AS HangMua,
                isnull(chitiet.fPhanCap,0) AS PhanCap,
                isnull(chitiet.fChuaPhanCap,0) AS ChuaPhanCap,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT SUM(fTuChi) as fTuChi,
			SUM(fUocThucHien) as fUocThucHien,
			SUM(fHangNhap) as fHangNhap,
			SUM(fHangMua) as fHangMua,
			SUM(fPhanCap) as fPhanCap,
			SUM(fChuaPhanCap) as fChuaPhanCap,
			bHangCha,
			--iID_MaDonVi,
			--sTenDonVi,
			sXauNoiMa
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach =@BudgetSource
     AND (iID_MaDonVi in  (SELECT *  FROM f_split(@AgencyId))
      --     AND bKhoa = 0
		 )
     AND iLoaiChungTu = @LoaiChungTu
	 group by 
	 sXauNoiMa, bHangCha
	 ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
  (SELECT *
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = @YearOfWork )map ON mlns.sXauNoiMa = map.sNS_XauNoiMa
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1 
  AND mlns.bHangChaDuToan IS NOT NULL
 -- and chitiet.fTuChi <> 0
 and(mlns.sLNS = '1'
     OR ((mlns.sLNS like '104%'
          AND @LoaiChungTu = '2')
         OR (mlns.sLNS not like '104%'
             AND @LoaiChungTu = '1')))
ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 27/06/2022 7:03:21 PM ******/
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
        QuyetToan =0 ,
        DuToan =0 ,
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
                    XauNoiMa ,
                    QuyetToan ,
                    DuToan ,
                    TuChi =0 ,
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
HAVING --sum(QuyetToan)<>0
 --or sum(DuToan)<>0
 --or sum(TuChi)<>0
 sum(TuChi)<>0
OR sum(fUocThucHien)<>0
) chitiet  left join (select * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns on chitiet.XauNoiMa = mlns.sXauNoiMa

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_so_lieu_chi_tiet_mlns_for_fill_budget]    Script Date: 27/06/2022 7:03:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_so_lieu_chi_tiet_mlns_for_fill_budget]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@ILoai int,
	@AgencyId nvarchar(4000),
	@LoaiChungTu nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		distinct 
		isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) as Id,
		chitiet.iID_CTDTDauNamChiTiet as IdDb,
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.iID_MLNS_Cha,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sMoTa as MoTa,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.bHangCha as BHangCha,
		chitiet.iID_MaDonVi as IdDonVi,
		chitiet.sTenDonVi as TenDonVi,
		chitiet.fTuChi as ChiTiet,
		chitiet.fTuChi as TuChi,
		chitiet.fHienVat as HienVat,
		chitiet.fHangNhap as HangNhap,
		chitiet.fHangMua as HangMua,
		chitiet.fPhanCap as PhanCap,
		chitiet.fDuPhong as DuPhong,
		chitiet.fUocThucHien as UocThucHien,
		map.sSKT_KyHieu as SKT_KyHieu
	FROM (select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork)mlns
	LEFT JOIN (
		SELECT 
			* 
		 FROM 
			NS_DTDauNam_ChungTuChiTiet  
		 WHERE
		 	 iNamLamViec = @YearOfWork
			 and iLoai = @ILoai
			 and iID_MaDonVi in (SELECT * FROM dbo.f_split(@AgencyId))
			 and bKhoa = 1
			 and iLoaiChungTu = @LoaiChungTu
	) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
	LEFT join 
		(select * from NS_MLSKT_MLNS where iNamLamViec = @YearOfWork) map 
	on mlns.SXauNoiMa = map.SNS_XauNoiMa
	WHERE 
		map.iNamLamViec = @YearOfWork
		AND (isnull(chitiet.fTuChi, 0) > 0 OR isnull(chitiet.fHienVat, 0) > 0 OR isnull(chitiet.fPhanCap, 0) > 0 OR isnull(chitiet.fHangNhap, 0) > 0 OR isnull(chitiet.fHangMua, 0) > 0)
		AND chitiet.iID_MaDonVi is not null
	ORDER BY mlns.sXauNoiMa;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 27/06/2022 7:03:21 PM ******/
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
                chitiet.iID_MaDonVi AS IdDonVi,
                donvi.sTenDonVi AS TenDonVi,
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
LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) donvi on donvi.iID_MaDonVi = chitiet.iID_MaDonVi

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
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_index_2]    Script Date: 27/06/2022 7:03:21 PM ******/
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
           AND iID_MaDonVi = @AgencyId)
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
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_2]    Script Date: 27/06/2022 7:03:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_index_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@Lns nvarchar(max),
	@VoucherId nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) AS Id,
                chitiet.iID_CTDTDauNamChiTiet AS IdDb,
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
                chitiet.iID_MaDonVi AS IdDonVi,
                chitiet.sTenDonVi AS TenDonVi,
                chitiet.fTuChi AS ChiTiet,
				chitiet.fUocThucHien AS UocThucHien,
                chitiet.fHangNhap AS HangNhap,
                chitiet.fHangMua AS HangMua,
                chitiet.fPhanCap AS PhanCap,
                chitiet.fChuaPhanCap AS ChuaPhanCap,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT *
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach =@BudgetSource
     AND (iID_MaDonVi = @AgencyId
          OR (@AgencyId = '00'
              AND bKhoa = 0))
	 AND iID_CTDTDauNam = @VoucherId
     AND iLoaiChungTu = @LoaiChungTu ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
  (SELECT *
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = @YearOfWork ) map ON mlns.sXauNoiMa = map.sNS_XauNoiMa
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1  
  AND mlns.bHangChaDuToan IS NOT NULL
 AND (mlns.sLNS = '1'
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
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 27/06/2022 6:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 21/04/2022
-- Description:	Lấy dữ liệu cho thêm mới bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE
					WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN @SoNgay * GIA_TRI 
					ELSE 0
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		WHERE
			canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		GROUP BY canBoPhuCap.MA_CBO
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 4)) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTri
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
GO

/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 27/06/2022 3:47:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 04/05/2022
-- Description:	Tính năm thâm niên
-- =============================================
CREATE FUNCTION [dbo].[f_luong_ntn]
(
	@NgayNN DATETIME,
	@NgayXN DATETIME,
	@NgayTN DATETIME,
	@ThangTNN int,
	@Thang int,
	@Nam int
)
RETURNS int
AS
BEGIN
	DECLARE @NamThamNien int SET @NamThamNien = 0
	DECLARE @monthDiff int SET @monthDiff = 0
	DECLARE @monthDiff2 int SET @monthDiff2 = 0

	IF (@NgayNN IS NOT NULL)
	BEGIN
		IF (@NgayXN IS NULL AND @NgayTN IS NULL)
		BEGIN
			SET @monthDiff = (@Nam - YEAR(@NgayNN)) * 12 + @Thang - MONTH(@NgayNN) + 1 + @ThangTNN
			IF(@monthDiff / 12 >= 1)
				BEGIN
					SET @NamThamNien = @monthDiff / 12
				END
			ELSE
				BEGIN
					SET @NamThamNien = @monthDiff / 12 - 1
				END
		END

		ELSE
		BEGIN
			IF (@NgayTN IS NULL)
			BEGIN
				SET @monthDiff = (YEAR(@NgayXN) - YEAR(@NgayNN)) * 12 + @Thang - MONTH(@NgayNN) + 1 + @ThangTNN
				IF(@monthDiff / 12 >= 1)
					BEGIN
						SET @NamThamNien = @monthDiff / 12
					END
				ELSE
					BEGIN
						SET @NamThamNien = @monthDiff / 12 - 1
					END
			END

			ELSE
			BEGIN
				DECLARE @Lan1 int SET @Lan1 = 12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN) + 1
				
				IF (@Lan1 < 6)
				BEGIN
					set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + @ThangTNN + 1
					IF(@monthDiff2 / 12 >= 1)
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12
						END
					ELSE
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12 - 1
						END
				END

				ELSE IF (@Lan1 >= 6 AND @Lan1 <= 12)
				BEGIN
					set @monthDiff2 = 12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN + 1
					IF(@monthDiff2 / 12 >= 1)
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12
						END
					ELSE
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12 - 1
						END
				END

				ELSE 
				BEGIN
					set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + 1 + @ThangTNN + @Lan1
					IF(@monthDiff2 / 12 >= 1)
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12
						END
					ELSE
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12 - 1
						END
				END
			END
		END
	END
	RETURN @NamThamNien
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 27/06/2022 3:47:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'NTN,LHT_HS,LHT_TT,PCCV_TT,PCTHD_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,PCTRA_SUM,LUONGTHANG_SUM,PHAITRU_SUM,TM,THANHTIEN,PCNU_TT,HSBL_HS,PCTNVK_HS'
	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CBo			AS MaCanBo,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonVi,
			donVi.Ten_Donvi		AS TenDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			canBo.Thang_TNN		AS Tnn,
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			chucVu.Ma_Cv AS MaChucVu,
			chucVu.Ten_Cv AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			canBo.Ngay_NN AS NgayNhapNguDate,
			canBo.Ngay_XN AS NgayXuatNguDate,
			canBo.Ngay_TN AS NgayTaiNguDate,
			CASE WHEN canBo.Thang_TNN IS NULL THEN 0 ELSE canBo.Thang_TNN END AS ThangTnn
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB=capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
			ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
	)
	SELECT MaDonVi, MaCanBo, TenCanBo, Tnn, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.MaCapBac,
			canBo.CapBac,
			canBo.HSChucVu,
			canBo.MaChucVu,
			canBo.TenChucVu,
			canBo.Stk,
			canBo.NgayNhapNgu,
			canBo.NgayXuatNgu,
			canBo.NgayTaiNgu,
			canBo.NgayNhapNguDate,
			canBo.NgayXuatNguDate,
			canBo.NgayTaiNguDate,
			canBo.ThangTnn,
			canBo.Tnn,
			CASE WHEN bangLuong.MaPhuCap = ' + '''NTN''' + ' THEN dbo.f_luong_ntn(canBo.NgayNhapNguDate, canBo.NgayXuatNguDate, canBo.NgayTaiNguDate, canBo.ThangTnn, 6, 2022) ELSE bangLuong.GiaTri END AS GiaTri,
			bangLuong.MaPhuCap
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt
	WHERE MaCapBac LIKE ''0%''
	ORDER BY HSChucVu DESC, MaCapBac DESC, TenCanBo DESC'
	execute(@Query)
END
GO
