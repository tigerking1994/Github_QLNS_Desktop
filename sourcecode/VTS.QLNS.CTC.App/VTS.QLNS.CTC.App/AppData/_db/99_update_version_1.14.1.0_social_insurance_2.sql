/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 3/6/2024 5:31:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 3/6/2024 5:31:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_khckcb_getBHYTQN]    Script Date: 3/6/2024 5:31:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_khckcb_getBHYTQN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_khckcb_getBHYTQN]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_khckcb_getBHYTQN]    Script Date: 3/6/2024 5:31:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_bhxh_khckcb_getBHYTQN]
	@sMaDonVi nvarchar(max),
	@iNamLamViec int,
	@fTyLeThu float,
	@fTyLeThuTheoPhanTram nvarchar(100)
	
AS
BEGIN

SELECT 
                NEWID() as IID_MucLucNganSach,
                (@fTyLeThuTheoPhanTram + '% ' + N'kế hoạch thu BHYT quân nhân') as SNoiDung,
                (sum(ISNULL(fTongThuBHYT,0)) * @fTyLeThu)/100 as FTienKeHoachThucHienNamNay,
                1 as IsRemainRow,
                1 as BHangCha,
                1 as IRemainRow

          FROM BH_KHT_BHXH_ChiTiet 
          WHERE 
                (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                AND iID_MaDonVi IN (SELECT * FROM splitstring(@sMaDonVi))
                AND iNamLamViec = @iNamLamViec
          UNION
          SELECT 
                NEWID() as IID_MucLucNganSach,
                N'Số còn lại' as SNoiDung,
                0 as FTienKeHoachThucHienNamNay,
                1 as IsRemainRow,
                1 as BHangCha,
                2 as IRemainRow;
END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 3/6/2024 5:31:17 PM ******/
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
	@Dvt int,
	@Loai int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM splitstring(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
			AND ML.bHangCha IS NOT NULL
	ORDER BY sXauNoiMa

	--- Chung tu thuong
	IF (@Loai=0)
	SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

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
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
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
		ml.sDuToanChiTietToi,
		dt.fTienTuChi
		ORDER BY sXauNoiMa
		--- Chung tu tong hop
		ELSE 
		SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

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
		LEFT JOIN (
					SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
							WHERE ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND BIsKhoa = 1
							AND ct.iID_LoaiDanhMucChi=@IDLoaiChi
							AND ct.iNamLamViec = @NamLamViec
							GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
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
		ml.sDuToanChiTietToi,
		dt.fTienTuChi
		ORDER BY sXauNoiMa;

		DROP TABLE #tblml

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 3/6/2024 5:31:17 PM ******/
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
GO
