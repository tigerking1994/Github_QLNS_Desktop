/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 12/30/2024 11:00:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getTienthuchien6thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dctc_get_data_quyet_toan]    Script Date: 12/30/2024 11:00:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dctc_get_data_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dctc_get_data_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dctc_get_data_quyet_toan]    Script Date: 12/30/2024 11:00:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_bh_dctc_get_data_quyet_toan] 
	@NamLamViec INT,
	@MaDonVi NVARCHAR(max),
	@Quy INT,
	@SLNS NVARCHAR(max)
AS
BEGIN

			select 
			ctct.iIDMaDonVi as IIDMaDonVi,
			ctct.sXauNoiMa,
			sum	(isnull(ctct.fTienSQ_DeNghi,0) + isnull(ctct.fTienQNCN_DeNghi,0) + isnull(ctct.fTienCNVCQP_DeNghi,0) + isnull(ctct.fTienHSQBS_DeNghi,0) + isnull(ctct.fTienLDHD_DeNghi,0)) as FTienThucHien06ThangDauNam
			from BH_QTC_Quy_CheDoBHXH_ChiTiet ctct
			left join BH_QTC_Quy_CheDoBHXH ct on ctct.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
			where ct.iNamChungTu=@NamLamViec
			and ct.bIsKhoa=1
			and ct.iQuyChungTu<=@Quy
			and ct.iID_MaDonVi in (select * from f_split(@MaDonVi))
			group by ctct.sXauNoiMa,ctct.iIDMaDonVi

			union all

			select  
			ctct.iIDMaDonVi as IIDMaDonVi,
			ctct.sXauNoiMa,
			sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) as FTienThucHien06ThangDauNam
			from BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct
			left join BH_QTC_Quy_KinhPhiQuanLy ct on ctct.iID_QTC_Quy_KinhPhiQuanLy=ct.ID_QTC_Quy_KinhPhiQuanLy
			where ct.iNamChungTu=@NamLamViec
			and ct.bIsKhoa=1
			and ct.iQuyChungTu<=@Quy
			and ct.iID_MaDonVi in (select * from f_split(@MaDonVi))
			group by ctct.sXauNoiMa,ctct.iIDMaDonVi

			union all
			select 
			ctct.iID_MaDonVi as IIDMaDonVi,
			ctct.sXauNoiMa,
			sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) as FTienThucHien06ThangDauNam
			from BH_QTC_Quy_KCB_ChiTiet ctct
			left join BH_QTC_Quy_KCB ct on ctct.iID_QTC_Quy_KCB=ct.ID_QTC_Quy_KCB
			where ct.iNamChungTu=@NamLamViec
			and ct.bIsKhoa=1
			and ct.iQuyChungTu<=@Quy
			and ct.iID_MaDonVi in (select * from f_split(@MaDonVi))
			group by ctct.sXauNoiMa,ctct.iID_MaDonVi

			union all

			select 
			ctct.iIDMaDonVi as IIDMaDonVi,
			ctct.sXauNoiMa,
			sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) as FTienThucHien06ThangDauNam
			from BH_QTC_Quy_KPK_ChiTiet ctct
			left join BH_QTC_Quy_KPK ct on ctct.iID_QTC_Quy_KPK=ct.ID_QTC_Quy_KPK
			where ct.iNamChungTu=@NamLamViec
			and ct.bIsKhoa=1
			and ct.sDSLNS=@SLNS
			and ct.iQuyChungTu<=@Quy
			and ct.iID_MaDonVi in (select * from f_split(@MaDonVi))
			group by ctct.sXauNoiMa,ctct.iIDMaDonVi 


END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 12/30/2024 11:00:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@IID_LoaiChi nvarchar(100),
	@SMaLoaiChi nvarchar(50),
	@SLNS nvarchar(100),
	@DNgayChungTu datetime
AS
BEGIN
		Create table #TempData6thang
		(
			iID_MaDonVi nvarchar(50),
			iID_MucLucNganSach uniqueidentifier,
			sM nvarchar(50),
			sTM nvarchar(50),
			sNoiDung nvarchar(max),
			iNamLamViec int ,
			fTienThucHien06ThangDauNam float
		);

		---- Get data 6 thang tu qtc quy 
		---- Data 6 thang Chi các chế độ BHXH
		 IF(@SLNS ='9010001,9010002')
		 	INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,sM,sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sLoaiTroCap sNoiDung,
					ctct.iNamLamViec,
					sum	(isnull(ctct.fTienSQ_DeNghi,0) + isnull(ctct.fTienQNCN_DeNghi,0) + isnull(ctct.fTienCNVCQP_DeNghi,0) + isnull(ctct.fTienHSQBS_DeNghi,0) + isnull(ctct.fTienLDHD_DeNghi,0)) as fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_CheDoBHXH ct
				LEFT JOIN BH_QTC_Quy_CheDoBHXH_ChiTiet ctct ON ct.ID_QTC_Quy_CheDoBHXH=ctct.iID_QTC_Quy_CheDoBHXH
				WHERE ct.iNamChungTu=@NamLamViec
				AND ctct.iIDMaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				--AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sLoaiTroCap,iNamLamViec;
		 ---- Data 6 thang Chi kinh phí quản lý BHXH, BHYT
		ELSE IF(@SLNS='9010003')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KinhPhiQuanLy ct
				LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				--AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 
		---- Data Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ
		ELSE IF(@SLNS='905,9050001,9050002')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KinhPhiQuanLy ct
				LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				--AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Chi kinh phí KCB tại quân y đơn vị --comment 10/01/2024 		ELSE IF(@SLNS='9010004,9010005')
		ELSE IF(@SLNS='9010004')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iID_MaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KCB ct
				LEFT JOIN BH_QTC_Quy_KCB_ChiTiet ctct ON ct.ID_QTC_Quy_KCB=ctct.iID_QTC_Quy_KCB
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				--AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  ctct.iID_MaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		   ---- Data 6 thang Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân
		ELSE IF(@SLNS='9010008')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				--AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Chi hỗ trợ BHTN 
		ELSE IF(@SLNS='9010009')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				--AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Kinh phí mua sắm trang thiết bị y tế 
		   ELSE
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				--AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

			SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as IID_MucLucNganSach,
			   tbl.fTienThucHien06ThangDauNam,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #TempData6thang  tbl 
			on tbl.iID_MucLucNganSach=dm.iID_MLNS
			where dm.iNamLamViec=@NamLamViec and dm.sLNS in (select * from f_split(@SLNS))
			order by dm.sXauNoiMa
			drop table #TempData6thang
END
;
;
;
;
;
GO