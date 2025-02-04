/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 01/12/2023 9:22:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_donvi_thang]    Script Date: 01/12/2023 9:22:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_donvi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_donvi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1]    Script Date: 30/11/2023 5:00:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]    Script Date: 30/11/2023 5:00:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]    Script Date: 30/11/2023 5:00:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]    Script Date: 30/11/2023 5:00:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dutoan_lns]    Script Date: 30/11/2023 5:00:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_dutoan_lns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_dutoan_lns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dutoan_lns]    Script Date: 30/11/2023 5:00:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_skt_dutoan_lns] (
@NamLamViec int,
@id_donvi nvarchar(MAX), 
@LoaiChungTu nvarchar(MAX),
@lns varchar(MAX))
RETURNS 
TABLE 
AS 
RETURN
SELECT iID_MaDonVi AS Id_DonVi,
       XauNoiMa,
       TuChi = SUM(TuChi)
FROM
  (SELECT XauNoiMa,
          iID_MaDonVi,
          TuChi
   FROM
     (SELECT sXauNoiMa XauNoiMa, iID_MaDonVi, TuChi = SUM(fTuChi)
      FROM NS_DTDauNam_ChungTuChiTiet
      WHERE iNamLamViec=@NamLamViec
        AND iLoaiChungTu = @LoaiChungTu
        AND iLoai = 3
		AND (ISNULL(@lns, '') = '' OR sLNS in (SELECT * FROM f_split(@lns)))
        AND (@id_donvi IS NULL
             OR iID_MaDonVi IN
               (SELECT *
                FROM f_split(@id_donvi)))
      GROUP BY sXauNoiMa,
               iID_MaDonVi) AS dt) AS a
WHERE XauNoiMa IS NOT NULL
GROUP BY iID_MaDonVi, XauNoiMa
HAVING SUM(TuChi) <> 0
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]    Script Date: 30/11/2023 5:00:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@IdChungTu varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLNS as iID_MLNS,
       iID_MLNS_Cha as iID_MLNS_Cha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   bHangCha,
       sLNS,
       sNG,
	   sTNG,
       ConCat(' ', ' - ',sMoTa) as sMoTa,
	   sXauNoiMa,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
				ml.sLNS,
                ml.sNG,
				ml.sTNG,
                ml.sMoTa,
				ml.sXauNoiMa,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                --IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_DTDauNam_ChungTuChiTiet ct
   Inner JOIN NS_DTDauNam_ChungTu chungtu ON ct.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
   RIGHT JOIN NS_MucLucNganSach ml ON ct.sXauNoiMa = ml.sXauNoiMa
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoaiChungTu = 1
	 AND ((@bTongHop = 0 and chungtu.sDSSoChungTuTongHop is  null)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop ))
	 AND (@IdChungTu is null or ct.iID_CTDTDauNam in (select * from f_split(@IdChungTu)))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec AND iLoai=1) AS dt_dv ON A.iID_MaDonVi=dt_dv.id		--th�m iLoai = 1
GROUP BY iID_MLNS,
         iID_MLNS_Cha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
		 bHangCha,
		 sLNS,
         sNG,
         sTNG,
         sMoTa,
		 sXauNoiMa
		 order by sNG, sTNG
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]    Script Date: 30/11/2023 5:00:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLNS as  iID_MLNS,
       iID_MLNS_Cha as iID_MLNS_Cha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   dt_dv.iLoai,
	   bHangCha,
	   sLNS,
       sNG,
       sMoTa,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
				ml.sLNS,
                ml.sNG,
                ml.sMoTa,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_DTDauNam_ChungTuChiTiet ct
   LEFT JOIN NS_DTDauNam_ChungTu chungtu ON ct.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
   RIGHT JOIN NS_MucLucNganSach ml ON ct.sXauNoiMa = ml.sXauNoiMa
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoaiChungTu = 1
	 AND ((@bTongHop = 0 and chungtu.bDaTongHop is not null)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
	WHERE dt_dv.iLoai != 0
GROUP BY iID_MLNS,
         iID_MLNS_Cha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
		 bHangCha,
		 sLNS,
         sNG,
         sMoTa,
		 iLoai
		 order by iID_MLNS
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]    Script Date: 30/11/2023 5:00:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1] 
@Loai varchar(MAX),
@IdDonVi varchar(MAX),
@NamLamViec int, 
@Dvt int,
@LoaiChungTu int,
@Lns varchar(MAX)
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
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.sKyHieu = mucluc.sKyHieu AND mucluc.iNamLamViec = @NamLamViec
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
      FROM f_skt_dutoan_lns(@NamLamViec, @IdDonVi, CAST(@LoaiChungTu AS nvarchar(MAX)), @lns) AS a
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1]    Script Date: 30/11/2023 5:00:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all_1] 
@Loai varchar(MAX),
@IdDonVi varchar(MAX),
@NamLamViec int, 
@Dvt int,
@LoaiChungTu int,
@Lns varchar(MAX)
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
						AND (ISNULL(@Lns, '') = '' OR sLNS in (SELECT * FROM f_split(@Lns)))
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
;
GO

/****** Object:  StoredProcedure [dbo].[sp_qt_donvi_thang]    Script Date: 01/12/2023 9:22:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_donvi_thang]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@QuarterMonth nvarchar(100),
	@QuarterMonthType int
AS
BEGIN
	SELECT dv.*
	FROM
	  (SELECT DISTINCT iID_MaDonVi
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
	     AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND (@QuarterMonth IS NULL
			  OR iThangQuy in
				(SELECT *
				 FROM f_split(@QuarterMonth))) 
		AND iThangQuyLoai = @QuarterMonthType)AS ct -- lay ten don vi

	LEFT JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	ORDER BY iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 01/12/2023 9:22:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@LNS nvarchar(max),
	@Dvt int,
	@IsInTongHop bit, 
	@IKhoi int
AS
BEGIN
declare @strChungTu nvarchar (500)
set @strChungTu=  (select sTongHop + ',' as 'data()' from NS_QT_ChungTu where  iID_MaDonVi in ( SELECT * FROM f_split(@AgencyId))  FOR XML PATH(''));
	
	SELECT * INTO #tempthongtridonvi
		FROM
		  (SELECT iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
		     AND iNamNganSach = @YearOfBudget
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
			 AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
		   GROUP BY iID_MaDonVi)AS ct 
		-- lay ten don vi
		JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id,
				  iKhoi
		   FROM DonVi
		   WHERE iTrangThai = 1
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
	
	if (@IsInTongHop = 0 OR @strChungTu = '')
		select * from #tempthongtridonvi;
	else if (@IsInTongHop = 1 AND EXISTS (SELECT * FROM #tempthongtridonvi where iKhoi is not null))
		select * from #tempthongtridonvi where @IKhoi = -1 OR iKhoi = @IKhoi;
	else

	SELECT *
		FROM
		  (SELECT ctct.iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet ctct 
		   INNER JOIN ns_qt_Chungtu ct on  ctct.iID_QTChungTu =  ct.iID_QTChungTu 
		   WHERE ctct.iNamLamViec = @YearOfWork
			 AND ctct.iNamNganSach = @YearOfBudget
			 AND ctct.iID_MaNguonNganSach = @BudgetSource
			 --AND (@AgencyId IS NULL OR ctct.iID_MaDonVi in  (select DonVi.iID_MaDonVi from DonVi  where DonVi.iID_Parent in ( SELECT * FROM f_split(@AgencyId))))
			 AND ct.bdatonghop = 1
			 AND (@QuarterMonth IS NULL OR ctct.iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
			 AND ct.sSoChungTu in (select * from f_split(Replace(@strChungTu, ' ', '')))
		   GROUP BY ctct.iID_MaDonVi)AS ct 
		-- lay ten don vi
		--LEFT JOIN
		INNER JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id,
				  iKhoi
		   FROM DonVi
		   WHERE iTrangThai = 1
		     AND (@IKhoi = -1 OR iKhoi = @IKhoi)
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
END
;
;
;
GO
