/****** Object:  StoredProcedure [dbo].[sp_rpt_pbdt_giao_du_toan_don_vi]    Script Date: 12/16/2024 4:44:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_pbdt_giao_du_toan_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_pbdt_giao_du_toan_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_pbdt_giao_du_toan_don_vi]    Script Date: 12/16/2024 4:44:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_pbdt_giao_du_toan_don_vi]
	-- Add the parameters for the stored procedure here
	@IIDMaDonVis nvarchar(max),
	@VoucherIds nvarchar(max),
	@DonViTinh int ,
	@YearOfWork int ,
	@YearOfBudget int ,
	@BudgetSource int 
AS
BEGIN
	-- GET DATA DETAILS
	SELECT ml.iID_MLNS MlnsId,
		   ml.iID_MLNS_Cha MlnsIdParent,
		   ml.sLNS Lns,
		   ml.sL L,
		   ml.sk K,
		   ml.sM M,
		   ml.sTM Tm,
		   ml.sTTM Ttm,
		   ml.sNG Ng,
		   ml.sTNG Tng,
		   ml.sTNG1 Tng1,
		   ml.sTNG2 Tng2,
		   ml.sTNG3 Tng3,
		   ml.sXauNoiMa XauNoiMa,
		   ml.sMoTa NoiDung,	   
		   ROUND(CAST(SUM(ctct.TuChi)  as float) / @DonViTinh,0) AS TuChi, 
		   --ml.bHangCha BHangCha,
		   ml.sDuToanChiTietToi SChiTietToi,
		   ctct.Id_DonVi IdDonVi,
		   dv.sTenDonVi TenDonVi,
		   dv.sMaSoKBNN SMaSoKBNN
		INTO #tmp  from NS_MucLucNganSach ml 
	INNER JOIN TN_DT_ChungTuChiTiet ctct ON ctct.XauNoiMa = ml.sXauNoiMa
	INNER JOIN TN_DT_ChungTu ct ON ct.ID = ctct.Id_ChungTu
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = ctct.Id_DonVi
	WHERE  ml.iNamLamViec = @YearOfWork 
		  AND ct.NamLamViec = @YearOfWork and ct.NguonNganSach = @BudgetSource AND ct.NamNganSach = @YearOfBudget
		  AND ct.Id IN (SELECT * FROM splitstring(@VoucherIds))
		  AND ctct.Id_DonVi IN (SELECT * FROM splitstring(@IIDMaDonVis))
		  AND dv.iNamLamViec = @YearOfWork
	GROUP BY ml.iID_MLNS, ml.iID_MLNS_Cha,
			 ml.sLNS ,
		     ml.sL ,
		     ml.sk ,
		     ml.sM ,
		     ml.sTM ,
		     ml.sTTM ,
		     ml.sNG ,
		     ml.sTNG ,
		     ml.sTNG1 ,
		     ml.sTNG2 ,
		     ml.sTNG3 ,
		     ml.sXauNoiMa,
		     ml.sMoTa,
		     ml.sDuToanChiTietToi,
			 ctct.Id_DonVi ,
			 dv.sTenDonVi ,
			 dv.sMaSoKBNN ;
	
	---- GETDATA RESULTS ----
	WITH tree
	AS
	(
		SELECT *, CAST(0 AS bit) AS BHangCha FROM #tmp
		UNION ALL
		SELECT
		   pr.iID_MLNS MlnsId,
		   pr.iID_MLNS_Cha MlnsIdParent,
		   pr.sLNS Lns,
		   pr.sL L,
		   pr.sk K,
		   pr.sM M,
		   pr.sTM Tm,
		   pr.sTTM Ttm,
		   pr.sNG Ng,
		   pr.sTNG Tng,
		   pr.sTNG1 Tng1,
		   pr.sTNG2 Tng2,
		   pr.sTNG3 Tng3,
		   pr.sXauNoiMa XauNoiMa,
		   pr.sMoTa NoiDung,	   
		   CAST(0 as float) AS TuChi,
		   pr.sDuToanChiTietToi SChiTietToi,
		   CAST('' AS nvarchar(50)) AS IdDonVi,
		   CAST('' AS nvarchar(500))  AS TenDonVi,
		   CAST('' AS nvarchar(50))  AS SMaSoKBNN,
		   pr.bHangCha BHangCha
		FROM NS_MucLucNganSach pr
		INNER JOIN tree chil on pr.iID_MLNS = chil.MlnsIdParent
		WHERE pr.iNamLamViec = @YearOfWork  and pr.iID_MLNS <> chil.MlnsId
	)

	SELECT DISTINCT * FROM tree ORDER BY XauNoiMa;
	DROP TABLE #tmp;
	
END
;
GO
