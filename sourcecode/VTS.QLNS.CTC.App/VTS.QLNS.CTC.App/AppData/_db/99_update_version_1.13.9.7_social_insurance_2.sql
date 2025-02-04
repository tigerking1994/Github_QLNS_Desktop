/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 2/5/2024 3:41:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]    Script Date: 2/5/2024 3:41:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet]    Script Date: 2/5/2024 3:41:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet]    Script Date: 2/5/2024 3:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@SLNS nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		
		ct.iID_MucLucNganSach,
		ct.iID_MaDonVi,
		ct.iNamLamViec,
	
		Sum(ISNULL(ct.fTienTuChi, 0))as FTienTuChi
		into #temp1
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DuToanChiTrenGiao bh
			JOIN 
				BH_DTC_DuToanChiTrenGiao_ChiTiet bhct ON bh.ID = bhct.iID_DTC_DuToanChiTrenGiao 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
				AND bh.iID_LoaiDanhMucChi=@loaiDanhMucCapChi
				AND bh.iNamLamViec=@NamLamViec
		) ct
		Group by 
		ct.iID_MucLucNganSach,
		ct.iID_MaDonVi,
		ct.iNamLamViec
		;

		SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as iID_MucLucNganSach,
			   tbl.FTienTuChi as fTienTuChi,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #temp1  tbl 
			on dm.iID_MLNS=tbl.iID_MucLucNganSach
			where dm.iNamLamViec=@NamLamViec 
			and dm.sLNS in (select * from splitstring(@SLNS))
			order by dm.sXauNoiMa

			drop table #temp1

END
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]    Script Date: 2/5/2024 3:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@SLNS nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		
		ct.iID_MucLucNganSach,
		ct.iID_MaDonVi,
		ct.iNamLamViec,
	
		Sum(ISNULL(ct.fTienTuChi, 0))as FTienTuChi
		into #temp1
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_PhanBoDuToanChi bh
			JOIN 
				BH_DTC_PhanBoDuToanChi_ChiTiet bhct ON bh.ID = bhct.iID_DTC_PhanBoDuToanChi 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
				AND bh.iID_LoaiDanhMucChi=@loaiDanhMucCapChi
				AND bh.iNamChungTu=@NamLamViec
		) ct
		Group by 
		ct.iID_MucLucNganSach,
		ct.iID_MaDonVi,
		ct.iNamLamViec
		;

		SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as iID_MLNS,
			   tbl.FTienTuChi as fTienTuChi,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #temp1  tbl 
			on dm.iID_MLNS=tbl.iID_MucLucNganSach
			where dm.iNamLamViec=@NamLamViec 
			and dm.sLNS in (select * from splitstring(@SLNS))
			order by dm.sXauNoiMa

			drop table #temp1

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 2/5/2024 3:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(200),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int
AS
BEGIN

	SELECT 
			B.iID_MLNS as iID_MucLucNganSach,
			B.iID_MLNS_Cha as IdParent,
			B.bHangCha as IsHangCha,
			B.sLNS,
			B.sL,
			B.sM,
			B.sTM,
			b.sMoTa sNoiDung,
			B.sXauNoiMa,
			B.sDuToanChiTietToi,
			A.fTienDuToanDuocGiao / @Dvt fTienDuToanDuocGiao,
			A.fTienSoSanhGiam/ @Dvt fTienSoSanhGiam,
			A.fTienSoSanhTang/ @Dvt fTienSoSanhTang,
			A.fTienThucHien06ThangDauNam/ @Dvt fTienThucHien06ThangDauNam,
			A.fTienUocThucHien06ThangCuoiNam / @Dvt fTienUocThucHien06ThangCuoiNam,
			A.fTienUocThucHienCaNam / @Dvt fTienUocThucHienCaNam,
			A.iID_MaDonVi,
			1 TYPE
			into #temp1
		FROM BH_DM_MucLucNganSach B
		LEFT JOIN (
			SELECT * FROM BH_DTC_DieuChinhDuToanChi_ChiTiet
			WHERE iID_BH_DTC IN 
								(SELECT iID_BH_DTC
									FROM BH_DTC_DieuChinhDuToanChi 
									WHERE iID_MaDonVi in (select * from splitstring(@IdDonVi))
									AND  iID_LoaiCap=@IDLoaiChi
									AND iNamLamViec=@NamLamViec
										)
			) A ON A.iID_MucLucNganSach=B.iID_MLNS
			WHERE B.iNamLamViec=@NamLamViec
			AND (A.fTienDuToanDuocGiao<>0 
			OR A.fTienSoSanhGiam<>0 
			OR A.fTienSoSanhTang<>0 
			OR A.fTienThucHien06ThangDauNam<>0
			OR A.fTienUocThucHien06ThangCuoiNam<>0
			OR A.fTienUocThucHienCaNam<>0)

	SELECT 
		    tbl1.iID_MucLucNganSach,
			tbl1.IdParent,
			tbl1.IsHangCha ,
			tbl1.sLNS,
			tbl1.sL,
			tbl1.sM,
			tbl1.sTM,
			tbl1.sDuToanChiTietToi,
			dv.sTenDonVi sNoiDung,
			tbl1.sXauNoiMa,
			tbl1.fTienDuToanDuocGiao,
			tbl1.fTienSoSanhGiam ,
			tbl1.fTienSoSanhTang ,
			tbl1.fTienThucHien06ThangDauNam ,
			tbl1.fTienUocThucHien06ThangCuoiNam ,
			tbl1.fTienUocThucHienCaNam ,
			tbl1.iID_MaDonVi,
			tbl1.TYPE
			INTO #tempChillMlns
		FROM #temp1 tbl1
		LEFT JOIN  DonVi dv ON tbl1.iID_MaDonVi=dv.iID_MaDonVi
		WHERE dv.iNamLamViec=2024
		AND tbl1.sDuToanChiTietToi is null
		order by sXauNoiMa

		SELECT 
			B.iID_MLNS as iID_MucLucNganSach,
			B.iID_MLNS_Cha as IdParent,
			B.bHangCha as IsHangCha,
			B.sLNS,
			B.sL,
			B.sM,
			B.sTM,
			B.sDuToanChiTietToi,
			b.sMoTa sNoiDung,
			B.sXauNoiMa,
			Sum(A.fTienDuToanDuocGiao) / @Dvt fTienDuToanDuocGiao,
			Sum(A.fTienSoSanhGiam) /@Dvt fTienSoSanhGiam,
			Sum(A.fTienSoSanhTang) / @Dvt fTienSoSanhTang,
			Sum(A.fTienThucHien06ThangDauNam) / @Dvt fTienThucHien06ThangDauNam,
			Sum(A.fTienUocThucHien06ThangCuoiNam) / @Dvt fTienUocThucHien06ThangCuoiNam,
			Sum(fTienUocThucHienCaNam) / @Dvt fTienUocThucHienCaNam,
			null iID_MaDonVi,
			0 TYPE
			into #tblBhMlns
		FROM BH_DM_MucLucNganSach B
		LEFT JOIN (
			SELECT * FROM BH_DTC_DieuChinhDuToanChi_ChiTiet
			WHERE iID_BH_DTC IN 
								(SELECT iID_BH_DTC
									FROM BH_DTC_DieuChinhDuToanChi 
									WHERE iID_MaDonVi in (select * from splitstring(@IdDonVi))
									AND  iID_LoaiCap=@IDLoaiChi
									AND iNamLamViec=@NamLamViec
										)
			) A ON A.iID_MucLucNganSach=B.iID_MLNS

			WHERE B.iNamLamViec=@NamLamViec
			AND B.sLNS IN (SELECT * FROM splitstring(@SLNS))
			group by B.iID_MLNS,
				B.sLNS,
				B.sMoTa,
				B.iID_MLNS_Cha,
				B.sL,
				B.sM,
				B.sTM,
				B.sDuToanChiTietToi,
				B.sXauNoiMa,
				B.bHangCha
		order by sXauNoiMa

	-- map bảng 
		SELECT * INTO #tblMLNS FROM (
			SELECT *
			FROM #tblBhMlns tbl
			UNION ALL
			SELECT *
			FROM #tempChillMlns 
		) mlns

		select * from #tblMLNS
		order by sXauNoiMa, TYPE 


		DROP TABLE #tblMLNS	
		DROP TABLE #tblBhMlns	
		DROP TABLE #tempChillMlns	
		DROP TABLE #temp1
END
;
;
;
;
;
;
GO
