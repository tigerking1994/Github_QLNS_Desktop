/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_getTienDuToanDuocGiao_chi_tiet]    Script Date: 1/8/2024 8:27:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_quanly_getTienDuToanDuocGiao_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_getTienDuToanDuocGiao_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 1/8/2024 8:27:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]    Script Date: 1/8/2024 8:27:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quyKCB_getTienDuToanDuocGiao_chi_tiet]    Script Date: 1/8/2024 8:27:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quyKCB_getTienDuToanDuocGiao_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quyKCB_getTienDuToanDuocGiao_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/8/2024 8:27:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getTienthuchien6thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]    Script Date: 1/8/2024 8:27:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]    Script Date: 1/8/2024 8:27:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KCB_ChiTiet
	( 
		ID_QTC_Quy_KCB_ChiTiet,
		iID_QTC_Quy_KCB,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		--fTien_DuToanGiaoNamNay,
		fTienQuyetToanDaDuyet
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		--Sum(qtcn_ct.fTien_DuToanGiaoNamNay),
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay)
	FROM BH_QTC_Quy_KCB_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/8/2024 8:27:42 AM ******/
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
				WHERE ct.iNamChungTu=2023
				AND ctct.iIDMaDonVi IN (SELECT * FROM splitstring('001'))
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
		  ---- Data 6 thang Chi kinh phí KCB tại Trường Sa 
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

		  ---- Data 6 thang Chi kinh phí KCB tại quân y đơn vị 
		ELSE IF(@SLNS='9010004,9010005')
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

		  SELECT * FROM #TempData6thang
		  drop table #TempData6thang
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quyKCB_getTienDuToanDuocGiao_chi_tiet]    Script Date: 1/8/2024 8:27:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 1/06/2024
-- Description:	Lấy danh sách hiển thị chứng từ 

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_quyKCB_getTienDuToanDuocGiao_chi_tiet]
	@YearOfWork int,
	@LNS nvarchar(max),
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(500),
	@Quy int
AS
BEGIN
		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS FTienDuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #Temp
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
	   GROUP BY iID_MaDonVi, 
	   iID_MucLucNganSach

	   SELECT * FROM #Temp
	--   SELECT 
	--	qtcn_ct.iID_MucLucNganSach,
	--	qtcn_ct.sNoiDung,
	--	SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay) fTienQuyetToanDaDuyet
	--	into #Temp1
	--FROM BH_QTC_Quy_KCB_ChiTiet qtcn_ct
	--	inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
	--WHERE qtcn.iQuyChungTu < @Quy
	--		AND qtcn.iID_MaDonVi in (SELECT * FROM f_split(@AgencyId))
	--		AND qtcn.iNamChungTu = @YearOfWork
	--		--AND qtcn.bIsKhoa=1
	--	GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung


	--SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
	--		into #tblNsMlns
	--FROM BH_DM_MucLucNganSach 
	--WHERE iNamLamViec = @YearOfWork 
	--	and	sLNS in (SELECT * FROM f_split(@LNS))

	--SELECT TBL.iID_MLNS AS iID_MucLucNganSach,
	--		T.FTienDuToanDuocGiao,
	--		T1.fTienQuyetToanDaDuyet
	--FROM #tblNsMlns TBL
	--LEFT JOIN #Temp T ON TBL.iID_MLNS=T.iID_MucLucNganSach
	--LEFT JOIN #Temp1 T1 ON TBL.iID_MLNS=T1.iID_MucLucNganSach

		DROP TABLE  #Temp
	--DROP TABLE  #Temp1
	--DROP TABLE  #tblNsMlns
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]    Script Date: 1/8/2024 8:27:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 1/06/2024
-- Description:	Lấy danh sách hiển thị chứng từ 

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]
	@YearOfWork int,
	@LNS nvarchar(max),
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(500),
	@Quy int
AS
BEGIN
		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS FTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #Temp
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
	   GROUP BY iID_MaDonVi, 
	   iID_MucLucNganSach

	   SELECT * FROM #Temp
	--   SELECT 
	--	qtcn_ct.iID_MucLucNganSach,
	--	qtcn_ct.sNoiDung,
	--	SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay) fTienQuyetToanDaDuyet
	--	into #Temp1
	--FROM BH_QTC_Quy_KCB_ChiTiet qtcn_ct
	--	inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
	--WHERE qtcn.iQuyChungTu < @Quy
	--		AND qtcn.iID_MaDonVi in (SELECT * FROM f_split(@AgencyId))
	--		AND qtcn.iNamChungTu = @YearOfWork
	--		--AND qtcn.bIsKhoa=1
	--	GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung


	--SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
	--		into #tblNsMlns
	--FROM BH_DM_MucLucNganSach 
	--WHERE iNamLamViec = @YearOfWork 
	--	and	sLNS in (SELECT * FROM f_split(@LNS))

	--SELECT TBL.iID_MLNS AS iID_MucLucNganSach,
	--		T.FTienDuToanDuocGiao,
	--		T1.fTienQuyetToanDaDuyet
	--FROM #tblNsMlns TBL
	--LEFT JOIN #Temp T ON TBL.iID_MLNS=T.iID_MucLucNganSach
	--LEFT JOIN #Temp1 T1 ON TBL.iID_MLNS=T1.iID_MucLucNganSach

		DROP TABLE  #Temp
	--DROP TABLE  #Temp1
	--DROP TABLE  #tblNsMlns
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 1/8/2024 8:27:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 20/09/2023
-- Description:	Lấy danh sách hiển thị chứng từ quyết toán chi quy kinh phí khác chi tiết

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]
	@VoucherId uniqueidentifier,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi uniqueidentifier,
	@iQuyChungTu int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
									WHERE iID_QTC_Quy_KinhPhiQuanLy =@VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToanDuocGiao,
          0 AS fTienDeNghiQuyetToanQuyNay,
          0 AS fTienQuyetToanDaDuyet,
		  0 AS fTienThucChi,
		  0 AS fTienXacNhanQuyetToanQuyNay,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

	SELECT DISTINCT iID_MucLucNganSach
	--, iID_MLNS_Cha 
	into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MucLucNganSach 


	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (SELECT * FROM #tempLNS)

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		WHERE sTM !='0'
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	-- map bảng mlns ns và đơn vị
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns

	IF @CountIndex=0
	BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		0 AS fTienDuToanDuocGiao,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
	    CTCT.iID_MucLucNganSach
		into #tblDataDaCap
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KinhPhiQuanLy=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

	--SELECT * into #tempAgency from  f_split(N'001'); 


	--SELECT 0 AS fTienDuToanDuocGiao,
	--	  SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	  SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	  SUM(fTienThucChi) AS fTienThucChi,
	--	  SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
 --         --iID_MaDonVi,
 --         --iID_MucLucNganSach
	--	  --into #tblDataDaCap
	--FROM BH_QTC_Quy_KinhPhiQuanLy_chiTiet
	--WHERE iID_QTC_Quy_KinhPhiQuanLy IN
 --      (
	--	SELECT iID_QTC_Quy_KinhPhiQuanLy
 --       FROM BH_QTC_Quy_KinhPhiQuanLy
 --       WHERE iNamChungTu = 2023
	--	  --AND i = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST('09-20-2023' AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(N'001'))
	--	  AND iID_QTC_Quy_KinhPhiQuanLy='4033EB9C-ECDE-4D24-A41C-FB5B303B5883'
	--	  AND iID_MaDonVi IN (SELECT * FROM  f_split(N'001'))
	--	)
		
	--GROUP BY iID_MucLucNganSach
	SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

	SELECT sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, sum(fTienQuyetToanDaDuyet) fTienQuyetToanDaDuyet, SUM(fTienThucChi) AS fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTienDuToanDuocGiao,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienQuyetToanDaDuyet,
		   fTienThucChi,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet,SUM(fTienThucChi) as fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) as fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTienDuToanDuocGiao,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienThucChi,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToan T 
		WHERE T.fTienDuToanDuocGiao <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienDuToanDuocGiao, 0) as FTienDuToanDuocGiao,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienThucChi, 0) as FTienThucChi,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
	ELSE 
		BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
		into #tblDataDaCapExist
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KinhPhiQuanLy=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu=@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	SELECT sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,SUM(fTienThucChi) AS fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
		SELECT * FROM #tblDataDaCapExist
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTienDuToanDuocGiao,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienQuyetToanDaDuyet,
		   fTienThucChi,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToanExist
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToanExist daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet,SUM(fTienThucChi) AS fTienThucChi, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResultExist FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTienDuToanDuocGiao,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienThucChi,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToanExist T 
		WHERE  T.fTienDuToanDuocGiao <> 0 OR T.fTienDeNghiQuyetToanQuyNay<>0 OR   T.fTienQuyetToanDaDuyet<>0 OR T.fTienThucChi <>0 OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienDuToanDuocGiao, 0) as fTienDuToanDuocGiao,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_getTienDuToanDuocGiao_chi_tiet]    Script Date: 1/8/2024 8:27:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 1/06/2024
-- Description:	Lấy danh sách hiển thị chứng từ 

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_getTienDuToanDuocGiao_chi_tiet]
	@YearOfWork int,
	@LNS nvarchar(max),
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(500)
AS
BEGIN
		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS FTienDuToanDuocGiao,
          iID_MaDonVi,
          iID_MucLucNganSach
		
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
	   GROUP BY iID_MaDonVi, 
	   iID_MucLucNganSach



END
;
;
GO
