/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]    Script Date: 13/09/2022 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 13/09/2022 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]    Script Date: 13/09/2022 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 13/09/2022 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_dulieu]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_dulieu]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 13/09/2022 3:54:05 PM ******/
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

   --UNION ALL SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG,
   --                 sLNS,
   --                 sL,
   --                 sK,
   --                 sM,
   --                 sTM,
   --                 sTTM,
   --                 sNG,
   --                 sMoTa,
   --                 iID_MaDonVi,
   --                 CASE
   --                     WHEN @LoaiChungTu = '1' THEN sum(fTuChi)
   --                     WHEN @LoaiChungTu = '2' THEN sum(fHangNhap) + SUM (fHangMua) + sum(fPhanCap)
   --                     ELSE 0
   --                 END AS DuToan,
   --                 QuyetToan=0,
			--		UocThucHien = 0
   --FROM NS_DTDauNam_ChungTuChiTiet
   --WHERE iLoaiChungTu = @LoaiChungTu
   --  AND iNamLamViec=(@NamLamViec-1)
   --  AND (@id_donvi IS NULL
   --       OR iID_MaDonVi in
   --         (SELECT *
   --          FROM f_split(@id_donvi)))
   --GROUP BY sLNS,
   --         sL,
   --         sK,
   --         sM,
   --         sTM,
   --         sTTM,
   --         sNG,
   --         sMoTa,
   --         iID_MaDonVi
   UNION ALL SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG,
                    sLNS,
                    sL,
                    sK,
                    sM,
                    sTM,
                    sTTM,
                    sNG,
                    NS_QT_ChungTuChiTiet.sMoTa,
                    NS_QT_ChungTuChiTiet.iID_MaDonVi,
                    DuToan=0,
					SUM(fTuChi_PheDuyet) as QuyetToan,
				    UocThucHien = 0
   --FROM NS_DTDauNam_ChungTuChiTiet
   FROM NS_QT_ChungTuChiTiet
   JOIN NS_QT_ChungTu ON NS_QT_ChungTuChiTiet.iID_QTChungTu = NS_QT_ChungTu.iID_QTChungTu
   WHERE NS_QT_ChungTuChiTiet.iNamLamViec = @NamLamViec - 2
   --WHERE iNamLamViec=(@NamLamViec-2)
     --AND iLoai=1
     --AND iLoaiChungTu = @LoaiChungTu
     AND (@id_donvi IS NULL
          OR NS_QT_ChungTuChiTiet.iID_MaDonVi in
            (SELECT *
             FROM f_split(@id_donvi)))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            NS_QT_ChungTuChiTiet.sMoTa,
            NS_QT_ChungTuChiTiet.iID_MaDonVi) AS a
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
;




GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]    Script Date: 13/09/2022 3:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt] 
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
     (SELECT mucluc.sKyHieu AS KyHieu,
             QuyetToan =0 ,
             DuToan =0 ,
             fTuChi ,
             TuChi2 =0
      FROM NS_SKT_ChungTuChiTiet chitiet
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.iID_MLSKT = mucluc.iID_MLSKT
      WHERE  chitiet.iNamLamViec=@NamLamViec
        AND chitiet.iLoai=@Loai
        AND chitiet.iLoaiChungTu = @LoaiChungTu
        AND (@IdDonVi IS NULL
             OR chitiet.iID_MaDonVi in
               (SELECT *
                FROM f_split(@IdDonVi)))
      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) AS KyHieu ,
                       QuyetToan ,
                       DuToan ,
                       TuChi =0 ,
                       TuChi2 =0
      FROM f_skt_cancu(@NamLamViec, @IdDonVi, cASt(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) IS NOT NULL -- lap chi tiet du toan

      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) AS KyHieu ,
                       QuyetToan =0 ,
                       DuToan =0 ,
                       TuChi =0 ,
                       TuChi2 =TuChi
      FROM f_skt_dutoan(@NamLamViec, @IdDonVi, cASt(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) IS NOT NULL ) AS a
   GROUP BY KyHieu --order by Tuchi
 ) chitiet ON mucluc.sKyHieu = chitiet.KyHieu
WHERE chitiet.KyHieu IS NOT NULL
  AND mucluc.iNamLamViec = @NamLamViec
ORDER BY sSTTBC END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 13/09/2022 3:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop]
	 @NamLamViec int,													
	 @IdDonvi nvarchar(MAX),
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
	--OR iID_MaChucNang = 'BUDGET_SETTLEMENT'
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
HAVING SUM(TuChi) <> 0
OR SUM(DuToan) <> 0
OR SUM(QuyetToan) <> 0
OR SUM(fUocThucHien) <> 0) chitiet  
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
HAVING SUM(QuyetToan) <> 0
OR SUM(TuChi) <> 0
OR SUM(DuToan) <> 0
OR SUM(DuToan) <> 0) chitiet
LEFT JOIN (select * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns on chitiet.XauNoiMa = mlns.sXauNoiMa

END



GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]    Script Date: 13/09/2022 3:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]
@iIdDuAnId uniqueidentifier
AS
BEGIN
	SELECT cp.iID_ChiPhiID, cp.iID_DuAn_ChiPhi, SUM(ISNULL(cp.fTienPheDuyetQDDT, 0)) as GiaTriPheDuyet INTO #tmpDuToanChiPhi
	FROM VDT_DA_DuToan as tbl
	INNER JOIN VDT_DA_DuToan_ChiPhi as cp on tbl.iID_DuToanID = cp.iID_DuToanID
	WHERE tbl.bActive = 1 AND tbl.iID_DuAnID = @iIdDuAnId
	GROUP BY cp.iID_ChiPhiID, cp.iID_DuAn_ChiPhi

	SELECT	
		NEWID() as Id,
		cp.sTenChiPhi as TenChiPhi,
		NULL as IdDuToanChiPhi,
		tmp.iID_ChiPhiID as IdChiPhi,
		NULL AS IdDuToan,
		tmp.GiaTriPheDuyet,
		tmp.iID_DuAn_ChiPhi as IdChiPhiDuAn,
		CAST(0 as bit) as IsHangCha,
		NULL as IsHangCha,
		(CASE WHEN cp.iID_ChiPhi_Parent IS NULL THEN CAST(1 AS bit) ELSE CAST(0 as bit) END) as IsLoaiChiPhi,
		cp.IThuTu,
		cp.iID_ChiPhi_Parent as IdChiPhiDuAnParent,
		CAST(1 as bit) as IsDuAnChiPhiOld,
		CAST(1 as bit) as IsEditHangMuc,
		CAST(cp.iID_DuAn_ChiPhi as nvarchar(max)) as MaOrder,
		NULL as FGiaTriDieuChinh,
		NULL as GiaTriTruocDieuChinh,
		CAST(1 as int) as PhanCap,
		cp.iID_DuAn_ChiPhi as ChiPhiId,
		cp.sMaChiPhi as MaChiPhi
	FROM #tmpDuToanChiPhi as tmp
	INNER JOIN VDT_DM_DuAn_ChiPhi as cp on tmp.iID_DuAn_ChiPhi = cp.iID_DuAn_ChiPhi
	ORDER BY cp.iThuTu

	DROP TABLE #tmpDuToanChiPhi
END
GO
