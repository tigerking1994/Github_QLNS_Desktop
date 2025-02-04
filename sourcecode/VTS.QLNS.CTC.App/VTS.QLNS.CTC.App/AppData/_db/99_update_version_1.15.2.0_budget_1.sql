/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 12/4/2024 4:17:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]    Script Date: 12/4/2024 4:17:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 12/4/2024 4:17:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 12/4/2024 4:17:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]    Script Date: 12/4/2024 4:17:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 12/4/2024 4:17:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_tonghop_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_tonghop_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]    Script Date: 12/4/2024 4:17:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 12/4/2024 4:17:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 12/4/2024 4:17:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 12/4/2024 4:17:27 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 12/4/2024 4:17:27 PM ******/
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
		   --TenDonVi,
		   ChiTieu = ROUND(SUM(ChiTieu) / @Dvt, 0),
		   TuChi = ROUND(SUM(TuChi) / @Dvt, 0),
		   Quy1 = ROUND(SUM(Quy1) / @Dvt, 0),
		   Quy2 = ROUND(SUM(Quy2) / @Dvt, 0),
		   Quy3 = ROUND(SUM(Quy3) / @Dvt, 0),
		   Quy4 = ROUND(SUM(Quy4) / @Dvt, 0)
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
	  (SELECT iID_MaDonVi
			  --TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY 
			 sLNS,
			 --TenDonVi,
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
		'' as TenDonVi
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_thang_clone]    Script Date: 12/4/2024 4:17:27 PM ******/
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
		   ChiTieu =  ROUND(SUM(ChiTieu) / @Dvt, 0),
		   TuChi =  ROUND(SUM(TuChi) / @Dvt, 0),
		   Thang1 =  ROUND(SUM(Thang1) / @Dvt, 0),
		   Thang2 =  ROUND(SUM(Thang2) / @Dvt, 0),
		   Thang3 =  ROUND(SUM(Thang3) / @Dvt, 0),
		   Thang4 =  ROUND(SUM(Thang4) / @Dvt, 0),
		   Thang5 =  ROUND(SUM(Thang5) / @Dvt, 0),
		   Thang6 =  ROUND(SUM(Thang6) / @Dvt, 0),
		   Thang7 =  ROUND(SUM(Thang7) / @Dvt, 0),
		   Thang8 =  ROUND(SUM(Thang8) / @Dvt, 0),
		   Thang9 =  ROUND(SUM(Thang9) / @Dvt, 0),
		   Thang10 =  ROUND(SUM(Thang10) / @Dvt, 0),
		   Thang11 =  ROUND(SUM(Thang11) / @Dvt, 0),
		   Thang12 =  ROUND(SUM(Thang12) / @Dvt, 0)
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
		isnull(mlns.iID_MLNS_Cha, '00000000-0000-0000-0000-000000000000') iID_MLNS_Cha,
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
		mlns.bHangChaDuToan as IsHangCha,
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 12/4/2024 4:17:27 PM ******/
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
		   ChiTieu = ROUND(SUM(ChiTieu) / @Dvt, 0),
		   TuChi = ROUND(SUM(TuChi) / @Dvt, 0),
		   Thang1 = ROUND(SUM(Thang1) / @Dvt, 0),
		   Thang2 = ROUND(SUM(Thang2) / @Dvt, 0),
		   Thang3 = ROUND(SUM(Thang3) / @Dvt, 0),
		   Thang4 = ROUND(SUM(Thang4) / @Dvt, 0),
		   Thang5 = ROUND(SUM(Thang5) / @Dvt, 0),
		   Thang6 = ROUND(SUM(Thang6) / @Dvt, 0),
		   Thang7 = ROUND(SUM(Thang7) / @Dvt, 0),
		   Thang8 = ROUND(SUM(Thang8) / @Dvt, 0),
		   Thang9 = ROUND(SUM(Thang9) / @Dvt, 0),
		   Thang10 = ROUND(SUM(Thang10) / @Dvt, 0),
		   Thang11 = ROUND(SUM(Thang11) / @Dvt, 0),
		   Thang12 = ROUND(SUM(Thang12) / @Dvt, 0)
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]    Script Date: 12/4/2024 4:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]
	@MaCongKhai nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	
	declare @CapDonVi int = (select top 1 iCapDonVi from DonVi where iLoai = 0 and inamlamviec = @YearOfWork)

	declare @DsDonViBanThan nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iCapDonVi = @CapDonVi and iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai <> 0
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViDuToan nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 2 and inamlamviec = @YearOfWork and iTrangThai = 1 and iLoai <> 0
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViDoanhNghiep nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 1 and inamlamviec = @YearOfWork and iTrangThai = 1 and iLoai <> 0
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViBVTC nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 3 and inamlamviec = @YearOfWork and iTrangThai = 1 and iLoai <> 0
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	select ck.* into #basemlns
	from NS_DanhMucCongKhai ck
	where (ck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@MaCongKhai)) OR (ck.Id IN (SELECT * FROM f_split(@MaCongKhai))))
		and ck.iNamLamViec = @YearOfWork
	
	--Thu
	select ctct.*, ck.sMa sMaMlck into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.LNS like '8%'
		and ct.NamLamViec = @YearOfWork

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ck.sMa sMaMlck, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ck.sMa, ctct.iPhanCap, ctct.NguonNganSach
	------------------------------------------------------------------------	
	--Chi
	select ctct.*, ck.sMa sMaMlck into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.sLNS not like '8%'
		and ct.iNamLamViec = @YearOfWork

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ck.sMa sMaMlck, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ck.sMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	CREATE TABLE #datapl1(
	IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fChiTaiBanThan float,
	fKhoiDuToan float,
	fKhoiDoanhNghiep float,
	fBVTC float,
	sLoai nvarchar(50));

	insert into #datapl1(IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fChiTaiBanThan, fKhoiDuToan, fKhoiDoanhNghiep, fBVTC, sLoai)
	select
       mlns.Id IIdMlns,
       mlns.iID_DMCongKhai_Cha IIdMlnsCha,
       mlns.sMa,
       concat(mlns.STT,'. ', mlns.sMoTa) sMoTa,
       mlns.bHangCha,
	   dtnsdg.TuChi fDuToanNSDuocGiao,
	   sum(isnull(ctbt.fChiTaiBanThan, 0)) fChiTaiBanThan,
	   sum(isnull(dt.fKhoiDuToan, 0)) fKhoiDuToan,
	   sum(isnull(dn.fKhoiDoanhNghiep, 0)) fKhoiDoanhNghiep,
	   sum(isnull(bvtc.fBVTC, 0)) fBVTC,
	   'THU' sLoai
	from #basemlns mlns
	left join #basedatathudutoan dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
	left join (select ctbt.sMaMlck, sum(isnull(ctbt.TuChi, 0))/@DVT fChiTaiBanThan
		from #basedatathudonvi ctbt
		where ctbt.iPhanCap = 1 and ctbt.NguonNganSach = @BudgetSource and ctbt.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by ctbt.sMaMlck
	) ctbt ON mlns.sMa = ctbt.sMaMlck
		
	left join (select dt.sMaMlck, sum(isnull(dt.TuChi, 0))/@DVT fKhoiDuToan 
		from #basedatathudonvi dt
		where dt.iPhanCap = 1 and dt.NguonNganSach = @BudgetSource and dt.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViDuToan)) and dt.Id_DonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by dt.sMaMlck
	) dt ON mlns.sMa = dt.sMaMlck
	left join (select dn.sMaMlck, sum(isnull(dn.TuChi, 0))/@DVT fKhoiDoanhNghiep 
		from #basedatathudonvi dn
		where dn.iPhanCap = 1 and dn.NguonNganSach = @BudgetSource and dn.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViDoanhNghiep)) and dn.Id_DonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by dn.sMaMlck
	) dn ON mlns.sMa = dn.sMaMlck 
	left join (select bvtc.sMaMlck, sum(isnull(bvtc.TuChi, 0))/@DVT fBVTC 
		from #basedatathudonvi bvtc
		where bvtc.iPhanCap = 1 and bvtc.NguonNganSach = @BudgetSource and bvtc.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViBVTC)) and bvtc.Id_DonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by bvtc.sMaMlck
	) bvtc ON mlns.sMa = bvtc.sMaMlck
	group by mlns.Id, mlns.iID_DMCongKhai_Cha, mlns.sMa, concat(mlns.STT,'. ', mlns.sMoTa), mlns.bHangCha, dtnsdg.TuChi

	insert into #datapl1(IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fChiTaiBanThan, fKhoiDuToan, fKhoiDoanhNghiep, fBVTC, sLoai)
	select
      mlns.Id IIdMlns,
       mlns.iID_DMCongKhai_Cha IIdMlnsCha,
       mlns.sMa,
       concat(mlns.STT,'. ', mlns.sMoTa) sMoTa,
       mlns.bHangCha,
	   dtnsdg.fTuChi fDuToanNSDuocGiao,
	   sum(isnull(ctbt.fChiTaiBanThan, 0)) fChiTaiBanThan,
	   sum(isnull(dt.fKhoiDuToan, 0)) fKhoiDuToan,
	   sum(isnull(dn.fKhoiDoanhNghiep, 0)) fKhoiDoanhNghiep,
	   sum(isnull(bvtc.fBVTC, 0)) fBVTC,
	   'CHI' sLoai
	from #basemlns mlns
	left join #basedatachidutoan dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
	left join (select ctbt.sMaMlck, sum(isnull(ctbt.fTuChi, 0))/@DVT fChiTaiBanThan
		from #basedatachidonvi ctbt
		where ctbt.iPhanCap = 1 and ctbt.iID_MaNguonNganSach = @BudgetSource and ctbt.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by ctbt.sMaMlck
	) ctbt ON mlns.sMa = ctbt.sMaMlck
		
	left join (select dt.sMaMlck, sum(isnull(dt.fTuChi, 0))/@DVT fKhoiDuToan 
		from #basedatachidonvi dt
		where dt.iPhanCap = 1 and dt.iID_MaNguonNganSach = @BudgetSource and dt.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViDuToan)) and dt.iID_MaDonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by dt.sMaMlck
	) dt ON mlns.sMa = dt.sMaMlck
	left join (select dn.sMaMlck, sum(isnull(dn.fTuChi, 0))/@DVT fKhoiDoanhNghiep 
		from #basedatachidonvi dn
		where dn.iPhanCap = 1 and dn.iID_MaNguonNganSach = @BudgetSource and dn.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViDoanhNghiep)) and dn.iID_MaDonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by dn.sMaMlck
	) dn ON mlns.sMa = dn.sMaMlck 
	left join (select bvtc.sMaMlck, sum(isnull(bvtc.fTuChi, 0))/@DVT fBVTC 
		from #basedatachidonvi bvtc
		where bvtc.iPhanCap = 1 and bvtc.iID_MaNguonNganSach = @BudgetSource and bvtc.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViBVTC)) and bvtc.iID_MaDonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by bvtc.sMaMlck
	) bvtc ON mlns.sMa = bvtc.sMaMlck
	group by mlns.Id, mlns.iID_DMCongKhai_Cha, mlns.sMa, concat(mlns.STT,'. ', mlns.sMoTa), mlns.bHangCha, dtnsdg.fTuChi

	select * from #datapl1

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 12/4/2024 4:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	where ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ct.NamLamViec = @YearOfWork

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	where ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ct.iNamLamViec = @YearOfWork

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	--Data Ban than

	CREATE TABLE #databanthan (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sMoTa nvarchar(max),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBoBanThan float,
	sLoai nvarchar(50));

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #databanthan(IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	CREATE TABLE #datadonvi (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
	insert into #datadonvi (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBo, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		   dtnsdg.TuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join (select * from #basedatathudonvi where Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))) donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathudutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.TuChi

	insert into #datadonvi (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBo, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		   dtnsdg.fTuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join (select * from #basedatachidonvi where iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))) donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachidutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.fTuChi

	--Ket qua
	CREATE TABLE #dataketqua (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBoBanThan float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai)
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai)
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	select
		IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, 
		FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo,
		isnull(FSoPhanBoBanThan, 0) + isnull(FSoPhanBo, 0) FTongSoPhanBo,
		sLoai
	from #dataketqua
	where isnull(FDuToanNSDuocGiao, 0) <> 0 or isnull(FSoPhanBoBanThan, 0) <> 0 or isnull(FSoPhanBo, 0) <> 0

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 12/4/2024 4:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN
	
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ct.Id = ctct.Id_ChungTu
	where ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.Id_DonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.NamLamViec = @YearOfWork

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
	where ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iNamLamViec = @YearOfWork

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	--Data Ban than
	CREATE TABLE #databanthan (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sMoTa nvarchar(max),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FChoPhanBo float,
	FSoPhanBoBanThan float,
	sLoai nvarchar(50));

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FChoPhanBo, FSoPhanBoBanThan, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   0 FChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FChoPhanBo, FSoPhanBoBanThan, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	CREATE TABLE #datadonvi1 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
	insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		  dtnsdg.TuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathudutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.TuChi

		insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai)

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		   dtnsdg.fTuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa 
		left join #basedatachidutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.fTuChi
	
	--Ket qua
	CREATE TABLE #dataketqua (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBoBanThan float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai)
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai)
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	select
		IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, 
		FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo,
		isnull(FSoPhanBoBanThan, 0) + isnull(FSoPhanBo, 0) FTongSoPhanBo,
		sLoai
	from #dataketqua
	where isnull(FDuToanNSDuocGiao, 0) <> 0 or isnull(FSoPhanBoBanThan, 0) <> 0 or isnull(FSoPhanBo, 0) <> 0

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]    Script Date: 12/4/2024 4:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN
	
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ct.Id = ctct.Id_ChungTu
	where ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.Id_DonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.NamLamViec = @YearOfWork

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
	where ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iNamLamViec = @YearOfWork

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	--Data Ban than
	CREATE TABLE #databanthan (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sMoTa nvarchar(max),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FChoPhanBo float,
	FSoPhanBoBanThan float,
	sLoai nvarchar(50));

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FChoPhanBo, FSoPhanBoBanThan, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   0 FChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FChoPhanBo, FSoPhanBoBanThan, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	--Data Don vi
	CREATE TABLE #datadonvi1 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	FSoPhanBo float,
	sLoai nvarchar(50),
	IIdMaDonVi nvarchar(500));

	insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai, IIdMaDonVi)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		  dtnsdg.TuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai,
		   donvi.Id_DonVi
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathudutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.TuChi, donvi.Id_DonVi

		insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai, IIdMaDonVi)

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangChaDuToan,
		   dtnsdg.fTuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai,
		   donvi.iID_MaDonVi
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa 
		left join #basedatachidutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.fTuChi, donvi.iID_MaDonVi
	
	--Ket qua
	CREATE TABLE #dataketqua (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBoBanThan float,
	FSoPhanBo float,
	sLoai nvarchar(50),
	IIdMaDonVi nvarchar(500));

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai, IIdMaDonVi)
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		banthan.FSoPhanBoBanThan FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'THU' sLoai,
		donvi.IIdMaDonVi
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, donvi.IIdMaDonVi, banthan.FSoPhanBoBanThan

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai, IIdMaDonVi)
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		banthan.FSoPhanBoBanThan FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'CHI' sLoai,
		donvi.IIdMaDonVi
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, donvi.IIdMaDonVi, banthan.FSoPhanBoBanThan

	select
		IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, 
		FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo,
		isnull(FSoPhanBoBanThan, 0) + isnull(FSoPhanBo, 0) FTongSoPhanBo,
		sLoai, IIdMaDonVi
	from #dataketqua
	where isnull(FDuToanNSDuocGiao, 0) <> 0 or isnull(FSoPhanBoBanThan, 0) <> 0 or isnull(FSoPhanBo, 0) <> 0

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 12/4/2024 4:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]
	@MaCongKhai nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select ck.* into #basemlns
	from NS_DanhMucCongKhai ck
	where (ck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@MaCongKhai)) OR (ck.Id IN (SELECT * FROM f_split(@MaCongKhai))))
		and ck.iNamLamViec = @YearOfWork

	--Thu
	select ctct.*, ck.sMa sMaMlck into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.LNS like '8%'
		and ct.NamLamViec = @YearOfWork

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ck.sMa sMaMlck, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ck.sMa, ctct.iPhanCap, ctct.NguonNganSach
	------------------------------------------------------------------------	
	--Chi
	select ctct.*, ck.sMa sMaMlck into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.sLNS not like '8%'
		and ct.iNamLamViec = @YearOfWork

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ck.sMa sMaMlck, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ck.sMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	CREATE TABLE #databanthanm02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBoBanThan float,
	sLoai nvarchar(50));

	--Data Ban than
	insert into #databanthanm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT fSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sMa = donvi.sMaMlck
		where donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

		insert into #databanthanm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sMa = donvi.sMaMlck
		where donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

	CREATE TABLE #datadonvim02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
		insert into #datadonvim02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBo, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   dtnsdg.TuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/1 FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join (select * from #basedatathudonvi where Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))) donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatathudutoan dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = 1
		--where donvi.Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha,
			dtnsdg.TuChi

		insert into #datadonvim02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBo, sLoai)

		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   dtnsdg.fTuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/1 FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join (select * from #basedatachidonvi where iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))) donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatachidutoan dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = 1
		--where donvi.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha,
			dtnsdg.fTuChi

	--Ket qua

	CREATE TABLE #tblresultm02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBoBanThan float,
	fSoPhanBo float,
	sLoai nvarchar(50),
	iLoai int,
	iRoot int,
	hasData bit);

	--A. DỰ TOÁN THU
	--insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, FSoPhanBo, sLoai, iLoai, iRoot, hasData)
	--select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'A. DỰ TOÁN THU' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 fSoPhanBoBanThan, 0 fSoPhanBo, 'THU' sLoai, 1 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		concat(mlns.STT,'. ', mlns.sMoTa),
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) fSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'THU' sLoai,
		1 iLoai,
		2 iRoot,
		1 hasData
	from #basemlns mlns
	left join #datadonvim02 donvi on mlns.sMa = donvi.sMa and donvi.sLoai = 'THU'
	left join #databanthanm02 banthan on mlns.sMa = banthan.sMa and banthan.sLoai = 'THU'
	group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			concat(mlns.STT,'. ', mlns.sMoTa),
			mlns.bHangCha
	
	--B. DỰ TOÁN CHI
	--insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, sLoai, iLoai, iRoot, hasData)
	--select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'B. DỰ TOÁN CHI' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 FSoPhanBoBanThan, 0 FSoPhanBo, 'CHI' sLoai, 2 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		concat(mlns.STT,'. ', mlns.sMoTa) sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'CHI' sLoai,
		2 iLoai,
		2 iRoot,
		1 hasData
	from #basemlns mlns
	left join #datadonvim02 donvi on mlns.sMa = donvi.sMa and donvi.sLoai = 'CHI'
	left join #databanthanm02 banthan on mlns.sMa = banthan.sMa and banthan.sLoai = 'CHI'
	group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			concat(mlns.STT,'. ', mlns.sMoTa),
			mlns.bHangCha

	update #tblresultm02 set hasData = 0
	where (fDuToanNSDuocGiao = 0 and FSoPhanBoBanThan = 0 and FSoPhanBo = 0 and iRoot = 2)
	--or (iRoot = 1 and iLoai not in (select distinct iLoai from #tblresultm02
	--			where fDuToanNSDuocGiao <> 0 or FSoPhanBoBanThan <> 0 or FSoPhanBo <> 0 or iRoot = 2))

	select * from #tblresultm02 where hasData = 1
	order by iLoai, iRoot
END
;
;
;
GO
