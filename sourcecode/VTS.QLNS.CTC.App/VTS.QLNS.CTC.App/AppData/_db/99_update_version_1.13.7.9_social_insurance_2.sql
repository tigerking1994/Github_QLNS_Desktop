/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/8/2024 3:17:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getTienthuchien6thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_lns_BH]    Script Date: 1/8/2024 3:17:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_lns_BH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_lns_BH]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]    Script Date: 1/8/2024 3:17:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_lns_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_tonghop_bh]    Script Date: 1/8/2024 3:17:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_donvi_tonghop_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_donvi_tonghop_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_tonghop_bh]    Script Date: 1/8/2024 3:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay don vi cua chung tu tổng hop cap phat chi tiet theo id
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_get_donvi_tonghop_bh]
	@YearOfWork int,
	@Quy int,
	@IDLoaiChi nvarchar(500)
AS
BEGIN
	SELECT 
	distinct
	dv.* 
	FROM BH_CP_ChungTu cp
	LEFT JOIN  DonVi  dv on dv.iID_MaDonVi IN (SELECT * FROM splitstring(cp.sID_MaDonVi))
	where cp.iNamChungTu=@YearOfWork
	and cp.iID_LoaiCap=@IDLoaiChi
	and cp.iQuy=@Quy
	and dv.iNamLamViec=@YearOfWork
	order by dv.iID_MaDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]    Script Date: 1/8/2024 3:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]
	 @NamLamViec int,
	 @IDLoaiChi nvarchar(500),
	 @IdDonvi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
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
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

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
		ISNULL(ctct.fTienKeHoachCap, 0) / @Dvt as FTienKeHoach 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(select ctct.* from BH_CP_ChungTu ct 
		left join BH_CP_ChungTu_ChiTiet ctct on ct.iID_CP_ChungTu=ctct.iID_CP_ChungTu
		where ct.iNamChungTu=@NamLamViec
		and ct.iID_LoaiCap=@IDLoaiChi
		and iID_MaDonVi in (select * from f_split(@IdDonVi))) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_lns_BH]    Script Date: 1/8/2024 3:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_lns_BH]
@NamLamViec int,
@Quy int,
@LoaiChi uniqueidentifier
AS BEGIN
SET NOCOUNT ON;

SELECT 
distinct
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

FROM BH_CP_ChungTu chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi IN (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iQuy=@Quy
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.iID_LoaiCap=@LoaiChi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
  order by donvi.iID_MaDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/8/2024 3:17:51 PM ******/
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

GO
