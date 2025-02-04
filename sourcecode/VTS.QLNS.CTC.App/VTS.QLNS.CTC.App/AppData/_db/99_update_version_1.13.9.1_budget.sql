/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns]    Script Date: 22/01/2024 10:56:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns]    Script Date: 22/01/2024 10:56:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns]
	 @NamLamViec int,
	 @CapPhatId nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @PhanCap nvarchar(10),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;

	SELECT DISTINCT VALUE
	INTO #tblLNS
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
			AND INamLamViec = @NamLamViec
			AND sLNS IN (SELECT * FROM f_split(@LNS))
	) LNS
	UNPIVOT
	(
		value
		FOR col in (LNS1, LNS3, LNS5, LNS)
	) un


	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN @PhanCap = 'M' and (sM is null OR sM = '') THEN cast(1 as bit)
				WHEN @PhanCap = 'TM' and (sTM is null OR sTM = '') THEN cast(1 as bit)
				WHEN @PhanCap = 'NG' and (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			INTO #tblMlnsByPhanCap
	FROM NS_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND (
			(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
			OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
			OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
			)
		AND sLNS IN (SELECT * FROM #tblLNS) 

	SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sMoTa as MoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTuChi, 0) / @Dvt as TuChi 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM NS_CP_ChungTuChiTiet WHERE iID_CTCapPhat = @CapPhatId AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))) ctct
	ON mlns.iID_MLNS = ctct.iID_MLNS 
	Order by mlns.sXauNoiMa
END
;
;
GO
