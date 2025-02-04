/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 1/11/2024 7:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 1/11/2024 7:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(max),
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
		ISNULL(SUM(ct.fTienDuToanDuocGiao) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
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
		ct.iNamLamViec
		ORDER BY sXauNoiMa;
		
		DROP TABLE #tblml


		----- PHÂN BO
	--	SELECT
	--	ct.iID_MaDonVi,
	--	ct.iID_MucLucNganSach,
	--	ct.sM,
	--	ct.sTM,
	--	ct.sNoiDung,
	--	ct.sXauNoiMa,
	--	ct.iNamLamViec,
	--	ct.sTenDonVi,
	--	ISNULL(SUM(ct.fTongTien), 0) as fTienDuToanDuocGiao,
	--	0 fTienThucHien06ThangDauNam,
	--	0 fTienUocThucHien06ThangCuoiNam,
	--	0 fTienUocThucHienCaNam,
	--	0 fTienSoSanhTang,
	--	0 fTienSoSanhGiam

	--	into #tblpbdt
	--FROM
	--	(
	--		SELECT
	--			bh.sMoTa,
	--			ddv.sTenDonVi,
	--			bhct.*
	--		FROM 
	--			BH_DTC_PhanBoDuToanChi bh
	--		JOIN 
	--			BH_DTC_PhanBoDuToanChi_ChiTiet bhct ON bh.ID = bhct.iID_DTC_PhanBoDuToanChi 
	--		LEFT JOIN 
	--			(SELECT * FROM DonVi WHERE iNamLamViec = 2023 AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
	--		WHERE
	--			bhct.iID_MaDonVi IN (SELECT * FROM splitstring('001'))
	--			and bh.bIsKhoa = 1
	--			AND bh.sSoQuyetDinh IS NOT NULL
	--			AND bh.sSoQuyetDinh <> ''
	--			AND cast(bh.DngayChungTu as date) <= cast('2023-12-05' as date)
	--			AND bh.iID_LoaiDanhMucChi='1E909740-3235-4BE4-B992-6C7D101EC384'
	--			AND bh.iNamChungTu=2023
	--	) ct

	--	GROUP BY ct.iID_MaDonVi,
	--	ct.iID_MucLucNganSach,
	--	ct.sM,
	--	ct.sTM,
	--	ct.sNoiDung,
	--	ct.sXauNoiMa,
	--	ct.iNamLamViec,
	--	ct.sTenDonVi;


		--DROP TABLE #tblpbdt
END
;
;
;

GO
