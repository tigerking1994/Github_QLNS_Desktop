/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 1/15/2024 5:28:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 1/15/2024 5:28:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
		FROM BH_DM_MucLucNganSach 
		WHERE 
		iNamLamViec = 2023 
		AND iTrangThai = 1
		AND (sLNS ='9010001' OR sLNS='9010002')

		SELECT 
		tblml.iID_MLNS as IID_MucLucNganSach,
		tblml.iID_MLNS_Cha as IdParent,
		tblml.sXauNoiMa  ,
		tblml.sLNS,
		tblml.sL,
		tblml.sK,
		tblml.sM,
		tblml.sTM,
		tblml.sTTM,
		tblml.sNG,
		tblml.sTNG,
		tblml.sTNG1,
		tblml.sTNG2,
		tblml.sTNG3,
		tblml.sMoTa,
		tblml.bHangCha,
		ROUND((SUM(ISNULL(chitiet.fTienCNVQP, 0))/@Dvt),0) fTienCNVQP,
		ROUND((SUM(ISNULL(chitiet.fTienDaThucHienNamTruoc, 0))/@Dvt),0) fTienDaThucHienNamTruoc,
		ROUND((SUM(ISNULL(chitiet.fTienHSQBS, 0))/@Dvt),0) fTienHSQBS,
		ROUND((SUM(ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0))/@Dvt),0) fTienKeHoachThucHienNamNay,
		ROUND((SUM(ISNULL(chitiet.fTienLDHD, 0))/@Dvt),0) fTienLDHD,
		ROUND((SUM(ISNULL(chitiet.fTienQNCN, 0))/@Dvt),0) fTienQNCN,
		ROUND((SUM(ISNULL(chitiet.fTienSQ, 0))/@Dvt),0) fTienSQ,
		ROUND((SUM(ISNULL(chitiet.fTienUocThucHienNamTruoc, 0))/@Dvt),0) fTienUocThucHienNamTruoc,
		ISNULL(chitiet.iSoCNVQP, 0) as iSoCNVQP,
		ISNULL(chitiet.iSoDaThucHienNamTruoc, 0) as iSoDaThucHienNamTruoc,
		ISNULL(chitiet.iSoHSQBS, 0) as iSoHSQBS,
		ISNULL(chitiet.iSoKeHoachThucHienNamNay, 0) as iSoKeHoachThucHienNamNay,
		ISNULL(chitiet.iSoLDHD, 0) as iSoLDHD,
		ISNULL(chitiet.iSoQNCN, 0) as iSoQNCN,
		ISNULL(chitiet.iSoSQ, 0) as iSoSQ,
		ISNULL(chitiet.iSoUocThucHienNamTruoc, 0) as iSoUocThucHienNamTruoc,
		chitiet.sGhiChu
		
		FROM #tblMlnsByPhanCap tblml
		LEFT JOIN
		(SELECT CTCT.* FROM
			BH_KHC_CheDoBHXH_ChiTiet CTCT
			WHERE  CTCT.iID_KHC_CheDoBHXH IN
			(
				SELECT CT.ID
						FROM BH_KHC_CheDoBHXH CT
						WHERE CT.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
							AND CT.sSoChungTu <> ''
							AND CT.sSoChungTu IS NOT NULL
							AND CT.iNamLamViec=@NamLamViec
			)) chitiet ON 

			chitiet.iID_MucLucNganSach=tblml.iID_MLNS

		GROUP BY tblml.iID_MLNS, tblml.iID_MLNS_Cha, tblml.sXauNoiMa, tblml.sLNS, tblml.sL, tblml.sK, tblml.sM, tblml.sTM, tblml.sTTM, tblml.sNG, tblml.sTNG, tblml.sTNG1
		, tblml.sTNG2, tblml.sTNG3, tblml.sMoTa, tblml.bHangCha, chitiet.iSoCNVQP,chitiet.iSoDaThucHienNamTruoc,  chitiet.iSoHSQBS, chitiet.iSoKeHoachThucHienNamNay, chitiet.iSoLDHD,
		chitiet.iSoQNCN, chitiet.iSoSQ, chitiet.iSoUocThucHienNamTruoc, chitiet.sGhiChu

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;
;
;
;
GO
