/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]    Script Date: 1/10/2024 6:18:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 1/10/2024 6:18:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]    Script Date: 1/10/2024 6:18:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_mlns_bhxh_by_year_of_work]    Script Date: 1/10/2024 6:18:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_mlns_bhxh_by_year_of_work]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_mlns_bhxh_by_year_of_work]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]    Script Date: 1/10/2024 6:18:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]    Script Date: 1/10/2024 6:18:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/10/2024 6:18:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getTienthuchien6thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/10/2024 6:18:29 PM ******/
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
					SUM(ctct.fTongTien_DeNghi) as fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_CheDoBHXH ct
				LEFT JOIN BH_QTC_Quy_CheDoBHXH_ChiTiet ctct ON ct.ID_QTC_Quy_CheDoBHXH=ctct.iID_QTC_Quy_CheDoBHXH
				WHERE ct.iNamChungTu=@NamLamViec
				AND ctct.iIDMaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
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
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KinhPhiQuanLy ct
				LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 
		--  ---- Data 6 thang < không biết của kinh phí nào nên comment truoc theo BA  >
		--ELSE IF(@SLNS='905,9050001,9050002')
		--  BEGIN
		--	INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		--   SELECT 
		--			ctct.iIDMaDonVi,
		--			ctct.iID_MucLucNganSach,
		--			'' sM,
		--			'' sTM,
		--			ctct.sNoiDung,
		--			ctct.iNamLamViec,
		--			SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
		--		FROM 
		--		BH_QTC_Quy_KinhPhiQuanLy ct
		--		LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
		--		WHERE ct.iNamChungTu=@NamLamViec
		--		AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
		--		AND ct.bIsKhoa=1
		--		AND	ct.iQuyChungTu<=2
		--		AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
		--		GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		--  END 

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
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KCB ct
				LEFT JOIN BH_QTC_Quy_KCB_ChiTiet ctct ON ct.ID_QTC_Quy_KCB=ctct.iID_QTC_Quy_KCB
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
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
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
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
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
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
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
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
			where dm.iNamLamViec=@NamLamViec and dm.sLNS in (select * from splitstring(@SLNS))
			order by dm.sXauNoiMa
			drop table #TempData6thang
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]    Script Date: 1/10/2024 6:18:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
--@SoQuyetDinh nvarchar(50),
@DNgayQuyetDinh nvarchar(50)
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
 -- AND chungtu.sSoQuyetDinh=@SoQuyetDinh
  AND CONVERT(nvarchar ,chungtu.dNgayQuyetDinh,103) <= @DNgayQuyetDinh 
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]    Script Date: 1/10/2024 6:18:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@SoQuyetDinh nvarchar(50),
@DNgayQuyetDinh nvarchar(50)
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
 -- AND chungtu.sSoQuyetDinh=@SoQuyetDinh -- ?? lấy lũy kế sao lại gán bằng Số quyết định dungnv719 sửa.
   AND( CONVERT(nvarchar ,chungtu.dNgayQuyetDinh,103) <= @DNgayQuyetDinh 
       )

  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_get_mlns_bhxh_by_year_of_work]    Script Date: 1/10/2024 6:18:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_get_mlns_bhxh_by_year_of_work]
	@NamLamViec int
AS
BEGIN
	with lns as
		(
			select * from BH_DM_MucLucNganSach where iNamLamViec = @NamLamViec
			and sL = '' and sK = '' and sM = '' and sTM = '' and sTTM = '' and sNG = '' and sTNG = ''
		),
		finalLns as (
		select iID_MLNS from lns where (select count(*) from lns t1 where t1.iID_MLNS_Cha = lns.iID_MLNS) = 0),
		tmp1 as
		(
		select distinct sXauNoiMa from BH_DTC_DieuChinhDuToanChi_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTC_DuToanChiTrenGiao_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTC_PhanBoDuToanChi_ChiTiet where iNamLamViec = @NamLamViec
		UNION 
		select distinct sXauNoiMa from BH_DTT_BHXH_ChungTu_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTT_BHXH_DieuChinh_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTT_BHXH_PhanBo_ChungTuChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTTM_BHYT_ThanNhan_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet where iNamLamViec = @NamLamViec
		),
		tmp2 as 
		(
			select sXauNoiMa from BH_CP_CapBoSung_KCB_BHYT_ChiTiet where iNamLamViec = @NamLamViec
			UNION
			select sXauNoiMa from BH_CP_CapTamUng_KCB_BHYT_ChiTiet where iNamLamViec = @NamLamViec
			UNION 
			select sXauNoiMa from BH_CP_ChungTu_ChiTiet ct
			INNER JOIN BH_CP_ChungTu m on m.iID_CP_ChungTu = ct.iID_CP_ChungTu
			where m.iNamChungTu = @NamLamViec
		),
		tmp3 as 
		(
		select distinct ct.sXauNoiMa from BH_QTC_CapKinhPhi_KCB_ChiTiet ct
			INNER JOIN BH_QTC_CapKinhPhi_KCB d on d.iID_ChungTu = ct.iID_ChungTu
			where d.iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Nam_CheDoBHXH_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet where iNamLamViec = @NamLamViec
		UNION 
		select distinct sXauNoiMa from BH_QTC_Nam_KinhPhiQuanLy_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Quy_CheDoBHXH_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Quy_KCB_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Quy_KinhPhiQuanLy_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Quy_KPK_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTT_BHXH_ChungTu_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTTM_BHYT_Chung_Tu_ChiTiet where iNamLamViec = @NamLamViec
		),
		root as (select * from BH_DM_MucLucNganSach where iNamLamViec = @NamLamViec)

		select distinct 
		r.*, 
		tmp1.sXauNoiMa as UsedDuToanChiTietToi, 
		tmp2.sXauNoiMa as UsedCPChiTietToi,
		tmp3.sXauNoiMa as UsedQuyetToanChiTietToi,
		finalLns.iID_MLNS as LNSID,
		parent.sXauNoiMa as MlnsParentName, parent.iID_MLNS as iID_MLNS_Cha
		from root r
		left join tmp1 on r.sXauNoiMa = tmp1.sXauNoiMa and iNamLamViec = @NamLamViec
		left join tmp2 on r.sXauNoiMa = tmp2.sXauNoiMa and iNamLamViec = @NamLamViec
		left join tmp3 on r.sXauNoiMa = tmp3.sXauNoiMa and iNamLamViec = @NamLamViec
		left join root parent on r.iID_MLNS_Cha = parent.iID_MLNS
		left join finalLns on finalLns.iID_MLNS = r.iID_MLNS 
		order by r.sxaunoima;
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]    Script Date: 1/10/2024 6:18:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@SNgayQuyetDinh nvarchar(MAX),
	@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 0 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi 

select * from #temp1;

DROP TABLE #tempall
DROP TABLE #temp1
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 1/10/2024 6:18:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@SNgayQuyetDinh nvarchar(MAX),
	@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 0 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi 

select 
N'A' STT,
N'Đơn vị dự toán' sTenDonVi,
'' iID_MaDonVi,
0 fTongTienDuToan,
1 IsHangCha,
0 RowNumber
into #tempDonViDuToan

select 
N'B' STT,
N'Đơn vị hạch toán' sTenDonVi,
'' iID_MaDonVi,
0 fTongTienDuToan,
1 IsHangCha,
0 RowNumber
into #tempDonViHachToan

------ create data don vi du toan
CREATE TABLE #tempDvKDT(STT VARCHAR(6), sTenDonVi nvarchar(50), iID_MaDonVi varchar(50), fTongTienDuToan float, IsHangCha int, RowNumber int)
INSERT INTO #tempDvKDT(STT, sTenDonVi, iID_MaDonVi, fTongTienDuToan, IsHangCha, RowNumber)
	SELECT B.* 
	from #temp1 B
LEFT JOIN DonVi A ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=2
 ORDER BY B.STT;
------ Create table Stt
	Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
		dv.iID_MaDonVi,
		dv.sTenDonVi
		into #tempSttKDT
		From #tempDvKDT dv
------ Update stt 
	Update #tempDvKDT set #tempDvKDT.STT=A.STT
		From #tempSttKDT A
		where #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi
			and #tempDvKDT.sTenDonVi=A.sTenDonVi
------ create data don vi hach toan
	SELECT B.* into #tempDvKHT
	From DonVi A
	LEFT JOIN #temp1 B ON A.iID_MaDonVi=B.iID_MaDonVi
	where A.iNamLamViec=@INamLamViec
	 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
	 and A.iKhoi=1

 ------ Create table Stt
	Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
		dv.iID_MaDonVi,
		dv.sTenDonVi
		into #tempSttKHT
		From #tempDvKHT dv
------ Update stt 
	Update #tempDvKHT set #tempDvKHT.STT=A.STT
		From #tempSttKHT A
		where #tempDvKHT.iID_MaDonVi=A.iID_MaDonVi
			and #tempDvKHT.sTenDonVi=A.sTenDonVi

 --- Create data merge don vi du toan
	SELECT  1 iLoai, * INTO #tempDataDVDT
	FROM
	(
		SELECT * FROM #tempDonViDuToan
		UNION ALL 
		SELECT * FROM #tempDvKDT
	)tempDataDVDT

	--- Tinh tong theo don vi du toan
	SELECT SUM(fTongTienDuToan) fTongTienDuToan
	INTO #SumTotalDuToan
	FROM #tempDvKDT
	--- update tong tien don vị du toan
	UPDATE #tempDataDVDT SET #tempDataDVDT.fTongTienDuToan=A.fTongTienDuToan
	FROM #SumTotalDuToan A
	WHERE #tempDataDVDT.sTenDonVi=N'Đơn vị dự toán' 
	AND #tempDataDVDT.STT=N'A'
	
	 --- Create data merge don vi hach toan
	SELECT  2 iLoai,* INTO #tempDataDVHT
	FROM
	(
		SELECT * FROM #tempDonViHachToan
		UNION ALL 
		SELECT * FROM #tempDvKHT
	)tempDataDVHT

	--- Tinh tong theo don vi hach toan
	SELECT SUM(fTongTienDuToan) fTongTienDuToan
	INTO #SumTotalHachToan
	FROM #tempDvKHT
	--- update tong tien don vị hach toan
	UPDATE #tempDataDVHT SET #tempDataDVHT.fTongTienDuToan=A.fTongTienDuToan
	FROM #SumTotalHachToan A
	WHERE #tempDataDVHT.sTenDonVi=N'Đơn vị hạch toán'
	AND #tempDataDVHT.STT=N'B'

	--- create merge don vi du toan voi don vi hach toan vào
	SELECT * 
	FROM
	(
		SELECT * FROM #tempDataDVDT
		UNION ALL 
		SELECT * FROM #tempDataDVHT
	)tempDataAll



DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempDvKDT
DROP TABLE #tempDvKHT
DROP TABLE #tempSttKDT
DROP TABLE #tempSttKHT
DROP TABLE #SumTotalDuToan
DROP TABLE #SumTotalHachToan
DROP TABLE #tempDataDVDT
DROP TABLE #tempDataDVHT
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]    Script Date: 1/10/2024 6:18:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @UserName nvarchar(100),
	 @iLoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;

	IF @iLoaiTongHop=1
	BEGIN
	SELECT  
		ROW_NUMBER() OVER (ORDER BY ct.iID_MaDonVi) as Stt
		,ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienDeNghiQuyetToanQuyNay,0)/ @Dvt FTongTienDeNghi,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KCB ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END
	ELSE
	BEGIN
	SELECT 
		ROW_NUMBER() OVER (ORDER BY ct.iID_MaDonVi) as Stt,
		ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienDeNghiQuyetToanQuyNay,0)/ @Dvt FTongTienDeNghi,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KCB ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END

END
;
;
;
GO
