/****** Object:  StoredProcedure [dbo].[sp_qt_donvi_thang]    Script Date: 10/04/2024 4:54:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_donvi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_donvi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_donvi_thang]    Script Date: 10/04/2024 4:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_donvi_thang]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@QuarterMonth nvarchar(100),
	@QuarterMonthType int,
	@LoaiQuyetToan nvarchar(20)
AS
BEGIN
	SELECT dv.*
	FROM
	  (SELECT DISTINCT ctct.iID_MaDonVi
	   FROM NS_QT_ChungTuChiTiet ctct
	   JOIN NS_QT_ChungTu ct ON ctct.iID_QTChungTu = ct.iID_QTChungTu
	   WHERE 
		 ct.sLoai in (select * from f_split(@LoaiQuyetToan))
	     AND ctct.iNamLamViec = @YearOfWork
	     AND ctct.iNamNganSach = @YearOfBudget
		 AND ctct.iID_MaNguonNganSach = @BudgetSource
		 AND (@QuarterMonth IS NULL
			  OR ctct.iThangQuy in
				(SELECT *
				 FROM f_split(@QuarterMonth))) 
		AND ctct.iThangQuyLoai = @QuarterMonthType) AS ct -- lay ten don vi

	LEFT JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	ORDER BY iID_MaDonVi
END
;
;
;
GO
