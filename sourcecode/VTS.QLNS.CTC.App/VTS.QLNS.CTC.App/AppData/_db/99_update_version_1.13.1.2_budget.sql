/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_lns_sochungtu]    Script Date: 24/08/2023 1:50:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_lns_sochungtu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_lns_sochungtu]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_lns_sochungtu]    Script Date: 24/08/2023 1:50:47 PM ******/
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
			iID_MLNS_Cha,
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
			mlnsParent.iID_MLNS_Cha,
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
GO
