/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 1/17/2024 10:18:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 1/17/2024 10:18:46 AM ******/
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
GO
