/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_donvi]    Script Date: 16/02/2024 9:15:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_tonghop_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_tonghop_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 16/02/2024 9:15:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_tonghop_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_tonghop_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]    Script Date: 16/02/2024 9:15:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 16/02/2024 9:15:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 16/02/2024 9:15:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop_contain]    Script Date: 16/02/2024 9:15:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dutoan_tonghop_contain]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dutoan_tonghop_contain]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop]    Script Date: 16/02/2024 9:15:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dutoan_tonghop]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dutoan_tonghop]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop]    Script Date: 16/02/2024 9:15:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dutoan_tonghop]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @NgayChungTu datetime,
	  @IdDonVi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT iID_MLNS,
		   iID_MLNS_Cha,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi),
		   HienVat =sum(fHienVat)
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec = @NamLamViec
	  AND iDuLieuNhan = 0
	  AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi IS NULL
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_DTChungTu in
		(SELECT iID_DTChungTu
		 FROM NS_DT_ChungTu
		 WHERE iNamLamViec = @NamLamViec
		   AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
		   AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
		   AND (CAST(dNgayQuyetDinh AS DATE) <= @NgayChungTu)
		   OR (YEAR(CAST(dNgayQuyetDinh AS DATE)) - YEAR(CAST(@NgayChungTu AS DATE)) = 1
		   AND MONTH(CAST(dNgayQuyetDinh AS DATE)) IN (1,2,3) AND MONTH(CAST(@NgayChungTu AS DATE)) = 12))
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi;
;
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop_contain]    Script Date: 16/02/2024 9:15:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dutoan_tonghop_contain]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(100),
	  @NguonNganSach nvarchar(20),
	  @NgayChungTu datetime,
	  @IdDonVi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT iID_MLNS,
		   iID_MLNS_Cha,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi),
		   HienVat =sum(fHienVat)
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec = @NamLamViec
	  AND iDuLieuNhan = 0
	  AND (@NamNganSach IS NULL OR iNamNganSach in (select * from f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi IS NULL
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_DTChungTu in
		(SELECT iID_DTChungTu
		 FROM NS_DT_ChungTu
		 WHERE iNamLamViec = @NamLamViec
		   AND (@NamNganSach IS NULL OR iNamNganSach in (select * from f_split(@NamNganSach)))
		   AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
		   AND (CAST(dNgayQuyetDinh AS DATE) <= @NgayChungTu)
		   OR (YEAR(CAST(dNgayQuyetDinh AS DATE)) - YEAR(CAST(@NgayChungTu AS DATE)) = 1
		   AND MONTH(CAST(dNgayQuyetDinh AS DATE)) IN (1,2,3) AND MONTH(CAST(@NgayChungTu AS DATE)) = 12))
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 16/02/2024 9:15:53 AM ******/
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
	   FROM f_quyettoan_clone(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		 
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
	   FROM f_quyettoan_clone(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)) AS ct
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 16/02/2024 9:15:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(100),
	@AgencyId nvarchar(100),
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
		   TuChi = SUM(TuChi) / @Dvt,
		   Quy1 = SUM(Quy1) / @Dvt,
		   Quy2 = SUM(Quy2) / @Dvt,
		   Quy3 = SUM(Quy3) / @Dvt,
		   Quy4 = SUM(Quy4) / @Dvt
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			ChiTieu = fTuChi + fHangNhap + fHangMua,
			TuChi = 0,
			Quy1 = 0,
			Quy2 = 0,
			Quy3 = 0,
			Quy4 = 0
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
		 
	   UNION ALL 
	   SELECT sLNS,
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi = fTuChi_PheDuyet,
			Quy1 = 0,
			Quy2 = 0,
			Quy3 = 0,
			Quy4 = 0
	   FROM f_quyettoan_clone(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)

	   UNION ALL 
	   select sLNS, 
			iID_MLNS, 
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi = 0,
			isnull([1], 0) AS Quy1,
			isnull([2], 0) AS Quy2,
			isnull([3], 0) AS Quy3,
			isnull([4], 0) AS Quy4
			from (
				select sLNS, iID_MLNS, iID_MaDonVi, isnull(fTuChi_PheDuyet, 0) as TuChi,
					case 
						when iThangQuy = 1 then 1
						when iThangQuy = 2 then 1
						when iThangQuy = 3 then 1
						when iThangQuy = 4 then 2
						when iThangQuy = 5 then 2
						when iThangQuy = 6 then 2
						when iThangQuy = 7 then 3
						when iThangQuy = 8 then 3
						when iThangQuy = 9 then 3
						when iThangQuy = 10 then 4
						when iThangQuy = 11 then 4
						when iThangQuy = 12 then 4
					end as quy
				FROM NS_QT_ChungTuChiTiet
				WHERE iID_QTChungTu in
					(SELECT iID_QTChungTu
					FROM NS_QT_ChungTu
					WHERE iNamLamViec = @YearOfWork
						AND INamNganSach = @YearOfBudget
						AND iID_MaNguonNganSach = @BudgetSource
						AND iThangQuy in (select * from f_split(@QuarterMonth))
						--AND iThangQuyLoai = @QuarterMonthType
						)
					AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					AND (@LNS IS NULL OR sLNS in (SELECT * FROM dbo.splitstring(@LNS)))
			) as data
			pivot 
			(
				SUM(TuChi) for quy IN ( [1], [2], [3], [4] )
			) as Thang
	) AS ct
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
	OR SUM(ChiTieu) <> 0
	OR SUM(Quy1) <> 0
	OR SUM(Quy2) <> 0
	OR SUM(Quy3) <> 0
	OR SUM(Quy4) <> 0;

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
		isnull(dt.TuChi, 0) as TuChi,
		isnull(Quy1, 0) as Quy1,
		isnull(Quy2, 0) as Quy2,
		isnull(Quy3, 0) as Quy3,
		isnull(Quy4, 0) as Quy4,
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
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.Quy1 <> 0 OR dt.Quy2 <> 0
		OR dt.Quy3 <> 0 OR dt.Quy4 <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblEstimateSettlement, #tblLNS;
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]    Script Date: 16/02/2024 9:15:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(100),
	@AgencyId nvarchar(100),
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
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			ChiTieu = fTuChi + fHangNhap + fHangMua,
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
		 
	   UNION ALL 
	   SELECT sLNS,
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
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
	   FROM f_quyettoan_clone(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)

	   UNION ALL 
	   select sLNS, 
			iID_MLNS, 
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
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
						--AND iThangQuyLoai = @QuarterMonthType
						)
					AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					AND (@LNS IS NULL OR sLNS in (SELECT * FROM dbo.splitstring(@LNS)))
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
	GROUP BY 
			 sLNS,
			 TenDonVi,
			 iID_MLNS
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
	OR SUM(Thang12) <> 0;

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
		isnull(dt.TuChi, 0) as TuChi,
		isnull(Thang1, 0) as Thang1,
		isnull(Thang2, 0) as Thang2,
		isnull(Thang3, 0) as Thang3,
		isnull(Thang4, 0) as Thang4,
		isnull(Thang5, 0) as Thang5,
		isnull(Thang6, 0) as Thang6,
		isnull(Thang7, 0) as Thang7,
		isnull(Thang8, 0) as Thang8,
		isnull(Thang9, 0) as Thang9,
		isnull(Thang10, 0) as Thang10,
		isnull(Thang11, 0) as Thang11,
		isnull(Thang12, 0) as Thang12,
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
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.Thang1 <> 0 OR dt.Thang2 <> 0
		OR dt.Thang3 <> 0 OR dt.Thang4 <> 0 OR dt.Thang5 <> 0 OR dt.Thang6 <> 0 OR dt.Thang7 <> 0 OR dt.Thang8 <> 0
		OR dt.Thang9 <> 0 OR dt.Thang10 <> 0 OR dt.Thang11 <> 0 OR dt.Thang12 <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblEstimateSettlement, #tblLNS;
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 16/02/2024 9:15:53 AM ******/
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
		 AND (@LNS IS NULL OR sLNS in (select * from f_split(@LNS)))
		 AND IDuLieuNhan = 0
		 AND iID_DTChungTu IN (
			SELECT iID_DTChungTu
			FROM NS_DT_ChungTu
			WHERE (sSoQuyetDinh IS NOT NULL OR sSoQuyetDinh <> '')
				AND (cast(dNgayQuyetDinh as date) <= cast(@VoucherDate AS date))
				OR (YEAR(CAST(dNgayQuyetDinh AS DATE)) - YEAR(CAST(@VoucherDate AS DATE)) = 1
				AND MONTH(CAST(dNgayQuyetDinh AS DATE)) IN (1,2,3) AND MONTH(CAST(@VoucherDate AS DATE)) = 12))
		 
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
					AND (@LNS IS NULL OR sLNS in (select * from f_split(@LNS)))
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_donvi]    Script Date: 16/02/2024 9:15:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_tonghop_donvi]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@EstimateAgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@LNS nvarchar(max),
	@VoucherDate date,
	@Dvt int,
	@IsAccumulated int
AS
BEGIN
	select iID_MLNS, 
		sum(isnull(DuToan, 0)) as DuToan, 
		sum(isnull(QuyetToan, 0)) as QuyetToan, 
		sum(isnull(TrongKy, 0)) as TrongKy 
		into #tblData from 
	 (
		SELECT iID_MLNS,
			DuToan = sum(fTuChi),
			QuyetToan = 0,
			TrongKy = 0
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
		AND (@LNS IS NULL OR sLNS in (select * from f_split(@LNS)))
		AND IDuLieuNhan = 0
		GROUP BY iID_MLNS
		union all
		select iID_MLNS,
			DuToan = 0,
			QuyetToan = TuChi,
			TrongKy = 0
		from f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		union all
		select iID_MLNS,
			DuToan = 0,
			QuyetToan = 0,
			TrongKy = TuChi
		from f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)
		) dt
	where (DuToan <> 0 or QuyetToan <> 0 or TrongKy <> 0)
	group by iID_MLNS

	select iID_MLNS,
		iID_MaDonVi,
		sum(QuyetToanDonVi) as QuyetToanDonVi,
		sum(QuyetToanDonViKyTruoc) as QuyetToanDonViKyTruoc,
		sum(DuToanDonVi) as DuToanDonVi
		into #tblDataDonVi
	from (
		select iID_MLNS,
			iID_MaDonVi,
			QuyetToanDonVi = TuChi,
			QuyetToanDonViKyTruoc = 0,
			DuToanDonVi = 0
		from f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)
		union all
		select iID_MLNS,
			iID_MaDonVi,
			QuyetToanDonVi = 0,
			QuyetToanDonViKyTruoc = TuChi,
			DuToanDonVi = 0
		from f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		union all
		SELECT iID_MLNS,
			iID_MaDonVi,
			QuyetToanDonVi = 0,
			QuyetToanDonViKyTruoc = 0,
			DuToanDonVi = sum(fTuChi)
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
		AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		AND (@LNS IS NULL OR sLNS in (select * from f_split(@LNS)))
		AND IDuLieuNhan = 0
		GROUP BY iID_MLNS, iID_MaDonVi
	) dataDonVi
	group by iID_MLNS, iID_MaDonVi


	select dt.*, 
		dv.iID_MaDonVi, 
		--(isnull(dv.QuyetToanDonVi, 0) + isnull(dv.QuyetToanDonViKyTruoc, 0)) as QuyetToanDonVi,					--duonglt16 sửa ngày 23/05/2023
		(isnull(dv.QuyetToanDonVi, 0)) as QuyetToanDonVi,															--duonglt16 sửa ngày 23/05/2023
		isnull(dv.DuToanDonVi, 0) as DuToanDonVi
		into #result 
	from #tblData dt
	left join #tblDataDonVi dv
	on dt.iID_MLNS = dv.iID_MLNS
	ORDER BY iID_MLNS

	select 
		mlns.iID_MLNS AS MlnsId,
		mlns.iID_MLNS_Cha AS MlnsIdCha,
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
		mlns.bHangCha AS IsHangCha,
		mlns.sXauNoiMa AS XauNoiMa, 
		mlns.sMoTa AS MoTa, 
		(isnull(rs.DuToan, 0) / @Dvt) AS DuToan, 
		((CASE
			WHEN @IsAccumulated = 1THEN isnull(rs.TrongKy, 0)
			ELSE (isnull(rs.QuyetToan, 0) + isnull(rs.TrongKy, 0))
		  END
		) / @Dvt) AS QuyetToan,
		--((isnull(rs.QuyetToan, 0) + isnull(rs.TrongKy, 0)) / @Dvt) AS QuyetToan, 
		(isnull(rs.TrongKy, 0) / @Dvt) AS TrongKy, 
		(isnull(rs.QuyetToanDonVi, 0) / @Dvt) AS QuyetToanDonVi, 
		(isnull(rs.DuToanDonVi, 0) / @Dvt) AS DuToanDonVi, 
		case
			when rs.iID_MaDonVi is null and bHangCha = 0 then @EstimateAgencyId
			else isnull(rs.iID_MaDonVi, '')
		end as IdMaDonVi
		
	from (select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork) mlns
	left join #result rs
	on mlns.iID_MLNS = rs.iID_MLNS
	WHERE bHangCha = 1 OR (DuToan <> 0 OR QuyetToan <> 0 OR TrongKy <> 0 OR QuyetToanDonVi <> 0)
	order by sXauNoiMa

	drop table #tblData, #tblDataDonVi, #result
END
;
;
;
;
;
GO
