UPDATE NS_QT_ChungTu
SET sTongHop = NULL
WHERE sTongHop = sSoChungTu

/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi]    Script Date: 20/04/2023 4:35:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_quyetoannam_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_quyetoannam_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_lns_thang]    Script Date: 20/04/2023 4:35:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_lns_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_lns_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 20/04/2023 4:35:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_tonghop_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_tonghop_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi]    Script Date: 20/04/2023 4:35:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi]    Script Date: 20/04/2023 4:35:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	DECLARE @IsDonViCha bit;
	IF @EstimateAgencyId = @AgencyId
		SET @IsDonViCha = 0
	ELSE
		SET @IsDonViCha = 1
	SELECT sLNS,
		   iID_MLNS,
		   TenDonVi,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt 
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			ChiTieu = fTuChi,
			TuChi2 = 0,
			TuChi = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iID_DTChungTu in 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				AND cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
		)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		 AND (@LNS IS NULL OR sLNS like @LNS + '%')
		 AND IDuLieuNhan = 0
		 
	   --số đã quyết toán
	   UNION ALL 
	   SELECT sLNS,
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu =0,
			TuChi2 = fTuChi_PheDuyet,
			TuChi = 0
	   FROM f_quyettoan(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		 
	   --quyết toán đợt này
	   UNION ALL 
	   SELECT sLNS,
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi2 = 0,
			fTuChi_PheDuyet AS TuChi
	   FROM f_quyettoan(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY 
			 sLNS,
			 TenDonVi,
			 iID_MLNS
	HAVING SUM(tuchi) <> 0
	OR SUM(TuChi2) <> 0
	OR SUM(ChiTieu) <> 0;

	select distinct sLNS into #tblLNS from #tblEstimateSettlement;
	SELECT mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
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
		mlns.sXauNoiMa,
		mlns.sMoTa,
		mlns.bHangCha as IsHangCha,
		isnull(dt.ChiTieu, 0) as ChiTieu,
		isnull(dt.TuChi2, 0) as TuChi2,
		isnull(dt.TuChi, 0) as TuChi,
		TenDonVi
	FROM (
		SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iTrangThai=1
			 AND iNamLamViec = @YearOfWork
			 AND sLNS in
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							CAST(sLNS AS nvarchar(10)) LNS
					FROM #tblLNS) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblEstimateSettlement dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.TuChi2 <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblEstimateSettlement, #tblLNS;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 20/04/2023 4:35:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_tonghop_thang]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	SELECT dv.iID_MaDonVi,
		   TenDonVi,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   Thang1 = SUM(Thang1) / @Dvt,
		   Thang2 = SUM(Thang2) / @Dvt,
		   Thang3 = SUM(Thang3) / @Dvt,
		   Thang4 = SUM(Thang4) / @Dvt,
		   Thang5 = SUM(Thang5) / @Dvt,
		   Thang6 = SUM(Thang6) / @Dvt,
		   Thang7 = SUM(Thang7) / @Dvt,
		   Thang8 = SUM(Thang8) / @Dvt,
		   Thang9 = SUM(Thang9) / @Dvt,
		   Thang10 = SUM(Thang10) / @Dvt,
		   Thang11 = SUM(Thang11) / @Dvt,
		   Thang12 = SUM(Thang12) / @Dvt
	FROM
	  (--chi tieu theo don vi
	  SELECT 
			iID_MaDonVi,
			ChiTieu = fTuChi,
			TuChi = 0,
			Thang1 = 0,
			Thang2 = 0,
			Thang3 = 0,
			Thang4 = 0,
			Thang5 = 0,
			Thang6 = 0,
			Thang7 = 0,
			Thang8 = 0, 
			Thang9 = 0,
			Thang10 = 0,
			Thang11 = 0,
			Thang12 = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS like @LNS + '%')
		 AND IDuLieuNhan = 0
		 AND iID_DTChungTu IN (
			SELECT iID_DTChungTu
			FROM NS_DT_ChungTu
			WHERE (sSoQuyetDinh IS NOT NULL OR sSoQuyetDinh <> '')
				AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate AS date))
		 
	   UNION ALL 
	   SELECT 
			iID_MaDonVi,
			ChiTieu = 0,
			TuChi = fTuChi_PheDuyet,
			Thang1 = 0,
			Thang2 = 0,
			Thang3 = 0,
			Thang4 = 0,
			Thang5 = 0,
			Thang6 = 0,
			Thang7 = 0,
			Thang8 = 0, 
			Thang9 = 0,
			Thang10 = 0,
			Thang11 = 0,
			Thang12 = 0
	   FROM f_quyettoan(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)

	   UNION ALL 
	   select 
			iID_MaDonVi,
			ChiTieu = 0,
			TuChi = 0,
			isnull([1], 0) AS Thang1,
			isnull([2], 0) AS Thang2,
			isnull([3], 0) AS Thang3,
			isnull([4], 0) AS Thang4,
			isnull([5], 0) AS Thang5,
			isnull([6], 0) AS Thang6,
			isnull([7], 0) AS Thang7,
			isnull([8], 0) AS Thang8,
			isnull([9], 0) AS Thang9,
			isnull([10], 0) AS Thang10,
			isnull([11], 0) AS Thang11,
			isnull([12], 0) AS Thang12
			from (
				select sLNS, iID_MLNS, iID_MaDonVi, isnull(fTuChi_PheDuyet, 0) as TuChi, iThangQuy 
				FROM NS_QT_ChungTuChiTiet
				WHERE iID_QTChungTu in
					(SELECT iID_QTChungTu
					FROM NS_QT_ChungTu
					WHERE iNamLamViec = @YearOfWork
						AND INamNganSach = @YearOfBudget
						AND iID_MaNguonNganSach = @BudgetSource
						AND iThangQuy in (select * from f_split(@QuarterMonth))
						)
					AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					AND (@LNS IS NULL OR sLNS like @lns + '%')
			) as data
			pivot 
			(
				SUM(TuChi) for iThangQuy IN ( [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12] )
			) as Thang
	) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY dv.iID_MaDonVi,
			 TenDonVi
	HAVING SUM(TuChi) <> 0
	OR SUM(ChiTieu) <> 0
	OR SUM(Thang1) <> 0
	OR SUM(Thang2) <> 0
	OR SUM(Thang3) <> 0
	OR SUM(Thang4) <> 0
	OR SUM(Thang5) <> 0
	OR SUM(Thang6) <> 0
	OR SUM(Thang7) <> 0
	OR SUM(Thang8) <> 0
	OR SUM(Thang9) <> 0
	OR SUM(Thang10) <> 0
	OR SUM(Thang11) <> 0
	OR SUM(Thang12) <> 0
	ORDER BY dv.iID_MaDonVi

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_lns_thang]    Script Date: 20/04/2023 4:35:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_lns_thang]
	@YearOfWork int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthType int
AS
BEGIN

	SELECT DISTINCT sLNS AS LNS,
		LEFT(sLNS, 1) AS LNS1,
		LEFT(sLNS, 3) AS LNS3
	FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec = @YearOfWork
	  AND iID_MaNguonNganSach = @BudgetSource
	  AND iID_MaDonVi in (SELECT * FROM f_split(@AgencyId))
	  AND (@QuarterMonth IS NULL
		   OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
	  AND (@QuarterMonthType IS NULL
		   OR iThangQuyLoai = @QuarterMonthType)
	  AND (fTuChi_DeNghi <> 0
		   OR fTuChi_PheDuyet <> 0);
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi]    Script Date: 20/04/2023 4:35:28 PM ******/
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
		 AND iPhanCap=1
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
