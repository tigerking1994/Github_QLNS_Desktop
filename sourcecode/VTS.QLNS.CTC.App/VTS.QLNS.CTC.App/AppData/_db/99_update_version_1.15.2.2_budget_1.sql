/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_lns_sochungtu]    Script Date: 12/11/2024 2:38:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_lns_sochungtu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_lns_sochungtu]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_lns]    Script Date: 12/11/2024 2:38:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 12/11/2024 2:38:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 12/11/2024 2:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
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
	declare @AdjustMonth nvarchar(100)
	declare @CurrentMonth nvarchar(100)
	if (@QuarterMonth like '%,%')
	begin
		set @AdjustMonth = (select top(1) Item from f_split(@QuarterMonth))
		set @CurrentMonth = REPLACE(@QuarterMonth,@AdjustMonth + ',', '')
	end
	else 
	begin
		set @AdjustMonth = @QuarterMonth
		set @CurrentMonth = ''
	end
	DECLARE @IsDonViCha bit;
	IF @EstimateAgencyId = @AgencyId
		SET @IsDonViCha = 0
	ELSE
		SET @IsDonViCha = 1
	SELECT sLNS,
		   iID_MLNS,
		   TenDonVi,
		   SoNguoi = SUM(SoNguoi),
		   SoNgay = SUM(SoNgay),
		   ChiTieu = ROUND(SUM(ChiTieu) / @Dvt, 0),
		   TuChi2 = ROUND(SUM(TuChi2) / @Dvt, 0),
		   TuChi = ROUND(SUM(TuChi) / @Dvt, 0) 
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			SoNguoi = 0,
			SoNgay = 0,
			ChiTieu = fTuChi + fHangNhap + fHangMua,
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
				AND (cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
				OR (YEAR(CAST(dNgayQuyetDinh AS DATE)) - YEAR(CAST(@VoucherDate AS DATE)) = 1
				AND MONTH(CAST(dNgayQuyetDinh AS DATE)) IN (1,2,3) AND MONTH(CAST(@VoucherDate AS DATE)) = 12))
		)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM dbo.splitstring(@LNS)))
		 AND IDuLieuNhan = 0		 
	   --số đã quyết toán
	   UNION ALL 
	   SELECT sLNS=dbo.f_get_lns_by_id(@YearOfWork,iID_MLNS),
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			SoNguoi = 0,
			SoNgay = 0,
			ChiTieu = 0,
			TuChi2 = TuChi,
			TuChi = 0
		from f_quyettoan_tonghop_dieuchinh(@YearOfWork, @YearOfBudget,@BudgetSource,@QuarterMonthBefore,@AdjustMonth,@AgencyId,@LNS)
	   --FROM f_quyettoan_tonghop_dieuchinh(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		 
	   --quyết toán đợt này
	   UNION ALL 
	   SELECT sLNS=dbo.f_get_lns_by_id(@YearOfWork,iID_MLNS),
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			SoNguoi,
			SoNgay,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi
	   FROM f_quyettoan_tonghop_dieuchinh_hientai(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @lns)
	   
	   UNION ALL 
	   SELECT sLNS=dbo.f_get_lns_by_id(@YearOfWork,iID_MLNS),
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			SoNguoi,
			SoNgay,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi
	   FROM f_quyettoan_tonghop_dieuchinh_hientai_quy(@YearOfWork, @YearOfBudget, @BudgetSource, @CurrentMonth, @AgencyId, @lns)) AS ct
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
	HAVING SUM(TuChi) <> 0
	OR SUM(TuChi2) <> 0
	OR SUM(ChiTieu) <> 0;

	select distinct sLNS into #tblLNS from #tblEstimateSettlement;
	SELECT mlns.iID_MLNS,
		isnull(mlns.iID_MLNS_Cha,'00000000-0000-0000-0000-000000000000') iID_MLNS_Cha,
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
		isnull(dt.SoNguoi, 0) as SoNguoi,
		isnull(dt.SoNgay, 0) as SoNgay,
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
	ORDER BY mlns.sXauNoiMa, dt.TenDonVi
	DROP TABLE #tblEstimateSettlement, #tblLNS;


END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_lns]    Script Date: 12/11/2024 2:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_lns]
	@VoucherId nvarchar(100),
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(100),
	@Dvt int
AS
BEGIN
	SELECT ct.iID_MLNS,
		sLNS,
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
		mlns.iID_MLNS_Cha,
		sMoTa,
		TuChi = SUM(TuChi) / @Dvt,
		HienVat = SUM(HienVat) / @Dvt,
		SoNguoi = SUM(SoNguoi),
		SoNgay = SUM(SoNgay),
		SoLuot = SUM(SoLuot) INTO #tblThongTriLns
	FROM
	  (SELECT iID_MLNS,
			sLNS,
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
			fTuChi_PheDuyet AS TuChi,
			cast(0 AS float) AS HienVat,
			fSoNguoi AS SoNguoi,
			fSoNgay AS SoNgay,
			fSoLuot AS SoLuot
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_QTChungTu = @VoucherId
		 AND iID_MaDonVi = @AgencyId
		 AND sLNS in (SELECT * FROM f_split(@LNS)) ) AS ct 
	--mota lns
	LEFT JOIN
	  (SELECT sMoTa,
			  iID_MLNS,
			  sXauNoiMa,
			  iID_MLNS_Cha
	   FROM NS_MucLucNganSach
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS mlns ON mlns.iID_MLNS = ct.iID_MLNS
	GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa, sMoTa, ct.iID_MLNS, mlns.iID_MLNS_Cha
	HAVING sum(TuChi) <> 0
	OR sum(HienVat) <> 0;

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
			isnull(iID_MLNS_Cha,'00000000-0000-0000-0000-000000000000') iID_MLNS_Cha,
			cast(TuChi AS float) AS TuChi,
			cast(HienVat AS float) AS HienVat,
			cast(SoNguoi AS float) AS SoNguoi,
			cast(SoNgay AS float) AS SoNgay,
			cast(SoLuot AS float) AS SoLuot,
			sMoTa,
			cast(0 AS bit) AS IsHangCha
	   FROM #tblThongTriLns
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
			isnull(mlnsParent.iID_MLNS_Cha,'00000000-0000-0000-0000-000000000000') iID_MLNS_Cha,
			cast(0 AS float) AS TuChi,
			cast(0 AS float) AS HienVat,
			cast(0 AS float) AS SoNguoi,
			cast(0 AS float) AS SoNgay,
			cast(0 AS float) AS SoLuot,
			mlnsParent.sMoTa,
			cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork )
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sXauNoiMa;
	DROP TABLE #tblThongTriLns;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_lns_sochungtu]    Script Date: 12/11/2024 2:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_lns_sochungtu]
	@VoucherName nvarchar(100),
	@VoucherType nvarchar(100),
	@LNS nvarchar(max),
	@YearOfWork int,
	@QuarterMonth int,
	@QuarterMonthType int,
	@Dvt int
AS
BEGIN
	SELECT ct.iID_MLNS,
		sLNS,
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
		mlns.iID_MLNS_Cha,
		sMoTa,
		iID_MaDonVi AS MaDonVi,
		sTenDonVi AS TenDonVi,
		TuChi = SUM(TuChi) / @Dvt,
		HienVat = SUM(HienVat) / @Dvt,
		SoNguoi = SUM(SoNguoi),
		SoNgay = SUM(SoNgay),
		SoLuot = SUM(SoLuot) INTO #tblThongTriLns
	FROM
	  (SELECT iID_MLNS,
			sLNS,
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
			fTuChi_PheDuyet AS TuChi,
			cast(0 AS float) AS HienVat,
			fSoNguoi AS SoNguoi,
			fSoNgay AS SoNgay,
			fSoLuot AS SoLuot,
			DonVi.iID_MaDonVi,
			DonVi.sTenDonVi
	   FROM NS_QT_ChungTuChiTiet
	   INNER JOIN NS_QT_ChungTu ON NS_QT_ChungTu.iID_QTChungTu = NS_QT_ChungTuChiTiet.iID_QTChungTu
	   LEFT JOIN DonVi ON DonVi.iID_MaDonVi = NS_QT_ChungTu.iID_MaDonVi AND DonVi.iNamLamViec = NS_QT_ChungTu.iNamLamViec
	   WHERE NS_QT_ChungTuChiTiet.iNamLamViec = @YearOfWork
		 AND NS_QT_ChungTu.sSoChungTu = @VoucherName
		 AND NS_QT_ChungTu.iThangQuy = @QuarterMonth
		 AND NS_QT_ChungTu.iThangQuyLoai = @QuarterMonthType
		 AND NS_QT_ChungTu.sLoai = @VoucherType
		 AND sLNS in (SELECT * FROM f_split(@LNS)) ) AS ct 
	--mota lns
	LEFT JOIN
	  (SELECT sMoTa,
			  iID_MLNS,
			  sXauNoiMa,
			  iID_MLNS_Cha
	   FROM NS_MucLucNganSach
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS mlns ON mlns.iID_MLNS = ct.iID_MLNS
	GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa, sMoTa, ct.iID_MLNS, mlns.iID_MLNS_Cha, iID_MaDonVi, sTenDonVi
	HAVING sum(TuChi) <> 0
	OR sum(HienVat) <> 0;

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
			isnull(iID_MLNS_Cha,'00000000-0000-0000-0000-000000000000') iID_MLNS_Cha,
			cast(TuChi AS float) AS TuChi,
			cast(HienVat AS float) AS HienVat,
			cast(SoNguoi AS float) AS SoNguoi,
			cast(SoNgay AS float) AS SoNgay,
			cast(SoLuot AS float) AS SoLuot,
			sMoTa,
			MaDonVi,
			TenDonVi,
			cast(0 AS bit) AS IsHangCha
	   FROM #tblThongTriLns
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
			isnull(mlnsParent.iID_MLNS_Cha,'00000000-0000-0000-0000-000000000000') iID_MLNS_Cha,
			cast(0 AS float) AS TuChi,
			cast(0 AS float) AS HienVat,
			cast(0 AS float) AS SoNguoi,
			cast(0 AS float) AS SoNgay,
			cast(0 AS float) AS SoLuot,
			mlnsParent.sMoTa,
			null AS MaDonVi,
			null AS TenDonVi,
			cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork )
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sXauNoiMa;
	DROP TABLE #tblThongTriLns;
END
;
;
;
;
;
GO
