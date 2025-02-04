/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 12/19/2024 5:27:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 12/19/2024 5:27:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(200),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int
AS
BEGIN

	SELECT 
		distinct
		ct.dNgayChungTu
		, ct.iID_MaDonVi as  iID_MaDonVi
		into #tempTblNgayChungTuDonVi
	FROM BH_DTC_DieuChinhDuToanChi ct
	left join  BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ct.iID_BH_DTC=ctct.iID_BH_DTC
	where ctct.iNamLamViec=@NamLamViec
		and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
		and ct.iID_LoaiCap=@IDLoaiChi

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
			LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date) 
				GROUP BY ctct.sXauNoiMa,ctct.iID_MaDonVi) dt ON dt.sXauNoiMa=A.sXauNoiMa  and dt.iID_MaDonVi=A.iID_MaDonVi
			WHERE B.iNamLamViec=@NamLamViec
			AND B.iTrangThai=1
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
			'         + ' + dv.sTenDonVi sNoiDung,
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
		AND ISNULL(tbl1.sDuToanChiTietToi, '') = ''
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
			LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date) 
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=B.sXauNoiMa 
			WHERE B.iNamLamViec=@NamLamViec
			AND B.sLNS IN (SELECT * FROM splitstring(@SLNS))
			AND B.iTrangThai=1
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
;
;
;
GO
