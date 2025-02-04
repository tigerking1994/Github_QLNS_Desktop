/****** Object:  StoredProcedure [dbo].[sp_qt_tonghop_nam_lns]    Script Date: 13/06/2023 11:57:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_tonghop_nam_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_tonghop_nam_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_tonghop_donvi_lns]    Script Date: 13/06/2023 11:57:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_tonghop_donvi_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_tonghop_donvi_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]    Script Date: 13/06/2023 11:57:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 13/06/2023 11:57:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 13/06/2023 11:57:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_mlns_nhanuoc]    Script Date: 13/06/2023 11:57:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_mlns_nhanuoc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_mlns_nhanuoc]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_clone]    Script Date: 13/06/2023 11:57:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_quyettoan_clone]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_quyettoan_clone]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_clone]    Script Date: 13/06/2023 11:57:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[f_quyettoan_clone]
(	
	@NamLamViec int,
	@NamNganSach nvarchar(20),
	@NguonNganSach nvarchar(20),
	@ithangquy nvarchar(100),
	@IdDonVi nvarchar(max),
	@lns nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			fTuChi_PheDuyet 
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iID_QTChungTu in
		   (SELECT iID_QTChungTu
			FROM NS_QT_ChungTu
			WHERE iNamLamViec = @NamLamViec
			  AND INamNganSach = @NamNganSach
			  AND iID_MaNguonNganSach = @NguonNganSach
			  AND iThangQuy in (select * from f_split(@ithangquy)))
		 AND (@IdDonVi IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
		 AND (@lns IS NULL OR sLNS in (SELECT * FROM dbo.splitstring(@LNS)))
)
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_mlns_nhanuoc]    Script Date: 13/06/2023 11:57:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_mlns_nhanuoc] 
	@YearOfWork int,
	@LNS nvarchar(max),
	@GenerateAgencyId nvarchar(50),
	@UserName nvarchar(100)
AS
BEGIN
	WITH LNSTree
		AS
		(
			SELECT *
			FROM NS_MucLucNganSach
			WHERE 
				sL = ''
				AND sK = ''
				AND sM = ''
				AND sTM = ''
				AND sTTM = ''
				AND sNG = ''
				AND sTNG = ''
				AND iNamLamViec = @YearOfWork
				AND sLNS in (SELECT * FROM f_split(@LNS))
			UNION ALL
			SELECT
				mlnsChild.*
			FROM NS_MucLucNganSach mlnsChild
			INNER JOIN LNSTree
				ON mlnsChild.iID_MLNS_Cha = lnstree.iID_MLNS
			WHERE 
				mlnsChild.sL = '' 
				AND mlnsChild.sK = '' 
				AND mlnsChild.sM = '' 
				AND mlnsChild.sTM = '' 
				AND mlnsChild.sTTM = '' 
				AND mlnsChild.sNG = '' 
				AND mlnsChild.sTNG = ''
				AND mlnsChild.iNamLamViec = @YearOfWork
		)
		--SELECT distinct * FROM LNSTree ORDER BY sLNS;

		SELECT mlns.* 
		FROM (SELECT DISTINCT * FROM LNSTree) mlns
		INNER JOIN 
		(
			SELECT DISTINCT VALUE
			FROM 
			(SELECT 
				CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
				CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
				CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
				CAST(sLNS AS nvarchar(10)) LNS 
			FROM NS_NguoiDung_LNS 
			WHERE 
				sMaNguoiDung = @UserName
				AND iNamLamViec = @YearOfWork
			) LNS
			UNPIVOT
			(
				value
				FOR col in (LNS1, LNS3, LNS5, LNS)
			) un) lns
		ON mlns.sLNS = lns.value

		ORDER BY sXauNoiMa
	END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 13/06/2023 11:57:51 AM ******/
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
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 13/06/2023 11:57:51 AM ******/
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
			ChiTieu = fTuChi,
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
				AND cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
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
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]    Script Date: 13/06/2023 11:57:51 AM ******/
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
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_tonghop_donvi_lns]    Script Date: 13/06/2023 11:57:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_tonghop_donvi_lns]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@QuarterMonth nvarchar(100),
	@VoucherDate date,
	@HasDuToan bit,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @tblLNS table (sLNS nvarchar(100))
	INSERT INTO @tblLNS (sLNS)
		SELECT sLNS
		FROM
		  (SELECT DISTINCT sLNS
		   FROM NS_QT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND fTuChi_PheDuyet <> 0
			 AND iID_QTChungTu in
				(
					select iID_QTChungTu from NS_QT_ChungTu
					where iThangQuy IN (SELECT * FROM f_split(@QuarterMonth))
				)
		   UNION 
		   SELECT DISTINCT sLNS
		   FROM NS_DT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND iDuLieuNhan = 0
			 AND fTuChi <> 0
			 AND iID_DTChungTu IN (
				SELECT iID_DTChungTu FROM NS_DT_ChungTu
				WHERE cast(dNgayQuyetDinh as date) <= cast(@VoucherDate as date)
					AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				)
			 ) lns

	SELECT sLNS as LNS, 
		sMoTa as MoTa, 
		iID_MLNS as MlnsId, 
		iID_MLNS_Cha as MlnsIdParent
	FROM NS_MucLucNganSach 
	INNER JOIN 
		(
			SELECT DISTINCT VALUE
			FROM 
			(SELECT 
				CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
				CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
				CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
				CAST(sLNS AS nvarchar(10)) LNS 
			FROM NS_NguoiDung_LNS 
			WHERE 
				sMaNguoiDung = @UserName
				AND iNamLamViec = @YearOfWork
			) LNS
			UNPIVOT
			(
				value
				FOR col in (LNS1, LNS3, LNS5, LNS)
			) un) lns
		ON sLNS = lns.value
	WHERE iNamLamViec = @YearOfWork
		AND sLNS in 
			(
				SELECT 
					DISTINCT VALUE
				FROM 
				(
					SELECT 
						CAST(LEFT(sLNS, 1) AS nvarchar(10)) sLNS1, 
						CAST(LEFT(sLNS, 3) AS nvarchar(10)) sLNS3, 
						CAST(LEFT(sLNS, 5) AS nvarchar(10)) sLNS5, 
						CAST(sLNS AS nvarchar(10)) sLNS 
					FROM
						@tblLNS
				) sLNS
				UNPIVOT
				(
					value
					FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
				) un
			)
			and sL = ''
	order by sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_tonghop_nam_lns]    Script Date: 13/06/2023 11:57:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_tonghop_nam_lns] 
	@YearOfWork int,
	@BudgetSource int,
	@DataType int,
	@UserName nvarchar(100)
AS
BEGIN
	SELECT left(sLNS, 1) AS LNS1,
       left(sLNS, 3) AS LNS3,
       sLNS as LNS
	FROM
	  (SELECT DISTINCT sLNS
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND ((@DataType=0AND (fTuChi_DeNghi <> 0 OR fTuChi_PheDuyet <> 0))
			  OR (@DataType=1 AND fTuChi_PheDuyet <> 0)
			  OR (@DataType=2 AND fTuChi_PheDuyet <> 0))
	   UNION 
	   SELECT DISTINCT sLNS
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iDuLieuNhan = 0
		 AND ((@DataType = 0 AND (fTuChi <> 0 OR fHienVat <> 0))
			  OR (@DataType = 1 AND fTuChi <> 0)
			  OR (@DataType = 2 AND fHienVat <> 0))) lns
		INNER JOIN 
		(
			SELECT DISTINCT VALUE
			FROM 
			(SELECT 
				CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
				CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
				CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
				CAST(sLNS AS nvarchar(10)) LNS 
			FROM NS_NguoiDung_LNS 
			WHERE 
				sMaNguoiDung = @UserName
				AND iNamLamViec = @YearOfWork
			) LNS
			UNPIVOT
			(
				value
				FOR col in (LNS1, LNS3, LNS5, LNS)
			) un) lnsNguoiDung
		ON lns.sLNS = lnsNguoiDung.value
END
;
;
GO
