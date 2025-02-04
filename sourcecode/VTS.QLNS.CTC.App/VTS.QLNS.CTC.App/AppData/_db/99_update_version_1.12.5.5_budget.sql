/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_thuongxuyen]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_thuongxuyen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_thuongxuyen]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_raquan_thangtruoc]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_raquan_thangtruoc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_raquan_thangtruoc]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_raquan]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_raquan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_raquan]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_lientham]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_lientham]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_donvi]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_chitiet_donvi]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_thang]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_binhquan_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_binhquan_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_donvi]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_binhquan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_binhquan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 06/02/2023 11:08:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_in_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_in_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 06/02/2023 11:08:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_nh_dutoan_in_khlcnt]
@iId uniqueidentifier,
@iIdDuAnId uniqueidentifier
AS
BEGIN
	 DECLARE @iExist int = (SELECT COUNT(*) FROM NH_DA_KHLCNhaThau WHERE Id = @iId)
	 IF(@iExist = 0)
	 BEGIN
		SELECT tbl.ID, tbl.iLoaiDuToan as IdLoaiDuToan,tbl.iLoaiDuToan,tbl.fGiaTriEUR,tbl.fGiaTriNgoaiTeKhac,tbl.fGiaTriUSD,tbl.fGiaTriVND,
		tbl.iID_DonViQuanLyID,tbl.iID_TiGiaID,tbl.iID_KHTT_NhiemVuChiID,tbl.sTenChuongTrinh,tbl.fTiGiaNhap,tbl.bIsActive,tbl.bIsGoc,
		tbl.bIsKhoa,tbl.bIsXoa,tbl.dNgayQuyetDinh,tbl.dNgaySua,tbl.dNgayTao,tbl.sNguoiTao,tbl.sNguoiSua,tbl.dNgayXoa,tbl.sNguoiXoa,
		tbl.iID_DuAnID,tbl.iID_DuToanGocID,tbl.iID_MaDonViQuanLy,tbl.iID_ParentID,tbl.iID_QDDauTuID,tbl.iID_TiGiaUSD_EURID,
		tbl.iID_TiGiaUSD_NgoaiTeKhacID,tbl.iID_TiGiaUSD_VNDID,tbl.iLanDieuChinh,tbl.iLoai,tbl.sMaNgoaiTeKhac,tbl.sMota,
		 Case When iLoaiDuToan = 1 then Concat(N'Dự toán mua sắm: ',sSoQuyetDinh)  
              When iLoaiDuToan = 2 then Concat(N'Dự toán đặt hàng: ',sSoQuyetDinh)  else sSoQuyetDinh end as sSoQuyetDinh
		FROM NH_DA_DuToan as tbl
		LEFT JOIN (SELECT DISTINCT iID_DuToanID FROM NH_DA_KHLCNhaThau WHERE bIsActive = 1 AND iID_DuToanID IS NOT NULL) as dt on tbl.ID = dt.iID_DuToanID
		WHERE tbl.iID_DuAnID = @iIdDuAnId and tbl.bIsActive=1
	 END
	 ELSE
	 BEGIN
		SELECT dt.*
		FROM NH_DA_KHLCNhaThau as tbl
		INNER JOIN NH_DA_DuToan as dt on tbl.iID_DuToanID = dt.ID
	 END
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_donvi]    Script Date: 06/02/2023 11:08:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_binhquan_donvi] 
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
		   fSoLDHD
	FROM
	  (SELECT iID_MaDonVi ,
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
			  fSoLDHD = sum(fSoLDHD)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
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
			  MoTa = iID_MaDonVi + ' - '+ sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork
		 AND iLoai=1
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId))) ) AS dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY ctct.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_thang]    Script Date: 06/02/2023 11:08:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_binhquan_thang]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100)
AS
BEGIN

	SELECT '' AS iID_MaDonVi ,
		   '' AS sTenDonVi ,
		   CASE
			WHEN t.iThangQuy = 0 THEN N'Đầu năm' 
			ELSE CONCAT('Tháng ', t.iThangQuy) 
		   END AS MoTa ,
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
		   fSoLDHD
	FROM
	  (SELECT iThangQuy ,
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
			  fSoLDHD = sum(fSoLDHD)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND (@Period IS NULL
			  OR iThangQuy in
				(SELECT *
				 FROM f_split(@Period)))
		 AND sKyHieu = '700'
	   GROUP BY iThangQuy) AS ctct
	RIGHT JOIN
	  (SELECT Item AS iThangQuy
	   FROM f_split(@Period)) AS t ON t.iThangQuy=ctct.iThangQuy
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_chitiet_donvi]    Script Date: 06/02/2023 11:08:55 AM ******/
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
			isnull(fSoLDHD, 0) as fSoLDHD 

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
			  fSoLDHD = sum(fSoLDHD)
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
		   0 as fSoLDHD
	FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2'
	ORDER BY sKyHieu, iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_donvi]    Script Date: 06/02/2023 11:08:55 AM ******/
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
		   fSoLDHD
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
			  fSoLDHD = sum(fSoLDHD)
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
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 06/02/2023 11:08:55 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_raquan]    Script Date: 06/02/2023 11:08:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_raquan]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(50)
AS
BEGIN
	SELECT 
		Huu,
		PhucVien,
		XuatNgu,
		ThoiViec,
		dv.iID_MaDonVi as Id_DonVi,
		dv.sTenDonVi as TenDonVi,
		dv.MoTa
	FROM 
	(
		SELECT 
			iID_MaDonVi
			,Huu = SUM(CASE WHEN sKyHieu = '310' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)
			,PhucVien = SUM(CASE WHEN sKyHieu = '331' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)
			,XuatNgu = SUM(CASE WHEN sKyHieu = '320' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)
			,ThoiViec = SUM(CASE WHEN sKyHieu = '330' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)
		FROM 
			NS_QS_ChungTuChiTiet
		WHERE  
			iNamLamViec=@YearOfWork
			AND (@AgencyId is null or iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId)))
			AND bHangCha=0
			AND iThangQuy in (select * from f_split(@Period))
		GROUP BY
			iID_MaDonVi
		HAVING SUM(CASE WHEN sKyHieu = '310' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END) <>0
			OR SUM(CASE WHEN sKyHieu = '331' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
			OR SUM(CASE WHEN sKyHieu = '320' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
			OR SUM(CASE WHEN sKyHieu = '330' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
	) AS ctct
	RIGHT JOIN 
	(
		SELECT 
			iID_MaDonVi, 
			sTenDonVi,	
			MoTa = iID_MaDonVi + ' - ' + sTenDonVi 
		FROM DonVi 
		WHERE 
			iTrangThai=1 
			AND iNamLamViec = @yearOfWork 
			AND iLoai=1 
			AND (@AgencyId IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId)))
	) AS dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY Id_DonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_raquan_thangtruoc]    Script Date: 06/02/2023 11:08:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_raquan_thangtruoc]
	@Month int,
	@YearOfWork int,
	@YearOfWorkBefore int,
	@AgencyId nvarchar(max)
AS
BEGIN
	SELECT 
		Huu = SUM(CASE WHEN sKyHieu = '310' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoCNVQP + fSoLDHD) ELSE 0 END)
		,PhucVien = SUM(CASE WHEN sKyHieu = '331' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoCNVQP + fSoLDHD) ELSE 0 END)
		,XuatNgu = SUM(CASE WHEN sKyHieu = '320' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoCNVQP + fSoLDHD) ELSE 0 END)
		,ThoiViec = SUM(CASE WHEN sKyHieu = '330' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoCNVQP + fSoLDHD) ELSE 0 END)
		,'' AS Id_DonVi
		,'' AS TenDonVi
		,'' AS MoTa
	FROM 
		NS_QS_ChungTuChiTiet
	WHERE  
		(
			(@Month = 1 and iNamLamViec = @YearOfWork and iThangQuy = 0) 
			OR 
			( 
				@Month <> 1 
				AND 
				(
					(iNamLamViec = @YearOfWork and iThangQuy < @Month) 
					OR (iNamLamViec < @YearOfWorkBefore and iThangQuy <= 12)
				)
			)
		)
		AND (@AgencyId is null OR iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId)))
	HAVING SUM(CASE WHEN sKyHieu = '310' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoCNVQP + fSoLDHD) ELSE 0 END) <>0
		OR SUM(CASE WHEN sKyHieu = '331' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
		OR SUM(CASE WHEN sKyHieu = '320' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
		OR SUM(CASE WHEN sKyHieu = '330' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + fSoQNCN + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_thuongxuyen]    Script Date: 06/02/2023 11:08:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_thuongxuyen]
	@month1 int,
	@month2 int,
	@month3 int,
	@month4 int,
	@yearOfWork int,
	@agencyId nvarchar(max)
AS
BEGIN
	SELECT 
		rSQ1 = SUM(CASE WHEN ((iThangQuy <=@month1 AND iNamLamViec=@yearOfWork))  THEN fSoSQ ELSE 0 END),
		rSQ2 = SUM(CASE WHEN ((iThangQuy <=@month2 AND iNamLamViec=@yearOfWork))   THEN fSoSQ ELSE 0 END),
		rSQ3 = SUM(CASE WHEN ((iThangQuy <=@month3 AND iNamLamViec=@yearOfWork))   THEN fSoSQ ELSE 0 END),
		rSQ4 = SUM(CASE WHEN ((iThangQuy <=@month4 AND iNamLamViec=@yearOfWork))  THEN fSoSQ ELSE 0 END),
				
		rQNCN1 = SUM(CASE WHEN ((iThangQuy <=@month1 AND iNamLamViec=@yearOfWork))   THEN fSoQNCN ELSE 0 END),
		rQNCN2 = SUM(CASE WHEN ((iThangQuy <=@month2 AND iNamLamViec=@yearOfWork))   THEN fSoQNCN ELSE 0 END),
		rQNCN3 = SUM(CASE WHEN ((iThangQuy <=@month3 AND iNamLamViec=@yearOfWork))   THEN fSoQNCN ELSE 0 END),
		rQNCN4 = SUM(CASE WHEN ((iThangQuy <=@month4 AND iNamLamViec=@yearOfWork))  THEN fSoQNCN ELSE 0 END),
				
		rCNVHD1 = SUM(CASE WHEN ((iThangQuy <=@month1 AND iNamLamViec=@yearOfWork))   THEN fSoCNVHD ELSE 0 END),
		rCNVHD2 = SUM(CASE WHEN ((iThangQuy <=@month2 AND iNamLamViec=@yearOfWork))   THEN fSoCNVHD ELSE 0 END),
		rCNVHD3 = SUM(CASE WHEN ((iThangQuy <=@month3 AND iNamLamViec=@yearOfWork))   THEN fSoCNVHD ELSE 0 END),
		rCNVHD4 = SUM(CASE WHEN ((iThangQuy <=@month4 AND iNamLamViec=@yearOfWork))  THEN fSoCNVHD ELSE 0 END),
				
		rHSQCS1 = SUM(CASE WHEN ((iThangQuy <=@month1 AND iNamLamViec=@yearOfWork))   THEN fSoHSQCS ELSE 0 END),
		rHSQCS2 = SUM(CASE WHEN ((iThangQuy <=@month2 AND iNamLamViec=@yearOfWork))   THEN fSoHSQCS ELSE 0 END),
		rHSQCS3 = SUM(CASE WHEN ((iThangQuy <=@month3 AND iNamLamViec=@yearOfWork))   THEN fSoHSQCS ELSE 0 END),
		rHSQCS4 = SUM(CASE WHEN ((iThangQuy <=@month4 AND iNamLamViec=@yearOfWork))  THEN fSoHSQCS ELSE 0 END),
		dv.iID_MaDonVi as Id_DonVi,
		dv.sTenDonVi as TenDonVi,
		dv.MoTa,
		iNamLamViec
	FROM (
		SELECT
			fSoSQ = SUM(CASE WHEN sKyHieu = '700' THEN fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa+ fSoTrungTa + fSoThuongTa + fSoDaiTa+ fSoTuong ELSE 0 END),
			fSoQNCN = SUM(CASE WHEN sKyHieu = '700' THEN fSoQNCN ELSE 0 END),
			fSoCNVHD = SUM(CASE WHEN sKyHieu = '700' THEN fSoCNVQP + fSoLDHD ELSE 0 END),
			fSoHSQCS = SUM(CASE WHEN sKyHieu = '700' THEN fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi ELSE 0 END),
			iID_MaDonVi,
			iThangQuy,
			iNamLamViec
		from  
			NS_QS_ChungTuChiTiet 
		WHERE 
			((iThangQuy <= @month4 AND iNamLamViec = @yearOfWork))
			and (@agencyId is null or iID_MaDonVi in (select * from f_split(@agencyId)))
		group by  
			iID_MaDonVi, iThangQuy, iNamLamViec 
	) as qs
	RIGHT JOIN 
		(
			SELECT iID_MaDonVi, sTenDonVi, MoTa = iID_MaDonVi + ' - ' + sTenDonVi from DonVi 
			WHERE iTrangThai=1 and iNamLamViec = @yearOfWork and iLoai=1 and (@AgencyId is null or iID_MaDonVi in (select * from f_split(@AgencyId)))) as dv
	on dv.iID_MaDonVi = qs.iID_MaDonVi
	
	group by dv.iID_MaDonVi, dv.sTenDonVi, dv.MoTa, iNamLamViec
	ORDER BY dv.iID_MaDonVi		
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 06/02/2023 11:08:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
	@YearOfWork int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@LNS nvarchar(max),
	@Dvt int,
	@IsInTongHop bit, 
	@IKhoi int
AS
BEGIN
declare @strChungTu nvarchar (500)
set @strChungTu=  (select sTongHop + ',' as 'data()' from NS_QT_ChungTu where  iID_MaDonVi in ( SELECT * FROM f_split(@AgencyId))  FOR XML PATH(''));
	
	SELECT * INTO #tempthongtridonvi
		FROM
		  (SELECT iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
			 AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
		   GROUP BY iID_MaDonVi)AS ct 
		-- lay ten don vi
		JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id,
				  iKhoi
		   FROM DonVi
		   WHERE iTrangThai = 1
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
	
	if (@IsInTongHop = 0 OR @strChungTu = '')
		select * from #tempthongtridonvi;
	else if (@IsInTongHop = 1 AND EXISTS (SELECT * FROM #tempthongtridonvi where iKhoi is not null))
		select * from #tempthongtridonvi where @IKhoi = -1 OR iKhoi = @IKhoi;
	else

	SELECT *
		FROM
		  (SELECT ctct.iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet ctct inner join ns_qt_Chungtu ct on  ctct.iID_QTChungTu =  ct.iID_QTChungTu 
		   WHERE ctct.iNamLamViec = @YearOfWork
			 AND ctct.iID_MaNguonNganSach = @BudgetSource
			 --AND (@AgencyId IS NULL OR ctct.iID_MaDonVi in  (select DonVi.iID_MaDonVi from DonVi  where DonVi.iID_Parent in ( SELECT * FROM f_split(@AgencyId))))
			 AND ct.bdatonghop = 1
			 AND (@QuarterMonth IS NULL OR ctct.iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
			 AND ct.sSoChungTu in (select * from f_split(Replace(@strChungTu, ' ', '')))
		   GROUP BY ctct.iID_MaDonVi)AS ct 
		-- lay ten don vi
		--LEFT JOIN
		INNER JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id,
				  iKhoi
		   FROM DonVi
		   WHERE iTrangThai = 1
		     AND (@IKhoi = -1 OR iKhoi = @IKhoi)
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
END
;
;
GO


-- Thêm danh mục đơn vị thông tri ban hành là Phòng tài chính
IF NOT EXISTS (SELECT * FROM DanhMuc WHERE iID_MaDanhMuc = 'DV_THONGTRI_BANHANH' AND iNamLamViec = 2023)
INSERT INTO DanhMuc VALUES(NEWID(), GETDATE(), GETDATE(), 'DV_THONGTRI_BANHANH', 2023, 0, 1, NULL, NULL, N'Phòng Tài chính', N'Cấp quản lý tài chính', 'admin', 'admin', N'Cấp quản lý tài chính', 'DM_CauHinh', NULL)

UPDATE DanhMuc
SET sTen = N'Cấp quản lý tài chính', sGiaTri = N'Phòng Tài chính', sMoTa = N'Cấp quản lý tài chính'
WHERE iID_MaDanhMuc = 'DV_THONGTRI_BANHANH' AND iNamLamViec = 2022 

UPDATE DM_ChuKy 
SET LoaiDVBanHanh1 = 2, LoaiDVBanHanh2 = 3
WHERE Id_Code = 'rptNS_QuyetToan_ThongTri_LNS'