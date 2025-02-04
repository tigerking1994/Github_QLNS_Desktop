/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi]    Script Date: 24/04/2023 4:19:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_quyetoannam_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_quyetoannam_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi]    Script Date: 24/04/2023 4:19:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_quyetoannam_donvi]
	@YearOfWork int,
	@AgencyId nvarchar(100),
	@LNS nvarchar(max),
	@DataType int,
	@Dvt int
AS
BEGIN

	SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct.sXauNoiMa,
		   mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sMoTa,
		   iID_MaDonVi,
		   TenDonVi,
		   ChiTieuNamNay = SUM(ChiTieuNamNay) / @Dvt,
		   ChiTieuNamSau = SUM(ChiTieuNamSau) / @Dvt,
		   QuyetToan = SUM(QuyetToan) / @Dvt INTO #tblQuyetToanNamDonVi
	FROM
	  (-- DU TOAN NAM TRUOC
	  SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai = 1,
			ChiTieuNamNay = CASE
							   WHEN @DataType=1 THEN fTuChi
							   ELSE fHienVat
						    END,
			ChiTieuNamSau = 0,
			QuyetToan = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = 1
		 --AND iPhanCap = 1
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
		 AND IDuLieuNhan = 0 
		 
	   -- DU TOAN NAM TRUOC DA CAP
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai = 1,
			ChiTieuNamNay = 0,
			ChiTieuNamSau = CASE
								WHEN @DataType = 1 THEN fTuChi
								ELSE fHienVat
							END,
			QuyetToan =0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = 5
		 --AND iPhanCap = 1
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
		 AND IDuLieuNhan = 0 
		 
	   -- DU TOAN NAM NAY
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai = 2,
			ChiTieuNamNay = CASE
								WHEN @DataType = 1 THEN fTuChi
								ELSE fHienVat
							END,
			ChiTieuNamSau = 0,
			QuyetToan = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = 2
		 --AND iPhanCap=1
		 AND (@AgencyId IS NULL  OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
		 AND IDuLieuNhan = 0
		 
	   -- DU TOAN CHUYEN NAM SAU
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai = 2,
			ChiTieuNamNay = 0,
			ChiTieuNamSau = CASE
								WHEN @DataType=1 THEN fTuChi
								ELSE fHienVat
							END,
			QuyetToan = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = 4
		 --AND iPhanCap = 1
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
		 AND IDuLieuNhan = 0
		 
	   --so da quyết toán nam truoc
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai = 1,
			ChiTieuNamNay = 0,
			ChiTieuNamSau = 0,
			QuyetToan = CASE
							WHEN @DataType = 1 THEN fTuChi_PheDuyet
							ELSE cast(0 AS float)
						END
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
		 AND iNamNganSach in (1, 5)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT *  FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT *  FROM f_split(@LNS)))
		 
	   -- QUYET TOAN NAM NAY
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai=2,
			ChiTieuNamNay = 0,
			ChiTieuNamSau = 0,
			QuyetToan = CASE
							WHEN @DataType = 1 THEN fTuChi_PheDuyet
							ELSE cast(0 AS float)
						END
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach in (2)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT *  FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS))) )AS ct  
	--mota lns
	LEFT JOIN
	  (SELECT sMoTa,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha
	   FROM NS_MucLucNganSach
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork) AS mlns ON mlns.sXauNoiMa = ct.sXauNoiMa 
    --donvi
	LEFT JOIN
	  (SELECT iID_MaDonVi AS dv_id,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
	GROUP BY Loai, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct.sXauNoiMa,
			 iID_MaDonVi, TenDonVi,
			 mlns.iID_MLNS,
		     mlns.iID_MLNS_Cha,
			 mlns.sMoTa
	HAVING SUM(ChiTieuNamNay) <> 0
	OR SUM(ChiTieuNamSau) <> 0
	OR SUM(QuyetToan) <> 0;

	WITH LNSTreeParent AS
	  (SELECT sLNS,
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
			sXauNoiMa,
			iID_MLNS,
			iID_MLNS_Cha,
			cast(iID_MaDonVi AS nvarchar(500)) AS IdDonVi,
			cast(TenDonVi AS nvarchar(500)) AS TenDonVi,
			cast(ChiTieuNamNay AS float) AS ChiTieuNamNay,
			cast(ChiTieuNamSau AS float) AS ChiTieuNamSau,
			cast(QuyetToan AS float) AS QuyetToan,
			sMoTa,
			cast(0 AS bit) AS IsHangCha
	   FROM #tblQuyetToanNamDonVi
	   UNION ALL 
	   SELECT mlnsParent.sLNS,
			mlnsParent.sL,
			mlnsParent.sK,
			mlnsParent.sM,
			mlnsParent.sTM,
			mlnsParent.sTTM,
			mlnsParent.sNG,
			mlnsParent.sTNG,
			mlnsParent.sTNG1,
			mlnsParent.sTNG2,
			mlnsParent.sTNG3,
			mlnsParent.sXauNoiMa,
			mlnsParent.iID_MLNS,
			mlnsParent.iID_MLNS_Cha,
			cast('' AS nvarchar(500)) AS IdDonVi,
			cast('' AS nvarchar(500)) AS TenDonVi,
			cast(0 AS float) AS ChiTieuNamNay,
			cast(0 AS float) AS ChiTieuNamSau,
			cast(0 AS float) AS QuyetToan,
			mlnsParent.sMoTa,
			cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork)
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sXauNoiMa;
	DROP TABLE #tblQuyetToanNamDonVi;
END
;
;
;
GO
