/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/9/2024 10:15:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getTienthuchien6thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 1/9/2024 10:15:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 1/9/2024 10:15:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 1/9/2024 10:15:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 1/9/2024 10:15:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet
	( 
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		iNamLamViec,
		sLoaiTroCap,
		dNgayTao,
		sNguoiTao,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay	
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		@NamLamViec,
		qtcn_ct.sLoaiTroCap,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.iTongSo_DeNghi),
		SUM(qtcn_ct.fTongTien_DeNghi)
	FROM BH_QTC_Quy_CheDoBHXH_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sLoaiTroCap
END
;
;
;
;


select * from BH_QTC_Quy_CheDoBHXH_ChiTiet
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 1/9/2024 10:15:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_BH_DTC_ChiTiet ,
		ct.iID_BH_DTC ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sM,
		ct.sTM,
		ct.sNoiDung,
		ct.sXauNoiMa,
		ct.iNamLamViec,
		ct.fTienDuToanDuocGiao,
		ct.fTienThucHien06ThangDauNam,
		ct.fTienUocThucHien06ThangCuoiNam,
		(isnull(ct.fTienThucHien06ThangDauNam,0) + isnull(ct.fTienUocThucHien06ThangCuoiNam,0)) fTienUocThucHienCaNam,

		(CASE  WHEN isnull(ct.fTienUocThucHienCaNam,0) > isnull(ct.fTienDuToanDuocGiao,0) THEN isnull(ct.fTienUocThucHienCaNam,0) - isnull(ct.fTienDuToanDuocGiao,0) ELSE 0 END ) as fTienSoSanhTang,
		(CASE  WHEN isnull(ct.fTienDuToanDuocGiao,0) > isnull(ct.fTienUocThucHienCaNam,0) THEN isnull(ct.fTienDuToanDuocGiao,0) - isnull(ct.fTienUocThucHienCaNam,0) ELSE 0 END ) as fTienSoSanhGiam,

		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu
	FROM
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
				bh.iID_MaDonVi in (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 1/9/2024 10:15:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop] @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50), 
  @IdChungTu nvarchar(100), 
  @NamLamViec int ,
  @MaDonVi nvarchar(50)
  AS 
  BEGIN
  INSERT INTO [dbo].BH_DTC_DieuChinhDuToanChi_ChiTiet (
    iID_BH_DTC_ChiTiet,
	iID_BH_DTC, 
    iID_MucLucNganSach,
	sM,
	sTM,
	sNoiDung,
	sXauNoiMa,
	iNamLamViec,
	iID_MaDonVi,
    fTienDuToanDuocGiao,
	fTienThucHien06ThangDauNam,
    fTienUocThucHien06ThangCuoiNam,
    fTienUocThucHienCaNam,
	fTienSoSanhTang, 
    fTienSoSanhGiam,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao
  ) 
SELECT 
  DISTINCT NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sM, 
  sTM, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec,
  @MaDonVi,
  sum(fTienDuToanDuocGiao), 
  sum(fTienThucHien06ThangDauNam), 
  sum(fTienUocThucHien06ThangCuoiNam), 
  sum(fTienUocThucHienCaNam), 
  sum(fTienSoSanhTang), 
  sum(fTienSoSanhGiam), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_DTC_DieuChinhDuToanChi_ChiTiet 
WHERE 
  iID_BH_DTC in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  and iNamLamViec=@NamLamViec
GROUP BY 
  sM, 
  sTM, 
  iID_MucLucNganSach, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec;
--danh dau chung tu da tong hop
update 
  BH_DTC_DieuChinhDuToanChi 
set 
  bDaTongHop = 1 
where 
  iID_BH_DTC in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) END;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/9/2024 10:15:03 AM ******/
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
;
GO
