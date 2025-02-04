/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 4/10/2024 3:01:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 4/10/2024 3:01:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 4/10/2024 3:01:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 4/10/2024 3:01:50 PM ******/
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
			
			LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
					, ctct.iID_MaDonVi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				GROUP BY ctct.sXauNoiMa,ctct.iID_MaDonVi) dt ON dt.sXauNoiMa=A.sXauNoiMa  and dt.iID_MaDonVi=A.iID_MaDonVi
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
			'          '+ dv.sTenDonVi sNoiDung,
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
		WHERE dv.iNamLamViec=@NamLamViec
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
			dt.fTienTuChi / @Dvt fTienDuToanDuocGiao,
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
			LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=B.sXauNoiMa 
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
				B.bHangCha,
				dt.fTienTuChi
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 4/10/2024 3:01:50 PM ******/
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 4/10/2024 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
AS
BEGIN
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			INTO #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @namLamViec 
		AND iTrangThai = 1
		and (sLNS='9010001' or sLNS='9010002')

		SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ct.fTienCNVQP,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienHSQBS,
		ct.fTienKeHoachThucHienNamNay,
		ct.fTienLDHD,
		ct.fTienQNCN,
		ct.fTienSQ,
		ct.fTienUocThucHienNamTruoc,
		ct.iID_KHC_CheDoBHXH
		INTO #tempchitiet
		FROM
		#tblMlnsByPhanCap mlns
		left join 
		(SELECT * FROM BH_KHC_CheDoBHXH_ChiTiet 
				WHERE iID_KHC_CheDoBHXH in
					( SELECT ID FROM BH_KHC_CheDoBHXH
								WHERE iNamLamViec=@namLamViec
								AND iID_MaDonVi IN (select * from f_split(@listTenDonVi))
								)) ct
								On mlns.iID_MLNS=ct.iID_MucLucNganSach

		WHERE  ct.iID_KHC_CheDoBHXH is not null

		SELECT 
		dv.sTenDonVi ,
		dv.iID_MaDonVi as IDDonVi,
		tblct.SM,
		tblct.SLNS,
		SUM(tblct.fTienKeHoachThucHienNamNay) TienKeHoachThucHienNamNay
		FROM #tempchitiet tblct
		inner join BH_KHC_CheDoBHXH ct on ct.ID=tblct.iID_KHC_CheDoBHXH
		left join DonVi dv on ct.iID_DonVi=dv.iID_DonVi and ct.iID_MaDonVi=dv.iID_MaDonVi
		WHERE dv.iNamLamViec=@namLamViec
		GROUP BY dv.sTenDonVi,
				dv.iID_MaDonVi, 
				tblct.SLNS, 
				tblct.SM
END
;
;
;
GO
