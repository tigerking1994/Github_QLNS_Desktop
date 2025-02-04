/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 12/26/2024 3:22:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 12/26/2024 3:22:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 12/26/2024 3:22:27 PM ******/
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
		mlns.bHangChaDuToan as BHangChaDuToan,
		mlns.bHangChaQuyetToan as BHangChaQuyetToan,
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
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 12/26/2024 3:22:27 PM ******/
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
		mlns.bHangCha as IsHangCha,
		mlns.bHangChaDuToan as BHangChaDuToan,
		mlns.bHangChaQuyetToan as BHangChaQuyetToan,
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
delete DM_ChuKy
where Id_Type='rptNS_DuToan_DauNam_ChiThuongXuyen_DacThu_Doc'

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_DuToan_DauNam_ChiThuongXuyen_DacThu_Doc', NULL, N'rptNS_DuToan_DauNam_ChiThuongXuyen_DacThu_Doc', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CHI TIẾT PHÂN BỔ DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM', NULL, N'NỘI DUNG CHI ĐẶC THÙ NGÀNH NGHIỆP VỤ PHÂN CẤP', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO