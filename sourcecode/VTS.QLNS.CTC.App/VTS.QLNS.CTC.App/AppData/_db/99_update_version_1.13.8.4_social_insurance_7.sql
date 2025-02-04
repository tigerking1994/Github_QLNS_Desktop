/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 12/01/2024 3:07:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 12/01/2024 3:07:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM splitstring(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
	ORDER BY sXauNoiMa

	SELECT 
		--ct.iID_MaDonVi,
		ct.sTenDonVi,
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangCha,
		ct.iNamLamViec,
		ISNULL(SUM(ct.fTienDuToanDuocGiao),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(ct.fTienSoSanhGiam),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				bh.sMoTa,
				CASE 
					WHEN (SELECT COUNT(*) FROM splitstring(@IdDonVi)) > 1 THEN ''
					ELSE ddv.sTenDonVi
				END as sTenDonVi,
				--ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		WHERE ml.iNamLamViec=@NamLamViec
		and ml.bHangCha = 1
		and ml.sTM = ''
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangCha,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ct.sTenDonVi
		--ct.iID_MaDonVi,
		ORDER BY sXauNoiMa;
		
		DROP TABLE #tblml		
END
;
;
;
GO
