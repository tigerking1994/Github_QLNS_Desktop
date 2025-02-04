/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 2/19/2024 9:10:08 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 2/19/2024 9:10:08 AM ******/
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
			ANd ML.bHangChaDuToanDieuChinh is not null
	ORDER BY sXauNoiMa

	SELECT 
						chitiet.fTienDuToanDuocGiao, 
						chitiet.fTienSoSanhGiam, chitiet.fTienSoSanhTang, 
						dtct_by_donvi.fTienThucHien06ThangDauNam as fTienThucHien06ThangDauNam, 
						dtct_by_donvi.fTienUocThucHien06ThangCuoiNam as fTienUocThucHien06ThangCuoiNam, 
						dtct_by_donvi.fTienUocThucHienCaNam as fTienUocThucHienCaNam,
						chitiet.iID_BH_DTC,
						chitiet.iID_MucLucNganSach,
						dtct_by_donvi.sNoiMa,
						chitiet.sXauNoiMa,
						chitiet.iNamLamViec
					into #bh_dtc_dieuchinh_chitiet
					FROM BH_DTC_DieuChinhDuToanChi_ChiTiet chitiet
					LEFT JOIN (
						SELECT sTM, 
							iID_MaDonVi, 
							sNoiMa,
							sM,
							SUM(fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam, 
							SUM(fTienUocThucHien06ThangCuoiNam) as fTienUocThucHien06ThangCuoiNam,
							SUM(fTienUocThucHienCaNam) as fTienUocThucHienCaNam 
						FROM (
								SELECT *, 
									CASE WHEN sXauNoiMa LIKE '9010001%' THEN '9010001' 
									WHEN sXauNoiMa LIKE '9010002%' THEN '9010002' END as sNoiMa
								FROM BH_DTC_DieuChinhDuToanChi_ChiTiet 
							) BH_DTC_DieuChinhDuToanChi_ChiTiet
						WHERE iNamLamViec=@NamLamViec
						AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
						AND sTM != ''
						GROUP BY sTM, iID_MaDonVi, sNoiMa, sM
					) dtct_by_donvi ON chitiet.sTM = '' 
						AND chitiet.sM = dtct_by_donvi.sM 
						AND chitiet.iID_MaDonVi = dtct_by_donvi.iID_MaDonVi
						AND chitiet.sXauNoiMa LIKE (dtct_by_donvi.sNoiMa + '%')
					ORDER BY chitiet.sXauNoiMa

	SELECT 
		--ct.iID_MaDonVi,
		ct.sTenDonVi as STenDonVi,
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh bHangCha,
		--ml.bHangChaDuToanDieuChinh IsHangCha,
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
				#bh_dtc_dieuchinh_chitiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
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
		and ml.bHangChaDuToanDieuChinh = 1
		and ml.sTM = ''
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
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
		DROP TABLE #bh_dtc_dieuchinh_chitiet
END
;
;
;
;
;
GO
