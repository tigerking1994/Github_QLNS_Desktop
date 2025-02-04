/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop_2_year]    Script Date: 18/08/2022 1:45:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_chungtu_tonghop_2_year]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop_2_year]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop_2_year]    Script Date: 18/08/2022 1:45:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop_2_year]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@YearOfBudget2 int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@VoucherDate datetime,
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@Dvt int
AS
BEGIN
	SELECT iID_MLNS,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   HienVat = SUM(HienVat) / @Dvt,
		   SoNguoi = SUM(SoNguoi),
		   SoNgay = SUM(SoNgay),
		   SoLuot = SUM(SoLuot)
		   into #tblData
	FROM
	  (-- dự toán
	  SELECT iID_MLNS,
			ChiTieu = TuChi,
			TuChi2 = 0,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_dutoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @EstimateAgencyId, @lns) 

	   --so da quyết toán
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = TuChi,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @lns) 
	   -- quyet toan dot nay
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi,
			HienVat,
			SoNguoi,
			SoNgay,
			SoLuot
	   FROM f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @lns)
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = TuChi,
			TuChi2 = 0,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_dutoan_tonghop(@YearOfWork, @YearOfBudget2, @BudgetSource, @VoucherDate, @EstimateAgencyId, @lns) 

	   --so da quyết toán
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = TuChi,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_quyettoan_tonghop(@YearOfWork, @YearOfBudget2, @BudgetSource, @QuarterMonthBefore, @AgencyId, @lns) 
	   -- quyet toan dot nay
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi,
			HienVat,
			SoNguoi,
			SoNgay,
			SoLuot
	   FROM f_quyettoan_tonghop(@YearOfWork, @YearOfBudget2, @BudgetSource, @QuarterMonth, @AgencyId, @lns)
	) AS ct
	GROUP BY iID_MLNS;

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
		isnull(dt.TuChi2, 0) + isnull(dt.TuChi, 0) as ThucChi,
		isnull(dt.HienVat, 0) as HienVat,
		isnull(dt.SoNguoi, 0) as SoNguoi,
		isnull(dt.SoNgay, 0) as SoNgay,
		isnull(dt.SoLuot, 0) as SoLuot
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
					FROM f_split(@LNS)) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblData dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.TuChi2 <> 0 OR dt.HienVat <> 0 OR dt.SoNgay <> 0 OR dt.SoNguoi <> 0 OR dt.SoLuot <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblData;
END
;
;
;
;
GO
