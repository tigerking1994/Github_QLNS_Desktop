/****** Object:  StoredProcedure [dbo].[sp_tl_canbo_phucap_saochep]    Script Date: 9/13/2023 8:54:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_canbo_phucap_saochep]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_canbo_phucap_saochep]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_canbo_phucap_saochep]    Script Date: 9/13/2023 8:54:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 04/05/2022
-- Description:	Sao chép cán bộ sang tháng mới
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_canbo_phucap_saochep]
	@MaCanBo NVARCHAR(MAX),
	@FromYear int,
	@FromMonth int,
	@ToYear int,
	@ToMonth int,
	@IsCopyValue bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	WITH DsCanBo AS (
		SELECT
			Ma_CanBo,
			Ngay_NN,
			Ngay_XN,
			Ngay_TN,
			Thang_TNN,
			Ma_Hieu_CanBo
		FROM TL_DM_CanBo
		WHERE
			Ma_CanBo IN (SELECT * FROM f_split(@MaCanBo))
	), HsPhuCapTruyLinh AS (
		SELECT
			cboPhuCap.MA_CBO,
			cboPhuCap.MA_PHUCAP + '_CU' AS MA_PHUCAP,
			cboPhuCap.GIA_TRI
		FROM TL_CanBo_PhuCap cboPhuCap
		INNER JOIN DsCanBo cbo ON cboPhuCap.MA_CBO = cbo.Ma_CanBo
		WHERE cboPhuCap.MA_PHUCAP IN ('LHT_HS', 'PCCV_HS', 'PCTHUHUT_HS', 'PCCOV_HS', 'PCCU_HS')
	)

	SELECT
		NEWID()					AS Id,
		FORMAT(@ToYear, 'D4') + FORMAT(@ToMonth, 'D2') + cbo.Ma_Hieu_CanBo	AS MaCbo,
		cboPhuCap.MA_PHUCAP		AS MaPhuCap,
		CASE
			WHEN cboPhuCap.MA_PHUCAP IN ('LCS', 'GTNN', 'GTPT_DG', 'TA_DG') THEN phuCap.Gia_Tri 
			WHEN cboPhuCap.MA_PHUCAP IN ('LHT_HS_CU', 'PCCV_HS_CU', 'PCTHUHUT_HS_CU', 'PCCOV_HS_CU', 'PCCU_HS_CU') THEN phuCapTruyLinh.GIA_TRI
			WHEN cboPhuCap.MA_PHUCAP = 'TTL' THEN 0
			WHEN cboPhuCap.ISoThang_Huong IS NOT NULL AND phuCap.IThang_ToiDa IS NOT NULL AND cboPhuCap.ISoThang_Huong >= phuCap.IThang_ToiDa THEN 0
			WHEN cboPhuCap.MA_PHUCAP = 'NTN' THEN (select dbo.f_luong_ntn(cbo.Ngay_NN, cbo.Ngay_XN, cbo.Ngay_TN, cbo.Thang_TNN, @ToMonth, @ToYear))
			WHEN ISNULL(phuCap.bSaoChep, 0) = 0 OR (@IsCopyValue = 1 AND phuCap.bSaoChep = 1) THEN cboPhuCap.GIA_TRI ELSE 0
			--cboPhuCap.bSaoChep IS NOT NULL AND (@IsCopyValue = 0 OR cboPhuCap.bSaoChep = 0) THEN 0 ELSE cboPhuCap.GIA_TRI
		END						AS GiaTri,
		cboPhuCap.HE_SO			AS HeSo,
		cboPhuCap.MA_KMCP		AS MaKmcp,
		cboPhuCap.CONG_THUC		AS CongThuc,
		cboPhuCap.PHANTRAM_CT	AS PhanTramCt,
		cboPhuCap.CHON			AS Chon,
		CASE 
		WHEN cboPhuCap.MA_PHUCAP IN ('LHT_HS') then null ELSE cboPhuCap.HuongPC_SN
		END	                    AS HuongPcSn,
		0						AS Flag,
		cboPhuCap.DateStart		AS DateStart,
		cboPhuCap.DateEnd		AS DateEnd,
		CASE
			WHEN cboPhuCap.ISoThang_Huong IS NOT NULL AND (phuCap.IThang_ToiDa IS NULL OR cboPhuCap.ISoThang_Huong < phuCap.IThang_ToiDa) THEN cboPhuCap.ISoThang_Huong + 1
			ELSE cboPhuCap.ISoThang_Huong
		END						AS ISoThang_Huong,
		cboPhuCap.bSaoChep		AS BSaoChep
	FROM TL_CanBo_PhuCap cboPhuCap
	INNER JOIN DsCanBo cbo ON cboPhuCap.MA_CBO = cbo.Ma_CanBo
	LEFT JOIN HsPhuCapTruyLinh phuCapTruyLinh ON cboPhuCap.MA_CBO = phuCapTruyLinh.MA_CBO AND cboPhuCap.MA_PHUCAP = phuCapTruyLinh.MA_PHUCAP
	LEFT JOIN TL_DM_PhuCap phuCap ON cboPhuCap.MA_PHUCAP = phuCap.Ma_PhuCap
END
;
;
;
;
GO
