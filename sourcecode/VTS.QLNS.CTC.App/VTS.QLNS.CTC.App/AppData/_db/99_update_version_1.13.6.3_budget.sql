/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 04/12/2023 2:43:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_lns_thang_2]    Script Date: 04/12/2023 2:43:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_lns_thang_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_lns_thang_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_lns_thang_2]    Script Date: 04/12/2023 2:43:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_lns_thang_2]
	@YearOfWork int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthType int,
	@LoaiQuyetToan nvarchar(20)
AS
BEGIN

	SELECT DISTINCT sLNS AS LNS,
		LEFT(sLNS, 1) AS LNS1,
		LEFT(sLNS, 3) AS LNS3
	FROM NS_QT_ChungTuChiTiet ctct
	INNER JOIN NS_QT_ChungTu ct ON ctct.iID_QTChungTu = ct.iID_QTChungTu
	WHERE ct.sLoai in (select * from f_split(@LoaiQuyetToan))
	  AND ctct.iNamLamViec = @YearOfWork
	  AND ctct.iID_MaNguonNganSach = @BudgetSource
	  AND ctct.iID_MaDonVi in (SELECT * FROM f_split(@AgencyId))
	  AND (@QuarterMonth IS NULL
		   OR ctct.iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
	  AND (@QuarterMonthType IS NULL
		   OR ctct.iThangQuyLoai = @QuarterMonthType)
	  AND (fTuChi_DeNghi <> 0
		   OR fTuChi_PheDuyet <> 0);
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 04/12/2023 2:43:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@LNS nvarchar(max),
	@Dvt int,
	@IsInTongHop bit, 
	@IKhoi int
AS
BEGIN
declare @strChungTu nvarchar (max)
set @strChungTu=  (select sTongHop + ',' as 'data()' from NS_QT_ChungTu where  iID_MaDonVi in ( SELECT * FROM f_split(@AgencyId))  FOR XML PATH(''));
	
	SELECT * INTO #tempthongtridonvi
		FROM
		  (SELECT iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
		     AND iNamNganSach = @YearOfBudget
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
			 AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
		   GROUP BY iID_MaDonVi)AS ct 
		-- lay ten don vi
		JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id,
				  iKhoi
		   FROM DonVi
		   WHERE iTrangThai = 1
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
	
	if (@IsInTongHop = 0 OR @strChungTu = '')
		select * from #tempthongtridonvi;
	else if (@IsInTongHop = 1 AND EXISTS (SELECT * FROM #tempthongtridonvi where iKhoi is not null))
		select * from #tempthongtridonvi where @IKhoi = -1 OR iKhoi = @IKhoi;
	else

	SELECT *
		FROM
		  (SELECT ctct.iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet ctct 
		   INNER JOIN ns_qt_Chungtu ct on  ctct.iID_QTChungTu =  ct.iID_QTChungTu 
		   WHERE ctct.iNamLamViec = @YearOfWork
			 AND ctct.iNamNganSach = @YearOfBudget
			 AND ctct.iID_MaNguonNganSach = @BudgetSource
			 --AND (@AgencyId IS NULL OR ctct.iID_MaDonVi in  (select DonVi.iID_MaDonVi from DonVi  where DonVi.iID_Parent in ( SELECT * FROM f_split(@AgencyId))))
			 AND ct.bdatonghop = 1
			 AND (@QuarterMonth IS NULL OR ctct.iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
			 AND ct.sSoChungTu in (select * from f_split(Replace(@strChungTu, ' ', '')))
			 --AND ct.sSoChungTu in (select * from f_split(@strChungTu))
		   GROUP BY ctct.iID_MaDonVi)AS ct 
		-- lay ten don vi
		--LEFT JOIN
		INNER JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id,
				  iKhoi
		   FROM DonVi
		   WHERE iTrangThai = 1
		     AND (@IKhoi = -1 OR iKhoi = @IKhoi)
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
END
;
;
;
;
GO
