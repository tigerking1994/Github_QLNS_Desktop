/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet_tonghop_donvi]    Script Date: 3/29/2024 2:52:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_bhxh_chitiet_tonghop_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet_tonghop_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet_tonghop_donvi]    Script Date: 3/29/2024 2:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet_tonghop_donvi]
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
		iNamLamViec = @NamLamViec 
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
		tblml.bHangCha IsHangCha,
		ROUND((SUM(ISNULL(chitiet.fTienCNVQP, 0))/@Dvt),0) fTienCNVQP,
		ROUND((SUM(ISNULL(chitiet.fTienDaThucHienNamTruoc, 0))/@Dvt),0) fTienDaThucHienNamTruoc,
		ROUND((SUM(ISNULL(chitiet.fTienHSQBS, 0))/@Dvt),0) fTienHSQBS,
		ROUND((SUM(ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0))/@Dvt),0) fTienKeHoachThucHienNamNay,
		ROUND((SUM(ISNULL(chitiet.fTienLDHD, 0))/@Dvt),0) fTienLDHD,
		ROUND((SUM(ISNULL(chitiet.fTienQNCN, 0))/@Dvt),0) fTienQNCN,
		ROUND((SUM(ISNULL(chitiet.fTienSQ, 0))/@Dvt),0) fTienSQ,
		ROUND((SUM(ISNULL(chitiet.fTienUocThucHienNamTruoc, 0))/@Dvt),0) fTienUocThucHienNamTruoc,
		SUM(ISNULL(chitiet.iSoCNVQP, 0)) as iSoCNVQP,
		SUM(ISNULL(chitiet.iSoDaThucHienNamTruoc, 0)) as iSoDaThucHienNamTruoc,
		SUM(ISNULL(chitiet.iSoHSQBS, 0)) as iSoHSQBS,
		SUM(ISNULL(chitiet.iSoKeHoachThucHienNamNay, 0)) as iSoKeHoachThucHienNamNay,
		SUM(ISNULL(chitiet.iSoLDHD, 0)) as iSoLDHD,
		SUM(ISNULL(chitiet.iSoQNCN, 0)) as iSoQNCN,
		SUM(ISNULL(chitiet.iSoSQ, 0)) as iSoSQ,
		SUM(ISNULL(chitiet.iSoUocThucHienNamTruoc, 0)) as iSoUocThucHienNamTruoc,
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
		, tblml.sTNG2, tblml.sTNG3, tblml.sMoTa, tblml.bHangCha, chitiet.sGhiChu

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;
;
;
;
;
;
GO
