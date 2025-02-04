/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_donvi]    Script Date: 12/17/2024 5:30:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_tonghop_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_tonghop_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_donvi]    Script Date: 12/17/2024 5:30:18 PM ******/
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
		isnull(mlns.bHangChaDuToan,0) AS IsHangChaDuToan,
		isnull(mlns.bHangChaQuyetToan,0) AS IsHangChaQuyetToan,
		mlns.sXauNoiMa AS XauNoiMa, 
		mlns.sMoTa AS MoTa, 
		(ROUND(isnull(rs.DuToan, 0) / @Dvt, 0)) AS DuToan, 
		(ROUND((CASE
			WHEN @IsAccumulated = 1THEN isnull(rs.TrongKy, 0)
			ELSE (isnull(rs.QuyetToan, 0) + isnull(rs.TrongKy, 0))
		  END
		) / @Dvt, 0)) AS QuyetToan,
		--((isnull(rs.QuyetToan, 0) + isnull(rs.TrongKy, 0)) / @Dvt) AS QuyetToan, 
		(ROUND(isnull(rs.TrongKy, 0) / @Dvt, 0)) AS TrongKy, 
		(ROUND(isnull(rs.QuyetToanDonVi, 0) / @Dvt, 0)) AS QuyetToanDonVi, 
		(ROUND(isnull(rs.DuToanDonVi, 0) / @Dvt, 0)) AS DuToanDonVi, 
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
;
;
GO
