/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_donvi]    Script Date: 31/01/2023 4:25:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_tonghop_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_tonghop_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_mlns_get_by_user]    Script Date: 31/01/2023 4:25:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_mlns_get_by_user]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_mlns_get_by_user]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]    Script Date: 31/01/2023 4:25:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_chiphi]    Script Date: 31/01/2023 4:25:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_chiphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_chiphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_chiphi]    Script Date: 31/01/2023 4:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_qddautu_chiphi]
@iIdQdDauTuId uniqueidentifier
AS
BEGIN
	SELECT qdnv.iID_NguonVonID as IIdNguonVonIdInt, tbl.ID as IIdChiPhiID, tbl.iID_ParentID as IIdParentID, tbl.STenChiPhi, tbl.SMaOrder , NULL as IIdGoiThauID, 
		ISNULL(tbl.FGiaTriNgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac, ISNULL(tbl.FGiaTriUSD, 0) as FGiaTriUSD, 
		ISNULL(tbl.FGiaTriVND, 0) as FGiaTriVND, ISNULL(tbl.FGiaTriEUR, 0) as FGiaTriEUR,
		CAST(0 as float) as FGiaTriPheDuyetNgoaiTeKhac,
		CAST(0 as float) as FGiaTriPheDuyetUSD,
		CAST(0 as float) as FGiaTriPheDuyetVND,
		CAST(0 as float) as FGiaTriPheDuyetEUR
	FROM NH_DA_QDDauTu_ChiPhi as tbl
	LEFT JOIN NH_DA_QDDauTu_NguonVon as qdnv on qdnv.ID = tbl.iID_QDDauTu_NguonVonID
	WHERE tbl.iID_QDDauTuID = @iIdQdDauTuId
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]    Script Date: 31/01/2023 4:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]
@iID_DTChungTu AS uniqueidentifier ,
@iID_CTDuToan_Nhan AS nvarchar(max)

AS
BEGIN

DELETE FROM NS_DT_ChungTuChiTiet 
WHERE iID_DTChungTu = @iID_DTChungTu 
AND iID_CTDuToan_Nhan NOT IN (SELECT * FROM f_split(@iID_CTDuToan_Nhan))

END

GO
/****** Object:  StoredProcedure [dbo].[sp_ns_mlns_get_by_user]    Script Date: 31/01/2023 4:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ns_mlns_get_by_user]
	@LNS nvarchar(max),
	@UserName nvarchar(100),
	@YearOfWork int,
	@LNSExcept nvarchar(max)
AS
BEGIN
	WITH LNSTree
		AS
		(
			SELECT *
			FROM NS_MucLucNganSach
			WHERE 
				sL = ''
				AND iNamLamViec = @YearOfWork
				AND (sLNS <> '' and sLNS in (SELECT * FROM f_split(@LNS)) or (@LNS = ''))
				AND ((@LNSExcept <> '' AND sLNS not in (SELECT * FROM f_split(@LNSExcept))) OR (@LNSExcept = ''))
			UNION ALL
			SELECT
				mlnsChild.*
			FROM NS_MucLucNganSach mlnsChild
			INNER JOIN LNSTree
				ON mlnsChild.iID_MLNS_Cha = lnstree.iID_MLNS
			WHERE 
				mlnsChild.sL = '' 
				AND mlnsChild.iNamLamViec = @YearOfWork
				AND ((@LNSExcept <> '' AND mlnsChild.sLNS not in (SELECT * FROM f_split(@LNSExcept))) OR (@LNSExcept = ''))
		)
		SELECT 
			mlns.* 
		FROM ( SELECT DISTINCT * FROM LNSTree) mlns
	    INNER JOIN 
		(
			SELECT 
				DISTINCT VALUE
			FROM 
			(
				SELECT 
					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
					CAST(sLNS AS nvarchar(10)) LNS 
				FROM
					NS_NguoiDung_LNS 
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
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_donvi]    Script Date: 31/01/2023 4:25:16 PM ******/
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
	@Dvt int
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
				AND cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
		)
		AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		AND (@LNS IS NULL OR sLNS in (select * from f_split(@LNS)))
		AND IDuLieuNhan = 0
		GROUP BY iID_MLNS
		union 
		select iID_MLNS,
			DuToan = 0,
			QuyetToan = TuChi,
			TrongKy = 0
		from f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		union
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
		sum(DuToanDonVi) as DuToanDonVi
		into #tblDataDonVi
	from (
		select iID_MLNS,
			iID_MaDonVi,
			QuyetToanDonVi = TuChi,
			DuToanDonVi = 0
		from f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)
		union
		SELECT iID_MLNS,
			iID_MaDonVi,
			QuyetToanDonVi = 0,
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
		isnull(dv.QuyetToanDonVi, 0) as QuyetToanDonVi,
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
		((isnull(rs.QuyetToan, 0) + isnull(rs.TrongKy, 0)) / @Dvt) AS QuyetToan, 
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
GO
