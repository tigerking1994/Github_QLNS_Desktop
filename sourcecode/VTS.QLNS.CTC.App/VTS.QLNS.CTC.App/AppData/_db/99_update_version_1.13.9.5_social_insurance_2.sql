/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 1/29/2024 5:18:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 1/29/2024 5:18:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 1/29/2024 5:18:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 1/30/2024 8:25:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_muclucngansach_theodieuchinh]    Script Date: 1/26/2024 4:44:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_muclucngansach_theodieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_muclucngansach_theodieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 1/29/2024 5:18:49 PM ******/
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
			AND ML.bHangChaDuToanDieuChinh IS NOT NULL
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
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
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
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ml.sDuToanChiTietToi
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 1/29/2024 5:18:49 PM ******/
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

	if @SLNS='9010003'

					select '9010003' sXauNoiMa, null iID_MaDonVi
					,N'Chi kinh phí quản lý BHXH, BHYT' sTenDonVi
					, null fTienUocThucHienCaNam
					, null fTienThucHien06ThangDauNam
					, null fTienUocThucHien06ThangCuoiNam
					, null fTienSoSanhTang
					, null fTienSoSanhGiam
					, null fTienDuToanDuocGiao
					, 0 Type
					union all
					select
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi,
						sum(ctct.fTienUocThucHienCaNam)/ @Dvt fTienUocThucHienCaNam,
						sum(ctct.fTienThucHien06ThangDauNam)/ @Dvt fTienThucHien06ThangDauNam,
						sum(ctct.fTienUocThucHien06ThangCuoiNam)/ @Dvt fTienUocThucHien06ThangCuoiNam,
						sum(ctct.fTienSoSanhTang)/ @Dvt fTienSoSanhTang,
						sum(ctct.fTienSoSanhGiam)/@Dvt fTienSoSanhGiam,
						sum(ctct.fTienDuToanDuocGiao)/ @Dvt fTienDuToanDuocGiao,
						1 Type
					from 
					BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
					join BH_DTC_DieuChinhDuToanChi ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
					left join DonVi A on A.iID_MaDonVi=ctct.iID_MaDonVi
					where ct.iID_MaDonVi in ((SELECT * FROM f_split(@IdDonVi))			)
					and ct.iID_LoaiCap=@IDLoaiChi
						and ct.iNamLamViec = @NamLamViec -- @NamLamViec
						and A.iNamLamViec=@NamLamViec
						and ctct.sXauNoiMa in (SELECT * FROM f_split('9010003'))
						--and ct.iLoaiTongHop =  'B04C48E3-1BBC-4981-B59E-BBDF0CCECE84'
					group by
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi
			ELSE IF @SLNS='9010004'
			select '9010004' sXauNoiMa, null iID_MaDonVi
					,N'Kinh phí KCB tại quân y đơn vị 10%' sTenDonVi
					, null fTienUocThucHienCaNam
					, null fTienThucHien06ThangDauNam
					, null fTienUocThucHien06ThangCuoiNam
					, null fTienSoSanhTang
					, null fTienSoSanhGiam
					, null fTienDuToanDuocGiao
					, 0 Type
					union all
					select
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi,
						sum(ctct.fTienUocThucHienCaNam)/ @Dvt fTienUocThucHienCaNam,
						sum(ctct.fTienThucHien06ThangDauNam)/ @Dvt fTienThucHien06ThangDauNam,
						sum(ctct.fTienUocThucHien06ThangCuoiNam)/ @Dvt fTienUocThucHien06ThangCuoiNam,
						sum(ctct.fTienSoSanhTang)/ @Dvt fTienSoSanhTang,
						sum(ctct.fTienSoSanhGiam)/@Dvt fTienSoSanhGiam,
						sum(ctct.fTienDuToanDuocGiao)/ @Dvt fTienDuToanDuocGiao,
						 1 Type
					from 
					BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
					join BH_DTC_DieuChinhDuToanChi ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
					left join DonVi A on A.iID_MaDonVi=ctct.iID_MaDonVi
					where ct.iID_MaDonVi in ((SELECT * FROM f_split(@IdDonVi))			)
					and ct.iID_LoaiCap=@IDLoaiChi
						and ct.iNamLamViec = @NamLamViec -- @NamLamViec
						and A.iNamLamViec=@NamLamViec
						and ctct.sXauNoiMa in (SELECT * FROM f_split('9010004'))
						--and ct.iLoaiTongHop =  'B04C48E3-1BBC-4981-B59E-BBDF0CCECE84'
					group by
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi
			ELSE IF @SLNS='9010006'
			select '9010006' sXauNoiMa, null iID_MaDonVi
					,N'Kinh phí KCB tại Trường Sa - DK ' sTenDonVi
					, null fTienUocThucHienCaNam
					, null fTienThucHien06ThangDauNam
					, null fTienUocThucHien06ThangCuoiNam
					, null fTienSoSanhTang
					, null fTienSoSanhGiam
					, null fTienDuToanDuocGiao
					, 0 Type
					union all
					select
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi,
						sum(ctct.fTienUocThucHienCaNam)/ @Dvt fTienUocThucHienCaNam,
						sum(ctct.fTienThucHien06ThangDauNam)/ @Dvt fTienThucHien06ThangDauNam,
						sum(ctct.fTienUocThucHien06ThangCuoiNam)/ @Dvt fTienUocThucHien06ThangCuoiNam,
						sum(ctct.fTienSoSanhTang)/ @Dvt fTienSoSanhTang,
						sum(ctct.fTienSoSanhGiam)/@Dvt fTienSoSanhGiam,
						sum(ctct.fTienDuToanDuocGiao)/ @Dvt fTienDuToanDuocGiao,
						 1 Type
					from 
					BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
					join BH_DTC_DieuChinhDuToanChi ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
					left join DonVi A on A.iID_MaDonVi=ctct.iID_MaDonVi
					where ct.iID_MaDonVi in ((SELECT * FROM f_split(@IdDonVi))			)
					and ct.iID_LoaiCap=@IDLoaiChi
						and ct.iNamLamViec = @NamLamViec -- @NamLamViec
						and A.iNamLamViec=@NamLamViec
						and ctct.sXauNoiMa in (SELECT * FROM f_split('9010006'))
						--and ct.iLoaiTongHop =  'B04C48E3-1BBC-4981-B59E-BBDF0CCECE84'
					group by
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi
			ELSE IF @SLNS='9010008'
			select '9010008' sXauNoiMa, null iID_MaDonVi
					,N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân' sTenDonVi
					, null fTienUocThucHienCaNam
					, null fTienThucHien06ThangDauNam
					, null fTienUocThucHien06ThangCuoiNam
					, null fTienSoSanhTang
					, null fTienSoSanhGiam
					, null fTienDuToanDuocGiao
					, 0 Type
					union all
					select
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi,
						sum(ctct.fTienUocThucHienCaNam)/ @Dvt fTienUocThucHienCaNam,
						sum(ctct.fTienThucHien06ThangDauNam)/ @Dvt fTienThucHien06ThangDauNam,
						sum(ctct.fTienUocThucHien06ThangCuoiNam)/ @Dvt fTienUocThucHien06ThangCuoiNam,
						sum(ctct.fTienSoSanhTang)/ @Dvt fTienSoSanhTang,
						sum(ctct.fTienSoSanhGiam)/@Dvt fTienSoSanhGiam,
						sum(ctct.fTienDuToanDuocGiao)/ @Dvt fTienDuToanDuocGiao,
						 1 Type
					from 
					BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
					join BH_DTC_DieuChinhDuToanChi ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
					left join DonVi A on A.iID_MaDonVi=ctct.iID_MaDonVi
					where ct.iID_MaDonVi in ((SELECT * FROM f_split(@IdDonVi))			)
					and ct.iID_LoaiCap=@IDLoaiChi
						and ct.iNamLamViec = @NamLamViec -- @NamLamViec
						and A.iNamLamViec=@NamLamViec
						and ctct.sXauNoiMa in (SELECT * FROM f_split('9010008'))
						--and ct.iLoaiTongHop =  'B04C48E3-1BBC-4981-B59E-BBDF0CCECE84'
					group by
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi
			ELSE IF @SLNS='9010009'
			select '9010009' sXauNoiMa, null iID_MaDonVi
					,N'Kinh phí mua sắm trang thiết bị y tế' sTenDonVi
					, null fTienUocThucHienCaNam
					, null fTienThucHien06ThangDauNam
					, null fTienUocThucHien06ThangCuoiNam
					, null fTienSoSanhTang
					, null fTienSoSanhGiam
					, null fTienDuToanDuocGiao
					, 0 Type
					union all
					select
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi,
						sum(ctct.fTienUocThucHienCaNam)/ @Dvt fTienUocThucHienCaNam,
						sum(ctct.fTienThucHien06ThangDauNam)/ @Dvt fTienThucHien06ThangDauNam,
						sum(ctct.fTienUocThucHien06ThangCuoiNam)/ @Dvt fTienUocThucHien06ThangCuoiNam,
						sum(ctct.fTienSoSanhTang)/ @Dvt fTienSoSanhTang,
						sum(ctct.fTienSoSanhGiam)/@Dvt fTienSoSanhGiam,
						sum(ctct.fTienDuToanDuocGiao)/ @Dvt fTienDuToanDuocGiao,
						1 Type
					from 
					BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
					join BH_DTC_DieuChinhDuToanChi ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
					left join DonVi A on A.iID_MaDonVi=ctct.iID_MaDonVi
					where ct.iID_MaDonVi in ((SELECT * FROM f_split(@IdDonVi))			)
					and ct.iID_LoaiCap=@IDLoaiChi
						and ct.iNamLamViec = @NamLamViec -- @NamLamViec
						and A.iNamLamViec=@NamLamViec
						and ctct.sXauNoiMa in (SELECT * FROM f_split('9010009'))
						--and ct.iLoaiTongHop =  'B04C48E3-1BBC-4981-B59E-BBDF0CCECE84'
					group by
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi

			ELSE IF @SLNS='9010010'
			select '9010010' sXauNoiMa, null iID_MaDonVi
					,N'Chi hỗ trợ người lao động tham gia BHTN' sTenDonVi
					, null fTienUocThucHienCaNam
					, null fTienThucHien06ThangDauNam
					, null fTienUocThucHien06ThangCuoiNam
					, null fTienSoSanhTang
					, null fTienSoSanhGiam
					, null fTienDuToanDuocGiao
					, 0 Type
					union all
					select
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi,
						sum(ctct.fTienUocThucHienCaNam)/ @Dvt fTienUocThucHienCaNam,
						sum(ctct.fTienThucHien06ThangDauNam)/ @Dvt fTienThucHien06ThangDauNam,
						sum(ctct.fTienUocThucHien06ThangCuoiNam)/ @Dvt fTienUocThucHien06ThangCuoiNam,
						sum(ctct.fTienSoSanhTang)/ @Dvt fTienSoSanhTang,
						sum(ctct.fTienSoSanhGiam)/@Dvt fTienSoSanhGiam,
						sum(ctct.fTienDuToanDuocGiao)/ @Dvt fTienDuToanDuocGiao,
						 1 Type
					from 
					BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
					join BH_DTC_DieuChinhDuToanChi ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
					left join DonVi A on A.iID_MaDonVi=ctct.iID_MaDonVi
					where ct.iID_MaDonVi in ((SELECT * FROM f_split(@IdDonVi))			)
					and ct.iID_LoaiCap=@IDLoaiChi
						and ct.iNamLamViec = @NamLamViec -- @NamLamViec
						and A.iNamLamViec=@NamLamViec
						and ctct.sXauNoiMa in (SELECT * FROM f_split('9010010'))
						--and ct.iLoaiTongHop =  'B04C48E3-1BBC-4981-B59E-BBDF0CCECE84'
					group by
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi
				ELSE
				select '905' sXauNoiMa, null iID_MaDonVi
					,N'Kinh phí CSSK ban đầu NLĐ và HSSV' sTenDonVi
					, null fTienUocThucHienCaNam
					, null fTienThucHien06ThangDauNam
					, null fTienUocThucHien06ThangCuoiNam
					, null fTienSoSanhTang
					, null fTienSoSanhGiam
					, null fTienDuToanDuocGiao
					, 0 Type
					union all
					select
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi,
						sum(ctct.fTienUocThucHienCaNam)/ @Dvt fTienUocThucHienCaNam,
						sum(ctct.fTienThucHien06ThangDauNam)/ @Dvt fTienThucHien06ThangDauNam,
						sum(ctct.fTienUocThucHien06ThangCuoiNam)/ @Dvt fTienUocThucHien06ThangCuoiNam,
						sum(ctct.fTienSoSanhTang)/ @Dvt fTienSoSanhTang,
						sum(ctct.fTienSoSanhGiam)/@Dvt fTienSoSanhGiam,
						sum(ctct.fTienDuToanDuocGiao)/ @Dvt fTienDuToanDuocGiao,
						1 Type
					from 
					BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
					join BH_DTC_DieuChinhDuToanChi ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
					left join DonVi A on A.iID_MaDonVi=ctct.iID_MaDonVi
					where ct.iID_MaDonVi in ((SELECT * FROM f_split(@IdDonVi))			)
					and ct.iID_LoaiCap=@IDLoaiChi
						and ct.iNamLamViec = @NamLamViec -- @NamLamViec
						and A.iNamLamViec=@NamLamViec
						and ctct.sXauNoiMa in (SELECT * FROM f_split('905'))
						--and ct.iLoaiTongHop =  'B04C48E3-1BBC-4981-B59E-BBDF0CCECE84'
					group by
						ctct.sXauNoiMa,
						ct.iID_MaDonVi,
						A.sTenDonVi
		
		--DROP TABLE #tblml		
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 1/29/2024 5:18:49 PM ******/
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
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 1/30/2024 8:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]
@INamLamViec int,
@IdDonVi NVARCHAR(MAX),
@DonViTinh int,
@LNS NVARCHAR(MAX),
@IsTongHop int
as
begin
	--- Lấy danh sách các đơn vi được chọn
	select 
		ROW_NUMBER() OVER(PARTITION BY DonVi.iKhoi  ORDER BY DonVi.iID_MaDonVi ASC) AS sTT,
		DonVi.iID_DonVi,
		DonVi.iID_MaDonVi,
		DonVi.sTenDonVi,
		DonVi.iKhoi
	into  #tblDonVi
	from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--- Lấy danh sách mục lục ngân sách
	select 
		BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
		BH_DM_MucLucNganSach.iID_MLNS_Cha,
		BH_DM_MucLucNganSach.sLNS,
		BH_DM_MucLucNganSach.sL,
		BH_DM_MucLucNganSach.sK,
		BH_DM_MucLucNganSach.sM,
		BH_DM_MucLucNganSach.sTM,
		BH_DM_MucLucNganSach.sTTM,
		BH_DM_MucLucNganSach.sNG,
		BH_DM_MucLucNganSach.sTNG,
		BH_DM_MucLucNganSach.sXauNoiMa,
		BH_DM_MucLucNganSach.sMoTa as sNoiDung,
		BH_DM_MucLucNganSach.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach 
		where   iNamLamViec = @INamLamViec  and (sLNS  in ('9010001', '9010002'))


	--- hiển thị mục lục ngân sách theo đơn vị
	select  
		case when #tblMucLucNganSach.sLNS = '9010001' then N'Khối dự toán' else N'Khối hạch toán' end sTenDonVi,
		#tblMucLucNganSach.sLNS,
		#tblDonVi.iID_DonVi,
		#tblDonVi.iID_MaDonVi,
		#tblDonVi.iKhoi
	into #donvi_MLNS
	from #tblMucLucNganSach cross join #tblDonVi 
	where #tblMucLucNganSach.sL =''

	---Lấy thông tin quyết toán chi tiết 
	select 
		tbl_qtc.iKhoi,
		tbl_qtc.iID_MaDonVi,
		tbl_qtc.sLNS,
		tbl_qtc.sL,
		tbl_qtc.sK,
		tbl_qtc.sM,
		case when tbl_qtc.sM ='0001' then tbl_qtc.fTongTien_ThucChi else 0 end fTroCapOmDau,
		case when tbl_qtc.sM = '0002' then tbl_qtc.fTongTien_ThucChi else 0 end fTroCapThaiSan,
		case when tbl_qtc.sM = '0003' then tbl_qtc.fTongTien_ThucChi else 0 end fTroCapTaiNanNN,
		case when tbl_qtc.sM = '0004' then tbl_qtc.fTongTien_ThucChi else 0 end fTroCapHuuTri,
		case when tbl_qtc.sM = '0005' then tbl_qtc.fTongTien_ThucChi else 0 end fTroCapPhucVien,
		case when tbl_qtc.sM = '0006' then tbl_qtc.fTongTien_ThucChi else 0 end fTroCapXuatNgu,
		case when tbl_qtc.sM = '0007' then tbl_qtc.fTongTien_ThucChi else 0 end fTroCapThoiViec,
		case when tbl_qtc.sM = '0008' then tbl_qtc.fTongTien_ThucChi else 0 end fTroCapTuTuat
		--case when tbl_qtc.sM = 1 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapOmDau,
		--case when tbl_qtc.sM = 2 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapThaiSan,
		--case when tbl_qtc.sM = 3 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapTaiNanNN,
		--case when tbl_qtc.sM = 4 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapHuuTri,
		--case when tbl_qtc.sM = 5 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapPhucVien,
		--case when tbl_qtc.sM = 6 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapXuatNgu,
		--case when tbl_qtc.sM = 7 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapThoiViec,
		--case when tbl_qtc.sM = 8 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapTuTuat
	into #tbl_qtcn_chitiet
	from
	(
		select 
			Sum(qtcn_ct.fTongTien_ThucChi) as fTongTien_ThucChi,
			#tblDonVi.iKhoi,
			#tblDonVi.iID_MaDonVi,
			#tblMucLucNganSach.sLNS,
			#tblMucLucNganSach.sL,
			#tblMucLucNganSach.sK,
			#tblMucLucNganSach.sM

		from BH_QTC_Nam_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		inner join #tblMucLucNganSach on qtcn_ct.iID_MucLucNganSach = #tblMucLucNganSach.iID_MLNS
		inner join #tblDonVi on qtcn.iID_MaDonVi = #tblDonVi.iID_MaDonVi
		where qtcn.iNamLamViec = @INamLamViec 
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by #tblDonVi.iKhoi, #tblDonVi.iID_MaDonVi, #tblMucLucNganSach.sLNS, #tblMucLucNganSach.sL,#tblMucLucNganSach.sK,#tblMucLucNganSach.sM
	) as tbl_qtc


	--- Lấy dữ liệu cấp nhỏ nhất - cấp 4
	select 
		null as STT,
		#donvi_MLNS.sTenDonVi,
		#donvi_MLNS.iID_MaDonVi,
		#donvi_MLNS.iKhoi,
		#donvi_MLNS.sLNS,
		sum(#tbl_qtcn_chitiet.fTroCapOmDau) as fTroCapOmDau,
		sum(#tbl_qtcn_chitiet.fTroCapThaiSan) as fTroCapThaiSan,
		sum(#tbl_qtcn_chitiet.fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(#tbl_qtcn_chitiet.fTroCapHuuTri) as fTroCapHuuTri,
		sum(#tbl_qtcn_chitiet.fTroCapPhucVien) as fTroCapPhucVien,
		sum(#tbl_qtcn_chitiet.fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(#tbl_qtcn_chitiet.fTroCapThoiViec) as fTroCapThoiViec,
		sum(#tbl_qtcn_chitiet.fTroCapTuTuat) as fTroCapTuTuat,
		4 as level,
		0 as bHangCha
	into #tbl_cap4
	from #donvi_MLNS 
	left join #tbl_qtcn_chitiet on #donvi_MLNS.iID_MaDonVi = #tbl_qtcn_chitiet.iID_MaDonVi and #donvi_MLNS.iKhoi = #tbl_qtcn_chitiet.iKhoi
	and #tbl_qtcn_chitiet.sLNS = #donvi_MLNS.sLNS
	group by #donvi_MLNS.sTenDonVi, #donvi_MLNS.iID_MaDonVi, #donvi_MLNS.iKhoi, #donvi_MLNS.sLNS
	order by #donvi_MLNS.iKhoi,#donvi_MLNS.iID_MaDonVi

	--- Lấy dữ liệu cấp 3
	select 
		#tblDonVi.sTT,
		#tblDonVi.sTenDonVi ,
		#tblDonVi.iID_MaDonVi,
		#tblDonVi.iKhoi,
		'' as sLNS, 
		sum(#tbl_cap4.fTroCapOmDau) as fTroCapOmDau,
		sum(#tbl_cap4.fTroCapThaiSan) as fTroCapThaiSan,
		sum(#tbl_cap4.fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(#tbl_cap4.fTroCapHuuTri) as fTroCapHuuTri,
		sum(#tbl_cap4.fTroCapPhucVien) as fTroCapPhucVien,
		sum(#tbl_cap4.fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(#tbl_cap4.fTroCapThoiViec) as fTroCapThoiViec,
		sum(#tbl_cap4.fTroCapTuTuat) as fTroCapTuTuat,
		3 as level,
		0 as bHangCha
	into #tbl_cap3
	from #tblDonVi
	left join #tbl_cap4 on #tblDonVi.iID_MaDonVi = #tbl_cap4.iID_MaDonVi and #tblDonVi.iKhoi = #tbl_cap4.iKhoi
	group by #tblDonVi.sTT, #tblDonVi.sTenDonVi, #tblDonVi.iID_MaDonVi, #tblDonVi.iKhoi

	---Lấy dữ liệu đơn vị cấp 2
	select 
		null as STT,
		case when #tbl_cap4.sLNS = '9010001' then N'+Khối dự toán' else N'+Khối hạch toán' end sTenDonVi,
		'' as iID_MaDonVi,
		#tbl_cap4.iKhoi,
		#tbl_cap4.sLNS as sLNS, 
		sum(fTroCapOmDau) as fTroCapOmDau,
		sum(fTroCapThaiSan) as fTroCapThaiSan,
		sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(fTroCapHuuTri) as fTroCapHuuTri,
		sum(fTroCapPhucVien) as fTroCapPhucVien,
		sum(fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(fTroCapThoiViec) as fTroCapThoiViec,
		sum(fTroCapTuTuat) as fTroCapTuTuat,
		2 as level,
		1 as bHangCha
	into #tbl_cap2
	from #tbl_cap4
	group by #tbl_cap4.iKhoi, #tbl_cap4.sLNS


	---Lấy dữ liệu đơn vị cấp 1
	select 
		null as STT,
		case when #tbl_cap4.iKhoi = 2 then N'A.Đơn vị dự toán' else N'B.Đơn vị hạch toán' end sTenDonVi,
		'' as iID_MaDonVi,
		#tbl_cap4.iKhoi,
		'' as sLNS, 
		sum(fTroCapOmDau) as fTroCapOmDau,
		sum(fTroCapThaiSan) as fTroCapThaiSan,
		sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(fTroCapHuuTri) as fTroCapHuuTri,
		sum(fTroCapPhucVien) as fTroCapPhucVien,
		sum(fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(fTroCapThoiViec) as fTroCapThoiViec,
		sum(fTroCapTuTuat) as fTroCapTuTuat,
		1 as level,
		1 as bHangCha
	into #tbl_cap1
	from #tbl_cap4
	group by #tbl_cap4.iKhoi

	---Hiển thị kết quả trả về
	select 
		sTT,
		sTenDonVi,
		iID_MaDonVi,
		iKhoi,
		sLNS,
		fTroCapOmDau,
		fTroCapThaiSan,
		fTroCapTaiNanNN,
		fTroCapHuuTri,
		fTroCapPhucVien,
		fTroCapXuatNgu,
		fTroCapThoiViec,
		fTroCapTuTuat,
		level,
		bHangCha
	into #tblResult
	from
		(
		select * from #tbl_cap1
		union all 
		select * from #tbl_cap2
		union all 
		select * from #tbl_cap3
		union all 
		select * from #tbl_cap4) as tblrt
	where isnull(fTroCapOmDau,0) != 0 or isnull(fTroCapThaiSan,0) != 0 or isnull(fTroCapTaiNanNN,0) != 0 or isnull(fTroCapHuuTri,0) != 0 or isnull(fTroCapPhucVien,0) != 0
			or isnull(fTroCapXuatNgu,0) != 0 or isnull(fTroCapThoiViec,0) != 0 or isnull(fTroCapTuTuat,0) != 0
	order by iKhoi desc,iID_MaDonVi,level, sLNS
	
	----insert dòng tổng cộng
	insert into #tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'Tổng cộng' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					7 as level,
					1 as bHangCha
			from #tblResult
			where #tblResult.level = 1
		) as tbltongcong


	---- Insert dòng dự toán
	insert into #tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'Khối dự toán' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					8 as level,
					1 as bHangCha
			from #tblResult
			where #tblResult.level = 4 and tblResult.sLNS = '9010001'
		) as tbldutoan



	---- Insert dòng Hạch toán
	insert into #tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'Khối hạch toán' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					9 as level,
					1 as bHangCha
			from #tblResult
			where #tblResult.level = 4 and  #tblResult.sLNS = '9010002'
		) as tbldutoan


	select  * from #tblResult order by iKhoi desc,iID_MaDonVi,level, sLNS


	drop table #tblDonVi;
	drop table  #tblMucLucNganSach;
	drop table #donvi_MLNS;
	drop table #tbl_qtcn_chitiet;
	drop table #tbl_cap4;
	drop table #tbl_cap3;
	drop table #tbl_cap2;
	drop table #tbl_cap1;
	drop table #tblResult;
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_muclucngansach_theodieuchinh]    Script Date: 1/26/2024 4:44:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_muclucngansach_theodieuchinh]
	 @namLamViec int,
	 @SLNS nvarchar(MAX)
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT *
	FROM BH_DM_MucLucNganSach
	WHERE sLNS in( SELECT * FROM splitstring(@SLNS))
	  AND iNamLamViec = @NamLamViec
	  AND bHangChaDuToanDieuChinh is not null
	 ORDER BY sXauNoiMa
END
;
;
GO