/****** Object:  StoredProcedure [dbo].[sp_qt_tonghop_donvi_lns]    Script Date: 07/07/2023 6:44:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_tonghop_donvi_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_tonghop_donvi_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_tonghop]    Script Date: 07/07/2023 6:44:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 07/07/2023 6:44:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_lientham]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_lientham]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_donvi]    Script Date: 07/07/2023 6:44:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_chitiet_donvi]    Script Date: 07/07/2023 6:44:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 07/07/2023 6:44:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ke_hoach_thu_mua_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 07/07/2023 6:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	KHTM.iID_KHTM_BHYT,
	KHTM.sSoChungTu,
	KHTM.iNamChungTu,
	KHTM.dNgayChungTu,
	KHTM.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHTM.sMoTa,
	KHTM.bKhoa,
	KHTM.fTongKeHoach,
	KHTM.sSoQuyetDinh,
	KHTM.dNgayQuyetDinh,
	KHTM.sNguoiTao,
	KHTM.sNguoiSua,
	KHTM.dNgayTao,
	KHTM.dNgaySua,
	KHTM.iLoaiTongHop,
	KHTM.sTongHop,
	KHTM.fThuBHYTNLDDong,
	KHTM.fThuBHYTNSDDong,
	KHTM.fTongBHYT,
	KHTM.fTong,
	KHTM.bDaTongHop
	FROM BH_KHTM_BHYT KHTM
	LEFT JOIN DonVi DV
	ON KHTM.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	ORDER BY KHTM.dNgayQuyetDinh DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_chitiet_donvi]    Script Date: 07/07/2023 6:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_chitiet_donvi]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100)
AS
BEGIN
	SELECT mlns.sKyHieu ,
		   dv.iID_MaDonVi ,
		   mlns.sMoTa ,
		   dv.iID_MaDonVi + ' - ' + dv.sTenDonVi as TenDonVi,
		   bHangCha ,
		   isnull(fSoThieuUy , 0) as fSoThieuUy , 
			isnull(fSoTrungUy , 0) as fSoTrungUy , 
			isnull(fSoThuongUy , 0) as fSoThuongUy , 
			isnull(fSoDaiUy , 0) as fSoDaiUy , 
			isnull(fSoThieuTa , 0) as fSoThieuTa , 
			isnull(fSoTrungTa , 0) as fSoTrungTa , 
			isnull(fSoThuongTa , 0) as fSoThuongTa , 
			isnull(fSoDaiTa , 0) as fSoDaiTa , 
			isnull(fSoTuong , 0) as fSoTuong , 
			isnull(fSoTSQ , 0) as fSoTSQ , 
			isnull(fSoBinhNhi , 0) as fSoBinhNhi , 
			isnull(fSoBinhNhat , 0) as fSoBinhNhat , 
			isnull(fSoHaSi , 0) as fSoHaSi , 
			isnull(fSoTrungSi , 0) as fSoTrungSi , 
			isnull(fSoThuongSi , 0) as fSoThuongSi , 
			isnull(fSoQNCN , 0) as fSoQNCN , 
			isnull(fSoVCQP , 0) as fSoVCQP , 
			isnull(fSoCNVQP , 0) as fSoCNVQP , 
			isnull(fSoLDHD, 0) as fSoLDHD ,
			isnull(fSoCcqp, 0) as fSoCcqp 

	FROM
	  (SELECT sKyHieu ,
			  iID_MaDonVi ,
			  fSoThieuUy = sum(fSoThieuUy) ,
			  fSoTrungUy = sum(fSoTrungUy) ,
			  fSoThuongUy = sum(fSoThuongUy) ,
			  fSoDaiUy = sum(fSoDaiUy) ,
			  fSoThieuTa = sum(fSoThieuTa) ,
			  fSoTrungTa = sum(fSoTrungTa) ,
			  fSoThuongTa = sum(fSoThuongTa) ,
			  fSoDaiTa = sum(fSoDaiTa) ,
			  fSoTuong = sum(fSoTuong) ,
			  fSoTSQ = sum(fSoTSQ) ,
			  fSoBinhNhi = sum(fSoBinhNhi) ,
			  fSoBinhNhat = sum(fSoBinhNhat) ,
			  fSoHaSi = sum(fSoHaSi) ,
			  fSoTrungSi = sum(fSoTrungSi) ,
			  fSoThuongSi = sum(fSoThuongSi) ,
			  fSoQNCN = sum(fSoQNCN) ,
			  fSoVCQP = sum(fSoVCQP) ,
			  fSoCNVQP = sum(fSoCNVQP) ,
			  fSoLDHD = sum(fSoLDHD),
			  fSoCcqp = sum(fSoCcqp)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND iThangQuy in
		   (SELECT *
			FROM f_split(@Period))
	   GROUP BY sKyHieu, iID_MaDonVi)ctct -- lay mucluc quan so

	RIGHT JOIN
	  (SELECT sKyHieu,
			  sMoTa,
			  bHangCha
	   FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2') AS mlns ON ctct.sKyHieu = mlns.sKyHieu
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	UNION 
	SELECT sKyHieu ,
		   null as iID_MaDonVi ,
		   sMoTa ,
		   null as TenDonVi,
		   bHangCha ,
		   0 as fSoThieuUy ,
		   0 as fSoTrungUy ,
		   0 as fSoThuongUy ,
		   0 as fSoDaiUy ,
		   0 as fSoThieuTa ,
		   0 as fSoTrungTa ,
		   0 as fSoThuongTa ,
		   0 as fSoDaiTa ,
		   0 as fSoTuong ,
		   0 as fSoTSQ ,
		   0 as fSoBinhNhi ,
		   0 as fSoBinhNhat ,
		   0 as fSoHaSi ,
		   0 as fSoTrungSi ,
		   0 as fSoThuongSi ,
		   0 as fSoQNCN ,
		   0 as fSoVCQP ,
		   0 as fSoCNVQP ,
		   0 as fSoLDHD ,
		   0 as fSoCcqp
	FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2'
	ORDER BY sKyHieu, iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_donvi]    Script Date: 07/07/2023 6:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_donvi]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100)
AS
BEGIN
	SELECT mlns.sKyHieu ,
		   sMoTa ,
		   bHangCha ,
		   fSoThieuUy ,
		   fSoTrungUy ,
		   fSoThuongUy ,
		   fSoDaiUy ,
		   fSoThieuTa ,
		   fSoTrungTa ,
		   fSoThuongTa ,
		   fSoDaiTa ,
		   fSoTuong ,
		   fSoTSQ ,
		   fSoBinhNhi ,
		   fSoBinhNhat ,
		   fSoHaSi ,
		   fSoTrungSi ,
		   fSoThuongSi ,
		   fSoQNCN ,
		   fSoVCQP ,
		   fSoCNVQP ,
		   fSoLDHD,
		   fSoCcqp
	FROM
	  (SELECT sKyHieu ,
			  fSoThieuUy = sum(fSoThieuUy) ,
			  fSoTrungUy = sum(fSoTrungUy) ,
			  fSoThuongUy = sum(fSoThuongUy) ,
			  fSoDaiUy = sum(fSoDaiUy) ,
			  fSoThieuTa = sum(fSoThieuTa) ,
			  fSoTrungTa = sum(fSoTrungTa) ,
			  fSoThuongTa = sum(fSoThuongTa) ,
			  fSoDaiTa = sum(fSoDaiTa) ,
			  fSoTuong = sum(fSoTuong) ,
			  fSoTSQ = sum(fSoTSQ) ,
			  fSoBinhNhi = sum(fSoBinhNhi) ,
			  fSoBinhNhat = sum(fSoBinhNhat) ,
			  fSoHaSi = sum(fSoHaSi) ,
			  fSoTrungSi = sum(fSoTrungSi) ,
			  fSoThuongSi = sum(fSoThuongSi) ,
			  fSoQNCN = sum(fSoQNCN) ,
			  fSoVCQP = sum(fSoVCQP) ,
			  fSoCNVQP = sum(fSoCNVQP) ,
			  fSoLDHD = sum(fSoLDHD),
			  fSoCcqp = sum(fSoCcqp)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND iThangQuy in
		   (SELECT *
			FROM f_split(@Period))
	   GROUP BY sKyHieu)ctct -- lay mucluc quan so

	RIGHT JOIN
	  (SELECT sKyHieu,
			  sMoTa,
			  bHangCha
	   FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2') AS mlns ON ctct.sKyHieu = mlns.sKyHieu
	ORDER BY sKyHieu
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 07/07/2023 6:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_lientham]
	@Month int,
	@YearOfWork int,
	@YearOfWorkBefore int,
	@AgencyId nvarchar(max)
AS
BEGIN
	
	SELECT 
		ctct.*,
		dv.sTenDonVi,
		dv.MoTa
	FROM(
		SELECT iID_MaDonVi
		--thieu uy
		,ThieuUy_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuUy ELSE 0 END)
		,ThieuUy_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuUy ELSE 0 END)
		,ThieuUy_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuUy ELSE 0 END)
		,ThieuUy_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuUy ELSE 0 END)

		--trung uy
		,TrungUy_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungUy ELSE 0 END)
		,TrungUy_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungUy ELSE 0 END)
		,TrungUy_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungUy ELSE 0 END)
		,TrungUy_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungUy ELSE 0 END)

		----thuong uy
		,ThuongUy_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongUy ELSE 0 END)
		,ThuongUy_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongUy ELSE 0 END)
		,ThuongUy_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongUy ELSE 0 END)
		,ThuongUy_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongUy ELSE 0 END)

		----Dai uy
		,DaiUy_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiUy ELSE 0 END)
		,DaiUy_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiUy ELSE 0 END)
		,DaiUy_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiUy ELSE 0 END)
		,DaiUy_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiUy ELSE 0 END)

		----thieu ta
		,ThieuTa_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuTa ELSE 0 END)
		,ThieuTa_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuTa ELSE 0 END)
		,ThieuTa_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuTa ELSE 0 END)
		,ThieuTa_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuTa ELSE 0 END)

		----trungta
		,TrungTa_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungTa ELSE 0 END)
		,TrungTa_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungTa ELSE 0 END)
		,TrungTa_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungTa ELSE 0 END)
		,TrungTa_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungTa ELSE 0 END)

		----thuong ta
		,ThuongTa_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongTa ELSE 0 END)
		,ThuongTa_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongTa ELSE 0 END)
		,ThuongTa_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongTa ELSE 0 END)
		,ThuongTa_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongTa ELSE 0 END)

		----Dai ta
		,DaiTa_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiTa ELSE 0 END)
		,DaiTa_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiTa ELSE 0 END)
		,DaiTa_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiTa ELSE 0 END)
		,DaiTa_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiTa ELSE 0 END)

		----thieu tuong
		,Tuong_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTuong ELSE 0 END)
		,Tuong_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTuong ELSE 0 END)
		,Tuong_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTuong ELSE 0 END)
		,Tuong_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTuong ELSE 0 END)

		----TSQ
		,TSQ_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTSQ ELSE 0 END)
		,TSQ_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTSQ ELSE 0 END)
		,TSQ_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTSQ ELSE 0 END)
		,TSQ_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTSQ ELSE 0 END)

		----binh nhi
		,BinhNhi_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoBinhNhi ELSE 0 END)
		,BinhNhi_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoBinhNhi ELSE 0 END)
		,BinhNhi_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoBinhNhi ELSE 0 END)
		,BinhNhi_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoBinhNhi ELSE 0 END)

		----binh nhat
		,BinhNhat_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoBinhNhat ELSE 0 END)
		,BinhNhat_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoBinhNhat ELSE 0 END)
		,BinhNhat_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoBinhNhat ELSE 0 END)
		,BinhNhat_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoBinhNhat ELSE 0 END)

		----ha si
		,HaSi_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoHaSi ELSE 0 END)
		,HaSi_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoHaSi ELSE 0 END)
		,HaSi_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoHaSi ELSE 0 END)
		,HaSi_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoHaSi ELSE 0 END)

		----trung si
		,TrungSi_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungSi ELSE 0 END)
		,TrungSi_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungSi ELSE 0 END)
		,TrungSi_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungSi ELSE 0 END)
		,TrungSi_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungSi ELSE 0 END)

		----thuong si
		,ThuongSi_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongSi ELSE 0 END)
		,ThuongSi_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongSi ELSE 0 END)
		,ThuongSi_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongSi ELSE 0 END)
		,ThuongSi_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongSi ELSE 0 END)

		----QNCN
		,QNCN_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoQNCN ELSE 0 END)
		,QNCN_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoQNCN ELSE 0 END)
		,QNCN_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoQNCN ELSE 0 END)
		,QNCN_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoQNCN ELSE 0 END)

		----CNVQP
		,CNVQP_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoCNVQP ELSE 0 END)
		,CNVQP_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoCNVQP ELSE 0 END)
		,CNVQP_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoCNVQP ELSE 0 END)
		,CNVQP_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoCNVQP ELSE 0 END)

		----LDHD
		,LDHD_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoLDHD ELSE 0 END)
		,LDHD_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoLDHD ELSE 0 END)
		,LDHD_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoLDHD ELSE 0 END)
		,LDHD_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoLDHD ELSE 0 END)

		----CCQP
		,CCQP_NamTruoc = SUM(
		CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
		THEN fSoCcqp ELSE 0 END)
		,CCQP_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoCcqp ELSE 0 END)
		,CCQP_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoCcqp ELSE 0 END)
		,CCQP_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoCcqp ELSE 0 END)


		FROM NS_QS_ChungTuChiTiet
		WHERE iNamLamViec = @YearOfWork
					and (@AgencyId is null or iID_MaDonVi in (select * from f_split(@AgencyId)))
		GROUP BY iID_MaDonVi) as ctct
		RIGHT JOIN 
			(
				SELECT 
					iID_MaDonVi, 
					sTenDonVi,	
					MoTa = iID_MaDonVi + ' - ' + sTenDonVi from DonVi 
				WHERE 
					iTrangThai=1 
					and iNamLamViec = @yearOfWork 
					and iLoai=1 
					and (@AgencyId is null or iID_MaDonVi in (select * from f_split(@AgencyId)))
			) as dv
			on dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY dv.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_tonghop]    Script Date: 07/07/2023 6:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_tonghop]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100)
AS
BEGIN
	SELECT dv.iID_MaDonVi ,
		   sTenDonVi ,
		   MoTa ,
		   fSoThieuUy ,
		   fSoTrungUy ,
		   fSoThuongUy ,
		   fSoDaiUy ,
		   fSoThieuTa ,
		   fSoTrungTa ,
		   fSoThuongTa ,
		   fSoDaiTa ,
		   fSoTuong ,
		   fSoTSQ ,
		   fSoBinhNhi ,
		   fSoBinhNhat ,
		   fSoHaSi ,
		   fSoTrungSi ,
		   fSoThuongSi ,
		   fSoQNCN ,
		   fSoVCQP ,
		   fSoCNVQP ,
		   fSoLDHD ,
		   fSoCCQP
	FROM
	  (SELECT iID_MaDonVi ,
			  fSoThieuUy = SUM(fSoThieuUy) ,
			  fSoTrungUy = SUM(fSoTrungUy) ,
			  fSoThuongUy = SUM(fSoThuongUy) ,
			  fSoDaiUy = sum(fSoDaiUy) ,
			  fSoThieuTa = SUM(fSoThieuTa) ,
			  fSoTrungTa = SUM(fSoTrungTa) ,
			  fSoThuongTa = SUM(fSoThuongTa) ,
			  fSoDaiTa = SUM(fSoDaiTa) ,
			  fSoTuong = SUM(fSoTuong) ,
			  fSoTSQ = SUM(fSoTSQ) ,
			  fSoBinhNhi = SUM(fSoBinhNhi) ,
			  fSoBinhNhat = SUM(fSoBinhNhat) ,
			  fSoHaSi = SUM(fSoHaSi) ,
			  fSoTrungSi = SUM(fSoTrungSi) ,
			  fSoThuongSi = SUM(fSoThuongSi) ,
			  fSoQNCN = SUM(fSoQNCN) ,
			  fSoVCQP = SUM(fSoVCQP) ,
			  fSoCNVQP = SUM(fSoCNVQP) ,
			  fSoLDHD = SUM(fSoLDHD) ,
			  fSoCCQP = SUM(fSoCcqp)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND (@Period IS NULL
			  OR iThangQuy in
				(SELECT *
				 FROM f_split(@Period)))
		 AND sKyHieu = '700'
	   GROUP BY iID_MaDonVi)ctct -- lay ten don vi
	RIGHT JOIN
	  (SELECT iID_MaDonVi,
			  sTenDonVi,
			  MoTa = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND iLoai = 1
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId))) ) AS dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_tonghop_donvi_lns]    Script Date: 07/07/2023 6:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_tonghop_donvi_lns]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@QuarterMonth nvarchar(100),
	@VoucherDate date,
	@HasDuToan bit,
	@UserName nvarchar(100),
	@AgencyIds nvarchar(max)

AS
BEGIN
	DECLARE @tblLNS table (sLNS nvarchar(100))
	INSERT INTO @tblLNS (sLNS)
		SELECT sLNS
		FROM
		  (SELECT DISTINCT sLNS
		   FROM NS_QT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND fTuChi_PheDuyet <> 0
			 AND iID_QTChungTu in
				(
					select iID_QTChungTu from NS_QT_ChungTu
					where iThangQuy IN (SELECT * FROM f_split(@QuarterMonth))
				)
			AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyIds))

		   UNION 
		   SELECT DISTINCT sLNS
		   FROM NS_DT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND iDuLieuNhan = 0
			 AND fTuChi <> 0
			 AND iID_DTChungTu IN (
				SELECT iID_DTChungTu FROM NS_DT_ChungTu
				WHERE cast(dNgayQuyetDinh as date) <= cast(@VoucherDate as date)
					AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				)
			AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyIds))

			 ) lns

	SELECT sLNS as LNS, 
		sMoTa as MoTa, 
		iID_MLNS as MlnsId, 
		iID_MLNS_Cha as MlnsIdParent
	FROM NS_MucLucNganSach 
	INNER JOIN 
		(
			SELECT DISTINCT VALUE
			FROM 
			(SELECT 
				CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
				CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
				CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
				CAST(sLNS AS nvarchar(10)) LNS 
			FROM NS_NguoiDung_LNS 
			WHERE 
				sMaNguoiDung = @UserName
				AND iNamLamViec = @YearOfWork
			) LNS
			UNPIVOT
			(
				value
				FOR col in (LNS1, LNS3, LNS5, LNS)
			) un) lns
		ON sLNS = lns.value
	WHERE iNamLamViec = @YearOfWork
		AND sLNS in 
			(
				SELECT 
					DISTINCT VALUE
				FROM 
				(
					SELECT 
						CAST(LEFT(sLNS, 1) AS nvarchar(10)) sLNS1, 
						CAST(LEFT(sLNS, 3) AS nvarchar(10)) sLNS3, 
						CAST(LEFT(sLNS, 5) AS nvarchar(10)) sLNS5, 
						CAST(sLNS AS nvarchar(10)) sLNS 
					FROM
						@tblLNS
				) sLNS
				UNPIVOT
				(
					value
					FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
				) un
			)
			and sL = ''
	order by sXauNoiMa
END
;
;
;
GO
